using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

using SmokingTestLibrary;

using Mysoft.Map.Extensions;
using Mysoft.Map.Extensions.Workflow;
using Mysoft.Map.Extensions.DAL;
using Mysoft.Map.Extensions.Xml;
using System.Reflection;
using System.Data.SqlClient;

namespace SmokingTest.DAL
{
	
	/// <summary>
	/// Xml保存测试
	/// </summary>
	//modify by 李俊峰 2013-05-29 由于外围API移除了对于SaveXML的操作.所以本用例也需要注释
	//[Test(InNewThread = false, RunTimes = 1)]
	public class TestSaveXml
	{

		/// <summary>
		/// 初始化表
		/// </summary>
		[TestMethod(Order = 0, RunTimes = 1)]
		public void InitData()
		{
			var query = "SELECT OBJECT_ID('TestDyContract')".AsCPQuery();
			var objectId = query.ExecuteScalar<long>();
			if( objectId > 0 ) {
				"drop table [TestDyContract]".AsCPQuery().ExecuteNonQuery();
			}

			query = "SELECT OBJECT_ID('TestDyRoom')".AsCPQuery();
			objectId = query.ExecuteScalar<long>();
			if( objectId > 0 ) {
				"drop table [TestDyRoom]".AsCPQuery().ExecuteNonQuery();
			}


			string sql = @"CREATE TABLE TestDyContract
			(
				ContractGUID UNIQUEIDENTIFIER,
				DyContractNo NVARCHAR(128),
				DyDate DATETIME,
				JkContractNo NVARCHAR(128),
				JkBank NVARCHAR(128),
				JkAmount MONEY,
				Pgcompany NVARCHAR(256),
				PgAmount MONEY,
				FkDate DATETIME,
				Remarks NTEXT
			)";

			sql.AsCPQuery().ExecuteNonQuery();

			sql = @"CREATE TABLE TestDyRoom
			(
				DyRoomGUID UNIQUEIDENTIFIER,
				DyContractGUID UNIQUEIDENTIFIER,
				RoomGUID UNIQUEIDENTIFIER,
				DyAmount MONEY,
				dyDate DATETIME,
				ZxNo nvarchar(128),
				ZxDate datetime,
				ZxYy nvarchar(128),
				DyMemo NTEXT
			)";

			sql.AsCPQuery().ExecuteNonQuery();

			InitAdditionalAPI();
		}


		private static void InitAdditionalAPI()
		{
			string path = Mysoft.Map.Extensions.CodeDom.BuildManager.BinDirectory + "Mysoft.Map.AdditionalAPI.dll";
			if( !System.IO.File.Exists(path) ) {
				throw new System.IO.FileNotFoundException("Mysoft.Map.AdditionalAPI.dll文件不存在!");
			}

			Assembly assembly = Assembly.LoadFile(path);

			Type type = assembly.GetType("Mysoft.Map.AdditionalAPI.XmlConvertor");
			if( type == null ) {
				throw new System.IO.FileNotFoundException("Mysoft.Map.AdditionalAPI.dll中未找到XmlConvertor类型!");
				throw new InvalidProgramException("Mysoft.Map.AdditionalAPI.dll中未找到XmlConvertor类型!");
			}

			MethodInfo miSaveXml = type.GetMethod("ParseAppFormXmlToSqlCommand", BindingFlags.Static | BindingFlags.Public);

			if( miSaveXml == null )
				throw new InvalidProgramException("没有找到期望的 Mysoft.Map.AdditionalAPI.XmlConvertor.ParseAppFormXmlToSqlCommand 方法。");

			Mysoft.Map.Extensions.Xml.XmlDataEntity.SaveXmlFunc = Delegate.CreateDelegate(typeof(Func<string, string, List<KeyValuePair<string, SqlCommand>>>), miSaveXml)
				as Func<string, string, List<KeyValuePair<string, SqlCommand>>>;
		}

		/// <summary>
		/// 测试主表保存
		/// </summary>
		[TestMethod(Order = 1, RunTimes = 1)]
		public void TestMaster()
		{
			string xml = @"<TestDyContract keyname='ContractGUID' keyvalue=''>
							<DyContractNo>lijf01</DyContractNo>
							<DyDate>2013-04-25</DyDate>
							<JkContractNo>no3</JkContractNo>
							<JkBank>招商银行</JkBank>
							<JkAmount>13800</JkAmount>
							<Pgcompany>GMC</Pgcompany>
							<PgAmount>23800</PgAmount>
							<FkDate>2013-04-26</FkDate>
							<Remarks>2222222</Remarks>
						</TestDyContract>";
			string oid;
			using( ConnectionScope scope = new ConnectionScope(TransactionMode.Required) ) {
				oid = XmlDataEntity.SaveMaster(xml);
				scope.Commit();

			}

			var parameter = new { ContractGUID = oid };

			CPQuery query = CPQuery.From("SELECT COUNT(*) FROM TestDyContract WHERE ContractGUID=@ContractGUID", parameter);
			int count = query.ExecuteScalar<int>();

			Assert.AreEqual<int>(count, 1);
		}

		/// <summary>
		/// 该用例已经废弃
		/// </summary>
		[TestMethod(Order = 2, RunTimes = 1)]
		public void TestDetail()
		{
			string xml = @"<UserData>
							<TestDyRoom keyname='DyRoomGUID' keyvalue=''>
								<RoomGUID>3b49b96a-92ef-4deb-877c-034d5a0b2b21</RoomGUID>
								<DyContractGUID></DyContractGUID>
								<DyAmount>2,122</DyAmount>
								<DyDate></DyDate>
								<ZxNo>111</ZxNo>
								<ZxDate>2013-04-28</ZxDate>
								<ZxYy></ZxYy>
								<DyMemo>111</DyMemo>
							</TestDyRoom>
							<TestDyRoom keyname='DyRoomGUID' keyvalue=''>
								<RoomGUID>49bdc087-fa00-4653-a1e5-0410da84c4f0</RoomGUID>
								<DyContractGUID></DyContractGUID>
								<DyAmount>222</DyAmount>
								<DyDate></DyDate>
								<ZxNo>111</ZxNo>
								<ZxDate></ZxDate>
								<ZxYy></ZxYy>
								<DyMemo>111</DyMemo>
							</TestDyRoom>
						</UserData>";
			List<string> oids;
			using( ConnectionScope scope = new ConnectionScope(TransactionMode.Required) ) {
				oids = XmlDataEntity.SaveDetail(xml);
				scope.Commit();

			}

			foreach( string oid in oids ) {
				var parameter = new { DyRoomGUID = oid };

				CPQuery query = CPQuery.From("SELECT COUNT(*) FROM TestDyRoom WHERE DyRoomGUID=@DyRoomGUID", parameter);
				int count = query.ExecuteScalar<int>();

				Assert.AreEqual<int>(count, 1);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		[TestMethod(Order = 3, RunTimes = 1)]
		public void TestAll()
		{
			string xml1 = @"<TestDyContract keyname='ContractGUID' keyvalue=''>
							<DyContractNo>lijf01</DyContractNo>
							<DyDate>2013-04-25</DyDate>
							<JkContractNo>no3</JkContractNo>
							<JkBank>招商银行</JkBank>
							<JkAmount>13800</JkAmount>
							<Pgcompany>GMC</Pgcompany>
							<PgAmount>23800</PgAmount>
							<FkDate>2013-04-26</FkDate>
							<Remarks>2222222</Remarks>
						</TestDyContract>";

			string xml2 = @"<UserData><TestDyRoom keyname='DyRoomGUID' keyvalue=''>
							<RoomGUID>3b49b96a-92ef-4deb-877c-034d5a0b2b21</RoomGUID>
							<DyContractGUID></DyContractGUID>
							<DyAmount>2,122</DyAmount>
							<DyDate></DyDate>
							<ZxNo>111</ZxNo>
							<ZxDate>2013-04-28</ZxDate>
							<ZxYy></ZxYy>
							<DyMemo>111</DyMemo>
						</TestDyRoom>
						<TestDyRoom keyname='DyRoomGUID' keyvalue=''>
							<RoomGUID>49bdc087-fa00-4653-a1e5-0410da84c4f0</RoomGUID>
							<DyContractGUID></DyContractGUID>
							<DyAmount>222</DyAmount>
							<DyDate></DyDate>
							<ZxNo>111</ZxNo>
							<ZxDate></ZxDate>
							<ZxYy></ZxYy>
							<DyMemo>111</DyMemo>
						</TestDyRoom></UserData>";
			string oid;
			List<string> oids;
			using( ConnectionScope scope = new ConnectionScope(TransactionMode.Required) ) {
				oid = XmlDataEntity.SaveMaster(xml1);
				oids = XmlDataEntity.SaveDetail(xml2, "DyContractGUID", oid);
				scope.Commit();
			}

			var parameter = new { ContractGUID = oid };

			CPQuery query = CPQuery.From("SELECT COUNT(*) FROM TestDyContract WHERE ContractGUID=@ContractGUID", parameter);
			int count = query.ExecuteScalar<int>();

			Assert.AreEqual<int>(count, 1);

			foreach( string id in oids ) {
				var parameter2 = new { DyRoomGUID = id };

				query = CPQuery.From("SELECT COUNT(*) FROM TestDyRoom WHERE DyRoomGUID=@DyRoomGUID", parameter2);
				count = query.ExecuteScalar<int>();
				Assert.AreEqual<int>(count, 1);
			}
		}
	}
}
