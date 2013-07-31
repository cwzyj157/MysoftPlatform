using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskManagerLib.Model.DTO
{
	public class QueryTaskParam : PagingInfo
	{
		public DateTime Start { get; set; }

		public DateTime End { get; set; }

	}
}
