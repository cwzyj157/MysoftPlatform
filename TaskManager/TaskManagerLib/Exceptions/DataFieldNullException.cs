using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskManagerLib.Exceptions
{
	[Serializable]
	public class DataFieldNullException : ValidDataException
	{
		public DataFieldNullException(string fieldName)
			: base(string.Format("数据成员 {0} 不允许为空。", fieldName))
		{
		}
	}
}
