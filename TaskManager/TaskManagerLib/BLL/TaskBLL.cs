using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagerLib.Model;
using TaskManagerLib.DAL;
using MongoDB;
using TaskManagerLib.Model.DTO;
using TaskManagerLib.Common;
using TaskManagerLib.Exceptions;


namespace TaskManagerLib.BLL
{
	public class TaskBLL : BaseBLL
	{
		private void AutoSetTaskStatus(Task task)
		{
			if( task.Developer != null && string.IsNullOrEmpty(task.Developer.ShortName) == false && task.Start.Year > 1000 ) {
				if( task.Tester != null && string.IsNullOrEmpty(task.Tester.ShortName) == false )
					task.Status = TaskStatus.Test;
				else
					task.Status = TaskStatus.Coding;
			}
			else
				task.Status = TaskStatus.Ready;
		}

		public void Insert(Task task)
		{
			task.ValidIsOK();

			if( task.Status == TaskStatus.Finished )
				throw new InvalidOperationException();


			task.TaskGuid = Guid.NewGuid().ToString();
			task.CreateTime = DateTime.Now;

			AutoSetTaskStatus(task);

			using( MyMongoDb mm = new MyMongoDb() ) {
				mm.GetCollection<Task>().Insert(task);
			}
		}


		public void Delete(string taskGuid)
		{
			if( string.IsNullOrEmpty(taskGuid) )
				throw new ArgumentNullException("taskGuid");

			using( MyMongoDb mm = new MyMongoDb() ) {
				mm.GetCollection<Task>().Remove(x => x.TaskGuid == taskGuid);
			}
		}

		public void Update(Task task)
		{
			task.ValidIsOK();

			if( string.IsNullOrEmpty(task.TaskGuid) )
				throw new ArgumentNullException("task.TaskGuid");

			if( this.CurrentUser.IsInRole(UserRole.Admin) == false ) 
				AutoSetTaskStatus(task);


			using( MyMongoDb mm = new MyMongoDb() ) {
				var collection = mm.GetCollection<Task>();

				Task t1 = collection.FindOne(x => x.TaskGuid == task.TaskGuid);
				if( t1 == null )
					throw new ArgumentOutOfRangeException("task.TaskGuid");

				// 不能更新创建时间
				task.CreateTime = t1.CreateTime;

				collection.Update(task, x => x.TaskGuid == task.TaskGuid);
			}
		}

		public void SetFinished(string taskGuid)
		{
			if( string.IsNullOrEmpty(taskGuid) )
				throw new ArgumentNullException("taskGuid");

			using( MyMongoDb mm = new MyMongoDb() ) {
				var collection = mm.GetCollection<Task>();

				Task t1 = collection.FindOne(x => x.TaskGuid == taskGuid);
				if( t1 == null )
					throw new ArgumentOutOfRangeException("task.TaskGuid");

				t1.Status = TaskStatus.Finished;
				t1.ActualEnd = DateTime.Now;

				collection.Update(t1, x => x.TaskGuid == taskGuid);
			}
		}

		public Task GetOne(string taskGuid)
		{
			if( string.IsNullOrEmpty(taskGuid) )
				throw new ArgumentNullException("taskGuid");

			using( MyMongoDb mm = new MyMongoDb() ) {
				return mm.GetCollection<Task>().FindOne(x => x.TaskGuid == taskGuid);
			}
		}


		public List<Task> GetActiveTasks()
		{
			using( MyMongoDb mm = new MyMongoDb() ) {
				return (from t in mm.GetCollection<Task>().Linq()
						where t.Status != TaskStatus.Finished
						orderby t.CreateTime
						select t).ToList();
			}
		}

		/// <summary>
		/// 查询一段时间登记的任务，查询参考字段为CreateTime
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public List<Task> Query(QueryTaskParam param)
		{
			using( MyMongoDb mm = new MyMongoDb() ) {

				var query = from t in mm.GetCollection<Task>().Linq()
							where t.CreateTime >= param.Start && t.CreateTime < param.End.AddDays(1)
							orderby t.CreateTime
							select t;

				return query.GetPagingList<Task>(param);
			}
		}



	}
}
