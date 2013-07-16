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
	/// 删除场景
	/// </summary>
	[SmokingTestLibrary.Test(InNewThread = true, RunTimes = 1)]
	[NUnit.Framework.TestFixture]
	public class TestDeleteCode
	{
		private CPQuery _queryNewRow;
		/// <summary>
		/// 初始化
		/// </summary>
		public TestDeleteCode()
		{
			_queryNewRow = String.Format("select top 1 * from  TestDelete").AsCPQuery();
		}
		/// <summary>
		/// 数据清除
		/// </summary>
		private void ClearData()
		{
			TestPackage.ClearData("TestDelete");
		}
		/// <summary>
		/// 初始化测试表
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 0, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void InitData()
		{
			TestPackage.InitData("TestDelete", Properties.Resources.TestDeleteScript);
		}
		/// <summary>
		/// 基本插入
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 1, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void Insert()
		{
			ClearData();
			TestDelete t =new TestDelete();
			t.Guiid = new Guid("15B7E353-BB79-4BA9-A402-227B315E4A02");
			t.Id=1;
			t.StrValue="1";
			Assert.AreEqual<int>(t.Insert(),1);
			
		}
		/// <summary>
		/// 基本的删除
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 2, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void Delete()
		{
			Insert();
			TestDelete t = _queryNewRow.ToSingle<TestDelete>();
			Assert.AreEqual<int>(t.Delete(), 1);
		}


	}
}
