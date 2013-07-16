using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mysoft.Map.Extensions.DAL;
using System.Data.SqlClient;
using SmokingTestLibrary;
using SmokingTest.CS.Entity;
using System.Threading;
using Mysoft.Map.Extensions.CodeDom;
using Mysoft.Map.Extensions;
namespace SmokingTest.DAL
{
	/// <summary>
	/// 并发修改的各种场景
	/// </summary>
	[SmokingTestLibrary.Test(InNewThread = true, RunTimes = 1)]
	[NUnit.Framework.TestFixture]
	public class TestConcurrencytDeleteCode
	{
		private CPQuery _queryNewRow;
		/// <summary>
		/// 初始化
		/// </summary>
		public TestConcurrencytDeleteCode()
		{
			_queryNewRow = String.Format("select top 1 * from TestConcurrencytDelete").AsCPQuery();
		}

		/// <summary>
		/// 数据清除
		/// </summary>
		private void ClearData()
		{
			TestPackage.ClearData("TestConcurrencytDelete");

		}
		/// <summary>
		/// 初始化测试表
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 0, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void InitData()
		{
			TestPackage.InitData("TestConcurrencytDelete", Properties.Resources.TestConcurrencytDeleteScript);

		}
		/// <summary>
		/// 插入数据
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 1)]
		[NUnit.Framework.Test]
		public void Insert()
		{
			ClearData();
			TestConcurrencytDelete t = new TestConcurrencytDelete();
			t.GuidRow = Guid.NewGuid();
			t.StrValue = "test";
			t.TimeStampValue = 600871;
			t.BinaryValue =new byte[] { 0x1, 0x22, 0x33 };
			Assert.AreEqual<int>(t.Insert(), 1);
		}
		/// <summary>
		/// 正常删除
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 2)]
		[NUnit.Framework.Test]
		public void Delete()
		{
			Insert();
			TestConcurrencytDelete t1 = new TestConcurrencytDelete();
			t1.GuidRow = _queryNewRow.ExecuteScalar<Guid>();
			int effectRows = t1.Delete();
			Assert.AreEqual(effectRows, 1);
		}
		/// <summary>
		/// 并发按时间戳删除
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 3)]
		[NUnit.Framework.Test]
		public void ConcurrencyDelete_TimeStamp()
		{
			Insert();
			TestConcurrencytDelete t1 = _queryNewRow.ToSingle<TestConcurrencytDelete>();
			CPQuery.Format("update [TestConcurrencytDelete] set StrValue = '123';").ExecuteNonQuery();
			TestPackage.AssertException<Mysoft.Map.Extensions.Exception.OptimisticConcurrencyException>(
				() => { t1.Delete(ConcurrencyMode.TimeStamp); }, (p) => { return true; }
				);
			t1 = _queryNewRow.ToSingle<TestConcurrencytDelete>();
			int effectRows = t1.Delete(ConcurrencyMode.TimeStamp);
			Assert.AreEqual(effectRows, 1);
		}
		/// <summary>
		/// 并发按原始值删除
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 4)]
		[NUnit.Framework.Test]
		public void ConcurrencyDelete_OriginalValue()
		{
			Insert();
			TestConcurrencytDelete t1 = _queryNewRow.ToSingle<TestConcurrencytDelete>();
			bool ok = false;
			try {
				TestConcurrencytDelete t2 = new TestConcurrencytDelete {
					StrValue = t1.StrValue + "aa",
					GuidRow = t1.GuidRow
				};

				// 参数指定不正确，不能与原始记录行匹配，此时执行删除不会影响任何记录。
				t2.Delete(ConcurrencyMode.OriginalValue);
			}
			catch( Mysoft.Map.Extensions.Exception.OptimisticConcurrencyException ) {
				ok = true;
			}
			Assert.AreEqual(ok, true);


			TestConcurrencytDelete t3 = new TestConcurrencytDelete {
				TimeStampValue = t1.TimeStampValue,
				StrValue = t1.StrValue,
				GuidRow = t1.GuidRow,
				BinaryValue = t1.BinaryValue
			};
			int effectRows = t3.Delete(ConcurrencyMode.OriginalValue);
			Assert.AreEqual(effectRows, 1);
		}

		/// <summary>
		/// 并发按时间戳删除，多线程带事务
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 5)]
		[NUnit.Framework.Test]
		public void ConcurrencyDelete_ScopeTimeStamp()
		{
			RunScopeDelete._state = 0;
			Insert();
			List<Thread> threads = new List<Thread>(2);
			TestConcurrencytDelete t1 = _queryNewRow.ToSingle<TestConcurrencytDelete>();
			for( int i = 0; i < 2; i++ ) {
				RunScopeDelete rsd = new RunScopeDelete();

				Thread thread = new Thread(rsd.RunScopeTimeStamp);
				threads.Add(thread);

			}
			// 开启线程
			foreach( Thread thread in threads )
				thread.Start(t1);
			// 等待所有线程执行线束。
			foreach( Thread thread in threads )
				thread.Join();
			//2次执行,一次成功一次失败
			Assert.AreEqual<int>(RunScopeDelete._state, 1);

		}
		/// <summary>
		/// 并发按原始值删除
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 6)]
		[NUnit.Framework.Test]
		public void ConcurrencyDelete_ScopeOriginalValue()
		{
			RunScopeDelete._state = 0;
			Insert();
			TestConcurrencytDelete t2 = _queryNewRow.ToSingle<TestConcurrencytDelete>();

			List<Thread> threads = new List<Thread>(2);
			for( int i = 0; i < 2; i++ ) {
				RunScopeDelete rsd = new RunScopeDelete();
				Thread thread = new Thread(rsd.RunScopeOriginalValue);
				threads.Add(thread);
			}
			// 开启线程
			foreach( Thread thread in threads )
				thread.Start(t2);
			// 等待所有线程执行线束。
			foreach( Thread thread in threads )
				thread.Join();
			//2次执行,一次成功一次失败
			Assert.AreEqual<int>(RunScopeDelete._state, 1);

		}



	}
	/// <summary>
	/// 带事务执行
	/// </summary>
	public class RunScopeDelete
	{
		/// <summary>
		/// 状态值
		/// </summary>
		public static int _state = 0;
		/// <summary>
		/// 时间戳并发删除
		/// </summary>
		/// <param name="ob"></param>
		public void RunScopeTimeStamp(Object ob)
		{
			try {
				TestConcurrencytDelete t1 = ob as TestConcurrencytDelete;
				using( ConnectionScope scope1 = new ConnectionScope(TransactionMode.Required) ) {
					t1.Delete(ConcurrencyMode.TimeStamp);
					scope1.Commit();
					Interlocked.Increment(ref _state);
				}
			}
			catch {

			}
		}
		/// <summary>
		/// 原始值并发删除
		/// </summary>
		/// <param name="ob"></param>
		public void RunScopeOriginalValue(Object ob)
		{
			try {
				TestConcurrencytDelete t2 = ob as TestConcurrencytDelete;
				using( ConnectionScope scope1 = new ConnectionScope(TransactionMode.Required) ) {
					t2.Delete(ConcurrencyMode.OriginalValue);
					scope1.Commit();
					Interlocked.Increment(ref _state);
				}
			}
			catch {
			}
		}

	}


}
