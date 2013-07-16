using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Data.SqlClient;

using SmokingTestLibrary;

using Mysoft.Map.Extensions;
using Mysoft.Map.Extensions.Workflow;
using Mysoft.Map.Extensions.DAL;
using Mysoft.Map.Extensions.Xml;

namespace SmokingTest.DAL
{
	/// <summary>
	/// 批量写入数据
	/// </summary>
	[SmokingTestLibrary.Test(InNewThread = false, RunTimes = 2)]
	[NUnit.Framework.TestFixture]
	public class TestBulkCopy
	{
		/// <summary>
		/// 初始化表
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 0, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void InitData()
		{
			var query = "SELECT OBJECT_ID('TestBulkCopy')".AsCPQuery();
			var objectId = query.ExecuteScalar<long>();
			if( objectId > 0 ) {
				"drop table [TestBulkCopy]".AsCPQuery().ExecuteNonQuery();
			}

			string sql = @"CREATE TABLE TestBulkCopy
			(
				GuidVal UNIQUEIDENTIFIER,
				IntVal int,
				DateTimeVal DATETIME,
				StringVal NVARCHAR(128),
				MoneyVal MONEY,
				FloatVal Float
			)";

			sql.AsCPQuery().ExecuteNonQuery();
		}

		/// <summary>
		/// 初始化表
		/// </summary>
		[TestMethod(Order = 0, RunTimes = 3)]
		[NUnit.Framework.Test]
		public void TestCopy()
		{
			string sql = "DELETE FROM TestBulkCopy";
			sql.AsCPQuery().ExecuteNonQuery();

			//准备数据
			DataTable dt = new DataTable();
			dt.Columns.Add("GuidVal", typeof(Guid));
			#region ...更多字段
			dt.Columns.Add("IntVal", typeof(int));
			dt.Columns.Add("DateTimeVal", typeof(DateTime));
			dt.Columns.Add("StringVal", typeof(string));
			dt.Columns.Add("MoneyVal", typeof(decimal));
			dt.Columns.Add("FloatVal", typeof(double));
			#endregion
			
			for( int i = 0; i < 10000; i++ ) {
				DataRow row = dt.NewRow();
				row["GuidVal"] = Guid.NewGuid();
				#region ...更多列数据
				Random rnd = new Random();
				row["IntVal"] = rnd.Next();
				row["DateTimeVal"] = DateTime.Now;
				row["StringVal"] = "TestValue";
				row["MoneyVal"] = 100m * (decimal)rnd.NextDouble();
				row["FloatVal"] = 100 * rnd.NextDouble();
				#endregion
				dt.Rows.Add(row);
			}

			//使用事务
			using(ConnectionScope scope = new ConnectionScope( TransactionMode.Required )){
				//创建SqlBulkCopy对象
				SqlBulkCopy bulkCopy = scope.CreateSqlBulkCopy(SqlBulkCopyOptions.FireTriggers);
				//设置写入目标表
				bulkCopy.DestinationTableName = "TestBulkCopy";
				//写入数据
				bulkCopy.WriteToServer(dt);
				//提交事务
				scope.Commit();
			}

			sql = "SELECT COUNT(*) FROM TestBulkCopy";
			int count = sql.AsCPQuery().ExecuteScalar<int>();
			Assert.AreEqual(count, 10000);


			sql = "DELETE FROM TestBulkCopy";
			sql.AsCPQuery().ExecuteNonQuery();

			//不使用事务
			using( ConnectionScope scope = new ConnectionScope() ) {

				//创建SqlBulkCopy对象
				SqlBulkCopy bulkCopy = scope.CreateSqlBulkCopy(SqlBulkCopyOptions.FireTriggers);
				//设置写入目标表
				bulkCopy.DestinationTableName = "TestBulkCopy";
				//写入数据
				bulkCopy.WriteToServer(dt);

			}

			sql = "SELECT COUNT(*) FROM TestBulkCopy";
			count = sql.AsCPQuery().ExecuteScalar<int>();
			Assert.AreEqual(count, 10000);
		}
	}
}
