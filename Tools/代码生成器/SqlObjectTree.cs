using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace 代码生成器
{
    public class SqlObjectTree
    {
        public static List<SqlObject> LoadTree(string connectionString)
        {
            SqlObject objTable = new SqlObject();
            objTable.Name = "Tables";
            objTable.Type = ObjectType.Folder;

            SqlObject objView = new SqlObject();
            objView.Name = "Views";
            objView.Type = ObjectType.Folder;

            SqlObject objProc = new SqlObject();
            objProc.Name = "Procedures";
            objProc.Type = ObjectType.Folder;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"SELECT
tbl.name AS [Name]
FROM
dbo.sysobjects AS tbl
INNER JOIN sysusers AS stbl ON stbl.uid = tbl.uid
WHERE
((tbl.type='U' or tbl.type='S'))and(CAST(
                CASE WHEN (OBJECTPROPERTY(tbl.id, N'IsMSShipped')=1) THEN 1 WHEN 1 = OBJECTPROPERTY(tbl.id, N'IsSystemTable') THEN 1 ELSE 0 END
             AS bit)=0)
ORDER BY
[Name] ASC";
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        List<SqlObject> childs = new List<SqlObject>();
                        while (dr.Read())
                        {
                            SqlObject obj = new SqlObject();
                            obj.Name = dr.GetString(0);
                            obj.Type = ObjectType.Table;
                            childs.Add(obj);
                        }
                        objTable.Childs = childs;
                    }
                    cmd.CommandText = @"SELECT
v.name AS [Name]
FROM
dbo.sysobjects AS v
INNER JOIN sysusers AS sv ON sv.uid = v.uid
WHERE
(v.type = 'V')and(CAST(
                CASE WHEN (OBJECTPROPERTY(v.id, N'IsMSShipped')=1) THEN 1 WHEN 1 = OBJECTPROPERTY(v.id, N'IsSystemTable') THEN 1 ELSE 0 END
             AS bit)=0)
ORDER BY
[Name] ASC";
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        List<SqlObject> childs = new List<SqlObject>();
						while (dr.Read())
                        {
                            SqlObject obj = new SqlObject();
                            obj.Name = dr.GetString(0);
                            obj.Type = ObjectType.View;
                            childs.Add(obj);
                        }
                        objView.Childs = childs;
                    }
                    cmd.CommandText = @"SELECT
sp.name AS [Name]
FROM
dbo.sysobjects AS sp
INNER JOIN sysusers AS ssp ON ssp.uid = sp.uid
WHERE
(sp.xtype = N'P' OR sp.xtype = N'RF')and(CAST(
                CASE WHEN (OBJECTPROPERTY(sp.id, N'IsMSShipped')=1) THEN 1 WHEN 1 = OBJECTPROPERTY(sp.id, N'IsSystemTable') THEN 1 ELSE 0 END
             AS bit)=0)
ORDER BY
[Name] ASC";
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        List<SqlObject> childs = new List<SqlObject>();
                        while (dr.Read())
                        {
                            SqlObject obj = new SqlObject();
                            obj.Name = dr.GetString(0);
                            obj.Type = ObjectType.StoreProcedure;
                            childs.Add(obj);
                        }
                        objProc.Childs = childs;
                    }
                }
            }

            List<SqlObject> list = new List<SqlObject>();
            list.Add(objTable);
            //list.Add(objView);
            list.Add(objProc);
            return list;
        }
    }
}
