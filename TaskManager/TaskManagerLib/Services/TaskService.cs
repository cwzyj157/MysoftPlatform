using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyMVC;
using TaskManagerLib.Model;
using TaskManagerLib.BLL;
using TaskManagerLib.Model.DTO;
using TaskManagerLib.Model.View;
using TaskManagerLib.Common;

//using TaskManagerLib.DAL;
//using MongoDB;
using Mysoft.Map.Excel;

namespace TaskManagerLib.Services
{
	public class TaskService
	{
		private TaskBLL _bll = new TaskBLL();



		[Action(Verb = "post")]
		[Authorize(Roles = "Admin, PM, Developer, Tester")]
		public int Insert(Task task)
		{
			_bll.Insert(task);
			return 1;
		}

		[Action(Verb = "post")]
		[Authorize(Roles = "Admin, PM")]
		public int Delete(string taskGuid)
		{
			_bll.Delete(taskGuid);
			return 1;
		}

		[Action(Verb = "post")]
		[Authorize(Roles = "Admin, PM, Developer, Tester")]
		public int Update(Task task)
		{
			_bll.Update(task);
			return 1;
		}

		[Action(Verb = "post")]
		[Authorize(Roles = "Admin, PM")]
		public int SetFinished(string taskGuid)
		{
			_bll.SetFinished(taskGuid);
			return 1;
		}

		[Action]
		public object GetOne(string taskGuid)
		{
			Task task = _bll.GetOne(taskGuid);
			return new JsonResult(task);
		}

		[Action]
		public object GetActiveTasks()
		{
			List<Task> list = _bll.GetActiveTasks();
			var result = new GridResult<Task>(list, list.Count);
			return new JsonResult(result);
		}

		[Action]
		public object Query(QueryTaskParam param)
		{
			List<Task> list = _bll.Query(param);
			var result = new GridResult<Task>(list, list.Count);
			return new JsonResult(result);
		}
		[Action]
		public bool ExportExcel(QueryTaskParam param)
		{
			List<Task> list = _bll.Query(param);

			// 导出Excel

			ExcelWookbook wookBook = new ExcelWookbook();
			string excelPath = System.IO.Path.Combine(System.Web.HttpRuntime.AppDomainAppPath, @"ExcelFiles\"+));

			Random


			wookBook.LoadXls("");


		}


		//[Action]
		//public string ConvertData()
		//{
		//    using( MyMongoDb mm = new MyMongoDb() ) {
		//        var collection = mm.GetCollection<Task>();

		//        List<Task> list = (from t in collection.Linq() select t).ToList();

		//        foreach( Task task in list ) {
		//            if( string.IsNullOrEmpty(task.TaskType) )
		//                task.TaskType = (task.IsBug ? "BUG" : "需求");


		//            collection.Update(task, x => x.TaskGuid == task.TaskGuid);
		//        }
		//    }


		//    return "OK";
		//}

	}
}
