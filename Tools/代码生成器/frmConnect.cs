using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Threading;

namespace 代码生成器
{
    public partial class frmConnect : Form
    {
        public frmConnect()
        {
            InitializeComponent();
        }

        public string ConnectionString { get; set; }

        public string ServerName { get; set; }

        private void SetProperty()
        {

            if (txtPort.Text.Trim() == "1433")
            {
                ConnectionString = string.Format("Password={0};Persist Security Info=True;User ID={1};Initial Catalog={2};Data Source={3}",
                    txtPwd.Text.Trim(),
                    txtUserName.Text.Trim(),
                    cbxDb.Text.Trim(),
                    txtServerName.Text.Trim());
            }
            else
            {
                ConnectionString = string.Format("Password={0};Persist Security Info=True;User ID={1};Initial Catalog={2};Data Source={3},{4}",
                    txtPwd.Text.Trim(),
                    txtUserName.Text.Trim(),
                    cbxDb.Text.Trim(),
                    txtServerName.Text.Trim(),
                    txtPort.Text.Trim());
            }

            ServerName = txtServerName.Text.Trim() + "@" + cbxDb.Text.Trim();
        }


        private void btnOK_Click(object sender, EventArgs e)
        {

            try
            {
                SetProperty();

                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                }
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                DialogResult = DialogResult.No;
            }
        }

        private void BindDbNames()
        {

            if (txtServerName.Text.Trim() == string.Empty
                || txtPwd.Text.Trim() == string.Empty
                || txtUserName.Text.Trim() == string.Empty)
            {
                return;
            }

            string connectionString = "";

            if (txtPort.Text.Trim() == "1433")
            {
                connectionString = string.Format("Password={0};Persist Security Info=True;User ID={1};Initial Catalog=master;Data Source={2}",
                    txtPwd.Text.Trim(),
                    txtUserName.Text.Trim(),
                    txtServerName.Text.Trim());
            }
            else
            {
                connectionString = string.Format("Password={0};Persist Security Info=True;User ID={1};Initial Catalog=master;Data Source={2},{3}",
                 txtPwd.Text.Trim(),
                 txtUserName.Text.Trim(),
                 txtServerName.Text.Trim(),
                 txtPort.Text.Trim());
            }

            Thread thread = new Thread(new ParameterizedThreadStart(p => {
                using (SqlConnection conn = new SqlConnection(p.ToString()))
                {
                    try
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = conn;
                        cmd.CommandText = "SELECT [name] FROM sysdatabases WHERE name NOT IN ('master', 'tempdb', 'model', 'msdb')";
                        cmd.CommandType = CommandType.Text;
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {

                            List<string> dbs = new List<string>();
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    dbs.Add(dr.GetString(0));
                                }
                            }

                            BeginInvoke(new Action(() =>
                            {

                                if (IsHandleCreated)
                                {
                                    btnOK.Enabled = true;

                                    string val = cbxDb.SelectedText;

                                    cbxDb.Text = "";
                                    cbxDb.Items.Clear();

                                    if (dbs != null)
                                    {
                                        foreach (string db in dbs)
                                        {
                                            cbxDb.Items.Add(db);
                                        }
                                    }

                                    if (dbs.Count > 0)
                                    {
                                        if (!string.IsNullOrEmpty(val))
                                        {
                                            bool bDone = true;
                                            for (int i = 0; i < cbxDb.Items.Count; i++)
                                            {
                                                if (cbxDb.Items[i].ToString() == val)
                                                {
                                                    cbxDb.SelectedIndex = i;
                                                    bDone = false;
                                                    break;
                                                }
                                            }
                                            if (bDone)
                                            {
                                                cbxDb.SelectedIndex = 0;
                                            }
                                        }
                                        else
                                        {
                                            cbxDb.SelectedIndex = 0;
                                        }
                                    }
                                }

                            }));
                        }
                    }
                    catch (Exception ex)
                    {
                        BeginInvoke(new Action(() =>
                        {
                            if (IsHandleCreated)
                            {
                                cbxDb.Text = "";
                                cbxDb.Items.Clear();
                                btnOK.Enabled = false;
                            }
                        }));
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                }
            }));
            thread.Start(connectionString);
        }

        private void txtServerName_Leave(object sender, EventArgs e)
        {
            BindDbNames();
        }

        private void txtPort_Leave(object sender, EventArgs e)
        {
            BindDbNames();
        }

        private void txtUserName_Leave(object sender, EventArgs e)
        {
            BindDbNames();
        }

        private void txtPwd_Leave(object sender, EventArgs e)
        {
            BindDbNames();
        }
    }
}
