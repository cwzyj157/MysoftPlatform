using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

using Mysoft.Map.Extensions;
using Mysoft.Map.Extensions.DAL;


namespace 代码生成器
{
    public abstract class CodeBuilder
    {

        public string ConnectionString { get; set; }

        protected bool _autoComplete = false;


        public bool AutoComplete
        {
            set
            {
                _autoComplete = value;
            }
        }

        public static CodeBuilder Create(string language)
        {
            if (language == "CSharp")
            {
                return new CSCodeBuilder();
            }
            if (language == "VB")
            {
                return new VBCodeBuilder();
            }
            return null;
        }

        /// <summary>
        /// 生成代码
        /// </summary>
        /// <param name="obj">SQL对象</param>
        /// <returns>生成结果</returns>
        public abstract BuildResult BuildCode(SqlObject obj);

        /// <summary>
        /// 获得数据库对象的结构
        /// </summary>
        /// <param name="obj">数据库对象</param>
        /// <param name="commandText">获得结构对应的SQL</param>
        /// <returns>结构数据表</returns>
        protected DataTable GetSchema(SqlObject obj, out string commandText)
        {
            if (obj.Type == ObjectType.Table)
            {

                string sql = @"SELECT
                                            clmns.name AS [Name] ,
                                            CAST(ISNULL(cik.colid, 0) AS BIT) AS [InPrimaryKey] ,
                                            usrt.name AS [DataType] ,
                                            COLUMNPROPERTY(clmns.id, clmns.name, 'IsIdentity') AS IsIdentity,
                                            ISNULL((select c.text from syscomments as c where c.id = clmns.cdefault), '') AS DefaultValue,
                                            clmns.isnullable
                                        FROM
                                            dbo.sysobjects AS tbl
                                            INNER JOIN sysusers AS stbl ON stbl.uid = tbl.uid
                                            INNER JOIN dbo.syscolumns AS clmns ON clmns.id = tbl.id
                                            LEFT OUTER JOIN dbo.sysindexes AS ik ON ik.id = clmns.id
                                                                                    AND 0 != ik.status & 0x0800
                                            LEFT OUTER JOIN dbo.sysindexkeys AS cik ON cik.indid = ik.indid
                                                                                       AND cik.colid = clmns.colid
                                                                                       AND cik.id = clmns.id
                                            LEFT OUTER JOIN systypes AS usrt ON usrt.xusertype = clmns.xusertype
                                            LEFT OUTER JOIN systypes AS baset ON baset.xusertype = clmns.xtype
                                                                                 AND baset.xusertype = baset.xtype

                                        WHERE
                                            ( (tbl.type = 'U'
                                              OR tbl.type = 'S')
                                            )
                                            AND ( tbl.name = @TableName )
                                        ORDER BY
                                            CAST(clmns.colid AS INT) ASC";


                var paramter = new { TableName = obj.Name };

                DataTable dt = null;

                List<ExtendedProperty> props;

                using (ConnectionScope scope = new ConnectionScope(TransactionMode.Inherits, ConnectionString))
                {
                    dt = CPQuery.From(sql, paramter).FillDataTable();
                    var parameter = new { tabName = obj.Name };
                    props = CPQuery.From("SELECT * FROM ::fn_listextendedproperty (NULL, 'user', 'dbo', 'table', @tabName, 'column', DEFAULT)", parameter).ToList<ExtendedProperty>();
                }

                dt.Columns.Add("Comment", typeof(string));

                foreach (DataRow row in dt.Rows)
                {
                    string columnName = row["Name"].ToString();
                    ExtendedProperty prop = props.FirstOrDefault(x => string.Compare(x.ObjName, columnName, StringComparison.OrdinalIgnoreCase) == 0);
                    if (prop != null)
                    {
                        row["Comment"] = prop.Value;
                    }
                }

                commandText = sql;

                return dt;
            }
            if (obj.Type == ObjectType.View)
            {
                using (ConnectionScope scope = new ConnectionScope(TransactionMode.Inherits, ConnectionString))
                {
                    string sql = string.Format("SELECT * FROM [{0}] WHERE 1=2", obj.Name);
                    commandText = sql;
                    return CPQuery.From(sql).FillDataTable();
                }

            }
            if (obj.Type == ObjectType.SQL)
            {
                using (ConnectionScope scope = new ConnectionScope(TransactionMode.Inherits, ConnectionString))
                {
                    string sql = obj.Name;
                    commandText = sql;
                    return CPQuery.From(sql).FillDataTable();
                }
                
            }
            commandText = "";
            return null;
        }

        /// <summary>
        /// 清除表名,字段名中包含的非字符及符号
        /// </summary>
        /// <param name="name">表名或字段名</param>
        /// <returns>处理后的名字</returns>
        protected static string CleanName(string name)
        {
            StringBuilder sb = new StringBuilder();

            bool bToUpper = false;
            for (int i = 0; i < name.Length; i++)
            {
                char c = name[i];
                if (c == '-' || c == '_' || c == ' ' || c == '.' || c == ' ' || c == '+')
                {
                    bToUpper = true;
                }
                else
                {
                    if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9'))
                    {
                        if (bToUpper || sb.Length == 0)
                        {
                            bToUpper = false;
                            sb.Append(char.ToUpper(c));

                        }
                        else
                        {
                            sb.Append(c);
                        }
                    }
                }
            }
            return sb.ToString();
        }
    }
}
