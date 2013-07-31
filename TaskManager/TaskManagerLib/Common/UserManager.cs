using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagerLib.Model;
using TaskManagerLib.Common;
using System.Web;
using TaskManagerLib.Modules;

namespace TaskManagerLib.Common
{
	public static class UserManager
	{
		//public static List<User> GetUserList()
		//{
		//    return AppInfo.RunOptions.Users;
		//}

		public static string GetCurrentUserFullName()
		{
			return UserHelper.GetCurrentUserFullName();
		}

		public static string GetFullNameByShortName(string shortName)
		{
			User user = AppInfo.RunOptions.Users.FirstOrDefault(x => x.Names.ShortName == shortName);
			if( user != null )
				return user.Names.FullName;

			return null;
		}

		public static User GetCurrentUser()
		{
			HttpContext context = HttpContext.Current;
			if( context == null )
				return null;

			ExtUserInfo userInfo = context.User as ExtUserInfo;
			if( userInfo == null )
				return null;

			return userInfo.UserInfo;
		}


		public static bool CheckCurrentUserRole(params UserRole[] roles)
		{
			User user = GetCurrentUser();
			if( user == null )
				return false;

			return user.IsInRole(roles);
		}


		

	}
}
