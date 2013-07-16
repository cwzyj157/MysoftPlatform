using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mysoft.Map.Extensions.DAL;
using Mysoft.Map.Extensions.CodeDom;
using _Tool.AutoGenerateCode;

namespace TestCodeDom
{
	[Serializable]
	public partial class TestNotNullDataTypeTable : BaseEntity
	{
		public long A { get; set; }
		public byte[] B { get; set; }
		public bool C { get; set; }
		public string D { get; set; }
		public DateTime E { get; set; }
		public DateTime F { get; set; }
		public decimal G { get; set; }
		public double H { get; set; }
		public byte[] I { get; set; }
		public int J { get; set; }
		public decimal K { get; set; }
		public string L { get; set; }
		public string M { get; set; }
		public decimal N { get; set; }
		public string O { get; set; }
		public string P { get; set; }
		public float Q { get; set; }
		public DateTime R { get; set; }
		public short S { get; set; }
		public short T { get; set; }
		public decimal U { get; set; }
		public string V { get; set; }
		public byte W { get; set; }
		public Guid X { get; set; }
		public string Y { get; set; }
		public string Z { get; set; }
		public DateTimeOffset A1 { get; set; }
		public string B1 { get; set; }
		public byte[] C1 { get; set; }
		public DateTime D1 { get; set; }
		[DataColumn(TimeStamp = true)]
		public byte[] E1 { get; set; }
		public TimeSpan F1 { get; set; }
		[DataColumn(PrimaryKey = true)]
		public Guid Guid { get; set; }
	}
	[Serializable]
	public partial class TestAncillaryUpdate : BaseEntity
	{
		[DataColumn(PrimaryKey = true)]
		public Guid GuidId { get; set; }
		[DataColumn(IsNullable = true)]
		public string StrValue { get; set; }
		[DataColumn(IsNullable = true)]
		public decimal? DecValue { get; set; }
		[DataColumn(IsNullable = true)]
		public int? IntId { get; set; }
		[DataColumn(TimeStamp = true)]
		public long TimeStampValue { get; set; }
	}
	public class TestNotNullDataTypeCode
	{
		public static void Run()
		{
			string connectionString = (from s in System.IO.File.ReadAllLines(@"c:\TestCodeDom.connectionString.txt")
									   where s.StartsWith(";") == false
									   select s
									).First();

			Console.WriteLine("Current ConnectionStirng: " + connectionString);
			Mysoft.Map.Extensions.Initializer.UnSafeInit(connectionString);
			try {
				TestCodeGenerator();
				//RunTestCase();
			}
			catch( Exception ex ) {
				Console.WriteLine(ex.ToString());
			}
			Console.ReadLine();
		}

		//private static void RunTestCase()
		//{
		//    var QueryNewRow = String.Format("select top 1 * from TestNotNullDataTypeTable").AsCPQuery();
		//    TestNotNullDataTypeTable t2 = QueryNewRow.ToSingle<TestNotNullDataTypeTable>();
		//    TestNotNullDataTypeTable t1 = QueryNewRow.ToSingle<TestNotNullDataTypeTable>();
		//    t1.A = 28;
		//    t1.D = "bbbbbbbbb";
		//    TestNotNullDataTypeTemplate.ConcurrencyUpdate_OriginalValue(t1, t2);
		//}

		private static void TestCodeGenerator()
		{
			CodeGenerator generator = new CodeGenerator(typeof(TestAncillaryUpdate));
			string classname = generator.GetCode("TestAncillaryUpdate");
			classname = CodeGenerator.GetCodeHeader() + classname + "}";
			System.IO.File.WriteAllText("..\\TestAncillaryUpdateTemplate.cs", classname, Encoding.UTF8);
		}

	}
}
