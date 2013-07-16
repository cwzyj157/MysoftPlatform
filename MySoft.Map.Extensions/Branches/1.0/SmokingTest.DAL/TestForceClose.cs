using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SmokingTestLibrary;

using Mysoft.Map.Extensions.DAL;
using System.Data.SqlClient;

namespace SmokingTest.DAL
{
	/// <summary>
	/// 测试强制关闭连接场景,即没有使用using块.也能保证连接正确关闭
	/// </summary>
	[SmokingTestLibrary.Test(InNewThread = true, RunTimes = 2)]
	[NUnit.Framework.TestFixture]
	public class TestForceClose
	{
		/// <summary>
		/// 
		/// </summary>
		[TestMethod(Order = 1)]
		[NUnit.Framework.Test]
		public void TestClose()
		{
			int count1 = "SELECT COUNT(*) FROM [Order Details]".AsCPQuery().ExecuteScalar<int>();

			//启动事务,不提交,这样将导致锁表
			ConnectionScope scope = new ConnectionScope(TransactionMode.Required);
			"DELETE FROM [Order Details]".AsCPQuery().ExecuteNonQuery();

			//强制释放资源
			ConnectionScope.ForceClose();

			//如果强制释放资源失败,此处有两种可能.
			//1.事务还在,得到的条数与count1不符
			//1.事务还在,但此处无法进行查询.导致本查询过程死掉.
			int count2 = "SELECT COUNT(*) FROM [Order Details]".AsCPQuery().ExecuteScalar<int>();

			Assert.AreEqual<int>(count1, count2);
		}
	}
}
