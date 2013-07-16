using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmokingTestLibrary;
using Mysoft.Map.Extensions.DAL;
using System.Data.SqlClient;
using System.Data;
using SmokingTest.CS.Entity;

namespace SmokingTest.DAL
{
	/// <summary>
	/// 测试存储过程场景
	/// </summary>
	[SmokingTestLibrary.Test(InNewThread = true, RunTimes = 150)]
	[NUnit.Framework.TestFixture]
	public class TestExecSP
	{

		/// <summary>
		/// 测试通过匿名对象调用存储过程场景
		/// </summary>
		[SmokingTestLibrary.TestMethod]
		[NUnit.Framework.Test]
		public void NoName()
		{
			var product = new {
				ProductName = Guid.NewGuid().ToString(),
				CategoryID = 1,
				Unit = "1个个个个个个个个个个个个个个个个个个个个个ha",
				UnitPrice = 12.36,
				Quantity = 25,
				Remark = "fsdf",
				ProductID = (SPOut)0		// output
			};

			StoreProcedure sp = new StoreProcedure("InsertProduct", product);
			sp.ExecuteNonQuery();

			int newProductId = (int)sp.Command.Parameters["@ProductID"].Value;

			string newProductName = "new name";

			var product2 = new {
				ProductName = newProductName,
				CategoryID = 1,
				Unit = "1个个个个个个个个个个个个个个个个个个个个个ha",
				UnitPrice = 12.36,
				Quantity = 25,
				Remark = "fsdf",
				ProductID = newProductId
			};

			StoreProcedure.Create("UpdateProduct", product2).ExecuteNonQuery();

			string name = CPQuery.Format("select ProductName from Products where ProductID = {0}", newProductId).ExecuteScalar<string>();
			Assert.AreEqual(newProductName, name);



			var parameters = new {
				ProductID = newProductId
			};
			StoreProcedure.Create("DeleteProduct", parameters).ExecuteNonQuery();

			int productId = CPQuery.Format("select top 1 1 from Products where ProductID = {0}", newProductId).ExecuteScalar<int>();
			Assert.AreEqual(productId, 0);
		}

		/// <summary>
		/// 测试通过压参调用存储过程场景
		/// </summary>
		[SmokingTestLibrary.TestMethod]
		[NUnit.Framework.Test]
		public void TestParameterSQL()
		{

			SqlParameter[] parameters1 = new SqlParameter[7];

			parameters1[0] = new SqlParameter("@ProductName", SqlDbType.NVarChar, 50);
			parameters1[0].Value = "测试产品名";
			parameters1[1] = new SqlParameter("@CategoryID", SqlDbType.Int);
			parameters1[1].Value = 1;
			parameters1[2] = new SqlParameter("@Unit", SqlDbType.NVarChar, 10);
			parameters1[2].Value = "个";
			parameters1[3] = new SqlParameter("@UnitPrice", SqlDbType.Money);
			parameters1[3].Value = 55;
			parameters1[4] = new SqlParameter("@Quantity", SqlDbType.Int);
			parameters1[4].Value = 44;
			parameters1[5] = new SqlParameter("@Remark", SqlDbType.NText);
			parameters1[5].Value = "产品备注";
			parameters1[6] = new SqlParameter("@ProductID", SqlDbType.Int);
			parameters1[6].Direction = ParameterDirection.Output;

			StoreProcedure.Create("InsertProduct", parameters1).ExecuteNonQuery();

			int newProductId = (int)parameters1[6].Value;

			int count = CPQuery.Format("SELECT COUNT(*) FROM Products WHERE ProductID = {0}", newProductId).ExecuteScalar<int>();

			Assert.AreEqual<int>(1, count);


			SqlParameter[] parameters2 = new SqlParameter[2];
			parameters2[0] = new SqlParameter("@ProductID", SqlDbType.Int);
			parameters2[0].Value = newProductId;
			parameters2[1] = new SqlParameter("@ProductName", SqlDbType.VarChar, 50);
			parameters2[1].Value = "测试产品名";
			Products product = CPQuery.From("SELECT * FROM Products WHERE ProductID = @ProductID AND ProductName=@ProductName", parameters2).ToSingle<Products>();

			Assert.AreEqual<int>(product.ProductID, newProductId);
			Assert.AreEqual<string>(product.Remark, "产品备注");
		}

	}


}
