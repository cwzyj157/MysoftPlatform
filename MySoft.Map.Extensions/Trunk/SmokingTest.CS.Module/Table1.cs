using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mysoft.Map.Extensions.DAL;

namespace SmokingTest.CS.Entity
{
	[Serializable]
	[DataEntity(Alias = "Table_1")]
	public partial class Table1 : BaseEntity
	{
		//[DataColumn(Alias = "Row_Id", PrimaryKey = true, Identity = true)]
		[DataColumn(Alias = "Row_Id", Identity = true)]
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
    [Serializable]
    public partial class TestSqlTrace : BaseEntity
    {
		[DataColumn(IsNullable = true)]
		public string TextData { get; set; }
		[DataColumn(IsNullable = true)]
		public byte[] BinaryData { get; set; }
		[DataColumn(IsNullable = true)]
		public long? TransactionID { get; set; }
		[DataColumn(IsNullable = true)]
		public string NTUserName { get; set; }
		[DataColumn(IsNullable = true)]
		public DateTime? StartTime { get; set; }
		[DataColumn(IsNullable = true)]
		public string SqlText { get; set; }
		[DataColumn(PrimaryKey = true, Identity = true)]
		public int Id { get; set; }
		public int DatabaseID { get; set; }
		
    }
	
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
	[DataEntity(Alias = "TestNotNullDataTypeTable")]
	[Serializable]
	public partial class TestNotNullDataTypeTable1 
	{
		public ulong A { get; set; }
		public byte[] B { get; set; }
		public bool C { get; set; }
		public string D { get; set; }
		public DateTime E { get; set; }
		public DateTime F { get; set; }
		public decimal G { get; set; }
		public double H { get; set; }
		public byte[] I { get; set; }
		public uint J { get; set; }
		public decimal K { get; set; }
		public string L { get; set; }
		public string M { get; set; }
		public decimal N { get; set; }
		public string O { get; set; }
		public string P { get; set; }
		public float Q { get; set; }
		public DateTime R { get; set; }
		public ushort S { get; set; }
		public short T { get; set; }
		public decimal U { get; set; }
		public char V { get; set; }
		public sbyte W { get; set; }
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
	public partial class TestConcurrencyUpdate : BaseEntity
	{
		[DataColumn(PrimaryKey = true)]
		public Guid GuidRow { get; set; }
		[DataColumn(IsNullable = true)]
		public string StrValue { get; set; }
		[DataColumn(TimeStamp = true, IsNullable = true)]
		public byte[] TimeStampValue { get; set; }
		[DataColumn(IsNullable = true)]
		public byte[] BinaryValue { get; set; }
	}

	[Serializable]
	public partial class TestInsert : BaseEntity
	{
		[DataColumn(Identity = true)]
		public int Ids { get; set; }
		[DataColumn(PrimaryKey = true)]
		public int PkId { get; set; }
		[DataColumn(TimeStamp = true, IsNullable = true)]
		public byte[] TimeStampValue { get; set; }
		[DataColumn(SeqGuid = true, IsNullable = true)]
		public Guid? GuidId { get; set; }
		[DataColumn(IsNullable = true)]
		public string StrValue { get; set; }
		[DataColumn(IsNullable = true)]
		public byte[] ByteValue { get; set; }
		
	}
	[Serializable]
	public partial class TestConcurrencytDelete : BaseEntity
	{
		[DataColumn(PrimaryKey = true)]
		public Guid GuidRow { get; set; }
		[DataColumn(IsNullable = true)]
		public string StrValue { get; set; }
		[DataColumn(TimeStamp = true, IsNullable = true)]
		public long TimeStampValue { get; set; }
		[DataColumn(IsNullable = true)]
		public byte[] BinaryValue { get; set; }
	}
	[Serializable]
	public partial class TestUpdate : BaseEntity
	{
		[DataColumn(Identity = true)]
		public int Ids { get; set; }
		[DataColumn(PrimaryKey = true)]
		public int PkId { get; set; }
		[DataColumn(TimeStamp = true, IsNullable = true)]
		public byte[] TimeStampValue { get; set; }
		[DataColumn(SeqGuid = true, IsNullable = true)]
		public Guid? GuidId { get; set; }
		[DataColumn(IsNullable = true)]
		public string StrValue { get; set; }
		[DataColumn(IsNullable = true)]
		public byte[] ByteValue { get; set; }
		
	}
	[Serializable]
	public partial class TestConcurrencyException : BaseEntity
	{
		[DataColumn(PrimaryKey = true)]
		public Guid GuiId { get; set; }
		[DataColumn(TimeStamp = true, IsNullable = true)]
		public byte[] TstValue { get; set; }
		[DataColumn(IsNullable = true)]
		public string StrValue { get; set; }
		[DataColumn(IsNullable = true)]
		public int? IntValue { get; set; }
	}

	[Serializable]
	public partial class TestPK : BaseEntity
	{
		[DataColumn(PrimaryKey = true)]
		public int Id { get; set; }
		[DataColumn(PrimaryKey = true)]
		public Guid Guiid { get; set; }
		[DataColumn(IsNullable = true)]
		public string StrValue { get; set; }
		[DataColumn(TimeStamp = true, IsNullable = true)]
		public byte[] TstValue { get; set; }
	}
	[Serializable]
	public partial class TestDelete : BaseEntity
	{
		[DataColumn(PrimaryKey = true)]
		public int Id { get; set; }
		[DataColumn(PrimaryKey = true)]
		public Guid Guiid { get; set; }
		[DataColumn(IsNullable = true)]
		public string StrValue { get; set; }
		[DataColumn(TimeStamp = true, IsNullable = true)]
		public byte[] TstValue { get; set; }
	}
	[Serializable]
	public partial class TestCUDValueTypeZero : BaseEntity
	{
		[DataColumn(PrimaryKey = true)]
		public int Id { get; set; }
		[DataColumn(IsNullable = true)]
		public string StrValue { get; set; }
		[DataColumn(IsNullable = true)]
		public byte[] ByteValue { get; set; }
		[DataColumn(IsNullable = true)]
		public int? IntValue { get; set; }
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
	[Serializable]
	public partial class TestAncillaryDelete : BaseEntity
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
		public byte[] TimeStampValue { get; set; }
	}
	[Serializable]
	public partial class TestSqlSplice : BaseEntity
	{
		[DataColumn(PrimaryKey = true)]
		public Guid GuidId { get; set; }
		[DataColumn(IsNullable = true)]
		public int? IntValue { get; set; }
		[DataColumn(IsNullable = true)]
		public string StrValue { get; set; }
		[DataColumn(TimeStamp = true, IsNullable = true)]
		public byte[] TimeStampValue { get; set; }
	}
	[Serializable]
	public partial class TestTimeStamp : BaseEntity
	{
		[DataColumn(PrimaryKey = true)]
		public Guid GuidId { get; set; }
		[DataColumn(TimeStamp = true)]
		public long TsValue { get; set; }
		[DataColumn]
		public string StrValue { get; set; }
		[DataColumn]
		public int IntValue { get; set; }

	}
	[Serializable]
	[DataEntity(Alias="TestTimeStamp")]
	public partial class TestTimeStamp1 : BaseEntity
	{
		[DataColumn(PrimaryKey = true)]
		public Guid GuidId { get; set; }
		[DataColumn(TimeStamp = true)]
		public byte[] TsValue { get; set; }
		[DataColumn]
		public string StrValue { get; set; }
		[DataColumn]
		public int IntValue { get; set; }
	}
	[Serializable]
	[DataEntity(Alias = "TestTimeStamp")]
	public partial class TestTimeStamp2
	{
		[DataColumn(PrimaryKey = true)]
		public Guid GuidId { get; set; }
		[DataColumn(TimeStamp = true)]
		public long TsValue { get; set; }
		[DataColumn]
		public string StrValue { get; set; }
		[DataColumn]
		public int IntValue { get; set; }
	}
	[Serializable]
	[DataEntity(Alias = "TestTimeStamp")]
	public partial class TestTimeStamp3
	{
		[DataColumn(PrimaryKey = true)]
		public Guid GuidId { get; set; }
		[DataColumn(TimeStamp = true)]
		public byte[] TsValue { get; set; }
		[DataColumn]
		public string StrValue { get; set; }
		[DataColumn]
		public int IntValue { get; set; }
	}
	[Serializable]
	public partial class TestSPOut : BaseEntity
	{
		[DataColumn(PrimaryKey = true)]
		public Guid GuiId { get; set; }
		[DataColumn(TimeStamp = true)]
		public long TstValue { get; set; }
		[DataColumn(IsNullable = true)]
		public string StrValue { get; set; }
		[DataColumn(IsNullable = true)]
		public int? IntValue { get; set; }

	}
}
