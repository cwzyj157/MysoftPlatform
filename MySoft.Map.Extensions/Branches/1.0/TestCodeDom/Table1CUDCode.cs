using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Xml;
using System.IO;
using Mysoft.Map.Extensions;
using Mysoft.Map.Extensions.DAL;
namespace _Tool.AutoGenerateCode
{
	public static class Table1CUD
	{
		private static readonly TestCodeDom.Table1 s_Empty = new TestCodeDom.Table1();

		public static object Execute(int flag, object[] parameters)
		{
			switch( flag ) {
				case 1:
					return DataReaderToList((SqlDataReader)parameters[0]);
				case 2:
					return DataReaderToSingle((SqlDataReader)parameters[0]);
				case 3:
					return Insert((TestCodeDom.Table1)parameters[0]);
				case 4:
					return Delete((TestCodeDom.Table1)parameters[0]);
				case 5:
					return ConcurrencyDelete_TimeStamp((TestCodeDom.Table1)parameters[0]);
				case 6:
					return ConcurrencyDelete_OriginalValue((TestCodeDom.Table1)parameters[0]);
				case 7:
					return Update((TestCodeDom.Table1)parameters[0], (TestCodeDom.Table1)parameters[1]);
				case 8:
					return ConcurrencyUpdate_TimeStamp((TestCodeDom.Table1)parameters[0], (TestCodeDom.Table1)parameters[1], (TestCodeDom.Table1)parameters[2]);
				case 9:
					return ConcurrencyUpdate_OriginalValue((TestCodeDom.Table1)parameters[0], (TestCodeDom.Table1)parameters[1], (TestCodeDom.Table1)parameters[2]);
				case 10:
					return DataTableToList((DataTable)parameters[0]);
				case 11:
					return XmlToSingle(parameters[0].ToString());
				case 12:
					return XmlToList(parameters[0].ToString());
				case 13:
					return CloneMe(parameters[0]);
				default:
					throw new NotImplementedException();
			}
		}
		private static CPQuery UpdateSetFields(TestCodeDom.Table1 t1, TestCodeDom.Table1 t2)
		{
			bool changed = false;
			string[] zeroProperties = t1.GetZeroProperties();
			CPQuery query = CPQuery.Create() + "update [Table_1] set ";

			if( t1.StrValue != t2.StrValue ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [StrValue] = " + (new SqlParameter("@StrValue", (t1.StrValue ?? (object)DBNull.Value)));
			}
			if( t1.StrValue2 != t2.StrValue2 ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [StrValue2] = " + (new SqlParameter("@StrValue2", (t1.StrValue2 ?? (object)DBNull.Value)));
			}
			if( t1.IntValue != t2.IntValue || Array.IndexOf(zeroProperties, "IntValue") >= 0 ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [IntValue] = " + (new SqlParameter("@IntValue", t1.IntValue));
			}
			if( t1.IntValue2 != t2.IntValue2 ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [IntValue2] = " + (new SqlParameter("@IntValue2", (t1.IntValue2.HasValue ? t1.IntValue2.Value : (object)DBNull.Value)));
			}
			if( t1.Money != t2.Money || Array.IndexOf(zeroProperties, "Money") >= 0 ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [Money] = " + (new SqlParameter("@Money", t1.Money));
			}
			if( t1.Money2 != t2.Money2 ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [Money2] = " + (new SqlParameter("@Money2", (t1.Money2.HasValue ? t1.Money2.Value : (object)DBNull.Value)));
			}
			if( t1.SeqGuid != t2.SeqGuid || Array.IndexOf(zeroProperties, "SeqGuid") >= 0 ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [SeqGuid] = " + (new SqlParameter("@SeqGuid", t1.SeqGuid));
			}
			if( t1.SeqGuid2 != t2.SeqGuid2 ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [SeqGuid2] = " + (new SqlParameter("@SeqGuid2", (t1.SeqGuid2.HasValue ? t1.SeqGuid2.Value : (object)DBNull.Value)));
			}
			if( t1.StrValue3 != t2.StrValue3 ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [StrValue3] = " + (new SqlParameter("@StrValue3", (t1.StrValue3 ?? (object)DBNull.Value)));
			}
			if( changed == false )
				return null;

			else
				return query;
		}
		private static void WhereByPK(CPQuery query, TestCodeDom.Table1 t1)
		{
			query = query + " where ";
			query = query + " [Rowguid] = " + (new SqlParameter("@Rowguid", t1.Rowguid));
		}
		private static void WhereByTimeStamp(CPQuery query, TestCodeDom.Table1 t2)
		{
			query = query + " where ";
			query = query + " [Rowguid] = " + (new SqlParameter("@Rowguid", t2.Rowguid));
			query = query + " and ";
			query = query + " [Time_Stamp] = " + (new SqlParameter("@original_TimeStamp", (t2.TimeStamp ?? (object)DBNull.Value)));
		}
		private static void WhereByOriginalValue(CPQuery query, TestCodeDom.Table1 t1, TestCodeDom.Table1 t2)
		{
			string[] zeroProperties = t2.GetZeroProperties();
			query = query + " where ";
			query = query + " [Rowguid] = " + (new SqlParameter("@original_Rowguid", t2.Rowguid));
			if( t1.RowId != t2.RowId || Array.IndexOf(zeroProperties, "RowId") >= 0 ) {
				query = query + " and ";
				query = query + " ([Row_Id] = " + (new SqlParameter("@original_RowId", t2.RowId)) + ")";
			}
			if( t1.StrValue != t2.StrValue ) {
				query = query + " and ";
				query = query + " ([StrValue] = " + (new SqlParameter("@original_StrValue", (t2.StrValue ?? (object)DBNull.Value))) + ")";
			}
			if( t1.StrValue2 != t2.StrValue2 ) {
				query = query + " and ";
				query = query + " ([StrValue2] = " + (new SqlParameter("@original_StrValue2", (t2.StrValue2 ?? (object)DBNull.Value))) + " or @original_StrValue2 is null and [StrValue2] is null)";
			}
			if( t1.IntValue != t2.IntValue || Array.IndexOf(zeroProperties, "IntValue") >= 0 ) {
				query = query + " and ";
				query = query + " ([IntValue] = " + (new SqlParameter("@original_IntValue", t2.IntValue)) + ")";
			}
			if( t1.IntValue2 != t2.IntValue2 ) {
				query = query + " and ";
				query = query + " ([IntValue2] = " + (new SqlParameter("@original_IntValue2", (t2.IntValue2.HasValue ? t2.IntValue2.Value : (object)DBNull.Value))) + " or @original_IntValue2 is null and [IntValue2] is null)";
			}
			if( t1.Money != t2.Money || Array.IndexOf(zeroProperties, "Money") >= 0 ) {
				query = query + " and ";
				query = query + " ([Money] = " + (new SqlParameter("@original_Money", t2.Money)) + ")";
			}
			if( t1.Money2 != t2.Money2 ) {
				query = query + " and ";
				query = query + " ([Money2] = " + (new SqlParameter("@original_Money2", (t2.Money2.HasValue ? t2.Money2.Value : (object)DBNull.Value))) + " or @original_Money2 is null and [Money2] is null)";
			}
			if( t1.SeqGuid != t2.SeqGuid || Array.IndexOf(zeroProperties, "SeqGuid") >= 0 ) {
				query = query + " and ";
				query = query + " ([SeqGuid] = " + (new SqlParameter("@original_SeqGuid", t2.SeqGuid)) + ")";
			}
			if( t1.SeqGuid2 != t2.SeqGuid2 ) {
				query = query + " and ";
				query = query + " ([SeqGuid2] = " + (new SqlParameter("@original_SeqGuid2", (t2.SeqGuid2.HasValue ? t2.SeqGuid2.Value : (object)DBNull.Value))) + " or @original_SeqGuid2 is null and [SeqGuid2] is null)";
			}
			if( t1.StrValue3 != t2.StrValue3 ) {
				query = query + " and ";
				query = query + " ([StrValue3] = " + (new SqlParameter("@original_StrValue3", (t2.StrValue3 ?? (object)DBNull.Value))) + ")";
			}
		}
		public static CPQuery Insert(TestCodeDom.Table1 t1)
		{
			TestCodeDom.Table1 t2 = s_Empty;
			bool changed = false;
			string[] zeroProperties = t1.GetZeroProperties();
			List<SqlParameter> parameters = new List<SqlParameter>();
			StringBuilder sqlBuilder = new StringBuilder();
			StringBuilder sqlParams = new StringBuilder();
			sqlBuilder.Append("insert into [Table_1] (");
			if( t1.Rowguid != t2.Rowguid || Array.IndexOf(zeroProperties, "Rowguid") >= 0 ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[Rowguid]");
				sqlParams.Append("@Rowguid");
				parameters.Add(new SqlParameter("@Rowguid", t1.Rowguid));
			}
			if( t1.StrValue != t2.StrValue ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[StrValue]");
				sqlParams.Append("@StrValue");
				parameters.Add(new SqlParameter("@StrValue", (t1.StrValue ?? (object)DBNull.Value)));
			}
			if( t1.StrValue2 != t2.StrValue2 ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[StrValue2]");
				sqlParams.Append("@StrValue2");
				parameters.Add(new SqlParameter("@StrValue2", (t1.StrValue2 ?? (object)DBNull.Value)));
			}
			if( t1.IntValue != t2.IntValue || Array.IndexOf(zeroProperties, "IntValue") >= 0 ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[IntValue]");
				sqlParams.Append("@IntValue");
				parameters.Add(new SqlParameter("@IntValue", t1.IntValue));
			}
			if( t1.IntValue2 != t2.IntValue2 ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[IntValue2]");
				sqlParams.Append("@IntValue2");
				parameters.Add(new SqlParameter("@IntValue2", (t1.IntValue2.HasValue ? t1.IntValue2.Value : (object)DBNull.Value)));
			}
			if( t1.Money != t2.Money || Array.IndexOf(zeroProperties, "Money") >= 0 ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[Money]");
				sqlParams.Append("@Money");
				parameters.Add(new SqlParameter("@Money", t1.Money));
			}
			if( t1.Money2 != t2.Money2 ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[Money2]");
				sqlParams.Append("@Money2");
				parameters.Add(new SqlParameter("@Money2", (t1.Money2.HasValue ? t1.Money2.Value : (object)DBNull.Value)));
			}
			if( t1.StrValue3 != t2.StrValue3 ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[StrValue3]");
				sqlParams.Append("@StrValue3");
				parameters.Add(new SqlParameter("@StrValue3", (t1.StrValue3 ?? (object)DBNull.Value)));
			}
			if( changed == false )
				return null;
			sqlBuilder.Append(") values (").Append(sqlParams.ToString()).Append(")");
			CPQuery query = CPQuery.From(sqlBuilder.ToString(), parameters.ToArray());
			return query;
		}
		public static CPQuery Delete(TestCodeDom.Table1 t1)
		{
			if( t1.Rowguid == s_Empty.Rowguid )
				throw new InvalidOperationException("没有为主键字段赋值：t1.Rowguid");
			CPQuery query = CPQuery.Create() + "delete from [Table_1] ";
			WhereByPK(query, t1);
			return query;
		}
		public static CPQuery ConcurrencyDelete_TimeStamp(TestCodeDom.Table1 t2)
		{
			if( t2.Rowguid == s_Empty.Rowguid )
				throw new InvalidOperationException("没有为主键字段赋值：t2.Rowguid");
			CPQuery query = CPQuery.Create() + "delete from [Table_1] ";
			WhereByTimeStamp(query, t2);
			return query;
		}
		public static CPQuery ConcurrencyDelete_OriginalValue(TestCodeDom.Table1 t2)
		{
			if( t2.Rowguid == s_Empty.Rowguid )
				throw new InvalidOperationException("没有为主键字段赋值：t2.Rowguid");
			TestCodeDom.Table1 t1 = s_Empty;
			CPQuery query = CPQuery.Create() + "delete from [Table_1] ";
			WhereByOriginalValue(query, t1, t2);
			return query;
		}
		public static CPQuery Update(TestCodeDom.Table1 t1, TestCodeDom.Table1 t3)
		{
			if( t1.Rowguid == s_Empty.Rowguid )
				throw new InvalidOperationException("没有为主键字段赋值：t1.Rowguid");
			TestCodeDom.Table1 t2 = t3 ?? s_Empty;
			CPQuery query = UpdateSetFields(t1, t2);
			if( query == null )
				return null;
			WhereByPK(query, t1);
			return query;
		}
		public static CPQuery ConcurrencyUpdate_TimeStamp(TestCodeDom.Table1 t1, TestCodeDom.Table1 t2, TestCodeDom.Table1 t3)
		{
			if( t2.Rowguid == s_Empty.Rowguid )
				throw new InvalidOperationException("没有为主键字段赋值：t2.Rowguid");
			CPQuery query = UpdateSetFields(t1, t3 ?? s_Empty);
			if( query == null )
				return null;
			WhereByTimeStamp(query, t2);
			return query;
		}
		public static CPQuery ConcurrencyUpdate_OriginalValue(TestCodeDom.Table1 t1, TestCodeDom.Table1 t2, TestCodeDom.Table1 t3)
		{
			if( t2.Rowguid == s_Empty.Rowguid )
				throw new InvalidOperationException("没有为主键字段赋值：t2.Rowguid");
			CPQuery query = UpdateSetFields(t1, t3 ?? s_Empty);
			if( query == null )
				return null;
			WhereByOriginalValue(query, s_Empty, t2);
			return query;
		}
		private static void SetEntityProperty(TestCodeDom.Table1 obj, string columnName, object value)
		{
			if( value == DBNull.Value || value == null ) {
				return;
			}
			switch( columnName ) {
				case "row_id":
					obj.RowId = (int)value;
					break;
				case "rowguid":
					obj.Rowguid = (Guid)value;
					break;
				case "strvalue":
					obj.StrValue = value.ToString();
					break;
				case "strvalue2":
					obj.StrValue2 = value.ToString();
					break;
				case "intvalue":
					obj.IntValue = (int)value;
					break;
				case "intvalue2":
					obj.IntValue2 = (int)value;
					break;
				case "money":
					obj.Money = (decimal)value;
					break;
				case "money2":
					obj.Money2 = (decimal)value;
					break;
				case "time_stamp":
					obj.TimeStamp = value.GetType() == typeof(long) ? CodeUtil.LongToByte((long)value) : (byte[])value;
					break;
				case "seqguid":
					obj.SeqGuid = (Guid)value;
					break;
				case "seqguid2":
					obj.SeqGuid2 = (Guid)value;
					break;
				case "strvalue3":
					obj.StrValue3 = value.ToString();
					break;
			}
		}
		public static object DataReaderToList(SqlDataReader dr)
		{
			List<TestCodeDom.Table1> list = new List<TestCodeDom.Table1>();
			string[] columnNames = CodeUtil.GetColumnNames(dr);
			while( dr.Read() ) {
				TestCodeDom.Table1 obj = new TestCodeDom.Table1();
				for( int i = 0; i < columnNames.Length; i++ ) {
					SetEntityProperty(obj, columnNames[i], dr.GetValue(i));
				}
				list.Add(obj);
			}
			return list;
		}
		public static object DataReaderToSingle(SqlDataReader dr)
		{
			TestCodeDom.Table1 obj = null;
			string[] columnNames = CodeUtil.GetColumnNames(dr);
			if( dr.Read() ) {
				obj = new TestCodeDom.Table1();
				for( int i = 0; i < columnNames.Length; i++ ) {
					SetEntityProperty(obj, columnNames[i], dr.GetValue(i));
				}
			}
			return obj;
		}
		public static object DataTableToList(DataTable table)
		{
			List<TestCodeDom.Table1> list = new List<TestCodeDom.Table1>(table.Rows.Count);
			string[] columnNames = CodeUtil.GetColumnNames(table);
			foreach( DataRow row in table.Rows ) {
				TestCodeDom.Table1 obj = new TestCodeDom.Table1();
				for( int i = 0; i < columnNames.Length; i++ ) {
					string columnName = columnNames[i];
					SetEntityProperty(obj, columnName, row[columnName]);
				}
				list.Add(obj);
			}
			return list;
		}
		private static void XmlToProperty(TestCodeDom.Table1 obj, string columnName, string value)
		{
			if( string.IsNullOrEmpty(value) ) { return; }
			switch( columnName ) {
				case "row_id": {
						int tmp;
						if( int.TryParse(value, out tmp) ) { obj.RowId = tmp; }
					}
					break;
				case "rowguid": {
						obj.Rowguid = new Guid(value);
					}
					break;
				case "strvalue": {
						obj.StrValue = value;
					}
					break;
				case "strvalue2": {
						obj.StrValue2 = value;
					}
					break;
				case "intvalue": {
						int tmp;
						if( int.TryParse(value, out tmp) ) { obj.IntValue = tmp; }
					}
					break;
				case "intvalue2": {
						int tmp;
						if( int.TryParse(value, out tmp) ) { obj.IntValue2 = tmp; }
					}
					break;
				case "money": {
						decimal tmp;
						if( decimal.TryParse(value, out tmp) ) { obj.Money = tmp; }
					}
					break;
				case "money2": {
						decimal tmp;
						if( decimal.TryParse(value, out tmp) ) { obj.Money2 = tmp; }
					}
					break;
				case "time_stamp": {
						long tmp;
						if( long.TryParse(value, out tmp) ) { obj.TimeStamp = CodeUtil.LongToByte(tmp); }
					}
					break;
				case "seqguid": {
						obj.SeqGuid = new Guid(value);
					}
					break;
				case "seqguid2": {
						obj.SeqGuid2 = new Guid(value);
					}
					break;
				case "strvalue3": {
						obj.StrValue3 = value;
					}
					break;
			}
		}
		private static void XmlToPrimaryKey(TestCodeDom.Table1 obj, XmlReader reader)
		{
			if( reader.MoveToAttribute("keyname") ) {
				string keyname = reader.ReadContentAsString();
				if( reader.MoveToAttribute("keyvalue") ) {
					string keyval = reader.ReadContentAsString();
					XmlToProperty(obj, keyname.ToLower(), keyval);
				}
				else {
					throw new InvalidOperationException("xml中不存在keyvalue属性");
				}
			}
			else {
				throw new InvalidOperationException("xml中不存在keyname属性");
			}
			reader.MoveToElement();
		}
		private static void XmlToSingle(TestCodeDom.Table1 obj, XmlReader reader)
		{
			int depth = reader.Depth;
			while( reader.Read() ) {
				if( reader.Depth == depth ) { break; }
				if( reader.NodeType == XmlNodeType.Element ) {
					string name = reader.Name;
					string val = reader.ReadString();
					XmlToProperty(obj, name.ToLower(), val);
				}
			}
		}
		public static object XmlToSingle(string xml)
		{
			TestCodeDom.Table1 obj = null;
			using( StringReader sr = new StringReader(xml) ) {
				using( XmlReader reader = XmlTextReader.Create(sr) ) {
					if( reader.ReadToFollowing("Table_1") ) {
						obj = new TestCodeDom.Table1();
						XmlToPrimaryKey(obj, reader);
						XmlToSingle(obj, reader);
					}
					else {
						throw new InvalidOperationException("xml中不存Table_1节点");
					}
				}
			}
			return obj;
		}
		public static object XmlToList(string xml)
		{
			List<TestCodeDom.Table1> list = new List<TestCodeDom.Table1>();
			using( StringReader sr = new StringReader(xml) ) {
				using( XmlReader reader = XmlTextReader.Create(sr) ) {
					if( reader.ReadToFollowing("Table_1") ) {
						TestCodeDom.Table1 obj = new TestCodeDom.Table1();
						XmlToPrimaryKey(obj, reader);
						XmlToSingle(obj, reader);
						list.Add(obj);
						while( reader.ReadToNextSibling("Table_1") ) {
							obj = new TestCodeDom.Table1();
							XmlToPrimaryKey(obj, reader);
							XmlToSingle(obj, reader);
							list.Add(obj);
						}
					}
					else {
						throw new InvalidOperationException("xml中不存Table_1节点");
					}
				}
			}
			return list;
		}
		public static object CloneMe(object src)
		{
			TestCodeDom.Table1 entity = (TestCodeDom.Table1)src;
			TestCodeDom.Table1 obj = new TestCodeDom.Table1();
			obj.RowId = entity.RowId;
			obj.Rowguid = entity.Rowguid;
			obj.StrValue = entity.StrValue;
			obj.StrValue2 = entity.StrValue2;
			obj.IntValue = entity.IntValue;
			obj.IntValue2 = entity.IntValue2;
			obj.Money = entity.Money;
			obj.Money2 = entity.Money2;
			obj.TimeStamp = entity.TimeStamp;
			obj.SeqGuid = entity.SeqGuid;
			obj.SeqGuid2 = entity.SeqGuid2;
			obj.StrValue3 = entity.StrValue3;
			return obj;
		}
	}
}