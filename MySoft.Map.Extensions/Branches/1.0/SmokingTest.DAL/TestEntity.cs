using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SmokingTestLibrary;

using Mysoft.Map.Extensions.DAL;
using SmokingTest.DAL;
using System.Data;
using SmokingTest.CS.Entity;
using Mysoft.Map.Extensions.Exception;

namespace SmokingTest.DAL
{
	/// <summary>
	/// 测试反射场景下,实体查询
	/// </summary>
	[SmokingTestLibrary.Test(InNewThread = true, RunTimes = 10)]
	[NUnit.Framework.TestFixture]
	public class TestEntity 
	{
		/// <summary>
		/// 版本信息
		/// </summary>
		/// <returns></returns>
		public static bool IsSupport()
		{
			return Program.SqlVeresion >= 10;
		}

		/// <summary>
		/// 初始化表
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 0, RunTimes = 1)]
		public void InitData()
		{
			var query = "SELECT OBJECT_ID('TestDataType')".AsCPQuery();
			var objectId = query.ExecuteScalar<long>();
			if( objectId > 0 )
				"drop table [TestDataType]".AsCPQuery().ExecuteNonQuery();

			query = "SELECT OBJECT_ID('usp_GetTestDataType')".AsCPQuery();
			objectId = query.ExecuteScalar<long>();
			if( objectId > 0 )
				"drop proc [usp_GetTestDataType]".AsCPQuery().ExecuteNonQuery();

			sqlCreateProc.AsCPQuery().ExecuteNonQuery();

			// 重新创建要测试的表
			if( Program.SqlVeresion == 8 ) {
				table2000.AsCPQuery().ExecuteNonQuery();
			}
			if( Program.SqlVeresion == 9 ) {
				table2000.AsCPQuery().ExecuteNonQuery();
			}
			if( Program.SqlVeresion >= 10 ) {
				table2008.AsCPQuery().ExecuteNonQuery();
			}
			
		}

		string table2008 = @"CREATE TABLE [dbo].[TestDataType](
				[a] [bigint] NULL,
				[b] [binary](50) NULL,
				[c] [bit] NULL,
				[d] [char](10) NULL,
				[e] [date] NULL,
				[f] [datetime] NULL,
				[g] [decimal](18, 4) NULL,
				[h] [float] NULL,
				[i] [image] NULL,
				[j] [int] NULL,
				[k] [money] NULL,
				[l] [nchar](10) NULL,
				[m] [ntext] NULL,
				[n] [numeric](18, 4) NULL,
				[o] [nvarchar](50) NULL,
				[p] [nvarchar](max) NULL,
				[q] [real] NULL,
				[r] [smalldatetime] NULL,
				[s] [smallint] NULL,
				[t] [smallint] NULL,
				[u] [smallmoney] NULL,
				[v] [text] NULL,
				[w] [tinyint] NULL,
				[x] [uniqueidentifier] NULL,
				[y] [varchar](50) NULL,
				[z] [nchar](10) NULL,
				[a1] [DATETIMEOFFSET] NULL,
				[b1] [XML],
				[c1] varbinary(8),
				[d1] datetime2,
				[e1] timestamp,
				[f1] time
			) ";

		string table2000 = @"CREATE TABLE [dbo].[TestDataType](
				[a] [bigint] NULL,
				[b] [binary](50) NULL,
				[c] [bit] NULL,
				[d] [char](10) NULL,
				[e] [datetime] NULL,
				[f] [datetime] NULL,
				[g] [decimal](18, 4) NULL,
				[h] [float] NULL,
				[i] [image] NULL,
				[j] [int] NULL,
				[k] [money] NULL,
				[l] [nchar](10) NULL,
				[m] [ntext] NULL,
				[n] [numeric](18, 4) NULL,
				[o] [nvarchar](50) NULL,
				[p] [nvarchar](4000) NULL,
				[q] [real] NULL,
				[r] [smalldatetime] NULL,
				[s] [smallint] NULL,
				[t] [smallint] NULL,
				[u] [smallmoney] NULL,
				[v] [text] NULL,
				[w] [tinyint] NULL,
				[x] [uniqueidentifier] NULL,
				[y] [varchar](50) NULL,
				[z] [nchar](10) NULL,
				[a1] [datetime] NULL,
				[b1] [ntext],
				[c1] varbinary(8),
				[d1] datetime,
				[e1] timestamp,
				[f1] datetime
				)";

		string sqlCreateProc = @"CREATE PROC usp_GetTestDataType
						AS
						SELECT * FROM TestDataType
						SELECT * FROM TestDataType";

		//写入一条记录
		private object[] Insert1()
		{
			object[] parameter = {(long)55, 
									new byte[]{0x11, 0x22, 0x33},
									false,
									"Test",
									DateTime.Now, 
									DateTime.Now,
									5.5m,
									5.6f,
									new byte[]{0x44,0x55,0x66},
									555,
									4.4m,
									"test2",
									"test3",
									5.5f,
									"test7",
									"test8",
									5.5f,
									DateTime.Now,
									444,
									333,
									5.5m,
									"test4",
									44,
									Guid.NewGuid(),
									"test5",
									"test6",
									DateTimeOffset.Now,
									"<data val=\"test\" />",
									new byte[]{0x11, 0x22, 0x33},
									DateTime.Now,
									DateTime.Now
								 };

			CPQuery query = CPQuery.Format(@"INSERT INTO TestDataType([a], [b], [c], [d], [e], [f], [g], [h], [i], [j], [k], [l], [m], [n], [o], [p], [q], [r], [s], [t], [u], [v], [w], [x], [y], [z], [a1], [b1], [c1], [d1], [f1])
			VALUES({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30})", parameter);
			query.ExecuteNonQuery();

			return parameter;
		}

		private void DeleteAll()
		{
			"DELETE FROM TestDataType".AsCPQuery().ExecuteNonQuery();
		}

		/// <summary>
		/// 测试单条数据的实体查询
		/// </summary>
		/// <remarks>
		/// <list type="bullet">
		/// <item><description>测试SELECT *场景,对象属性赋值</description></item>
		/// <item><description>测试数据库为NULL,对象属性为Nullable场景,是否能赋值到对象属性上</description></item>
		/// <item><description>测试数据库为NULL,对象属性为非Nullable场景,是否能赋值到对象属性上</description></item>
		/// <item><description>测试VB实体与C#实体对象属性赋值</description></item>
		/// </list>
		/// </remarks>
		[SmokingTestLibrary.TestMethod(Order = 1, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void TestToSingle()
		{
			DeleteAll();
			object[] parameters = Insert1();

			TestDataType obj1 = CPQuery.Format("SELECT * FROM TestDataType").ToSingle<TestDataType>();
			Assert.AreEqual<string>("Test", obj1.d.Trim());

			TestDataType obj2 = CPQuery.Format("SELECT NULL AS a,NULL AS b,NULL AS c,NULL AS d,NULL AS e,NULL AS f,NULL AS g,NULL AS h,NULL AS i,NULL AS j,NULL AS k,NULL AS l,NULL AS m,NULL AS n,NULL AS o,NULL AS p,NULL AS q,NULL AS r,NULL AS s,NULL AS t,NULL AS u,NULL AS v,NULL AS w,NULL AS x,NULL AS y,NULL AS z, NULL AS a1, NULL AS b1, NULL AS c1, NULL AS d1, NULL AS e1, NULL AS f1 FROM TestDataType").ToSingle<TestDataType>();
			Assert.AreEqual<string>(null, obj2.d);

			TestDataTypeNoNull obj3 = CPQuery.Format("SELECT NULL AS a,NULL AS b,NULL AS c,NULL AS d,NULL AS e,NULL AS f,NULL AS g,NULL AS h,NULL AS i,NULL AS j,NULL AS k,NULL AS l,NULL AS m,NULL AS n,NULL AS o,NULL AS p,NULL AS q,NULL AS r,NULL AS s,NULL AS t,NULL AS u,NULL AS v,NULL AS w,NULL AS x,NULL AS y,NULL AS z, NULL AS a1, NULL AS b1, NULL AS c1, NULL AS d1, NULL AS e1, NULL AS f1 FROM TestDataType").ToSingle<TestDataTypeNoNull>();
			Assert.AreEqual<long>((long)0, obj3.a);

			TestDataGuidToString obj4 = CPQuery.Format("SELECT * FROM TestDataType").ToSingle<TestDataGuidToString>();
			Assert.AreEqual<string>(obj4.x, parameters[23].ToString());

			//测试反射分支的场景
			TestDataTypeReflection obj5 = CPQuery.Format("SELECT * FROM TestDataType").ToSingle<TestDataTypeReflection>();
			Assert.AreEqual<string>("Test", obj5.d.Trim());
		}

		/// <summary>
		/// 测试多条数据查询返回实体
		/// </summary>
		/// <remarks>
		/// <list type="bullet">
		/// <item><description>测试SELECT * 场景,对象属性赋值</description></item>
		/// <item><description>测试返回集合条数是否正确</description></item>
		/// <item><description>测试常见数据类型是否正确</description></item>
		/// <item><description>测试数据库为NULL,对象属性为Nullable场景,是否能赋值到对象属性上</description></item>
		/// <item><description>测试数据库为NULL,对象属性为非Nullable场景,是否能赋值到对象属性上</description></item>
		/// </list>
		/// </remarks>
		[SmokingTestLibrary.TestMethod(Order = 2, RunTimes = 3)]
		[NUnit.Framework.Test]
		public void TestToList()
		{
			DeleteAll();
			object[] parameter1 = Insert1();

			List<TestDataType> list1 = CPQuery.Format("SELECT * FROM TestDataType").ToList<TestDataType>();
			List<TestDataTypeNoNull> list2 = CPQuery.Format("SELECT * FROM TestDataType").ToList<TestDataTypeNoNull>();

			Assert.AreEqual<int>(1, list1.Count);
			Assert.AreEqual<int>(1, list2.Count);

			Assert.AreEqual<long>(list1[0].a.Value, (long)parameter1[0]);
			Assert.AreEqual<long>(list2[0].a, (long)parameter1[0]);

			Assert.AreEqual<bool>(list1[0].c.Value, (bool)parameter1[2]);
			Assert.AreEqual<bool>(list2[0].c, (bool)parameter1[2]);

			Assert.AreEqual<string>(list1[0].e.Value.ToString("yyyyMMdd"), ((DateTime)parameter1[4]).ToString("yyyyMMdd"));
			Assert.AreEqual<string>(list2[0].e.ToString("yyyyMMdd"), ((DateTime)parameter1[4]).ToString("yyyyMMdd"));

			Assert.AreEqual<string>(list1[0].f.Value.ToString("yyyyMMddHHmmss"), ((DateTime)parameter1[5]).ToString("yyyyMMddHHmmss"));
			Assert.AreEqual<string>(list2[0].f.ToString("yyyyMMddHHmmss"), ((DateTime)parameter1[5]).ToString("yyyyMMddHHmmss"));

			Assert.AreEqual<int>(list1[0].j.Value, (int)parameter1[9]);
			Assert.AreEqual<int>(list2[0].j, (int)parameter1[9]);

			Assert.AreEqual<string>(list1[0].x.Value.ToString(), parameter1[23].ToString());
			Assert.AreEqual<string>(list2[0].x.ToString(), parameter1[23].ToString());

			Assert.AreEqual<string>(list1[0].y, (string)parameter1[24]);
			Assert.AreEqual<string>(list2[0].y, (string)parameter1[24]);

			Assert.AreEqual<string>(list1[0].z.Trim(), (string)parameter1[25]);
			Assert.AreEqual<string>(list2[0].z.Trim(), (string)parameter1[25]);

			Assert.AreEqual<string>(list1[0].a1.Value.ToString("yyyyMMddHHmmss"), ((DateTimeOffset)parameter1[26]).ToString("yyyyMMddHHmmss"));
			Assert.AreEqual<string>(list2[0].a1.ToString("yyyyMMddHHmmss"), ((DateTimeOffset)parameter1[26]).ToString("yyyyMMddHHmmss"));

			Assert.AreEqual<string>(list1[0].b1, (string)parameter1[27]);
			Assert.AreEqual<string>(list1[0].b1, (string)parameter1[27]);

			List<TestDataType> list3 = CPQuery.Format("SELECT NULL AS a,NULL AS b,NULL AS c,NULL AS d,NULL AS e,NULL AS f,NULL AS g,NULL AS h,NULL AS i,NULL AS j,NULL AS k,NULL AS l,NULL AS m,NULL AS n,NULL AS o,NULL AS p,NULL AS q,NULL AS r,NULL AS s,NULL AS t,NULL AS u,NULL AS v,NULL AS w,NULL AS x,NULL AS y,NULL AS z, NULL AS a1, NULL AS b1, NULL AS c1, NULL AS d1, NULL AS e1, NULL AS f1 FROM TestDataType").ToList<TestDataType>();
			List<TestDataTypeNoNull> list4 = CPQuery.Format("SELECT NULL AS a,NULL AS b,NULL AS c,NULL AS d,NULL AS e,NULL AS f,NULL AS g,NULL AS h,NULL AS i,NULL AS j,NULL AS k,NULL AS l,NULL AS m,NULL AS n,NULL AS o,NULL AS p,NULL AS q,NULL AS r,NULL AS s,NULL AS t,NULL AS u,NULL AS v,NULL AS w,NULL AS x,NULL AS y,NULL AS z, NULL AS a1, NULL AS b1, NULL AS c1, NULL AS d1, NULL AS e1, NULL AS f1 FROM TestDataType").ToList<TestDataTypeNoNull>();
			Assert.AreEqual<int>(1, list3.Count);
			Assert.AreEqual<int>(1, list4.Count);

			Assert.AreEqual<object>(list3[0].a, null);
			Assert.AreEqual<long>(list4[0].a, 0);

			Assert.AreEqual<object>(list3[0].c, null);
			Assert.AreEqual<bool>(list4[0].c, false);

			Assert.AreEqual<object>(list3[0].e, null);
			Assert.AreEqual<DateTime>(list4[0].e, DateTime.MinValue);

			Assert.AreEqual<object>(list3[0].f, null);
			Assert.AreEqual<DateTime>(list4[0].f, DateTime.MinValue);

			Assert.AreEqual<object>(list3[0].j, null);
			Assert.AreEqual<int>(list4[0].j, 0);

			Assert.AreEqual<object>(list3[0].x, null);
			Assert.AreEqual<Guid>(list4[0].x, Guid.Empty);

			Assert.AreEqual<string>(list3[0].y, null);
			Assert.AreEqual<string>(list4[0].y, null);

			Assert.AreEqual<string>(list3[0].z, null);
			Assert.AreEqual<string>(list4[0].z, null);

			Assert.AreEqual<string>(list3[0].b1, null);
			Assert.AreEqual<string>(list4[0].b1, null);

			//测试反射分支的场景
			List<TestDataTypeReflection> list5 = CPQuery.Format("SELECT NULL AS a,NULL AS b,NULL AS c,NULL AS d,NULL AS e,NULL AS f,NULL AS g,NULL AS h,NULL AS i,NULL AS j,NULL AS k,NULL AS l,NULL AS m,NULL AS n,NULL AS o,NULL AS p,NULL AS q,NULL AS r,NULL AS s,NULL AS t,NULL AS u,NULL AS v,NULL AS w,NULL AS x,NULL AS y,NULL AS z, NULL AS a1, NULL AS b1, NULL AS c1, NULL AS d1, NULL AS e1, NULL AS f1 FROM TestDataType").ToList<TestDataTypeReflection>();
			Assert.AreEqual<int>(1, list5.Count);
		}



		/// <summary>
		/// 测试多线程场景下,是否能够正确返回实体
		/// </summary>
		/// <remarks>
		/// <list type="bullet">
		/// <item><description>测试4线程场景</description></item>
		/// <item><description>测试返回实体条数是否正确</description></item>
		/// </list>
		/// </remarks>
		[SmokingTestLibrary.TestMethod(Order = 3, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void TestMutilThread()
		{
			DeleteAll();
			for( int i = 0; i < 100; i++ ) {
				Insert1();
			}
			var threads = new List<Thread> { new Thread(() => {
				
				for (int i = 0; i < 100; i++){
					List<TestDataType> list1 = CPQuery.Format("SELECT * FROM TestDataType").ToList<TestDataType>();
					List<TestDataTypeNoNull> list2 = CPQuery.Format("SELECT * FROM TestDataType").ToList<TestDataTypeNoNull>();

					Assert.AreEqual<int>(100, list1.Count);
					Assert.AreEqual<int>(100, list2.Count);
				}
			}), new Thread(() => {
				for (int i = 0; i < 100; i++)
				{
					List<TestDataType> list1 = CPQuery.Format("SELECT * FROM TestDataType").ToList<TestDataType>();
					List<TestDataTypeNoNull> list2 = CPQuery.Format("SELECT * FROM TestDataType").ToList<TestDataTypeNoNull>();

					Assert.AreEqual<int>(100, list1.Count);
					Assert.AreEqual<int>(100, list2.Count);
				}
			}), new Thread(() => {
				for (int i = 0; i < 100; i++)
				{
					List<TestDataType> list1 = CPQuery.Format("SELECT * FROM TestDataType").ToList<TestDataType>();
					List<TestDataTypeNoNull> list2 = CPQuery.Format("SELECT * FROM TestDataType").ToList<TestDataTypeNoNull>();

					Assert.AreEqual<int>(100, list1.Count);
					Assert.AreEqual<int>(100, list2.Count);
				}
			}), new Thread(() => {
					List<TestDataType> list1 = CPQuery.Format("SELECT * FROM TestDataType").ToList<TestDataType>();
					List<TestDataTypeNoNull> list2 = CPQuery.Format("SELECT * FROM TestDataType").ToList<TestDataTypeNoNull>();

					Assert.AreEqual<int>(100, list1.Count);
					Assert.AreEqual<int>(100, list2.Count);
			}) };

			foreach(Thread thread in threads){
				thread.Start();
			}

			foreach (Thread thread in threads)
			{
				thread.Join();
			}

			int count =  "SELECT COUNT(*) FROM TestDataType".AsCPQuery().ExecuteScalar<int>();
			Assert.AreEqual<int>(100, count);
		}

		/// <summary>
		/// 测试表查询,即一个SQL语句返回多个结果集
		/// </summary>
		/// <remarks>
		/// <list type="bullet">
		/// <item><description>存储过程与SQL场景</description></item>
		/// <item><description>测试返回2个结果集的场景</description></item>
		/// <item><description>测试两个结果集返回的表名及行数是否正确</description></item>
		/// </list>
		/// </remarks>
		[SmokingTestLibrary.TestMethod(Order = 4)]
		[NUnit.Framework.Test]
		public void TestMutilTable()
		{
			DeleteAll();

			//写入三行记录
			Insert1();
			Insert1();
			Insert1();

			DataSet ds1 = CPQuery.From("SELECT * FROM TestDataType;SELECT * FROM TestDataType").FillDataSet();
			Assert.AreEqual<int>(2, ds1.Tables.Count);

			int index = 0;
			foreach( DataTable table in ds1.Tables ) {

				//验证表名是否获取得到
				Assert.AreEqual<string>(table.TableName, "_tb" + index.ToString());

				//返回的集合数量应该是3行
				List<TestDataType> list = table.ToList<TestDataType>();
				Assert.AreEqual<int>(list.Count, 3);

				//验证反射版本的DataTable是否能够正确获取
				List<TestDataTypeReflection> listReflection = table.ToList<TestDataTypeReflection>();
				Assert.AreEqual<int>(listReflection.Count, 3);

				index++;
			}

			DataSet ds2 = StoreProcedure.Create("usp_GetTestDataType").FillDataSet();
			Assert.AreEqual<int>(2, ds2.Tables.Count);

			index = 0;
			foreach( DataTable table in ds2.Tables ) {

				//验证表名是否获取得到
				Assert.AreEqual<string>(table.TableName, "_tb" + index.ToString());

				//返回的集合数量应该是3行
				List<TestDataType> list = table.ToList<TestDataType>();
				Assert.AreEqual<int>(list.Count, 3);

				//验证反射版本的DataTable是否能够正确获取
				List<TestDataTypeReflection> listReflection = table.ToList<TestDataTypeReflection>();
				Assert.AreEqual<int>(listReflection.Count, 3);

				index++;
			}
		}

		/// <summary>
		/// 验证异常是否能够正确抛出
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 4)]
		[NUnit.Framework.Test]
		public void TestException()
		{
			//实体继承自BaseEntity,但不放在*.Entity.dll文件中,验证异常是否能正确抛出
			TestDataTypeNotInEntity obj1 = new TestDataTypeNotInEntity();
			obj1.a = 95;

			bool bEx = false;

			try {
				obj1.Insert();
			}
			catch( NonStandardExecption ) {
				bEx = true;
			}
			Assert.AreEqual<bool>(true, bEx);

			bEx = false;
			try {
				obj1.Update();
			}
			catch( NonStandardExecption ) {
				bEx = true;
			}
			Assert.AreEqual<bool>(true, bEx);

			bEx = false;
			try {
				obj1.Delete();
			}
			catch( NonStandardExecption ) {
				bEx = true;
			}
			Assert.AreEqual<bool>(true, bEx);

			TestDataType obj2 = new TestDataType();

			//直接new出对象,没办法生成正确的SQL语句
			bEx = false;
			try {
				obj2.Insert();
			}
			catch( InvalidOperationException ) {
				bEx = true;
			}
			Assert.AreEqual<bool>(true, bEx);

			//验证不生成任何语句的Update,应该返回0
			Products obj3 = new Products();
			obj3.ProductID = 999;
			int count = obj3.Update();
			Assert.AreEqual<int>(0, count);
		}
		/// <summary>
		/// TestDataType 实体类反射版本
		/// </summary>
		[DataEntity(Alias = "TestDataType")]
		public class TestDataTypeReflection
		{
			/// <summary>
			/// long类型的测试字段
			/// </summary>
			public long? a { get; set; }
			/// <summary>
			/// Byte[]类型测试字段
			/// </summary>
			public byte[] b { get; set; }
			/// <summary>
			/// Bool类型的测试字段
			/// </summary>
			public bool? c { get; set; }
			/// <summary>
			/// string类型的测试字段
			/// </summary>
			public string d { get; set; }
		}
		/// <summary>
		/// TestDataType 实体类CODEDOM版本
		/// </summary>
		[DataEntity(Alias = "TestDataType")]
		public class TestDataTypeNotInEntity : BaseEntity
		{
			/// <summary>
			/// long类型的测试字段
			/// </summary>
			public long? a { get; set; }
			/// <summary>
			/// Byte[]类型测试字段
			/// </summary>
			public byte[] b { get; set; }
			/// <summary>
			/// Bool类型的测试字段
			/// </summary>
			public bool? c { get; set; }
			/// <summary>
			/// string类型的测试字段
			/// </summary>
			public string d { get; set; }
		}
	}
}
