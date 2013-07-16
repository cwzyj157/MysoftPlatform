using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmokingTestLibrary;
using SmokingTest.CS.Entity;
using Mysoft.Map.Extensions.DAL;
using System.Threading;
namespace SmokingTest.DAL
{
	/// <summary>
	/// 验证并发修改的各个场景
	/// </summary>
	[SmokingTestLibrary.Test(InNewThread = true, RunTimes = 1)]
	[NUnit.Framework.TestFixture]
	public class TestConcurrencyUpdateCode
	{
		private CPQuery _queryNewRow;
		/// <summary>
		/// 初始化
		/// </summary>
		public TestConcurrencyUpdateCode()
		{
			_queryNewRow = String.Format("select top 1 * from TestConcurrencyUpdate").AsCPQuery();
		}
		/// <summary>
		/// 数据清除
		/// </summary>
		private void ClearData()
		{
			"TRUNCATE TABLE TestConcurrencyUpdate".AsCPQuery().ExecuteNonQuery();
		}
		/// <summary>
		/// 初始化测试表
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 0, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void InitData()
		{
			var query = "SELECT OBJECT_ID('TestConcurrencyUpdate')".AsCPQuery();
			var objectId = query.ExecuteScalar<long>();
			if( objectId > 0 )
				"drop table [TestConcurrencyUpdate]".AsCPQuery().ExecuteNonQuery();
			SqlHelper.ExecuteTSql(Properties.Resources.TestConcurrencyUpdateScript);
		}
		/// <summary>
		/// 插入数据
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 1)]
		[NUnit.Framework.Test]
		public void Insert()
		{
			ClearData();
			TestConcurrencyUpdate t = new TestConcurrencyUpdate();
			t.GuidRow = Guid.NewGuid();
			t.StrValue = "test";
			t.BinaryValue = new byte[] { 0x11, 0x22, 0x33 };
			Assert.AreEqual<int>(t.Insert(), 1);

		}
		/// <summary>
		/// 时间戳并发修改
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 2)]
		[NUnit.Framework.Test]
		public void ConcurrencyUpdate_TimeStamp()
		{
			Insert();
			TestConcurrencyUpdate t2 = _queryNewRow.ToSingle<TestConcurrencyUpdate>();
			TestConcurrencyUpdate t1 = _queryNewRow.ToSingle<TestConcurrencyUpdate>();
			t1.StrValue = "zr";
			int effectRows = t1.Update(t2, ConcurrencyMode.TimeStamp);
			//第一次修改成功
			Assert.AreEqual(effectRows, 1);
			bool ok = false;
			try {
				t1.StrValue = "zr";
				//第二次因为用的是原来的时间戳,所以修改失败
				t1.Update(t2, ConcurrencyMode.TimeStamp);
			}
			catch( Mysoft.Map.Extensions.Exception.OptimisticConcurrencyException ) {
				ok = true;
			}
			Assert.AreEqual(ok, true);
			//第三次因为查询了在修改，所以又会成功
			t2 = _queryNewRow.ToSingle<TestConcurrencyUpdate>();
			t1.StrValue = "zr";
			effectRows = t1.Update(t2, ConcurrencyMode.TimeStamp);
			Assert.AreEqual(effectRows, 1);
		}
		/// <summary>
		/// 验证多线程带事务的场景 时间戳
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 3)]
		[NUnit.Framework.Test]
		public void ConcurrencyUpdate_ScopeTimeStamp()
		{
			Insert();
			RunScopeUpdate._state = 0;
			TestConcurrencyUpdate t2 = _queryNewRow.ToSingle<TestConcurrencyUpdate>();
			List<Thread> threads = new List<Thread>(2);
			for (int i = 0; i < 2; i++)
			{
				RunScopeUpdate rsu = new RunScopeUpdate();
				Thread thread = new Thread(rsu.RunScopeTimeStamp);
				threads.Add(thread);
			}
			// 开启线程
			foreach( Thread thread in threads )
				thread.Start(t2);
			// 等待所有线程执行线束。
			foreach( Thread thread in threads )
				thread.Join();
			//2次执行,一次成功一次失败
			Assert.AreEqual<int>(RunScopeUpdate._state, 1);
		
		}

		/// <summary>
		/// 验证多线程带事务的场景 原始值
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 4)]
		[NUnit.Framework.Test]
		public void ConcurrencyUpdate_OriginalValue()
		{
			Insert();
			RunScopeUpdate._state = 0;
			TestConcurrencyUpdate t2 = _queryNewRow.ToSingle<TestConcurrencyUpdate>();
			List<Thread> threads = new List<Thread>(2);
			for( int i = 0; i < 2; i++ ) {
				RunScopeUpdate rsu = new RunScopeUpdate();
				Thread thread = new Thread(rsu.RunScopeOriginalValue);
				threads.Add(thread);
			}
			// 开启线程
			foreach( Thread thread in threads )
				thread.Start(t2);
			// 等待所有线程执行线束。
			foreach( Thread thread in threads )
				thread.Join();
			//2次执行,一次成功一次失败
			Assert.AreEqual<int>(RunScopeUpdate._state, 1);

		}
		
		
	}
	/// <summary>
	/// 多线程并发测试类
	/// </summary>
	public class RunScopeUpdate
	{
		private CPQuery _queryNewRow;
		/// <summary>
		/// 初始化
		/// </summary>
		public RunScopeUpdate()
		{
			_queryNewRow = String.Format("select top 1 * from TestConcurrencyUpdate").AsCPQuery();
		}
		/// <summary>
		/// 状态值
		/// </summary>
		public static int _state = 0;
		/// <summary>
		/// 时间戳修改
		/// </summary>
		/// <param name="ob"></param>
		public void RunScopeTimeStamp(Object ob)
		{
			try {
				TestConcurrencyUpdate t2 = ob as TestConcurrencyUpdate;
				TestConcurrencyUpdate t1 = new TestConcurrencyUpdate();
				t1.BinaryValue = t2.BinaryValue;
				t1.GuidRow = t2.GuidRow;
				t1.StrValue = "sadfasdfasdfsadf";
				t1.TimeStampValue = t2.TimeStampValue;
				using( ConnectionScope scope1 = new ConnectionScope(TransactionMode.Required) ) {
					t1.Update(t2, ConcurrencyMode.TimeStamp);
					scope1.Commit();
					Interlocked.Increment(ref _state);
				}
				
			}
			catch {
			}
		}
		/// <summary>
		/// 原始值修改
		/// </summary>
		/// <param name="ob"></param>
		public void RunScopeOriginalValue(Object ob)
		{
			try {
				TestConcurrencyUpdate t2 = ob as TestConcurrencyUpdate;
				TestConcurrencyUpdate t1 = new TestConcurrencyUpdate();
				t1.BinaryValue = t2.BinaryValue;
				t1.GuidRow = t2.GuidRow;
				t1.StrValue = "1sa";
				t1.TimeStampValue = t2.TimeStampValue;
				using( ConnectionScope scope1 = new ConnectionScope(TransactionMode.Required) ) {
					t1.Update(t2, ConcurrencyMode.OriginalValue);
					scope1.Commit();
					Interlocked.Increment(ref _state);
				}
				
			}
			catch {
			}
		}
	}
}
