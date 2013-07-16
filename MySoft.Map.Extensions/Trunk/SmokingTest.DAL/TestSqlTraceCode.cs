using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmokingTestLibrary;

using Mysoft.Map.Extensions.DAL;
using SmokingTest.DAL;
using SmokingTest.CS.Entity;
using Mysoft.Map.Extensions.Exception;
using System.Data.SqlClient;
using System.Reflection;

namespace SmokingTest.DAL
{


	/// <summary>
	/// 验证增，修改参数按需配置,生成的CPQuery是否有效
	///  验证自增类型场景
	/// 验证InvalidOperationException是否正确抛出
    /// </summary>
	[SmokingTestLibrary.Test(InNewThread = true, RunTimes = 2)]
	[NUnit.Framework.TestFixture]
    public class TestSqlTraceCode
    {
		/// <summary>
		/// 清除数据
		/// </summary>
		private void ClearData()
		{
			"TRUNCATE TABLE TestSqlTrace".AsCPQuery().ExecuteNonQuery();
		}
		/// <summary>
		/// 初始化测试表
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 0, RunTimes = 1)]
		[NUnit.Framework.Test]
        public void InitData()
        {
            var query = "SELECT OBJECT_ID('TestSqlTrace')".AsCPQuery();
            var objectId = query.ExecuteScalar<long>();
            if (objectId > 0)
                "drop table [TestSqlTrace]".AsCPQuery().ExecuteNonQuery();
            SqlHelper.ExecuteTSql(Properties.Resources.TestSqlTraceScript);

        }
		/// <summary>
		/// 通过插入测试参数是否配置正确
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 1, RunTimes = 3)]
		[NUnit.Framework.Test]
		public void TestParameterInsert()
        {
            ClearData();
            TestSqlTrace model = new TestSqlTrace();
            byte[] BinaryDatastr=model.BinaryData = Encoding.Default.GetBytes("0x123213123");
			int DatabaseIDstr= model.DatabaseID = 1;
			String NTUserNamestr = model.NTUserName = "zhour'";
			CPQuery cp = model.GetCPQuery(3, model);
			int i= cp.ExecuteNonQuery();
			Assert.AreEqual<int>(i, 1);
			SqlCommand comm = cp.GetCommand();
			//验证生成的SQL语句中有赋值的属性
			Assert.AreNotEqual<int>(comm.CommandText.IndexOf("BinaryData"),-1);
			Assert.AreNotEqual<int>(comm.CommandText.IndexOf("DatabaseID"), -1);
			Assert.AreNotEqual<int>(comm.CommandText.IndexOf("NTUserName"), -1);
			//验证生成的SQL语句中没有没赋值的属性
			Assert.AreEqual<int>(comm.CommandText.IndexOf("@SqlText"), -1);
			//验证参数生成的值是否正确
			foreach( SqlParameter parameter in comm.Parameters ) {
				if( parameter.ParameterName == "@BinaryData" ) { Assert.AreEqual<string>(Encoding.Default.GetString(parameter.Value as byte[]), Encoding.Default.GetString(BinaryDatastr)); }
				if( parameter.ParameterName == "@DatabaseID" ) { Assert.AreEqual<string>(parameter.Value.ToString(), DatabaseIDstr.ToString()); }
				if( parameter.ParameterName == "@NTUserName" ) { Assert.AreEqual<string>(parameter.Value.ToString(), NTUserNamestr); }
			}
			//验证表名是否正确
			Assert.AreNotEqual<int>(comm.CommandText.IndexOf("TestSqlTrace"),-1);
        }
		/// <summary>
		/// 通过修改测试参数是否配置正确
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 2, RunTimes = 3)]
		[NUnit.Framework.Test]
		public void TestParameterUpdate()
		{
			TestSqlTrace model = new TestSqlTrace();
			byte[] BinaryDatastr = model.BinaryData = Encoding.Default.GetBytes("0x456345");
			int DatabaseIDstr = model.DatabaseID = 1;
			String NTUserNamestr = model.NTUserName = "mys-11";
			model.Id = 1;
			CPQuery cp = model.GetCPQuery(3, model, null);
			int i = cp.ExecuteNonQuery();
			Assert.AreEqual<int>(i, 1);
			SqlCommand comm = cp.GetCommand();

			//验证生成的SQL语句中有赋值的属性
			Assert.AreNotEqual<int>(comm.CommandText.IndexOf("BinaryData"), -1);
			Assert.AreNotEqual<int>(comm.CommandText.IndexOf("DatabaseID"), -1);
			Assert.AreNotEqual<int>(comm.CommandText.IndexOf("NTUserName"), -1);
			//验证生成的SQL语句中没有没赋值的属性
			Assert.AreEqual<int>(comm.CommandText.IndexOf("@SqlText"), -1);
			//验证参数生成的值是否正确
			foreach( SqlParameter parameter in comm.Parameters ) {
				if( parameter.ParameterName == "@BinaryData" ) { Assert.AreEqual<string>(Encoding.Default.GetString(parameter.Value as byte[]), Encoding.Default.GetString(BinaryDatastr)); }
				if( parameter.ParameterName == "@DatabaseID" ) { Assert.AreEqual<string>(parameter.Value.ToString(), DatabaseIDstr.ToString()); }
				if( parameter.ParameterName == "@NTUserName" ) { Assert.AreEqual<string>(parameter.Value.ToString(), NTUserNamestr); }
			}
			//验证表名是否正确
			Assert.AreNotEqual<int>(comm.CommandText.IndexOf("TestSqlTrace"), -1);
		}

		/// <summary>
		/// 测试InvalidOperationException 是否正常抛出
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 3, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void TestInvalidOperationException()
		{
			TestSqlTrace model = new TestSqlTrace();
			byte[] BinaryDatastr = model.BinaryData = Encoding.Default.GetBytes("0x456345");
			int DatabaseIDstr = model.DatabaseID = 1;
			String NTUserNamestr = model.NTUserName = "mys-11";
			//model.Id = 1;
			bool ok = false;
			try {
				model.Update();
			}
			catch(InvalidOperationException )
			{
				ok = true;
			}
			Assert.AreEqual<bool>(ok, true);
		}

	
	
    }
}
