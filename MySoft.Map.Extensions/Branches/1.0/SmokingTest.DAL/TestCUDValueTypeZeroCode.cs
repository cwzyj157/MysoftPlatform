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
	/// 验证SetPropertyDefaultValue
	/// </summary>
	[SmokingTestLibrary.Test(InNewThread = true, RunTimes = 1)]
	[NUnit.Framework.TestFixture]
	 public	class TestCUDValueTypeZeroCode
	{
		private CPQuery _queryNewRow;
		/// <summary>
		/// 初始化
		/// </summary>
		public TestCUDValueTypeZeroCode()
		{
			_queryNewRow = String.Format("select top 1 * from  TestCUDValueTypeZero").AsCPQuery();
		}
		/// <summary>
		/// 数据清除
		/// </summary>
		private void ClearData()
		{
			TestPackage.ClearData("TestCUDValueTypeZero");
		}
		/// <summary>
		/// 初始化测试表
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 0, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void InitData()
		{
			TestPackage.InitData("TestCUDValueTypeZero", Properties.Resources.TestCUDValueTypeZeroScript);
		}
		/// <summary>
		/// 插入场景
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 1, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void Insert()
		{
			ClearData();
			TestCUDValueTypeZero t = new TestCUDValueTypeZero();
			t.SetPropertyDefaultValue("Id");
			t.Id = 1;
			t.SetPropertyDefaultValue("Id");
			t.StrValue = "123";
			t.ByteValue = new byte[] { 0x1, 0x2, 0x3 };
			Assert.AreEqual<int>(t.Insert(), 1);
			//验证多次赋值后，结果是否仍然正确
			Assert.AreEqual<int>(_queryNewRow.ToSingle<TestCUDValueTypeZero>().Id, 0);
			t.Id = 0;
			//验证 "指定的属性名 StrValue 不是值类型，不需要这个调用。" 异常
			TestPackage.AssertException<InvalidOperationException>(() => { t.SetPropertyDefaultValue("StrValue"); }, "指定的属性名 StrValue 不是值类型，不需要这个调用。");
			//验证 "指定的属性名 IntValue 是可空类型，不需要这个调用。" 异常
			TestPackage.AssertException<InvalidOperationException>(() => { t.SetPropertyDefaultValue("IntValue"); }, "指定的属性名 IntValue 是可空类型，不需要这个调用。");
			//验证 "指定的属性名 df 不能匹配任何属性。" 异常
			TestPackage.AssertException<ArgumentOutOfRangeException>(() => { t.SetPropertyDefaultValue("df"); }, "指定的属性名 df 不能匹配任何属性。");
			//验证大小写敏感
			TestPackage.AssertException<ArgumentOutOfRangeException>(() => { t.SetPropertyDefaultValue("id"); }, "指定的属性名 id 不能匹配任何属性。");


			
		}
	}
}
