using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using SmokingTestLibrary;

using Mysoft.Map.Extensions;
using Mysoft.Map.Extensions.Xml;
using Mysoft.Map.Extensions.Json;
using Mysoft.Map.Extensions.Web;

namespace SmokingTest.DAL
{
	/// <summary>
	/// 测试序列化
	/// </summary>
	[SmokingTestLibrary.Test(InNewThread = false, RunTimes = 3)]
	[NUnit.Framework.TestFixture]
	public class TestSerialize
	{
		/// <summary>
		/// 测试对象序列化为XML
		/// </summary>
		[TestMethod(Order = 1, RunTimes = 1)]
		public void TestToXML()
		{
			HttpResult hr = new HttpResult();
			hr.Result = true;
			hr.KeyValue = Guid.NewGuid().ToString();

			string xml1 = hr.ToXml();
			string xml2 = XmlHelper.XmlSerialize(hr, Encoding.Default);

			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(xml1);
			XmlAttribute attrResult = xmlDoc.DocumentElement.Attributes["result"];
			XmlAttribute attrKeyValue = xmlDoc.DocumentElement.Attributes["keyvalue"];

			Assert.AreEqual<string>(attrResult.Value, "true");
			Assert.AreEqual<string>(attrKeyValue.Value, hr.KeyValue);

			xmlDoc.LoadXml(xml2);
			attrResult = xmlDoc.DocumentElement.Attributes["result"];
			attrKeyValue = xmlDoc.DocumentElement.Attributes["keyvalue"];

			Assert.AreEqual<string>(attrResult.Value, "true");
			Assert.AreEqual<string>(attrKeyValue.Value, hr.KeyValue);

			HttpResult<string> hr2 = new HttpResult<string>();

			hr2.Result = true;
			hr2.KeyValue = Guid.NewGuid().ToString();
			hr2.Data = Guid.NewGuid().ToString();

			xml1 = hr2.ToXml();
			xml2 = XmlHelper.XmlSerialize(hr2, Encoding.Default);


			xmlDoc.LoadXml(xml1);
			attrResult = xmlDoc.DocumentElement.Attributes["result"];
			attrKeyValue = xmlDoc.DocumentElement.Attributes["keyvalue"];
			XmlAttribute attrData = xmlDoc.DocumentElement.Attributes["data"];

			Assert.AreEqual<string>(attrResult.Value, "true");
			Assert.AreEqual<string>(attrKeyValue.Value, hr2.KeyValue);
			Assert.AreEqual<string>(attrData.Value, hr2.Data);


			xmlDoc.LoadXml(xml2);
			attrResult = xmlDoc.DocumentElement.Attributes["result"];
			attrKeyValue = xmlDoc.DocumentElement.Attributes["keyvalue"];
			attrData = xmlDoc.DocumentElement.Attributes["data"];

			Assert.AreEqual<string>(attrResult.Value, "true");
			Assert.AreEqual<string>(attrKeyValue.Value, hr2.KeyValue);
			Assert.AreEqual<string>(attrData.Value, hr2.Data);
		}

		/// <summary>
		/// 测试XML反序列化为对象
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 2, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void TestFromXml()
		{
			string xml = @"<xml result='true' keyvalue='57013559-FDF3-4032-A784-2A4F45856B08'></xml>";
			HttpResult hr = xml.FromXml<HttpResult>();
			Assert.AreEqual<bool>(hr.Result, true);
			Assert.AreEqual<string>(hr.KeyValue, "57013559-FDF3-4032-A784-2A4F45856B08");

			hr = XmlHelper.XmlDeserialize<HttpResult>(xml, Encoding.Default);

			Assert.AreEqual<bool>(hr.Result, true);
			Assert.AreEqual<string>(hr.KeyValue, "57013559-FDF3-4032-A784-2A4F45856B08");


			xml = @"<xml result='true' keyvalue='57013559-FDF3-4032-A784-2A4F45856B08' data='57013559-FDF3-4032-A784-2A4F45856B08'></xml>";

			HttpResult<string> hr2 = xml.FromXml<HttpResult<string>>();


			Assert.AreEqual<bool>(hr2.Result, true);
			Assert.AreEqual<string>(hr2.KeyValue, "57013559-FDF3-4032-A784-2A4F45856B08");
			Assert.AreEqual<string>(hr2.Data, "57013559-FDF3-4032-A784-2A4F45856B08");
		}

		/// <summary>
		/// 测试对象序列化为JSON字符串
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 3, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void TestToJSON()
		{
			Guid guid = Guid.NewGuid();
			HttpResult hr = new HttpResult();
			hr.Result = true;
			hr.KeyValue = guid.ToString();

			string json = hr.ToJson();
			Assert.AreEqual<string>(json, "{\"Result\":true,\"KeyValue\":\"" + guid.ToString() + "\",\"ErrorMessage\":null}");

			json = JsonHelper.JsonSerialize(hr);
			Assert.AreEqual<string>(json, "{\"Result\":true,\"KeyValue\":\"" + guid.ToString() + "\",\"ErrorMessage\":null}");


			HttpResult<string> hr2 = new HttpResult<string>();

			hr2.Result = true;
			hr2.KeyValue = guid.ToString();
			hr2.Data = guid.ToString();

			json = hr2.ToJson();
			Assert.AreEqual<string>(json, "{\"Data\":\"" + guid.ToString() + "\",\"Result\":true,\"KeyValue\":\"" + guid.ToString() + "\",\"ErrorMessage\":null}");
		}

		/// <summary>
		/// 测试JSON字符串反序列化为对象
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 4, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void TestFromJSON()
		{
			Guid guid = Guid.NewGuid();
			string json = "{\"Result\":true,\"KeyValue\":\"" + guid.ToString() + "\",\"ErrorMessage\":null}";
			HttpResult hr = json.FromJson<HttpResult>();

			Assert.AreEqual<bool>(hr.Result, true);
			Assert.AreEqual<string>(hr.KeyValue, guid.ToString());

			hr = JsonHelper.JsonDeserialize<HttpResult>(json);

			Assert.AreEqual<bool>(hr.Result, true);
			Assert.AreEqual<string>(hr.KeyValue, guid.ToString());

			json = "{\"Data\":\"" + guid.ToString() + "\",\"Result\":true,\"KeyValue\":\"" + guid.ToString() + "\",\"ErrorMessage\":null}";

			HttpResult<string> hr2 = json.FromJson<HttpResult<string>>();

			Assert.AreEqual<bool>(hr2.Result, true);
			Assert.AreEqual<string>(hr2.KeyValue, guid.ToString());
			Assert.AreEqual<string>(hr2.Data, guid.ToString());

		}
	}
}
