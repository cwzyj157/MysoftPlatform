using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmokingTestLibrary;
using SmokingTest.CS.Entity;
using System.Data.SqlClient;
using Mysoft.Map.Extensions.DAL;
using System.Data;



namespace SmokingTest.DAL
{
	/// <summary>
	/// 测试存储过程场景
	/// </summary>
	[Test(InNewThread = true, RunTimes = 1)]
	public	class TestSPOutCode
	{
		/// <summary>
		/// 插入测试数据
		/// </summary>
		[TestMethod(Order = 0, RunTimes = 1)]
		public void Insert()
		{
			TestPackage.InitData("TestSPOut", Properties.Resources.TestSPOutScript);
			TestPackage.ClearData("TestSPOut");
			TestSPOut t1 = new TestSPOut();
			t1.GuiId =new Guid("CB47AC1A-56DB-4AF8-BDE2-A4B2BE131F9B");
			t1.StrValue = "dsf";
			t1.IntValue = 1;
			t1.Insert();
		}
		
		/// <summary>
		/// 测试.CPQuery.From对SqlParameter的支持
		/// </summary>
		[TestMethod(Order = 1, RunTimes = 1)]
		public void SelectSingTestConcurrencyException()
		{
		
			var parameters = new { param1 = new SqlParameter("@GuiId", "CB47AC1A-56DB-4AF8-BDE2-A4B2BE131F9B") };
			string sv = CPQuery.From("select StrValue from TestSPOut where GuiId=@GuiId", parameters).ExecuteScalar<String>();
			Assert.AreEqual(sv, "dsf");
		}
		/// <summary>
		/// 测试.StoreProcedure对SqlParameter的支持
		/// </summary>
		[TestMethod(Order = 2, RunTimes = 1)]
		public void SelectSingByProc()
		{

			var parameters = new { param1 = new SqlParameter("@GuiId", "CB47AC1A-56DB-4AF8-BDE2-A4B2BE131F9B"), param2 = new SqlParameter("@StrValue", SqlDbType.VarChar, 3) };
			parameters.param2.Direction = ParameterDirection.Output;
			StoreProcedure sp = StoreProcedure.Create("GetToSingleTestSPOut", parameters);
			sp.ExecuteNonQuery();
			string sv = (string)sp.Command.Parameters["@StrValue"].Value;
			Assert.AreEqual(sv, "dsf");
				
			//验证SPOUT情况
			Insert();
			var ps1 = new { GuiId = "CB47AC1A-56DB-4AF8-BDE2-A4B2BE131F9B", StrValue = (SPOut)"" };
			StoreProcedure sp1 = StoreProcedure.Create("GetToSingleTestSPOut", ps1);
			sp1.ExecuteNonQuery();
			string sv1 = (string)sp1.Command.Parameters["@StrValue"].Value;
			Assert.AreEqual(sv1, "dsf");


			//验证SPOUT情况 指定长度
			Insert();
			var psl2 = new { GuiId = "CB47AC1A-56DB-4AF8-BDE2-A4B2BE131F9B", StrValue = (SPOut)"" };
			psl2.StrValue.Size = 4;
			StoreProcedure sp2 = StoreProcedure.Create("GetToSingleTestSPOut", psl2);
			sp2.ExecuteNonQuery();
			string sv2 = (string)sp2.Command.Parameters["@StrValue"].Value;
			Assert.AreEqual(sv2, "dsf");

			TestPackage.ClearData("TestConcurrencyException");

		}
	}
}
