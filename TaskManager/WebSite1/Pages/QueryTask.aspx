<%@ Page Title="任务记录查询" Language="C#" MasterPageFile="~/Pages/MasterPage.master" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div class="easyui-layout" fit="true">
	<div region="north" title="任务记录查询" split="false" style="height:65px; overflow: hidden; border: 0px; float:left" border="false">
		<table cellpadding="4" cellspacing="0" >
		<tr><td>创建日期 从</td>
			<td><input type="text" id="txtStartDate" name="StartDate" value='<%= DateTime.Now.Date.GetMonday().ToDateString() %>' class="myTextbox easyui-datebox" style="width: 120px" /></td>
			<td style="width: 35px; text-align: right">到</td>
			<td><input type="text" id="txtEndDate" name="EndDate" value='<%= DateTime.Now.ToDateString() %>' class="myTextbox easyui-datebox" style="width: 120px" /></td>
			<td><a href="#" id="btnQuery" class="easyui-linkbutton" data-options="plain:true,iconCls:'icon-find'" >查找任务</a></td>
		</tr>
		</table>
	</div>
	<div region="center" style="overflow:hidden;">	
		<table id="grid1"></table>
	</div>
</div>


<%= UcExecutor.Render("/Controls/TaskInfo.ascx", null)%>

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="script" Runat="Server">
<%= UiHelper.RefJsFileHtml("/js/pages/Task.js")%>
<%= UiHelper.RefJsFileHtml("/js/pages/QueryTask.js")%>
</asp:Content>
