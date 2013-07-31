using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace TaskManagerLib.Model
{
	public class AdUserName 
	{
		[XmlAttribute]
		public string FullName { get; set; }

		[XmlAttribute]
		public string ShortName { get; set; }

		public static implicit operator AdUserName(string value)
		{
			AdUserName name = new AdUserName { ShortName = value };
			if( string.IsNullOrEmpty(value) == false ) {
				User user = TaskManagerLib.Common.AppInfo.RunOptions.Users.FirstOrDefault(x => x.Names.ShortName == value);
				name.FullName = (user == null ? value : user.Names.FullName);
				//name.FullName = TaskManagerLib.Common.UserHelper.TryGetFullNameFromCache(value);
			}

			return name;
		}
	}


	public class User
	{
		public AdUserName Names { get; set; }

		[XmlAttribute]
		public UserRole[] Role { get; set; }


		public bool IsInRole(UserRole role)
		{
			foreach( UserRole r in this.Role )
				if( r == role )
					return true;

			return false;
		}

		public bool IsInRole(string role)
		{
			switch( role ) {
				case "Admin":
					return IsInRole(UserRole.Admin);
				case "Developer":
					return IsInRole(UserRole.Developer);
				case "Tester":
					return IsInRole(UserRole.Tester);
				case "PM":
					return IsInRole(UserRole.PM);
				default:
					return false;
			}
		}
		
		public bool IsInRole(params UserRole[] roles)
		{
			if( roles == null || roles.Length == 0 )
				return false;

			foreach( UserRole role in roles )
				if( IsInRole(role) )
					return true;

			return false;
		}
	}
}
