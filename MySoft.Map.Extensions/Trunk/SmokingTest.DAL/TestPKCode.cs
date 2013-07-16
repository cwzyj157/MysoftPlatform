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
	/// 验证：联合主键的基本操作，
	/// </summary>
	[SmokingTestLibrary.Test(InNewThread = true, RunTimes = 1)]
	[NUnit.Framework.TestFixture]
	public class TestPKCode
	{
		private CPQuery _queryNewRow;
		private Guid g = new Guid("15B7E353-BB79-4BA9-A402-227B315E4A02");
		/// <summary>
		/// 初始化
		/// </summary>
		public TestPKCode()
		{
			_queryNewRow = String.Format("select top 1 * from  TestPK").AsCPQuery();
		}
		/// <summary>
		/// 数据清除
		/// </summary>
		private void ClearData()
		{
			TestPackage.ClearData("TestPK");
		}
		/// <summary>
		/// 初始化测试表
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 0, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void InitData()
		{
			TestPackage.InitData("TestPK", Properties.Resources.TestPKScript);
		}
		/// <summary>
		/// 基本插入
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 1, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void Insert()
		{
			ClearData();
			TestPK t =new TestPK();
			t.Guiid = g;
			t.Id=1;
			t.StrValue="1";

			Assert.AreEqual<int>(t.Insert(),1);
			Assert.AreEqual<string>(_queryNewRow.ToSingle<TestPK>().StrValue, t.StrValue);
		}
		/// <summary>
		/// 基本修改
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 2, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void Update()
		{
			TestPK t =new TestPK();
			t.Guiid = g;
			t.Id=1;
			t.StrValue="2";
			Assert.AreEqual<int>(t.Update(),1);
			Assert.AreEqual<string>(_queryNewRow.ToSingle<TestPK>().StrValue, t.StrValue);
		}
		/// <summary>
		/// 基本的并发修改
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 3, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void ConcurrencyUpdate_TimeStamp()
		{
			TestPK t1=_queryNewRow.ToSingle<TestPK>();
			TestPK t2=_queryNewRow.ToSingle<TestPK>();
			t1.StrValue="oo00";
			Assert.AreEqual<int>(t1.Update(t2,ConcurrencyMode.TimeStamp),1);
			Assert.AreEqual<string>(_queryNewRow.ToSingle<TestPK>().StrValue, t1.StrValue);
		}
		/// <summary>
		/// 基本的并发修改
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 4, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void ConcurrencyUpdate_OriginalValue()
		{
			Insert();
			TestPK t1=_queryNewRow.ToSingle<TestPK>();
			TestPK t2=_queryNewRow.ToSingle<TestPK>();
			t1.StrValue="oo001";
			Assert.AreEqual<int>(t1.Update(t2,ConcurrencyMode.OriginalValue),1);
			t1.StrValue = "oo002";
			TestPackage.AssertException<Mysoft.Map.Extensions.Exception.OptimisticConcurrencyException>(
				() => { t1.Update(t2, ConcurrencyMode.OriginalValue); },
				(p)=>{
					if(p.Message.IndexOf("并发操作失败，本次操作没有更新任何记录，请确认当前数据行没有被其他用户更新或删除。")>-1)
						return true;
					else
						return false;
				}
				);
		}
		/// <summary>
		/// 基本的删除
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 5, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void Delete()
		{
			Insert();
			TestPK t = _queryNewRow.ToSingle<TestPK>();
			Assert.AreEqual<int>(t.Delete(), 1);
		}

		/// <summary>
		/// 并发删除 原始值
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 6, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void Delete_OriginalValue()
		{
			Insert();
			TestPK t1= _queryNewRow.ToSingle<TestPK>();
			TestPK t2 = new TestPK();
			t2.Guiid=t1.Guiid;
			t2.Id=t1.Id;
			t2.StrValue=t1.StrValue;
			t2.TstValue=t1.TstValue;
			Assert.AreEqual<int>(t2.Delete(ConcurrencyMode.OriginalValue),1);
			t2.StrValue = "err";
			TestPackage.AssertException<Mysoft.Map.Extensions.Exception.OptimisticConcurrencyException>(
				() => { t1.Delete(ConcurrencyMode.OriginalValue); },
				(p) => {
					if( p.Message.IndexOf("并发操作失败，本次操作没有删除任何记录，请确认当前数据行没有被其他用户更新或删除。" )>-1)
						return true;
					else
						return false;
				}
				);
		}

		/// <summary>
		/// 并发删除
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 7, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void Delete_TimeStamp()
		{
			Insert();
			TestPK t1= _queryNewRow.ToSingle<TestPK>();
			TestPK t2 = new TestPK();
			t2.Guiid=t1.Guiid;
			t2.Id=t1.Id;
			t2.StrValue=t1.StrValue;
			t2.TstValue=t1.TstValue;
			Assert.AreEqual<int>(t2.Delete(ConcurrencyMode.TimeStamp),1);
			Insert();
			TestPackage.AssertException<Mysoft.Map.Extensions.Exception.OptimisticConcurrencyException>(
			() => { t2.Delete(ConcurrencyMode.TimeStamp); },
			(p) => {
				if( p.Message.IndexOf( "并发操作失败，本次操作没有删除任何记录，请确认当前数据行没有被其他用户更新或删除。" )>-1)
					return true;
				else
					return false;
			}
			);
			
		}

	}
}
