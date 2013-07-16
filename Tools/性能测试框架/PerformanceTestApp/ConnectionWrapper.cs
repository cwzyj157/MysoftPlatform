using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;

using Mysoft.Map.Extensions.DAL;

namespace PerformanceTestApp
{
    public class ConnectionWrapper : IDisposable
    {
        private object _conn;
        private bool _sharedConnection;

        public ConnectionWrapper(UiParameters parameter)
        {
            _sharedConnection = parameter.SharedConnection;
            if (parameter.SharedConnection)
            {
                if (parameter.TestName.IndexOf("DAL") != -1)
                {
                    _conn = new ConnectionScope();
                }
                else
                {
                    SqlConnection conn = new SqlConnection();
                    conn.ConnectionString = Program.ConnectionString;
                    conn.Open();
                    _conn  = conn;
                }
            }
        }

        public SqlConnection GetSqlConnection()
        {
            if (_conn != null)
            {
                return _conn as SqlConnection;
            }
            else
            {
                SqlConnection conn = new SqlConnection();
                conn.ConnectionString = Program.ConnectionString;
                conn.Open();
                _conn = conn;
                return conn;
            }
        }

        public ConnectionScope GetConnectionScope()
        {
            if (_conn != null)
            {
                return _conn as ConnectionScope;
            }
            else
            {
                ConnectionScope scope = new ConnectionScope();
                _conn = scope;
                return scope;
            }
        }

        public void Close()
        {
            if (_sharedConnection == false)
            {
                Dispose();
            }
        }

        public void Dispose()
        {
            if (_conn != null)
            {
                ((IDisposable)_conn).Dispose();
                _conn = null;
            }
        }

    }
}
