using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

/// <summary>
/// AppHelper 的摘要说明
/// </summary>
public static class AppHelper
{
	public static void Init()
	{
		string connectionString = ConfigurationManager.ConnectionStrings["demo"].ConnectionString;

		Mysoft.Map.Extensions.Initializer.UnSafeInit(connectionString);

	}

}