using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mysoft.Map.Extensions.DAL;
using System.Data.SqlClient;
using SmokingTestLibrary;
using SmokingTest.CS.Entity;
namespace SmokingTest.DAL
{
	/// <summary>
	/// 各个类型不为NULL的类型场景
	/// </summary>
	 [SmokingTestLibrary.Test(InNewThread = true, RunTimes = 1)]
	 [NUnit.Framework.TestFixture]
	public class TestNotNullDataType
	{
		 /// <summary>
		 /// 数据清除
		 /// </summary>
		private void ClearData()
		{
			"TRUNCATE TABLE TestNotNullDataTypeTable".AsCPQuery().ExecuteNonQuery();
		}
		 /// <summary>
		 /// 初始化测试表
		 /// </summary>
		[SmokingTestLibrary.TestMethod(Order = 0, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void InitData()
		{
			var query = "SELECT OBJECT_ID('TestNotNullDataTypeTable')".AsCPQuery();
			var objectId = query.ExecuteScalar<long>();
			if( objectId > 0 )
				"drop table [TestNotNullDataTypeTable]".AsCPQuery().ExecuteNonQuery();
			SqlHelper.ExecuteTSql(Properties.Resources.TestNotNullDataTypeScript);
		}

		/// <summary>
		/// 通过插入测试不为空类型场景
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 1, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void TestInsert()
		{
			
			ClearData();
			TestNotNullDataTypeTable model = new TestNotNullDataTypeTable();
			model.SetPropertyDefaultValue("A");
			model.B = new byte[] { 0x11, 0x22, 0x33 };
			model.C = true;
			model.D = "Test";
			model.E = DateTime.Now;
			model.F = DateTime.Now;
			model.SetPropertyDefaultValue("G");
			model.G = 5.5m;
			model.SetPropertyDefaultValue("H");
			model.H = 5.6f;
			model.I = new byte[] { 0x44, 0x55, 0x66 };
			model.J = 555;
			model.K = 4.4m;
			model.L = "test2";
			model.M = "test3";
			model.N = 5.5m;
			model.O = "test7";
			model.P = "test8";
			model.SetPropertyDefaultValue("Q");
			model.Q = 5.5f;
			model.SetPropertyDefaultValue("R");
			model.R = DateTime.Now;
			model.S = 444;
			model.SetPropertyDefaultValue("T");
			model.T = 333;
			model.U = 5.5m;
			model.V = "test4";
			model.SetPropertyDefaultValue("W");
			model.W = 44;
			model.X = Guid.NewGuid();
			model.Y = "test5";
			model.Z = "test6";
			model.SetPropertyDefaultValue("A1");
			model.A1 = DateTimeOffset.Now;
			model.B1 = "<data val=\"test\" />";
			model.C1 = new byte[] { 0x11, 0x22, 0x33 };
			model.D1 = DateTime.Now;
			model.E1 = new byte[] { 0x0 };
			model.SetPropertyDefaultValue("F1");
			model.F1 = DateTime.Now.TimeOfDay;
			model.SetPropertyDefaultValue("Guid");
			model.Guid = Guid.NewGuid();
			Assert.AreEqual<int>(model.Insert(), 1);
	
			
		}
		/// <summary>
		/// 通过修改测试不为空类型场景
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 2, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void TestUpdate()
		{
			TestInsert();
			TestNotNullDataTypeTable model = new TestNotNullDataTypeTable();
			model.SetPropertyDefaultValue("A");
			model.SetPropertyDefaultValue("C");
			model.D = "Test568471";
			model.Guid = "select top 1 [guid] from TestNotNullDataTypeTable".AsCPQuery().ExecuteScalar<Guid>();
			model.Update();
			TestNotNullDataTypeTable testmode=String.Format("select A,C,D from TestNotNullDataTypeTable where guid='{0}'", model.Guid).AsCPQuery().ToSingle<TestNotNullDataTypeTable>();
			Assert.AreEqual<bool>(model.C, false);
			Assert.AreEqual<long>(model.A, testmode.A);
			Assert.AreEqual<String>(model.D, testmode.D);
		}
		 /// <summary>
		/// 测试用SetPropertyZero赋值后，再对属性进行赋值是否正确
		 /// </summary>
		[SmokingTestLibrary.TestMethod(Order = 3, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void TestInsert_1()
		{
			TestInsert();
			TestNotNullDataTypeTable model = new TestNotNullDataTypeTable();
			model.SetPropertyDefaultValue("A");
			model.A=5; 
			model.B = new byte[] { 0x11, 0x22, 0x33 };
			model.C = true;
			model.D = "Test";
			model.E = DateTime.Now;
			model.F = DateTime.Now;
			model.G = 5.5m;
			model.H = 5.6f;
			model.I = new byte[] { 0x44, 0x55, 0x66 };
			model.J = 555;
			model.K = 4.4m;
			model.L = "test2";
			model.M = "test3";
			model.N = 5.5m;
			model.O = "test7";
			model.P = "test8";
			model.Q = 5.5f;
			model.R = DateTime.Now;
			model.S = 444;
			model.T = 333;
			model.U = 5.5m;
			model.V = "test4";
			model.W = 44;
			model.X = Guid.NewGuid();
			model.Y = "test5";
			model.Z = "test6";
			model.A1 = DateTimeOffset.Now;
			model.B1 = "<data val=\"test\" />";
			model.C1 = new byte[] { 0x11, 0x22, 0x33 };
			model.D1 = DateTime.Now;
			model.E1 = new byte[] { 0x0 };
			model.F1 = DateTime.Now.TimeOfDay;
			model.Guid = Guid.NewGuid();
			model.Insert();
			long i = String.Format("select A from TestNotNullDataTypeTable where Guid='{0}'", model.Guid.ToString()).AsCPQuery().ExecuteScalar<long>();
			Assert.AreEqual<long>(i, model.A);
		}
		/// <summary>
		/// 测试不为NULL类型使用时间戳更新的场景
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 4)]
		[NUnit.Framework.Test]
		public void ConcurrencyUpdate_TimeStamp()
		{
			TestInsert();
			var QueryNewRow = String.Format("select top 1 * from TestNotNullDataTypeTable").AsCPQuery();
			TestNotNullDataTypeTable t2 = QueryNewRow.ToSingle<TestNotNullDataTypeTable>();
			TestNotNullDataTypeTable t1 = QueryNewRow.ToSingle<TestNotNullDataTypeTable>();
			t1.D = "zr";
			int effectRows = t1.Update(t2, ConcurrencyMode.TimeStamp);
			//第一次修改成功
			Assert.AreEqual(effectRows, 1);
			bool ok = false;
			try {
				t1.D = "zr";
				//第二次因为用的是原来的时间戳,所以修改失败
				t1.Update(t2, ConcurrencyMode.TimeStamp);
			}
			catch( Mysoft.Map.Extensions.Exception.OptimisticConcurrencyException ) {
				ok = true;
			}
			Assert.AreEqual(ok, true);

			t2 = QueryNewRow.ToSingle<TestNotNullDataTypeTable>();
			t1.D = "zr";
			effectRows = t1.Update(t2, ConcurrencyMode.TimeStamp);
			Assert.AreEqual(effectRows, 1);

		}
		/// <summary>
		///验证利用原始值更新,更新对象和原始对象是同一个对象的时候会抛出异常
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 5)]
		[NUnit.Framework.Test]
		public void ConcurrencyUpdate_OriginalValueEx()
		{
			TestInsert();
			var QueryNewRow = String.Format("select top 1 * from TestNotNullDataTypeTable").AsCPQuery();
			TestNotNullDataTypeTable t1 = QueryNewRow.ToSingle<TestNotNullDataTypeTable>();
			t1.A = 28;
			t1.D = "bbbbbbbbb";
			bool ok = false;
			try {
				t1.Update(t1, ConcurrencyMode.OriginalValue);
			}
			catch(ArgumentException e) {
				if( e.Message == "用于并发检测的原始对象不能是当前对象。" )
				ok = true;
			}
			Assert.AreEqual(ok, true);
		}
	}
}
