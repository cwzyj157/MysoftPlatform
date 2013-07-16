using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using SmokingTestLibrary;

using Mysoft.Map.Extensions.DAL;
using SmokingTest.DAL;

using SmokingTest.CS.Entity;
using SmokingTest.VB.Entity;

namespace SmokingTest.DAL
{
	/// <summary>
	/// 测试CodeDom场景下,实体查询
	/// </summary>
	[SmokingTestLibrary.Test(InNewThread = true, RunTimes = 5)]
	[NUnit.Framework.TestFixture]
	public class TestCodeDom
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
		[NUnit.Framework.Test]
		public void InitData()
		{
			var query = "SELECT OBJECT_ID('TestCodeDom')".AsCPQuery();
			var objectId = query.ExecuteScalar<long>();
			if( objectId > 0 )
				"drop table [TestCodeDom]".AsCPQuery().ExecuteNonQuery();

			//if( Program.SqlVeresion == 8 ) {
			//	table2000.AsCPQuery().ExecuteNonQuery();
			//}
			//if( Program.SqlVeresion == 9 ) {
			//	table2000.AsCPQuery().ExecuteNonQuery();
			//}
			//if( Program.SqlVeresion >= 10 ) {
			//	table2008.AsCPQuery().ExecuteNonQuery();
			//}

			table2008.AsCPQuery().ExecuteNonQuery();
		}

		string table2008 = @"CREATE TABLE [dbo].[TestCodeDom](
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

			CPQuery query = CPQuery.Format(@"INSERT INTO TestCodeDom([a], [b], [c], [d], [e], [f], [g], [h], [i], [j], [k], [l], [m], [n], [o], [p], [q], [r], [s], [t], [u], [v], [w], [x], [y], [z], [a1], [b1], [c1], [d1], [f1])
			VALUES({0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26},{27},{28},{29},{30})", parameter);
			query.ExecuteNonQuery();

			return parameter;
		}

		private void DeleteAll()
		{
			"DELETE FROM TestCodeDom".AsCPQuery().ExecuteNonQuery();
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

			TestCodeDomCS obj1 = CPQuery.Format("SELECT * FROM TestCodeDom").ToSingle<TestCodeDomCS>();
			Assert.AreEqual("Test", obj1.d.Trim());

			TestCodeDomCS obj2 = CPQuery.Format("SELECT NULL AS a,NULL AS b,NULL AS c,NULL AS d,NULL AS e,NULL AS f,NULL AS g,NULL AS h,NULL AS i,NULL AS j,NULL AS k,NULL AS l,NULL AS m,NULL AS n,NULL AS o,NULL AS p,NULL AS q,NULL AS r,NULL AS s,NULL AS t,NULL AS u,NULL AS v,NULL AS w,NULL AS x,NULL AS y,NULL AS z, NULL AS a1, NULL AS b1, NULL AS c1, NULL AS d1, NULL AS e1, NULL AS f1 FROM TestCodeDom").ToSingle<TestCodeDomCS>();
			Assert.AreEqual(null, obj2.d);

			TestCodeDomCSNotNull obj3 = CPQuery.Format("SELECT NULL AS a,NULL AS b,NULL AS c,NULL AS d,NULL AS e,NULL AS f,NULL AS g,NULL AS h,NULL AS i,NULL AS j,NULL AS k,NULL AS l,NULL AS m,NULL AS n,NULL AS o,NULL AS p,NULL AS q,NULL AS r,NULL AS s,NULL AS t,NULL AS u,NULL AS v,NULL AS w,NULL AS x,NULL AS y,NULL AS z, NULL AS a1, NULL AS b1, NULL AS c1, NULL AS d1, NULL AS e1, NULL AS f1 FROM TestCodeDom").ToSingle<TestCodeDomCSNotNull>();
			Assert.AreEqual((long)0, obj3.a);

			TestCodeDomVBToString obj4 = CPQuery.Format("SELECT * FROM TestCodeDom").ToSingle<TestCodeDomVBToString>();
			Assert.AreEqual(obj4.x, parameters[23].ToString());

			TestCodeDomVB obj5 = CPQuery.Format("SELECT * FROM TestCodeDom").ToSingle<TestCodeDomVB>();
			Assert.AreEqual("Test", obj1.d.Trim());

			TestCodeDomVB obj6 = CPQuery.Format("SELECT NULL AS a,NULL AS b,NULL AS c,NULL AS d,NULL AS e,NULL AS f,NULL AS g,NULL AS h,NULL AS i,NULL AS j,NULL AS k,NULL AS l,NULL AS m,NULL AS n,NULL AS o,NULL AS p,NULL AS q,NULL AS r,NULL AS s,NULL AS t,NULL AS u,NULL AS v,NULL AS w,NULL AS x,NULL AS y,NULL AS z, NULL AS a1, NULL AS b1, NULL AS c1, NULL AS d1, NULL AS e1, NULL AS f1 FROM TestCodeDom").ToSingle<TestCodeDomVB>();
			Assert.AreEqual(null, obj2.d);

			TestCodeDomVBNotNull obj7 = CPQuery.Format("SELECT NULL AS a,NULL AS b,NULL AS c,NULL AS d,NULL AS e,NULL AS f,NULL AS g,NULL AS h,NULL AS i,NULL AS j,NULL AS k,NULL AS l,NULL AS m,NULL AS n,NULL AS o,NULL AS p,NULL AS q,NULL AS r,NULL AS s,NULL AS t,NULL AS u,NULL AS v,NULL AS w,NULL AS x,NULL AS y,NULL AS z, NULL AS a1, NULL AS b1, NULL AS c1, NULL AS d1, NULL AS e1, NULL AS f1 FROM TestCodeDom").ToSingle<TestCodeDomVBNotNull>();
			Assert.AreEqual((long)0, obj3.a);

			TestCodeDomVBToString obj8 = CPQuery.Format("SELECT * FROM TestCodeDom").ToSingle<TestCodeDomVBToString>();
			Assert.AreEqual(obj4.x, parameters[23].ToString());

		}

		/// <summary>
		/// 测试C#实体多条数据查询
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
		[SmokingTestLibrary.TestMethod(Order = 2, RunTimes = 1)]
		private void TestCSList()
		{
			DeleteAll();
			object[] parameter1 = Insert1();
			List<TestCodeDomCS> list1 = CPQuery.Format("SELECT * FROM TestCodeDom").ToList<TestCodeDomCS>();
			List<TestCodeDomCSNotNull> list2 = CPQuery.Format("SELECT * FROM TestCodeDom").ToList<TestCodeDomCSNotNull>();

			Assert.AreEqual(1, list1.Count);
			Assert.AreEqual(1, list2.Count);

			Assert.AreEqual(list1[0].a.Value, (long)parameter1[0]);
			Assert.AreEqual(list2[0].a, (long)parameter1[0]);

			Assert.AreEqual(list1[0].c.Value, (bool)parameter1[2]);
			Assert.AreEqual(list2[0].c, (bool)parameter1[2]);

			Assert.AreEqual(list1[0].e.Value.ToString("yyyyMMdd"), ((DateTime)parameter1[4]).ToString("yyyyMMdd"));
			Assert.AreEqual(list2[0].e.ToString("yyyyMMdd"), ((DateTime)parameter1[4]).ToString("yyyyMMdd"));

			Assert.AreEqual(list1[0].f.Value.ToString("yyyyMMddHHmmss"), ((DateTime)parameter1[5]).ToString("yyyyMMddHHmmss"));
			Assert.AreEqual(list2[0].f.ToString("yyyyMMddHHmmss"), ((DateTime)parameter1[5]).ToString("yyyyMMddHHmmss"));

			Assert.AreEqual(list1[0].j.Value, (int)parameter1[9]);
			Assert.AreEqual(list2[0].j, (int)parameter1[9]);

			Assert.AreEqual(list1[0].x.Value.ToString(), parameter1[23].ToString());
			Assert.AreEqual(list2[0].x.ToString(), parameter1[23].ToString());

			Assert.AreEqual(list1[0].y, (string)parameter1[24]);
			Assert.AreEqual(list2[0].y, (string)parameter1[24]);

			Assert.AreEqual(list1[0].z.Trim(), (string)parameter1[25]);
			Assert.AreEqual(list2[0].z.Trim(), (string)parameter1[25]);

			Assert.AreEqual(list1[0].a1.Value.ToString("yyyyMMddHHmmss"), ((DateTimeOffset)parameter1[26]).ToString("yyyyMMddHHmmss"));
			Assert.AreEqual(list2[0].a1.ToString("yyyyMMddHHmmss"), ((DateTimeOffset)parameter1[26]).ToString("yyyyMMddHHmmss"));

			Assert.AreEqual(list1[0].b1, (string)parameter1[27]);
			Assert.AreEqual(list1[1].b1, (string)parameter1[27]);

			List<TestCodeDomCS> list3 = CPQuery.Format("SELECT NULL AS a,NULL AS b,NULL AS c,NULL AS d,NULL AS e,NULL AS f,NULL AS g,NULL AS h,NULL AS i,NULL AS j,NULL AS k,NULL AS l,NULL AS m,NULL AS n,NULL AS o,NULL AS p,NULL AS q,NULL AS r,NULL AS s,NULL AS t,NULL AS u,NULL AS v,NULL AS w,NULL AS x,NULL AS y,NULL AS z, NULL AS a1, NULL AS b1, NULL AS c1, NULL AS d1, NULL AS e1, NULL AS f1 FROM TestCodeDom").ToList<TestCodeDomCS>();
			List<TestCodeDomCSNotNull> list4 = CPQuery.Format("SELECT NULL AS a,NULL AS b,NULL AS c,NULL AS d,NULL AS e,NULL AS f,NULL AS g,NULL AS h,NULL AS i,NULL AS j,NULL AS k,NULL AS l,NULL AS m,NULL AS n,NULL AS o,NULL AS p,NULL AS q,NULL AS r,NULL AS s,NULL AS t,NULL AS u,NULL AS v,NULL AS w,NULL AS x,NULL AS y,NULL AS z, NULL AS a1, NULL AS b1, NULL AS c1, NULL AS d1, NULL AS e1, NULL AS f1 FROM TestCodeDom").ToList<TestCodeDomCSNotNull>();
			Assert.AreEqual(1, list3.Count);
			Assert.AreEqual(1, list4.Count);

			Assert.AreEqual(list3[0].a, null);
			Assert.AreEqual(list4[0].a, 0);

			Assert.AreEqual(list3[0].c, null);
			Assert.AreEqual(list4[0].c, false);

			Assert.AreEqual(list3[0].e, null);
			Assert.AreEqual(list4[0].e, DateTime.MinValue);

			Assert.AreEqual(list3[0].f, null);
			Assert.AreEqual(list4[0].f, DateTime.MinValue);

			Assert.AreEqual(list3[0].j, null);
			Assert.AreEqual(list4[0].j, 0);

			Assert.AreEqual(list3[0].x, null);
			Assert.AreEqual(list4[0].x, Guid.Empty);

			Assert.AreEqual(list3[0].y, null);
			Assert.AreEqual(list4[0].y, null);

			Assert.AreEqual(list3[0].z, null);
			Assert.AreEqual(list4[0].z, null);

			Assert.AreEqual(list3[0].b1, null);
			Assert.AreEqual(list4[1].b1, null);
		}

		/// <summary>
		/// 测试VB实体多条数据查询
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
		[SmokingTestLibrary.TestMethod(Order = 3, RunTimes = 1)]
		private void TestVBList()
		{
			DeleteAll();
			object[] parameter1 = Insert1();
			List<TestCodeDomVB> list1 = CPQuery.Format("SELECT * FROM TestCodeDom").ToList<TestCodeDomVB>();
			List<TestCodeDomVBNotNull> list2 = CPQuery.Format("SELECT * FROM TestCodeDom").ToList<TestCodeDomVBNotNull>();

			Assert.AreEqual(1, list1.Count);
			Assert.AreEqual(1, list2.Count);

			Assert.AreEqual(list1[0].a.Value, (long)parameter1[0]);
			Assert.AreEqual(list2[0].a, (long)parameter1[0]);

			Assert.AreEqual(list1[0].c.Value, (bool)parameter1[2]);
			Assert.AreEqual(list2[0].c, (bool)parameter1[2]);

			Assert.AreEqual(list1[0].e.Value.ToString("yyyyMMdd"), ((DateTime)parameter1[4]).ToString("yyyyMMdd"));
			Assert.AreEqual(list2[0].e.ToString("yyyyMMdd"), ((DateTime)parameter1[4]).ToString("yyyyMMdd"));

			Assert.AreEqual(list1[0].f.Value.ToString("yyyyMMddHHmmss"), ((DateTime)parameter1[5]).ToString("yyyyMMddHHmmss"));
			Assert.AreEqual(list2[0].f.ToString("yyyyMMddHHmmss"), ((DateTime)parameter1[5]).ToString("yyyyMMddHHmmss"));

			Assert.AreEqual(list1[0].j.Value, (int)parameter1[9]);
			Assert.AreEqual(list2[0].j, (int)parameter1[9]);

			Assert.AreEqual(list1[0].x.Value.ToString(), parameter1[23].ToString());
			Assert.AreEqual(list2[0].x.ToString(), parameter1[23].ToString());

			Assert.AreEqual(list1[0].y, (string)parameter1[24]);
			Assert.AreEqual(list2[0].y, (string)parameter1[24]);

			Assert.AreEqual(list1[0].z.Trim(), (string)parameter1[25]);
			Assert.AreEqual(list2[0].z.Trim(), (string)parameter1[25]);

			Assert.AreEqual(list1[0].a1.Value.ToString("yyyyMMddHHmmss"), ((DateTimeOffset)parameter1[26]).ToString("yyyyMMddHHmmss"));
			Assert.AreEqual(list2[0].a1.ToString("yyyyMMddHHmmss"), ((DateTimeOffset)parameter1[26]).ToString("yyyyMMddHHmmss"));

			Assert.AreEqual(list1[0].b1, (string)parameter1[27]);
			Assert.AreEqual(list1[1].b1, (string)parameter1[27]);

			List<TestCodeDomVB> list3 = CPQuery.Format("SELECT NULL AS a,NULL AS b,NULL AS c,NULL AS d,NULL AS e,NULL AS f,NULL AS g,NULL AS h,NULL AS i,NULL AS j,NULL AS k,NULL AS l,NULL AS m,NULL AS n,NULL AS o,NULL AS p,NULL AS q,NULL AS r,NULL AS s,NULL AS t,NULL AS u,NULL AS v,NULL AS w,NULL AS x,NULL AS y,NULL AS z, NULL AS a1, NULL AS b1, NULL AS c1, NULL AS d1, NULL AS e1, NULL AS f1 FROM TestCodeDom").ToList<TestCodeDomVB>();
			List<TestCodeDomVBNotNull> list4 = CPQuery.Format("SELECT NULL AS a,NULL AS b,NULL AS c,NULL AS d,NULL AS e,NULL AS f,NULL AS g,NULL AS h,NULL AS i,NULL AS j,NULL AS k,NULL AS l,NULL AS m,NULL AS n,NULL AS o,NULL AS p,NULL AS q,NULL AS r,NULL AS s,NULL AS t,NULL AS u,NULL AS v,NULL AS w,NULL AS x,NULL AS y,NULL AS z, NULL AS a1, NULL AS b1, NULL AS c1, NULL AS d1, NULL AS e1, NULL AS f1 FROM TestCodeDom").ToList<TestCodeDomVBNotNull>();
			Assert.AreEqual(1, list3.Count);
			Assert.AreEqual(1, list4.Count);

			Assert.AreEqual(list3[0].a, null);
			Assert.AreEqual(list4[0].a, 0);

			Assert.AreEqual(list3[0].c, null);
			Assert.AreEqual(list4[0].c, false);

			Assert.AreEqual(list3[0].e, null);
			Assert.AreEqual(list4[0].e, DateTime.MinValue);

			Assert.AreEqual(list3[0].f, null);
			Assert.AreEqual(list4[0].f, DateTime.MinValue);

			Assert.AreEqual(list3[0].j, null);
			Assert.AreEqual(list4[0].j, 0);

			Assert.AreEqual(list3[0].x, null);
			Assert.AreEqual(list4[0].x, Guid.Empty);

			Assert.AreEqual(list3[0].y, null);
			Assert.AreEqual(list4[0].y, null);

			Assert.AreEqual(list3[0].z, null);
			Assert.AreEqual(list4[0].z, null);

			Assert.AreEqual(list3[0].b1, null);
			Assert.AreEqual(list4[1].b1, null);
		}
	}
}
