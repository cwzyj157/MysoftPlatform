using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SmokingTestLibrary;

using Mysoft.Map.Extensions.DAL;
using System.Data.SqlClient;

namespace SmokingTest.DAL
{
	/// <summary>
	/// 测试事务
	/// </summary>
	[SmokingTestLibrary.Test(InNewThread = true, RunTimes = 5)]
	[NUnit.Framework.TestFixture]
	public class TestTransaction
	{
		/// <summary>
		/// 初始化数据
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 0, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void InitData()
		{
			var query = "select top 1 object_id from sys.objects where name = 'TestTransaction'".AsCPQuery();
			var objectId = query.ExecuteScalar<long>();

			if( objectId > 0 )
				"drop table [TestTransaction]".AsCPQuery().ExecuteNonQuery();


			// 重新创建要测试的表
			@"CREATE TABLE [dbo].[TestTransaction](
				[RowId] [int] IDENTITY(1,1) NOT NULL,
				RowGuid uniqueidentifier  NOT NULL,
				[RowString] [nvarchar](50) NOT NULL,
			) ON [PRIMARY]"
				.AsCPQuery().ExecuteNonQuery();
		}

		/// <summary>
		/// 测试简单事务场景,事务不提交
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 1)]
		[NUnit.Framework.Test]
		public void TestTransaction1()
		{
			CPQuery.Format("DELETE FROM TestTransaction").ExecuteNonQuery();
			using( ConnectionScope scope0 = new ConnectionScope(TransactionMode.Required) ) {
				TestTransaction1Inner1();
			}
			//由于scope0没有提交.此时应该是0
			int count = CPQuery.Format("SELECT COUNT(*) FROM TestTransaction").ExecuteScalar<int>();
			Assert.AreEqual<int>(0, count);
		}

		/// <summary>
		/// 测试事务嵌套场景,以外层事务为准
		/// </summary>
		public void TestTransaction1Inner1()
		{
			CPQuery.Format("INSERT INTO TestTransaction(RowGuid,RowString) VALUES({0},{1})", Guid.NewGuid(), "Test1").ExecuteNonQuery();
			using( ConnectionScope scope1 = new ConnectionScope(TransactionMode.Required) ) {
				CPQuery.Format("INSERT INTO TestTransaction(RowGuid,RowString) VALUES({0},{1})", Guid.NewGuid(), "Test2").ExecuteNonQuery();
				using( ConnectionScope scope2 = new ConnectionScope() ) {
					CPQuery.Format("INSERT INTO TestTransaction(RowGuid,RowString) VALUES({0},{1})", Guid.NewGuid(), "Test3").ExecuteNonQuery();
					//scope2.Commit();
				}
				scope1.Commit();
			}
		}

		/// <summary>
		/// 测试事务嵌套场景,使用Required枚举,以外层事务为准
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 2)]
		[NUnit.Framework.Test]
		public void TestTransaction2()
		{
			CPQuery.Format("DELETE FROM TestTransaction").ExecuteNonQuery();
			using( ConnectionScope scope0 = new ConnectionScope(TransactionMode.Required) ) {
				CPQuery.Format("INSERT INTO TestTransaction(RowGuid,RowString) VALUES({0},{1})", Guid.NewGuid(), "Test1").ExecuteNonQuery();
				using( ConnectionScope scope1 = new ConnectionScope(TransactionMode.Required) ) {
					CPQuery.Format("INSERT INTO TestTransaction(RowGuid,RowString) VALUES({0},{1})", Guid.NewGuid(), "Test2").ExecuteNonQuery();
					using( ConnectionScope scope2 = new ConnectionScope() ) {
						CPQuery.Format("INSERT INTO TestTransaction(RowGuid,RowString) VALUES({0},{1})", Guid.NewGuid(), "Test3").ExecuteNonQuery();
					}
					// 此时故意没有调用Commit()，以外层ConnectionScope为准。
				}
				scope0.Commit();
			}
			//scope0提交.此时应该是3
			int count = CPQuery.Format("SELECT COUNT(*) FROM TestTransaction").ExecuteScalar<int>();
			Assert.AreEqual<int>(3, count);
		}

		/// <summary>
		/// 测试事务嵌套场景,外层不提交
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 2)]
		[NUnit.Framework.Test]
		public void TestTransaction3()
		{
			int count = 0;
			CPQuery.Format("DELETE FROM TestTransaction").ExecuteNonQuery();

			using( ConnectionScope scope0 = new ConnectionScope() ) {
				CPQuery.Format("INSERT INTO TestTransaction(RowGuid,RowString) VALUES({0},{1})", Guid.NewGuid(), "Test1").ExecuteNonQuery();
				count = CPQuery.Format("SELECT COUNT(*) FROM TestTransaction").ExecuteScalar<int>();
				Assert.AreEqual<int>(1, count);

				using( ConnectionScope scope1 = new ConnectionScope(TransactionMode.Required) ) {
					CPQuery.Format("INSERT INTO TestTransaction(RowGuid,RowString) VALUES({0},{1})", Guid.NewGuid(), "Test2").ExecuteNonQuery();
					using( ConnectionScope scope2 = new ConnectionScope() ) {
						CPQuery.Format("INSERT INTO TestTransaction(RowGuid,RowString) VALUES({0},{1})", Guid.NewGuid(), "Test3").ExecuteNonQuery();
					}
					// 此时故意没有调用Commit()，因此当前作用域的语句将被回滚。
				}
			}
			count = CPQuery.Format("SELECT COUNT(*) FROM TestTransaction").ExecuteScalar<int>();
			Assert.AreEqual<int>(1, count);
		}

		/// <summary>
		/// 测试事务嵌套场景,外层不启用事务,内层启用事务
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 2)]
		[NUnit.Framework.Test]
		public void TestTransaction4()
		{
			int count = 0;
			CPQuery.Format("DELETE FROM TestTransaction").ExecuteNonQuery();

			using( ConnectionScope scope0 = new ConnectionScope() ) {
				CPQuery.Format("INSERT INTO TestTransaction(RowGuid,RowString) VALUES({0},{1})", Guid.NewGuid(), "Test1").ExecuteNonQuery();
				count = CPQuery.Format("SELECT COUNT(*) FROM TestTransaction").ExecuteScalar<int>();
				Assert.AreEqual<int>(1, count);

				using( ConnectionScope scope1 = new ConnectionScope(TransactionMode.Required) ) {
					CPQuery.Format("INSERT INTO TestTransaction(RowGuid,RowString) VALUES({0},{1})", Guid.NewGuid(), "Test2").ExecuteNonQuery();
					using( ConnectionScope scope2 = new ConnectionScope() ) {
						CPQuery.Format("INSERT INTO TestTransaction(RowGuid,RowString) VALUES({0},{1})", Guid.NewGuid(), "Test3").ExecuteNonQuery();
					}
					scope1.Commit();
				}
			}
			count = CPQuery.Format("SELECT COUNT(*) FROM TestTransaction").ExecuteScalar<int>();
			Assert.AreEqual<int>(3, count);
		}

		/// <summary>
		/// 测试事务嵌套,多段事务场景
		/// 即开启事务(内层包含继承事务),不开启事务,开启事务(内层包含继承事务)
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 3)]
		[NUnit.Framework.Test]
		public void TestTransaction5()
		{
			using( ConnectionScope scope0 = new ConnectionScope() ) {
				CPQuery.Format("DELETE FROM TestTransaction").ExecuteNonQuery();

				CPQuery.Format("INSERT INTO TestTransaction(RowGuid,RowString) VALUES({0},{1})", Guid.NewGuid(), "Test1").ExecuteNonQuery();

				int count = CPQuery.Format("SELECT COUNT(*) FROM TestTransaction").ExecuteScalar<int>();
				Assert.AreEqual<int>(1, count);

				using( ConnectionScope scope1 = new ConnectionScope(TransactionMode.Required) ) {
					CPQuery.Format("INSERT INTO TestTransaction(RowGuid,RowString) VALUES({0},{1})", Guid.NewGuid(), "Test2").ExecuteNonQuery();
					using( ConnectionScope scope2 = new ConnectionScope() ) {
						CPQuery.Format("INSERT INTO TestTransaction(RowGuid,RowString) VALUES({0},{1})", Guid.NewGuid(), "Test3").ExecuteNonQuery();
						//scope2.Commit();	
					}
					scope1.Commit();
					//此时事务已提交.应该是3条
					count = CPQuery.Format("SELECT COUNT(*) FROM TestTransaction").ExecuteScalar<int>();
					Assert.AreEqual<int>(3, count);
				}

				CPQuery.Format("INSERT INTO TestTransaction(RowGuid,RowString) VALUES({0},{1})", Guid.NewGuid(), "Test4").ExecuteNonQuery();
				count = CPQuery.Format("SELECT COUNT(*) FROM TestTransaction").ExecuteScalar<int>();
				Assert.AreEqual<int>(4, count);

				using( ConnectionScope scope1 = new ConnectionScope(TransactionMode.Required) ) {
					CPQuery.Format("INSERT INTO TestTransaction(RowGuid,RowString) VALUES({0},{1})", Guid.NewGuid(), "Test5").ExecuteNonQuery();
					using( ConnectionScope scope2 = new ConnectionScope() ) {
						CPQuery.Format("INSERT INTO TestTransaction(RowGuid,RowString) VALUES({0},{1})", Guid.NewGuid(), "Test6").ExecuteNonQuery();
						//scope2.Commit();
					}
					// 此时故意没有调用Commit()，因此当前作用域的语句将被回滚。
				}
				count = CPQuery.Format("SELECT COUNT(*) FROM TestTransaction").ExecuteScalar<int>();
				Assert.AreEqual<int>(4, count);
			}
		}

		/// <summary>
		/// 测试不执行任何SQL.直接提交,是否能正确的抛出InvalidOperationException异常
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 4)]
		[NUnit.Framework.Test]
		public void TestTransactionException1()
		{
			using( ConnectionScope scope = new ConnectionScope() ) {
				try {
					scope.Commit();
				}
				catch( InvalidOperationException ex ) {
					//没有启用事务时,Commit将会抛出异常.这里捕获到异常说明函数正常.
					string message = ex.Message;
				}
			}
		}

		/// <summary>
		/// 测试没有开启事务,是否能正确的抛出InvalidOperationException异常
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 5)]
		[NUnit.Framework.Test]
		public void TestTransactionException2()
		{
			using( ConnectionScope scope1 = new ConnectionScope(TransactionMode.Required) ) {
				"SELECT 1".AsCPQuery().ExecuteNonQuery();
				using( ConnectionScope scope2 = new ConnectionScope() ) {
					try {
						scope2.Commit();
					}
					catch( InvalidOperationException ex ) {
						//即使父级启用事务,本级没有启用事务,Commit也会抛出异常.这里捕获到异常说明函数正常.
						string message = ex.Message;
					}
				}
			}
		}

		/// <summary>
		/// 测试开启事务,但不执行任何SQL场景
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 6)]
		[NUnit.Framework.Test]
		public void TestTransactionException3()
		{
			//这里不要删除.就是验证这种场景是否正常
			using( ConnectionScope scope1 = new ConnectionScope(TransactionMode.Required) ) {

			}
		}
		/// <summary>
		/// 测试开启事务,但不执行任何SQL场景,提交事务
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 7)]
		[NUnit.Framework.Test]
		public void TestTransactionException4()
		{
			//这里不要删除.就是验证这种场景是否正常
			using( ConnectionScope scope1 = new ConnectionScope(TransactionMode.Required) ) {
				scope1.Commit();
			}
		}
		/// <summary>
		/// 测试开启事务,但不执行任何SQL场景,提交事务
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 8)]
		[NUnit.Framework.Test]
		public void TestTransactionException5()
		{
			//这里不要删除.就是验证这种场景是否正常
			using( ConnectionScope scope1 = new ConnectionScope(TransactionMode.Required) ) {
				using( ConnectionScope scope2 = new ConnectionScope(TransactionMode.Required) )
				{
					scope1.Commit();
				}
				scope1.Commit();
			}
		}
		/// <summary>
		/// 测试不开启事务,但不执行任何SQL场景,提交
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 9)]
		[NUnit.Framework.Test]
		public void TestTransactionException6()
		{

			TestPackage.AssertException<InvalidOperationException>(()=>{
			//这里不要删除.就是验证这种场景是否正常
			using( ConnectionScope scope1 = new ConnectionScope() ) {
				scope1.Commit();
			}
			},"当前的作用域不支持事务操作");
		}

		/// <summary>
		/// 测试开启嵌套事务，不执行SQL的场景
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 10)]
		[NUnit.Framework.Test]
		public void TestTransactionException7()
		{
			using( ConnectionScope scope1 = new ConnectionScope(TransactionMode.Required) ) {
				using( ConnectionScope scope2 = new ConnectionScope(TransactionMode.Required) ) {
					scope1.Commit();
				}
			}
		}
		/// <summary>
		/// 测试开启嵌套事务，不执行SQL的场景，不提交事务
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 8)]
		[NUnit.Framework.Test]
		public void TestTransactionException8()
		{
			using( ConnectionScope scope1 = new ConnectionScope(TransactionMode.Required) ) {
				using( ConnectionScope scope2 = new ConnectionScope(TransactionMode.Required) ) {
					
				}
			}
		}
	}
}
