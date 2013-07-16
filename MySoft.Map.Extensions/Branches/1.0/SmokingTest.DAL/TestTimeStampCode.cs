using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mysoft.Map.Extensions.DAL;
using System.Data.SqlClient;
using SmokingTestLibrary;
using SmokingTest.CS.Entity;
using Mysoft.Map.Extensions;
using Mysoft.Map.Extensions.Exception;

namespace SmokingTest.DAL
{
	/// <summary>
	/// 验证时间戳的各个场景
	/// </summary>
	[SmokingTestLibrary.Test(InNewThread = true, RunTimes = 1)]
	[NUnit.Framework.TestFixture]
	public class TestTimeStampCode
	{
		private CPQuery _queryNewRow;
		private CPQuery _queryNewRow1;
		/// <summary>
		/// 初始化
		/// </summary>
		public TestTimeStampCode()
		{
			_queryNewRow = String.Format("select top 1 GuidId,TsValue,StrValue,IntValue from  TestTimeStamp").AsCPQuery();
			_queryNewRow1 = String.Format("select top 1 GuidId,cast(TsValue as bigint) as TsValue,StrValue,IntValue from  TestTimeStamp").AsCPQuery();

		}
		/// <summary>
		/// 数据清除
		/// </summary>
		private void ClearData()
		{
			TestPackage.ClearData("TestTimeStamp");
		}
		/// <summary>
		/// 初始化测试表
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 0, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void InitData()
		{
			TestPackage.InitData("TestTimeStamp", Properties.Resources.TestTimeStampScript);
		}
		/// <summary>
		/// 插入数据
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 1)]
		[NUnit.Framework.Test]
		public void Insert()
		{
			ClearData();
			TestTimeStamp t = new TestTimeStamp();
			t.GuidId = Guid.NewGuid();
			t.StrValue = "test";
			t.IntValue = 2;
			Assert.AreEqual<int>(t.Insert(), 1);
		
		}

		/// <summary>
		/// 查询数据 验证:CodeDom赋值是否正确
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 2)]
		[NUnit.Framework.Test]
		public void SelectSingle()
		{
			long ts = String.Format("Select  TOP 1 cast(TsValue as bigint) t1 FROM TestTimeStamp").AsCPQuery().ExecuteScalar<long>();
			TestTimeStamp t = _queryNewRow.ToSingle<TestTimeStamp>();
			TestTimeStamp1 t1 = _queryNewRow.ToSingle<TestTimeStamp1>();
			Assert.AreEqual<long>(t.TsValue, ts);
			Assert.AreEqual<long>(t1.TsValue.TimeStampToInt64(), ts);
			//如果时间戳已经在数据库中进行了转换
			TestTimeStamp t2 = _queryNewRow1.ToSingle<TestTimeStamp>();
			TestTimeStamp1 t3 = _queryNewRow1.ToSingle<TestTimeStamp1>();
			Assert.AreEqual<long>(t2.TsValue, ts);
			Assert.AreEqual<long>(t3.TsValue.TimeStampToInt64(), ts);
			//验证long to FillDataTable to list
			TestTimeStamp a = _queryNewRow.FillDataTable().ToList<TestTimeStamp>()[0];
			Assert.AreEqual<long>(a.TsValue, ts);
			//验证 long to list 
			TestTimeStamp b= _queryNewRow.ToList<TestTimeStamp>()[0];
			Assert.AreEqual<long>(b.TsValue, ts);
			//验证 byte[] to FillDataTable to list
			TestTimeStamp1 a1 = _queryNewRow.FillDataTable().ToList<TestTimeStamp1>()[0];
			Assert.AreEqual<long>(a1.TsValue.TimeStampToInt64(), ts);
			//验证 byte[]  to list
			TestTimeStamp1 b1 = _queryNewRow.ToList<TestTimeStamp1>()[0];
			Assert.AreEqual<long>(b1.TsValue.TimeStampToInt64(), ts);

		}

		/// <summary>
		/// 并发删除,时间戳类型为long
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 3)]
		[NUnit.Framework.Test]
		public void ConcurrencyUpdate_TimeStamp()
		{
			Insert();
			TestTimeStamp t1 = _queryNewRow.ToSingle<TestTimeStamp>();
			t1.TrackChange();
			TestTimeStamp t2 = new TestTimeStamp();
			t2.GuidId = t1.GuidId;
			t2.TsValue = t1.TsValue;
			t1.StrValue = "123123123123";
			//第一次修改成功，因为时间戳一样
			t1.Update(t2, ConcurrencyMode.TimeStamp);

			//第二次修改失败，因为时间戳更新了
			TestPackage.AssertException<Mysoft.Map.Extensions.Exception.OptimisticConcurrencyException>(
				() => {
					t1.StrValue = "第二次发生改变的字符串";
					t1.Update(t2, ConcurrencyMode.TimeStamp);
				}, "并发操作失败，本次操作没有更新任何记录，请确认当前数据行没有被其他用户更新或删除"
			);
		}
		/// <summary>
		/// 并发删除,时间戳类型为byte[]
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 4)]
		[NUnit.Framework.Test]
		public void ConcurrencyUpdate_TimeStamp1()
		{
			Insert();
			TestTimeStamp1 t1 = _queryNewRow.ToSingle<TestTimeStamp1>();
			t1.TrackChange();
			TestTimeStamp1 t2 = new TestTimeStamp1();
			t2.GuidId = t1.GuidId;
			t2.TsValue = t1.TsValue;
			t1.StrValue = "dfdfdfdfdfdf";
			//第一次修改成功，因为时间戳一样
			t1.Update(t2, ConcurrencyMode.TimeStamp);
			//第二次修改失败，因为时间戳更新了
			TestPackage.AssertException<OptimisticConcurrencyException>(() => {
					t1.StrValue = "第二次发生改变的字符串";
					t1.Update(t2, ConcurrencyMode.TimeStamp);
				},
				"并发操作失败，本次操作没有更新任何记录，请确认当前数据行没有被其他用户更新或删除"
			);
		}
		/// <summary>
		/// 并发按原始值删除,时间戳类型为long。
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 5)]
		[NUnit.Framework.Test]
		public void ConcurrencyDelete_TimeStamp()
		{
			TestTimeStamp t1 = _queryNewRow.ToSingle<TestTimeStamp>();
			TestTimeStamp t2 = new TestTimeStamp();
			t2.GuidId = t1.GuidId;
			t2.TsValue = t1.TsValue;
			Assert.AreEqual<int>(t2.Delete(ConcurrencyMode.TimeStamp), 1);
		}
		/// <summary>
		/// 并发按原始值删除,时间戳类型为byte[]
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 6)]
		[NUnit.Framework.Test]
		public void ConcurrencyDelete_TimeStamp1()
		{
			Insert();
			TestTimeStamp1 t1 = _queryNewRow.ToSingle<TestTimeStamp1>();
			TestTimeStamp1 t2 = new TestTimeStamp1();
			t2.GuidId = t1.GuidId;
			t2.TsValue = t1.TsValue;
			Assert.AreEqual<int>(t2.Delete(ConcurrencyMode.TimeStamp), 1);
		}

		/// <summary>
		/// 查询数据,反射版本
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 7)]
		[NUnit.Framework.Test]
		public void SelectSingle1()
		{
			Insert();
			long ts = String.Format("Select  TOP 1 cast(TsValue as bigint) t1 FROM dbo.TestTimeStamp").AsCPQuery().ExecuteScalar<long>();
			TestTimeStamp2 t1 = _queryNewRow.ToSingle<TestTimeStamp2>();
			TestTimeStamp2 t2 = _queryNewRow.ToList<TestTimeStamp2>()[0];
			TestTimeStamp2 t3 = _queryNewRow.FillDataTable().ToList<TestTimeStamp2>()[0];
			Assert.AreEqual<long>(t1.TsValue, ts);
			Assert.AreEqual<long>(t2.TsValue, ts);
			Assert.AreEqual<long>(t3.TsValue, ts);
			TestTimeStamp3 t4 = _queryNewRow.ToSingle<TestTimeStamp3>();
			TestTimeStamp3 t5 = _queryNewRow.ToList<TestTimeStamp3>()[0];
			TestTimeStamp3 t6 = _queryNewRow.FillDataTable().ToList<TestTimeStamp3>()[0];
			Assert.AreEqual<long>(t4.TsValue.TimeStampToInt64(), ts);
			Assert.AreEqual<long>(t5.TsValue.TimeStampToInt64(), ts);
			Assert.AreEqual<long>(t6.TsValue.TimeStampToInt64(), ts);
			//验证在数据库中进行了转化的场景
			TestTimeStamp2 t7 = _queryNewRow1.ToSingle<TestTimeStamp2>();
			TestTimeStamp2 t8 = _queryNewRow1.ToList<TestTimeStamp2>()[0];
			TestTimeStamp2 t9 = _queryNewRow1.FillDataTable().ToList<TestTimeStamp2>()[0];
			Assert.AreEqual<long>(t7.TsValue, ts);
			Assert.AreEqual<long>(t8.TsValue, ts);
			Assert.AreEqual<long>(t9.TsValue, ts);



			//验证在数据库中进行了转化的场景2
			TestTimeStamp3 t10 = _queryNewRow1.ToSingle<TestTimeStamp3>();
			TestTimeStamp3 t11 = _queryNewRow1.ToList<TestTimeStamp3>()[0];
			TestTimeStamp3 t12 = _queryNewRow1.FillDataTable().ToList<TestTimeStamp3>()[0];
			Assert.AreEqual<long>(t10.TsValue.TimeStampToInt64(), ts);
			Assert.AreEqual<long>(t11.TsValue.TimeStampToInt64(), ts);
			Assert.AreEqual<long>(t12.TsValue.TimeStampToInt64(), ts);

		}
	}
}
