using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmokingTestLibrary
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple=false, Inherited =false)]
	public class TestAttribute : Attribute
	{
		/// <summary>
		/// 是否在新的线程中运行
		/// </summary>
		public bool InNewThread { get; set; }

		/// <summary>
		/// 执行次数
		/// </summary>
		public int RunTimes { get; set; }

		
	}

	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
	public class TestMethodAttribute : Attribute
	{
		/// <summary>
		/// 执行顺序
		/// </summary>
		public int Order { get; set; }


		/// <summary>
		/// 执行次数
		/// </summary>
		public int RunTimes { get; set; }


		public int TimeOut { get; set; }
	}
}
