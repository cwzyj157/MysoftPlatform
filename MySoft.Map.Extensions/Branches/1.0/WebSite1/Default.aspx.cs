using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Mysoft.Map.Extensions.DAL;
using Demo.Entity;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
		string mode = Request.QueryString["Mode"];

		ClientScript.RegisterHiddenField("__mode", mode);

		if( IsPostBack ) {
			return;
		}
		
		//存储过程方式入口
		if( mode == "SP" ) {
			ddlMode.SelectedIndex = 1;

			//调用存储过程,返回实体集合
			List<PRoom> list = StoreProcedure.Create("usp_GetAllRoom", null).ToList<PRoom>();

			//将实体集合绑定到界面上
			gridViewMain.DataSource = list;
			gridViewMain.DataBind();
		}
		else {
			//直接通过SQL,返回实体集合
			List<PRoom> list = CPQuery.From("SELECT * FROM p_Room").ToList<PRoom>();

			//将实体集合绑定到界面上
			gridViewMain.DataSource = list;
			gridViewMain.DataBind();
		}
		
    }
	protected void btnQuery_Click(object sender, EventArgs e)
	{
		string mode = Request.QueryString["Mode"];

		//存储过程方式入口
		if( mode == "SP" ) {

			var parameter = new { RoomInfo = txtRoomInfo.Text.Trim(), DjArea = txtDjArea.Text.Trim() };

			//调用存储过程,返回实体集合
			List<PRoom> list = StoreProcedure.Create("usp_GetSearchRoom", parameter).ToList<PRoom>();

			//将实体集合绑定到界面上
			gridViewMain.DataSource = list;
			gridViewMain.DataBind();
		}
		else {
			//直接通过SQL,返回实体集合
			CPQuery query = CPQuery.From("SELECT * FROM p_Room WHERE 1=1 ");

			// 拼接【字符串】参数
			// 请注意下面的 LIKE 拼接方法：【百分号和参数值】是做为一个参数值，
			// 百分号绝对不能写到SQL语句中，因为那就不是参数化的SQL语句了。
			if( string.IsNullOrEmpty(txtRoomInfo.Text.Trim()) == false )
				query = query + " AND RoomInfo LIKE " +  ("%" + txtRoomInfo.Text + "%").AsQueryParameter();

			if( string.IsNullOrEmpty(txtDjArea.Text.Trim()) == false )
				query = query + " AND DjArea = " + txtDjArea.Text.AsQueryParameter();


			List<PRoom> list = query.ToList<PRoom>();

			//将实体集合绑定到界面上
			gridViewMain.DataSource = list;
			gridViewMain.DataBind();
		}
	}
	protected void gridViewMain_RowDataBound(object sender, GridViewRowEventArgs e)
	{
		int lng = e.Row.Cells.Count - 1;
		e.Row.Cells[lng].Attributes.Add("style", "display:none");
	}
}