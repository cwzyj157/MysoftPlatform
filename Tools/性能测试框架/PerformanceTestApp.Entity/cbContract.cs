using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mysoft.Map.Extensions.DAL;


namespace PerformanceTestApp.Entity
{
    [Serializable]
	[DataEntity(Alias = "cb_Contract")]
	public class cbContract : BaseEntity
	{
		public string ApproveState { get; set; }
		public string JsState { get; set; }
		public string HtProperty { get; set; }
		public string ContractCode { get; set; }
		public string ContractName { get; set; }
		public decimal? HtAmount { get; set; }
		public DateTime? SignDate { get; set; }
		public string YfCorporation { get; set; }
		public byte? IsLock { get; set; }
		public string ProjectNameList { get; set; }
	}
}
