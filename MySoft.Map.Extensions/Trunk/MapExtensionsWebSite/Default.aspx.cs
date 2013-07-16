using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Mysoft.Map.Extensions;
using Mysoft.Map.Extensions.Web;
using Mysoft.Map.Extensions.Workflow;
using Mysoft.Map.Extensions.DAL;
using System.Text;
using System.Data;
using System.Data.SqlClient;

public partial class _Default : System.Web.UI.Page
{
	private void BusinessTypeManager_DEMO()
	{
		// 1. 创建一个BusinessTypeManager实例
		BusinessTypeManager manager = BusinessTypeManager.FromFile("/PubProject/......./CbContractBalanceSP_HTML.xml");

		// 2. 获取数据
		DataTable table = CPQuery.From(@"SELECT BUGUID as [公司GUID], ProjGUID as [项目GUID], .......
										FROM ... WHERE ..... ").FillDataTable();

		// 3. 将查询结果绑定到所有Domain节点的 innerText 属性
		// 对于每个Doamin节点，BusinessTypeManager会根据name属性值做为列名从table中获取数据，然后未完成赋值操作。
		manager.Bind(table);


		// ---------------------------------------------------
		// 继续使用前面的 manager 实例

		// 1. 为第一个Group获取数据
		DataTable table1 = CPQuery.From(@"SELECT BudgetName AS [合约规划名称], CostShortName AS [科目名称], .... 
									FROM .... WHERE ...........").FillDataTable();
		// 2. 为第一个Group绑定数据
		manager.BindGroup("合约规划使用明细列表", table1);


		// 3. 为第二个Group获取数据
		DataTable table2 = CPQuery.From(@"SELECT ZrrName AS [责任人], DeptName AS [责任部门], .... 
									FROM .... WHERE ...........").FillDataTable();
		// 4. 为第二个Group绑定数据
		manager.BindGroup("部门费用列表", table2);
	}


	private void BusinessTypeManager_AdvDEMO()
	{

		//从文件加载BusinessType类实例
		BusinessTypeManager btm = BusinessTypeManager.FromFile("/PubProject/......./CbContractBalanceSP_HTML.xml");

		//查询SQL
		string sql = @"SELECT
				BudgetName,
				CostName,
				BudgetAmount,
				CfAmount
			   FROM ...";

		//映射关系 { ColumnName, DomainName }
		Dictionary<string, string> dict = new Dictionary<string, string>() { 
	{ "BudgetName", "合约规划名称" },
	{ "CostName", "科目名称" },
	{ "BudgetAmount", "合约规划金额" },
	{ "CfAmount", "合同对应金额" }
};

		//HashColumns仅在BindGroup中有效
		//需要计算哈希值的字段,使用映射后的名称
		//[合约规划金额][合同对应金额]这两个字段的值将被逐行拼接,并且对拼接后的字符串做GetHashCode()操作
		//最后将得到的哈希值写入[合约规划使用明细列表比较域]中
		List<string> hash = new List<string>() { "合约规划金额", "合同对应金额" };

		//数据表
		DataTable dt = CPQuery.From(sql).FillDataTable();

		//声明绑定对象
		BindOption bo = new BindOption();
		bo.ColumnMap = dict;
		bo.HashColumns = hash;
		//IdentityDomain仅在BindGroup中有效
		bo.IdentityDomain = "序号";  //循环域中的[序号]域将被填充为1,2,3,4....序列

		//绑定循环域
		btm.BindGroup("合约规划使用明细", dt, bo);


	}

	private void HttpResult_DEMO()
	{
		// 1. 创建一个HttpResult实例
		HttpResult result = new HttpResult { Result = false, ErrorMessage = "错误原因错误原因错误原因" };

		// 2. 得到返回值
		//    XML格式的返回值
		// string xml = result.ToXML();

		// 建议采用JSON返回值，可以这样写：
		string json = result.ToJson();
		Response.Write(json);
	}

	private void HttpResult2_DEMO()
	{
		// 1. 创建一个HttpResult实例
		HttpResult<string> result = new HttpResult<string> { Result = true };
		result.Data = "aaaaaaaaaaaaaaaaaa";

		// 2. 得到返回值
		string json = result.ToJson();
		Response.Write(json);
	}

	private void BatchData_DEMO()
	{
		Guid buGuid = Guid.NewGuid();
		Guid projGuid = Guid.NewGuid();
		Guid bldGuid = Guid.NewGuid();

//准备房间数据
DataTable dt = new DataTable();
dt.Columns.Add("RoomGUID", typeof(Guid));
dt.Columns.Add("BUGUID", typeof(Guid));
dt.Columns.Add("ProjGUID", typeof(Guid));
dt.Columns.Add("BldGUID", typeof(Guid));
dt.Columns.Add("Unit", typeof(string));
dt.Columns.Add("Floor", typeof(string));
dt.Columns.Add("No", typeof(string));
dt.Columns.Add("Room", typeof(string));
dt.Columns.Add("RoomCode", typeof(string));
dt.Columns.Add("HuXing", typeof(string));
dt.Columns.Add("Status", typeof(string));
dt.Columns.Add("BldArea", typeof(decimal));
dt.Columns.Add("TnArea", typeof(decimal));
//...其他字段

//模拟生成1W个房间
Random rnd = new Random();
for( int i = 0; i < 10000; i++ ) {
	DataRow row = dt.NewRow();
	row["RoomGUID"] = Guid.NewGuid();
	row["BUGUID"] = buGuid;
	row["ProjGUID"] = projGuid;
	row["BldGUID"] = bldGuid;
	row["Unit"] = "...";
	row["Floor"] = "...";
	row["No"] = "...";
	row["Room"] = "...";
	row["RoomCode"] = "...";
	row["HuXing"] = "...";
	row["Status"] = "销控";
	row["BldArea"] = 100m * (decimal)rnd.NextDouble();
	row["TnArea"] = 100m * (decimal)rnd.NextDouble();
	//...其他字段
	dt.Rows.Add(row);
}

//这里使用了事务,如果不使用事务,请使用ConnectionScope类的无参构造函数
using( ConnectionScope scope = new ConnectionScope(Mysoft.Map.Extensions.DAL.TransactionMode.Required) ) {

	//创建SqlBulkCopy对象,并设定引发触发器动作
	SqlBulkCopy bulkCopy = scope.CreateSqlBulkCopy(SqlBulkCopyOptions.FireTriggers);

	//设置写入目标表名
	bulkCopy.DestinationTableName = "TestBulkCopy";

	//写入数据
	bulkCopy.WriteToServer(dt);

	//其他数据库操作
	//update OtherTable set Column1 = @Column1....

	//提交事务,如果不启用事务,无需Commit();
	scope.Commit();
}
	}


	protected void Page_Load(object sender, EventArgs e)
	{
//        string xml = System.IO.File.ReadAllText(@"E:\Work\平台部门工作\数据访问层\标准258\明源整体解决方案\map\PubProject\workflow\cbgl\CbHTFkSP_HTML.xml", Encoding.Default);

//        BusinessTypeManager btm = BusinessTypeManager.FromXml(xml);

//        btm.SetFormat(DomainType.DateTime, "yyyy-MM-dd");
//        btm.SetFormat(DomainType.Money, "#,##0.00");

//        btm.DomainBinding += new EventHandler<BindEventArgs>(btm_DomainBinding);

//        string sql = @"SELECT a.[Subject] as [申请主题],a.[ApplyCode] as [申请编号],a.[ApplyDate] as [申请日期],b.[ContractCode] as [合同编号],b.[ProjectNameList],b.[ContractName] as [合同名称],b.[HtAmount] AS [合同有效签约金额],dbo.fn_ChnMoney_New(b.[HtAmount]) as [合同有效签约金额（大写）],a.[FundType],a.[FundName],a.[PayProviderName],b.[YfProviderName],a.[ReceiveProviderName],a.[BankName] 
//						,a.[ApplyType],a.[ApplyAmount],a.[DfdkAmount],a.[YfAmount],dbo.fn_ChnMoney_New(a.[YfAmount]) as YfAmountCaps,a.[BankAccounts],b.[HtAmount_Bz],(b.[HtAmount_Bz] + b.[SumAlterAmount_Bz]) AS HtDtAmount_Bz 
//						,Isnull((SELECT SUM(PayAmount) FROM cb_Pay WHERE [ContractGUID] = b.[ContractGUID]) ,0) AS SumPaidAmount 
//						,dbo.fn_ChnMoney_New(Isnull((SELECT SUM(PayAmount) FROM cb_Pay WHERE [ContractGUID] = b.[ContractGUID]) ,0)) AS SumPaidAmountCaps 
//						,CASE a.[HtDtAmount] WHEN 0 THEN 0 ELSE Isnull((SELECT SUM(PayAmount) FROM cb_Pay WHERE [ContractGUID] = b.[ContractGUID]) ,0)/a.[HtDtAmount]*100 END AS SumPaidAmountRate 
//						,(a.[HtDtAmount]-Isnull((SELECT SUM(PayAmount) FROM cb_Pay WHERE [ContractGUID] = b.[ContractGUID]) ,0)) AS SumWfAmount 
//						,a.[ApplyAmount_Bz],a.[HtDtAmount],b.[JsBxAmount], (CASE isnull(a.rate,0) WHEN 0 THEN 1 ELSE a.rate END * a.RemainAmount) AS [RemainAmount],a.[AppliedByName],c.[BuName],a.[BalanceAmount],b.[JsAmount],a.[SumScheduleAmount] 
//						,dbo.fn_ChnMoney_New(a.[SumScheduleAmount]) as SumScheduleAmountCaps,a.[SumApplyAmount],a.[YfAmount_Bz],dbo.fn_ChnMoney(isnull(a.[YfAmount_Bz] ,0)) as UpperYfAmount_Bz,a.BudgetInfo,a.StockInfo 
//						,a.MonthContractRemainAmount,a.[ScheduleConsultAmount],a.[MonthContractPlanAmount],a.[MonthContractSPAmount],a.[MonthContractApplyAmount] 
//						,a.[MonthPersonPlanAmount],a.[MonthPersonSpAmount],a.[MonthPersonApplyAmount],a.[MonthPersonRemainAmount],a.[MonthDeptPlanAmount],a.[MonthDeptSpAmount] 
//						,a.[MonthDeptApplyAmount],a.[MonthDeptRemainAmount],a.[ApplyRemarks],isnull(a.IsSpecialFlow,0) as IsSpecialFlow 
//					 FROM [cb_HTFKApply] a	LEFT JOIN [cb_Contract] b ON a.[ContractGUID]=b.[ContractGUID] LEFT JOIN myBusinessUnit c ON c.BUGUID=a.ApplyBUGUID 
//					 WHERE a.[HTFKApplyGUID]='6DF321AD-D906-414D-BC50-CCE947B38D05'";

//        DataTable dt = CPQuery.From(sql).FillDataTable();

//        btm.BusinessGUID = "sdfsdf";

//        btm.Bind(dt);

//        btm.BindDomain("付款说明", "木有说明");

//        sql = @"SELECT  a.costguid,b.LayoutSpare,a.CfAmount as [对应金额],c.ProjName as [项目名称],b.CostshortName + '(' + a.CostCode + ')' AS [科目名称],cb.BudgetName as [合约规划名称],cb.BudgetAmount as [合约规划金额],a.RefGUID,a.ProjectCode,a.CostCode,dbo.fn_GetEffectsLayoutSpare(a.RefGUID,'付款申请',a.ProjectCode,a.CostCode) AS [生效后规划余量] 
//                             FROM cb_BudgetUse a  
//                                 LEFT JOIN cb_Cost b ON b.CostGUID=a.CostGUID 
//                                 LEFT JOIN p_Project c ON c.ProjCode=a.ProjectCode  
//                                 LEFT JOIN cb_Budget cb ON cb.BudgetGUID = a.BudgetGUID  
//                             WHERE a.RefGUID='6DF321AD-D906-414D-BC50-CCE947B38D05' 
//                             ORDER BY a.CostCode";

//        dt = CPQuery.From(sql).FillDataTable();
//        List<string> list = new List<string>() { "对应金额", "项目名称" };

//        btm.BindGroup("合约规划列表", dt);

//        string file = @"E:\Work\平台部门工作\数据访问层\Src\Branches\1.0\MapExtensionsWebSite\App_Data\workflow.xml";
//        if( System.IO.File.Exists(file) ) {
//            System.IO.File.Delete(file);
//        }
//        System.IO.File.WriteAllText(file, btm.ToXML(), Encoding.Default);

//        A a = new A();
//        a.StringValue = "sdfsdfsdfdf";
//        a.DatetimeValue = DateTime.Now;
//        a.GuidValue = Guid.NewGuid();

//        string str = a.ToJSON();
//        A b = str.FromJSON<A>();

//        HttpResult hr = new HttpResult();
//        hr.Result = true;
//        hr.KeyValue = Guid.NewGuid().ToString();

//        string s1  = hr.ToJSON();
//        string s2 = hr.ToXML();
	}
}
