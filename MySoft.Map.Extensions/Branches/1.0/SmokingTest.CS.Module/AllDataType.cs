using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mysoft.Map.Extensions.DAL;


namespace SmokingTest.CS.Entity
{
	/// <summary>
	/// 实体类,使用Nullable数据
	/// </summary>
	public class TestDataType : BaseEntity
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

	/// <summary>
	/// 实体类,不使用Nullable数据
	/// </summary>
	public class TestDataTypeNoNull : BaseEntity
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

	/// <summary>
	/// 测试GUID与字符串的互转
	/// </summary>
	public class TestDataGuidToString : BaseEntity
	{
		public string x { get; set; }
	}


	public partial class Products : BaseEntity
	{
		[DataColumn(PrimaryKey = true, Identity = true)]
		public int ProductID { get; set; }
		public string ProductName { get; set; }
		public int CategoryID { get; set; }
		public string Unit { get; set; }
		public decimal UnitPrice { get; set; }
		public string Remark { get; set; }
		public int Quantity { get; set; }
	}

	/// <summary>
	/// 标记了列别名的实体类
	/// </summary>
	public class AttrEntity : BaseEntity
	{
		[DataColumn(Alias = "AliasColumn")]
		public string AliasColumn { get; set; }

		[DataColumn(Alias = "RawGuid")]
		public Guid RawGuid { get; set; }

		[DataColumn(Alias = "StringGuid")]
		public string StringGuid { get; set; }
	}
}
