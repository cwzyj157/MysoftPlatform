using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagerLib.Model;

namespace TaskManagerLib.Common
{
	public class AppRunOptions
	{
		public string AppName { get; set; }

		public List<User> Users { get; set; }

		public List<string> TaskTypes { get; set; }


	}
}
