using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mysoft.Map.Extensions.DAL;

namespace SmokingTest.DAL
{
	/// <summary>
	/// 初始化连接,兼容NUNIT
	/// </summary>
	public static class AsmInit
	{
		/// <summary>
		/// 初始化方法
		/// </summary>
		public static void Init()
		{
			string connectionString = (from s in System.IO.File.ReadAllLines(@"c:\SmokingTest.DAL.connectionString.txt")
									   where s.StartsWith(";") == false
									   select s
									).First();

			Console.WriteLine("Current ConnectionStirng: " + connectionString);
			Mysoft.Map.Extensions.Initializer.UnSafeInit(connectionString);

			Program.SqlVeresion = CPQuery.Format("select (@@microsoftversion / 0x01000000);").ExecuteScalar<int>();
		}
	}
}
