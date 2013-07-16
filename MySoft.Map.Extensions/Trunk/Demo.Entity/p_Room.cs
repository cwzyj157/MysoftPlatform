using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mysoft.Map.Extensions.DAL;

namespace Demo.Entity
{
	[DataEntity(Alias = "p_Room")]
	public partial class PRoom : BaseEntity
	{
		public Guid? RoomGUID { get; set; }
		public string RoomInfo { get; set; }
		public decimal? BldArea { get; set; }
		public decimal? TnArea { get; set; }
		public string DjArea { get; set; }
		public decimal? Price { get; set; }
		public decimal? TnPrice { get; set; }
		public decimal? Total { get; set; }
		public string RawDjArea { get; set; }
		public decimal? RawPrice { get; set; }
		public decimal? RawTnPrice { get; set; }
	}
}
