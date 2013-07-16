using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Xml;
using Mysoft.Map.Extensions.DAL;
namespace _Tool.AutoGenerateCode
{
	public static class TestSqlTraceCodeTemplate
	{
		private static readonly TestCodeDom.TestSqlTrace s_Empty = new TestCodeDom.TestSqlTrace();

		public static object Execute(int flag, object[] parameters)
		{
			switch( flag ) {
				case 1:
					return DataReaderToList((SqlDataReader)parameters[0]);
				case 2:
					return DataReaderToSingle((SqlDataReader)parameters[0]);
				case 3:
					return Insert((TestCodeDom.TestSqlTrace)parameters[0]);
				case 4:
					return Delete((TestCodeDom.TestSqlTrace)parameters[0]);
				case 5:
					return ConcurrencyDelete_TimeStamp((TestCodeDom.TestSqlTrace)parameters[0]);
				case 6:
					return ConcurrencyDelete_OriginalValue((TestCodeDom.TestSqlTrace)parameters[0]);
				case 7:
					return Update((TestCodeDom.TestSqlTrace)parameters[0]);
				case 8:
					return ConcurrencyUpdate_TimeStamp((TestCodeDom.TestSqlTrace)parameters[0], (TestCodeDom.TestSqlTrace)parameters[1]);
				case 9:
					return ConcurrencyUpdate_OriginalValue((TestCodeDom.TestSqlTrace)parameters[0], (TestCodeDom.TestSqlTrace)parameters[1]);
				case 10:
					return DataTableToList((DataTable)parameters[0]);
				default:
					throw new NotImplementedException();
			}
		}
		private static CPQuery UpdateSetFields(TestCodeDom.TestSqlTrace t1, TestCodeDom.TestSqlTrace t2)
		{
			bool changed = false;
			CPQuery query = CPQuery.Create() + "update [TestSqlTrace] set ";

			if( t1.TextData != t2.TextData ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [TextData] = " + (new SqlParameter("@TextData", (t1.TextData ?? (object)DBNull.Value)));
			}
			if( t1.BinaryData != t2.BinaryData ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [BinaryData] = " + (new SqlParameter("@BinaryData", (t1.BinaryData ?? (object)DBNull.Value)));
			}
			if( t1.TransactionID != t2.TransactionID ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [TransactionID] = " + (new SqlParameter("@TransactionID", (t1.TransactionID.HasValue ? t1.TransactionID.Value : (object)DBNull.Value)));
			}
			if( t1.NTUserName != t2.NTUserName ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [NTUserName] = " + (new SqlParameter("@NTUserName", (t1.NTUserName ?? (object)DBNull.Value)));
			}
			if( t1.StartTime != t2.StartTime ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [StartTime] = " + (new SqlParameter("@StartTime", (t1.StartTime.HasValue ? t1.StartTime.Value : (object)DBNull.Value)));
			}
			if( t1.SqlText != t2.SqlText ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [SqlText] = " + (new SqlParameter("@SqlText", (t1.SqlText ?? (object)DBNull.Value)));
			}
			if( t1.DatabaseID != t2.DatabaseID ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [DatabaseID] = " + (new SqlParameter("@DatabaseID", t1.DatabaseID));
			}
			if( changed == false )
				return null;

			else
				return query;
		}
		private static void WhereByPK(CPQuery query, TestCodeDom.TestSqlTrace t1)
		{
			query = query + " where ";
			query = query + " [Id] = " + (new SqlParameter("@Id", t1.Id));
		}
		private static void WhereByOriginalValue(CPQuery query, TestCodeDom.TestSqlTrace t1, TestCodeDom.TestSqlTrace t2)
		{
			query = query + " where ";
			query = query + " [Id] = " + (new SqlParameter("@original_Id", t2.Id));
			if( t1.TextData != t2.TextData ) {
				query = query + " and ";
				query = query + " ([TextData] = " + (new SqlParameter("@original_TextData", (t2.TextData ?? (object)DBNull.Value))) + " or @original_TextData is null and [TextData] is null)";
			}
			if( t1.BinaryData != t2.BinaryData ) {
				query = query + " and ";
				query = query + " ([BinaryData] = " + (new SqlParameter("@original_BinaryData", (t2.BinaryData ?? (object)DBNull.Value))) + " or @original_BinaryData is null and [BinaryData] is null)";
			}
			if( t1.TransactionID != t2.TransactionID ) {
				query = query + " and ";
				query = query + " ([TransactionID] = " + (new SqlParameter("@original_TransactionID", (t2.TransactionID.HasValue ? t2.TransactionID.Value : (object)DBNull.Value))) + " or @original_TransactionID is null and [TransactionID] is null)";
			}
			if( t1.NTUserName != t2.NTUserName ) {
				query = query + " and ";
				query = query + " ([NTUserName] = " + (new SqlParameter("@original_NTUserName", (t2.NTUserName ?? (object)DBNull.Value))) + " or @original_NTUserName is null and [NTUserName] is null)";
			}
			if( t1.StartTime != t2.StartTime ) {
				query = query + " and ";
				query = query + " ([StartTime] = " + (new SqlParameter("@original_StartTime", (t2.StartTime.HasValue ? t2.StartTime.Value : (object)DBNull.Value))) + " or @original_StartTime is null and [StartTime] is null)";
			}
			if( t1.SqlText != t2.SqlText ) {
				query = query + " and ";
				query = query + " ([SqlText] = " + (new SqlParameter("@original_SqlText", (t2.SqlText ?? (object)DBNull.Value))) + " or @original_SqlText is null and [SqlText] is null)";
			}
			if( t1.DatabaseID != t2.DatabaseID ) {
				query = query + " and ";
				query = query + " ([DatabaseID] = " + (new SqlParameter("@original_DatabaseID", t2.DatabaseID)) + ")";
			}
		}
		public static CPQuery Insert(TestCodeDom.TestSqlTrace t1)
		{
			TestCodeDom.TestSqlTrace t2 = s_Empty;
			bool changed = false;
			List<SqlParameter> parameters = new List<SqlParameter>();
			StringBuilder sqlBuilder = new StringBuilder();
			StringBuilder sqlParams = new StringBuilder();
			sqlBuilder.Append("insert into [TestSqlTrace] (");
			if( t1.TextData != t2.TextData ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[TextData]");
				sqlParams.Append("@TextData");
				parameters.Add(new SqlParameter("@TextData", (t1.TextData ?? (object)DBNull.Value)));
			}
			if( t1.BinaryData != t2.BinaryData ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[BinaryData]");
				sqlParams.Append("@BinaryData");
				parameters.Add(new SqlParameter("@BinaryData", (t1.BinaryData ?? (object)DBNull.Value)));
			}
			if( t1.TransactionID != t2.TransactionID ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[TransactionID]");
				sqlParams.Append("@TransactionID");
				parameters.Add(new SqlParameter("@TransactionID", (t1.TransactionID.HasValue ? t1.TransactionID.Value : (object)DBNull.Value)));
			}
			if( t1.NTUserName != t2.NTUserName ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[NTUserName]");
				sqlParams.Append("@NTUserName");
				parameters.Add(new SqlParameter("@NTUserName", (t1.NTUserName ?? (object)DBNull.Value)));
			}
			if( t1.StartTime != t2.StartTime ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[StartTime]");
				sqlParams.Append("@StartTime");
				parameters.Add(new SqlParameter("@StartTime", (t1.StartTime.HasValue ? t1.StartTime.Value : (object)DBNull.Value)));
			}
			if( t1.SqlText != t2.SqlText ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[SqlText]");
				sqlParams.Append("@SqlText");
				parameters.Add(new SqlParameter("@SqlText", (t1.SqlText ?? (object)DBNull.Value)));
			}
			if( t1.DatabaseID != t2.DatabaseID ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[DatabaseID]");
				sqlParams.Append("@DatabaseID");
				parameters.Add(new SqlParameter("@DatabaseID", t1.DatabaseID));
			}
			if( changed == false )
				return null;
			sqlBuilder.Append(") values (").Append(sqlParams.ToString()).Append(")");
			CPQuery query = CPQuery.From(sqlBuilder.ToString(), parameters.ToArray());
			return query;
		}
		public static CPQuery Delete(TestCodeDom.TestSqlTrace t1)
		{
			if( t1.Id == s_Empty.Id )
				throw new InvalidOperationException("没有为主键字段赋值：t1.Id");
			CPQuery query = CPQuery.Create() + "delete from [TestSqlTrace] ";
			WhereByPK(query, t1);
			return query;
		}
		public static CPQuery ConcurrencyDelete_TimeStamp(TestCodeDom.TestSqlTrace t2)
		{
			throw new InvalidOperationException("数据实体类型 TestCodeDom.TestSqlTrace 对应的数据表没有TimeStamp字段。");
		}
		public static CPQuery ConcurrencyDelete_OriginalValue(TestCodeDom.TestSqlTrace t2)
		{
			if( t2.Id == s_Empty.Id )
				throw new InvalidOperationException("没有为主键字段赋值：t2.Id");
			TestCodeDom.TestSqlTrace t1 = s_Empty;
			CPQuery query = CPQuery.Create() + "delete from [TestSqlTrace] ";
			WhereByOriginalValue(query, t1, t2);
			return query;
		}
		public static CPQuery Update(TestCodeDom.TestSqlTrace t1)
		{
			if( t1.Id == s_Empty.Id )
				throw new InvalidOperationException("没有为主键字段赋值：t1.Id");
			TestCodeDom.TestSqlTrace t2 = s_Empty;
			CPQuery query = UpdateSetFields(t1, t2);
			if( query == null )
				return null;
			WhereByPK(query, t1);
			return query;
		}
		public static CPQuery ConcurrencyUpdate_TimeStamp(TestCodeDom.TestSqlTrace t1, TestCodeDom.TestSqlTrace t2)
		{
			throw new InvalidOperationException("数据实体类型 TestCodeDom.TestSqlTrace 对应的数据表没有TimeStamp字段。");
		}
		public static CPQuery ConcurrencyUpdate_OriginalValue(TestCodeDom.TestSqlTrace t1, TestCodeDom.TestSqlTrace t2)
		{
			if( t2.Id == s_Empty.Id )
				throw new InvalidOperationException("没有为主键字段赋值：t2.Id");
			CPQuery query = UpdateSetFields(t1, t2);
			if( query == null )
				return null;
			WhereByOriginalValue(query, t1, t2);
			return query;
		}
		public static void SetEntityProperty(TestCodeDom.TestSqlTrace obj, string columnName, object value)
		{
			switch( columnName ) {
				case "textdata":
					if( value != DBNull.Value && value != null ) { obj.TextData = value.ToString(); }
					break;
				case "binarydata":
					if( value != DBNull.Value && value != null ) { obj.BinaryData = (byte[])value; }
					break;
				case "transactionid":
					if( value != DBNull.Value && value != null ) { obj.TransactionID = (long)value; }
					break;
				case "ntusername":
					if( value != DBNull.Value && value != null ) { obj.NTUserName = value.ToString(); }
					break;
				case "starttime":
					if( value != DBNull.Value && value != null ) { obj.StartTime = (DateTime)value; }
					break;
				case "sqltext":
					if( value != DBNull.Value && value != null ) { obj.SqlText = value.ToString(); }
					break;
				case "id":
					if( value != DBNull.Value && value != null ) { obj.Id = (int)value; }
					break;
				case "databaseid":
					if( value != DBNull.Value && value != null ) { obj.DatabaseID = (int)value; }
					break;
			}
		}
		public static object DataReaderToList(SqlDataReader dr)
		{
			List<TestCodeDom.TestSqlTrace> list = new List<TestCodeDom.TestSqlTrace>();
			string[] columnNames = CodeUtil.GetColumnNames(dr);
			while( dr.Read() ) {
				TestCodeDom.TestSqlTrace obj = new TestCodeDom.TestSqlTrace();
				for( int i = 0; i < columnNames.Length; i++ ) {
					SetEntityProperty(obj, columnNames[i], dr.GetValue(i));
				}
				list.Add(obj);
			}
			return list;
		}
		public static object DataReaderToSingle(SqlDataReader dr)
		{
			TestCodeDom.TestSqlTrace obj = null;
			string[] columnNames = CodeUtil.GetColumnNames(dr);
			if( dr.Read() ) {
				obj = new TestCodeDom.TestSqlTrace();
				for( int i = 0; i < columnNames.Length; i++ ) {
					SetEntityProperty(obj, columnNames[i], dr.GetValue(i));
				}
			}
			return obj;
		}
		public static object DataTableToList(DataTable table)
		{
			List<TestCodeDom.TestSqlTrace> list = new List<TestCodeDom.TestSqlTrace>(table.Rows.Count);
			string[] columnNames = CodeUtil.GetColumnNames(table);
			foreach( DataRow row in table.Rows ) {
				TestCodeDom.TestSqlTrace obj = new TestCodeDom.TestSqlTrace();
				for( int i = 0; i < columnNames.Length; i++ ) {
					string columnName = columnNames[i];
					SetEntityProperty(obj, columnName, row[columnName]);
				}
				list.Add(obj);
			}
			return list;
		}
	}
}