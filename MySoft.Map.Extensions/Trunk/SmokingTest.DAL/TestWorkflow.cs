using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Xml;

using SmokingTestLibrary;

using Mysoft.Map.Extensions;
using Mysoft.Map.Extensions.Workflow;
using Mysoft.Map.Extensions.DAL;
using Mysoft.Map.Extensions.Xml;


namespace SmokingTest.DAL
{
	/// <summary>
	/// 工作流封装测试
	/// </summary>
	[SmokingTestLibrary.Test(InNewThread = false, RunTimes = 5)]
	[NUnit.Framework.TestFixture]
	public class TestWorkflow
	{
		/// <summary>
		/// 测试从文件加载场景
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 1, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void TestLoadFromFile()
		{
			BusinessTypeManager btm = BusinessTypeManager.FromFile("Demo1_HTML.xml");
			Assert.AreEqual<int>(11, btm.BusinessType.Item.Count);
			DomainAssert(btm);
			GroupAssert(btm);
		}

		/// <summary>
		/// 测试从字符串加载场景
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 2, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void TestLoadFromString()
		{
			string xml = File.ReadAllText("Demo1_HTML.xml", Encoding.Default);
			BusinessTypeManager btm = BusinessTypeManager.FromXml(xml);
			Assert.AreEqual<int>(11, btm.BusinessType.Item.Count);
			DomainAssert(btm);
			GroupAssert(btm);
		}

		/// <summary>
		/// 测试各业务域的值是否正确
		/// </summary>
		/// <param name="btm">工作流封装</param>
		private void DomainAssert(BusinessTypeManager btm)
		{
			
			//验证Domain内的值是否正确序列化
			Domain domain = btm.GetDomain("IntVal");
			
			Assert.AreEqual<string>(domain.DefaultValue, "defaultVal");
			Assert.AreEqual<string>(domain.DisplayType, "text");
			Assert.AreEqual<string>(domain.DropdownOptions, "1|2");
			Assert.AreEqual<int>(domain.IsApprovemodify, 0);
			Assert.AreEqual<int>(domain.IsNull, 1);
			Assert.AreEqual<int>(domain.IsUpdate, 0);
			Assert.AreEqual<int>(domain.Length, 128);
			Assert.AreEqual<string>(domain.Name, "IntVal");
			Assert.AreEqual<string>(domain.Type, "int");
			Assert.AreEqual<string>(domain.Value, "138");

			//节点在XML文件中不存在的情况
			domain = btm.GetDomain("NotExists");
			Assert.AreEqual<object>(null, domain);
		}

		/// <summary>
		/// 测试循环域的值是否正确
		/// </summary>
		/// <param name="btm"></param>
		private void GroupAssert(BusinessTypeManager btm)
		{
			//验证Group内的值情况
			Group group = btm.GetGroup("TestGroup");
			Assert.AreEqual<string>(group.Name, "TestGroup");
			Assert.AreEqual<int>(group.GroupItems.Count, 1);
			GroupItem item = group.GroupItems[0];
			Assert.AreEqual<int>(item.Domains.Count, 4);
			Domain domain = item.Domains[0];
			Assert.AreEqual<string>(domain.Name, "Item1");
		}

		/// <summary>
		/// 从文件返回工作流封装,且设置数据格式
		/// </summary>
		/// <returns>工作流对象封装</returns>
		private BusinessTypeManager GetBtm()
		{
			BusinessTypeManager btm = BusinessTypeManager.FromFile("Demo1_HTML.xml");
			btm.OnDomainBinding += new EventHandler<BindEventArgs>(btm_DomainBinding);
			btm.SetFormat(DomainType.DateTime, "yyyy-MM-dd");
			btm.SetFormat(DomainType.Int, "#,##0.00");
			btm.SetFormat(DomainType.Money, "#,##0.00");
			return btm;
		}

		/// <summary>
		/// 测试XML中包含SQL节点,绑定的场景
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 3, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void TestBind()
		{
			BusinessTypeManager btm = GetBtm();
			btm.Bind(null);
			btm.BindGroup("合约规划使用明细", null);
			BindAssert(btm);
		}

		/// <summary>
		/// 测试代码编写SQL语句,绑定的场景
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 4, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void TestBindDataTable()
		{
			BusinessTypeManager btm = GetBtm();

			DataTable dt;
			string sql = @"SELECT
						'Code1'  AS [合同编码],
						'合同名称'  AS [合同名称],
						GETDATE() AS [签订日期],
						'abc' AS [经办人],
						555 AS [IntVal]";

			dt = CPQuery.From(sql).FillDataTable();
			btm.Bind(dt);

			sql = @"SELECT
				'拆分来源'  AS [拆分来源],
				'拆分类型'  AS [拆分类型],
				GETDATE() AS [科目编码],
				CAST(55.5 AS MONEY) AS [拆分金额]";

			dt = CPQuery.From(sql).FillDataTable();
			btm.BindGroup("合约规划使用明细", dt);

			BindAssert(btm);
		}

		/// <summary>
		/// 测试使用代码执行SQL.且通过字典映射填充业务域的场景
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 5, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void TestBindDataMapping()
		{
			BusinessTypeManager btm = GetBtm();

			DataTable dt;
			string sql = @"SELECT
						'Code1'  AS [A],
						'合同名称'  AS [B],
						GETDATE() AS [C],
						'abc' AS [D],
						555 AS [E]";
			Dictionary<string, string> dict1 = new Dictionary<string, string>() { 
				{ "A", "合同编码" },
				{ "B", "合同名称" },
				{ "C", "签订日期" },
				{ "D", "经办人" },
				{ "E", "IntVal" }
			};
			dt = CPQuery.From(sql).FillDataTable();

			BindOption bdo1 = new BindOption();
			bdo1.ColumnMap = dict1;

			btm.Bind(dt, bdo1);

			sql = @"SELECT
				'拆分来源'  AS [A],
				'拆分类型'  AS [B],
				GETDATE() AS [C],
				CAST(55.5 AS MONEY) AS [D]";
			Dictionary<string, string> dict2 = new Dictionary<string, string>() { 
				{ "A", "拆分来源" },
				{ "B", "拆分类型" },
				{ "C", "科目编码" },
				{ "D", "拆分金额" }
			};
			dt = CPQuery.From(sql).FillDataTable();

			BindOption bdo2 = new BindOption();
			bdo2.ColumnMap = dict2;

			btm.BindGroup("合约规划使用明细", dt, bdo2);

			BindAssert(btm);
		}

		private void BindAssert(BusinessTypeManager btm)
		{
			Domain domain = null;

			domain = btm.GetDomain("合同编码");
			Assert.AreEqual<string>(domain.Value, "Code1");

			domain = btm.GetDomain("合同名称");
			Assert.AreEqual<string>(domain.Value, "合同名称");

			domain = btm.GetDomain("签订日期");
			Assert.AreEqual<string>(domain.Value, DateTime.Now.ToString("yyyy-MM-dd"));

			int intVal = 555;
			domain = btm.GetDomain("IntVal");
			Assert.AreEqual<string>(domain.Value, intVal.ToString("#,##0.00"));

			domain = btm.GetDomain("经办人");
			Assert.AreEqual<string>(domain.Value, intVal.ToString("李俊峰"));


			Group group = btm.GetGroup("合约规划使用明细");
			Assert.AreEqual<int>(1, group.GroupItems.Count);
			GroupItem item = group.GroupItems[0];
			Assert.AreEqual<string>(item.Domains[0].Value, "拆分来源");
			Assert.AreEqual<string>(item.Domains[1].Value, "拆分类型");
			Assert.AreEqual<string>(item.Domains[2].Value, DateTime.Now.ToString("yyyy-MM-dd"));
			decimal decVal = 55.5m;
			Assert.AreEqual<string>(item.Domains[3].Value, decVal.ToString("#,##0.00"));

			domain = btm.GetDomain("合约规划使用明细列表比较域");
			Assert.AreEqual<bool>(true, string.IsNullOrEmpty(domain.Value));
		}

		private void btm_DomainBinding(object sender, BindEventArgs e)
		{
			if( e.Domain.Name == "经办人" ) {
				e.Domain.Value = "李俊峰";
			}
		}

		/// <summary>
		/// 测试循环域填充时,DataTable没有任何记录的场景
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 4, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void Test0Rows()
		{
			BusinessTypeManager btm = GetBtm();
			//即使数据库中没有记录.也要保持一行记录
			DataTable dt = new DataTable();
			btm.BindGroup("合约规划使用明细", dt);

			Group group = btm.GetGroup("合约规划使用明细");
			Assert.AreEqual<int>(1, group.GroupItems.Count);
		}

		/// <summary>
		/// 测试循环域自动赋值哈希业务域的场景
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 5, RunTimes = 1)]
		public void TestHash()
		{
			BusinessTypeManager btm = GetBtm();

			string sql = @"SELECT
				'拆分来源'  AS [A],
				'拆分类型'  AS [B],
				GETDATE() AS [C],
				CAST(55.5 AS MONEY) AS [D]";
			Dictionary<string, string> dict2 = new Dictionary<string, string>() { 
				{ "A", "拆分来源" },
				{ "B", "拆分类型" },
				{ "C", "科目编码" },
				{ "D", "拆分金额" }
			};
			List<string> hash = new List<string>() { "拆分来源", "拆分类型" };

			DataTable dt = CPQuery.From(sql).FillDataTable();

			BindOption bdo = new BindOption();
			bdo.ColumnMap = dict2;
			bdo.HashColumns = hash;

			btm.BindGroup("合约规划使用明细", dt, bdo);

			Domain domain = btm.GetDomain("合约规划使用明细列表比较域");
			Assert.AreEqual<bool>(false, string.IsNullOrEmpty(domain.Value));
		}

		/// <summary>
		/// 测试工作流封装转换回XML的场景
		/// </summary>
		[SmokingTestLibrary.TestMethod(Order = 6, RunTimes = 1)]
		[NUnit.Framework.Test]
		public void ToXML()
		{
			Guid guid = Guid.NewGuid();
			BusinessTypeManager btm = BusinessTypeManager.FromFile("Demo1_HTML.xml");
			btm.BusinessType.BusinessGUID = guid.ToString();

			string xml = btm.BusinessType.ToXml();

			XmlDocument doc = new XmlDocument();
			doc.LoadXml(xml);

			XmlNode node = doc.SelectSingleNode("/BusinessType/Item");
			Assert.AreEqual<int>(node.ChildNodes.Count, 11);

			XmlAttribute attr = doc.DocumentElement.Attributes["BusinessGUID"];

			Assert.AreEqual<string>(attr.Value, guid.ToString());
		}
	}
}
