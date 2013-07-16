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
	/// 1:拿到生成的语句，用IndexOf验证
	/// 2,验证普通的修改是否成功,断言影响行数为1
	/// </summary>
	[SmokingTestLibrary.Test(InNewThread = true, RunTimes = 1)]
	[NUnit.Framework.TestFixture]
	public class TestUpdateCode
	{
	
		private CPQuery _queryNewRow;
		/// <summary>
		/// 初始化
		/// </summary>
		public TestUpdateCode()
		{
			_queryNewRow = String.Format("select top 1 * from  TestUpdate").AsCPQuery();
		}
		/// <summary>
		/// 数据清除
		/// </summary>
		private void ClearData()
		{
			TestPackage.ClearData("TestUpdate");
		}
		/// <summary>
		/// 初始化测试表
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 0, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void InitData()
		{
			TestPackage.InitData("TestUpdate", Properties.Resources.TestUpdateScript);
		}

		/// <summary>
		/// 插入数据
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 1)]
		[NUnit.Framework.Test]
		public void Insert()
		{
			ClearData();
			TestUpdate t = new TestUpdate();
			t.PkId = 1;
			t.StrValue = "testsdf";
			t.ByteValue = new byte[] { 0x1, 0x2 };
			t.Ids = 1;
			t.TimeStampValue = new byte[] { 0x1, 0x2 };
			t.GuidId = Guid.NewGuid();
			CPQuery cp = t.GetCPQuery(3, t);



			String ComStr = cp.GetCommand().CommandText;
			//不能包含自增
			Assert.AreEqual<int>(ComStr.IndexOf("Ids"), -1);
			//不能包含有序GUID
			Assert.AreEqual<int>(ComStr.IndexOf("GuidId"), -1);
			//不能包含TimeStamp类型
			Assert.AreEqual<int>(ComStr.IndexOf("TimeStampValue"), -1);
			int i = cp.ExecuteNonQuery();
			//插入是否成功
			Assert.AreEqual<int>(i, 1);
		}

		/// <summary>
		/// 修改数据
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 2)]
		[NUnit.Framework.Test]
		public void Update()
		{
			TestUpdate t = new TestUpdate();
			t.PkId = 1;
			t.StrValue = "博客园";
			t.ByteValue = new byte[] { 0x23, 0x22 };
			t.Ids = 2;
			t.TimeStampValue = new byte[] { 0x34, 0x24 };
			t.GuidId = _queryNewRow.ToSingle<TestUpdate>().GuidId;
			CPQuery cp = t.GetCPQuery(7, t,null);

			String ComStr = cp.GetCommand().CommandText.Replace("where","W").Split('W')[0];
			//不能包含自增
			Assert.AreEqual<int>(ComStr.IndexOf("Ids"), -1);
			//不能包主键
			Assert.AreEqual<int>(ComStr.IndexOf("PkId"), -1);
			//不能包含TimeStamp类型
			Assert.AreEqual<int>(ComStr.IndexOf("TimeStampValue"), -1);

			int i = cp.ExecuteNonQuery();
			Assert.AreEqual<int>(i, 1);
		}

	}
}
