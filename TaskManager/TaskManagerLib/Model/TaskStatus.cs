using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskManagerLib.Model
{
	public enum TaskStatus
	{
		/// <summary>
		/// 未处理
		/// </summary>
		Ready,
		/// <summary>
		/// 开发中
		/// </summary>
		Coding,
		/// <summary>
		/// 测试中
		/// </summary>
		Test,
		/// <summary>
		/// 已完成
		/// </summary>
		Finished
	}
}
