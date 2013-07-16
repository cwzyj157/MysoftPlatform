	using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mysoft.Map.Extensions.DAL;
using SmokingTestLibrary;

namespace SmokingTest.DAL
{

	/// <summary>
	/// 测试事件触发 这里需要将InNewThread设置为true,连接保存在线程数据槽中.新开连接才能收到连接打开事件
	/// </summary>
	[SmokingTestLibrary.Test(InNewThread = true, RunTimes = 1)]
	[NUnit.Framework.TestFixture]
	public class TestEvent
	{

		private bool _connection = false;
		private bool _beforeExecute = false;
		private bool _afterExecute = false;
		private bool _onException = false;
		private Guid _guid = Guid.NewGuid();

		/// <summary>
		/// 测试事件触发
		/// <list type="bullet">
		/// <item><description>测试连接打开事件是否正确触发</description></item>
		/// <item><description>测试SQL执行前事件是否正确触发</description></item>
		/// <item><description>测试SQL执行后事件是否正确触发</description></item>
		/// <item><description>测试执行异常事件,是否正确触发</description></item>
		/// </list>
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 1, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void Event1()
		{
			EventManager.ConnectionOpened += new EventHandler<ConnectionEventArgs>(EventManager_ConnectionOpened);
			EventManager.BeforeExecute += new EventHandler<CommandEventArgs>(EventManager_BeforeExecute);
			EventManager.AfterExecute += new EventHandler<CommandEventArgs>(EventManager_AfterExecute);
			EventManager.OnException += new EventHandler<ExceptionEventArgs>(EventManager_OnException);

			using( ConnectionScope scope = new ConnectionScope() ) {
				Guid guid = "SELECT newid()".AsCPQuery().ExecuteScalar<Guid>();
				try {
					"SELECT x".AsCPQuery().ExecuteScalar<string>();
				}
				catch {
					//仅仅为了验证是否能触发OnException函数
				}
			}


			Assert.AreEqual<bool>(_connection, true);
			Assert.AreEqual<bool>(_beforeExecute, true);
			Assert.AreEqual<bool>(_afterExecute, true);
			Assert.AreEqual<bool>(_onException, true);
		}

		void EventManager_OnException(object sender, ExceptionEventArgs e)
		{
			if( e.UserData != null ) {
				if( e.UserData.Equals(_guid) ) {
					_onException = true;
				}
			}
		}

		void EventManager_AfterExecute(object sender, CommandEventArgs e)
		{
			if( e.UserData != null ) {
				if( e.UserData.Equals(_guid) ) {
					_afterExecute = true;
				}
			}
		}

		void EventManager_BeforeExecute(object sender, CommandEventArgs e)
		{
			e.UserData = _guid;
			_beforeExecute = true;
		}

		void EventManager_ConnectionOpened(object sender, ConnectionEventArgs e)
		{
			_connection = true;
		}
	}
}
