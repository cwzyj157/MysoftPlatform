<%@ Control Language="C#" %>

<form  id="formEditTask" title="任务" style="padding: 10px" method="post" action="xxx.aspx">
	<table cellpadding="4" border="0px">
	<tr><td style="width: 80px">任务编号</td>
		<td style="width: 170px"><input name="TaskNo" type="text" id="txtTaskNo" class="myTextbox w150" /></td>
		<td style="width: 100px">客户名称</td>
		<td style="width: 170px"><input name="CustomerName" type="text" id="txtCustomerName" class="myTextbox w150" /></td>
		<td style="width: 100px">ABU PM</td>
		<td style="width: 170px"><input name="AbuPM" type="text" id="txtPM" class="myTextbox w150" /></td>
	</tr>
	<tr><td style="width: 80px">ERP版本</td>
		<td><input name="ErpVersion" type="text" id="txtErpVersion" class="myTextbox w150" /></td>
		<td>MAP版本</td>
		<td><input name="MapVersion" type="text" id="txtMapVersion" class="myTextbox w150" /></td>
		<td></td>
		<td></td>
	</tr>
	
	<tr><td>任务类别</td>
		<td><select name="TaskType" id="cboTaskType" class="myTextbox w150 easyui-combobox" >
<% foreach( string type in AppInfo.RunOptions.TaskTypes ) { %>
				<option value="</option><%= type.HtmlAttributeEncode() %>"><%= type.HtmlEncode() %>
<% } %>
			</select>
		</td>
		<td>开发人员</td>
		<td><select name="Developer" id="cboDeveloper" class="myTextbox w150 easyui-combobox">
			<option value="">---请选择---</option>
<% foreach( var user in AppInfo.RunOptions.Users.FindAll(u => u.IsInRole(UserRole.Developer)) ) { %>
	<option value="<%= user.Names.ShortName %>"><%= user.Names.FullName %></option>
<% } %>
		</select></td>
		<td>测试人员</td>
		<td><select name="Tester" id="cboTester" class="myTextbox w150 easyui-combobox">
			<option value="">---请选择---</option>
<% foreach( var user in AppInfo.RunOptions.Users.FindAll(u=>u.IsInRole(UserRole.Tester)) ) { %>
	<option value="<%= user.Names.ShortName %>"><%= user.Names.FullName %></option>
<% } %>
		</select></td>		
		</tr>
	<tr><td style="width: 80px">开始时间</td>
		<td><input name="Start" type="text" id="txtStart" class="myTextbox w150 easyui-datebox" /></td>
		<td>预计完成时间</td>
		<td><input name="ExpectEnd" type="text" id="txtExpectEnd" class="myTextbox w150 easyui-datebox" /></td>
		<td>工作量(天)</td>
		<td><input name="Workload" type="text" id="txtWorkload" class="myTextbox w150 easyui-numberbox" data-options="min:0,precision:1" /></td>
	</tr>

	<tr><td>任务标题</td><td colspan="3">
		<input name="TaskTitle" type="text" id="txtTaskTitle" class="myTextbox" style="width: 440px" />
		</td>
<% if( UserManager.CheckCurrentUserRole(UserRole.Admin) ) { %>
		<td>任务状态</td>
		<td><select name="Status" id="cboStatus" class="myTextbox w150 easyui-combobox" >
				<option value="Ready" selected="selected">未处理</option>
				<option value="Coding">开发中</option>
				<option value="Test">测试中</option>
				<option value="Finished">已完成</option> 
<% } else { %>
		<td></td>
		<td></td>
<% } %>
				
			</select>
		</td>
		</tr>
	<tr><td>备注</td><td colspan="5">
		<textarea name="Comment" id="txtComment" rows="50" cols="300" class="myTextbox" style="width: 720px; height: 70px"></textarea>
		</td></tr>
	
	</table>
	<div><input type="hidden" id="hiddenTaskGuid" name="TaskGuid" /></div>
</form>
