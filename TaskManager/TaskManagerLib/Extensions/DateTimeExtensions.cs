using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskManagerLib
{
	public static class DateTimeExtensions
	{
		public static DateTime GetMonday(this DateTime dt)
		{
			if( dt.DayOfWeek == DayOfWeek.Monday )
				return dt;

			if( dt.DayOfWeek == DayOfWeek.Sunday )
				return dt.AddDays(-6);

			return dt.AddDays(-1 * ((int)dt.DayOfWeek - 1));
		}


		public static string ToDateString(this DateTime dt)
		{
			return dt.ToString("yyyy-MM-dd");
		}


	}
}
