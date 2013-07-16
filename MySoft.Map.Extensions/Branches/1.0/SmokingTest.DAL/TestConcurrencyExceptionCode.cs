using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmokingTestLibrary;

using Mysoft.Map.Extensions.DAL;
using System.Data.SqlClient;

using SmokingTest.CS.Entity;
namespace SmokingTest.DAL
{
	/// <summary>
	/// 并发异常的各个场景
	/// </summary>
	[SmokingTestLibrary.Test(InNewThread = true, RunTimes = 1)]
	[NUnit.Framework.TestFixture]
	public class TestConcurrencyExceptionCode
	{
		
		private CPQuery _queryNewRow;
		/// <summary>
		/// 初始化
		/// </summary>
		public TestConcurrencyExceptionCode()
		{
			_queryNewRow = String.Format("select top 1 * from  TestConcurrencyException").AsCPQuery();
		}
		/// <summary>
		/// 数据清除
		/// </summary>
		private void ClearData()
		{
			TestPackage.ClearData("TestConcurrencyException");
		}
		/// <summary>
		/// 初始化测试表
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 0, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void InitData()
		{
			TestPackage.InitData("TestConcurrencyException", Properties.Resources.TestConcurrencyExceptionScript);
		}
		/// <summary>
		/// 插入
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 1, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void Insert()
		{
			ClearData();
			TestConcurrencyException t1 = new TestConcurrencyException();
			t1.GuiId = Guid.NewGuid();
			t1.StrValue="dsf";
			t1.IntValue=1;
			Assert.AreEqual<int>(t1.Insert(),1);
		}
		/// <summary>
		///验证异常"用于并发检测的原始对象不能是当前对象"
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 2)]
		[NUnit.Framework.Test]
		public void ConcurrencyUpdate_OriginalValueEx()
		{
	
			TestConcurrencyException t1 =_queryNewRow.ToSingle<TestConcurrencyException>();
			t1.StrValue = "bbbbbbbbb";
			t1.IntValue = 28;
			TestPackage.AssertException<ArgumentException>(() => { t1.Update(t1, ConcurrencyMode.OriginalValue); },
				(p) => {
					if( p.Message.IndexOf("用于并发检测的原始对象不能是当前对象。")>-1 )
						return true;
					else
						return false;
				});
		}
		/// <summary>
		/// 验证时间戳不存在该异常
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 3)]
		[NUnit.Framework.Test]
		public void ConcurrencyUpdate_TimeStampEx()
		{

			TestConcurrencyException t1 = _queryNewRow.ToSingle<TestConcurrencyException>();
			t1.StrValue = "ddddd";
			t1.IntValue = 123;
			Assert.AreEqual<int>(t1.Update(t1, ConcurrencyMode.TimeStamp), 1);
		}

	
		/// <summary>
		/// 验证"并发操作失败，本次操作没有更新任何记录，请确认当前数据行没有被其他用户更新或删除。"是否抛出
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 4)]
		[NUnit.Framework.Test]
		public void ConcurrencyUpdate_TimeStampEx1()
		{
			Insert();
			TestConcurrencyException t1 = _queryNewRow.ToSingle<TestConcurrencyException>();
			TestConcurrencyException t2 = _queryNewRow.ToSingle<TestConcurrencyException>();
			String.Format("update TestConcurrencyException set StrValue='time' ").AsCPQuery().ExecuteNonQuery();
			t1.StrValue = "ddddd";
			t1.IntValue = 123;
			TestPackage.AssertException<Mysoft.Map.Extensions.Exception.OptimisticConcurrencyException>(() => { t1.Update(t2, ConcurrencyMode.TimeStamp); },
				(p) => {
					if( p.Message.IndexOf("并发操作失败，本次操作没有更新任何记录，请确认当前数据行没有被其他用户更新或删除。")>-1 )
						return true;
					else
						return false;
				});
		}

	}
}
