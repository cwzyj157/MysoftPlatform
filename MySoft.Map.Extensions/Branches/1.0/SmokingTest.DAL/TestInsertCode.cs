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
	/// 验证插入场景
	/// </summary>
	[SmokingTestLibrary.Test(InNewThread = true, RunTimes = 1)]
	[NUnit.Framework.TestFixture]
	public class TestInsertCode
	{
		/// <summary>
		/// 数据清除
		/// </summary>
		private void ClearData()
		{
			"TRUNCATE TABLE TestInsert".AsCPQuery().ExecuteNonQuery();
		}
		/// <summary>
		/// 初始化测试表
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 0, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void InitData()
		{
			var query = "SELECT OBJECT_ID('TestInsert')".AsCPQuery();
			var objectId = query.ExecuteScalar<long>();
			if( objectId > 0 )
				"drop table [TestInsert]".AsCPQuery().ExecuteNonQuery();
			SqlHelper.ExecuteTSql(Properties.Resources.TestInsertScript);
		}
		/// <summary>
		/// 插入
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 1)]
		[NUnit.Framework.Test]
		public void Insert()
		{
			ClearData();
			TestInsert t = new TestInsert();
			t.PkId = 1;
			t.StrValue = "testsdf' ' ' ' ' '@^@*&%@@%$^@^*@(@(";
			t.ByteValue = new byte[0] ;
			t.Ids = 1;
			t.TimeStampValue = new byte[] { 0x1, 0x2 };
			t.GuidId = Guid.NewGuid();
			CPQuery cp =t.GetCPQuery(3,t);
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
	}
}
