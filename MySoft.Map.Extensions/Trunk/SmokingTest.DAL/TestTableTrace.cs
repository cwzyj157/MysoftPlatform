using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mysoft.Map.Extensions.DAL;
using SmokingTestLibrary;

namespace SmokingTest.DAL
{
	/// <summary>
	/// 测试Map中原有的Trace功能
	/// </summary>
	[SmokingTestLibrary.Test(InNewThread = true, RunTimes = 2)]
	[NUnit.Framework.TestFixture]
	public class TestTableTrace
	{
		/// <summary>
		/// 初始化
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 0)]
		[NUnit.Framework.Test]
		public void InitData()
		{
			string table1 = @"CREATE TABLE [mySQLLog]
			(
				[LogGUID] [uniqueidentifier],
				[ExeDateTime] [datetime] ,
				[ExeUser] [varchar] (50), 
				[ExeIP] [varchar] (16), 
				[ExePage] [varchar] (100), 
				[ExeSQL] [text],
				[ErrorMessage] [text] 
			)";

			string table2 = @"CREATE TABLE TestTrace1
			(
				ColInt int,
				ColString varchar(36)
			)";

			string table3 = @"CREATE TABLE TestTrace2
			(
				ColInt int,
				ColString varchar(36)
			)";

			object value = null;
			//From的第二个参数null不要删除.验证该函数是否正常
			value = CPQuery.From("SELECT OBJECT_ID('mySQLLog')", null).ExecuteScalar<object>();
			if( value == null ) {
				CPQuery.From(table1, null).ExecuteNonQuery();
			}

			value = CPQuery.From("SELECT OBJECT_ID('TestTrace1')").ExecuteScalar<object>();
			if( value == null ) {
				CPQuery.From(table2, null).ExecuteNonQuery();
			}

			value = CPQuery.From("SELECT OBJECT_ID('TestTrace2')").ExecuteScalar<object>();
			if( value == null ) {
				CPQuery.From(table3, null).ExecuteNonQuery();
			}

			Mysoft.Map.Extensions.TableTrace.InitTrace();
		}

		/// <summary>
		/// 测试Trace的表是否正确被监听到
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 1)]
		[NUnit.Framework.Test]
		public void TestTrace()
		{
			int count = 0;

			ClearTable();
			"DELETE FROM TestTrace1 WHERE ColInt < 0".AsCPQuery().ExecuteNonQuery();
			count = GetLogCount();

			//此时应该监控到1条日志,即TestTrace1
			Assert.AreEqual<int>(count, 1);

			ClearTable();
			"DELETE FROM TestTrace2 WHERE ColInt < 0".AsCPQuery().ExecuteNonQuery();
			count = GetLogCount();
			
			//此时应该监控到1条日志,即TestTrace2
			Assert.AreEqual<int>(count, 1);

			ClearTable();
			try {
				//此处不使用事务,且SQL是错误的.日志表应该写入1条,并且包含异常信息字段
				"DELETE FROM TestTrace2 WHERE a < 0".AsCPQuery().ExecuteNonQuery();
			}
			catch {
			}
			count = GetLogCount();

			//此时应该监控到0条日志
			Assert.AreEqual<int>(count, 1);

			ClearTable();
			try {
				//此处使用事务,且SQL是错误的.日志表应该跟随事务一起回滚.
				using( ConnectionScope scope = new ConnectionScope(TransactionMode.Required) ) {
					"DELETE FROM TestTrace2 WHERE a < 0".AsCPQuery().ExecuteNonQuery();
				}
			}
			catch {
			}
			count = GetLogCount();

			//由于同宿主共用事务,此时应该监控到0条日志
			Assert.AreEqual<int>(count, 0);
		}

		private void ClearTable()
		{
			"DELETE FROM mySQLLog".AsCPQuery().ExecuteNonQuery();
		}

		private int GetLogCount()
		{
			return "SELECT COUNT(*) FROM mySQLLog".AsCPQuery().ExecuteScalar<int>();
		}
	
	}
}
