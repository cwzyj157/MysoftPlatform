using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mysoft.Map.Extensions.DAL;
using SmokingTestLibrary;

namespace SmokingTest.DAL
{
	/// <summary>
	/// 测试SQL语句执行
	/// </summary>
	[SmokingTestLibrary.Test(InNewThread = true, RunTimes = 30)]
	[NUnit.Framework.TestFixture]
	public class TestExecCPQuery : IDisposable
	{
		/// <summary>
		/// 
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 0)]
		[NUnit.Framework.Test]
		public void _Smoking_0_InitTestTable()
		{
			// 先删除要测试的数据表，避免数据干扰。
			DropTestTable();


			// 重新创建要测试的表
			@"CREATE TABLE [dbo].[TestTable](
				[RowId] [int] IDENTITY(1,1) NOT NULL,
				RowGuid uniqueidentifier  NOT NULL,
				[RowString] [nvarchar](50) NOT NULL,
				[RowImage] image,
				[RowNull] [nvarchar](50)
			) ON [PRIMARY]"
				.AsCPQuery().ExecuteNonQuery();
		}


		private void DropTestTable()
		{
			var query = "select top 1 object_id from sys.objects where name = 'TestTable'".AsCPQuery();
			long objectId = query.ExecuteScalar<long>();

			if( objectId > 0 )
				"drop table [TestTable]".AsCPQuery().ExecuteNonQuery();

		}


		void IDisposable.Dispose()
		{
			DropTestTable();
		}
		private void ClearTable()
		{
			CPQuery.Format("delete from TestTable").ExecuteNonQuery();

			int count = CPQuery.Format("select count(*) from TestTable").ExecuteScalar<int>();
			Assert.AreEqual(count, 0);
		}

		/// <summary>
		/// CRUD操作测试
		/// </summary>
		/// <remarks>
		/// <list type="bullet">
		/// <item><description>测试新增场景</description></item>
		/// <item><description>测试更新场景</description></item>
		/// <item><description>测试查询列表场景</description></item>
		/// </list>
		/// </remarks>
		[TestMethod(Order = 1, RunTimes=3)]
		[NUnit.Framework.Test]
		public void _Smoking_1_SimpleCRUD()
		{
			ClearTable();

			Guid guid1 = Guid.NewGuid();
			Guid guid2 = Guid.NewGuid();

			string s1 = "aaaaaaaaa";
			string s2 = "bbbbbbbbb";
			string s3 = "cccccccccc";

			CPQuery.Format("insert into TestTable(RowGuid, RowString) values({0}, {1})", guid1, s1).ExecuteNonQuery();
			CPQuery.Format("insert into TestTable(RowGuid, RowString) values({0}, {1})", guid2, s2).ExecuteNonQuery();

			List<Guid> list = "select RowGuid from TestTable order by RowId".AsCPQuery().FillScalarList<Guid>();
			if( list.Count != 2 || list[0] != guid1 || list[1] != guid2 )
				throw new AssertFailedException("FillScalarList 并没有返回二条记录，或者结果不是期望的。");



			CPQuery.Format("update TestTable set RowString = {0} where RowGuid = {1}", s3, guid1).ExecuteNonQuery();

			string r1 = CPQuery.Format("select RowString from TestTable where RowGuid = {0}", guid1).ExecuteScalar<string>();
			Assert.AreEqual(r1, s3);
		}

		/// <summary>
		/// 测试共享连接场景
		/// </summary>
		[TestMethod(Order = 2)]
		[NUnit.Framework.Test]
		public void _Smoking_2_TestShareConnect()
		{
			ClearTable();

			using( ConnectionScope scope = new ConnectionScope() ) {
				var query = "insert into TestTable(RowGuid, RowString) values(".AsCPQuery()
					+ Guid.NewGuid()
					+ "," + (QueryParameter)"dddddddddd" + ")";
				query.ExecuteNonQuery();


				query = "insert into TestTable(RowGuid, RowString) values(".AsCPQuery()
					+ Guid.NewGuid() 
					+ "," + "eeeeeeeeee".AsQueryParameter() + ")";
				query.ExecuteNonQuery();
			}


			int count = CPQuery.Format("select count(*) from TestTable").ExecuteScalar<int>();
			Assert.AreEqual(count, 2);


		}

		/// <summary>
		/// 测试启动事务,且不提交场景
		/// </summary>
		[TestMethod(Order = 2)]
		[NUnit.Framework.Test]
		public void _Smoking_2_TestTranscationNotCommit()
		{
			ClearTable();

			using( ConnectionScope scope = new ConnectionScope(TransactionMode.Required) ) {
				var query = "insert into TestTable(RowGuid, RowString) values(".AsCPQuery()
					+ Guid.NewGuid()
					+ "," + (QueryParameter)"dddddddddd" + ")";
				query.ExecuteNonQuery();


				query = "insert into TestTable(RowGuid, RowString) values(".AsCPQuery()
					+ Guid.NewGuid()
					+ "," + "eeeeeeeeee".AsQueryParameter() + ")";
				query.ExecuteNonQuery();

				// 由于没有调用 scope.Commit();
				// 当离开 using 语句块后，所以提交将被回滚。
			}


			int count = CPQuery.Format("select count(*) from TestTable").ExecuteScalar<int>();
			Assert.AreEqual(count, 0);

		}

	   

		/// <summary>
		/// 测试启动事务,且提交场景
		/// </summary>
		[TestMethod(Order = 2)]
		[NUnit.Framework.Test]
		public void _Smoking_2_TestTranscationWithCommit()
		{
			ClearTable();

			using( ConnectionScope scope = new ConnectionScope(TransactionMode.Required) ) {
				var query = "insert into TestTable(RowGuid, RowString) values(".AsCPQuery()
					+ Guid.NewGuid()
					+ "," + (QueryParameter)"dddddddddd" + ")";
				query.ExecuteNonQuery();


				query = "insert into TestTable(RowGuid, RowString) values(".AsCPQuery()
					+ Guid.NewGuid()
					+ "," + "eeeeeeeeee".AsQueryParameter() + ")";
				query.ExecuteNonQuery();

				scope.Commit();
			}


			int count = CPQuery.Format("select count(*) from TestTable").ExecuteScalar<int>();
			Assert.AreEqual(count, 2);

		}

		/// <summary>
		/// 测试参数化查询
		/// </summary>
		/// <remarks>
		/// <list type="bullet">
		/// <item><description>测试新增语句场景</description></item>
		/// <item><description>测试查询语句场景</description></item>
		/// </list>
		/// </remarks>
		[TestMethod(Order = 3)]
		[NUnit.Framework.Test]
		public void _Smoking_3_TestParameterSQL()
		{
			var product = new {
				ProductName = Guid.NewGuid().ToString(),
				CategoryID = 1,
				Unit = "个",
				UnitPrice = 12.36,
				Quantity = 25,
				Remark = "ssdfsdfsdf",
			};

			CPQuery.From("INSERT INTO Products(ProductName, CategoryID, Unit, UnitPrice, Remark, Quantity) VALUES(@ProductName, @CategoryID, @Unit, @UnitPrice, @Remark, @Quantity)", product).ExecuteNonQuery();


			CPQuery query = "SELECT ProductName FROM Products WHERE ProductName = ".AsCPQuery() + product.ProductName.AsQueryParameter();
			string productName = query.ExecuteScalar<string>();

			Assert.AreEqual<string>(productName, product.ProductName);
		}

		/// <summary>
		/// 
		/// </summary>
		[TestMethod(Order = 4)]
		[NUnit.Framework.Test]
		public void _Smoking_4_TestParameterNull()
		{
			string str = null;

			var parameter1 = new {
				RowGuid = Guid.NewGuid(),
				RowNull = str
			};

			CPQuery.From("insert into TestTable(RowGuid, RowNull, RowString) VALUES(@RowGuid, @RowNull, 'abc')", parameter1).ExecuteNonQuery();

			var parameter2 = new {
				RowGuid = parameter1.RowGuid
			};
			str = CPQuery.From("SELECT RowNull FROM TestTable WHERE RowGUID=@RowGUID", parameter2).ExecuteScalar<string>();

			Assert.AreEqual<bool>(true, string.IsNullOrEmpty(str));

			var parameter3 = new {
				RowGuid = Guid.NewGuid(),
				RowNull = DBNull.Value
			};

			CPQuery.From("insert into TestTable(RowGuid, RowNull, RowString) VALUES(@RowGuid, @RowNull, 'abc')", parameter3).ExecuteNonQuery();

			var parameter4 = new {
				RowGuid = parameter3.RowGuid
			};

			str = CPQuery.From("SELECT RowNull FROM TestTable WHERE RowGUID=@RowGUID", parameter2).ExecuteScalar<string>();

			Assert.AreEqual<bool>(true, string.IsNullOrEmpty(str));
		}

	}

}
