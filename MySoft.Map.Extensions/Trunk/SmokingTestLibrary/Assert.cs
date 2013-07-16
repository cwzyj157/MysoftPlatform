using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmokingTestLibrary
{
	public static class Assert
	{
		private static bool IsEqual<T>(T a, T b)
		{
			if( typeof(T) == typeof(string) ) {
				return (a as string) == (b as string);
			}
			else if( typeof(T).IsClass ) {
				return object.ReferenceEquals(a, b);
			}
			else {
				return EqualityComparer<T>.Default.Equals(a, b);
			}
		}


		public static void AreEqual<T>(T a, T b)
		{
			if( IsEqual<T>(a, b) == false )
				throw new AssertFailedException(string.Format("二个数据项不相等， a=【{0}】, b=【{1}】", a, b));
		}


		public static void AreNotEqual<T>(T a, T b)
		{
			if( IsEqual<T>(a, b) )
				throw new AssertFailedException(string.Format("二个数据项相等， a=【{0}】, b=【{1}】", a, b));
		}

	}
}
