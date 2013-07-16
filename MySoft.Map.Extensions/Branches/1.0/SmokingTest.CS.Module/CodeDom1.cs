using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mysoft.Map.Extensions.DAL;

namespace SmokingTest.CS.Entity
{
	public class TestCodeDomCS : BaseEntity
	{
		public long? a { get; set; }
		public byte[] b { get; set; }
		public bool? c { get; set; }
		public string d { get; set; }
		public DateTime? e { get; set; }
		public DateTime? f { get; set; }
		public decimal? g { get; set; }
		public double? h { get; set; }
		public byte[] i { get; set; }
		public int? j { get; set; }
		public decimal? k { get; set; }
		public string l { get; set; }
		public string m { get; set; }
		public decimal? n { get; set; }
		public string o { get; set; }
		public string p { get; set; }
		public float? q { get; set; }
		public DateTime? r { get; set; }
		public short? s { get; set; }
		public short? t { get; set; }
		public decimal? u { get; set; }
		public string v { get; set; }
		public byte? w { get; set; }
		public Guid? x { get; set; }
		public string y { get; set; }
		public string z { get; set; }
		public DateTimeOffset? a1 { get; set; }
		public string b1 { get; set; }
		public byte[] c1 { get; set; }
		public DateTime? d1 { get; set; }
		public byte[] e1 { get; set; }
		public TimeSpan? f1 { get; set; }
	}


	public class TestCodeDomCSNotNull : BaseEntity
	{
		public long a { get; set; }
		public byte[] b { get; set; }
		public bool c { get; set; }
		public string d { get; set; }
		public DateTime e { get; set; }
		public DateTime f { get; set; }
		public decimal g { get; set; }
		public double h { get; set; }
		public byte[] i { get; set; }
		public int j { get; set; }
		public decimal k { get; set; }
		public string l { get; set; }
		public string m { get; set; }
		public decimal n { get; set; }
		public string o { get; set; }
		public string p { get; set; }
		public float q { get; set; }
		public DateTime r { get; set; }
		public short s { get; set; }
		public short t { get; set; }
		public decimal u { get; set; }
		public string v { get; set; }
		public byte w { get; set; }
		public Guid x { get; set; }
		public string y { get; set; }
		public string z { get; set; }
		public DateTimeOffset a1 { get; set; }
		public string b1 { get; set; }
		public byte[] c1 { get; set; }
		public DateTime d1 { get; set; }
		public byte[] e1 { get; set; }
		public TimeSpan f1 { get; set; }
	}

	public class TestCodeDomCSToString : BaseEntity
	{
		public string x { get; set; }
	}

	[Serializable]
	[DataEntity(Alias = "Test_CUD1")]
	public partial class TestCUD1 : BaseEntity
	{
		[DataColumn(PrimaryKey = true, Identity = true)]
		public int PK { get; set; }
		[DataColumn(Alias = "seqGuid_Val", SeqGuid = true, IsNullable = true)]
		public Guid? SeqGuidVal { get; set; }
		[DataColumn(Alias = "guid_Val", IsNullable = true)]
		public Guid? GuidVal { get; set; }
		[DataColumn(Alias = "int_Val", IsNullable = true)]
		public int? IntVal { get; set; }
		[DataColumn(Alias = "str_Val", IsNullable = true)]
		public string StrVal { get; set; }
		[DataColumn(Alias = "dtm_Val", IsNullable = true)]
		public DateTime? DtmVal { get; set; }
		[DataColumn(Alias = "money_Val", IsNullable = true)]
		public decimal? MoneyVal { get; set; }
		[DataColumn(Alias = "float_Val", IsNullable = true)]
		public double? FloatVal { get; set; }
		[DataColumn(Alias = "ts_val", TimeStamp = true)]
		public byte[] TsVal { get; set; }
	}

	/// <summary>
	/// 测试xml字符串转换为实体的类型
	/// </summary>
	[Serializable]
	[DataEntity(Alias = "TestXmlToEntity")]
	public partial class TestXmlToEntityPO : BaseEntity
	{
		[DataColumn(PrimaryKey = true)]
		public Guid EntityGUID { get; set; }
		[DataColumn(IsNullable = true, Alias="Long_val")]
		public long? LongVal { get; set; }
		[DataColumn(IsNullable = true)]
		public byte[] BinVal { get; set; }
		[DataColumn(IsNullable = true)]
		public bool? BitVal { get; set; }
		[DataColumn(IsNullable = true)]
		public string CharVal { get; set; }
		[DataColumn(IsNullable = true)]
		public DateTime? DateVal { get; set; }
		[DataColumn(IsNullable = true)]
		public DateTime? DtmVal { get; set; }
		[DataColumn(IsNullable = true)]
		public decimal? DecVal { get; set; }
		[DataColumn(IsNullable = true)]
		public double? FloatVal { get; set; }
		[DataColumn(IsNullable = true)]
		public byte[] ImgVal { get; set; }
		[DataColumn(IsNullable = true)]
		public int? IntVal { get; set; }
		[DataColumn(IsNullable = true)]
		public decimal? MoneyVal { get; set; }
		[DataColumn(IsNullable = true)]
		public string NcharVal { get; set; }
		[DataColumn(IsNullable = true)]
		public string NtextVal { get; set; }
		[DataColumn(IsNullable = true)]
		public decimal? NumVal { get; set; }
		[DataColumn(IsNullable = true)]
		public string NvarcharVal { get; set; }
		[DataColumn(IsNullable = true)]
		public float? RealVal { get; set; }
		[DataColumn(IsNullable = true)]
		public DateTime? SmallDtmVal { get; set; }
		[DataColumn(IsNullable = true)]
		public short? SmallIntVal { get; set; }
		[DataColumn(IsNullable = true)]
		public decimal? SmallMoneyVal { get; set; }
		[DataColumn(IsNullable = true)]
		public string TextVal { get; set; }
		[DataColumn(IsNullable = true)]
		public byte? TintVal { get; set; }
		[DataColumn(IsNullable = true)]
		public Guid? GuidVal { get; set; }
		[DataColumn(IsNullable = true)]
		public string VcharVal { get; set; }
		[DataColumn(IsNullable = true)]
		public DateTimeOffset DtmOffsetVal { get; set; }
		[DataColumn(IsNullable = true)]
		public string XmlVal { get; set; }
		[DataColumn(IsNullable = true)]
		public byte[] VarbinVal { get; set; }
		[DataColumn(IsNullable = true)]
		public DateTime Dtm2Val { get; set; }
		[DataColumn(IsNullable = true)]
		public TimeSpan TimeVal { get; set; }
		[DataColumn(TimeStamp = true)]
		public byte[] TsVal { get; set; }
	}
	/// <summary>
	/// 测试xml字符串转换为实体的类型 时间戳为long
	/// </summary>
	[Serializable]
	[DataEntity(Alias = "TestXmlToEntity")]
	public partial class TestXmlToEntityPOLong : BaseEntity
	{
		[DataColumn(PrimaryKey = true)]
		public Guid EntityGUID { get; set; }
		[DataColumn(IsNullable = true, Alias = "Long_val")]
		public long? LongVal { get; set; }
		[DataColumn(IsNullable = true)]
		public byte[] BinVal { get; set; }
		[DataColumn(IsNullable = true)]
		public bool? BitVal { get; set; }
		[DataColumn(IsNullable = true)]
		public string CharVal { get; set; }
		[DataColumn(IsNullable = true)]
		public DateTime? DateVal { get; set; }
		[DataColumn(IsNullable = true)]
		public DateTime? DtmVal { get; set; }
		[DataColumn(IsNullable = true)]
		public decimal? DecVal { get; set; }
		[DataColumn(IsNullable = true)]
		public double? FloatVal { get; set; }
		[DataColumn(IsNullable = true)]
		public byte[] ImgVal { get; set; }
		[DataColumn(IsNullable = true)]
		public int? IntVal { get; set; }
		[DataColumn(IsNullable = true)]
		public decimal? MoneyVal { get; set; }
		[DataColumn(IsNullable = true)]
		public string NcharVal { get; set; }
		[DataColumn(IsNullable = true)]
		public string NtextVal { get; set; }
		[DataColumn(IsNullable = true)]
		public decimal? NumVal { get; set; }
		[DataColumn(IsNullable = true)]
		public string NvarcharVal { get; set; }
		[DataColumn(IsNullable = true)]
		public float? RealVal { get; set; }
		[DataColumn(IsNullable = true)]
		public DateTime? SmallDtmVal { get; set; }
		[DataColumn(IsNullable = true)]
		public short? SmallIntVal { get; set; }
		[DataColumn(IsNullable = true)]
		public decimal? SmallMoneyVal { get; set; }
		[DataColumn(IsNullable = true)]
		public string TextVal { get; set; }
		[DataColumn(IsNullable = true)]
		public byte? TintVal { get; set; }
		[DataColumn(IsNullable = true)]
		public Guid? GuidVal { get; set; }
		[DataColumn(IsNullable = true)]
		public string VcharVal { get; set; }
		[DataColumn(IsNullable = true)]
		public DateTimeOffset DtmOffsetVal { get; set; }
		[DataColumn(IsNullable = true)]
		public string XmlVal { get; set; }
		[DataColumn(IsNullable = true)]
		public byte[] VarbinVal { get; set; }
		[DataColumn(IsNullable = true)]
		public DateTime Dtm2Val { get; set; }
		[DataColumn(IsNullable = true)]
		public TimeSpan TimeVal { get; set; }
		[DataColumn(TimeStamp = true)]
		public long TsVal { get; set; }
	}


	[Serializable]
	[DataEntity(Alias = "s_DyContract")]
	public partial class SDyContract : BaseEntity
	{
		[DataColumn(PrimaryKey = true)]
		public Guid ContractGUID { get; set; }
		[DataColumn(IsNullable = true)]
		public string DyContractNo { get; set; }
		[DataColumn(IsNullable = true)]
		public DateTime? DyDate { get; set; }
		[DataColumn(IsNullable = true)]
		public string JkContractNo { get; set; }
		[DataColumn(IsNullable = true)]
		public string JkBank { get; set; }
		[DataColumn(IsNullable = true)]
		public decimal? JkAmount { get; set; }
		[DataColumn(IsNullable = true)]
		public string Pgcompany { get; set; }
		[DataColumn(IsNullable = true)]
		public decimal? PgAmount { get; set; }
		[DataColumn(IsNullable = true)]
		public DateTime? FkDate { get; set; }
		[DataColumn(IsNullable = true)]
		public string Remarks { get; set; }
		[DataColumn(TimeStamp = true)]
		public long ContractVersion { get; set; }
		[DataColumn(IsNullable = true)]
		public string ApproveState { get; set; }
	}
}
