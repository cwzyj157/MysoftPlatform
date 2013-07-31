using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;
using TaskManagerLib.Caching;
using MyMVC;

namespace TaskManagerLib.Common
{
	public static class AppInfo
	{
		private static FileDependencyManager<AppRunOptions> 
					s_RunOptions = new FileDependencyManager<AppRunOptions>(
							files => MyMVC.XmlHelper.XmlDeserializeFromFile<AppRunOptions>(files[0], Encoding.UTF8),
							Path.Combine(HttpRuntime.AppDomainAppPath, @"App_Data\website.config"));



		public static AppRunOptions RunOptions
		{
			get
			{
				return s_RunOptions.CacheResult.Result;
			}
		}

		public static void Init()
		{
		}


		
	}
}
