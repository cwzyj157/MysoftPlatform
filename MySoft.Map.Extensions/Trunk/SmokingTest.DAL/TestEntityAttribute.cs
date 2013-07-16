using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SmokingTestLibrary;
using SmokingTest.CS.Entity;

using Mysoft.Map.Extensions.DAL;


namespace SmokingTest.DAL
{

	/// <summary>
	/// 测试实体的属性标记是否正确
	/// </summary>
	[SmokingTestLibrary.Test(InNewThread = true, RunTimes = 5)]
	[NUnit.Framework.TestFixture]
	public class TestEntityAttribute 
	{
		/// <summary>
		/// 初始化数据
		/// </summary>
		[TestMethod(Order = 0, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void InitData()
		{
			var query = "select top 1 object_id from sys.objects where name = 'TestAttribute'".AsCPQuery();
			var objectId = query.ExecuteScalar<long>();

			if( objectId > 0 )
				"drop table [TestAttribute]".AsCPQuery().ExecuteNonQuery();

			// 重新创建要测试的表
			@"CREATE TABLE [dbo].[TestAttribute](
				[RowId] [int] IDENTITY(1,1) NOT NULL,
				RowGuid uniqueidentifier  NOT NULL,
				[RowString] [nvarchar](50) NOT NULL,
			) ON [PRIMARY]"
				.AsCPQuery().ExecuteNonQuery();
		}

		/// <summary>
		/// 测试列别名及对应的数据类型是否能正确赋值
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 1)]
		[NUnit.Framework.Test]
		public void TestColumnAlias()
		{
			"DELETE FROM TestAttribute".AsCPQuery().ExecuteNonQuery();
			Guid guid = Guid.NewGuid();
			CPQuery.Format("INSERT INTO TestAttribute(RowGuid,RowString) VALUES({0}, {1})", guid, "Test").ExecuteNonQuery();

			AttrEntity entity = "SELECT RowString AS AliasColumn, RowGuid AS RawGuid, RowGuid As StringGuid FROM TestAttribute".AsCPQuery().ToSingle<AttrEntity>();
			Assert.AreEqual<string>(entity.AliasColumn, "Test");
			Assert.AreEqual<Guid>(entity.RawGuid, guid);
			Assert.AreEqual<string>(entity.StringGuid, guid.ToString());
		}

		
	}
}
