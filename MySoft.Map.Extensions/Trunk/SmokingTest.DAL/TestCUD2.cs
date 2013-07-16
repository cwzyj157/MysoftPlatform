using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmokingTestLibrary;
using SmokingTest.CS.Entity;
using Mysoft.Map.Extensions.DAL;


namespace SmokingTest.DAL
{

	/// <summary>
	/// 对CUD的测试
	/// </summary>
	[SmokingTestLibrary.Test(InNewThread = true)]
	[NUnit.Framework.TestFixture]
	public class TestCUD2
	{
		private Guid _rowGuid ;
		private CPQuery _queryNewRow ;
		/// <summary>
		/// 初始化
		/// </summary>
		public TestCUD2()
		{
			_rowGuid = Guid.NewGuid();
			_queryNewRow = CPQuery.Format("select * from Table_1 where RowGuid = {0}", _rowGuid);
		}

		/// <summary>
		/// 初始化
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 0, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void _Smoking_0_InitData()
		{
			long tid = CPQuery.Format("select object_id from sys.objects where name = 'Table_1'").ExecuteScalar<long>();

			if( tid > 0 )
				CPQuery.Format("drop table Table_1").ExecuteNonQuery();

			SqlHelper.ExecuteTSql(Properties.Resources.CreateTable1Script);
		}


		/// <summary>
		/// 插入场景
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 1)]
		[NUnit.Framework.Test]
		public void _Smoking_1_Insert()
		{
			Table1 t1 = new Table1 {
				IntValue = 200,
				StrValue = "aaaaaaaaa",
				Money = 123.45M,
				Rowguid = _rowGuid
			};

			int effectRows = t1.Insert();
			Assert.AreEqual(effectRows, 1);
			Table1 result = _queryNewRow.ToSingle<Table1>();
			Assert.AreEqual<Guid>(result.Rowguid, _rowGuid);
			Assert.AreEqual<int>(t1.IntValue, result.IntValue);
			Assert.AreEqual<string>(t1.StrValue, result.StrValue);
			Assert.AreEqual<decimal>(t1.Money, result.Money);
		}
		/// <summary>
		/// 修改场景
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 2)]
		[NUnit.Framework.Test]
		public void _Smoking_2_Update()
		{
			Table1 t1 = new Table1();
			t1.Rowguid = _rowGuid;
			t1.RowId = 11111111;	// 这是个自增列，内容不会被修改
			t1.StrValue2 = "Hello...." + DateTime.Now.Ticks.ToString();
			int effectRows = t1.Update();
			Assert.AreEqual(effectRows, 1);

			Table1 result = _queryNewRow.ToSingle<Table1>();
			Assert.AreNotEqual<int>(t1.RowId, result.RowId);
			Assert.AreEqual<string>(t1.StrValue2, result.StrValue2);
		}
		/// <summary>
		/// 原始值并发修改场景
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 3)]
		[NUnit.Framework.Test]
		public void _Smoking_3_ConcurrencyUpdate_TimeStamp()
		{
			Table1 t2 = _queryNewRow.ToSingle<Table1>();
			Table1 t1 = _queryNewRow.ToSingle<Table1>();
			t1.IntValue = 3;
			t1.StrValue = "Fish Li";
			int effectRows = t1.Update(t2, ConcurrencyMode.TimeStamp);
			Assert.AreEqual(effectRows, 1);

			bool ok = false;
			try {
				t1.StrValue = "Fish Li";
				// 记录已经更新了，但是 t2 没有与最新记录同步，所以下面的调用会引发异常
				t1.Update(t2, ConcurrencyMode.TimeStamp);
			}
			catch( Mysoft.Map.Extensions.Exception.OptimisticConcurrencyException ) {
				ok = true;
			}
			Assert.AreEqual(ok, true);


			t2 = _queryNewRow.ToSingle<Table1>();
			t1.IntValue = 3;
			t1.StrValue = "Fish Li";
			effectRows = t1.Update(t2, ConcurrencyMode.TimeStamp);
			Assert.AreEqual(effectRows, 1);
		}
		/// <summary>
		/// 原始值并发修改场景
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 4)]
		[NUnit.Framework.Test]
		public void _Smoking_4_ConcurrencyUpdate_OriginalValue()
		{
			Table1 t2 = _queryNewRow.ToSingle<Table1>();
			Table1 t1 = _queryNewRow.ToSingle<Table1>();
			t1.IntValue2 = 5;
			t1.StrValue2 = "bbbbbbbbb";
			int effectRows = t1.Update(t2, ConcurrencyMode.OriginalValue);
			Assert.AreEqual(effectRows, 1);


			bool ok = false;
			try {
				t1.StrValue2 = "bbbbbbbbb";
				// 记录已经更新了，但是 t2 没有与最新记录同步，所以下面的调用会引发异常
				t1.Update(t2, ConcurrencyMode.OriginalValue);
			}
			catch( Mysoft.Map.Extensions.Exception.OptimisticConcurrencyException ) {
				ok = true;
			}
			Assert.AreEqual(ok, true);

			
			t2 = _queryNewRow.ToSingle<Table1>();
			t1.IntValue2 = 5;
			t1.StrValue2 = "bbbbbbbbb";
			effectRows = t1.Update(t2, ConcurrencyMode.OriginalValue);
			//原来和0比较是因为,t1和t2做比较，t1和t2相等的时候，set条件没有生成，所以等于0
			//Assert.AreEqual(effectRows, 0);
			
			//现在是和空对象比较,所以set条件生成了，所以影响行数为1
			Assert.AreEqual(effectRows, 1);
		}
		/// <summary>
		/// 删除场景
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 5)]
		[NUnit.Framework.Test]
		public void _Smoking_5_Delete()
		{
			Table1 t1 = new Table1();
			t1.Rowguid = _rowGuid;
			int effectRows = t1.Delete();
			Assert.AreEqual(effectRows, 1);
		}
		/// <summary>
		/// 时间戳并发修改场景
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 6)]
		[NUnit.Framework.Test]
		public void _Smoking_6_ConcurrencyDelete_TimeStamp()
		{
			_Smoking_1_Insert();
			
			Table1 t1 = _queryNewRow.ToSingle<Table1>();
			CPQuery.Format("update [Table_1] set IntValue = 1;").ExecuteNonQuery();


			bool ok = false;
			try {
				Table1 t2 = new Table1 {
					TimeStamp = t1.TimeStamp,
					Rowguid = t1.Rowguid
				};
				
				// 前面已执行更新了，所以时间戳已经改变了，此时执行删除不会影响任何记录。
				t2.Delete(ConcurrencyMode.TimeStamp);
			}
			catch( Mysoft.Map.Extensions.Exception.OptimisticConcurrencyException ) {
				ok = true;
			}
			Assert.AreEqual(ok, true);



			t1 = _queryNewRow.ToSingle<Table1>();

			Table1 t3 = new Table1 {
				TimeStamp = t1.TimeStamp,
				Rowguid = t1.Rowguid
			};
			int effectRows = t3.Delete(ConcurrencyMode.TimeStamp);
			Assert.AreEqual(effectRows, 1);
		}

		/// <summary>
		/// 原始值并发修改场景
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 7)]
		[NUnit.Framework.Test]
		public void _Smoking_7_ConcurrencyDelete_OriginalValue()
		{
			_Smoking_1_Insert();
			_Smoking_2_Update();


			Table1 t1 = _queryNewRow.ToSingle<Table1>();


			bool ok = false;
			try {
				Table1 t2 = new Table1 {
					IntValue = t1.IntValue + 2,
					StrValue = t1.StrValue + "aa",
					StrValue2 = t1.StrValue2 + "bb",
					Rowguid = t1.Rowguid
				};

				// 参数指定不正确，不能与原始记录行匹配，此时执行删除不会影响任何记录。
				t2.Delete(ConcurrencyMode.OriginalValue);
			}
			catch( Mysoft.Map.Extensions.Exception.OptimisticConcurrencyException ) {
				ok = true;
			}
			Assert.AreEqual(ok, true);


			Table1 t3 = new Table1 {
				IntValue = t1.IntValue,
				StrValue = t1.StrValue,
				StrValue2 = t1.StrValue2,
				Rowguid = t1.Rowguid
			};
			int effectRows = t3.Delete(ConcurrencyMode.OriginalValue);
			Assert.AreEqual(effectRows, 1);
		}

		/// <summary>
		/// TrackChange的修改场景
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 8)]
		[NUnit.Framework.Test]
		public void _Smoking_8_Update_TrackChange()
		{
			TestPackage.ClearData("Table_1");
			_Smoking_1_Insert();

			Table1 t1 = _queryNewRow.ToSingle<Table1>();
			t1.TrackChange();
			t1.IntValue = 0;

			CPQuery query = t1.GetCPQuery(7, t1, t1.bakObject);
			//Console.WriteLine(query.GetCommand().CommandText);
			Console.WriteLine(query.Command.CommandText);
				
			t1.Update();


			Table1 t2 = _queryNewRow.ToSingle<Table1>();
			Assert.AreEqual(t2.IntValue, 0);

		}

	}
}
