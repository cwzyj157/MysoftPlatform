using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;

using MyTestAppFramework;

using PerformanceTestApp.Entity;

using Mysoft.Map.Extensions.DAL;

namespace PerformanceTestApp
{

    public interface IPerformanceTest : IDisposable
    {
        void Run();
    }

    #region ###平台性能测试用例###
    // 说明：TestMethod的构造函数接受二个参数：
	//        1. 一个字符串，表示测试名称
	//        2. 显示序号。用于决定在下拉列表中的显示位置。

    [TestMethod("ToList,ADO", 1)]
    public class Test_ADO_ToList : IPerformanceTest
    {
        private UiParameters uiParam;
        private ConnectionWrapper wrapper;

        public Test_ADO_ToList(UiParameters param)
        {
            this.uiParam = param;
            wrapper = new ConnectionWrapper(param);
        }

        private string _sql2list = "SELECT TOP {0} ApproveState, JsState, HtProperty, ContractCode, ContractName, HtAmount, SignDate, YfCorporation, IsLock, ProjectNameList  FROM cb_Contract";

        public void Run()
        {
            SqlCommand cmd = wrapper.GetSqlConnection().CreateCommand();
            string sql = string.Format(_sql2list, 20);
            List<cbContract> list = new List<cbContract>();
            cmd.CommandText = sql;
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    cbContract contract = new cbContract();
                    object value = dr["ApproveState"];
                    if (value != DBNull.Value && value != null)
                    {
                        contract.ApproveState = value.ToString();
                    }
                    value = dr["JsState"];
                    if (value != DBNull.Value && value != null)
                    {
                        contract.JsState = value.ToString();
                    }
                    value = dr["HtProperty"];
                    if (value != DBNull.Value && value != null)
                    {
                        contract.HtProperty = value.ToString();
                    }
                    value = dr["ContractCode"];
                    if (value != DBNull.Value && value != null)
                    {
                        contract.ContractCode = value.ToString();
                    }
                    value = dr["ContractName"];
                    if (value != DBNull.Value && value != null)
                    {
                        contract.ContractName = value.ToString();
                    }
                    value = dr["HtAmount"];
                    if (value != DBNull.Value && value != null)
                    {
                        contract.HtAmount = (decimal)value;
                    }
                    value = dr["SignDate"];
                    if (value != DBNull.Value && value != null)
                    {
                        contract.SignDate = (DateTime)value;
                    }
                    value = dr["YfCorporation"];
                    if (value != DBNull.Value && value != null)
                    {
                        contract.YfCorporation = value.ToString();
                    }
                    value = dr["IsLock"];
                    if (value != DBNull.Value && value != null)
                    {
                        contract.IsLock = (byte)value;
                    }
                    value = dr["ApproveState"];
                    if (value != DBNull.Value && value != null)
                    {
                        contract.ApproveState = value.ToString();
                    }
                    list.Add(contract);
                }
            }
            wrapper.Close();
        }

        public void Dispose()
        {
            wrapper.Dispose();
        }
    }

    [TestMethod("ToList,DAL", 2)]
    public class Test_Extensions_ToList : IPerformanceTest
    {
        private UiParameters uiParam;
        private ConnectionWrapper wrapper;

        public Test_Extensions_ToList(UiParameters param)
        {
            this.uiParam = param;
            wrapper = new ConnectionWrapper(param);
        }

        private string _sql2list = "SELECT TOP {0} ApproveState, JsState, HtProperty, ContractCode, ContractName, HtAmount, SignDate, YfCorporation, IsLock, ProjectNameList  FROM cb_Contract";

        public void Run()
        {
            ConnectionScope scope = wrapper.GetConnectionScope();
            string sql = string.Format(_sql2list, 20);
            List<cbContract> list = CPQuery.From(sql).ToList<cbContract>();
            wrapper.Close();
        }

        public void Dispose()
        {
            wrapper.Dispose();
        }
    }

    [TestMethod("ExecSP,ADO", 3)]
    public class Test_ADO_ExecSP : IPerformanceTest
    {
        private UiParameters uiParam;
        private ConnectionWrapper wrapper;

        public Test_ADO_ExecSP(UiParameters param)
        {
            this.uiParam = param;
            wrapper = new ConnectionWrapper(param);
        }


        public void Run()
        {
            SqlCommand cmd = wrapper.GetSqlConnection().CreateCommand();

            cmd.CommandText = "usp_TestParameters";
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pInt", 1);
            cmd.Parameters.AddWithValue("@pStr", "123456789123456789123456789123456789");
            cmd.Parameters.AddWithValue("@pGUID", Guid.NewGuid());
            cmd.Parameters.AddWithValue("@pDtm", DateTime.Now);
            cmd.ExecuteNonQuery();

            wrapper.Close();
        }

        public void Dispose()
        {
            wrapper.Dispose();
        }
    }

    [TestMethod("ExecSP,DAL", 4)]
    public class Test_Extensions_ExecSP : IPerformanceTest
    {
        private UiParameters uiParam;
        private ConnectionWrapper wrapper;

        public Test_Extensions_ExecSP(UiParameters param)
        {
            this.uiParam = param;
            wrapper = new ConnectionWrapper(param);
        }


        public void Run()
        {
            ConnectionScope scope = wrapper.GetConnectionScope();
            var parameter = new { pInt = 1, pStr = "123456789123456789123456789123456789", pGUID = Guid.NewGuid(), pDtm = DateTime.Now };
            StoreProcedure sp = new StoreProcedure("usp_TestParameters", parameter);
            sp.ExecuteNonQuery();
            wrapper.Close();
        }

        public void Dispose()
        {
            wrapper.Dispose();
        }
    }

    [TestMethod("ExecSQL,ADO", 5)]
    public class Test_ADO_ExecSQL : IPerformanceTest
    {
        private UiParameters uiParam;
        private ConnectionWrapper wrapper;

        public Test_ADO_ExecSQL(UiParameters param)
        {
            this.uiParam = param;
            wrapper = new ConnectionWrapper(param);
        }


        public void Run()
        {
            SqlCommand cmd = wrapper.GetSqlConnection().CreateCommand();
            cmd.CommandText = "EXEC dbo.usp_TestParameters @pInt , @pStr, @pGUID, @pDtm";
            cmd.Parameters.AddWithValue("@pInt", 1);
            cmd.Parameters.AddWithValue("@pStr", "123456789123456789123456789123456789");
            cmd.Parameters.AddWithValue("@pGUID", Guid.NewGuid());
            cmd.Parameters.AddWithValue("@pDtm", DateTime.Now);
            cmd.ExecuteNonQuery();
            wrapper.Close();
        }

        public void Dispose()
        {
            wrapper.Dispose();
        }
    }

    [TestMethod("ExecSQL,DAL", 6)]
    public class Test_Extensions_ExecSQL : IPerformanceTest
    {
        private UiParameters uiParam;
        private ConnectionWrapper wrapper;

        public Test_Extensions_ExecSQL(UiParameters param)
        {
            this.uiParam = param;
            wrapper = new ConnectionWrapper(param);
        }


        public void Run()
        {
            ConnectionScope scope = wrapper.GetConnectionScope();
            var parameter = new { pInt = 1, pStr = "123456789123456789123456789123456789", pGUID = Guid.NewGuid(), pDtm = DateTime.Now };
            CPQuery query = CPQuery.From("EXEC dbo.usp_TestParameters @pInt , @pStr, @pGUID, @pDtm", parameter);
            query.ExecuteNonQuery();
            wrapper.Close();
        }

        public void Dispose()
        {
            wrapper.Dispose();
        }
    }


    [TestMethod(SeparatorComboBox.SeparatorString, 30)]
    public class Separator1
    {
        // 这只是一个用于定义分隔符的临时类型。
        // 注意TestMethod构造函数中的第一个参数。
    }

	[TestMethod("Insert,ADO", 31)]
    public class Test_ADO_Insert : IPerformanceTest
	{
		private UiParameters uiParam;
		private ConnectionWrapper wrapper;

        public Test_ADO_Insert(UiParameters param)
		{
			this.uiParam = param;
            wrapper = new ConnectionWrapper(param);
		}

        public void Run()
        {
            SqlCommand cmd = wrapper.GetSqlConnection().CreateCommand();
            SqlParameter[] ps = new SqlParameter[] {
                    new SqlParameter("@GuidValue", Guid.NewGuid()),
                    new SqlParameter("@IntValue", 754),
                    new SqlParameter("@MoneyValue", 3325.6m),
                    new SqlParameter("@VcharValue", "TestValue"),
                    new SqlParameter("@DtmValue", DateTime.Now)
                };

            cmd.CommandText = "INSERT INTO tbl_ForEntity(GuidValue ,IntValue ,MoneyValue ,VcharValue ,DtmValue) VALUES(@GuidValue ,@IntValue ,@MoneyValue ,@VcharValue ,@DtmValue)";
            cmd.Parameters.AddRange(ps);

            cmd.ExecuteNonQuery();
            wrapper.Close();
        }

        public void Dispose()
		{
            wrapper.Dispose();
		}
    }

    [TestMethod("Insert,DAL", 32)]
    public class Test_Extensions_Insert : IPerformanceTest
	{
        private UiParameters uiParam;
        private ConnectionWrapper wrapper;
        private ConnectionScope scope;

        public Test_Extensions_Insert(UiParameters param)
		{
            uiParam = param;
            wrapper = new ConnectionWrapper(param);
		}

        public void Run()
        {
            ConnectionScope scope = wrapper.GetConnectionScope();
            
            TblForEntity entity = new TblForEntity();
            entity.GuidValue = Guid.NewGuid();
            entity.IntValue = 754;
            entity.MoneyValue = 3325.6m;
            entity.VcharValue = "TestValue";
            entity.DtmValue = DateTime.Now;
            entity.Insert();
            
            wrapper.Close();
        }

		public void Dispose()
		{
            wrapper.Dispose();
		}
    }

    [TestMethod("Insert,BulkCopy", 33)]
    public class Test_BulkCopy_Insert : IPerformanceTest
    {
        private UiParameters uiParam;

        public Test_BulkCopy_Insert(UiParameters param)
        {
        }

        public void Run()
        {

    //            GuidValue UNIQUEIDENTIFIER,
    //IntValue int,
    //MoneyValue money,
    //VcharValue varchar(16),
    //DtmValue datetime 

            DataTable dt = new DataTable();
            dt.Columns.Add("EntityGuid", typeof(Guid));
            dt.Columns.Add("GuidValue", typeof(Guid));
            dt.Columns.Add("IntValue", typeof(int));
            dt.Columns.Add("MoneyValue", typeof(decimal));
            dt.Columns.Add("VcharValue", typeof(string));
            dt.Columns.Add("DtmValue", typeof(DateTime));

            for (int i = 0; i < 100000; i++)
            {
                DataRow row = dt.NewRow();
                row["EntityGuid"] = Guid.NewGuid();
                row["GuidValue"] = Guid.NewGuid();
                row["IntValue"] = (int)754;
                row["MoneyValue"] = 3325.6m;
                row["VcharValue"] = "TestValue";
                row["DtmValue"] = DateTime.Now;
                dt.Rows.Add(row);
            }

            //使用事务
            using (ConnectionScope scope = new ConnectionScope(TransactionMode.Required))
            {
                SqlBulkCopy bulkCopy = scope.CreateSqlBulkCopy(SqlBulkCopyOptions.FireTriggers);
                bulkCopy.DestinationTableName = "tbl_ForEntity";
                bulkCopy.WriteToServer(dt);
                scope.Commit();
            }
        }

        public void Dispose()
        {
        }
    }

    [TestMethod("Update,ADO", 34)]
    public class Test_ADO_Update : IPerformanceTest
    {
        private UiParameters uiParam;
        private ConnectionWrapper wrapper;

        public Test_ADO_Update(UiParameters param)
        {
            this.uiParam = param;
            wrapper = new ConnectionWrapper(param);
        }

        private Guid _guid = new Guid("F22BBDA0-8CCD-E211-9AD5-463500000031");

        public void Run()
        {
            SqlCommand cmd = wrapper.GetSqlConnection().CreateCommand();
            SqlParameter[] ps = new SqlParameter[] {
                    new SqlParameter("@GuidValue", Guid.NewGuid()),
                    new SqlParameter("@IntValue", 754),
                    new SqlParameter("@MoneyValue", 3325.6m),
                    new SqlParameter("@VcharValue", "TestValue"),
                    new SqlParameter("@DtmValue", DateTime.Now),
                    new SqlParameter("@EntityGuid", _guid)
                };

            cmd.CommandText = "UPDATE tbl_ForEntity SET GuidValue = @GuidValue ,IntValue = @IntValue, MoneyValue = @MoneyValue, VcharValue = @VcharValue, DtmValue = @DtmValue WHERE EntityGuid = @EntityGuid";
            cmd.Parameters.AddRange(ps);

            cmd.ExecuteNonQuery();
            wrapper.Close();
        }

        public void Dispose()
        {
            wrapper.Dispose();
        }
    }

    [TestMethod("Update,DAL", 35)]
    public class Test_Extensions_Update : IPerformanceTest
    {
        private UiParameters uiParam;
        private ConnectionWrapper wrapper;

        public Test_Extensions_Update(UiParameters param)
        {
            this.uiParam = param;
            wrapper = new ConnectionWrapper(param);
        }

        private Guid _guid = new Guid("F22BBDA0-8CCD-E211-9AD5-463500000031");

        public void Run()
        {
            ConnectionScope scope = wrapper.GetConnectionScope();
            TblForEntity entity = new TblForEntity();
            entity.GuidValue = Guid.NewGuid();
            entity.IntValue = 754;
            entity.MoneyValue = 3325.6m;
            entity.VcharValue = "TestValue";
            entity.DtmValue = DateTime.Now;
            entity.EntityGuid = _guid;
            entity.Update();
            wrapper.Close();
        }

        public void Dispose()
        {
            wrapper.Dispose();
        }
    }

    [TestMethod("Delete,ADO", 36)]
    public class Test_ADO_Delete : IPerformanceTest
    {
        private UiParameters uiParam;
        private ConnectionWrapper wrapper;

        public Test_ADO_Delete(UiParameters param)
        {
            this.uiParam = param;
            wrapper = new ConnectionWrapper(param);
        }

        private Guid _guid = new Guid("F22BBDA0-8CCD-E211-9AD5-463500000031");

        public void Run()
        {
            SqlCommand cmd = wrapper.GetSqlConnection().CreateCommand();
            cmd.CommandText = "DELETE FROM tbl_ForEntity WHERE EntityGuid = @EntityGuid";
            cmd.Parameters.Add(new SqlParameter("@EntityGuid", _guid));
            cmd.ExecuteNonQuery();
            wrapper.Close();
        }

        public void Dispose()
        {
            wrapper.Dispose();
        }
    }

    [TestMethod("Delete,DAL", 37)]
    public class Test_Extensions_Delete : IPerformanceTest
    {
        private UiParameters uiParam;
        private ConnectionWrapper wrapper;

        public Test_Extensions_Delete(UiParameters param)
        {
            this.uiParam = param;
            wrapper = new ConnectionWrapper(param);
        }

        private Guid _guid = new Guid("F22BBDA0-8CCD-E211-9AD5-463500000031");

        public void Run()
        {
            ConnectionScope scope = wrapper.GetConnectionScope();
            TblForEntity entity = new TblForEntity();
            entity.EntityGuid = _guid;
            entity.Delete();
            wrapper.Close();
        }

        public void Dispose()
        {
            wrapper.Dispose();
        }
    }

    #endregion
}
