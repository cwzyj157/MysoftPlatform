using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Reflection;


namespace TestCodeDom
{
	class Program
	{
		static void Main(string[] args)
		{
			string entryPoint = (from s in System.IO.File.ReadAllLines(@"c:\TestCodeDom.EntryPoint.txt")
									   where s.StartsWith(";") == false
									   select s
									).First();

			int p = entryPoint.LastIndexOf(".");

			Type testType = Type.GetType(entryPoint.Substring(0, p));
			MethodInfo method = testType.GetMethod(entryPoint.Substring(p + 1));

			method.Invoke(null, null);
		}


	}

}
