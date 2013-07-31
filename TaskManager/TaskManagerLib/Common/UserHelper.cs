using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using TaskManagerLib.Exceptions;
using TaskManagerLib.Log;
using System.Web;
using System.DirectoryServices;
using System.Management;

namespace TaskManagerLib.Common
{
	internal sealed class ADUserInfo
	{
		public string FullName;
		public string ShortName;
		public string[] MemberOf;

		public bool CheckInGroup(string group)
		{
			if( string.IsNullOrEmpty(group) )
				throw new ArgumentNullException("group");

			if( this.MemberOf == null )
				return false;

			foreach( string s in this.MemberOf )
				if( s.IndexOf(group) > 0 )
					return true;

			return false;
		}
	}


	internal static class UserHelper
	{
		internal static readonly string UnknownUser = "[Unknown User]";
		private static Hashtable s_table = Hashtable.Synchronized(new Hashtable(200));



		public static string GetShortNameFromHttpContext(HttpContext context)
		{
			if( context == null )
				return null;

			if( context.Request.IsAuthenticated == false )
				return null;

			//return "fl45";

			string loginName = context.User.Identity.Name;

			string[] array = loginName.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
			if( array.Length == 2 )
				return array[1];
				
			else
				LogHelper.SafeLogException(new MyMessageException("无效的域用户名：" + loginName));
			
			return null;
		}


		public static string GetCurrentUserFullName()
		{
			string shortName = GetShortNameFromHttpContext(HttpContext.Current);
			if( string.IsNullOrEmpty(shortName) )
				return UnknownUser;

			return TryGetFullNameFromCache(shortName);
		}


		/// <summary>
		/// 尝试先从缓存中，根据用户短名获取对应的全名，如果缓存没有，则查询AD来获取。
		/// </summary>
		/// <param name="shortName"></param>
		/// <returns></returns>
		public static string TryGetFullNameFromCache(string shortName)
		{
			ADUserInfo info = TryGetUserInfoFromCache(shortName);
			if( info == null )
				return null;
			else
				return info.FullName;
		}


		/// <summary>
		/// 尝试先从缓存中，根据用户短名获取对应的全名，如果缓存没有，则查询AD来获取。
		/// </summary>
		/// <param name="shortName"></param>
		/// <returns></returns>
		internal static ADUserInfo TryGetUserInfoFromCache(string shortName)
		{
			if( string.IsNullOrEmpty(shortName) )
				throw new ArgumentNullException("shortName");

			// 使用缓存，避免每次都搜索活动目录。
			ADUserInfo info = (ADUserInfo)s_table[shortName];
			if( info == null ) {
				info = GetUserInfoByShortName(shortName);
				if( info != null )
					s_table[shortName] = info;
			}

			return info;
		}


		private static readonly string DomainName =  GetDomainName();



		internal static ADUserInfo GetUserInfoByShortName(string shortName)
		{
			if( string.IsNullOrEmpty(shortName) )
				throw new ArgumentNullException("shortName");

			//return new ADUserInfo {
			//    FullName = "Fish.Q.Li",
			//    MemberOf = new string[] { "CN=.nesc-sh.mis.cnec.developer" }
			//};
			
			try {
				DirectoryEntry entry = new DirectoryEntry("LDAP://" + DomainName);
				DirectorySearcher search = new DirectorySearcher(entry);
				search.Filter = "(SAMAccountName=" + shortName + ")";
				search.PropertiesToLoad.Add("cn");
				search.PropertiesToLoad.Add("memberOf");
				SearchResult result = search.FindOne();

				if( result != null ) {
					ADUserInfo info = new ADUserInfo();
					info.ShortName = shortName;
					info.FullName = result.Properties["cn"][0].ToString();
					info.MemberOf = (from s in result.Properties["memberOf"].Cast<string>() select s).ToArray();
					return info;
				}
			}
			catch( Exception ex ) {
				LogHelper.SafeLogException(ex);
			}


			if( string.IsNullOrEmpty(DomainName) )
				// 自己在本机测试用，如果有域，可以删除下面的代码。
				return new ADUserInfo {
					ShortName = "fl45",
					FullName = "Fish.Q.Li",
					MemberOf = new string[] { "CN=.nesc-sh.mis.cnec.developer" }
				};
			

			return null;
		}


		internal static string GetDomainName()
		{
			SelectQuery query = new SelectQuery("Win32_ComputerSystem");
			using( ManagementObjectSearcher searcher = new ManagementObjectSearcher(query) ) {
				foreach( ManagementObject mo in searcher.Get() ) {
					if( (bool)mo["partofdomain"] )
						return mo["domain"].ToString();
				}
			}
			return null;
		}


	}
}
