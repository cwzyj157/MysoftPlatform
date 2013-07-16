using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Data.SqlClient;
using Microsoft.Win32;


namespace Mysoft.Map.Extensions2
{
	/// <summary>
	/// 一些与安全相关的方法类
	/// </summary>
	public static class SecurityHelper
	{
		/// <summary>
		/// 从注册表获取【明源】风格的连接字符串
		/// </summary>
		/// <param name="path">注册表路径，例如：SOFTWARE\mysoft\ERP25</param>
		/// <returns>读取到的连接字符串。</returns>
		public static string GetConnectionStringFromRegistry(string path)
		{
			using( RegistryKey reg = Registry.LocalMachine.OpenSubKey(path, false) ) {
				SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
				string server = reg.GetValue("ServerName", string.Empty).ToString();
				string port = reg.GetValue("1433", string.Empty).ToString();

				if( string.IsNullOrEmpty(port) )
					builder.DataSource = server;
				else
					builder.DataSource = string.Format("{0},{1}", server, port);

				builder.InitialCatalog = reg.GetValue("DBName", string.Empty).ToString();
				builder.UserID = reg.GetValue("UserName", string.Empty).ToString();
				
				string encryptedPwd = reg.GetValue("SaPassword", string.Empty).ToString();
				if( string.IsNullOrEmpty(encryptedPwd) )
					throw new System.Configuration.ConfigurationErrorsException("注册表中没有指定密码。");

				builder.Password = DecryptMysoftPassword(encryptedPwd);

				return builder.ToString();
			}
		}

		/// <summary>
		/// 解密明源密码
		/// </summary>
		/// <param name="inStr">密文</param>
		/// <returns>明文</returns>
		private static string DecryptMysoftPassword(string inStr)
		{

			string StrBuff = null;
			int IntLen = 0;
			int IntCode = 0;
			int IntCode1 = 0;
			int IntCode2 = 0;
			int IntCode3 = 0;
			int i = 0;

			StrBuff = "";

			IntLen = inStr.Trim().Length;

			IntCode1 = IntLen % 3;
			IntCode2 = IntLen % 9;
			IntCode3 = IntLen % 5;

			if( IntLen % 2 == 0 ) {
				IntCode = IntCode2 + IntCode3;
			}
			else {
				IntCode = IntCode1 + IntCode3;
			}
			
			for( i = 1; i <= IntLen; i++ ) {
				StrBuff = StrBuff + Convert.ToChar(Convert.ToInt16(inStr.Substring(IntLen - i, 1).ToCharArray()[0]) + IntCode);

				if( IntCode == IntCode1 + IntCode3 ) {
					IntCode = IntCode2 + IntCode3;
				}
				else {
					IntCode = IntCode1 + IntCode3;
				}
			}

			return StrBuff + new string(' ', inStr.Length - IntLen);
		}


	}
}
