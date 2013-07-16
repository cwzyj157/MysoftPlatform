using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmokingTestLibrary;
using SmokingTest.CS.Entity;
using Mysoft.Map.Extensions;
using Mysoft.Map.Extensions.DAL;
using System.Reflection;
using Mysoft.Map.Extensions.Xml;

namespace SmokingTest.DAL
{
	/// <summary>
	/// 测试Map平台下xml实体转换为对象实体的用例
	/// </summary>
	[SmokingTestLibrary.Test(InNewThread = false)]
	[NUnit.Framework.TestFixture]
	public class TestXmlToEntity
	{
		/// <summary>
		/// 初始化表
		/// </summary>
		[TestMethod(Order = 0)]
		public void InitData()
		{
			var query = "SELECT OBJECT_ID('TestXmlToEntity')".AsCPQuery();
			var objectId = query.ExecuteScalar<long>();
			if( objectId > 0 )
				"drop table [TestXmlToEntity]".AsCPQuery().ExecuteNonQuery();

			string sql = @"CREATE TABLE [TestXmlToEntity](
							[EntityGUID] [UNIQUEIDENTIFIER] PRIMARY KEY NOT NULL,
							[Long_val] [bigint] NULL,
							[binVal] [binary](50) NULL,
							[bitVal] [bit] NULL,
							[charVal] [char](10) NULL,
							[dateVal] [date] NULL,
							[dtmVal] [datetime] NULL,
							[decVal] [decimal](18, 4) NULL,
							[floatVal] [float] NULL,
							[imgVal] [image] NULL,
							[intVal] [int] NULL,
							[moneyVal] [money] NULL,
							[ncharVal] [nchar](10) NULL,
							[ntextVal] [ntext] NULL,
							[numVal] [numeric](18, 4) NULL,
							[nvarcharVal] [nvarchar](50) NULL,
							[realVal] [real] NULL,
							[smallDtmVal] [smalldatetime] NULL,
							[smallIntVal] [smallint] NULL,
							[smallMoneyVal] [smallmoney] NULL,
							[textVal] [text] NULL,
							[tintVal] [tinyint] NULL,
							[guidVal] [uniqueidentifier] NULL,
							[vcharVal] [varchar](50) NULL,
							[dtmOffsetVal] [datetimeoffset] NULL,
							[xmlVal] [xml] NULL,
							[varbinVal] [varbinary](8) NULL,
							[dtm2Val] [datetime2] NULL,
							[timeVal] [time] NULL,
							[tsVal] [timestamp] NOT NULL,
						)";

			sql.AsCPQuery().ExecuteNonQuery();
		}

		private TestXmlToEntityPO Create()
		{
			byte[] buffer = new byte[8];
			Random rnd = new Random(Guid.NewGuid().GetHashCode());

			TestXmlToEntityPO po = new TestXmlToEntityPO();
			po.EntityGUID = Guid.NewGuid();
			po.LongVal = (long)rnd.Next();
			rnd.NextBytes(buffer);
			po.BinVal = buffer;
			po.BitVal = true;
			po.CharVal = "char";
			po.DateVal = DateTime.Now;
			po.DtmVal = DateTime.Now;
			po.DecVal = (decimal)rnd.NextDouble();
			po.FloatVal = rnd.NextDouble();
			rnd.NextBytes(buffer);
			po.ImgVal = buffer;
			po.IntVal = rnd.Next();
			po.MoneyVal = (decimal)rnd.NextDouble();
			po.NcharVal = "nchar";
			po.NtextVal = "ntext";
			po.NumVal = (decimal)rnd.NextDouble();
			po.NvarcharVal = "nvarchar";
			po.RealVal = (float)rnd.NextDouble();
			po.SmallDtmVal = DateTime.Now;
			po.SmallIntVal = (short)rnd.Next();
			po.SmallMoneyVal = (decimal)rnd.NextDouble();
			po.TextVal = "text";
			po.TintVal = 0xFE;
			po.GuidVal = Guid.NewGuid();
			po.VcharVal = "varchar";
			po.DtmOffsetVal = DateTime.Now;
			po.XmlVal = "xml";
			rnd.NextBytes(buffer);
			po.VarbinVal = buffer;
			po.Dtm2Val = DateTime.Now;
			po.TimeVal = DateTime.Now.TimeOfDay;

			return po;
		}

		private void ClearData()
		{
			"truncate table TestXmlToEntity".AsCPQuery().ExecuteNonQuery();
		}

		private void AssertEntity(TestXmlToEntityPO po, string priKey, long firstVal)
		{
			Assert.AreEqual<Guid>(po.EntityGUID, new Guid(priKey));
			Assert.AreEqual<long>(po.LongVal.Value, firstVal);
			Assert.AreEqual<string>(Convert.ToBase64String(po.BinVal), "Wfz9Xu2bpLs=");
			Assert.AreEqual<bool>(po.BitVal.Value, true);
			Assert.AreEqual<string>(po.CharVal.Trim(), "char");
			Assert.AreEqual<string>(po.DateVal.Value.ToString("yyyy-MM-dd HH:mm:ss"), "2013-05-29 15:38:00");
			Assert.AreEqual<string>(po.DtmVal.Value.ToString("yyyy-MM-dd HH:mm:ss"), "2013-05-29 15:38:00");
			Assert.AreEqual<decimal>(po.DecVal.Value, 0.25m);
			if( Math.Abs( po.FloatVal.Value - 0.54f) > 0.0001f ) {
				Assert.AreEqual<bool>(true, false);
			}
			Assert.AreEqual<string>(Convert.ToBase64String(po.ImgVal), "Wfz9Xu2bpLs=");
			Assert.AreEqual<int>(po.IntVal.Value, 48430913);
			Assert.AreEqual<decimal>(po.MoneyVal.Value, 0.26m);
			Assert.AreEqual<string>(po.NcharVal, "nchar");
			Assert.AreEqual<string>(po.NtextVal, "ntext");
			Assert.AreEqual<decimal>(po.NumVal.Value, 0.16m);
			Assert.AreEqual<string>(po.NvarcharVal, "nvarchar");
			if( Math.Abs(po.RealVal.Value - 0.65f) > 0.0001f ) {
				Assert.AreEqual<bool>(true, false);
			}
			Assert.AreEqual<string>(po.SmallDtmVal.Value.ToString("yyyy-MM-dd HH:mm:ss"), "2013-05-29 15:38:00");
			Assert.AreEqual<short>(po.SmallIntVal.Value, 2774);
			Assert.AreEqual<decimal>(po.SmallMoneyVal.Value, 123123m);
			Assert.AreEqual<string>(po.TextVal, "text");
			Assert.AreEqual<byte>(po.TintVal.Value, 254);
			Assert.AreEqual<Guid>(po.GuidVal.Value, new Guid("1214746e-1950-47a9-9c68-fb3a2cda0981"));
			Assert.AreEqual<string>(po.VcharVal, "varchar");
			Assert.AreEqual<string>(po.DtmOffsetVal.ToString("yyyy-MM-dd HH:mm:ss"), "2013-05-29 15:38:00");
			Assert.AreEqual<string>(po.XmlVal, "xml");
			Assert.AreEqual<string>(Convert.ToBase64String(po.VarbinVal), "Wfz9Xu2bpLs=");
			Assert.AreEqual<string>(po.Dtm2Val.ToString("yyyy-MM-dd HH:mm:ss"), "2013-05-29 15:38:00");
			Assert.AreEqual<string>(po.TimeVal.ToString(), "15:38:00");
			Assert.AreEqual<long>(po.TsVal.TimeStampToInt64(), (long)600871);
		}
		/// <summary>
		/// 测试XML转实体使用ConvertXmlToSingle
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 1, RunTimes = 2)]
		[NUnit.Framework.Test]
		public void TestSingle()
		{
			string xml = @"<TestXmlToEntity keyname='EntityGUID' keyvalue='4b463a54-69b4-47dc-a553-05c1e041a1bd'>
								<Long_val>2095752454</Long_val>
								<BinVal>Wfz9Xu2bpLs=</BinVal>
								<BitVal>true</BitVal>
								<CharVal>char</CharVal>
								<DateVal>2013-05-29 15:38:00</DateVal>
								<DtmVal>2013-05-29 15:38:00</DtmVal>
								<DecVal>0.25</DecVal>
								<FloatVal>0.54</FloatVal>
								<ImgVal>Wfz9Xu2bpLs=</ImgVal>
								<IntVal>48430913</IntVal>
								<MoneyVal>0.26</MoneyVal>
								<NcharVal>nchar</NcharVal>
								<NtextVal>ntext</NtextVal>
								<NumVal>0.16</NumVal>
								<NvarcharVal>nvarchar</NvarcharVal>
								<RealVal>0.65</RealVal>
								<SmallDtmVal>2013-05-29 15:38:00</SmallDtmVal>
								<SmallIntVal>2774</SmallIntVal>
								<SmallMoneyVal>123,123</SmallMoneyVal>
								<TextVal>text</TextVal>
								<TintVal>254</TintVal>
								<GuidVal>1214746e-1950-47a9-9c68-fb3a2cda0981</GuidVal>
								<VcharVal>varchar</VcharVal>
								<DtmOffsetVal>2013-05-29 15:38:00</DtmOffsetVal>
								<XmlVal>xml</XmlVal>
								<VarbinVal>Wfz9Xu2bpLs=</VarbinVal>
								<Dtm2Val>2013-05-29 15:38:00</Dtm2Val>
								<TimeVal>15:38:00</TimeVal>
								<TsVal>600871</TsVal>
							</TestXmlToEntity>";

			TestXmlToEntityPO po = XmlDataEntity.ConvertXmlToSingle<TestXmlToEntityPO>(xml);
			AssertEntity(po, "4b463a54-69b4-47dc-a553-05c1e041a1bd", 2095752454);
			ClearData();
			int rows = po.Insert();
			Assert.AreEqual<int>(1, rows);
		}
		/// <summary>
		///  测试XML转实体使用TestList
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 2, RunTimes = 2)]
		[NUnit.Framework.Test]
		public void TestList()
		{
			string xml = @"<Data><TestXmlToEntity keyname='EntityGUID' keyvalue='D5B0103C-2CB0-4944-BDF8-BC87EAC680BC'>
								<Long_val>2095752454</Long_val>
								<BinVal>Wfz9Xu2bpLs=</BinVal>
								<BitVal>true</BitVal>
								<CharVal>char</CharVal>
								<DateVal>2013-05-29 15:38:00</DateVal>
								<DtmVal>2013-05-29 15:38:00</DtmVal>
								<DecVal>0.25</DecVal>
								<FloatVal>0.54</FloatVal>
								<ImgVal>Wfz9Xu2bpLs=</ImgVal>
								<IntVal>48430913</IntVal>
								<MoneyVal>0.26</MoneyVal>
								<NcharVal>nchar</NcharVal>
								<NtextVal>ntext</NtextVal>
								<NumVal>0.16</NumVal>
								<NvarcharVal>nvarchar</NvarcharVal>
								<RealVal>0.65</RealVal>
								<SmallDtmVal>2013-05-29 15:38:00</SmallDtmVal>
								<SmallIntVal>2774</SmallIntVal>
								<SmallMoneyVal>123,123</SmallMoneyVal>
								<TextVal>text</TextVal>
								<TintVal>254</TintVal>
								<GuidVal>1214746e-1950-47a9-9c68-fb3a2cda0981</GuidVal>
								<VcharVal>varchar</VcharVal>
								<DtmOffsetVal>2013-05-29 15:38:00</DtmOffsetVal>
								<XmlVal>xml</XmlVal>
								<VarbinVal>Wfz9Xu2bpLs=</VarbinVal>
								<Dtm2Val>2013-05-29 15:38:00</Dtm2Val>
								<TimeVal>15:38:00</TimeVal>
								<TsVal>600871</TsVal>
							</TestXmlToEntity>
							<TestXmlToEntity keyname='EntityGUID' keyvalue='38F7252B-EAE8-4742-B0D3-17D299BE2F99'>
								<Long_val>2095752499</Long_val>
								<BinVal>Wfz9Xu2bpLs=</BinVal>
								<BitVal>true</BitVal>
								<CharVal>char</CharVal>
								<DateVal>2013-05-29 15:38:00</DateVal>
								<DtmVal>2013-05-29 15:38:00</DtmVal>
								<DecVal>0.25</DecVal>
								<FloatVal>0.54</FloatVal>
								<ImgVal>Wfz9Xu2bpLs=</ImgVal>
								<IntVal>48430913</IntVal>
								<MoneyVal>0.26</MoneyVal>
								<NcharVal>nchar</NcharVal>
								<NtextVal>ntext</NtextVal>
								<NumVal>0.16</NumVal>
								<NvarcharVal>nvarchar</NvarcharVal>
								<RealVal>0.65</RealVal>
								<SmallDtmVal>2013-05-29 15:38:00</SmallDtmVal>
								<SmallIntVal>2774</SmallIntVal>
								<SmallMoneyVal>123,123</SmallMoneyVal>
								<TextVal>text</TextVal>
								<TintVal>254</TintVal>
								<GuidVal>1214746e-1950-47a9-9c68-fb3a2cda0981</GuidVal>
								<VcharVal>varchar</VcharVal>
								<DtmOffsetVal>2013-05-29 15:38:00</DtmOffsetVal>
								<XmlVal>xml</XmlVal>
								<VarbinVal>Wfz9Xu2bpLs=</VarbinVal>
								<Dtm2Val>2013-05-29 15:38:00</Dtm2Val>
								<TimeVal>15:38:00</TimeVal>
								<TsVal>600871</TsVal>
							</TestXmlToEntity></Data>";

			List<TestXmlToEntityPO> pos = XmlDataEntity.ConvertXmlToList<TestXmlToEntityPO>(xml);
			Assert.AreEqual<int>(pos.Count, 2);

			AssertEntity(pos[0], "D5B0103C-2CB0-4944-BDF8-BC87EAC680BC", 2095752454);
			ClearData();
			int rows = pos[0].Insert();
			Assert.AreEqual<int>(1, rows);

			AssertEntity(pos[1], "38F7252B-EAE8-4742-B0D3-17D299BE2F99", 2095752499);
			ClearData();
			rows = pos[1].Insert();
			Assert.AreEqual<int>(1, rows);
		}
		/// <summary>
		/// 验证时间戳转LONG
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 3, RunTimes = 2)]
		[NUnit.Framework.Test]
		public void TestTimeStampConvert()
		{
			long lng = 600871;
			byte[] byts = lng.Int64ToTimeStamp();
			lng = byts.TimeStampToInt64();
			Assert.AreEqual<long>(lng, (long)600871);
		}
		/// <summary>
		/// 验证TestXmlToSingle异常场景
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 4, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void TestXmlToSingle()
		{
			string xml = @"<TestXmlToEntity keyname='EntityGUID' keyvalue='4b463a54-69b4-47dc-a553-05c1e041a1bd'>
								<Long_val>2095752454</Long_val>
								<BinVal>Wfz9Xu2bpLs=</BinVal>
							<TestXmlToEntity>";
			TestPackage.AssertException<System.Xml.XmlException>(() => { TestXmlToEntityPO po = XmlDataEntity.ConvertXmlToSingle<TestXmlToEntityPO>(xml); }, "出现意外的文件结尾");
			TestPackage.AssertException<ArgumentNullException>(() => { TestXmlToEntityPO po = XmlDataEntity.ConvertXmlToSingle<TestXmlToEntityPO>(null); }, "值不能为空");
			xml = @"<TestXmlToEntity keyvalue='4b463a54-69b4-47dc-a553-05c1e041a1bd'>
								<Long_val>2095752454</Long_val>
								<BinVal>Wfz9Xu2bpLs=</BinVal>
							</TestXmlToEntity>";
			TestPackage.AssertException<InvalidOperationException>(() => { TestXmlToEntityPO po = XmlDataEntity.ConvertXmlToSingle<TestXmlToEntityPO>(xml); }, "xml中不存在keyname属性");
			xml = @"<TestXmlToEntity keyname='EntityGUID' >
								<Long_val>2095752454</Long_val>
								<BinVal>Wfz9Xu2bpLs=</BinVal>
							</TestXmlToEntity>";
			TestPackage.AssertException<InvalidOperationException>(() => { TestXmlToEntityPO po = XmlDataEntity.ConvertXmlToSingle<TestXmlToEntityPO>(xml); }, "xml中不存在keyvalue属性");
		}
		/// <summary>
		///  验证TestConvertXmlToList异常场景
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 5, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void TestConvertXmlToList()
		{
			string xml = @"<TestXmlToEntity keyname='EntityGUID' keyvalue='4b463a54-69b4-47dc-a553-05c1e041a1bd'>
								<Long_val>2095752454</Long_val>
								<BinVal>Wfz9Xu2bpLs=</BinVal>
							<TestXmlToEntity>";
			TestPackage.AssertException<System.Xml.XmlException>(() => {  var po = XmlDataEntity.ConvertXmlToList<TestXmlToEntityPO>(xml); }, "出现意外的文件结尾");
			TestPackage.AssertException<ArgumentNullException>(() => { var po = XmlDataEntity.ConvertXmlToList<TestXmlToEntityPO>(null); }, "值不能为空");
			xml = @"<TestXmlToEntity keyvalue='4b463a54-69b4-47dc-a553-05c1e041a1bd'>
								<Long_val>2095752454</Long_val>
								<BinVal>Wfz9Xu2bpLs=</BinVal>
							</TestXmlToEntity>";
			TestPackage.AssertException<InvalidOperationException>(() => { var po = XmlDataEntity.ConvertXmlToList<TestXmlToEntityPO>(xml); }, "xml中不存在keyname属性");
			xml = @"<TestXmlToEntity keyname='EntityGUID' >
								<Long_val>2095752454</Long_val>
								<BinVal>Wfz9Xu2bpLs=</BinVal>
							</TestXmlToEntity>";
			TestPackage.AssertException<InvalidOperationException>(() => { var po = XmlDataEntity.ConvertXmlToList<TestXmlToEntityPO>(xml); }, "xml中不存在keyvalue属性");
		}

		/// <summary>
		/// 验证：实体定义为long，是否能进行反序列化
		/// 验证方法：ConvertXmlToSingle，ConvertXmlToList
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 6, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void TestSelectLong()
		{
			string xml = @"<TestXmlToEntity keyname='EntityGUID' keyvalue='4b463a54-69b4-47dc-a553-05c1e041a1bd'>
								<Long_val>2095752454</Long_val>
								<BinVal>Wfz9Xu2bpLs=</BinVal>
								<BitVal>true</BitVal>
								<CharVal>char</CharVal>
								<DateVal>2013-05-29 15:38:00</DateVal>
								<DtmVal>2013-05-29 15:38:00</DtmVal>
								<DecVal>0.25</DecVal>
								<FloatVal>0.54</FloatVal>
								<ImgVal>Wfz9Xu2bpLs=</ImgVal>
								<IntVal>48430913</IntVal>
								<MoneyVal>0.26</MoneyVal>
								<NcharVal>nchar</NcharVal>
								<NtextVal>ntext</NtextVal>
								<NumVal>0.16</NumVal>
								<NvarcharVal>nvarchar</NvarcharVal>
								<RealVal>0.65</RealVal>
								<SmallDtmVal>2013-05-29 15:38:00</SmallDtmVal>
								<SmallIntVal>2774</SmallIntVal>
								<SmallMoneyVal>123,123</SmallMoneyVal>
								<TextVal>text</TextVal>
								<TintVal>254</TintVal>
								<GuidVal>1214746e-1950-47a9-9c68-fb3a2cda0981</GuidVal>
								<VcharVal>varchar</VcharVal>
								<DtmOffsetVal>2013-05-29 15:38:00</DtmOffsetVal>
								<XmlVal>xml</XmlVal>
								<VarbinVal>Wfz9Xu2bpLs=</VarbinVal>
								<Dtm2Val>2013-05-29 15:38:00</Dtm2Val>
								<TimeVal>15:38:00</TimeVal>
								<TsVal>600871</TsVal>
							</TestXmlToEntity>";
			TestXmlToEntityPOLong po = XmlDataEntity.ConvertXmlToSingle<TestXmlToEntityPOLong>(xml);
			Assert.AreEqual<long>(po.TsVal, (long)600871);
			TestXmlToEntityPOLong poList = XmlDataEntity.ConvertXmlToList<TestXmlToEntityPOLong>(xml)[0];
			Assert.AreEqual<long>(poList.TsVal, (long)600871);
			ClearData();
			int rows = po.Insert();
			Assert.AreEqual<int>(1, rows);
		}
	}
}
