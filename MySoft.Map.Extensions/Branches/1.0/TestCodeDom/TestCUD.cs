using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mysoft.Map.Extensions.DAL;
using Mysoft.Map.Extensions.CodeDom;
using Mysoft.Map.Extensions.Xml;
using System.Data.SqlClient;

namespace TestCodeDom
{
	[Serializable]
	[DataEntity(Alias = "Table_1")]
	public partial class Table1 : BaseEntity
	{
		//[DataColumn(Alias = "Row_Id", PrimaryKey = true, Identity = true)]
		[DataColumn(Alias = "Row_Id",  Identity = true)]
		public int RowId { get; set; }
		[DataColumn(PrimaryKey = true)]
		public Guid Rowguid { get; set; }
		public string StrValue { get; set; }
		[DataColumn(IsNullable = true)]
		public string StrValue2 { get; set; }
		public int IntValue { get; set; }
		[DataColumn(IsNullable = true)]
		public int? IntValue2 { get; set; }
		public decimal Money { get; set; }
		[DataColumn(IsNullable = true)]
		public decimal? Money2 { get; set; }
		[DataColumn(Alias = "Time_Stamp", TimeStamp = true, IsNullable = true)]
		public byte[] TimeStamp { get; set; }
		[DataColumn(SeqGuid = true)]
		public Guid SeqGuid { get; set; }
		[DataColumn(SeqGuid = true, IsNullable = true)]
		public Guid? SeqGuid2 { get; set; }
		public string StrValue3 { get; set; }
	}


	class TestCUD
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
		}


		static void TestCodeGenerator()
		{
			CodeGenerator generator = new CodeGenerator(typeof(Table1));
			string code = generator.GetCode("Table1CUD");

			code = CodeGenerator.GetCodeHeader() + code + "}";

			System.IO.File.WriteAllText("..\\Table1CUDCode.cs", code, Encoding.UTF8);
			//Console.WriteLine(code);
		}

		static void RunTestCase()
		{
			TestCUD tester = new TestCUD();
			tester.Insert();
			tester.Update();

			tester.ConcurrencyUpdate_TimeStamp();
			tester.ConcurrencyUpdate_OriginalValue();

			tester.Delete();

			tester.Insert();
			tester.ConcurrencyDelete_TimeStamp();

			tester.Insert();
			tester.Update();
			tester.ConcurrencyDelete_OriginalValue();

		}




		private Guid _rowGuid ;
		private CPQuery _queryNewRow ;

		public TestCUD()
		{
			_rowGuid = Guid.NewGuid();
			_queryNewRow = CPQuery.Format("select * from Table_1 where RowGuid = {0}", _rowGuid);
		}

		private void Insert()
		{
			Console.WriteLine("\r\n----------Insert---------------------------");
			Table1 t1 = new Table1 {
				IntValue = 200,
				StrValue = "aaaaaaaaa",
				Money = 123.45M,
				Rowguid = _rowGuid
			};
			ShowAndExecuteCPQuery(_Tool.AutoGenerateCode.Table1CUD.Insert(t1));
		}

		private void Update()
		{
			Console.WriteLine("\r\n----------Update---------------------------");
			Table1 t1 = new Table1();
			t1.Rowguid = _rowGuid;
			t1.RowId = 11;	// 这是个自增列，内容不会被修改
			t1.StrValue2 = "Hello...." + DateTime.Now.Ticks.ToString();
			ShowAndExecuteCPQuery(_Tool.AutoGenerateCode.Table1CUD.Update(t1, null));
		}

		private void ConcurrencyUpdate_TimeStamp()
		{
			Console.WriteLine("\r\n----------ConcurrencyUpdate_TimeStamp---------------------------");
			Table1 t2 = _queryNewRow.ToSingle<Table1>();
			Table1 t1 = _queryNewRow.ToSingle<Table1>();
			t1.IntValue = 3;
			t1.StrValue = "Fish Li";
			ShowAndExecuteCPQuery(_Tool.AutoGenerateCode.Table1CUD.ConcurrencyUpdate_TimeStamp(t1, t2, null));
		}

		private void ConcurrencyUpdate_OriginalValue()
		{
			Console.WriteLine("\r\n----------ConcurrencyUpdate_OriginalValue---------------------------");
			Table1 t2 = _queryNewRow.ToSingle<Table1>();
			Table1 t1 = _queryNewRow.ToSingle<Table1>();
			t1.IntValue2 = 5;
			t1.StrValue2 = "bbbbbbbbb";
			ShowAndExecuteCPQuery(_Tool.AutoGenerateCode.Table1CUD.ConcurrencyUpdate_OriginalValue(t1, t2, null));
		}

		private void ConcurrencyDelete_TimeStamp()
		{
			Console.WriteLine("\r\n----------ConcurrencyDelete_TimeStamp---------------------------");
			Table1 t1 = _queryNewRow.ToSingle<Table1>();
			Table1 t2 = new Table1 {
				IntValue = t1.IntValue,
				StrValue2 = t1.StrValue2,
				TimeStamp = t1.TimeStamp,
				Rowguid = t1.Rowguid
			};
			ShowAndExecuteCPQuery(_Tool.AutoGenerateCode.Table1CUD.ConcurrencyDelete_TimeStamp(t2));
		}

		private void ConcurrencyDelete_OriginalValue()
		{
			Console.WriteLine("\r\n----------ConcurrencyDelete_OriginalValue---------------------------");
			Table1 t1 = _queryNewRow.ToSingle<Table1>();
			Table1 t2 = new Table1 {
				IntValue = t1.IntValue,
				StrValue = t1.StrValue,
				StrValue2 = t1.StrValue2,
				Rowguid = t1.Rowguid
			};
			ShowAndExecuteCPQuery(_Tool.AutoGenerateCode.Table1CUD.ConcurrencyDelete_OriginalValue(t2));
		}

		private void Delete()
		{
			Console.WriteLine("\r\n----------Delete---------------------------");
			Table1 t1 = new Table1();
			t1.Rowguid = _rowGuid;
			ShowAndExecuteCPQuery(_Tool.AutoGenerateCode.Table1CUD.Delete(t1));
		}



		static void ShowAndExecuteCPQuery(CPQuery query)
		{
			SqlCommand command = query.GetCommand();

			Console.WriteLine("CommandText: " + command.CommandText);
			foreach( SqlParameter parameter in command.Parameters )
				Console.WriteLine("{0}: {1}", parameter.ParameterName, GetParameterValue(parameter.Value));

			int result = query.ExecuteNonQuery();
			Console.WriteLine("ExecuteNonQuery Result: " + result);
			if( result != 1 )
				Console.WriteLine("#################### ExecuteNonQuery 结果不为零，请检查。#############################");
		}

		static string GetParameterValue(object val)
		{
			if( DBNull.Value.Equals(val) )
				return "DBNull";

			if( val == null )
				return "null";

			if( val.GetType() == typeof(byte[]) )
				return "0x" + BitConverter.ToString((byte[])val).Replace("-", "");
			else
				return val.ToString();
			
		}
	}
}


/* 控制台输出：

Current ConnectionStirng: server=localhost\sqlexpress;database=MyNorthwind;Integrated Security=SSPI

----------Insert---------------------------
CommandText: insert into [Table_1] ([Rowguid],[StrValue],[IntValue],[Money]) values (@Rowguid,@StrValue,@IntValue,@Money)
@Rowguid: 7a4ab4ca-6efb-4379-b154-6e5041a86b43
@StrValue: aaaaaaaaa
@IntValue: 200
@Money: 123.45
ExecuteNonQuery Result: 1

----------Update---------------------------
CommandText: update [Table_1] set  [StrValue2] = @StrValue2 where  [Rowguid] = @Rowguid
@StrValue2: Hello....635050139094654409
@Rowguid: 7a4ab4ca-6efb-4379-b154-6e5041a86b43
ExecuteNonQuery Result: 1

----------ConcurrencyUpdate_TimeStamp---------------------------
CommandText: update [Table_1] set  [StrValue] = @StrValue ,  [IntValue] = @IntValue 
where  [Rowguid] = @Rowguid and  [Time_Stamp] = @original_TimeStamp
@StrValue: Fish Li
@IntValue: 3
@Rowguid: 7a4ab4ca-6efb-4379-b154-6e5041a86b43
@original_TimeStamp: 0x0000000000002FEB
ExecuteNonQuery Result: 1

----------ConcurrencyUpdate_OriginalValue---------------------------
CommandText: update [Table_1] set  [StrValue2] = @StrValue2 ,  [IntValue2] = @IntValue2 
where  [Rowguid] = @original_Rowguid 
and  ([StrValue2] = @original_StrValue2 or @original_StrValue2 is null and[StrValue2] is null) 
and  ([IntValue2] = @original_IntValue2 or @original_IntValue2 is null and [IntValue2] is null)
@StrValue2: bbbbbbbbb
@IntValue2: 5
@original_Rowguid: 7a4ab4ca-6efb-4379-b154-6e5041a86b43
@original_StrValue2: Hello....635050139094654409
@original_IntValue2: 456
ExecuteNonQuery Result: 1

----------Delete---------------------------
CommandText: delete from [Table_1]  where  [Rowguid] = @Rowguid
@Rowguid: 7a4ab4ca-6efb-4379-b154-6e5041a86b43
ExecuteNonQuery Result: 1

----------Insert---------------------------
CommandText: insert into [Table_1] ([Rowguid],[StrValue],[IntValue],[Money]) values (@Rowguid,@StrValue,@IntValue,@Money)
@Rowguid: 7a4ab4ca-6efb-4379-b154-6e5041a86b43
@StrValue: aaaaaaaaa
@IntValue: 200
@Money: 123.45
ExecuteNonQuery Result: 1

----------ConcurrencyDelete_TimeStamp---------------------------
CommandText: delete from [Table_1]  where  [Rowguid] = @Rowguid and  [Time_Stamp] = @original_TimeStamp
@Rowguid: 7a4ab4ca-6efb-4379-b154-6e5041a86b43
@original_TimeStamp: 0x0000000000002FEE
ExecuteNonQuery Result: 1

----------Insert---------------------------
CommandText: insert into [Table_1] ([Rowguid],[StrValue],[IntValue],[Money]) values (@Rowguid,@StrValue,@IntValue,@Money)
@Rowguid: 7a4ab4ca-6efb-4379-b154-6e5041a86b43
@StrValue: aaaaaaaaa
@IntValue: 200
@Money: 123.45
ExecuteNonQuery Result: 1

----------Update---------------------------
CommandText: update [Table_1] set  [StrValue2] = @StrValue2 where  [Rowguid] = @Rowguid
@StrValue2: Hello....635050139095278419
@Rowguid: 7a4ab4ca-6efb-4379-b154-6e5041a86b43
ExecuteNonQuery Result: 1

----------ConcurrencyDelete_OriginalValue---------------------------
CommandText: delete from [Table_1]  
where  [Rowguid] = @original_Rowguid 
and  ([StrValue] = @original_StrValue) 
and  ([StrValue2] = @original_StrValue2 or @original_StrValue2 is null and [StrValue2] is null) 
and  ([IntValue] = @original_IntValue)
@original_Rowguid: 7a4ab4ca-6efb-4379-b154-6e5041a86b43
@original_StrValue: aaaaaaaaa
@original_StrValue2: Hello....635050139095278419
@original_IntValue: 200
ExecuteNonQuery Result: 1

*/