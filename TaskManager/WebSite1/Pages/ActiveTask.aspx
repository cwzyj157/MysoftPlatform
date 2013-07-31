<%@ Page Title="活动任务表" Language="C#" MasterPageFile="~/Pages/MasterPage.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table id="grid1"></table>

<%= UcExecutor.Render("/Controls/TaskInfo.ascx", null)%>

<form  id="formSetStatus" title="设置任务状态" style="padding: 20px" method="post" action="xxx.aspx">
<div>
	确定要将任务 <b id="labTaskTitle"></b> <br /><br />的状态设置为【已完成】吗？
</div>
<p><br /><br /><br /><br /><br /></p>
</form>
</asp:Content>




<asp:Content ID="Content2" ContentPlaceHolderID="script" Runat="Server">
<%= UiHelper.RefJsFileHtml("/js/pages/Task.js")%>
<%= UiHelper.RefJsFileHtml("/js/pages/ActiveTask.js")%>
</asp:Content>
