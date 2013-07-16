using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SmokingTestLibrary;

using Mysoft.Map.Extensions.DAL;
using SmokingTest.DAL;
using SmokingTest.CS.Entity;
using Mysoft.Map.Extensions.Exception;

namespace SmokingTest.DAL
{
	/// <summary>
	/// 测试增,删,改场景
	/// </summary>
	[SmokingTestLibrary.Test(InNewThread = true, RunTimes = 2)]
	[NUnit.Framework.TestFixture]
	public class TestCUD 
	{

		/// <summary>
		/// 初始化表
		/// </summary>
		[TestMethod(Order = 0, RunTimes = 1)]
		public void InitData()
		{
			var query = "SELECT OBJECT_ID('Test_CUD1')".AsCPQuery();
			var objectId = query.ExecuteScalar<long>();
			if( objectId > 0 )
				"drop table [Test_CUD1]".AsCPQuery().ExecuteNonQuery();

			string sql;
			sql = @"CREATE TABLE Test_CUD1
					(
						PK INT IDENTITY(1,1),
						seqGuid_Val UNIQUEIDENTIFIER DEFAULT(newsequentialid()),
						guid_Val UNIQUEIDENTIFIER,
						int_Val int,
						str_Val varchar(128),
						dtm_Val datetime,
						money_Val money,
						float_Val float,
						ts_val TIMESTAMP,
						CONSTRAINT [PK_Test_CUD1] PRIMARY KEY CLUSTERED 
						(
							[PK] ASC
						)
					)";

			sql.AsCPQuery().ExecuteNonQuery();
			
		}

		private void AssertEntity(TestCUD1 cud1, TestCUD1 cud2)
		{
			Assert.AreEqual<Guid?>(cud1.GuidVal, cud2.GuidVal);
			Assert.AreEqual<int?>(cud1.IntVal, cud2.IntVal);
			Assert.AreEqual<string>(cud1.StrVal, cud2.StrVal);
			Assert.AreEqual<string>(cud1.DtmVal.Value.ToString("yyyyMMddHHmmss"), cud2.DtmVal.Value.ToString("yyyyMMddHHmmss"));
			if( Math.Abs(cud1.MoneyVal.Value - cud2.MoneyVal.Value) > 0.01m ) {
				Assert.AreEqual<bool>(true, false);
			}
			if( Math.Abs(cud1.FloatVal.Value - cud2.FloatVal.Value) > 0.01 ) {
				Assert.AreEqual<bool>(true, false);
			}
		}

		private TestCUD1 Insert()
		{
			Random rnd = new Random(Guid.NewGuid().GetHashCode());

			TestCUD1 cud1 = new TestCUD1();

			cud1.GuidVal = Guid.NewGuid();
			cud1.IntVal = rnd.Next();
			cud1.StrVal = "你好" + rnd.Next().ToString();
			cud1.DtmVal = DateTime.Now;
			cud1.MoneyVal = (decimal)rnd.NextDouble() * 10000;
			cud1.FloatVal = rnd.NextDouble() * 10000;

			int count = cud1.Insert();

			Assert.AreEqual<int>(count, 1);

			return cud1;
		}

		private void ClearData()
		{
			"DELETE FROM [Test_CUD1]".AsCPQuery().ExecuteNonQuery();
		}

		/// <summary>
		/// 测试插入数据
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 1, RunTimes = 2)]
		[NUnit.Framework.Test]
		public void TestInsert()
		{
			ClearData();

			TestCUD1 cud1 = Insert();

			int count = "SELECT COUNT(*) FROM Test_CUD1".AsCPQuery().ExecuteScalar<int>();

			Assert.AreEqual<int>(count, 1);

			TestCUD1 cud2 = CPQuery.From("SELECT * FROM Test_CUD1").ToSingle<TestCUD1>();

			AssertEntity(cud1, cud2);

		}

		/// <summary>
		/// 测试更新数据
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 2, RunTimes = 2)]
		[NUnit.Framework.Test]
		public void TestUpdate()
		{
			ClearData();

			Insert();

			//原对象,并将数据库的值更新为固定值,后续用来还原
			TestCUD1 cudRaw = CPQuery.From("SELECT * FROM Test_CUD1").ToSingle<TestCUD1>();

			//拿到新对象
			TestCUD1 cudNew = CPQuery.From("SELECT * FROM Test_CUD1").ToSingle<TestCUD1>();

			//修改数据库
			"UPDATE Test_CUD1 SET Str_Val='更新个值'".AsCPQuery().ExecuteNonQuery();

			//再次修改
			cudNew.StrVal = "更新后的值2";

			bool bEx = false;
			string message;
			try {
				cudNew.Update(cudRaw, ConcurrencyMode.TimeStamp);
			}
			catch( OptimisticConcurrencyException ex ) {
				message = ex.Message;
				//并发保存应该抛出此异常
				bEx = true;
			}
			Assert.AreEqual<bool>(true, bEx);

			bEx = false;
			try {
				cudNew.Update(cudRaw, ConcurrencyMode.OriginalValue);
			}
			catch( OptimisticConcurrencyException ex ) {
				message = ex.Message;
				//并发保存应该抛出此异常
				bEx = true;
			}
			Assert.AreEqual<bool>(true, bEx);
		}

		/// <summary>
		/// 测试删除
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 2, RunTimes = 2)]
		[NUnit.Framework.Test]
		public void TestDelete()
		{
			ClearData();

			Insert();

			TestCUD1 cud1 = CPQuery.From("SELECT * FROM Test_CUD1").ToSingle<TestCUD1>();

			int count = cud1.Delete();
			Assert.AreEqual<int>(count, 1);

			Insert();
			//原对象
			TestCUD1 cudRaw = CPQuery.From("SELECT * FROM Test_CUD1").ToSingle<TestCUD1>();
			
			"UPDATE Test_CUD1 SET Str_Val='更新个值'".AsCPQuery().ExecuteNonQuery();

			string message;
			bool bEx = false;
			try {
				cudRaw.Delete(ConcurrencyMode.TimeStamp);
			}
			catch( OptimisticConcurrencyException ex ) {
				message = ex.Message;
				//并发保存应该抛出此异常
				bEx = true;
			}
			Assert.AreEqual<bool>(bEx, true);

			bEx = false;
			try {
				cudRaw.Delete(ConcurrencyMode.OriginalValue);
			}
			catch( OptimisticConcurrencyException ex ) {
				message = ex.Message;
				//并发保存应该抛出此异常
				bEx = true;
			}
			Assert.AreEqual<bool>(bEx, true);

		}

	}
}
