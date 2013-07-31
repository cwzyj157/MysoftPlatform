using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskManagerLib.Exceptions
{
	[Serializable]
	public class ValidDataException : MyMessageException
	{
		public ValidDataException(string message)
			: base(message)
		{
		}
	}

	
}
