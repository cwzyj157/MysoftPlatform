using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmokingTestLibrary
{
	public class AssertFailedException : Exception
	{
		public AssertFailedException() : this("二个数据项不相等。")
		{

		}

		public AssertFailedException(string message)
			: base(message)
		{

		}
	}

	public class AssertTimeoutException : Exception
	{
		public AssertTimeoutException(long expect, long result)
			: base(string.Format("测试用例执行超时，预期时间 {0} 毫秒，实现执行时间 {1}", expect, result))
		{
		}
	}
}
