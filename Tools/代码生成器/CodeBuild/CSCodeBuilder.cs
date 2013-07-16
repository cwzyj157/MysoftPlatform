using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;


namespace 代码生成器
{
    /// <summary>
    /// C#代码生成
    /// </summary>
    public class CSCodeBuilder : CodeBuilder
    {
        public override BuildResult BuildCode(SqlObject obj)
        {
            if (obj.Type == ObjectType.Table)
            {
                string sql;
                DataTable dt = GetSchema(obj, out sql);

                BuildResult code = new BuildResult();
                code.SQL = sql;
                code.Code = TableToCSProperty(dt, obj);
                return code;
            }

            if (obj.Type == ObjectType.View)
            {

                string sql;
                DataTable dt = GetSchema(obj, out sql);

                BuildResult code = new BuildResult();
                code.SQL = sql;

                code.Code = ViewToCSProperty(dt, obj);
                return code;
            }

            if (obj.Type == ObjectType.SQL)
            {
                string sql;
                DataTable dt = GetSchema(obj, out sql);
                StringBuilder sb = new StringBuilder();
                
                sb.AppendFormat("[Serializable]{0}", Environment.NewLine);
                sb.AppendFormat("[DataEntity(Alias=\"{0}\")]{1}", "xxxxxxxx", Environment.NewLine);
                sb.AppendFormat("public class xxxxxxxx : BaseEntity{ {0}", Environment.NewLine);
                foreach (DataColumn col in dt.Columns)
                {
                    sb.AppendFormat("    [DataColumn(Alias=\"{0}\")]{1}", col.ColumnName, Environment.NewLine);
                    sb.AppendFormat("    public {1} {0} {{get;set;}}", CleanName(col.ColumnName), TypeUtil.TypeToVB(col.DataType), Environment.NewLine);
                }
                sb.Append("}");
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

                        code.Code = SPToCS(cmd, obj);

                        return code;
                    }
                }
            }
            return null;
        }

        private static string TableToCSProperty(DataTable dt, SqlObject obj)
        {
            bool bPrimaryKey = false;
            StringBuilder sb = new StringBuilder();
            string cleanName = CleanName(obj.Name);
            sb.AppendFormat("[Serializable]{0}", Environment.NewLine);
            if (cleanName.ToLower() != obj.Name.ToLower())
            {
                sb.AppendFormat("[DataEntity(Alias=\"{0}\")]{1}", obj.Name, Environment.NewLine);
            }
            sb.AppendFormat("public partial class {0} : BaseEntity {{{1}", cleanName, Environment.NewLine);
            foreach (DataRow row in dt.Rows)
            {
                cleanName = CleanName(row["Name"].ToString());

                List<string> attrs = new List<string>();
                bool bTimeStamp = false;

                if (cleanName.ToLower() != row["Name"].ToString().ToLower())
                {
                    attrs.Add("Alias=\"" + row["Name"].ToString() + "\"");
                }
                if ((bool)row["InPrimaryKey"])
                {
                    bPrimaryKey = true;
                    attrs.Add("PrimaryKey=true");
                }
                if (row["DataType"].ToString() == "timestamp")
                {
                    bTimeStamp = true;
                    attrs.Add("TimeStamp=true");
                }
                if (row["IsIdentity"].ToString() == "1")
                {
                    attrs.Add("Identity=true");
                }
                if (row["DefaultValue"].ToString().IndexOf("NewSequentialId", StringComparison.OrdinalIgnoreCase) != -1)
                {
                    attrs.Add("SeqGuid=true");
                }
                if (row["isnullable"].ToString() == "1" && bTimeStamp == false)
                {
                    attrs.Add("IsNullable=true");
                }
                //if( row["DefaultValue"].ToString().Length > 0 ) {
                //    attrs.Add("DefaultValue=\"" + row["DefaultValue"].ToString().Replace("\"", "\\\"") + "\"");
                //}

                
                object objComment = row["Comment"];
                if (objComment != null && objComment != DBNull.Value)
                {
                    sb.AppendLine("    ///<summary>");
                    sb.AppendLine("    /// " + objComment.ToString());
                    sb.AppendLine("    ///</summary>");
                }

                if (attrs.Count > 0)
                {
                    bool bDone = true;
                    sb.AppendFormat("    [DataColumn(");
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
                    sb.AppendFormat(")]{0}", Environment.NewLine);
                }
                else
                {
                    sb.AppendLine("    [DataColumn]");
                }

                string typeName;

                if (bTimeStamp)
                {
                    typeName = "long";
                    
                }
                else
                {
                    typeName = TypeUtil.SqlTypeToCS(row["DataType"].ToString());
                }
                if (row["isnullable"].ToString() == "0")
                {
                    typeName = typeName.Replace("?", "");
                }

                sb.AppendFormat("    public {1} {0} {{ get; set; }}{2}", cleanName, typeName, Environment.NewLine);
            }
            sb.Append("}");
            if (bPrimaryKey)
            {
                return sb.ToString();
            }
            else
            {
                throw new PrimaryKeyNotFoundException();
            }
            
        }

        private static string ViewToCSProperty(DataTable dt, SqlObject obj)
        {
            StringBuilder sb = new StringBuilder();
            string cleanName = CleanName(obj.Name);
            sb.AppendFormat("[Serializable]{0}", Environment.NewLine);
            if (cleanName.ToLower() != obj.Name.ToLower())
            {
                sb.AppendFormat("[DataEntity(Alias=\"{0}\")]{1}", obj.Name, Environment.NewLine);
            }
            sb.AppendFormat("public partial class {0} : BaseEntity {{{1}", cleanName, Environment.NewLine);
            foreach (DataColumn col in dt.Columns)
            {
                cleanName = CleanName(col.ColumnName);
                if (cleanName.ToLower() != col.ColumnName.ToLower())
                {
                    sb.AppendFormat("    [DataColumn(Alias:=\"{0}\")]{1}", col.ColumnName, Environment.NewLine);
                }
                sb.AppendFormat("    public {1} {0} {{ get; set;}}{2}", CleanName(col.ColumnName), TypeUtil.TypeToCS(col.DataType), Environment.NewLine);
            }
            sb.Append("}");
            return sb.ToString();
        }

        private static string SPToCS(SqlCommand cmd, SqlObject obj)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("var parameter = new {");
            bool bDone = true;
            int count = 0;
            foreach (SqlParameter p in cmd.Parameters)
            {
                if (p.Direction != ParameterDirection.ReturnValue)
                {
                    count++;
                    string output = "";
                    string newline = "";
                    if (count > 0 && cmd.Parameters.Count > 2)
                    {
                        newline = Environment.NewLine + "    ";
                    }
                    if (p.Direction == ParameterDirection.Output || p.Direction == ParameterDirection.InputOutput)
                    {
                        output = "(SPOut)";
                    }
                    string name = p.ParameterName.Replace("@", "");
                    if (bDone)
                    {
                        sb.AppendFormat(" {0} = {2}{1} /* {3} */", name, "xxxxxxxx", output, p.SqlDbType.ToString());
                        bDone = false;
                    }
                    else
                    {
                        sb.AppendFormat(",{0} {1} = {3}{2} /* {4} */", newline, name, "xxxxxxxx", output, p.SqlDbType.ToString());
                    }

                }
            }
            sb.AppendFormat(" }};{0}{0}", Environment.NewLine);
            if (count == 0)
            {
                sb.Length = 0;
            }

            if (obj.Name.ToLower().IndexOf("get") != -1)
            {
                sb.AppendFormat("List<xxxx> list = StoreProcedure.Create(\"{0}\", {1}).ToList<xxxx>();{2}{2}", obj.Name, sb.Length == 0 ? "null" : "parameter", Environment.NewLine);
                sb.AppendFormat("//xxxx obj = StoreProcedure.Create(\"{0}\", {1}).ToSingle<xxxx>();{2}{2}", obj.Name, sb.Length == 0 ? "null" : "parameter", Environment.NewLine);
            }
            else
            {
                sb.AppendFormat("StoreProcedure.Create(\"{0}\", {1}).ExecuteNonQuery();{2}", obj.Name, sb.Length == 0 ? "null" : "parameter", Environment.NewLine);
            }
            return sb.ToString();
        }
    }
}
