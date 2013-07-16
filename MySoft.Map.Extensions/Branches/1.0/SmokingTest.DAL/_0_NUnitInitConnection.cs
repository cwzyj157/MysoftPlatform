using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace SmokingTest.DAL
{
	/// <summary>
	/// 初始化连接字符串
	/// NUnit入口
	/// </summary>
	[NUnit.Framework.TestFixture]
	public class _0_NUnitInitConnection
	{
		static _0_NUnitInitConnection()
		{
			string connectionString = (from s in System.IO.File.ReadAllLines(@"c:\SmokingTest.DAL.connectionString.txt")
									   where s.StartsWith(";") == false
									   select s
										).First();

			Mysoft.Map.Extensions.Initializer.UnSafeInit(connectionString);
		}
		/// <summary>
		/// 
		/// </summary>
		[NUnit.Framework.Test]
		public void NUnitInitConnectionDescription()
		{
			Console.WriteLine("NUnit连接初始化");
		}

	}
}
