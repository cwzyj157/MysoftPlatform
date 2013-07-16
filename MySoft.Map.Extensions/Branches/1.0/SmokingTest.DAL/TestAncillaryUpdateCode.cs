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
	/// 并发修改的扩展场景
	/// </summary>
	[SmokingTestLibrary.Test(InNewThread = true, RunTimes = 1)]
	[NUnit.Framework.TestFixture]
	public class TestAncillaryUpdateCode
	{
		private CPQuery _queryNewRow;
		/// <summary>
		/// 初始化
		/// </summary>
		public TestAncillaryUpdateCode()
		{
			_queryNewRow = String.Format("select top 1 * from  TestAncillaryUpdate").AsCPQuery();
		}
		/// <summary>
		/// 数据清除
		/// </summary>
		private void ClearData()
		{
			TestPackage.ClearData("TestAncillaryUpdate");
		}
		/// <summary>
		/// 初始化测试表
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 0, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void InitData()
		{
			TestPackage.InitData("TestAncillaryUpdate", Properties.Resources.TestAncillaryUpdateScript);
		}
		/// <summary>
		/// 插入
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 1, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void Insert()
		{
			ClearData();
			TestAncillaryUpdate t1 = new TestAncillaryUpdate();
			t1.GuidId = Guid.NewGuid();
			t1.StrValue="BY ZR DEMO";
			t1.DecValue=0.2213m;
			t1.IntId=1;
			Assert.AreEqual(t1.Insert(),1);
		}
		/// <summary>
		/// 验证时间戳类型的并发修改，取原始值的方式为NEW的方式
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 2)]
		[NUnit.Framework.Test]
		public void ConcurrencyUpdate_TimeStamp()
		{
			Insert();
			TestAncillaryUpdate t1 = _queryNewRow.ToSingle<TestAncillaryUpdate>();
			TestAncillaryUpdate t2=new TestAncillaryUpdate();
			//给金额赋值
			t2.DecValue=t1.DecValue;
			//必须给主键
			t2.GuidId = t1.GuidId;
			//必须给时间戳
			t2.TimeStampValue = t1.TimeStampValue;
			//修改金额
			t1.DecValue = 0.2m;
			t1.StrValue = String.Empty;
			int effectRows = t1.Update(t2, ConcurrencyMode.TimeStamp);
			//第一次修改成功
			Assert.AreEqual(effectRows, 1);
			//第二次时间戳发生改变，修改失败
			TestPackage.AssertException<Mysoft.Map.Extensions.Exception.OptimisticConcurrencyException>(
				()=>{
					t1.DecValue=0.2m;
					t1.StrValue = "第二次发生改变的字符串";
					t1.Update(t2, ConcurrencyMode.TimeStamp);
				},"并发操作失败，本次操作没有更新任何记录，请确认当前数据行没有被其他用户更新或删除"
				);
			
		}
		/// <summary>
		/// 验证原始值类型的并发修改，取原始值的方式为NEW的方式
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 3)]
		[NUnit.Framework.Test]
		public void ConcurrencyUpdate_OriginalValue()
		{
			TestAncillaryUpdate t1 = _queryNewRow.ToSingle<TestAncillaryUpdate>();
			TestAncillaryUpdate t2 = new TestAncillaryUpdate();
			//给金额赋值
			t2.DecValue = t1.DecValue;
			//必须给主键
			t2.GuidId = t1.GuidId;
			t2.TimeStampValue = t1.TimeStampValue;
			//修改金额
			t1.DecValue = 0.2m;
			int effectRows = t1.Update(t2, ConcurrencyMode.OriginalValue);
			//第一次修改成功
			Assert.AreEqual(effectRows, 1);
			//第二次原始值发生改变，修改失败
			TestPackage.AssertException<Mysoft.Map.Extensions.Exception.OptimisticConcurrencyException>(
				() => {
					//原始值发生变化，
					t2.DecValue = 0.3m;
					t1.StrValue = "第二次发生改变的字符串";
					t1.Update(t2, ConcurrencyMode.OriginalValue);
				}, "并发操作失败，本次操作没有更新任何记录，请确认当前数据行没有被其他用户更新或删除"
				);
			
		}
		/// <summary>
		/// TrackChange修改
		/// 验证参数是否按需加载
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 4)]
		[NUnit.Framework.Test]
		public void Update_TrackChange()
		{
			Insert();
			TestAncillaryUpdate t1 = _queryNewRow.ToSingle<TestAncillaryUpdate>();
			t1.TrackChange();
			t1.DecValue = 0.989m;
			t1.StrValue = "TrackChang普通修改";
			CPQuery cp = t1.GetCPQuery(7, t1, t1.bakObject);
			String strWhere = String.Empty;
			String strSet = TestPackage.SelectSetAndWhere(cp.GetCommand().CommandText, ref strWhere);
			//set中，只能有DecValue,StrValue
			Assert.AreNotEqual(strSet.IndexOf("@DecValue"), -1);
			Assert.AreNotEqual(strSet.IndexOf("@StrValue"), -1);
			Assert.AreEqual(strSet.IndexOf("@IntId"), -1);
			Assert.AreEqual(strSet.IndexOf("@GuidId"), -1);
			//where中，只能有主键
			Assert.AreNotEqual(strWhere.IndexOf("GuidId"), -1);
			Assert.AreEqual(strWhere.IndexOf("DecValue"), -1);
			Assert.AreEqual(strWhere.IndexOf("StrValue"), -1);
			Assert.AreEqual(strWhere.IndexOf("IntId"), -1);
			//验证修改成功
			Assert.AreEqual(t1.Update(), 1);
		}
		/// <summary>
		/// TrackChange 原始值修改
		/// 验证参数是否按需加载
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 5)]
		[NUnit.Framework.Test]
		public void ConcurrencyUpdate_OriginalValue_TrackChange()
		{
			Insert();
			TestAncillaryUpdate t1 = _queryNewRow.ToSingle<TestAncillaryUpdate>();
			TestAncillaryUpdate t2 = new TestAncillaryUpdate();
			t1.TrackChange();
			//给金额赋值
			t2.DecValue = t1.DecValue;
			//必须给主键
			t2.GuidId = t1.GuidId;
			t1.DecValue = 0.61m;
			t1.StrValue = "原始值带TrackChange并发修改";
			CPQuery cp = t1.GetCPQuery(9, t1,t2,t1.bakObject);
			String strWhere = String.Empty;
			String strSet = TestPackage.SelectSetAndWhere(cp.GetCommand().CommandText, ref strWhere);
			//set中，只能有DecValue,StrValue
			Assert.AreNotEqual(strSet.IndexOf("@DecValue"), -1);
			Assert.AreNotEqual(strSet.IndexOf("@StrValue"), -1);
			Assert.AreEqual(strSet.IndexOf("@IntId"), -1);
			Assert.AreEqual(strSet.IndexOf("@GuidId"), -1);
			//where中，只能有主键和DecValue
			Assert.AreNotEqual(strWhere.IndexOf("GuidId"), -1);
			Assert.AreNotEqual(strWhere.IndexOf("DecValue"), -1);
			Assert.AreEqual(strWhere.IndexOf("StrValue"), -1);
			Assert.AreEqual(strWhere.IndexOf("IntId"), -1);
			//验证修改成功
			Assert.AreEqual(t1.Update(t2,ConcurrencyMode.OriginalValue), 1);
			TestPackage.AssertException<Mysoft.Map.Extensions.Exception.OptimisticConcurrencyException>(
			() => {
				//原始值发生变化，
				t2.DecValue = 0.8m;
				t1.StrValue = "第二次发生改变的字符串";
				t1.Update(t2, ConcurrencyMode.OriginalValue);
			}, "并发操作失败，本次操作没有更新任何记录，请确认当前数据行没有被其他用户更新或删除"
			);
		}
		/// <summary>
		/// TrackChange 时间戳修改
		/// 验证参数是否按需加载
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 6)]
		[NUnit.Framework.Test]
		public void ConcurrencyUpdate_TimeStamp_TrackChange()
		{
			Insert();
			TestAncillaryUpdate t1 = _queryNewRow.ToSingle<TestAncillaryUpdate>();
			t1.TrackChange();
			TestAncillaryUpdate t2 = new TestAncillaryUpdate();
			//给金额赋值
			t2.DecValue = t1.DecValue;
			//必须给主键
			t2.GuidId = t1.GuidId;
			//必须给时间戳
			t2.TimeStampValue = t1.TimeStampValue;
			//修改金额
			t1.DecValue = 0.2m;
			t1.StrValue = "123123123";
			CPQuery cp = t1.GetCPQuery(8, t1, t2, t1.bakObject);
			String strWhere = String.Empty;
			String strSet = TestPackage.SelectSetAndWhere(cp.GetCommand().CommandText, ref strWhere);
			//set中，只能有DecValue,StrValue
			Assert.AreNotEqual(strSet.IndexOf("@DecValue"), -1);
			Assert.AreNotEqual(strSet.IndexOf("@StrValue"), -1);
			Assert.AreEqual(strSet.IndexOf("@IntId"), -1);
			Assert.AreEqual(strSet.IndexOf("@GuidId"), -1);
			Assert.AreEqual(strSet.IndexOf("@TimeStampValue"), -1);
			//where中，只能有主键和时间戳
			Assert.AreNotEqual(strWhere.IndexOf("GuidId"), -1);
			Assert.AreNotEqual(strWhere.IndexOf("TimeStampValue"), -1);
			Assert.AreEqual(strWhere.IndexOf("DecValue"), -1);
			Assert.AreEqual(strWhere.IndexOf("StrValue"), -1);
			Assert.AreEqual(strWhere.IndexOf("IntId"), -1);
			int effectRows = t1.Update(t2, ConcurrencyMode.TimeStamp);
			//第一次修改成功
			Assert.AreEqual(effectRows, 1);
			//第二次时间戳发生改变，修改失败
			TestPackage.AssertException<Mysoft.Map.Extensions.Exception.OptimisticConcurrencyException>(
				() => {
					t1.DecValue = 0.2m;
					t1.StrValue = "第二次发生改变的字符串";
					t1.Update(t2, ConcurrencyMode.TimeStamp);
				}, "并发操作失败，本次操作没有更新任何记录，请确认当前数据行没有被其他用户更新或删除"
				);
		}


		
	}
}
