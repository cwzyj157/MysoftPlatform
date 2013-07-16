using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;


/// <summary>
/// XML保存接口
/// </summary>
public interface ISaveXML
{
	/// <summary>
	/// 新增或修改单条记录
	
	/// </summary>
	/// <para>下面的代码演示了SaveMaster保存单条记录的用法</para>
	/// <code>
	/// <![CDATA[
	///		string xml = @"<cb_Contract keyname="ContractGUID"  keyvalue="" >
	///						  <ContractName>&quot;合同名称1&apos;&lt;&gt;</ContractName>
	///						  <HTAmount>55567.87</HTAmount>
	///						  <HTType>合同类型</HTType>
	///						</cb_Contract>";
	///						
	///		string keyvalue = xxx.SaveMaster(xml);
	///		//XML文档中, cb_Contract表示表名
	///		//XML文档中, keyname表示主键, keyvalue表示主键值,如果主键值存在,则生成UPDATE语句,如果不存在,则生成keyvalue,生成INSERT语句
	///		//XML文档中, ContractName等节点表示字段名,节点内容表示字段值
	///		//对于301及以后的系统,生成的keyvalue使用有序GUID
	///		//返回值实例: 
	///		//    KeyValuePair<string, SqlCommand> kvp = new KeyValuePair<string, SqlCommand>();
	///		//
	///		//    SqlCommand cmd = new SqlCommand();
	///		//    cmd.CommandText = "生成的语句";
	///		//    cmd.Parameters.Add("@参数名", "参数值");
	///		//    ..其他参数
	///		//
	///		//    kvp.Key = "生成或原有的Guid";
	///		//    kvp.Value = cmd;
	///		//    return kvp
	/// ]]>
	/// </code>
	/// <param name="xml">xml字符串</param>
	/// <returns>返回生成或使用的主键与SqlCommand</returns>
	KeyValuePair<string, SqlCommand> SaveMaster(string xml);

	/// <summary>
	/// 新增或修改多条记录
	/// </summary>
	/// <para>下面的代码演示了SaveDetail保存多条记录的用法</para>
	/// <code>
	/// <![CDATA[
	///		string xml = @"<cb_HTFKApply keyname="HTFKApplyGUID"  keyvalue="89F6218B-925C-4910-9E60-FEE853B5414B" >
	///						  <HTFKType>进度款</HTFKType>
	///						  <ApplyName>第1笔款</ApplyName>
	///						  <FKAmount>77.6</FKAmount>
	///						  <ContractGUID>65D30FF4-76E9-4619-9527-A4559414F1E6</ContractGUID>
	///						</cb_HTFKApply>
	///						<cb_HTFKApply keyname="HTFKApplyGUID"  keyvalue="C245CA2D-6BBA-4DCB-AE24-99B2A9C23FFF" >
	///						  <HTFKType>合同款</HTFKType>
	///						  <ApplyName>第2笔款</ApplyName>
	///						  <FKAmount>77.6</FKAmount>
	///						  <ContractGUID>65D30FF4-76E9-4619-9527-A4559414F1E6</ContractGUID>
	///						</cb_HTFKApply>";
	///						
	///		List<KeyValuePair<string, SqlCommand>> cmds = xxx.SaveDetail(xml);
	///		//XML文档中, cb_HTFKApply表示表明
	///		//XML文档中, keyname表示主键, keyvalue表示主键值,如果主键值存在,则生成UPDATE语句,如果不存在,则生成keyvalue,同时生成INSERT语句
	///		//XML文档中, HTFKType等节点表示字段名,节点内容表示字段值
	///		//对于301及以后的系统,生成的keyvalue使用有序GUID
	/// ]]>
	/// </code>
	/// <param name="xml">xml字符串</param>
	/// <returns>返回生成或使用的主键与SqlCommand集合</returns>
	List<KeyValuePair<string, SqlCommand>> SaveDetail(string xml);
}