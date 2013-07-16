using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;
using Mysoft.Map.Extensions;

namespace 代码生成器
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private string connectionString = @"";

        private void BindTree()
        {
            treeView1.Nodes.Clear();
            if (!string.IsNullOrEmpty(connectionString))
            {
                List<SqlObject> list = SqlObjectTree.LoadTree(connectionString);
                foreach (SqlObject obj in list)
                {
                    TreeNode node = new TreeNode();
                    node.Text = obj.Name;
                    node.Tag = obj;
                    treeView1.Nodes.Add(node);
                    BindTree(obj, node);
                }
            }
        }

        private void BindTree(SqlObject parent, TreeNode node)
        {
            if (parent.Childs != null)
            {
                foreach(SqlObject obj in parent.Childs){
                    TreeNode childNode = new TreeNode();
                    childNode.Text = obj.Name;
                    childNode.Tag = obj;
                    node.Nodes.Add(childNode);
                    BindTree(obj, childNode);
                }
            }
        }


        private void NewConnection()
        {
            frmConnect frmConnect = new frmConnect();
            if (frmConnect.ShowDialog(this) == DialogResult.OK)
            {
                Config cfg = Config.Load();
                if (cfg == null)
                {
                    cfg = new Config();
                    ConfigItem cs = new ConfigItem();
                    List<ConfigItem> list = new List<ConfigItem>();
                    cs.Name = frmConnect.ServerName;
                    cs.ConnectionString = frmConnect.ConnectionString;
                    list.Add(cs);
                    cfg.Item = list;
                }
                else
                {
                    ConfigItem cs = cfg.Item.Find(p => { return p.Name == frmConnect.ServerName; });
                    if (cs == null)
                    {
                        cs = new ConfigItem();
                        cs.Name = frmConnect.ServerName;
                        cs.ConnectionString = frmConnect.ConnectionString;
                        cfg.Item.Add(cs);
                    }
                    else
                    {
                        cs.ConnectionString = frmConnect.ConnectionString;
                    }
                }
                LoadMenu(toolStripSplitButtonRecentConnect, cfg);
                Config.Save(cfg);

                connectionString = frmConnect.ConnectionString;

                BindTree();
            }
        }

        private void LoadMenu(ToolStripSplitButton btn, Config cfg)
        {
            btn.DropDownItems.Clear();
            foreach (ConfigItem item in cfg.Item)
            {
                ToolStripMenuItem menu = new ToolStripMenuItem();
                menu.Text = item.Name;
                menu.Tag = item.ConnectionString;
                menu.Click += new EventHandler(menu_Click);
                btn.DropDownItems.Add(menu);
            }

        }

        private void menu_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem menu = sender as ToolStripMenuItem;
            if (menu != null)
            {
                try
                {
                    using(SqlConnection conn = new SqlConnection()){
                        conn.ConnectionString = menu.Tag.ToString();
                        conn.Open();
                    }
                    connectionString = menu.Tag.ToString();
                    BindTree();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }
        }

        private void ExecuteSQL()
        {
            SqlObject obj = new SqlObject();
            obj.Type = ObjectType.SQL;

            string language = toolStripComboBoxLanguage.Text;


            CodeBuilder builder = CodeBuilder.Create(language);
            builder.AutoComplete = false;
            builder.ConnectionString = connectionString;

            BuildResult code = builder.BuildCode(obj);
            if (code != null)
            {
                richTextBox1.Text = code.Code;
                richTextBox1.ForeColor = Color.Blue;
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            this.Text += " - V" + 
            System.Diagnostics.FileVersionInfo.GetVersionInfo(Application.ExecutablePath).FileVersion.ToString();

            try
            {
                Config cfg = Config.Load();
                if (cfg != null)
                {
                    LoadMenu(toolStripSplitButtonRecentConnect, cfg);
                }
                cfg = Config.LoadByRegistry();
                if (cfg != null)
                {
                    LoadMenu(toolStripSplitButtonRegistry, cfg);
                }

                toolStripComboBoxLanguage.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("读取配置文件出错,错误信息:" + ex.Message);
            }
        }

        private void toolStripButtonConnect_Click(object sender, EventArgs e)
        {
            NewConnection();
        }

        private void toolStripButtonRefresh_Click(object sender, EventArgs e)
        {
            BindTree();
        }

        private void toolStripSplitButtonRecentConnect_ButtonClick(object sender, EventArgs e)
        {
            toolStripSplitButtonRecentConnect.ShowDropDown();
        }

        private void toolStripSplitButtonRegistry_ButtonClick(object sender, EventArgs e)
        {
            toolStripSplitButtonRegistry.ShowDropDown();
        }

        private void toolStripMenuItemCopy_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(richTextBox1.Text) == false)
            {
                Clipboard.SetText(richTextBox1.Text, TextDataFormat.Text);
            }
        }

        private void toolStripButtonExport_Click(object sender, EventArgs e)
        {

            if (connectionString == "")
            {
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog();
            if (toolStripComboBoxLanguage.Text.IndexOf("CSharp") != -1)
            {
                
                sfd.Filter = "cs文件|*.cs";
            }
            else
            {
                sfd.Filter = "vb文件|*.vb";
            }
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(sfd.FileName))
                {
                    File.Delete(sfd.FileName);
                }

                if (toolStripComboBoxLanguage.Text.IndexOf("CSharp") != -1)
                {

                    File.AppendAllText(sfd.FileName, string.Format(@"using System;
using System.Collections.Generic;
using System.Text;
using Mysoft.Map.Extensions.DAL;{0}{0}", Environment.NewLine));
                }
                else
                {

                    File.AppendAllText(sfd.FileName, string.Format(@"Import System
Import System.Collections.Generic
Import System.Text
Import Mysoft.Map.Extensions.DAL{0}{0}", Environment.NewLine));

                }

                string language = toolStripComboBoxLanguage.Text;


                foreach (TreeNode node in treeView1.Nodes)
                {
                    SqlObject obj = node.Tag as SqlObject;
                    if (obj.Type == ObjectType.Folder && (obj.Name == "Tables"))
                    {
                        foreach (TreeNode childNode in node.Nodes)
                        {
                            obj = childNode.Tag as SqlObject;
                            if (obj != null)
                            {


                                CodeBuilder builder = CodeBuilder.Create(language);
                                builder.AutoComplete = false;
                                builder.ConnectionString = connectionString;


                                BuildResult code = builder.BuildCode(obj);

                                if (code != null)
                                {
                                    File.AppendAllText(sfd.FileName, code.Code);
                                    File.AppendAllText(sfd.FileName, Environment.NewLine);
                                }

                            }
                        }
                    }

                }
                MessageBox.Show("导出完毕!");
            }
        }

        private void toolStripButtonCopy_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(richTextBox1.Text))
            {
                Clipboard.SetText(richTextBox1.Text, TextDataFormat.Text);
            }
            
        }

        private void toolStripComboBoxLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            TreeNode node = treeView1.SelectedNode;
            if (node == null)
            {
                return;   
            }
            TreeViewEventArgs arg = new TreeViewEventArgs(node);
            treeView1_AfterSelect(treeView1, arg);
        }

		private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
		{
			SqlObject obj = e.Node.Tag as SqlObject;
			if( obj != null ) {
                try
                {
                    if (obj.Name == "Procedures" || obj.Type == ObjectType.StoreProcedure)
                    {
                        toolStripComboBoxLanguage.Visible = true;
                    }
                    else
                    {
                        toolStripComboBoxLanguage.SelectedIndex = 0;
                        toolStripComboBoxLanguage.Visible = false;
                    }

                    string language = toolStripComboBoxLanguage.Text;

                    CodeBuilder builder = CodeBuilder.Create(language);
                    builder.AutoComplete = false;
                    builder.ConnectionString = connectionString;

                    BuildResult code = builder.BuildCode(obj);

                    if (code != null)
                    {
                        richTextBox1.Text = code.Code;
                        richTextBox1.ForeColor = Color.Blue;
                    }
                }
                catch (PrimaryKeyNotFoundException)
                {
                    richTextBox1.Text = string.Format("表 {0} 中不存在主键,无法生成有效代码。", obj.Name);
                    richTextBox1.ForeColor = Color.Red;

                    MessageBox.Show(string.Format("表 {0} 中不存在主键,无法生成有效代码。", obj.Name), "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
			}
		}
    }
}
