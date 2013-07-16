using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.CSharp;
using Mysoft.Map.Extensions.DAL;

namespace SmokingTest.DAL
{
	class Program
	{
		static void Main(string[] args)
		{
			AsmInit.Init();
			

			////SmokingTestLibrary.TestExecutor.Start(typeof(Program).Assembly);
			////SmokingTestLibrary.TestExecutor.Start(typeof(TestSqlTraceCode));
			//SmokingTestLibrary.TestExecutor.Start(typeof(TestAncillaryUpdateCode));

			RunTestTask();

			Console.ReadLine();
		}

		internal static int SqlVeresion { get; set; }


		static void RunTestTask()
		{
			string code = System.IO.File.ReadAllText(@"c:\SmokingTest_EntryPoint.txt");

			if( string.IsNullOrEmpty(code) ) {
				SmokingTestLibrary.TestExecutor.Start(typeof(Program).Assembly);
				return;
			}

			Console.WriteLine("Run Task:");
			Console.WriteLine(code);
			Console.WriteLine("-------------------------------------------");

			string codeTemplate = @"
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Mysoft.Map.Extensions.DAL;
using SmokingTest.DAL;
using SmokingTestLibrary;

namespace SmokingTest.CommandLine
{
	public class Program
	{
		public static void RunTest()
		{
			{code};
		}
	}
}
";
			code = codeTemplate.Replace("{code}", code);


			// 2. 设置编译参数，主要是指定将要引用哪些程序集
			CompilerParameters cp = new CompilerParameters();
			cp.GenerateExecutable = false;
			cp.GenerateInMemory = true;
			cp.ReferencedAssemblies.Add("System.dll");
			cp.ReferencedAssemblies.Add(typeof(Program).Assembly.Location);
			cp.ReferencedAssemblies.Add(typeof(CPQuery).Assembly.Location);
			cp.ReferencedAssemblies.Add(typeof(SmokingTestLibrary.TestExecutor).Assembly.Location);

			// 3. 获取编译器并编译代码
			// 由于我的代码使用了【自动属性】特性，所以需要 C# .3.5版本的编译器。
			// 获取与CLR匹配版本的C#编译器可以这样写：CodeDomProvider.CreateProvider("CSharp")

			Dictionary<string, string> dict = new Dictionary<string, string>();
			dict["CompilerVersion"] = "v3.5";
			dict["WarnAsError"] = "false";

			CSharpCodeProvider csProvider = new CSharpCodeProvider(dict);
			CompilerResults cr = csProvider.CompileAssemblyFromSource(cp, code);

			// 4. 检查有没有编译错误
			if( cr.Errors != null && cr.Errors.HasErrors ) {
				foreach( CompilerError error in cr.Errors )
					Console.WriteLine(error.ErrorText);

				return;
			}

			// 5. 获取编译结果，它是编译后的程序集
			Assembly asm = cr.CompiledAssembly;

			// 6. 找到目标方法，并调用
			Type t = asm.GetType("SmokingTest.CommandLine.Program");
			MethodInfo method = t.GetMethod("RunTest");
			method.Invoke(null, null);
		}
	}
}
