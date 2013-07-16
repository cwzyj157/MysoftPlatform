<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>数据访问层DEMO</title>
	<style type="text/css">
		* { font-size:14px; }
		div { padding-top:5px; }
		input {border:solid 1px black;}
	</style>
	<script type="text/javascript" src="Scripts/jquery-1.9.1.min.js"></script>
	<script type="text/javascript" src="Scripts/default.js"></script>
	<script type="text/javascript">
		$(function () {
			//绑定下拉框事件,切换存储过程调用方式及SQL调用方式
			$("#ddlMode").on("change", function () {
				$(this).jumpToUrl();
			});

			//根据定价面积，生成对应的文本框
			$("#gridViewMain").find("tr").each(function (i) {
				if (i == 0) {
					return true;
				}
				$(this).bindRow();
			});

			//下拉框切换时，根据下拉框的选择,切换行的文本框
			$("#gridViewMain").find("select").each(function () {
				$(this).on("change", function () {
					$(this).parent().parent().bindRow();
				})
			});

			//订阅保存按钮点击事件
			$("#btnSave").on("click", function () {
				var data = $("#gridViewMain").getData();
				var mode = $("#__mode").val();
				//AJAX保存数据
				$.post("ajax.ashx?t=" + Math.random(), { xml: data, method: mode }, function (message) {
					location.href = location.href;
				}, "text");
			})
		});
	</script>
</head>
<body>
    <form id="frmMain" runat="server">
    <div>
		DEMO运行方式:
    	<asp:DropDownList ID="ddlMode" runat="server">
			<asp:ListItem Value="?Mode=">SQL查询</asp:ListItem>
			<asp:ListItem Value="?Mode=SP">存储过程</asp:ListItem>
		</asp:DropDownList>
		&nbsp;&nbsp;(SQL查询将使用CPQuery类完成功能,存储过程将使用StoreProcedure类完成功能.)
    </div>
	
	<div style="text-align:right;padding-bottom:3px;">
		房间名称 : <asp:TextBox  ID="txtRoomInfo" runat="server" Width="120px"></asp:TextBox>
		&nbsp; 
		定价面积 : 
		<asp:DropDownList ID="txtDjArea" Width="120px" runat="server">
			<asp:ListItem Text=""></asp:ListItem>
			<asp:ListItem Text="建筑面积"></asp:ListItem>
			<asp:ListItem Text="套内面积"></asp:ListItem>
			<asp:ListItem Text="套"></asp:ListItem>
		</asp:DropDownList>
		&nbsp; 
		&nbsp;
		<asp:Button ID="btnQuery" runat="server" onclick="btnQuery_Click" Text="查找" />
	</div>

	<div style="text-align:right;border-top:solid 1px black;">
		<input type="button" id="btnSave" value="保存修改"></button>
	</div>

	<div style="text-align:right;">
		<asp:GridView ID="gridViewMain" runat="server" AutoGenerateColumns="False" 
			CellPadding="4" EnableModelValidation="True" ForeColor="#333333" Width="100%" 
			EnableViewState="false" onrowdatabound="gridViewMain_RowDataBound">
			<AlternatingRowStyle BackColor="White" ForeColor="#284775" />
			<Columns>
				<asp:BoundField DataField="RoomInfo" HeaderText="房间信息" />
				<asp:BoundField DataField="BldArea" HeaderText="建筑面积" />
				<asp:BoundField DataField="TnArea" HeaderText="套内面积" />
				<asp:TemplateField HeaderText="定价面积">
					<ItemTemplate>
						<asp:DropDownList ID="ddlDjArea" runat="server" SelectedValue='<%# Eval("DjArea") %>'>
							<asp:ListItem Text="建筑面积"></asp:ListItem>
							<asp:ListItem Text="套内面积"></asp:ListItem>
							<asp:ListItem Text="套"></asp:ListItem>
						</asp:DropDownList>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:BoundField DataField="Price" HeaderText="建筑单价" DataFormatString="{0:F2}"/>
				<asp:BoundField DataField="TnPrice" HeaderText="套内单价" DataFormatString="{0:F2}"/>
				<asp:BoundField DataField="Total" HeaderText="标准总价" DataFormatString="{0:F2}"/>
				<asp:BoundField DataField="RawDjArea" HeaderText="原定价面积" />
				<asp:BoundField DataField="RawPrice" HeaderText="原建筑单价" />
				<asp:BoundField DataField="RawTnPrice" HeaderText="原套内单价" />

				<asp:BoundField DataField="RoomGUID" HeaderText="RoomGUID"/>
			</Columns>
			<EditRowStyle BackColor="#999999" />
			<FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
			<HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
			<PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
			<RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
			<SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
		</asp:GridView>
	</div>
    </form>
</body>
</html>
