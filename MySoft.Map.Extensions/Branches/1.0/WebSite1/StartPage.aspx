<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StartPage.aspx.cs" Inherits="StartPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
	<style type="text/css">
		.main{ display:inline;}
	</style>
</head>
<body>
    <form id="frmMain" runat="server">
		<div class="main" style="width:150px;">
			<asp:TreeView ID="TreeView1" runat="server">
			<Nodes>
				<asp:TreeNode Text="新建节点" Value="新建节点"></asp:TreeNode>
				<asp:TreeNode Text="新建节点" Value="新建节点"></asp:TreeNode>
				<asp:TreeNode Text="新建节点" Value="新建节点"></asp:TreeNode>
				<asp:TreeNode Text="新建节点" Value="新建节点"></asp:TreeNode>
				<asp:TreeNode Text="新建节点" Value="新建节点"></asp:TreeNode>
			</Nodes>
			</asp:TreeView>
			</div>
		<div class="main">
			<div>查看源文件</div>
			<div>
				<iframe src="Default.aspx" frameborder="0"></iframe>
			</div>
		</div>
    </form>
</body>
</html>
