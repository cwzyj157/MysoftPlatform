using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskManagerLib.Caching
{
	public sealed class CacheResult<T>
	{
		internal CacheResult(T result, Exception ex)
		{
			_exception = ex;
			_result = result;
		}

		private Exception _exception;

		private T _result;

		public T Result
		{
			get
			{
				if( _exception != null )
					throw _exception;

				return _result;
			}
		}
	}
}
