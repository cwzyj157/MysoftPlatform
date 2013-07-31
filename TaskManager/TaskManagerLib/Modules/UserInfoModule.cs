using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using TaskManagerLib.Common;
using TaskManagerLib.Model;

namespace TaskManagerLib.Modules
{
	public class UserInfoModule : IHttpModule
	{
		public void Dispose()
		{
		}

		public void Init(HttpApplication app)
		{
			app.PostAuthenticateRequest += new EventHandler(app_PostAuthenticateRequest);
		}

		void app_PostAuthenticateRequest(object sender, EventArgs e)
		{
			HttpApplication app = (HttpApplication)sender;
			if( app.Request.IsAuthenticated == false )
				return;

			string shortName = UserHelper.GetShortNameFromHttpContext(app.Context);
			if( string.IsNullOrEmpty(shortName) )
				return;


			User user = AppInfo.RunOptions.Users.FirstOrDefault(x => x.Names.ShortName == shortName);
			if( user != null ) 
				app.Context.User = new ExtUserInfo(app.Context.User, user);
		}


	}
}
