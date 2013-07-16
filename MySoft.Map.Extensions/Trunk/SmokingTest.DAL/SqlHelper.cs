using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mysoft.Map.Extensions.DAL;

namespace SmokingTest.DAL
{
	internal static class SqlHelper
	{
		public static void ExecuteTSql(string batch)
		{
			string[] lines = batch.Split(new string[] { "\r\nGO\r\n" }, StringSplitOptions.RemoveEmptyEntries);

			foreach( string sql in lines )
				CPQuery.Format(sql).ExecuteNonQuery();

		}
	}
}
