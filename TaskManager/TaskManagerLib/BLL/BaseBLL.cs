using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagerLib.Model;
using TaskManagerLib.Common;

namespace TaskManagerLib.BLL
{
	public abstract class BaseBLL
	{
		private User _user;

		public User CurrentUser
		{
			set { _user = value; }
			get
			{
				if( _user == null )
					_user = UserManager.GetCurrentUser();
				return _user;
			}
		}
	}
}
