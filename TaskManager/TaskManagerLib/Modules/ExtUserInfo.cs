using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Principal;
using TaskManagerLib.Model;


namespace TaskManagerLib.Modules
{
	public class ExtUserInfo : IPrincipal
	{
		private IPrincipal _principal;
		private User _user;

		public ExtUserInfo(IPrincipal principal, User user)
		{
			if( principal == null )
				throw new ArgumentNullException("principal");

			//if( user == null )
			//    throw new ArgumentNullException("user");


			_principal = principal;
			_user = user;
		}


		public IIdentity Identity
		{
			get { return _principal.Identity; }
		}

		public bool IsInRole(string role)
		{
			if( _user == null )
				return false;

			return _user.IsInRole(role);
		}

		public User UserInfo
		{
			get { return _user; }
		}

	}
}
