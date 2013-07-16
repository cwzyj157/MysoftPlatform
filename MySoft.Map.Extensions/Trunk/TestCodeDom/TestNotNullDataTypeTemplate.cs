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
	public static class TestNotNullDataTypeTemplate
	{
		private static readonly TestCodeDom.TestNotNullDataTypeTable s_Empty = new TestCodeDom.TestNotNullDataTypeTable();

		public static object Execute(int flag, object[] parameters)
		{
			switch( flag ) {
				case 1:
					return DataReaderToList((SqlDataReader)parameters[0]);
				case 2:
					return DataReaderToSingle((SqlDataReader)parameters[0]);
				case 3:
					return Insert((TestCodeDom.TestNotNullDataTypeTable)parameters[0]);
				case 4:
					return Delete((TestCodeDom.TestNotNullDataTypeTable)parameters[0]);
				case 5:
					return ConcurrencyDelete_TimeStamp((TestCodeDom.TestNotNullDataTypeTable)parameters[0]);
				case 6:
					return ConcurrencyDelete_OriginalValue((TestCodeDom.TestNotNullDataTypeTable)parameters[0]);
				case 7:
					return Update((TestCodeDom.TestNotNullDataTypeTable)parameters[0], (TestCodeDom.TestNotNullDataTypeTable)parameters[1]);
				case 8:
					return ConcurrencyUpdate_TimeStamp((TestCodeDom.TestNotNullDataTypeTable)parameters[0], (TestCodeDom.TestNotNullDataTypeTable)parameters[1], (TestCodeDom.TestNotNullDataTypeTable)parameters[2]);
				case 9:
					return ConcurrencyUpdate_OriginalValue((TestCodeDom.TestNotNullDataTypeTable)parameters[0], (TestCodeDom.TestNotNullDataTypeTable)parameters[1], (TestCodeDom.TestNotNullDataTypeTable)parameters[2]);
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
		private static CPQuery UpdateSetFields(TestCodeDom.TestNotNullDataTypeTable t1, TestCodeDom.TestNotNullDataTypeTable t2)
		{
			bool changed = false;
			string[] zeroProperties = t1.GetZeroProperties();
			CPQuery query = CPQuery.Create() + "update [TestNotNullDataTypeTable] set ";

			if( t1.A != t2.A || Array.IndexOf(zeroProperties, "A") >= 0 ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [A] = " + (new SqlParameter("@A", t1.A));
			}
			if( t1.B != null ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [B] = " + (new SqlParameter("@B", (t1.B ?? (object)DBNull.Value)));
			}
			if( t1.C != t2.C || Array.IndexOf(zeroProperties, "C") >= 0 ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [C] = " + (new SqlParameter("@C", t1.C));
			}
			if( t1.D != t2.D ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [D] = " + (new SqlParameter("@D", (t1.D ?? (object)DBNull.Value)));
			}
			if( t1.E != t2.E || Array.IndexOf(zeroProperties, "E") >= 0 ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [E] = " + (new SqlParameter("@E", t1.E));
			}
			if( t1.F != t2.F || Array.IndexOf(zeroProperties, "F") >= 0 ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [F] = " + (new SqlParameter("@F", t1.F));
			}
			if( t1.G != t2.G || Array.IndexOf(zeroProperties, "G") >= 0 ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [G] = " + (new SqlParameter("@G", t1.G));
			}
			if( t1.H != t2.H || Array.IndexOf(zeroProperties, "H") >= 0 ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [H] = " + (new SqlParameter("@H", t1.H));
			}
			if( t1.I != null ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [I] = " + (new SqlParameter("@I", (t1.I ?? (object)DBNull.Value)));
			}
			if( t1.J != t2.J || Array.IndexOf(zeroProperties, "J") >= 0 ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [J] = " + (new SqlParameter("@J", t1.J));
			}
			if( t1.K != t2.K || Array.IndexOf(zeroProperties, "K") >= 0 ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [K] = " + (new SqlParameter("@K", t1.K));
			}
			if( t1.L != t2.L ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [L] = " + (new SqlParameter("@L", (t1.L ?? (object)DBNull.Value)));
			}
			if( t1.M != t2.M ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [M] = " + (new SqlParameter("@M", (t1.M ?? (object)DBNull.Value)));
			}
			if( t1.N != t2.N || Array.IndexOf(zeroProperties, "N") >= 0 ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [N] = " + (new SqlParameter("@N", t1.N));
			}
			if( t1.O != t2.O ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [O] = " + (new SqlParameter("@O", (t1.O ?? (object)DBNull.Value)));
			}
			if( t1.P != t2.P ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [P] = " + (new SqlParameter("@P", (t1.P ?? (object)DBNull.Value)));
			}
			if( t1.Q != t2.Q || Array.IndexOf(zeroProperties, "Q") >= 0 ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [Q] = " + (new SqlParameter("@Q", t1.Q));
			}
			if( t1.R != t2.R || Array.IndexOf(zeroProperties, "R") >= 0 ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [R] = " + (new SqlParameter("@R", t1.R));
			}
			if( t1.S != t2.S || Array.IndexOf(zeroProperties, "S") >= 0 ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [S] = " + (new SqlParameter("@S", t1.S));
			}
			if( t1.T != t2.T || Array.IndexOf(zeroProperties, "T") >= 0 ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [T] = " + (new SqlParameter("@T", t1.T));
			}
			if( t1.U != t2.U || Array.IndexOf(zeroProperties, "U") >= 0 ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [U] = " + (new SqlParameter("@U", t1.U));
			}
			if( t1.V != t2.V ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [V] = " + (new SqlParameter("@V", (t1.V ?? (object)DBNull.Value)));
			}
			if( t1.W != t2.W || Array.IndexOf(zeroProperties, "W") >= 0 ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [W] = " + (new SqlParameter("@W", t1.W));
			}
			if( t1.X != t2.X || Array.IndexOf(zeroProperties, "X") >= 0 ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [X] = " + (new SqlParameter("@X", t1.X));
			}
			if( t1.Y != t2.Y ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [Y] = " + (new SqlParameter("@Y", (t1.Y ?? (object)DBNull.Value)));
			}
			if( t1.Z != t2.Z ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [Z] = " + (new SqlParameter("@Z", (t1.Z ?? (object)DBNull.Value)));
			}
			if( t1.A1 != t2.A1 || Array.IndexOf(zeroProperties, "A1") >= 0 ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [A1] = " + (new SqlParameter("@A1", t1.A1));
			}
			if( t1.B1 != t2.B1 ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [B1] = " + (new SqlParameter("@B1", (t1.B1 ?? (object)DBNull.Value)));
			}
			if( t1.C1 != null ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [C1] = " + (new SqlParameter("@C1", (t1.C1 ?? (object)DBNull.Value)));
			}
			if( t1.D1 != t2.D1 || Array.IndexOf(zeroProperties, "D1") >= 0 ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [D1] = " + (new SqlParameter("@D1", t1.D1));
			}
			if( t1.F1 != t2.F1 || Array.IndexOf(zeroProperties, "F1") >= 0 ) {
				if( changed )
					query = query + " , ";
				else
					changed = true;
				query = query + " [F1] = " + (new SqlParameter("@F1", t1.F1));
			}
			if( changed == false )
				return null;

			else
				return query;
		}
		private static void WhereByPK(CPQuery query, TestCodeDom.TestNotNullDataTypeTable t1)
		{
			query = query + " where ";
			query = query + " [Guid] = " + (new SqlParameter("@Guid", t1.Guid));
		}
		private static void WhereByTimeStamp(CPQuery query, TestCodeDom.TestNotNullDataTypeTable t2)
		{
			query = query + " where ";
			query = query + " [Guid] = " + (new SqlParameter("@Guid", t2.Guid));
			query = query + " and ";
			query = query + " [E1] = " + (new SqlParameter("@original_E1", (t2.E1 ?? (object)DBNull.Value)));
		}
		private static void WhereByOriginalValue(CPQuery query, TestCodeDom.TestNotNullDataTypeTable t1, TestCodeDom.TestNotNullDataTypeTable t2)
		{
			string[] zeroProperties = t2.GetZeroProperties();
			query = query + " where ";
			query = query + " [Guid] = " + (new SqlParameter("@original_Guid", t2.Guid));
			if( t1.A != t2.A || Array.IndexOf(zeroProperties, "A") >= 0 ) {
				query = query + " and ";
				query = query + " ([A] = " + (new SqlParameter("@original_A", t2.A)) + ")";
			}
			if( t1.C != t2.C || Array.IndexOf(zeroProperties, "C") >= 0 ) {
				query = query + " and ";
				query = query + " ([C] = " + (new SqlParameter("@original_C", t2.C)) + ")";
			}
			if( t1.D != t2.D ) {
				query = query + " and ";
				query = query + " ([D] = " + (new SqlParameter("@original_D", (t2.D ?? (object)DBNull.Value))) + ")";
			}
			if( t1.E != t2.E || Array.IndexOf(zeroProperties, "E") >= 0 ) {
				query = query + " and ";
				query = query + " ([E] = " + (new SqlParameter("@original_E", t2.E)) + ")";
			}
			if( t1.F != t2.F || Array.IndexOf(zeroProperties, "F") >= 0 ) {
				query = query + " and ";
				query = query + " ([F] = " + (new SqlParameter("@original_F", t2.F)) + ")";
			}
			if( t1.G != t2.G || Array.IndexOf(zeroProperties, "G") >= 0 ) {
				query = query + " and ";
				query = query + " ([G] = " + (new SqlParameter("@original_G", t2.G)) + ")";
			}
			if( t1.H != t2.H || Array.IndexOf(zeroProperties, "H") >= 0 ) {
				query = query + " and ";
				query = query + " ([H] = " + (new SqlParameter("@original_H", t2.H)) + ")";
			}
			if( t1.J != t2.J || Array.IndexOf(zeroProperties, "J") >= 0 ) {
				query = query + " and ";
				query = query + " ([J] = " + (new SqlParameter("@original_J", t2.J)) + ")";
			}
			if( t1.K != t2.K || Array.IndexOf(zeroProperties, "K") >= 0 ) {
				query = query + " and ";
				query = query + " ([K] = " + (new SqlParameter("@original_K", t2.K)) + ")";
			}
			if( t1.L != t2.L ) {
				query = query + " and ";
				query = query + " ([L] = " + (new SqlParameter("@original_L", (t2.L ?? (object)DBNull.Value))) + ")";
			}
			if( t1.M != t2.M ) {
				query = query + " and ";
				query = query + " ([M] = " + (new SqlParameter("@original_M", (t2.M ?? (object)DBNull.Value))) + ")";
			}
			if( t1.N != t2.N || Array.IndexOf(zeroProperties, "N") >= 0 ) {
				query = query + " and ";
				query = query + " ([N] = " + (new SqlParameter("@original_N", t2.N)) + ")";
			}
			if( t1.O != t2.O ) {
				query = query + " and ";
				query = query + " ([O] = " + (new SqlParameter("@original_O", (t2.O ?? (object)DBNull.Value))) + ")";
			}
			if( t1.P != t2.P ) {
				query = query + " and ";
				query = query + " ([P] = " + (new SqlParameter("@original_P", (t2.P ?? (object)DBNull.Value))) + ")";
			}
			if( t1.Q != t2.Q || Array.IndexOf(zeroProperties, "Q") >= 0 ) {
				query = query + " and ";
				query = query + " ([Q] = " + (new SqlParameter("@original_Q", t2.Q)) + ")";
			}
			if( t1.R != t2.R || Array.IndexOf(zeroProperties, "R") >= 0 ) {
				query = query + " and ";
				query = query + " ([R] = " + (new SqlParameter("@original_R", t2.R)) + ")";
			}
			if( t1.S != t2.S || Array.IndexOf(zeroProperties, "S") >= 0 ) {
				query = query + " and ";
				query = query + " ([S] = " + (new SqlParameter("@original_S", t2.S)) + ")";
			}
			if( t1.T != t2.T || Array.IndexOf(zeroProperties, "T") >= 0 ) {
				query = query + " and ";
				query = query + " ([T] = " + (new SqlParameter("@original_T", t2.T)) + ")";
			}
			if( t1.U != t2.U || Array.IndexOf(zeroProperties, "U") >= 0 ) {
				query = query + " and ";
				query = query + " ([U] = " + (new SqlParameter("@original_U", t2.U)) + ")";
			}
			if( t1.V != t2.V ) {
				query = query + " and ";
				query = query + " ([V] = " + (new SqlParameter("@original_V", (t2.V ?? (object)DBNull.Value))) + ")";
			}
			if( t1.W != t2.W || Array.IndexOf(zeroProperties, "W") >= 0 ) {
				query = query + " and ";
				query = query + " ([W] = " + (new SqlParameter("@original_W", t2.W)) + ")";
			}
			if( t1.X != t2.X || Array.IndexOf(zeroProperties, "X") >= 0 ) {
				query = query + " and ";
				query = query + " ([X] = " + (new SqlParameter("@original_X", t2.X)) + ")";
			}
			if( t1.Y != t2.Y ) {
				query = query + " and ";
				query = query + " ([Y] = " + (new SqlParameter("@original_Y", (t2.Y ?? (object)DBNull.Value))) + ")";
			}
			if( t1.Z != t2.Z ) {
				query = query + " and ";
				query = query + " ([Z] = " + (new SqlParameter("@original_Z", (t2.Z ?? (object)DBNull.Value))) + ")";
			}
			if( t1.A1 != t2.A1 || Array.IndexOf(zeroProperties, "A1") >= 0 ) {
				query = query + " and ";
				query = query + " ([A1] = " + (new SqlParameter("@original_A1", t2.A1)) + ")";
			}
			if( t1.B1 != t2.B1 ) {
				query = query + " and ";
				query = query + " ([B1] = " + (new SqlParameter("@original_B1", (t2.B1 ?? (object)DBNull.Value))) + ")";
			}
			if( t1.D1 != t2.D1 || Array.IndexOf(zeroProperties, "D1") >= 0 ) {
				query = query + " and ";
				query = query + " ([D1] = " + (new SqlParameter("@original_D1", t2.D1)) + ")";
			}
			if( t1.F1 != t2.F1 || Array.IndexOf(zeroProperties, "F1") >= 0 ) {
				query = query + " and ";
				query = query + " ([F1] = " + (new SqlParameter("@original_F1", t2.F1)) + ")";
			}
		}
		public static CPQuery Insert(TestCodeDom.TestNotNullDataTypeTable t1)
		{
			TestCodeDom.TestNotNullDataTypeTable t2 = s_Empty;
			bool changed = false;
			string[] zeroProperties = t1.GetZeroProperties();
			List<SqlParameter> parameters = new List<SqlParameter>();
			StringBuilder sqlBuilder = new StringBuilder();
			StringBuilder sqlParams = new StringBuilder();
			sqlBuilder.Append("insert into [TestNotNullDataTypeTable] (");
			if( t1.A != t2.A || Array.IndexOf(zeroProperties, "A") >= 0 ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[A]");
				sqlParams.Append("@A");
				parameters.Add(new SqlParameter("@A", t1.A));
			}
			if( t1.B != null ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[B]");
				sqlParams.Append("@B");
				parameters.Add(new SqlParameter("@B", (t1.B ?? (object)DBNull.Value)));
			}
			if( t1.C != t2.C || Array.IndexOf(zeroProperties, "C") >= 0 ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[C]");
				sqlParams.Append("@C");
				parameters.Add(new SqlParameter("@C", t1.C));
			}
			if( t1.D != t2.D ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[D]");
				sqlParams.Append("@D");
				parameters.Add(new SqlParameter("@D", (t1.D ?? (object)DBNull.Value)));
			}
			if( t1.E != t2.E || Array.IndexOf(zeroProperties, "E") >= 0 ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[E]");
				sqlParams.Append("@E");
				parameters.Add(new SqlParameter("@E", t1.E));
			}
			if( t1.F != t2.F || Array.IndexOf(zeroProperties, "F") >= 0 ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[F]");
				sqlParams.Append("@F");
				parameters.Add(new SqlParameter("@F", t1.F));
			}
			if( t1.G != t2.G || Array.IndexOf(zeroProperties, "G") >= 0 ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[G]");
				sqlParams.Append("@G");
				parameters.Add(new SqlParameter("@G", t1.G));
			}
			if( t1.H != t2.H || Array.IndexOf(zeroProperties, "H") >= 0 ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[H]");
				sqlParams.Append("@H");
				parameters.Add(new SqlParameter("@H", t1.H));
			}
			if( t1.I != null ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[I]");
				sqlParams.Append("@I");
				parameters.Add(new SqlParameter("@I", (t1.I ?? (object)DBNull.Value)));
			}
			if( t1.J != t2.J || Array.IndexOf(zeroProperties, "J") >= 0 ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[J]");
				sqlParams.Append("@J");
				parameters.Add(new SqlParameter("@J", t1.J));
			}
			if( t1.K != t2.K || Array.IndexOf(zeroProperties, "K") >= 0 ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[K]");
				sqlParams.Append("@K");
				parameters.Add(new SqlParameter("@K", t1.K));
			}
			if( t1.L != t2.L ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[L]");
				sqlParams.Append("@L");
				parameters.Add(new SqlParameter("@L", (t1.L ?? (object)DBNull.Value)));
			}
			if( t1.M != t2.M ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[M]");
				sqlParams.Append("@M");
				parameters.Add(new SqlParameter("@M", (t1.M ?? (object)DBNull.Value)));
			}
			if( t1.N != t2.N || Array.IndexOf(zeroProperties, "N") >= 0 ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[N]");
				sqlParams.Append("@N");
				parameters.Add(new SqlParameter("@N", t1.N));
			}
			if( t1.O != t2.O ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[O]");
				sqlParams.Append("@O");
				parameters.Add(new SqlParameter("@O", (t1.O ?? (object)DBNull.Value)));
			}
			if( t1.P != t2.P ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[P]");
				sqlParams.Append("@P");
				parameters.Add(new SqlParameter("@P", (t1.P ?? (object)DBNull.Value)));
			}
			if( t1.Q != t2.Q || Array.IndexOf(zeroProperties, "Q") >= 0 ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[Q]");
				sqlParams.Append("@Q");
				parameters.Add(new SqlParameter("@Q", t1.Q));
			}
			if( t1.R != t2.R || Array.IndexOf(zeroProperties, "R") >= 0 ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[R]");
				sqlParams.Append("@R");
				parameters.Add(new SqlParameter("@R", t1.R));
			}
			if( t1.S != t2.S || Array.IndexOf(zeroProperties, "S") >= 0 ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[S]");
				sqlParams.Append("@S");
				parameters.Add(new SqlParameter("@S", t1.S));
			}
			if( t1.T != t2.T || Array.IndexOf(zeroProperties, "T") >= 0 ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[T]");
				sqlParams.Append("@T");
				parameters.Add(new SqlParameter("@T", t1.T));
			}
			if( t1.U != t2.U || Array.IndexOf(zeroProperties, "U") >= 0 ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[U]");
				sqlParams.Append("@U");
				parameters.Add(new SqlParameter("@U", t1.U));
			}
			if( t1.V != t2.V ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[V]");
				sqlParams.Append("@V");
				parameters.Add(new SqlParameter("@V", (t1.V ?? (object)DBNull.Value)));
			}
			if( t1.W != t2.W || Array.IndexOf(zeroProperties, "W") >= 0 ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[W]");
				sqlParams.Append("@W");
				parameters.Add(new SqlParameter("@W", t1.W));
			}
			if( t1.X != t2.X || Array.IndexOf(zeroProperties, "X") >= 0 ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[X]");
				sqlParams.Append("@X");
				parameters.Add(new SqlParameter("@X", t1.X));
			}
			if( t1.Y != t2.Y ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[Y]");
				sqlParams.Append("@Y");
				parameters.Add(new SqlParameter("@Y", (t1.Y ?? (object)DBNull.Value)));
			}
			if( t1.Z != t2.Z ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[Z]");
				sqlParams.Append("@Z");
				parameters.Add(new SqlParameter("@Z", (t1.Z ?? (object)DBNull.Value)));
			}
			if( t1.A1 != t2.A1 || Array.IndexOf(zeroProperties, "A1") >= 0 ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[A1]");
				sqlParams.Append("@A1");
				parameters.Add(new SqlParameter("@A1", t1.A1));
			}
			if( t1.B1 != t2.B1 ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[B1]");
				sqlParams.Append("@B1");
				parameters.Add(new SqlParameter("@B1", (t1.B1 ?? (object)DBNull.Value)));
			}
			if( t1.C1 != null ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[C1]");
				sqlParams.Append("@C1");
				parameters.Add(new SqlParameter("@C1", (t1.C1 ?? (object)DBNull.Value)));
			}
			if( t1.D1 != t2.D1 || Array.IndexOf(zeroProperties, "D1") >= 0 ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[D1]");
				sqlParams.Append("@D1");
				parameters.Add(new SqlParameter("@D1", t1.D1));
			}
			if( t1.F1 != t2.F1 || Array.IndexOf(zeroProperties, "F1") >= 0 ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[F1]");
				sqlParams.Append("@F1");
				parameters.Add(new SqlParameter("@F1", t1.F1));
			}
			if( t1.Guid != t2.Guid || Array.IndexOf(zeroProperties, "Guid") >= 0 ) {
				if( changed ) { sqlBuilder.Append(","); sqlParams.Append(","); }
				else
					changed = true;
				sqlBuilder.Append("[Guid]");
				sqlParams.Append("@Guid");
				parameters.Add(new SqlParameter("@Guid", t1.Guid));
			}
			if( changed == false )
				return null;
			sqlBuilder.Append(") values (").Append(sqlParams.ToString()).Append(")");
			CPQuery query = CPQuery.From(sqlBuilder.ToString(), parameters.ToArray());
			return query;
		}
		public static CPQuery Delete(TestCodeDom.TestNotNullDataTypeTable t1)
		{
			if( t1.Guid == s_Empty.Guid )
				throw new InvalidOperationException("没有为主键字段赋值：t1.Guid");
			CPQuery query = CPQuery.Create() + "delete from [TestNotNullDataTypeTable] ";
			WhereByPK(query, t1);
			return query;
		}
		public static CPQuery ConcurrencyDelete_TimeStamp(TestCodeDom.TestNotNullDataTypeTable t2)
		{
			if( t2.Guid == s_Empty.Guid )
				throw new InvalidOperationException("没有为主键字段赋值：t2.Guid");
			CPQuery query = CPQuery.Create() + "delete from [TestNotNullDataTypeTable] ";
			WhereByTimeStamp(query, t2);
			return query;
		}
		public static CPQuery ConcurrencyDelete_OriginalValue(TestCodeDom.TestNotNullDataTypeTable t2)
		{
			if( t2.Guid == s_Empty.Guid )
				throw new InvalidOperationException("没有为主键字段赋值：t2.Guid");
			TestCodeDom.TestNotNullDataTypeTable t1 = s_Empty;
			CPQuery query = CPQuery.Create() + "delete from [TestNotNullDataTypeTable] ";
			WhereByOriginalValue(query, t1, t2);
			return query;
		}
		public static CPQuery Update(TestCodeDom.TestNotNullDataTypeTable t1, TestCodeDom.TestNotNullDataTypeTable t3)
		{
			if( t1.Guid == s_Empty.Guid )
				throw new InvalidOperationException("没有为主键字段赋值：t1.Guid");
			TestCodeDom.TestNotNullDataTypeTable t2 = t3 ?? s_Empty;
			CPQuery query = UpdateSetFields(t1, t2);
			if( query == null )
				return null;
			WhereByPK(query, t1);
			return query;
		}
		public static CPQuery ConcurrencyUpdate_TimeStamp(TestCodeDom.TestNotNullDataTypeTable t1, TestCodeDom.TestNotNullDataTypeTable t2, TestCodeDom.TestNotNullDataTypeTable t3)
		{
			if( t2.Guid == s_Empty.Guid )
				throw new InvalidOperationException("没有为主键字段赋值：t2.Guid");
			CPQuery query = UpdateSetFields(t1, t3 ?? s_Empty);
			if( query == null )
				return null;
			WhereByTimeStamp(query, t2);
			return query;
		}
		public static CPQuery ConcurrencyUpdate_OriginalValue(TestCodeDom.TestNotNullDataTypeTable t1, TestCodeDom.TestNotNullDataTypeTable t2, TestCodeDom.TestNotNullDataTypeTable t3)
		{
			if( t2.Guid == s_Empty.Guid )
				throw new InvalidOperationException("没有为主键字段赋值：t2.Guid");
			CPQuery query = UpdateSetFields(t1, t3 ?? s_Empty);
			if( query == null )
				return null;
			WhereByOriginalValue(query, s_Empty, t2);
			return query;
		}
		private static void SetEntityProperty(TestCodeDom.TestNotNullDataTypeTable obj, string columnName, object value)
		{
			if( value == DBNull.Value || value == null ) {
				return;
			}
			switch( columnName ) {
				case "a":
					obj.A = (long)value;
					break;
				case "b":
					obj.B = (byte[])value;
					break;
				case "c":
					obj.C = (bool)value;
					break;
				case "d":
					obj.D = value.ToString();
					break;
				case "e":
					obj.E = (DateTime)value;
					break;
				case "f":
					obj.F = (DateTime)value;
					break;
				case "g":
					obj.G = (decimal)value;
					break;
				case "h":
					obj.H = (double)value;
					break;
				case "i":
					obj.I = (byte[])value;
					break;
				case "j":
					obj.J = (int)value;
					break;
				case "k":
					obj.K = (decimal)value;
					break;
				case "l":
					obj.L = value.ToString();
					break;
				case "m":
					obj.M = value.ToString();
					break;
				case "n":
					obj.N = (decimal)value;
					break;
				case "o":
					obj.O = value.ToString();
					break;
				case "p":
					obj.P = value.ToString();
					break;
				case "q":
					obj.Q = (float)value;
					break;
				case "r":
					obj.R = (DateTime)value;
					break;
				case "s":
					obj.S = (short)value;
					break;
				case "t":
					obj.T = (short)value;
					break;
				case "u":
					obj.U = (decimal)value;
					break;
				case "v":
					obj.V = value.ToString();
					break;
				case "w":
					obj.W = (byte)value;
					break;
				case "x":
					obj.X = (Guid)value;
					break;
				case "y":
					obj.Y = value.ToString();
					break;
				case "z":
					obj.Z = value.ToString();
					break;
				case "a1":
					obj.A1 = (DateTimeOffset)value;
					break;
				case "b1":
					obj.B1 = value.ToString();
					break;
				case "c1":
					obj.C1 = (byte[])value;
					break;
				case "d1":
					obj.D1 = (DateTime)value;
					break;
				case "e1":
					obj.E1 = value.GetType() == typeof(long) ? CodeUtil.LongToByte((long)value) : (byte[])value;
					break;
				case "f1":
					obj.F1 = (TimeSpan)value;
					break;
				case "guid":
					obj.Guid = (Guid)value;
					break;
			}
		}
		public static object DataReaderToList(SqlDataReader dr)
		{
			List<TestCodeDom.TestNotNullDataTypeTable> list = new List<TestCodeDom.TestNotNullDataTypeTable>();
			string[] columnNames = CodeUtil.GetColumnNames(dr);
			while( dr.Read() ) {
				TestCodeDom.TestNotNullDataTypeTable obj = new TestCodeDom.TestNotNullDataTypeTable();
				for( int i = 0; i < columnNames.Length; i++ ) {
					SetEntityProperty(obj, columnNames[i], dr.GetValue(i));
				}
				list.Add(obj);
			}
			return list;
		}
		public static object DataReaderToSingle(SqlDataReader dr)
		{
			TestCodeDom.TestNotNullDataTypeTable obj = null;
			string[] columnNames = CodeUtil.GetColumnNames(dr);
			if( dr.Read() ) {
				obj = new TestCodeDom.TestNotNullDataTypeTable();
				for( int i = 0; i < columnNames.Length; i++ ) {
					SetEntityProperty(obj, columnNames[i], dr.GetValue(i));
				}
			}
			return obj;
		}
		public static object DataTableToList(DataTable table)
		{
			List<TestCodeDom.TestNotNullDataTypeTable> list = new List<TestCodeDom.TestNotNullDataTypeTable>(table.Rows.Count);
			string[] columnNames = CodeUtil.GetColumnNames(table);
			foreach( DataRow row in table.Rows ) {
				TestCodeDom.TestNotNullDataTypeTable obj = new TestCodeDom.TestNotNullDataTypeTable();
				for( int i = 0; i < columnNames.Length; i++ ) {
					string columnName = columnNames[i];
					SetEntityProperty(obj, columnName, row[columnName]);
				}
				list.Add(obj);
			}
			return list;
		}
		private static void XmlToProperty(TestCodeDom.TestNotNullDataTypeTable obj, string columnName, string value)
		{
			if( string.IsNullOrEmpty(value) ) { return; }
			switch( columnName ) {
				case "a": {
						long tmp;
						if( long.TryParse(value, out tmp) ) { obj.A = tmp; }
					}
					break;
				case "b": {
						obj.B = Convert.FromBase64String(value);
					}
					break;
				case "c": {
						bool tmp;
						if( bool.TryParse(value, out tmp) ) { obj.C = tmp; }
					}
					break;
				case "d": {
						obj.D = value;
					}
					break;
				case "e": {
						DateTime tmp;
						if( DateTime.TryParse(value, out tmp) ) { obj.E = tmp; };
					}
					break;
				case "f": {
						DateTime tmp;
						if( DateTime.TryParse(value, out tmp) ) { obj.F = tmp; };
					}
					break;
				case "g": {
						decimal tmp;
						if( decimal.TryParse(value, out tmp) ) { obj.G = tmp; }
					}
					break;
				case "h": {
						double tmp;
						if( double.TryParse(value, out tmp) ) { obj.H = tmp; }
					}
					break;
				case "i": {
						obj.I = Convert.FromBase64String(value);
					}
					break;
				case "j": {
						int tmp;
						if( int.TryParse(value, out tmp) ) { obj.J = tmp; }
					}
					break;
				case "k": {
						decimal tmp;
						if( decimal.TryParse(value, out tmp) ) { obj.K = tmp; }
					}
					break;
				case "l": {
						obj.L = value;
					}
					break;
				case "m": {
						obj.M = value;
					}
					break;
				case "n": {
						decimal tmp;
						if( decimal.TryParse(value, out tmp) ) { obj.N = tmp; }
					}
					break;
				case "o": {
						obj.O = value;
					}
					break;
				case "p": {
						obj.P = value;
					}
					break;
				case "q": {
						float tmp;
						if( float.TryParse(value, out tmp) ) { obj.Q = tmp; }
					}
					break;
				case "r": {
						DateTime tmp;
						if( DateTime.TryParse(value, out tmp) ) { obj.R = tmp; };
					}
					break;
				case "s": {
						short tmp;
						if( short.TryParse(value, out tmp) ) { obj.S = tmp; }
					}
					break;
				case "t": {
						short tmp;
						if( short.TryParse(value, out tmp) ) { obj.T = tmp; }
					}
					break;
				case "u": {
						decimal tmp;
						if( decimal.TryParse(value, out tmp) ) { obj.U = tmp; }
					}
					break;
				case "v": {
						obj.V = value;
					}
					break;
				case "w": {
						byte tmp;
						if( byte.TryParse(value, out tmp) ) { obj.W = tmp; }
					}
					break;
				case "x": {
						obj.X = new Guid(value);
					}
					break;
				case "y": {
						obj.Y = value;
					}
					break;
				case "z": {
						obj.Z = value;
					}
					break;
				case "a1": {
						DateTimeOffset tmp;
						if( DateTimeOffset.TryParse(value, out tmp) ) { obj.A1 = tmp; };
					}
					break;
				case "b1": {
						obj.B1 = value;
					}
					break;
				case "c1": {
						obj.C1 = Convert.FromBase64String(value);
					}
					break;
				case "d1": {
						DateTime tmp;
						if( DateTime.TryParse(value, out tmp) ) { obj.D1 = tmp; };
					}
					break;
				case "e1": {
						long tmp;
						if( long.TryParse(value, out tmp) ) { obj.E1 = CodeUtil.LongToByte(tmp); }
					}
					break;
				case "f1": {
						TimeSpan tmp;
						if( TimeSpan.TryParse(value, out tmp) ) { obj.F1 = tmp; };
					}
					break;
				case "guid": {
						obj.Guid = new Guid(value);
					}
					break;
			}
		}
		private static void XmlToPrimaryKey(TestCodeDom.TestNotNullDataTypeTable obj, XmlReader reader)
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
		private static void XmlToSingle(TestCodeDom.TestNotNullDataTypeTable obj, XmlReader reader)
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
			TestCodeDom.TestNotNullDataTypeTable obj = null;
			using( StringReader sr = new StringReader(xml) ) {
				using( XmlReader reader = XmlTextReader.Create(sr) ) {
					if( reader.ReadToFollowing("TestNotNullDataTypeTable") ) {
						obj = new TestCodeDom.TestNotNullDataTypeTable();
						XmlToPrimaryKey(obj, reader);
						XmlToSingle(obj, reader);
					}
					else {
						throw new InvalidOperationException("xml中不存TestNotNullDataTypeTable节点");
					}
				}
			}
			return obj;
		}
		public static object XmlToList(string xml)
		{
			List<TestCodeDom.TestNotNullDataTypeTable> list = new List<TestCodeDom.TestNotNullDataTypeTable>();
			using( StringReader sr = new StringReader(xml) ) {
				using( XmlReader reader = XmlTextReader.Create(sr) ) {
					if( reader.ReadToFollowing("TestNotNullDataTypeTable") ) {
						TestCodeDom.TestNotNullDataTypeTable obj = new TestCodeDom.TestNotNullDataTypeTable();
						XmlToPrimaryKey(obj, reader);
						XmlToSingle(obj, reader);
						list.Add(obj);
						while( reader.ReadToNextSibling("TestNotNullDataTypeTable") ) {
							obj = new TestCodeDom.TestNotNullDataTypeTable();
							XmlToPrimaryKey(obj, reader);
							XmlToSingle(obj, reader);
							list.Add(obj);
						}
					}
					else {
						throw new InvalidOperationException("xml中不存TestNotNullDataTypeTable节点");
					}
				}
			}
			return list;
		}
		public static object CloneMe(object src)
		{
			TestCodeDom.TestNotNullDataTypeTable entity = (TestCodeDom.TestNotNullDataTypeTable)src;
			TestCodeDom.TestNotNullDataTypeTable obj = new TestCodeDom.TestNotNullDataTypeTable();
			obj.A = entity.A;
			obj.B = entity.B;
			obj.C = entity.C;
			obj.D = entity.D;
			obj.E = entity.E;
			obj.F = entity.F;
			obj.G = entity.G;
			obj.H = entity.H;
			obj.I = entity.I;
			obj.J = entity.J;
			obj.K = entity.K;
			obj.L = entity.L;
			obj.M = entity.M;
			obj.N = entity.N;
			obj.O = entity.O;
			obj.P = entity.P;
			obj.Q = entity.Q;
			obj.R = entity.R;
			obj.S = entity.S;
			obj.T = entity.T;
			obj.U = entity.U;
			obj.V = entity.V;
			obj.W = entity.W;
			obj.X = entity.X;
			obj.Y = entity.Y;
			obj.Z = entity.Z;
			obj.A1 = entity.A1;
			obj.B1 = entity.B1;
			obj.C1 = entity.C1;
			obj.D1 = entity.D1;
			obj.E1 = entity.E1;
			obj.F1 = entity.F1;
			obj.Guid = entity.Guid;
			return obj;
		}
	}
}