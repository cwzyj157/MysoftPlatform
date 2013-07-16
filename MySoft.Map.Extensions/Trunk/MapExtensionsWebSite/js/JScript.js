    
//原ERP系统内存在大量的XML字符串拼接问题,XML转义字符也没有处理
var arrUserXml = []
arrUserXml.push("<cb_sjkProductIndex keyname='ProductIndexGUID' keyvalue='" 
	+ appForm.appForm_ProductIndexGUID.value + "' oid='" 
	+ appForm.appForm_ProductIndexGUID.value + "'>");
		
arrUserXml.push("<RefGUID>" + appForm.RefGUID.value + "</RefGUID> ");
arrUserXml.push("<ProductGUID>" + appForm.ProductGUID.value + "</ProductGUID> ");
arrUserXml.push("<ProductName>" + appForm.ProductName.value + "</ProductName> ");
//...表字段越多,拼接越多
arrUserXml.push("</cb_sjkProductIndex>")
	


//新API使用js对象来生成XML,无字符串拼接,内部处理XML转义字符
var keyval = appForm.appForm_ProductIndexGUID.value;

var attr = { keyname: "ProductIndexGUID", keyvalue: keyval, oid: keyval };

var data = { RefGUID: appForm.RefGUID.value,
    ProductGUID: appForm.ProductGUID.value,
    ProductName: appForm.ProductName.value,
    __attr: attr
};

var xml = MapExt.toXML("cb_sjkProductIndex", data);


//原ERP系统,返回OK字符串
var strRtn = openXMLHTTP(strFile, "SaveProductVerify", appForm.dataxml); 
if (strRtn != "OK") {
    if (strRtn.substr(0, 3) == "NO|") {
        var arrRtn = strRtn.substr(3).split("；");
	}
}
//返回SUCCESS字符串.还有很多返回ERR,YES,Y等方式		
sRtn = openXMLHTTP(sFile,"ApproveHTFKApply");	

if (sRtn == "SUCCESS"){
}
	
//新API封装了json->js对象的转换方式,服务端使用json输出,客户端直接解析
var responseText = openXMLHTTP(url, "a=1&b=2&c=3");
var result = MapExt.parseHttpResult(responseText);
if (result && result.Result) {
	// 处理结果
}
//说明： 前端API不解析XML格式返回值。





var responseText = openXMLHTTP(url, "a=1&b=2&c=3");
var result = MapExt.parseHttpResult(responseText);
if (result && result.Result) {
	// 处理结果， 这里可以访问 result.Data 获取从服务端返回的数据
	alert(result.Data);
}



