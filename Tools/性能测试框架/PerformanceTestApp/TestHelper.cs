using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Mysoft.Map.Extensions;
using Mysoft.Map.Extensions.DAL;

namespace PerformanceTestApp
{
	public static class TestHelper
	{
        public static void DropTable(string tableName){
            object obj = CPQuery.From("SELECT OBJECT_ID('" + tableName + "')").ExecuteScalar<object>();
            if (obj != null && obj != DBNull.Value)
            {
                CPQuery.From("DROP Table [" + tableName + "]").ExecuteNonQuery();
            }
        }

        public static void DropProc(string procName)
        {
            object obj = CPQuery.From("SELECT OBJECT_ID('" + procName + "')").ExecuteScalar<object>();
            if (obj != null && obj != DBNull.Value)
            {
                CPQuery.From("DROP Proc [" + procName + "]").ExecuteNonQuery();
            }
        }

        public static void CreateTable(string sql)
        {
            CPQuery.From(sql).ExecuteNonQuery();
        }

        public static void InitResource()
        {
            TestHelper.DropProc("usp_TestParameters");
            TestHelper.DropTable("tbl_ForDelete");
            TestHelper.DropTable("tbl_ForEntity");

            //初始化表usp_TestParameters
            TestHelper.CreateTable(Properties.Resources.usp_TestParametersScript);

            //初始化tbl_ForDeleteScript
            TestHelper.CreateTable(Properties.Resources.tbl_ForDeleteScript);

            //初始化tbl_ForEntity
            TestHelper.CreateTable(Properties.Resources.tbl_ForEntityScript);
        }

        public static void ClearResource()
        {
            TestHelper.DropTable("tbl_ForEntity");
        }
	}
}
