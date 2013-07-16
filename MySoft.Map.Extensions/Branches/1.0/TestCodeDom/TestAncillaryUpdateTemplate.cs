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
	public static class TestAncillaryUpdate
	{
		private static readonly TestCodeDom.TestAncillaryUpdate s_Empty = new TestCodeDom.TestAncillaryUpdate();

		public static object Execute(int flag, object[] parameters)
		{
			switch( flag ) {
				case 1:
					return DataReaderToList((SqlDataReader)parameters[0]);
				case 2:
					return DataReaderToSingle((SqlDataReader)parameters[0]);
				case 3:
					return Insert((TestCodeDom.TestAncillaryUpdate)parameters[0]);
				case 4:
					return Delete((TestCodeDom.TestAncillaryUpdate)parameters[0]);
				case 5:
					return ConcurrencyDelete_TimeStamp((TestCodeDom.TestAncillaryUpdate)parameters[0]);
				case 6:
					return ConcurrencyDelete_OriginalValue((TestCodeDom.TestAncillaryUpdate)parameters[0]);
				case 7:
					return Update((TestCodeDom.TestAncillaryUpdate)parameters[0], (TestCodeDom.TestAncillaryUpdate)parameters[1]);
				case 8:
					return ConcurrencyUpdate_TimeStamp((TestCodeDom.TestAncillaryUpdate)parameters[0], (TestCodeDom.TestAncillaryUpdate)parameters[1], (TestCodeDom.TestAncillaryUpdate)parameters[2]);
				case 9:
					return ConcurrencyUpdate_OriginalValue((TestCodeDom.TestAncillaryUpdate)parameters[0], (TestCodeDom.TestAncillaryUpdate)parameters[1], (TestCodeDom.TestAncillaryUpdate)parameters[2]);
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
		private static CPQuery UpdateSetFields(TestCodeDom.TestAncillaryUpdate t1, TestCodeDom.TestAncillaryUpdate t2)
		{
			bool changed = false;
			string[] zeroProperties = t1.GetZeroProperties();
			CPQuery query = CPQuery.Create() + "update [TestAncillaryUpdate] set ";

			if( t1.StrValue != t2.StrValue ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [StrValue] = " + (new SqlParameter("@StrValue", (t1.StrValue ?? (object)DBNull.Value)));
			}
			if( t1.DecValue != t2.DecValue ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [DecValue] = " + (new SqlParameter("@DecValue", (t1.DecValue.HasValue ? t1.DecValue.Value : (object)DBNull.Value)));
			}
			if( t1.IntId != t2.IntId ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [IntId] = " + (new SqlParameter("@IntId", (t1.IntId.HasValue ? t1.IntId.Value : (object)DBNull.Value)));
			}
			if( changed == false )
				return null;

			else
				return query;
		}
		private static void WhereByPK(CPQuery query, TestCodeDom.TestAncillaryUpdate t1)
		{
			query = query + " where ";
			query = query + " [GuidId] = " + (new SqlParameter("@GuidId", t1.GuidId));
		}
		private static void WhereByTimeStamp(CPQuery query, TestCodeDom.TestAncillaryUpdate t2)
		{
			query = query + " where ";
			query = query + " [GuidId] = " + (new SqlParameter("@GuidId", t2.GuidId));
			query = query + " and ";
			query = query + " [TimeStampValue] = " + (new SqlParameter("@original_TimeStampValue", CodeUtil.LongToByte(t2.TimeStampValue)));
		}
		private static void WhereByOriginalValue(CPQuery query, TestCodeDom.TestAncillaryUpdate t1, TestCodeDom.TestAncillaryUpdate t2)
		{
			string[] zeroProperties = t2.GetZeroProperties();
			query = query + " where ";
			query = query + " [GuidId] = " + (new SqlParameter("@original_GuidId", t2.GuidId));
			if( t1.StrValue != t2.StrValue ) {
				query = query + " and ";
				query = query + " ([StrValue] = " + (new SqlParameter("@original_StrValue", (t2.StrValue ?? (object)DBNull.Value))) + " or @original_StrValue is null and [StrValue] is null)";
			}
			if( t1.DecValue != t2.DecValue ) {
				query = query + " and ";
				query = query + " ([DecValue] = " + (new SqlParameter("@original_DecValue", (t2.DecValue.HasValue ? t2.DecValue.Value : (object)DBNull.Value))) + " or @original_DecValue is null and [DecValue] is null)";
			}
			if( t1.IntId != t2.IntId ) {
				query = query + " and ";
				query = query + " ([IntId] = " + (new SqlParameter("@original_IntId", (t2.IntId.HasValue ? t2.IntId.Value : (object)DBNull.Value))) + " or @original_IntId is null and [IntId] is null)";
			}
		}
		public static CPQuery Insert(TestCodeDom.TestAncillaryUpdate t1)
		{
			TestCodeDom.TestAncillaryUpdate t2 = s_Empty;
			bool changed = false;
			string[] zeroProperties = t1.GetZeroProperties();
			List<SqlParameter> parameters = new List<SqlParameter>();
			StringBuilder sqlBuilder = new StringBuilder();
			StringBuilder sqlParams = new StringBuilder();
			sqlBuilder.Append("insert into [TestAncillaryUpdate] (");
			if( t1.GuidId != t2.GuidId || Array.IndexOf(zeroProperties, "GuidId") >= 0 ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[GuidId]");
				sqlParams.Append("@GuidId");
				parameters.Add(new SqlParameter("@GuidId", t1.GuidId));
			}
			if( t1.StrValue != t2.StrValue ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[StrValue]");
				sqlParams.Append("@StrValue");
				parameters.Add(new SqlParameter("@StrValue", (t1.StrValue ?? (object)DBNull.Value)));
			}
			if( t1.DecValue != t2.DecValue ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[DecValue]");
				sqlParams.Append("@DecValue");
				parameters.Add(new SqlParameter("@DecValue", (t1.DecValue.HasValue ? t1.DecValue.Value : (object)DBNull.Value)));
			}
			if( t1.IntId != t2.IntId ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[IntId]");
				sqlParams.Append("@IntId");
				parameters.Add(new SqlParameter("@IntId", (t1.IntId.HasValue ? t1.IntId.Value : (object)DBNull.Value)));
			}
			if( changed == false )
				return null;
			sqlBuilder.Append(") values (").Append(sqlParams.ToString()).Append(")");
			CPQuery query = CPQuery.From(sqlBuilder.ToString(), parameters.ToArray());
			return query;
		}
		public static CPQuery Delete(TestCodeDom.TestAncillaryUpdate t1)
		{
			if( t1.GuidId == s_Empty.GuidId )
				throw new InvalidOperationException("没有为主键字段赋值：t1.GuidId");
			CPQuery query = CPQuery.Create() + "delete from [TestAncillaryUpdate] ";
			WhereByPK(query, t1);
			return query;
		}
		public static CPQuery ConcurrencyDelete_TimeStamp(TestCodeDom.TestAncillaryUpdate t2)
		{
			if( t2.GuidId == s_Empty.GuidId )
				throw new InvalidOperationException("没有为主键字段赋值：t2.GuidId");
			CPQuery query = CPQuery.Create() + "delete from [TestAncillaryUpdate] ";
			WhereByTimeStamp(query, t2);
			return query;
		}
		public static CPQuery ConcurrencyDelete_OriginalValue(TestCodeDom.TestAncillaryUpdate t2)
		{
			if( t2.GuidId == s_Empty.GuidId )
				throw new InvalidOperationException("没有为主键字段赋值：t2.GuidId");
			TestCodeDom.TestAncillaryUpdate t1 = s_Empty;
			CPQuery query = CPQuery.Create() + "delete from [TestAncillaryUpdate] ";
			WhereByOriginalValue(query, t1, t2);
			return query;
		}
		public static CPQuery Update(TestCodeDom.TestAncillaryUpdate t1, TestCodeDom.TestAncillaryUpdate t3)
		{
			if( t1.GuidId == s_Empty.GuidId )
				throw new InvalidOperationException("没有为主键字段赋值：t1.GuidId");
			TestCodeDom.TestAncillaryUpdate t2 = t3 ?? s_Empty;
			CPQuery query = UpdateSetFields(t1, t2);
			if( query == null )
				return null;
			WhereByPK(query, t1);
			return query;
		}
		public static CPQuery ConcurrencyUpdate_TimeStamp(TestCodeDom.TestAncillaryUpdate t1, TestCodeDom.TestAncillaryUpdate t2, TestCodeDom.TestAncillaryUpdate t3)
		{
			if( t2.GuidId == s_Empty.GuidId )
				throw new InvalidOperationException("没有为主键字段赋值：t2.GuidId");
			CPQuery query = UpdateSetFields(t1, t3 ?? s_Empty);
			if( query == null )
				return null;
			WhereByTimeStamp(query, t2);
			return query;
		}
		public static CPQuery ConcurrencyUpdate_OriginalValue(TestCodeDom.TestAncillaryUpdate t1, TestCodeDom.TestAncillaryUpdate t2, TestCodeDom.TestAncillaryUpdate t3)
		{
			if( t2.GuidId == s_Empty.GuidId )
				throw new InvalidOperationException("没有为主键字段赋值：t2.GuidId");
			CPQuery query = UpdateSetFields(t1, t3 ?? s_Empty);
			if( query == null )
				return null;
			WhereByOriginalValue(query, s_Empty, t2);
			return query;
		}
		private static void SetEntityProperty(TestCodeDom.TestAncillaryUpdate obj, string columnName, object value)
		{
			if( value == DBNull.Value || value == null ) {
				return;
			}
			switch( columnName ) {
				case "guidid":
					obj.GuidId = (Guid)value;
					break;
				case "strvalue":
					obj.StrValue = value.ToString();
					break;
				case "decvalue":
					obj.DecValue = (decimal)value;
					break;
				case "intid":
					obj.IntId = (int)value;
					break;
				case "timestampvalue":
					obj.TimeStampValue = value.GetType() == typeof(long) ? (long)value : CodeUtil.ByteToLong((byte[])value);
					break;
			}
		}
		public static object DataReaderToList(SqlDataReader dr)
		{
			List<TestCodeDom.TestAncillaryUpdate> list = new List<TestCodeDom.TestAncillaryUpdate>();
			string[] columnNames = CodeUtil.GetColumnNames(dr);
			while( dr.Read() ) {
				TestCodeDom.TestAncillaryUpdate obj = new TestCodeDom.TestAncillaryUpdate();
				for( int i = 0; i < columnNames.Length; i++ ) {
					SetEntityProperty(obj, columnNames[i], dr.GetValue(i));
				}
				list.Add(obj);
			}
			return list;
		}
		public static object DataReaderToSingle(SqlDataReader dr)
		{
			TestCodeDom.TestAncillaryUpdate obj = null;
			string[] columnNames = CodeUtil.GetColumnNames(dr);
			if( dr.Read() ) {
				obj = new TestCodeDom.TestAncillaryUpdate();
				for( int i = 0; i < columnNames.Length; i++ ) {
					SetEntityProperty(obj, columnNames[i], dr.GetValue(i));
				}
			}
			return obj;
		}
		public static object DataTableToList(DataTable table)
		{
			List<TestCodeDom.TestAncillaryUpdate> list = new List<TestCodeDom.TestAncillaryUpdate>(table.Rows.Count);
			string[] columnNames = CodeUtil.GetColumnNames(table);
			foreach( DataRow row in table.Rows ) {
				TestCodeDom.TestAncillaryUpdate obj = new TestCodeDom.TestAncillaryUpdate();
				for( int i = 0; i < columnNames.Length; i++ ) {
					string columnName = columnNames[i];
					SetEntityProperty(obj, columnName, row[columnName]);
				}
				list.Add(obj);
			}
			return list;
		}
		private static void XmlToProperty(TestCodeDom.TestAncillaryUpdate obj, string columnName, string value)
		{
			if( string.IsNullOrEmpty(value) ) { return; }
			switch( columnName ) {
				case "guidid": {
						obj.GuidId = new Guid(value);
					}
					break;
				case "strvalue": {
						obj.StrValue = value;
					}
					break;
				case "decvalue": {
						decimal tmp;
						if( decimal.TryParse(value, out tmp) ) { obj.DecValue = tmp; }
					}
					break;
				case "intid": {
						int tmp;
						if( int.TryParse(value, out tmp) ) { obj.IntId = tmp; }
					}
					break;
				case "timestampvalue": {
						long tmp;
						if( long.TryParse(value, out tmp) ) { obj.TimeStampValue = tmp; }
					}
					break;
			}
		}
		private static void XmlToPrimaryKey(TestCodeDom.TestAncillaryUpdate obj, XmlReader reader)
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
		private static void XmlToSingle(TestCodeDom.TestAncillaryUpdate obj, XmlReader reader)
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
			TestCodeDom.TestAncillaryUpdate obj = null;
			using( StringReader sr = new StringReader(xml) ) {
				using( XmlReader reader = XmlTextReader.Create(sr) ) {
					if( reader.ReadToFollowing("TestAncillaryUpdate") ) {
						obj = new TestCodeDom.TestAncillaryUpdate();
						XmlToPrimaryKey(obj, reader);
						XmlToSingle(obj, reader);
					}
					else {
						throw new InvalidOperationException("xml中不存TestAncillaryUpdate节点");
					}
				}
			}
			return obj;
		}
		public static object XmlToList(string xml)
		{
			List<TestCodeDom.TestAncillaryUpdate> list = new List<TestCodeDom.TestAncillaryUpdate>();
			using( StringReader sr = new StringReader(xml) ) {
				using( XmlReader reader = XmlTextReader.Create(sr) ) {
					if( reader.ReadToFollowing("TestAncillaryUpdate") ) {
						TestCodeDom.TestAncillaryUpdate obj = new TestCodeDom.TestAncillaryUpdate();
						XmlToPrimaryKey(obj, reader);
						XmlToSingle(obj, reader);
						list.Add(obj);
						while( reader.ReadToNextSibling("TestAncillaryUpdate") ) {
							obj = new TestCodeDom.TestAncillaryUpdate();
							XmlToPrimaryKey(obj, reader);
							XmlToSingle(obj, reader);
							list.Add(obj);
						}
					}
					else {
						throw new InvalidOperationException("xml中不存TestAncillaryUpdate节点");
					}
				}
			}
			return list;
		}
		public static object CloneMe(object src)
		{
			TestCodeDom.TestAncillaryUpdate entity = (TestCodeDom.TestAncillaryUpdate)src;
			TestCodeDom.TestAncillaryUpdate obj = new TestCodeDom.TestAncillaryUpdate();
			obj.GuidId = entity.GuidId;
			obj.StrValue = entity.StrValue;
			obj.DecValue = entity.DecValue;
			obj.IntId = entity.IntId;
			obj.TimeStampValue = entity.TimeStampValue;
			return obj;
		}
	}
}