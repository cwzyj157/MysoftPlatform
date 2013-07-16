using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Mysoft.Map.Extensions;
using Mysoft.Map.Extensions.DAL;
using Mysoft.Map.Extensions.Xml;
using Mysoft.Map.Extensions.Web;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using System.Data;
using Mysoft.Map.Extensions.Exception;

/// <summary>
///Class1 的摘要说明
/// </summary>
public class Class1
{
	public Class1()
	{


		//    public class Project{
		//    }

		//        public void test(){

		//            //新API提供了XML序列化与反序列化功能
		//            //直接结合第一批数据访问API提供的实体功能
		//            public class cbHTFKApply{
		//                [XmlAttribute]
		//                public string HTFKApplyGUID { get; set; }
		//                [XmlAttribute]
		//                public string ContractGUID { get; set; }
		//                [XmlAttribute]
		//                public string Subject { get; set; }
		//            }

		//            //直接将SQL语句结果生成实体,无需硬编码,遍历DataTable
		//            List<cbHTFKApply> list = CPQuery.From("SELECT * FROM vcb_HTFKApply WHERE ...")
		//                .ToList<cbHTFKApply>();

		//            //直接序列化为XML字符串,无需考虑转义问题
		//            string xml = list.ToXML();

		//            string xml2 = "<cbHTFKApply>.....";
		//            //反序列化到对象也很简单
		//            cbHTFKApply apply = xml2.FromXml<cbHTFKApply>();


		//        //新API直接封装了工作流XML对象,可以直接进行序列化,绑定
		//        BusinessTypeManager btm = BusinessTypeManager.FromFile("/PubProject/.../ExpenseApprove_HTML.xml");

		//        DataTable dt = CPQuery.From(@"SELECT ExpenseAmount AS [报销金额(￥)], BalanceAmount AS [冲账金额(￥)] 
		//				FROM Expense WHERE ...").FillDataTable();

		//        //使用DataTable直接绑定
		//        //如果没在SQL中写别名,还可以通过指定Dictionary的方式,映射绑定
		//        btm.Bind(dt);

		//        //填充单一单元格
		//        btm.BindDomain("应付金额(￥)", "55.6");


		//        //新API也可以直接绑定循环域
		//        BusinessTypeManager btm = BusinessTypeManager.FromFile("/PubProject/.../DJApprove.xml");

		//        DataTable dt = CPQuery.From(@"SELECT BldFullName AS [楼栋], zts AS [总套数] 
		//			FROM p_room WHERE ...").FillDataTable();

		//        btm.BindGroup("本次定价审批信息比较域", dt);


		//            //结合第一批提供的数据访问层
		//            Project proj = CPQuery.From("SELECT * FROM p_Project WHERE ...")
		//                .ToSingle<Project>();

		//            //新API提供的帮助类
		//            string html = HtmlHelper.GetHtml("/Cbgl/PUB/ProjInfo.xsl", proj);

		//            //也可以转义xml字符串
		//            string html = HtmlHelper.GetHtml("/Cbgl/PUB/ProjInfo.xsl", "<Project>....");
		//        }



	}

	//加载领借款界面
	public void LoadLoan()
	{
		//查询数据库对象
		CbLoan loan = CPQuery.From(@"SELECT LoanGUID, LoanCode ... 
			FROM cb_Loan WHERE ...").ToSingle<CbLoan>();

		//保存原始状态值到UI
		CbLoan oldLoan = new CbLoan();
		oldLoan.ApplyState = loan.ApplyState;
		//oldLoan.xxxx = loan.xxxx; //其他需要做并发检查的字段
		//...

		//将对象序列化到UI中
		//HtmlHelper.RegisterHiddenInput("__ApproveState", oldLoan);

		//绑定界面UI
		//txtXXXX.Text = loan.xxxx; //需要绑定的字段...
		//...
	}

	//后台更新领借款逻辑
	public void UpdateLoan()
	{
		//从UI搜集数据,并赋值给新对象
		//CbLoan newLoan = new CbLoan();
		//newLoan.LoanGUID = new Guid(Request.Form["LoanGUID"]);//为属性赋值...
		//...

		////恢复旧对象的原始值
		//CbLoan oldLoan = HtmlHelper.GetObjectFromHiddenInput<CbLoan>("__ApproveState");

		////启用事务,如果并发检测失败,事务将回滚.
		//using( ConnectionScope scope = 
		//    new ConnectionScope(TransactionMode.Required) ) {
		//    try {
		//        //带并发检测的更新
		//        newLoan.Update(oldLoan, ConcurrencyMode.OriginalValue);
		//    }
		//    catch( OptimisticConcurrencyException ) {
		//        //并发检测失败,将会抛出OptimisticConcurrencyException异常
		//        throw new Exception("当前编辑的领借款已被其他人更改,请刷新页面后再操作。");
		//    }
		//    //其他操作数据库的业务逻辑
		//}
	}


	//    //加载合同界面
	//    public void LoadCbContract()
	//    {
	//        //查询数据库对象
	//        CbContract contract = CPQuery.From(@"SELECT ContractGUID, ContractCode ... 
	//			FROM cb_Contract WHERE ...").ToSingle<CbContract>();

	//        //保存时间戳值到UI
	//        CbContract oldContract = new CbContract();
	//        oldContract.TimeStampField = contract.TimeStampField; //只需要时间戳字段即可

	//        //将对象序列化到UI中
	//        HtmlHelper.RegisterHiddenInput("__Contract", oldContract);

	//        //绑定界面UI
	//        //txtXXXX.Text = contract.xxxx; //需要绑定的字段...
	//        //...
	//    }

	//    //后台更新合同逻辑
	//    public void UpdateCbContract()
	//    {
	//        //从UI搜集数据,并赋值给新对象
	//        CbContract newContract = new CbContract();
	//        //newContract.ContractGUID = new Guid(Request.Form["ContractGUID"]);//为属性赋值...
	//        //...

	//        //恢复旧对象的原始值
	//        CbContract oldContract = HtmlHelper.GetObjectFromHiddenInput<string>("__Contract");

	//        //启用事务,如果并发检测失败,事务将回滚.
	//        using( ConnectionScope scope =
	//            new ConnectionScope(TransactionMode.Required) ) {
	//            try {
	//                //带并发检测的更新
	//                newContract.Update(oldContract, ConcurrencyMode.TimeStamp);
	//            }
	//            catch( OptimisticConcurrencyException ex ) {
	//                //并发检测失败,将会抛出OptimisticConcurrencyException异常
	//                throw new Exception("当前编辑的合同已被其他人更改,请刷新页面后再操作。");
	//            }
	//            //其他操作数据库的业务逻辑
	//        }
	//    }


	//    //加载辅助核算对照列表
	//    public void LoadHsXmList()
	//    {
	//        //查询从表数据
	//        List<PCwjkHsxmMap> listHsxmMap = CPQuery.From(@"SELECT HsxmMapGUID, HsxmCode, HsxmName, ... 
	//			FROM p_CwjkHsxmMap WHERE HsTypeGUID = ... ").ToList<PCwjkHsxmMap>();

	//        //得到主表的版本字段(可以是时间戳,或自己维护的自增长值)
	//        //本例中使用自增长值
	//        List<PCwjkHsxm> listHsxmMap = CPQuery.From(@"SELECT XmVersion FROM p_CwjkHsxm WHERE HsTypeGUID = ... ").ToList<PCwjkHsxm>();

	//        //绑定数据
	//        //RepeaterHsmx.DataSource = listHsxmMap;
	//        //RepeaterHsmx.DataBind();
	//    }

	//    //后台更新辅助核算对照列表
	//    public void UpdateHxMxList()
	//    {

	//    }

	public string XmlToSingle()
	{

		//AppForm返回的dataxml
		string xml = @"<Product keyname='ProductGUID' keyvalue=''>
						<ProductName>产品名称一</ProductName>
						<ModifyDate>2013-04-25</ModifyDate>
						<Amount>23800</Amount>
						<Remarks>这里是备注</Remarks>
					</Product>";

		//使用统一的返回值对象来声明返回值
		HttpResult hr = new HttpResult();

		//将xml直接转换为Product实体
		Product product = XmlDataEntity.ConvertXmlToSingle<Product>(xml);

		//启用事务,可以与其他数据库操作共用一个事务
		using( ConnectionScope scope = new ConnectionScope(TransactionMode.Required) ) {

			//主键为空表示是新增状态
			if( product.ProductGUID == Guid.Empty ) {

				//对主键赋值,如果系统中已经使用有序GUID,请使用有序GUID
				product.ProductGUID = Guid.NewGuid();
				product.Insert(); //插入数据库
			}
			else {

				product.Update(); //更新数据库
			}

			//...其他数据库操作

			hr.Result = true;

			//提交事务
			scope.Commit();
		}

		//为返回值的KeyValue属性赋值
		hr.KeyValue = product.ProductGUID.ToString();
		return hr.ToXml(); //调用xml序列化扩展方法,直接序列化为<xml result=...格式的xml


	}

	public string XmlToList()
	{
		//xml文本,第二层节点采用AppFrom格式的Xml
		string xml = @"<UserData>
				<p_room keyname='RoomGUID' keyvalue=''>
					<RoomCode>1-1</RoomCode>
					<BldArea>44.56</BldArea>
				</p_room>
				<p_room keyname='RoomGUID' keyvalue=''>
					<RoomCode>1-2</RoomCode>
					<BldArea>55.67</BldArea>
				</p_room>
			</UserData>";

		//使用统一的返回值对象来声明返回值
		HttpResult hr = new HttpResult();

		List<PRoom> rooms = XmlDataEntity.ConvertXmlToList<PRoom>(xml);

		//启用事务,可以与其他数据库操作共用一个事务
		using( ConnectionScope scope = new ConnectionScope(TransactionMode.Required) ) {

			foreach( PRoom room in rooms ) {

				//主键为空表示是新增状态
				if( room.RoomGUID == Guid.Empty ) {
					//对主键赋值,如果系统中已经使用有序GUID,请使用有序GUID
					room.RoomGUID = Guid.NewGuid();
					room.Insert();
				}
				else {
					room.Update(); //更新数据库
				}
			}

			//...其他数据库操作

			hr.Result = true;

			//提交事务
			scope.Commit();
		}

		return hr.ToXml();
	}

	public string OneToMany()
	{
//主表xml文本
string xmlMaster = @"<TestDyContract keyname='DyContractGUID' keyvalue=''>
                <DyContractNo>lijf01</DyContractNo>
                <DyDate>2013-04-25</DyDate>
                <JkContractNo>no3</JkContractNo>
                <JkBank>招商银行</JkBank>
                <JkAmount>13800</JkAmount>
                <Pgcompany>GMC</Pgcompany>
                <PgAmount>23800</PgAmount>
                <Remarks>2222222</Remarks>
            </TestDyContract>";

//从表xml文本
string xmlDetail = @"<TestDyRoom keyname='DyRoomGUID' keyvalue=''>
                <RoomGUID>3b49b96a-92ef-4deb-877c-034d5a0b2b21</RoomGUID>
                <DyContractGUID></DyContractGUID>
                <DyAmount>2,122</DyAmount>
                <DyDate></DyDate>
                <ZxNo>111</ZxNo>
                <DyMemo>111</DyMemo>
            </TestDyRoom>
            <TestDyRoom keyname='DyRoomGUID' keyvalue=''>
                <RoomGUID>49bdc087-fa00-4653-a1e5-0410da84c4f0</RoomGUID>
                <DyContractGUID></DyContractGUID>
                <DyAmount>222</DyAmount>
                <DyDate></DyDate>
                <ZxNo>111</ZxNo>
                <DyMemo>111</DyMemo>
            </TestDyRoom>";

HttpResult hr = new HttpResult();

//直接转换主从表xml为实体
TestDyContract contract = XmlDataEntity.ConvertXmlToSingle<TestDyContract>(xmlMaster);
List<TestDyRoom> rooms = XmlDataEntity.ConvertXmlToList<TestDyRoom>(xmlDetail);

//启用事务,主从表共用一个事务保存
using( ConnectionScope scope = new ConnectionScope(TransactionMode.Required) ) {

	//保存主表
	if( contract.DyContractGUID == Guid.Empty ) {

		//主键为空表示是新增状态
		//对主键赋值,如果系统中已经使用有序GUID,请使用有序GUID
		contract.DyContractGUID = Guid.NewGuid();
		contract.Insert(); //插入数据库
	}
	else {
		contract.Update(); //更新数据库
	}

	//保存从表
	foreach( TestDyRoom room in rooms ) {

		//外键字段
		room.DyContractGUID = contract.DyContractGUID;

		if( room.RoomGUID == Guid.Empty ) {

			//主键为空表示是新增状态
			//对主键赋值,如果系统中已经使用有序GUID,请使用有序GUID
			room.RoomGUID = Guid.NewGuid();
			room.Insert(); //插入数据库
		}
		else {
			room.Update(); //更新数据库
		}
	}

	//...其他数据库操作

	hr.Result = true;

	//提交事务
	scope.Commit();
}

hr.KeyValue = contract.DyContractGUID.ToString();
return hr.ToXml();

	}

	public void CUD(string dataXml)
	{

		////将AppForm提交的DataXml直接转换为实体
		//CbContract contract = XmlDataEntity.ConvertXmlToSingle<CbContract>(dataXml);

		////主键为空表示新增
		//if( contract.ContractGUID == Guid.Empty ) {
		//    //给主键赋值,如果系统启用有序GUID,请使用有序GUID
		//    contract.ContractGUID = Guid.NewGuid(); 
		//    contract.Insert(); //插入数据
		//}
		//else {
		//    contract.Update(); //更新数据
		//}

		////如果要删除合同只,需要对主键赋值后调用Delete()即可
		//CbContract contractForDel = new CbContract();
		//contractForDel.ContractGUID = Guid.NewGuid("主键值");
		//contractForDel.Delete();

	}
}


public class TestDyContract : BaseEntity
{
	public Guid DyContractGUID { get; set; }
}

public class TestDyRoom : BaseEntity
{
	public Guid RoomGUID { get; set; }
	public Guid DyContractGUID { get; set; }
}

public class Product : BaseEntity
{
	public Guid ProductGUID;
}

public class PRoom : BaseEntity
{
	public Guid RoomGUID;
}


[Serializable]
[DataEntity(Alias = "cb_Loan")]
public partial class CbLoan : BaseEntity
{
	[DataColumn(IsNullable = true)]
	public Guid? LoanGUID { get; set; }
	[DataColumn(IsNullable = true)]
	public Guid? BUGUID { get; set; }
	[DataColumn(IsNullable = true)]
	public string Subject { get; set; }
	[DataColumn(IsNullable = true)]
	public string LoanCode { get; set; }
	[DataColumn(IsNullable = true)]
	public string ApplyState { get; set; }
	[DataColumn(IsNullable = true)]
	public Guid? AppliedBy { get; set; }
	[DataColumn(IsNullable = true)]
	public string AppliedByName { get; set; }
	[DataColumn(IsNullable = true)]
	public Guid? ApplyBUGUID { get; set; }
	[DataColumn(IsNullable = true)]
	public DateTime? ApplyDate { get; set; }
	[DataColumn(IsNullable = true)]
	public string PayMode { get; set; }
	[DataColumn(IsNullable = true)]
	public Guid? PayProviderGUID { get; set; }
	[DataColumn(IsNullable = true)]
	public string PayProviderName { get; set; }
	[DataColumn(IsNullable = true)]
	public Guid? ReceiveProviderGUID { get; set; }
	[DataColumn(IsNullable = true)]
	public string ReceiveProviderName { get; set; }
	[DataColumn(IsNullable = true)]
	public string BankName { get; set; }
	[DataColumn(IsNullable = true)]
	public string BankAccounts { get; set; }
	[DataColumn(IsNullable = true)]
	public Guid? CurrencyGUID { get; set; }
	[DataColumn(Alias = "LoanAmount_Bz", IsNullable = true)]
	public decimal? LoanAmountBz { get; set; }
	[DataColumn(IsNullable = true)]
	public decimal? LoanAmount { get; set; }
	[DataColumn(Alias = "BalanceAmount_Bz", IsNullable = true)]
	public decimal? BalanceAmountBz { get; set; }
	[DataColumn(IsNullable = true)]
	public decimal? BalanceAmount { get; set; }
	[DataColumn(Alias = "RemainAmount_Bz", IsNullable = true)]
	public decimal? RemainAmountBz { get; set; }
	[DataColumn(IsNullable = true)]
	public decimal? RemainAmount { get; set; }
	[DataColumn(IsNullable = true)]
	public byte? IsAccount { get; set; }
	[DataColumn(IsNullable = true)]
	public string Remarks { get; set; }
	[DataColumn(IsNullable = true)]
	public byte? IfLocked { get; set; }
	[DataColumn(IsNullable = true)]
	public string PayState { get; set; }
	[DataColumn(IsNullable = true)]
	public Guid? LoanTypeGUID { get; set; }
	[DataColumn(IsNullable = true)]
	public byte? IfCqbyj { get; set; }
	[DataColumn(IsNullable = true)]
	public Guid? ProjGUID { get; set; }
	[DataColumn(IsNullable = true)]
	public DateTime? FinishDateTime { get; set; }
	[DataColumn(IsNullable = true)]
	public DateTime? ApproveBy { get; set; }
	[DataColumn(IsNullable = true)]
	public string LoanTypeName { get; set; }
	[DataColumn(IsNullable = true)]
	public decimal? Rate { get; set; }
}

[Serializable]
[DataEntity(Alias = "cb_Contract")]
public partial class CbContract : BaseEntity
{
	[DataColumn(PrimaryKey=true)]
	public Guid ContractGUID { get; set; }
	public Guid BUGUID { get; set; }
	[DataColumn(IsNullable = true)]
	public string HtTypeCode { get; set; }
	[DataColumn(IsNullable = true)]
	public string HtKind { get; set; }
	[DataColumn(IsNullable = true)]
	public string ContractCode { get; set; }
	[DataColumn(IsNullable = true)]
	public string ContractName { get; set; }
	[DataColumn(IsNullable = true)]
	public string HtClass { get; set; }
	[DataColumn(IsNullable = true)]
	public string SignMode { get; set; }
	[DataColumn(IsNullable = true)]
	public string CostProperty { get; set; }
	[DataColumn(IsNullable = true)]
	public string Jbr { get; set; }
	[DataColumn(IsNullable = true)]
	public DateTime? SignDate { get; set; }
	[DataColumn(IsNullable = true)]
	public string JfCorporation { get; set; }
	[DataColumn(IsNullable = true)]
	public string YfCorporation { get; set; }
	[DataColumn(IsNullable = true)]
	public string BfCorporation { get; set; }
	[DataColumn(IsNullable = true)]
	public string HtProperty { get; set; }
	[DataColumn(IsNullable = true)]
	public byte? IfDdhs { get; set; }
	[DataColumn(IsNullable = true)]
	public Guid? MasterContractGUID { get; set; }
	[DataColumn(IsNullable = true)]
	public decimal? TotalAmount { get; set; }
	[DataColumn(IsNullable = true)]
	public decimal? BjcbAmount { get; set; }
	[DataColumn(IsNullable = true)]
	public decimal? ItemAmount { get; set; }
	[DataColumn(IsNullable = true)]
	public decimal? HtAmount { get; set; }
	[DataColumn(IsNullable = true)]
	public decimal? ItemDtAmount { get; set; }
	[DataColumn(IsNullable = true)]
	public decimal? HtycAmount { get; set; }
	[DataColumn(IsNullable = true)]
	public string JsState { get; set; }
	[DataColumn(IsNullable = true)]
	public decimal? ZjsAmount { get; set; }
	[DataColumn(IsNullable = true)]
	public decimal? JsAmount { get; set; }
	[DataColumn(IsNullable = true)]
	public decimal? JsBxAmount { get; set; }
	[DataColumn(IsNullable = true)]
	public decimal? JsOtherDeduct { get; set; }
	[DataColumn(IsNullable = true)]
	public decimal? JsItemDeduct { get; set; }
	[DataColumn(IsNullable = true)]
	public decimal? LocaleAlterAmount { get; set; }
	[DataColumn(IsNullable = true)]
	public decimal? DesignAlterAmount { get; set; }
	[DataColumn(IsNullable = true)]
	public decimal? OtherAlterAmount { get; set; }
	[DataColumn(IsNullable = true)]
	public decimal? BalanceAdjustAmount { get; set; }
	[DataColumn(IsNullable = true)]
	public decimal? SumALterAmount { get; set; }
	[DataColumn(IsNullable = true)]
	public decimal? SumYfAmount { get; set; }
	[DataColumn(IsNullable = true)]
	public decimal? SumScheduleAmount { get; set; }
	[DataColumn(IsNullable = true)]
	public decimal? SumFactAmount { get; set; }
	[DataColumn(IsNullable = true)]
	public decimal? ConfirmJhfkAmount { get; set; }
	[DataColumn(IsNullable = true)]
	public byte? IfConfirmFkPlan { get; set; }
	[DataColumn(IsNullable = true)]
	public decimal? SumPayAmount { get; set; }
	[DataColumn(IsNullable = true)]
	public string LandSource { get; set; }
	[DataColumn(IsNullable = true)]
	public string LandUseLimit { get; set; }
	[DataColumn(IsNullable = true)]
	public decimal? BuildArea { get; set; }
	[DataColumn(IsNullable = true)]
	public string LandProperty { get; set; }
	[DataColumn(IsNullable = true)]
	public string LandUse { get; set; }
	[DataColumn(IsNullable = true)]
	public string LandRemarks { get; set; }
	[DataColumn(IsNullable = true)]
	public DateTime? BeginDate { get; set; }
	[DataColumn(IsNullable = true)]
	public DateTime? EndDate { get; set; }
	[DataColumn(IsNullable = true)]
	public int? WorkPeriod { get; set; }
	[DataColumn(IsNullable = true)]
	public decimal? BxAmount { get; set; }
	[DataColumn(IsNullable = true)]
	public string BxLimit { get; set; }
	[DataColumn(IsNullable = true)]
	public decimal? PerformBail { get; set; }
	[DataColumn(IsNullable = true)]
	public string PerformRemarks { get; set; }
	[DataColumn(IsNullable = true)]
	public string TechnicRemarks { get; set; }
	[DataColumn(IsNullable = true)]
	public string RewardRemarks { get; set; }
	[DataColumn(IsNullable = true)]
	public string BreachRemarks { get; set; }
	[DataColumn(IsNullable = true)]
	public string TermRemarks { get; set; }
	[DataColumn(IsNullable = true)]
	public string ApproveState { get; set; }
	[DataColumn(IsNullable = true)]
	public DateTime? ApproveDate { get; set; }
	[DataColumn(IsNullable = true)]
	public string ApprovedBy { get; set; }
	[DataColumn(IsNullable = true)]
	public string CfMode { get; set; }
	[DataColumn(IsNullable = true)]
	public decimal? YcfAmount { get; set; }
	[DataColumn(IsNullable = true)]
	public string HtCfState { get; set; }
	[DataColumn(IsNullable = true)]
	public string AlterCfState { get; set; }
	[DataColumn(IsNullable = true)]
	public decimal? FactCfAmount { get; set; }
	[DataColumn(IsNullable = true)]
	public string FactCfState { get; set; }
	[DataColumn(IsNullable = true)]
	public string PayCfState { get; set; }
	[DataColumn(IsNullable = true)]
	public decimal? ItemCfAmount { get; set; }
	[DataColumn(IsNullable = true)]
	public string ItemCfState { get; set; }
	[DataColumn(IsNullable = true)]
	public decimal? HtycCfAmount { get; set; }
	[DataColumn(IsNullable = true)]
	public string HtycCfState { get; set; }
	[DataColumn(IsNullable = true)]
	public string FinanceHsxmCode { get; set; }
	[DataColumn(IsNullable = true)]
	public string FinanceHsxmName { get; set; }
	[DataColumn(IsNullable = true)]
	public string ApproveLog { get; set; }
	[DataColumn(IsNullable = true)]
	public byte? ProcessStatusContract { get; set; }
	[DataColumn(IsNullable = true)]
	public Guid? TacticProtocolGUID { get; set; }
	[DataColumn(IsNullable = true)]
	public Guid? CgPlanGUID { get; set; }
	[DataColumn(IsNullable = true)]
	public Guid? JfProviderGUID { get; set; }
	[DataColumn(IsNullable = true)]
	public Guid? YfProviderGUID { get; set; }
	[DataColumn(IsNullable = true)]
	public Guid? BfProviderGUID { get; set; }
	[DataColumn(IsNullable = true)]
	public byte? IsJtContract { get; set; }
	[DataColumn(IsNullable = true)]
	public Guid? JbrGUID { get; set; }
	[DataColumn(IsNullable = true)]
	public string ProjType { get; set; }
	[DataColumn(IsNullable = true)]
	public Guid? DeptGUID { get; set; }
	[DataColumn(IsNullable = true)]
	public string ContractCodeFormat { get; set; }
	[DataColumn(IsNullable = true)]
	public string JfProviderName { get; set; }
	[DataColumn(IsNullable = true)]
	public string YfProviderName { get; set; }
	[DataColumn(IsNullable = true)]
	public string BfProviderName { get; set; }
	[DataColumn(IsNullable = true)]
	public Guid? Bz { get; set; }
	[DataColumn(IsNullable = true)]
	public decimal? Rate { get; set; }
	[DataColumn(Alias = "SumScheduleAmount_Bz", IsNullable = true)]
	public decimal? SumScheduleAmountBz { get; set; }
	[DataColumn(Alias = "SumPayAmount_Bz", IsNullable = true)]
	public decimal? SumPayAmountBz { get; set; }
	[DataColumn(Alias = "SumAlterAmount_Bz", IsNullable = true)]
	public decimal? SumAlterAmountBz { get; set; }
	[DataColumn(Alias = "SumYfAmount_Bz", IsNullable = true)]
	public decimal? SumYfAmountBz { get; set; }
	[DataColumn(Alias = "JsAmount_Bz", IsNullable = true)]
	public decimal? JsAmountBz { get; set; }
	[DataColumn(Alias = "ZjsAmount_Bz", IsNullable = true)]
	public decimal? ZjsAmountBz { get; set; }
	[DataColumn(Alias = "HtAmount_Bz", IsNullable = true)]
	public decimal? HtAmountBz { get; set; }
	[DataColumn(Alias = "JsOtherDeduct_Bz", IsNullable = true)]
	public decimal? JsOtherDeductBz { get; set; }
	[DataColumn(IsNullable = true)]
	public string UseCostInfo { get; set; }
	[DataColumn(IsNullable = true)]
	public string UseCostColor { get; set; }
	[DataColumn(IsNullable = true)]
	public byte? IsLock { get; set; }
	[DataColumn(IsNullable = true)]
	public string ProjectCodeList { get; set; }
	[DataColumn(IsNullable = true)]
	public string ProjectNameList { get; set; }
	[DataColumn(IsNullable = true)]
	public decimal? YgAlterAmount { get; set; }
	[DataColumn(IsNullable = true)]
	public string YgAlterRemarks { get; set; }
	[DataColumn(IsNullable = true)]
	public string YgAlterBudget { get; set; }
	[DataColumn(IsNullable = true)]
	public int? SchedulePayRate { get; set; }
	[DataColumn(IsNullable = true)]
	public byte? ProjectPlanAffect { get; set; }
	[DataColumn(IsNullable = true)]
	public string UseStockInfo { get; set; }
	[DataColumn(IsNullable = true)]
	public string HsCfState { get; set; }
	[DataColumn(IsNullable = true)]
	public decimal? InvoiceAmount { get; set; }
	[DataColumn(IsNullable = true)]
	public decimal? SumYfBxAmount { get; set; }
	[DataColumn(IsNullable = true)]
	public string DemoApproveState { get; set; }
}


[Serializable]
[DataEntity(Alias = "p_CwjkHsxm")]
public partial class PCwjkHsxm : BaseEntity
{
	[DataColumn(IsNullable = true)]
	public Guid? CwztGUID { get; set; }
	[DataColumn(IsNullable = true)]
	public string OperObject { get; set; }
	[DataColumn(PrimaryKey = true, SeqGuid = true)]
	public Guid HsTypeGUID { get; set; }
	[DataColumn(IsNullable = true)]
	public string HsTypeName { get; set; }
	[DataColumn(IsNullable = true)]
	public string HsTypeNameU8 { get; set; }
	[DataColumn(IsNullable = true)]
	public long XmVersion { get; set; }
}

[Serializable]
[DataEntity(Alias = "p_CwjkHsxmMap")]
public partial class PCwjkHsxmMap : BaseEntity
{
	[DataColumn(IsNullable = true)]
	public Guid? BUGUID { get; set; }
	[DataColumn(IsNullable = true)]
	public Guid? CwztGUID { get; set; }
	[DataColumn(SeqGuid = true, IsNullable = true)]
	public Guid? HsxmMapGUID { get; set; }
	[DataColumn(IsNullable = true)]
	public Guid? HsTypeGUID { get; set; }
	[DataColumn(IsNullable = true)]
	public string HsxmCode { get; set; }
	[DataColumn(IsNullable = true)]
	public string HsxmName { get; set; }
	[DataColumn(IsNullable = true)]
	public Guid? ObjectGUID { get; set; }
	[DataColumn(IsNullable = true)]
	public string ObjectName { get; set; }
	public bool IsExport { get; set; }
}