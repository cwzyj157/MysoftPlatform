using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MyMVC;
using TaskManagerLib.Common;


namespace TaskManagerLib.Controller
{
	public class HomePageController
	{
		[Action]
		[PageUrl(Url = "/")]
		[PageUrl(Url = "/Default.aspx")]
		//[PageUrl(Url = "/Pages/Default.aspx")]
		public object Show()
		{
			string filePath = System.IO.Path.Combine(HttpContextHelper.AppRootPath, @"App_Data\website.config");

			if( System.IO.File.Exists(filePath) == false ) {
				AppRunOptions options = CreateDefaultOptions();
				MyMVC.XmlHelper.XmlSerializeToFile(options, filePath, Encoding.UTF8);
			}


			return new XmlResult(AppInfo.RunOptions);
		}


		private static AppRunOptions CreateDefaultOptions()
		{
			AppRunOptions options = new AppRunOptions();
			options.AppName = "TaskManager";
			options.Users = new List<TaskManagerLib.Model.User>();

			options.Users.Add(new TaskManagerLib.Model.User {
				Names = new TaskManagerLib.Model.AdUserName {
					FullName = "李奇峰",
					ShortName = "liqf01"
				},
				Role = new TaskManagerLib.Model.UserRole[] { TaskManagerLib.Model.UserRole.Admin }
			});

			options.Users.Add(new TaskManagerLib.Model.User {
				Names = new TaskManagerLib.Model.AdUserName {
					FullName = "李俊峰",
					ShortName = "lijf"
				},
				Role = new TaskManagerLib.Model.UserRole[] { TaskManagerLib.Model.UserRole.PM, TaskManagerLib.Model.UserRole.Developer }
			});

			options.Users.Add(new TaskManagerLib.Model.User {
				Names = new TaskManagerLib.Model.AdUserName {
					FullName = "陈伟",
					ShortName = "chenw03"
				},
				Role = new TaskManagerLib.Model.UserRole[]{ TaskManagerLib.Model.UserRole.Developer }
			});

			options.Users.Add(new TaskManagerLib.Model.User {
				Names = new TaskManagerLib.Model.AdUserName {
					FullName = "周睿",
					ShortName = "zhour"
				},
				Role =new TaskManagerLib.Model.UserRole[]{  TaskManagerLib.Model.UserRole.Tester }
			});

			return options;
		}
	}
}
