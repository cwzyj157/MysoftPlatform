using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskManagerLib.Model.DTO
{
	public class PagingInfo
	{
		// Fields
		public int PageIndex { get; set; }
		public int PageSize { get; set; }
		public int RecCount { get; set; }

		// Methods
		public int CalcPageCount()
		{
			if( this.PageSize != 0 && this.RecCount != 0 )
				return (int)Math.Ceiling((double)(((double)this.RecCount) / ((double)this.PageSize)));
			return 0;
		}
	}

}
