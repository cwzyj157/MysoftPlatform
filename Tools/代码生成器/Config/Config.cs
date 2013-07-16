using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using Microsoft.Win32;

namespace 代码生成器
{
    [XmlRoot]
    public class Config
    {
        [XmlElement("Item")]
        public List<ConfigItem> Item;

        public static Config Load()
        {
            if (File.Exists("config.xml"))
            {
                XmlSerializer seri = new XmlSerializer(typeof(Config));
                using (FileStream fs = new FileStream("config.xml", FileMode.Open)) {
                    return seri.Deserialize(fs) as Config;
                }
            }
            return null;
        }

        public static void Save(Config config){
            if (File.Exists("config.xml"))
            {
                File.Delete("config.xml");
            }
            XmlSerializer seri = new XmlSerializer(typeof(Config));
            using (StreamWriter writer = new StreamWriter("config.xml"))
            {
                seri.Serialize(writer, config);
            }
            
        }

        public static Config LoadByRegistry()
        {
            Config cfg = new Config();

            RegistryKey regKey = null;

            if (System.IntPtr.Size == 8)
            {
                regKey = Registry.LocalMachine.OpenSubKey("Software\\Wow6432Node\\mysoft" , false);
            }
            else
            {
                regKey = Registry.LocalMachine.OpenSubKey("Software\\mysoft", false);
            }

            if (regKey == null)
            {
                return null;
            }

            List<ConfigItem> list = new List<ConfigItem>();

            foreach (string subName in regKey.GetSubKeyNames())
            {
                RegistryKey keyConn = regKey.OpenSubKey(subName);
                System.Diagnostics.Debug.WriteLine(keyConn.Name);

                string pwd = keyConn.GetValue("SaPassword", "").ToString();
                pwd = Cryptography.DeCode(pwd);
                string dbName = keyConn.GetValue("DBName", "").ToString();
                string serverName = keyConn.GetValue("ServerName", "").ToString();
                string port = "";
                if (keyConn.GetValue("ServerProt", "") == null || keyConn.GetValue("ServerProt", "").ToString().Length < 1)
                {
                    port = "1433";
                }
                else
                {
                    port = keyConn.GetValue("ServerProt", "").ToString();
                }
                string userName = keyConn.GetValue("UserName", "").ToString();
                ConfigItem item = new ConfigItem();
                item.Name = serverName + "@" + dbName;
                if (port == "1433")
                {
                    item.ConnectionString = string.Format("Password={0};Persist Security Info=True;User ID={1};Initial Catalog={2};Data Source={3}", pwd, userName, dbName, serverName);
                }
                else
                {
                    item.ConnectionString = string.Format("Password={0};Persist Security Info=True;User ID={1};Initial Catalog={2};Data Source={3},{4}", pwd, userName, dbName, serverName, port);
                }
                
                if (!string.IsNullOrEmpty(serverName) && !string.IsNullOrEmpty(dbName) && userName != "sa")
                {
                    list.Add(item);
                }
            }

            cfg.Item = list;

            return cfg;
        }
    }
}
