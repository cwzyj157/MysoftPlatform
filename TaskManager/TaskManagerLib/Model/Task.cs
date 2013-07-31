using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Attributes;
using TaskManagerLib.Exceptions;

namespace TaskManagerLib.Model
{
	public class Task
	{
		[MongoId]
		public string TaskGuid { get; set; }

		/// <summary>
		/// 任务编号
		/// </summary>
		public string TaskNo { get; set; }
		/// <summary>
		/// 任务标题
		/// </summary>
		public string TaskTitle { get; set; }
		/// <summary>
		/// 客户名称
		/// </summary>
		public string CustomerName { get; set; }
		/// <summary>
		/// ERP 版本
		/// </summary>
		public string ErpVersion { get; set; }
		/// <summary>
		/// MAP 版本
		/// </summary>
		public string MapVersion { get; set; }
		/// <summary>
		/// ABU小组的PM
		/// </summary>
		public string AbuPM { get; set; }
		/// <summary>
		/// 开发人员
		/// </summary>
		public AdUserName Developer { get; set; }
		/// <summary>
		/// 测试人员
		/// </summary>
		public AdUserName Tester { get; set; }
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CreateTime { get; set; }
		/// <summary>
		/// 开始时间
		/// </summary>
		public DateTime Start { get; set; }
		/// <summary>
		/// 预计完成时间
		/// </summary>
		public DateTime ExpectEnd { get; set; }
		/// <summary>
		/// 实际完成时间
		/// </summary>
		public DateTime ActualEnd { get; set; }
		/// <summary>
		/// 工作量
		/// </summary>
		public double Workload { get; set; }
		/// <summary>
		/// 状态
		/// </summary>
		public TaskStatus Status { get; set; }
		/// <summary>
		/// 备注
		/// </summary>
		public string Comment { get; set; }

		/// <summary>
		/// 任务类别
		/// </summary>
		public string TaskType { get; set; }


		public bool IsBug { get; set; }


		public string StatusText
		{
			get { return this.Status.ToString(); }
		}


		public void ValidIsOK()
		{
			if( string.IsNullOrEmpty(this.TaskTitle) )
				throw new DataFieldNullException("TaskTitle");

			if( string.IsNullOrEmpty(this.CustomerName) )
				throw new DataFieldNullException("CustomerName");
		}
	}
}
