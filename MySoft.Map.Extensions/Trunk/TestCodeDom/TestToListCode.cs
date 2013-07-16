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
	/// <summary>
	///
	/// </summary>
	public static class TestCUD1
	{
		private static readonly SmokingTest.CS.Entity.SDyContract s_Empty = new SmokingTest.CS.Entity.SDyContract();
		/// <summary>
		/// 
		/// </summary>
		/// <param name="flag"></param>
		/// <param name="parameters"></param>
		/// <returns></returns>
		public static object Execute(int flag, object[] parameters)
		{
			switch( flag ) {
				case 1:
					return DataReaderToList((SqlDataReader)parameters[0]);
				case 2:
					return DataReaderToSingle((SqlDataReader)parameters[0]);
				case 3:
					return Insert((SmokingTest.CS.Entity.SDyContract)parameters[0]);
				case 4:
					return Delete((SmokingTest.CS.Entity.SDyContract)parameters[0]);
				case 5:
					return ConcurrencyDelete_TimeStamp((SmokingTest.CS.Entity.SDyContract)parameters[0]);
				case 6:
					return ConcurrencyDelete_OriginalValue((SmokingTest.CS.Entity.SDyContract)parameters[0]);
				case 7:
					return Update((SmokingTest.CS.Entity.SDyContract)parameters[0], (SmokingTest.CS.Entity.SDyContract)parameters[1]);
				case 8:
					return ConcurrencyUpdate_TimeStamp((SmokingTest.CS.Entity.SDyContract)parameters[0], (SmokingTest.CS.Entity.SDyContract)parameters[1], (SmokingTest.CS.Entity.SDyContract)parameters[2]);
				case 9:
					return ConcurrencyUpdate_OriginalValue((SmokingTest.CS.Entity.SDyContract)parameters[0], (SmokingTest.CS.Entity.SDyContract)parameters[1], (SmokingTest.CS.Entity.SDyContract)parameters[2]);
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
		private static CPQuery UpdateSetFields(SmokingTest.CS.Entity.SDyContract t1, SmokingTest.CS.Entity.SDyContract t2)
		{
			bool changed = false;
			string[] zeroProperties = t1.GetZeroProperties();
			CPQuery query = CPQuery.Create() + "update [s_DyContract] set ";

			if( t1.DyContractNo != t2.DyContractNo ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [DyContractNo] = " + (new SqlParameter("@DyContractNo", (t1.DyContractNo ?? (object)DBNull.Value)));
			}
			if( t1.DyDate != t2.DyDate ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [DyDate] = " + (new SqlParameter("@DyDate", (t1.DyDate.HasValue ? t1.DyDate.Value : (object)DBNull.Value)));
			}
			if( t1.JkContractNo != t2.JkContractNo ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [JkContractNo] = " + (new SqlParameter("@JkContractNo", (t1.JkContractNo ?? (object)DBNull.Value)));
			}
			if( t1.JkBank != t2.JkBank ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [JkBank] = " + (new SqlParameter("@JkBank", (t1.JkBank ?? (object)DBNull.Value)));
			}
			if( t1.JkAmount != t2.JkAmount ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [JkAmount] = " + (new SqlParameter("@JkAmount", (t1.JkAmount.HasValue ? t1.JkAmount.Value : (object)DBNull.Value)));
			}
			if( t1.Pgcompany != t2.Pgcompany ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [Pgcompany] = " + (new SqlParameter("@Pgcompany", (t1.Pgcompany ?? (object)DBNull.Value)));
			}
			if( t1.PgAmount != t2.PgAmount ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [PgAmount] = " + (new SqlParameter("@PgAmount", (t1.PgAmount.HasValue ? t1.PgAmount.Value : (object)DBNull.Value)));
			}
			if( t1.FkDate != t2.FkDate ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [FkDate] = " + (new SqlParameter("@FkDate", (t1.FkDate.HasValue ? t1.FkDate.Value : (object)DBNull.Value)));
			}
			if( t1.Remarks != t2.Remarks ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [Remarks] = " + (new SqlParameter("@Remarks", (t1.Remarks ?? (object)DBNull.Value)));
			}
			if( t1.ApproveState != t2.ApproveState ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [ApproveState] = " + (new SqlParameter("@ApproveState", (t1.ApproveState ?? (object)DBNull.Value)));
			}
			if( changed == false )
				return null;

			else
				return query;
		}
		private static void WhereByPK(CPQuery query, SmokingTest.CS.Entity.SDyContract t1)
		{
			query = query + " where ";
			query = query + " [ContractGUID] = " + (new SqlParameter("@ContractGUID", t1.ContractGUID));
		}
		private static void WhereByTimeStamp(CPQuery query, SmokingTest.CS.Entity.SDyContract t2)
		{
			query = query + " where ";
			query = query + " [ContractGUID] = " + (new SqlParameter("@ContractGUID", t2.ContractGUID));
			query = query + " and ";
			query = query + " [ContractVersion] = " + (new SqlParameter("@original_ContractVersion", CodeUtil.LongToByte(t2.ContractVersion)));
		}
		private static void WhereByOriginalValue(CPQuery query, SmokingTest.CS.Entity.SDyContract t1, SmokingTest.CS.Entity.SDyContract t2)
		{
			string[] zeroProperties = t2.GetZeroProperties();
			query = query + " where ";
			query = query + " [ContractGUID] = " + (new SqlParameter("@original_ContractGUID", t2.ContractGUID));
			if( t1.DyContractNo != t2.DyContractNo ) {
				query = query + " and ";
				query = query + " ([DyContractNo] = " + (new SqlParameter("@original_DyContractNo", (t2.DyContractNo ?? (object)DBNull.Value))) + " or @original_DyContractNo is null and [DyContractNo] is null)";
			}
			if( t1.DyDate != t2.DyDate ) {
				query = query + " and ";
				query = query + " ([DyDate] = " + (new SqlParameter("@original_DyDate", (t2.DyDate.HasValue ? t2.DyDate.Value : (object)DBNull.Value))) + " or @original_DyDate is null and [DyDate] is null)";
			}
			if( t1.JkContractNo != t2.JkContractNo ) {
				query = query + " and ";
				query = query + " ([JkContractNo] = " + (new SqlParameter("@original_JkContractNo", (t2.JkContractNo ?? (object)DBNull.Value))) + " or @original_JkContractNo is null and [JkContractNo] is null)";
			}
			if( t1.JkBank != t2.JkBank ) {
				query = query + " and ";
				query = query + " ([JkBank] = " + (new SqlParameter("@original_JkBank", (t2.JkBank ?? (object)DBNull.Value))) + " or @original_JkBank is null and [JkBank] is null)";
			}
			if( t1.JkAmount != t2.JkAmount ) {
				query = query + " and ";
				query = query + " ([JkAmount] = " + (new SqlParameter("@original_JkAmount", (t2.JkAmount.HasValue ? t2.JkAmount.Value : (object)DBNull.Value))) + " or @original_JkAmount is null and [JkAmount] is null)";
			}
			if( t1.Pgcompany != t2.Pgcompany ) {
				query = query + " and ";
				query = query + " ([Pgcompany] = " + (new SqlParameter("@original_Pgcompany", (t2.Pgcompany ?? (object)DBNull.Value))) + " or @original_Pgcompany is null and [Pgcompany] is null)";
			}
			if( t1.PgAmount != t2.PgAmount ) {
				query = query + " and ";
				query = query + " ([PgAmount] = " + (new SqlParameter("@original_PgAmount", (t2.PgAmount.HasValue ? t2.PgAmount.Value : (object)DBNull.Value))) + " or @original_PgAmount is null and [PgAmount] is null)";
			}
			if( t1.FkDate != t2.FkDate ) {
				query = query + " and ";
				query = query + " ([FkDate] = " + (new SqlParameter("@original_FkDate", (t2.FkDate.HasValue ? t2.FkDate.Value : (object)DBNull.Value))) + " or @original_FkDate is null and [FkDate] is null)";
			}
			if( t1.Remarks != t2.Remarks ) {
				query = query + " and ";
				query = query + " ([Remarks] = " + (new SqlParameter("@original_Remarks", (t2.Remarks ?? (object)DBNull.Value))) + " or @original_Remarks is null and [Remarks] is null)";
			}
			if( t1.ApproveState != t2.ApproveState ) {
				query = query + " and ";
				query = query + " ([ApproveState] = " + (new SqlParameter("@original_ApproveState", (t2.ApproveState ?? (object)DBNull.Value))) + " or @original_ApproveState is null and [ApproveState] is null)";
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="t1"></param>
		/// <returns></returns>
		public static CPQuery Insert(SmokingTest.CS.Entity.SDyContract t1)
		{
			SmokingTest.CS.Entity.SDyContract t2 = s_Empty;
			bool changed = false;
			string[] zeroProperties = t1.GetZeroProperties();
			List<SqlParameter> parameters = new List<SqlParameter>();
			StringBuilder sqlBuilder = new StringBuilder();
			StringBuilder sqlParams = new StringBuilder();
			sqlBuilder.Append("insert into [s_DyContract] (");
			if( t1.ContractGUID != t2.ContractGUID || Array.IndexOf(zeroProperties, "ContractGUID") >= 0 ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[ContractGUID]");
				sqlParams.Append("@ContractGUID");
				parameters.Add(new SqlParameter("@ContractGUID", t1.ContractGUID));
			}
			if( t1.DyContractNo != t2.DyContractNo ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[DyContractNo]");
				sqlParams.Append("@DyContractNo");
				parameters.Add(new SqlParameter("@DyContractNo", (t1.DyContractNo ?? (object)DBNull.Value)));
			}
			if( t1.DyDate != t2.DyDate ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[DyDate]");
				sqlParams.Append("@DyDate");
				parameters.Add(new SqlParameter("@DyDate", (t1.DyDate.HasValue ? t1.DyDate.Value : (object)DBNull.Value)));
			}
			if( t1.JkContractNo != t2.JkContractNo ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[JkContractNo]");
				sqlParams.Append("@JkContractNo");
				parameters.Add(new SqlParameter("@JkContractNo", (t1.JkContractNo ?? (object)DBNull.Value)));
			}
			if( t1.JkBank != t2.JkBank ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[JkBank]");
				sqlParams.Append("@JkBank");
				parameters.Add(new SqlParameter("@JkBank", (t1.JkBank ?? (object)DBNull.Value)));
			}
			if( t1.JkAmount != t2.JkAmount ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[JkAmount]");
				sqlParams.Append("@JkAmount");
				parameters.Add(new SqlParameter("@JkAmount", (t1.JkAmount.HasValue ? t1.JkAmount.Value : (object)DBNull.Value)));
			}
			if( t1.Pgcompany != t2.Pgcompany ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[Pgcompany]");
				sqlParams.Append("@Pgcompany");
				parameters.Add(new SqlParameter("@Pgcompany", (t1.Pgcompany ?? (object)DBNull.Value)));
			}
			if( t1.PgAmount != t2.PgAmount ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[PgAmount]");
				sqlParams.Append("@PgAmount");
				parameters.Add(new SqlParameter("@PgAmount", (t1.PgAmount.HasValue ? t1.PgAmount.Value : (object)DBNull.Value)));
			}
			if( t1.FkDate != t2.FkDate ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[FkDate]");
				sqlParams.Append("@FkDate");
				parameters.Add(new SqlParameter("@FkDate", (t1.FkDate.HasValue ? t1.FkDate.Value : (object)DBNull.Value)));
			}
			if( t1.Remarks != t2.Remarks ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[Remarks]");
				sqlParams.Append("@Remarks");
				parameters.Add(new SqlParameter("@Remarks", (t1.Remarks ?? (object)DBNull.Value)));
			}
			if( t1.ApproveState != t2.ApproveState ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[ApproveState]");
				sqlParams.Append("@ApproveState");
				parameters.Add(new SqlParameter("@ApproveState", (t1.ApproveState ?? (object)DBNull.Value)));
			}
			if( changed == false )
				return null;
			sqlBuilder.Append(") values (").Append(sqlParams.ToString()).Append(")");
			CPQuery query = CPQuery.From(sqlBuilder.ToString(), parameters.ToArray());
			return query;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="t1"></param>
		/// <returns></returns>
		public static CPQuery Delete(SmokingTest.CS.Entity.SDyContract t1)
		{
			if( t1.ContractGUID == s_Empty.ContractGUID )
				throw new InvalidOperationException("没有为主键字段赋值：t1.ContractGUID");
			CPQuery query = CPQuery.Create() + "delete from [s_DyContract] ";
			WhereByPK(query, t1);
			return query;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="t2"></param>
		/// <returns></returns>
		public static CPQuery ConcurrencyDelete_TimeStamp(SmokingTest.CS.Entity.SDyContract t2)
		{
			if( t2.ContractGUID == s_Empty.ContractGUID )
				throw new InvalidOperationException("没有为主键字段赋值：t2.ContractGUID");
			CPQuery query = CPQuery.Create() + "delete from [s_DyContract] ";
			WhereByTimeStamp(query, t2);
			return query;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="t2"></param>
		/// <returns></returns>
		public static CPQuery ConcurrencyDelete_OriginalValue(SmokingTest.CS.Entity.SDyContract t2)
		{
			if( t2.ContractGUID == s_Empty.ContractGUID )
				throw new InvalidOperationException("没有为主键字段赋值：t2.ContractGUID");
			SmokingTest.CS.Entity.SDyContract t1 = s_Empty;
			CPQuery query = CPQuery.Create() + "delete from [s_DyContract] ";
			WhereByOriginalValue(query, t1, t2);
			return query;
		}
		/// <summary>
		/// 修改
		/// </summary>
		/// <param name="t1"></param>
		/// <param name="t3"></param>
		/// <returns></returns>
		public static CPQuery Update(SmokingTest.CS.Entity.SDyContract t1, SmokingTest.CS.Entity.SDyContract t3)
		{
			if( t1.ContractGUID == s_Empty.ContractGUID )
				throw new InvalidOperationException("没有为主键字段赋值：t1.ContractGUID");
			SmokingTest.CS.Entity.SDyContract t2 = t3 ?? s_Empty;
			CPQuery query = UpdateSetFields(t1, t2);
			if( query == null )
				return null;
			WhereByPK(query, t1);
			return query;
		}
		/// <summary>
		/// 时间戳并发修改
		/// </summary>
		/// <param name="t1"></param>
		/// <param name="t2"></param>
		/// <param name="t3"></param>
		/// <returns></returns>
		public static CPQuery ConcurrencyUpdate_TimeStamp(SmokingTest.CS.Entity.SDyContract t1, SmokingTest.CS.Entity.SDyContract t2, SmokingTest.CS.Entity.SDyContract t3)
		{
			if( t2.ContractGUID == s_Empty.ContractGUID )
				throw new InvalidOperationException("没有为主键字段赋值：t2.ContractGUID");
			CPQuery query = UpdateSetFields(t1, t3 ?? s_Empty);
			if( query == null )
				return null;
			WhereByTimeStamp(query, t2);
			return query;
		}
		/// <summary>
		/// 原始值并发修改
		/// </summary>
		/// <param name="t1"></param>
		/// <param name="t2"></param>
		/// <param name="t3"></param>
		/// <returns></returns>
		public static CPQuery ConcurrencyUpdate_OriginalValue(SmokingTest.CS.Entity.SDyContract t1, SmokingTest.CS.Entity.SDyContract t2, SmokingTest.CS.Entity.SDyContract t3)
		{
			if( t2.ContractGUID == s_Empty.ContractGUID )
				throw new InvalidOperationException("没有为主键字段赋值：t2.ContractGUID");
			CPQuery query = UpdateSetFields(t1, t3 ?? s_Empty);
			if( query == null )
				return null;
			WhereByOriginalValue(query, s_Empty, t2);
			return query;
		}
		private static void SetEntityProperty(SmokingTest.CS.Entity.SDyContract obj, string columnName, object value)
		{
			if( value == DBNull.Value || value == null ) {
				return;
			}
			switch( columnName ) {
				case "contractguid":
					obj.ContractGUID = (Guid)value;
					break;
				case "dycontractno":
					obj.DyContractNo = value.ToString();
					break;
				case "dydate":
					obj.DyDate = (DateTime)value;
					break;
				case "jkcontractno":
					obj.JkContractNo = value.ToString();
					break;
				case "jkbank":
					obj.JkBank = value.ToString();
					break;
				case "jkamount":
					obj.JkAmount = (decimal)value;
					break;
				case "pgcompany":
					obj.Pgcompany = value.ToString();
					break;
				case "pgamount":
					obj.PgAmount = (decimal)value;
					break;
				case "fkdate":
					obj.FkDate = (DateTime)value;
					break;
				case "remarks":
					obj.Remarks = value.ToString();
					break;
				case "contractversion":
					obj.ContractVersion = value.GetType() == typeof(long) ? (long)value : CodeUtil.ByteToLong((byte[])value);
					break;
				case "approvestate":
					obj.ApproveState = value.ToString();
					break;
			}
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="dr"></param>
		/// <returns></returns>
		public static object DataReaderToList(SqlDataReader dr)
		{
			List<SmokingTest.CS.Entity.SDyContract> list = new List<SmokingTest.CS.Entity.SDyContract>();
			string[] columnNames = CodeUtil.GetColumnNames(dr);
			while( dr.Read() ) {
				SmokingTest.CS.Entity.SDyContract obj = new SmokingTest.CS.Entity.SDyContract();
				for( int i = 0; i < columnNames.Length; i++ ) {
					SetEntityProperty(obj, columnNames[i], dr.GetValue(i));
				}
				list.Add(obj);
			}
			return list;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="dr"></param>
		/// <returns></returns>
		public static object DataReaderToSingle(SqlDataReader dr)
		{
			SmokingTest.CS.Entity.SDyContract obj = null;
			string[] columnNames = CodeUtil.GetColumnNames(dr);
			if( dr.Read() ) {
				obj = new SmokingTest.CS.Entity.SDyContract();
				for( int i = 0; i < columnNames.Length; i++ ) {
					SetEntityProperty(obj, columnNames[i], dr.GetValue(i));
				}
			}
			return obj;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static object DataTableToList(DataTable table)
		{
			List<SmokingTest.CS.Entity.SDyContract> list = new List<SmokingTest.CS.Entity.SDyContract>(table.Rows.Count);
			string[] columnNames = CodeUtil.GetColumnNames(table);
			foreach( DataRow row in table.Rows ) {
				SmokingTest.CS.Entity.SDyContract obj = new SmokingTest.CS.Entity.SDyContract();
				for( int i = 0; i < columnNames.Length; i++ ) {
					string columnName = columnNames[i];
					SetEntityProperty(obj, columnName, row[columnName]);
				}
				list.Add(obj);
			}
			return list;
		}
		private static void XmlToProperty(SmokingTest.CS.Entity.SDyContract obj, string columnName, string value)
		{
			if( string.IsNullOrEmpty(value) ) { return; }
			switch( columnName ) {
				case "contractguid": {
						obj.ContractGUID = new Guid(value);
					}
					break;
				case "dycontractno": {
						obj.DyContractNo = value;
					}
					break;
				case "dydate": {
						DateTime tmp;
						if( DateTime.TryParse(value, out tmp) ) { obj.DyDate = tmp; };
					}
					break;
				case "jkcontractno": {
						obj.JkContractNo = value;
					}
					break;
				case "jkbank": {
						obj.JkBank = value;
					}
					break;
				case "jkamount": {
						decimal tmp;
						if( decimal.TryParse(value, out tmp) ) { obj.JkAmount = tmp; }
					}
					break;
				case "pgcompany": {
						obj.Pgcompany = value;
					}
					break;
				case "pgamount": {
						decimal tmp;
						if( decimal.TryParse(value, out tmp) ) { obj.PgAmount = tmp; }
					}
					break;
				case "fkdate": {
						DateTime tmp;
						if( DateTime.TryParse(value, out tmp) ) { obj.FkDate = tmp; };
					}
					break;
				case "remarks": {
						obj.Remarks = value;
					}
					break;
				case "contractversion": {
						long tmp;
						if( long.TryParse(value, out tmp) ) { obj.ContractVersion = tmp; }
					}
					break;
				case "approvestate": {
						obj.ApproveState = value;
					}
					break;
			}
		}
		private static void XmlToPrimaryKey(SmokingTest.CS.Entity.SDyContract obj, XmlReader reader)
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
		private static void XmlToSingle(SmokingTest.CS.Entity.SDyContract obj, XmlReader reader)
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
		/// <summary>
		/// XmlToSingle
		/// </summary>
		/// <param name="xml">xml</param>
		/// <returns></returns>
		public static object XmlToSingle(string xml)
		{
			SmokingTest.CS.Entity.SDyContract obj = null;
			using( StringReader sr = new StringReader(xml) ) {
				using( XmlReader reader = XmlTextReader.Create(sr) ) {
					if( reader.ReadToFollowing("s_DyContract") ) {
						obj = new SmokingTest.CS.Entity.SDyContract();
						XmlToPrimaryKey(obj, reader);
						XmlToSingle(obj, reader);
					}
					else {
						throw new InvalidOperationException("xml中不存s_DyContract节点");
					}
				}
			}
			return obj;
		}
		/// <summary>
		/// XmlToList
		/// </summary>
		/// <param name="xml">xml</param>
		/// <returns></returns>
		public static object XmlToList(string xml)
		{
			List<SmokingTest.CS.Entity.SDyContract> list = new List<SmokingTest.CS.Entity.SDyContract>();
			using( StringReader sr = new StringReader(xml) ) {
				using( XmlReader reader = XmlTextReader.Create(sr) ) {
					if( reader.ReadToFollowing("s_DyContract") ) {
						SmokingTest.CS.Entity.SDyContract obj = new SmokingTest.CS.Entity.SDyContract();
						XmlToPrimaryKey(obj, reader);
						XmlToSingle(obj, reader);
						list.Add(obj);
						while( reader.ReadToNextSibling("s_DyContract") ) {
							obj = new SmokingTest.CS.Entity.SDyContract();
							XmlToPrimaryKey(obj, reader);
							XmlToSingle(obj, reader);
							list.Add(obj);
						}
					}
					else {
						throw new InvalidOperationException("xml中不存s_DyContract节点");
					}
				}
			}
			return list;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="src"></param>
		/// <returns></returns>
		public static object CloneMe(object src)
		{
			SmokingTest.CS.Entity.SDyContract entity = (SmokingTest.CS.Entity.SDyContract)src;
			SmokingTest.CS.Entity.SDyContract obj = new SmokingTest.CS.Entity.SDyContract();
			obj.ContractGUID = entity.ContractGUID;
			obj.DyContractNo = entity.DyContractNo;
			obj.DyDate = entity.DyDate;
			obj.JkContractNo = entity.JkContractNo;
			obj.JkBank = entity.JkBank;
			obj.JkAmount = entity.JkAmount;
			obj.Pgcompany = entity.Pgcompany;
			obj.PgAmount = entity.PgAmount;
			obj.FkDate = entity.FkDate;
			obj.Remarks = entity.Remarks;
			obj.ContractVersion = entity.ContractVersion;
			obj.ApproveState = entity.ApproveState;
			return obj;
		}
	}
}