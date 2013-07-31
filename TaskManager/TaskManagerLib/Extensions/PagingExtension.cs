using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagerLib.Model.DTO;

namespace TaskManagerLib
{
	public static class PagingExtension
	{
		public static List<T> GetPagingList<T>(this IQueryable<T> query, PagingInfo pagingInfo)
		{
			if( query == null )
				throw new ArgumentNullException("query");

			pagingInfo.RecCount = query.Count();

			List<T> list = query.Skip(pagingInfo.PageIndex * pagingInfo.PageSize).Take(pagingInfo.PageSize).ToList();

			if( list == null || list.Count == 0 ) {
				if( pagingInfo.PageIndex > 0 && pagingInfo.RecCount > 0 ) {
					pagingInfo.PageIndex = 0;
					list = query.Skip(pagingInfo.PageIndex * pagingInfo.PageSize).Take(pagingInfo.PageSize).ToList();
				}
			}

			return list;
		}
	}
}
