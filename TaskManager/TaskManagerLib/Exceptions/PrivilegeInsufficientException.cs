using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskManagerLib.Exceptions
{
	[Serializable]
	public sealed class PrivilegeInsufficientException : MyMessageException
	{
		// Methods
		public PrivilegeInsufficientException()
			: base("您没有执行这个操作的权限。")
		{
		}
	}
}
