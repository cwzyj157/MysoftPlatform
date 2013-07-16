using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading;
using System.Diagnostics;

namespace SmokingTestLibrary
{
	public sealed class TestExecutor
	{
		public static void Start(Assembly assembly)
		{
			List<Type> list =
				 (from t in assembly.GetExportedTypes()
				  let a = t.GetMyAttribute<TestAttribute>()
				  where a != null
				  select t).ToList();
			
			ExecuteTest(list);

			Console.WriteLine("\r\n\r\n===============================================================================");
			Console.WriteLine("已完成对 {0} 的测试。", assembly.FullName);
			Console.WriteLine("===============================================================================");
		}


		public static void Start(params Type[] list)
		{
			ExecuteTest(list);

			Console.WriteLine("\r\n\r\n===============================================================================");
			Console.WriteLine("已完成对以下类型的冒烟测试：");
			foreach( Type t in list )
				Console.WriteLine(t.FullName);
			Console.WriteLine("===============================================================================");
		}


		public static void ExecuteTest(IEnumerable<Type> list)
		{
			// 需要单独运行的测试任务
			List<Type> standalong = list.Where(t => t.GetMyAttribute<TestAttribute>().InNewThread).ToList();

			// 可以混在一起运行的测试任务
			Type[] batch = list.Where(t => t.GetMyAttribute<TestAttribute>().InNewThread == false).ToArray();


			List<Thread> threads = new List<Thread>(standalong.Count + 1);

			if( standalong != null && standalong.Count > 0 ) {
				foreach( Type t in standalong ) {
					TestExecutor executor = new TestExecutor(t);
					Thread thread = new Thread(executor.Run);
					thread.Name = "独立线程-" + t.Name;
					threads.Add(thread);
					thread.Start();
				}
			}


			if( batch != null && batch.Length > 0 ) {
				TestExecutor batchExector = new TestExecutor(batch);
				Thread batchThread = new Thread(batchExector.Run);
				batchThread.Name = "BatchThread";
				threads.Add(batchThread);
				batchThread.Start();
			}


			foreach( Thread t in threads )
				t.Join();


			
		}


		

		private Type[] _testClassTypes;

		internal TestExecutor(params Type[] types)
		{
			if( types == null || types.Length == 0 )
				throw new ArgumentNullException("types");

			_testClassTypes = types;
		}

		internal void Run()
		{
			foreach( Type t in _testClassTypes ) {
				if( CheckTypeSupportTest(t) == false )
					continue;

				TestAttribute attr = t.GetMyAttribute<TestAttribute>();
				int count = attr.RunTimes > 1 ? attr.RunTimes : 1;

				try {
					for( int i = 0; i < count; i++ )
						RunTestClass(t);
				}
				catch( Exception ex ) {
					ShowException(ex, t, null);
				}
			}
		}


		private bool CheckTypeSupportTest(Type t)
		{
			MethodInfo isSupoort = t.GetMethod("IsSupport", BindingFlags.Public | BindingFlags.Static,
				null, Type.EmptyTypes, null);

			if( isSupoort != null ) {
				bool supported = (bool)isSupoort.Invoke(null, null);

				if( supported == false ) {
					Console.WriteLine("警告： 类型 {0} 由于不满足条件，将不参与冒烟测试。", t);
					return false;
				}
			}
			return true;

		}

		private void RunTestClass(Type t)
		{
			MethodInfo[] methods = (from m in t.GetMethods(BindingFlags.Instance | BindingFlags.Public)
									let a = m.GetMyAttribute<TestMethodAttribute>(true)
									where a != null && m.GetParameters().Length == 0
									orderby a.Order ascending
									select m).ToArray();

			if( methods == null || methods.Length == 0 )
				return;


			object instance = Activator.CreateInstance(t);

			foreach( MethodInfo m in methods ) {
				TestMethodAttribute attr = m.GetMyAttribute<TestMethodAttribute>(true);
				int count = attr.RunTimes > 1 ? attr.RunTimes : 1;

				Stopwatch watch = null;
				if( attr.TimeOut > 0 )
					watch = Stopwatch.StartNew();

				try {
					for( int i = 0; i < count; i++ ) {
						m.Invoke(instance, null);
						//Console.WriteLine("{0} => {1}.{2} OK.", Thread.CurrentThread.Name, t.Name, m.Name);
					}

					if( watch != null ) {
						watch.Stop();

						if( watch.ElapsedMilliseconds > attr.TimeOut )
							throw new AssertTimeoutException(attr.TimeOut, watch.ElapsedMilliseconds);
					}
				}
				catch( Exception ex ) {
					ShowException(ex, t, m);
				}
				finally {
					if( watch != null )
						watch.Stop();
				}
			}

			IDisposable dispose = instance as IDisposable;
			if( dispose != null )
				try {
					dispose.Dispose();
				}
				catch( Exception ex ) {
					ShowException(ex, t, null, "正在调用IDisposable接口。");
				}
		}


		private static void ShowException(Exception ex, Type t, MethodInfo m, params string[] messages)
		{
			Console.WriteLine("-----------------------------------------");
			Console.WriteLine("当前线程名称: " + Thread.CurrentThread.Name);

			if( t != null )
				Console.WriteLine("运行测试时发生异常，正在运行的类型：" + t.ToString());
			if( m != null )
				Console.WriteLine("正在运行的方法名称：" + m.Name);

			if( messages != null )
				foreach( string message in messages )
					Console.WriteLine(message);

			Console.WriteLine("异常内容：");
			Console.WriteLine(ex.ToString());
			Console.WriteLine();
		}
	}
}
