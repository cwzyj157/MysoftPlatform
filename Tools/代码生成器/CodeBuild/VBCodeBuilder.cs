using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace 代码生成器
{
    public class VBCodeBuilder : CodeBuilder
    {
        public override BuildResult BuildCode(SqlObject obj)
        {
            if (obj.Type == ObjectType.Table)
            {
                string sql;
                DataTable dt = GetSchema(obj, out sql);

                BuildResult code = new BuildResult();
                code.SQL = sql;
                if (_autoComplete)
                {
                    code.Code = TableToVBAutoCompleteProperty(dt, obj);
                }
                else
                {
                    code.Code = TableToVBProperty(dt, obj);
                }
                
                return code;
            }
            if (obj.Type == ObjectType.View)
            {
                string sql;
                DataTable dt = GetSchema(obj, out sql);

                BuildResult code = new BuildResult();
                code.SQL = sql;
                if (_autoComplete)
                {
                    code.Code = ViewToVBAutoCompleteProperty(dt, obj);
                }
                else
                {
                    code.Code = ViewToVBProperty(dt, obj);
                }
                

                return code;
            }
            if (obj.Type == ObjectType.SQL)
            {

                string sql;
                DataTable dt = GetSchema(obj, out sql);
                StringBuilder sb = new StringBuilder();

                sb.AppendFormat("<Serializable()> _{0}", Environment.NewLine);
                sb.AppendFormat("<DataEntity(Alias:=\"{0}\")> _{1}", "xxxxxxxx", Environment.NewLine);
                sb.AppendFormat("Public Partial Class Entity xxxxxxxx Inherits BaseEntity {0}", Environment.NewLine);
                foreach (DataColumn col in dt.Columns)
                {
                    sb.AppendFormat("    <DataColumn(Alias:=\"{0}\")>{1}", col.ColumnName, Environment.NewLine);
                    sb.AppendFormat("    Public Property {0} As {1}{2}", CleanName(col.ColumnName), TypeUtil.TypeToVB(col.DataType), Environment.NewLine);
                }
                sb.Append("End Class");

                BuildResult code = new BuildResult();
                code.SQL = sql;
                code.Code = sb.ToString();
                return code;

            }
            if (obj.Type == ObjectType.StoreProcedure)
            {
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = obj.Name;
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        SqlCommandBuilder.DeriveParameters(cmd);

                        BuildResult code = new BuildResult();
                        code.SQL = "exec " + cmd.CommandText;
                        code.Code = SPToCode(cmd, obj);

                        return code;
                    }
                }
            }
            return null;
        }


        private static string TableToVBAutoCompleteProperty(DataTable dt, SqlObject obj)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<Serializable()> _{0}", Environment.NewLine);


            string cleanName = CleanName(obj.Name);
            if (cleanName.ToLower() != obj.Name.ToLower())
                sb.AppendFormat("<DataEntity(Alias:=\"{0}\")> _{1}", obj.Name, Environment.NewLine);

            sb.AppendFormat("Public Partial Class {0}     {1}    Inherits BaseEntity {1}", cleanName, Environment.NewLine);
            foreach (DataRow row in dt.Rows)
            {
                cleanName = CleanName(row["Name"].ToString());

                List<string> attrs = new List<string>();

                if (cleanName.ToLower() != row["Name"].ToString().ToLower())
                {
                    attrs.Add("Alias:=\"" + cleanName + "\"");
                }
                if ((bool)row["InPrimaryKey"])
                {
                    attrs.Add("PrimaryKey:=True");
                }
                if (row["DataType"].ToString() == "timestamp")
                {
                    attrs.Add("TimeStamp:=True");
                }
                if (row["IsIdentity"].ToString() == "1")
                {
                    attrs.Add("Identity:=True");
                }
                if (row["DefaultValue"].ToString().IndexOf("NewSequentialId", StringComparison.OrdinalIgnoreCase) != -1)
                {
                    attrs.Add("SeqGuid:=True");
                }
                if (row["isnullable"].ToString() == "1")
                {
                    attrs.Add("IsNullable:=true");
                }

                if (attrs.Count > 0)
                {
                    bool bDone = true;
                    sb.AppendFormat("    <DataColumn(");
                    foreach (string attr in attrs)
                    {
                        if (bDone)
                        {
                            bDone = false;
                            sb.AppendFormat(attr);
                        }
                        else
                        {
                            sb.AppendFormat(", {0}", attr);
                        }
                    }
                    sb.AppendFormat(")>{0}", Environment.NewLine);
                }

                string typeName = TypeUtil.SqlTypeToVB(row["DataType"].ToString());
                if (row["isnullable"].ToString() == "0")
                {
                    typeName = typeName.Replace("?", "");
                }

                sb.AppendFormat("    Public Property {0} As {1}{2}", cleanName, typeName, Environment.NewLine);
            }
            sb.Append("End Class");
            return sb.ToString();
        }

        private static string TableToVBProperty(DataTable dt, SqlObject obj)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<Serializable()> _{0}", Environment.NewLine);

            string cleanName = CleanName(obj.Name);
            if (cleanName.ToLower() != obj.Name.ToLower())
                sb.AppendFormat("<DataEntity(Alias:=\"{0}\")> _{1}", obj.Name, Environment.NewLine);

            sb.AppendFormat("Public Partial Class {0}     {1}    Inherits BaseEntity {1}", cleanName, Environment.NewLine);
            sb.Append(Environment.NewLine);
            foreach (DataRow row in dt.Rows)
            {
                cleanName = CleanName(row["Name"].ToString());
                cleanName = char.ToLower(cleanName[0]) + cleanName.Remove(0, 1);

                string typeName = TypeUtil.SqlTypeToVB(row["DataType"].ToString());
                if (row["isnullable"].ToString() == "0")
                {
                    typeName = typeName.Replace("?", "");
                }
                sb.AppendFormat("    Private _{0} As {1}{2}", cleanName, typeName, Environment.NewLine);

            }
            sb.Append(Environment.NewLine);
            foreach (DataRow row in dt.Rows)
            {
                cleanName = CleanName(row["Name"].ToString());
                string fieldName = char.ToLower(cleanName[0]) + cleanName.Remove(0, 1);

                List<string> attrs = new List<string>();

                if (cleanName.ToLower() != row["Name"].ToString().ToLower())
                {
                    attrs.Add("Alias:=\"" + cleanName + "\"");
                }
                if ((bool)row["InPrimaryKey"])
                {
                    attrs.Add("PrimaryKey:=True");
                }
                if (row["DataType"].ToString() == "timestamp")
                {
                    attrs.Add("TimeStamp:=True");
                }
                if (row["IsIdentity"].ToString() == "1")
                {
                    attrs.Add("Identity:=True");
                }
                if (row["DefaultValue"].ToString().IndexOf("NewSequentialId", StringComparison.OrdinalIgnoreCase) != -1)
                {
                    attrs.Add("SeqGuid:=True");
                }
                if (row["isnullable"].ToString() == "1")
                {
                    attrs.Add("IsNullable:=true");
                }

                if (attrs.Count > 0)
                {
                    bool bDone = true;
                    sb.AppendFormat("    <DataColumn(");
                    foreach (string attr in attrs)
                    {
                        if (bDone)
                        {
                            bDone = false;
                            sb.AppendFormat(attr);
                        }
                        else
                        {
                            sb.AppendFormat(", {0}", attr);
                        }
                    }
                    sb.AppendFormat(")>{0}", Environment.NewLine);
                }

                string typeName = TypeUtil.SqlTypeToVB(row["DataType"].ToString());
                if (row["isnullable"].ToString() == "0")
                {
                    typeName = typeName.Replace("?", "");
                }

                sb.AppendFormat("    Public Property {0} As {1}{2}", cleanName, typeName, Environment.NewLine);
                sb.AppendFormat("        Get{0}", Environment.NewLine);
                sb.AppendFormat("            Return _{0}{1}", fieldName, Environment.NewLine);
                sb.AppendFormat("        End Get{0}", Environment.NewLine);
                sb.AppendFormat("        Set(value As {0}){1}", typeName, Environment.NewLine);
                sb.AppendFormat("            _{0} = value{1}", fieldName, Environment.NewLine);
                sb.AppendFormat("        End Set{0}", Environment.NewLine);
                sb.AppendFormat("    End Property{0}", Environment.NewLine);
            }
            sb.Append("End Class");
            return sb.ToString();
        }

        private static string ViewToVBAutoCompleteProperty(DataTable dt, SqlObject obj)
        {
            StringBuilder sb = new StringBuilder();
            string cleanName = CleanName(obj.Name);
            if (cleanName.ToLower() != obj.Name.ToLower())
            {
                sb.AppendFormat("<Serializable()> _{0}", Environment.NewLine);
                sb.AppendFormat("<DataEntity(Alias:=\"{0}\")> _{1}", obj.Name, Environment.NewLine);
            }
            sb.AppendFormat("Public Partial Class Entity {0}{1}    Inherits BaseEntity {1}", cleanName, Environment.NewLine);
            foreach (DataColumn col in dt.Columns)
            {
                cleanName = CleanName(col.ColumnName);
                if (cleanName.ToLower() != col.ColumnName.ToLower())
                {
                    sb.AppendFormat("    <DataColumn(Alias:=\"{0}\")>{1}", col.ColumnName, Environment.NewLine);
                }
                sb.AppendFormat("    Public Property {0} As {1}{2}", CleanName(col.ColumnName), TypeUtil.TypeToVB(col.DataType), Environment.NewLine);
            }
            sb.Append("End Class");
            return sb.ToString();
        }

        private static string ViewToVBProperty(DataTable dt, SqlObject obj)
        {
            StringBuilder sb = new StringBuilder();
            string cleanName = CleanName(obj.Name);
            if (cleanName.ToLower() != obj.Name.ToLower())
            {
                sb.AppendFormat("<Serializable()> _{0}", Environment.NewLine);
                sb.AppendFormat("<DataEntity(Alias:=\"{0}\")> _{1}", obj.Name, Environment.NewLine);
            }
            sb.AppendFormat("Public Partial Class Entity {0}{1}    Inherits BaseEntity {1}", cleanName, Environment.NewLine);
            sb.Append(Environment.NewLine);
            foreach (DataColumn col in dt.Columns)
            {
                cleanName = CleanName(col.ColumnName);
                cleanName = char.ToLower(cleanName[0]) + cleanName.Remove(0, 1);
                string typeName = TypeUtil.TypeToVB(col.DataType);
                sb.AppendFormat("    Private _{0} As {1}{2}", cleanName, typeName, Environment.NewLine);
            }
            sb.Append(Environment.NewLine);
            foreach (DataColumn col in dt.Columns)
            {
                cleanName = CleanName(col.ColumnName);
                string fieldName = char.ToLower(cleanName[0]) + cleanName.Remove(0, 1);
                string typeName = TypeUtil.TypeToVB(col.DataType);
                if (cleanName.ToLower() != col.ColumnName.ToLower())
                {
                    sb.AppendFormat("    <DataColumn(Alias:=\"{0}\")>{1}", col.ColumnName, Environment.NewLine);
                }
                sb.AppendFormat("    Public Property {0} As {1}{2}", cleanName, typeName, Environment.NewLine);
                sb.AppendFormat("        Get{0}", Environment.NewLine);
                sb.AppendFormat("            Return _{0}{1}", fieldName, Environment.NewLine);
                sb.AppendFormat("        End Get{0}", Environment.NewLine);
                sb.AppendFormat("        Set(value As {0}){1}", typeName, Environment.NewLine);
                sb.AppendFormat("            _{0} = value{1}", fieldName, Environment.NewLine);
                sb.AppendFormat("        End Set{0}", Environment.NewLine);
                sb.AppendFormat("    End Property{0}", Environment.NewLine);
            }
            sb.Append("End Class");
            return sb.ToString();
        }

        private static string SPToCode(SqlCommand cmd, SqlObject obj)
        {
            StringBuilder sb = new StringBuilder();

            foreach (SqlParameter p in cmd.Parameters)
            {
                if (p.Direction != ParameterDirection.ReturnValue)
                {
                    sb.AppendFormat("' {0} {1}\r\n", p.ParameterName, p.SqlDbType.ToString());
                }
            }

            sb.Append("Dim parameter = New With {");
            bool bDone = true;
            int count = 0;
            foreach (SqlParameter p in cmd.Parameters)
            {
                if (p.Direction != ParameterDirection.ReturnValue)
                {
                    count++;
                    string name = p.ParameterName.Replace("@", "");
                    string newline = "";
                    if (count > 0 && cmd.Parameters.Count > 2)
                    {
                        newline = " _" + Environment.NewLine + "    ";
                    }
                    if (p.Direction == ParameterDirection.Output || p.Direction == ParameterDirection.InputOutput)
                    {
                        if (bDone)
                        {
                            sb.AppendFormat(" .{0} = CType(SPOut, {1})", name, "xxxxxxxx");
                            bDone = false;
                        }
                        else
                        {
                            sb.AppendFormat(",{0} .{1} = CType(SPOut, {2})", newline, name, "xxxxxxxx");
                        }
                    }
                    else
                    {
                        if (bDone)
                        {
                            sb.AppendFormat(" .{0} = {1}", name, "xxxxxxxx");
                            bDone = false;
                        }
                        else
                        {
                            sb.AppendFormat(",{0} .{1} = {2}", newline, name, "xxxxxxxx");
                        }
                    }

                }
            }
            sb.AppendFormat(" }}{0}{0}", Environment.NewLine);
            if (count == 0)
            {
                sb.Length = 0;
            }
            if (obj.Name.ToLower().IndexOf("get") != -1)
            {
                sb.AppendFormat("Dim list As List(Of xxxx) = StoreProcedure.Create(\"{0}\", {1}).ToList(Of xxxx)(){2}{2}", obj.Name, sb.Length == 0 ? "Nothing" : "parameter", Environment.NewLine);
                sb.AppendFormat("'Dim obj As xxxx = StoreProcedure.Create(\"{0}\", {1}).ToSingle(Of xxxx)(){2}{2}", obj.Name, sb.Length == 0 ? "Nothing" : "parameter", Environment.NewLine);
            }
            else
            {
                sb.AppendFormat("StoreProcedure.Create(\"{0}\", {1}).ExecuteNonQuery(){2}{2}", obj.Name, sb.Length == 0 ? "Nothing" : "parameter", Environment.NewLine);
            }
            return sb.ToString();
        }

        
    }
}
