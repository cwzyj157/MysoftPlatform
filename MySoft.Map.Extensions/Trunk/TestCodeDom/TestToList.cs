using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using Mysoft.Map.Extensions.CodeDom;
using Mysoft.Map.Extensions.Xml;
using Mysoft.Map.Extensions.DAL;

namespace TestCodeDom
{

	public class TestToList
	{
		public static void Run()
		{
			string connectionString = (from s in System.IO.File.ReadAllLines(@"c:\test_erp_connectionString.txt")
									   where s.StartsWith(";") == false
									   select s
									).First();

			Console.WriteLine("Current ConnectionStirng: " + connectionString);

			Mysoft.Map.Extensions.Initializer.UnSafeInit(connectionString);

			try {
				//TestCodeGenerator();
				RunTestCase();
			}
			catch( Exception ex ) {
				Console.WriteLine(ex.ToString());
			}
		}


		static void TestCodeGenerator()
		{
			CodeGenerator generator = new CodeGenerator(typeof(SmokingTest.CS.Entity.SDyContract));
			string code = generator.GetCode("TestCUD1");

			code = CodeGenerator.GetCodeHeader() + code + "}";

			System.IO.File.WriteAllText("..\\TestToListCode.cs", code, Encoding.UTF8);
			//Console.WriteLine(code);

		}

		static void RunTestCase()
		{
//            string xml = @"<Data><TestXmlToEntity keyname='EntityGUID' keyvalue='4b463a54-69b4-47dc-a553-05c1e041a1bd'>
//								<LongVal>2095752454</LongVal>
//								<BinVal>Wfz9Xu2bpLs=</BinVal>
//								<BitVal>true</BitVal>
//								<CharVal>char</CharVal>
//								<DateVal>2013-05-29 15:38:00</DateVal>
//								<DtmVal>2013-05-29 15:38:00</DtmVal>
//								<DecVal>0.25</DecVal>
//								<FloatVal>0.54</FloatVal>
//								<ImgVal>Wfz9Xu2bpLs=</ImgVal>
//								<IntVal>48430913</IntVal>
//								<MoneyVal>0.26</MoneyVal>
//								<NcharVal>nchar</NcharVal>
//								<NtextVal>ntext</NtextVal>
//								<NumVal>0.16</NumVal>
//								<NvarcharVal>nvarchar</NvarcharVal>
//								<RealVal>0.65</RealVal>
//								<SmallDtmVal>2013-05-29 15:38:00</SmallDtmVal>
//								<SmallIntVal>2774</SmallIntVal>
//								<SmallMoneyVal>0.95</SmallMoneyVal>
//								<TextVal>text</TextVal>
//								<TintVal>254</TintVal>
//								<GuidVal>1214746e-1950-47a9-9c68-fb3a2cda0981</GuidVal>
//								<VcharVal>varchar</VcharVal>
//								<DtmOffsetVal>2013-05-29 15:38:00</DtmOffsetVal>
//								<XmlVal>xml</XmlVal>
//								<VarbinVal>Wfz9Xu2bpLs=</VarbinVal>
//								<Dtm2Val>2013-05-29 15:38:00</Dtm2Val>
//								<TimeVal>15:38:00</TimeVal>
//								<TsVal>600871</TsVal>
//							</TestXmlToEntity>
//							<TestXmlToEntity keyname='EntityGUID' keyvalue='4b463a54-69b4-47dc-a553-05c1e041a1bd'>
//								<LongVal>2095752454</LongVal>
//								<BinVal>Wfz9Xu2bpLs=</BinVal>
//								<BitVal>true</BitVal>
//								<CharVal>char</CharVal>
//								<DateVal>2013-05-29 15:38:00</DateVal>
//								<DtmVal>2013-05-29 15:38:00</DtmVal>
//								<DecVal>0.25</DecVal>
//								<FloatVal>0.54</FloatVal>
//								<ImgVal>Wfz9Xu2bpLs=</ImgVal>
//								<IntVal>48430913</IntVal>
//								<MoneyVal>0.26</MoneyVal>
//								<NcharVal>nchar</NcharVal>
//								<NtextVal>ntext</NtextVal>
//								<NumVal>0.16</NumVal>
//								<NvarcharVal>nvarchar</NvarcharVal>
//								<RealVal>0.65</RealVal>
//								<SmallDtmVal>2013-05-29 15:38:00</SmallDtmVal>
//								<SmallIntVal>2774</SmallIntVal>
//								<SmallMoneyVal>0.95</SmallMoneyVal>
//								<TextVal>text</TextVal>
//								<TintVal>254</TintVal>
//								<GuidVal>1214746e-1950-47a9-9c68-fb3a2cda0981</GuidVal>
//								<VcharVal>varchar</VcharVal>
//								<DtmOffsetVal>2013-05-29 15:38:00</DtmOffsetVal>
//								<XmlVal>xml</XmlVal>
//								<VarbinVal>Wfz9Xu2bpLs=</VarbinVal>
//								<Dtm2Val>2013-05-29 15:38:00</Dtm2Val>
//								<TimeVal>15:38:00</TimeVal>
//								<TsVal>600871</TsVal>
//							</TestXmlToEntity></Data>";

			//_Tool.AutoGenerateCode.TestCUD1.XmlToList(xml);


			DataTable dt = CPQuery.From("SELECT CAST(@@DBTS AS BIGINT) AS ContractVersion").FillDataTable();

			_Tool.AutoGenerateCode.TestCUD1.DataTableToList(dt);
		}
	}
}
