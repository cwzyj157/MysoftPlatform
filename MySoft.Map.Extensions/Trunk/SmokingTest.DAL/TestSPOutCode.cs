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
	[SmokingTestLibrary.Test(InNewThread = true, RunTimes = 1)]
	[NUnit.Framework.TestFixture]
	public	class TestSPOutCode
	{
		/// <summary>
		/// 插入测试数据
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 0, RunTimes = 1)]
		[NUnit.Framework.Test]
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
		[SmokingTestLibrary.TestMethod(Order = 1, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void 验证CPQuery_From对SqlParameter支持()
		{
			Insert();
			var parameters = new { param1 = new SqlParameter("@GuiId", "CB47AC1A-56DB-4AF8-BDE2-A4B2BE131F9B") };
			string sv = CPQuery.From("select StrValue from TestSPOut where GuiId=@GuiId", parameters).ExecuteScalar<String>();
			Assert.AreEqual(sv, "dsf");
		}
		/// <summary>
		/// 验证存储过程对SqlParameter的支持
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 2, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void 验证存储过程对SqlParameter的支持()
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

		/// <summary>
		/// 验证存储过程输入参数为Null
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 3, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void 验证存储过程输入参数为Null()
		{
			var parameters = new { name=DBNull.Value };
			StoreProcedure sp = StoreProcedure.Create("TestNullValue", parameters);
			Assert.AreEqual(sp.ExecuteNonQuery(),-1);
		}
		/// <summary>
		///  验证CPQuery.From的输入参数为Null
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 4, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void 验证CPQuery_From的输入参数为Null()
		{
			var parameters = new { StrValue = DBNull.Value };
			CPQuery.From("UPDATE dbo.TestSPOut SET StrValue=NULL WHERE GuiId='CB47AC1A-56DB-4AF8-BDE2-A4B2BE131F9B'").ExecuteNonQuery();

			string Result = CPQuery.From("select GuiId from TestSPOut where StrValue=@StrValue or @StrValue is null", parameters).ExecuteScalar<Guid>().ToString();
			string Result1 = CPQuery.Format("select GuiId from TestSPOut where StrValue={0} or {0} is null", DBNull.Value).ExecuteScalar<Guid>().ToString();

			Assert.AreEqual(Result, "cb47ac1a-56db-4af8-bde2-a4b2be131f9b");
			Assert.AreEqual(Result, Result1);
		}
		/// <summary>
		/// 验证SqlParameter的输入参数为Null的场景
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order=5,RunTimes=1)]
		[NUnit.Framework.Test]
		public void 验证CPQuery_From的SqlParameter参数值为Null()
		{
			var parameters = new { StrValue1 = new SqlParameter("@StrValue", DBNull.Value) };
			string guid = CPQuery.From("select GuiId from TestSPOut where StrValue=@StrValue or @StrValue is null", parameters).ExecuteScalar<Guid>().ToString();
			Assert.AreEqual(guid, "cb47ac1a-56db-4af8-bde2-a4b2be131f9b");
		}
		/// <summary>
		/// 验证存储过程中输出参数为Null
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 6, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void 验证存储过程的输出参数为Null()
		{
			//验证SPOUT情况
			Insert();
			var ps1 = new { GuiId = "CB47AC1A-56DB-4AF8-BDE2-A4B2BE131F9B", StrValue = (SPOut)DBNull.Value };
			TestPackage.AssertException<ArgumentException>(() => {
				StoreProcedure sp1 = StoreProcedure.Create("GetToSingleTestSPOut", ps1);
			});
		}
		/// <summary>
		/// 验证StoreProcedure.Create的第二个参数为Null
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 7, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void 验证StoreProcedure_Create的第二个参数为Null()
		{
			Insert();
			object p =null;
			StoreProcedure sp1 = StoreProcedure.Create("TestNullValue", p);
			TestPackage.AssertException<SqlException>(() => {
				sp1.ExecuteNonQuery();
			});
		}
	}
}
