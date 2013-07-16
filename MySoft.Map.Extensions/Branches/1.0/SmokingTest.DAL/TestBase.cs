using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mysoft.Map.Extensions.DAL;

namespace SmokingTest.DAL
{
	/// <summary>
	/// 
	/// </summary>
	public abstract class TestBase : IDisposable
	{
		/// <summary>
		/// 
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = -1)]
		public void InitTestTable()
		{
			// 先删除要测试的数据表，避免数据干扰。
			DropTestTable();


			// 重新创建要测试的表
			@"CREATE TABLE [dbo].[TestTable](
				[RowId] [int] IDENTITY(1,1) NOT NULL,
				RowGuid uniqueidentifier  NOT NULL,
				[RowString] [nvarchar](50) NOT NULL,
				[RowImage] image,
				[RowNull] [nvarchar](50)
			) ON [PRIMARY]"
				.AsCPQuery().ExecuteNonQuery();
		}

		/// <summary>
		/// 
		/// </summary>
		private void DropTestTable()
		{
			var query = "select top 1 object_id from sys.objects where name = 'TestTable'".AsCPQuery();
			long objectId = query.ExecuteScalar<long>();

			if( objectId > 0 )
				"drop table [TestTable]".AsCPQuery().ExecuteNonQuery();

		}


		void IDisposable.Dispose()
		{
			DropTestTable();
		}

	}
}
