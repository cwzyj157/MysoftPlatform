using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Mysoft.Map.Extensions.DAL;

namespace PerformanceTestApp.Entity
{
    [Serializable]
    [DataEntity(Alias = "tbl_ForEntity")]
    public partial class TblForEntity : BaseEntity
    {
        [DataColumn(PrimaryKey = true, SeqGuid = true)]
        public Guid EntityGuid { get; set; }
        [DataColumn(IsNullable = true)]
        public Guid? GuidValue { get; set; }
        [DataColumn(IsNullable = true)]
        public int? IntValue { get; set; }
        [DataColumn(IsNullable = true)]
        public decimal? MoneyValue { get; set; }
        [DataColumn(IsNullable = true)]
        public string VcharValue { get; set; }
        [DataColumn(IsNullable = true)]
        public DateTime? DtmValue { get; set; }
    }
}
