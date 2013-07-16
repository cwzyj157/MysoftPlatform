using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace _Tool.AutoGenerateCode
{
	/// <summary>
	/// 
	/// </summary>
	public class CodeUtil
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="reader"></param>
		/// <returns></returns>
		public static string[] GetColumnNames(SqlDataReader reader)
		{
			int count = reader.FieldCount;
			string[] names = new string[count];
			for( int i = 0; i < count; i++ ) {
				names[i] = reader.GetName(i).ToLower();
			}
			return names;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="table"></param>
		/// <returns></returns>
		public static string[] GetColumnNames(DataTable table)
		{
			int count = table.Columns.Count;
			string[] names = new string[count];
			for( int i = 0; i < count; i++ ) {
				names[i] = table.Columns[i].ColumnName.ToLower();
			}
			return names;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="oldArray"></param>
		/// <returns></returns>
		public static byte[] ReverseArray(byte[] oldArray)
		{
			byte[] newArray = new byte[oldArray.Length];
			int index = oldArray.Length - 1;
			for( int i = 0; i < oldArray.Length; i++) {
				newArray[index] = oldArray[i];
				index--;
			}
			return newArray;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static long ByteToLong(byte[] value){
			return BitConverter.ToInt64(ReverseArray(value), 0);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static byte[] LongToByte(long value)
		{
			return ReverseArray(BitConverter.GetBytes(value));
		}
	}
}