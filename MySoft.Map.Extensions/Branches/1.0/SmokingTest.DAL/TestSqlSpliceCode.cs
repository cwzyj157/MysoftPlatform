using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Mysoft.Map.Extensions.DAL;
using System.Data.SqlClient;
using SmokingTestLibrary;
using SmokingTest.CS.Entity;
using Mysoft.Map.Extensions;
namespace SmokingTest.DAL
{
	/// <summary>
	/// 验证并发修改的时候，set和where的情况
	/// </summary>
	[SmokingTestLibrary.Test(InNewThread = true, RunTimes = 1)]
	[NUnit.Framework.TestFixture]
	public class TestSqlSpliceCode
	{
		private CPQuery _queryNewRow;
		/// <summary>
		/// 初始化
		/// </summary>
		public TestSqlSpliceCode()
		{
			_queryNewRow = String.Format("select top 1 * from  TestSqlSplice").AsCPQuery();
		}
		/// <summary>
		/// 数据清除
		/// </summary>
		private void ClearData()
		{
			TestPackage.ClearData("TestSqlSplice");
		}
		/// <summary>
		/// 初始化测试表
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 0, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void InitData()
		{
			TestPackage.InitData("TestSqlSplice", Properties.Resources.TestSqlSpliceScript);
		}
		/// <summary>
		/// 插入数据
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 1)]
		[NUnit.Framework.Test]
		public void Insert()
		{
			ClearData();
			TestSqlSplice t = new TestSqlSplice();
			t.GuidId = Guid.NewGuid();
			t.StrValue = "test";
			t.IntValue =2;
			Assert.AreEqual<int>(t.Insert(), 1);
		}
		/// <summary>
		/// 并发修改，set条件和where条件相等的情况
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 2)]
		[NUnit.Framework.Test]
		public void ConcurrencyUpdate_OriginalValue1()
		{
			TestSqlSplice t3 = _queryNewRow.ToSingle<TestSqlSplice>();
			TestSqlSplice t2 = new TestSqlSplice();
			TestSqlSplice t1 = new TestSqlSplice();
			//主键赋值
			t2.GuidId = t3.GuidId;
			t1.GuidId = t3.GuidId;
			//修改StrValue
			t1.StrValue = "1";
			t2.StrValue = t3.StrValue;
			CPQuery cp = t1.GetCPQuery(9, t1, t2,null);
		
			String strWhere =String.Empty;
			String strSet = TestPackage.SelectSetAndWhere(cp.GetCommand().CommandText, ref strWhere);
			
			//断言SET条件中只有StrValue;
			Assert.AreNotEqual<int>(strSet.IndexOf("@StrValue"),-1);
			Assert.AreEqual<int>(strSet.IndexOf("@GuidId"),-1);
			Assert.AreEqual<int>(strSet.IndexOf("@IntValue"),-1);
			Assert.AreEqual<int>(strSet.IndexOf("@TimeStampValue"),-1);
			//断言WHERE条件只有主键和StrValue
			Assert.AreNotEqual<int>(strWhere.IndexOf("StrValue"), -1);
			Assert.AreNotEqual<int>(strWhere.IndexOf("GuidId"), -1);
			Assert.AreEqual<int>(strWhere.IndexOf("IntValue"), -1);
			Assert.AreEqual<int>(strWhere.IndexOf("TimeStampValue"), -1);
			Assert.AreEqual<int>(cp.ExecuteNonQuery(), 1);
		}
		/// <summary>
		/// 并发修改，set 比where多的情形
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 3)]
		[NUnit.Framework.Test]
		public void ConcurrencyUpdate_OriginalValue2()
		{
			TestSqlSplice t1 = _queryNewRow.ToSingle<TestSqlSplice>();
			TestSqlSplice t2 = new TestSqlSplice();
			t2.GuidId = t1.GuidId;
			t2.StrValue = "1";
			CPQuery cp = t1.GetCPQuery(9, t1,t2,null);
			String strWhere = String.Empty;
			String strSet = TestPackage.SelectSetAndWhere(cp.GetCommand().CommandText, ref strWhere);
			//断言SET条件中只有StrValue,IntValue;
			Assert.AreNotEqual<int>(strSet.IndexOf("@StrValue"), -1);
			Assert.AreEqual<int>(strSet.IndexOf("@GuidId"), -1);
			Assert.AreNotEqual<int>(strSet.IndexOf("@IntValue"), -1);
			Assert.AreEqual<int>(strSet.IndexOf("@TimeStampValue"), -1);
			//断言WHERE条件只有主键和StrValue
			Assert.AreNotEqual<int>(strWhere.IndexOf("StrValue"), -1);
			Assert.AreNotEqual<int>(strWhere.IndexOf("GuidId"), -1);
			Assert.AreEqual<int>(strWhere.IndexOf("IntValue"), -1);
			Assert.AreEqual<int>(strWhere.IndexOf("TimeStampValue"), -1);
			Assert.AreEqual<int>(cp.ExecuteNonQuery(), 1);
		}

		/// <summary>
		/// 并发修改，set 比where少的情形
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 4)]
		[NUnit.Framework.Test]
		public void ConcurrencyUpdate_OriginalValue3()
		{
			TestSqlSplice t3 = _queryNewRow.ToSingle<TestSqlSplice>();
			TestSqlSplice t2 = new TestSqlSplice();
			TestSqlSplice t1 = new TestSqlSplice();
			//主键赋值
			t2.GuidId = t3.GuidId;
			t1.GuidId = t3.GuidId;
			t1.StrValue = "dfdfdf";
			t2.StrValue = t3.StrValue;
			t2.IntValue = t3.IntValue;

			CPQuery cp = t1.GetCPQuery(9, t1, t2, null);
			String strWhere = String.Empty;
			String strSet = TestPackage.SelectSetAndWhere(cp.GetCommand().CommandText, ref strWhere);
			//断言SET条件中只有StrValue;
			Assert.AreNotEqual<int>(strSet.IndexOf("@StrValue"), -1);
			Assert.AreEqual<int>(strSet.IndexOf("@GuidId"), -1);
			Assert.AreEqual<int>(strSet.IndexOf("@IntValue"), -1);
			Assert.AreEqual<int>(strSet.IndexOf("@TimeStampValue"), -1);
			//断言WHERE条件只有主键和StrValue,IntValue
			Assert.AreNotEqual<int>(strWhere.IndexOf("StrValue"), -1);
			Assert.AreNotEqual<int>(strWhere.IndexOf("GuidId"), -1);
			Assert.AreNotEqual<int>(strWhere.IndexOf("IntValue"), -1);
			Assert.AreEqual<int>(strWhere.IndexOf("TimeStampValue"), -1);
			Assert.AreEqual<int>(cp.ExecuteNonQuery(), 1);
		}
		

	}
}
