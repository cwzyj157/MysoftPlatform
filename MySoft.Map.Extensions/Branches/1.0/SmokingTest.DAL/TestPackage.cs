using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SmokingTestLibrary;

using Mysoft.Map.Extensions.DAL;
using System.Data.SqlClient;
using SmokingTest.CS.Entity;
using System.Reflection;

namespace SmokingTest.DAL
{

	/// <summary>
	/// 封装测试用例的常用操作
	/// BY ZR 
	/// </summary>
	public static class TestPackage
	{


		/// <summary>
		/// 数据清除
		/// </summary>
		public static void ClearData(string tableName)
		{
			if( string.IsNullOrEmpty(tableName) )
				throw new ArgumentNullException("没有表名");
			string.Format("TRUNCATE TABLE {0}", tableName).AsCPQuery().ExecuteNonQuery();
		}
		/// <summary>
		/// 初始化测试表
		/// </summary>
		public static void InitData(string tableName, string resourcesScript)
		{
			if( string.IsNullOrEmpty(tableName) )
				throw new ArgumentNullException("没有表名");
			if( string.IsNullOrEmpty(resourcesScript) )
				throw new ArgumentNullException("资源不能为空");
			var query = String.Format("SELECT OBJECT_ID('{0}')",tableName).AsCPQuery();
			var objectId = query.ExecuteScalar<long>();
			if( objectId > 0 )
				string.Format("drop table [{0}]", tableName).AsCPQuery().ExecuteNonQuery();
			SqlHelper.ExecuteTSql(resourcesScript);
		}
		#region 断言相关异常是否产生方法
		/// <summary>
		///  断言相关异常是否产生方法
		/// </summary>
		/// <typeparam name="K"></typeparam>
		/// <param name="action"></param>
		/// <param name="func"></param>
		public static void AssertException<K>(Action action, Func<K, bool> func) where K : Exception
		{
			bool ok = false;
			try {
				action();
			}
			catch( Exception ex ) {
				if( ex.GetType() == typeof(K) )
					ok = func((K)ex);
				else
					throw;
			}
			Assert.AreEqual(ok, true);
			
		}
		/// <summary>
		/// 异常断言
		/// </summary>
		/// <typeparam name="K"></typeparam>
		/// <param name="action"></param>
		/// <param name="message"></param>
		public static void AssertException<K>(Action action,string message) where K : Exception
		{
			if( action == null ) {
				throw new ArgumentNullException("action 无效！");
			}
			if( string.IsNullOrEmpty(message) ) {
				throw new ArgumentNullException("message 无效！");
			}
			bool ok = false;
			try {
				action();
			}
			catch( Exception ex ) {
				if( ex.GetType() == typeof(K) ) {
					if( (ex as K).Message.IndexOf(message) > -1 ) {
						ok = true;
					}
					else {
						ok = false;
					}
				}
				else
					throw;
			}
			Assert.AreEqual(ok, true);

		}
		/// <summary>
		/// 异常断言
		/// </summary>
		/// <typeparam name="K"></typeparam>
		/// <param name="action"></param>
		public static void AssertException<K>(Action action) where K : Exception
		{
			if( action == null ) {
				throw new ArgumentNullException("action 无效！");
			}
			bool ok = false;
			try {
				action();
			}
			catch( Exception ex ) {
				if( ex.GetType() == typeof(K) ) {
					ok = true;
				}
				else
					throw;
			}
			Assert.AreEqual(ok, true);
		}

		//CodeReview : ...返回一个结构,包含set段,where段不是更好...

		/// <summary>
		/// 筛选出set 和 where 
		/// </summary>
		/// <param name="strSql">sql语句</param>
		/// <param name="strWhere"></param>
		/// <returns></returns>
		public static string SelectSetAndWhere(string strSql, ref string strWhere)
		{
			if( string.IsNullOrEmpty(strSql) )
				throw new ArgumentNullException("需要拼接好的SQL");
			int start = strSql.IndexOf("set");
			int end = strSql.IndexOf("where");
			if( end - start > 3 ) {
				strWhere = strSql.Substring(end + 5);
				return strSql.Substring(start + 3, end - start - 3);
			}
			return string.Empty;
		}

		#endregion
	}
}
