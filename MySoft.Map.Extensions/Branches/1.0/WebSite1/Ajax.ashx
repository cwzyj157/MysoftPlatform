<%@ WebHandler Language="C#" Class="ajax" %>

using System;
using System.Web;
using System.Xml;
using Mysoft.Map.Extensions.DAL;

public class ajax : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
		
		string data = context.Request.Form["xml"];
		
		if( string.IsNullOrEmpty(data) ) {
			throw new Exception("数据为空!");
		}
		string mode = context.Request.Form["method"];
		
		//数据目前仍然使用xml传递
		XmlDocument doc = new XmlDocument();
		doc.LoadXml(data);
		
		
		//如果是存储过程调用方式
		if( mode == "SP" ) {
			//启用事务
			using( ConnectionScope scope = new ConnectionScope( TransactionMode.Required) ) {
				foreach( XmlNode node in doc.SelectNodes("//row") ) {

					decimal price, tnprice, total;
					Guid roomguid;

					price = decimal.Parse(node.Attributes["price"].Value);
					tnprice = decimal.Parse(node.Attributes["tnprice"].Value);
					total = decimal.Parse(node.Attributes["total"].Value);
					roomguid = new Guid(node.Attributes["oid"].Value);
					
					var parameter = new {
						RoomGUID = roomguid,
						Price = price,
						TnPrice = tnprice,
						Total = total,
						DjArea = node.Attributes["djarea"].Value
					};
					//调用存储过程
					StoreProcedure.Create("usp_UpdateRoom", parameter).ExecuteNonQuery();
				}
				//提交事务
				scope.Commit();
			}
		}
		else {
			//启用事务
			using( ConnectionScope scope = new ConnectionScope(TransactionMode.Required) ) {
				foreach( XmlNode node in doc.SelectNodes("//row") ) {
					decimal price, tnprice, total;
					Guid roomguid;

					price = decimal.Parse(node.Attributes["price"].Value);
					tnprice = decimal.Parse(node.Attributes["tnprice"].Value);
					total = decimal.Parse(node.Attributes["total"].Value);
					roomguid = new Guid(node.Attributes["oid"].Value);

					var parameter = new {
						RoomGUID = roomguid,
						Price = price,
						TnPrice = tnprice,
						Total = total,
						DjArea = node.Attributes["djarea"].Value
					};
					
					//批量更新
					CPQuery.From("UPDATE p_room SET RawDjArea = DjArea,RawPrice=Price,RawTnPrice=TnPrice WHERE RoomGUID = @RoomGUID", parameter).ExecuteNonQuery();
					CPQuery.From("UPDATE p_Room SET Price = @Price, TnPrice = @TnPrice, Total = @Total, DjArea = @DjArea WHERE RoomGUID = @RoomGUID", parameter).ExecuteNonQuery();
				}
				//提交事务
				scope.Commit();
			}
		}

		context.Response.Write("保存成功！");
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}