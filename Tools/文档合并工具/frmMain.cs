using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Text.RegularExpressions;

namespace 文档合并工具
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void btnSelectFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult dr = fbd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                txtLocation.Text = fbd.SelectedPath;
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            txtLocation.Text = Application.StartupPath;
        }

        private void btnMerge_Click(object sender, EventArgs e)
        {
            string pathUser = Path.Combine(txtLocation.Text, "UserManual\\WebTOC.xml");
            string pathIndex = Path.Combine(txtLocation.Text, "Index.aspx");
            string pathTools = Path.Combine(txtLocation.Text, "WebTOC.xml");
            string pathHtml = Path.Combine(txtLocation.Text, "Index.html");

            if (!File.Exists(pathUser))
            {
                MessageBox.Show("文件:[" + pathUser + "]不存在!");
                return;
            }

            if (!File.Exists(pathTools))
            {
                MessageBox.Show("文件:[" + pathTools + "]不存在!");
                return;
            }

            if (!File.Exists(pathIndex))
            {
                MessageBox.Show("文件:[" + pathIndex + "]不存在!");
                return;
            }

            XmlDocument docUser = new XmlDocument();
            docUser.Load(pathUser);

            XmlDocument docTool = new XmlDocument();
            docTool.Load(pathTools);

            //判断是否合并过
            XmlNodeList nodes = docTool.SelectNodes("//HelpTOCNode[@Merge='true']");

            if (nodes.Count == 0)
            {
                nodes = docTool.SelectNodes("/HelpTOC/HelpTOCNode");
            }

            if (nodes.Count == 0)
            {
                MessageBox.Show("无法找到HelpTOCNode节点!");
                return;
            }

            XmlNode node = docUser.SelectSingleNode("//HelpTOCNode[@Title='API手册']");

            if (node == null)
            {
                MessageBox.Show("无法找到API手册节点!");
                return;
            }

            foreach (XmlNode childNode in node.ChildNodes)
            {
                XmlAttribute attrTitle = childNode.Attributes["Title"];
                if (attrTitle != null)
                {
                    if (attrTitle.Value.IndexOf("前端API参考") == -1)
                    {
                        node.RemoveChild(childNode);
                    }
                }
            }

            foreach (XmlNode childNode in nodes)
            {
                XmlNode newChildNode = childNode.CloneNode(docUser);
                XmlAttribute attrMerge = newChildNode.Attributes["Merge"];

                if (attrMerge == null)
                {
                    attrMerge = docUser.CreateAttribute("Merge");
                    attrMerge.Value = "true";
                    newChildNode.Attributes.Append(attrMerge);
                }

                node.AppendChild(newChildNode);
            }

            string path = Path.Combine(txtLocation.Text, "WebTOC.xml");

            docUser.Save(path);

            string page = File.ReadAllText(pathIndex);
            page = page.Replace("src=\"html/N_Mysoft_Map_Extensions.htm\"", "src=\"UserManual/Home.htm\"");

            Regex regex = new Regex(@"<a (.*)\n(.*)\n(.*)Direct Link</a>", RegexOptions.Compiled);
            page = regex.Replace(page, "");

            File.Delete(pathIndex);
            File.WriteAllText(pathIndex, page, Encoding.UTF8);

            foreach (string file in Directory.GetFiles(txtLocation.Text, "*.php", SearchOption.TopDirectoryOnly))
            {
                File.Delete(file);
            }

            if (File.Exists(pathHtml))
            {
                File.Delete(pathHtml);
            }

            MessageBox.Show("合并完毕!");
        }

    }
}
