using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;

namespace PerformanceTestApp
{
	static class Program
	{
		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			AppInit();

			OptionControl ctrl = new OptionControl();
			TestObject executor = new TestObject();

			MyTestAppFramework.TestMainForm mainForm = new MyTestAppFramework.TestMainForm(ctrl, executor);

            mainForm.Load += new EventHandler(mainForm_Load);
            mainForm.FormClosing += new FormClosingEventHandler(mainForm_FormClosing);

			Application.Run(mainForm);
		}

        static void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            TestHelper.ClearResource();
        }

        static void mainForm_Load(object sender, EventArgs e)
        {
            
            TestHelper.InitResource();
        }

		public static string ConnectionString = null;

		static void AppInit()
		{
            ConnectionStringSettings setting = ConfigurationManager.ConnectionStrings["Default"];
			ConnectionString = setting.ConnectionString;

            Mysoft.Map.Extensions.Initializer.UnSafeInit(ConnectionString);
		}

	}
}
