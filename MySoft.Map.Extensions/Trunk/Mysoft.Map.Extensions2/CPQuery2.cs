using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mysoft.Map.Extensions.DAL
{
	/// <summary>
	/// CPQuery的扩展类，提供一些增强功能。
	/// </summary>
	public static class CPQuery2
	{
		/// <summary>
		/// 调用 CPQuery.Format方法，快速生成一个CPQuery对象，使用方法与 string.Format类似。
		/// </summary>
		/// <param name="format">包含{0},{1}..这些占位符的SQL语句</param>
		/// <param name="parameters">占位符对应的参数值</param>
		/// <returns>一个CPQuery对象</returns>
		public static CPQuery Format(string format, params object[] parameters)
		{
			return CPQuery.Format(format, parameters);
		}



	}
}
