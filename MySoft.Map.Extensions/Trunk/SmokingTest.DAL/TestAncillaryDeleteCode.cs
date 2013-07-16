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
	/// 并发删除的扩展场景
	/// </summary>
	[SmokingTestLibrary.Test(InNewThread = true, RunTimes = 1)]
	[NUnit.Framework.TestFixture]
	public class TestAncillaryDeleteCode
	{
		private CPQuery _queryNewRow;
		/// <summary>
		/// 初始化
		/// </summary>
		public TestAncillaryDeleteCode()
		{
			_queryNewRow = String.Format("select top 1 * from  TestAncillaryDelete").AsCPQuery();
		}
	
		/// <summary>
		/// 数据清除
		/// </summary>
		private void ClearData()
		{
			TestPackage.ClearData("TestAncillaryDelete");
		}
		/// <summary>
		/// 初始化测试表
		/// </summary>
		[SmokingTestLibrary.TestMethod]
		[NUnit.Framework.Test]
		public void _0_InitData()
		{
			TestPackage.InitData("TestAncillaryDelete", Properties.Resources.TestAncillaryDeleteScript);
		}
		/// <summary>
		/// 插入数据
		/// </summary>
		[SmokingTestLibrary.TestMethod]
		[NUnit.Framework.Test]
		public void _1_Insert()
		{
			ClearData();
			TestAncillaryDelete t1 = new TestAncillaryDelete();
			t1.GuidId = Guid.NewGuid();
			t1.StrValue = "BY ZR DEMO";
			t1.DecValue = 0.2213m;
			t1.IntId = 1;
			Assert.AreEqual(t1.Insert(), 1);
		}
		/// <summary>
		/// 并发按时间戳删除
		/// </summary>
		[SmokingTestLibrary.TestMethod]
		[NUnit.Framework.Test]
		public void _2_ConcurrencyDelete_TimeStamp()
		{
			_1_Insert();
			TestAncillaryDelete t1 = _queryNewRow.ToSingle<TestAncillaryDelete>();
			CPQuery.Format("update [TestAncillaryDelete] set StrValue = '123';").ExecuteNonQuery();
			TestPackage.AssertException<Mysoft.Map.Extensions.Exception.OptimisticConcurrencyException>(
				() => { t1.Delete(ConcurrencyMode.TimeStamp); }, (p) => { return true; }
				);
			t1 = _queryNewRow.ToSingle<TestAncillaryDelete>();
			int effectRows = t1.Delete(ConcurrencyMode.TimeStamp);
			Assert.AreEqual(effectRows, 1);
		}
	}
}
