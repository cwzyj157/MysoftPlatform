var MapExt = MapExt || {};

(function() {
    var 
    rtrim = /^[\s\uFEFF\xA0]+|[\s\uFEFF\xA0]+$/g,
    space = null,
    line = "\r\n",
    depth = 0;
    
    function _formatDate(date, format) {
        format = format || 'yyyy-MM-dd HH:mm';
        var o = {
            "M+": date.getMonth() + 1,  //month
            "d+": date.getDate(),       //day
            "h+": date.getHours(),      //hour
            "H+": date.getHours(),      //hour
            "m+": date.getMinutes(),    //minute
            "s+": date.getSeconds(),    //second
            "q+": Math.floor((date.getMonth() + 3) / 3),  //quarter
            "S": date.getMilliseconds() //millisecond
        }

        if (/(y+)/.test(format)) {
            format = format.replace(RegExp.$1, (date.getFullYear() + "").substr(4 - RegExp.$1.length));
        }

        for (var k in o) {
            if (new RegExp("(" + k + ")").test(format)) {
                format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
            }
        }
        return format;
    }
    
    function _xmlEscape(sXml) {
        if (sXml){
            return sXml.toString().replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;").replace(/"/g, "&quot;").replace(/'/g, "&apos;");
        }
        else{
            return "";
        }
    }
    
    function _toXML(rootName, jsObject) {
        var s = "";
        var textNode = false;
        if (!(jsObject instanceof Object)
            || jsObject instanceof Number
            || jsObject instanceof String
            || jsObject instanceof Boolean
            || jsObject instanceof Date) {

            s += space.join("") + "<" + rootName + ">";
            if (jsObject instanceof Date) {
                s += _xmlEscape(_formatDate(jsObject, "yyyy-MM-ddTHH:mm:ss"));
            }
            else {
                s += _xmlEscape(jsObject);
            }
            s += "</" + rootName + ">" + line;
        }
        else {
            var isArray = jsObject instanceof Array;
            if (depth != 0 || !isArray){
                s += space.join("") + "<" + rootName
                var text = "";
                for (var i in jsObject) {
                    if (jsObject[i] instanceof Function) {
                        continue;
                    }
                    if (i == "__attr") {
                        var attr = jsObject[i];
                        for (var j in attr) {
                            if (attr[j] instanceof Function) {
                                continue;
                            }
                            s += " " + j + "=\"" + _xmlEscape(attr[j]) + "\" "
                        }
                    }
                    if (i == "__text"){
                        if (jsObject[i]){
                            text = jsObject[i].toString();
                        }
                    }
                }
                if (text != ""){
                    textNode = true;
                    s += ">" + text;
                }
                else{
                    s += ">" + line;
                }
                space.push("  ");
            }
            for (var k in jsObject) {
                var instance = jsObject[k];
                if (k == "__attr" || k == "__text" || instance instanceof Function) {
                    continue;
                }
                if (instance instanceof Array){
                    for (var l in instance){
                        if (instance[l] instanceof Function) {
                            continue;
                        }
                        depth++;
                        s += _toXML(k, instance[l]);
                        depth--;
                    }
                }
                else{
                    depth++;
                    s += _toXML(isArray ? rootName : k, jsObject[k]);
                    depth--;
                }
            }
            if (depth != 0 || !isArray){
                space.pop();
                if (!textNode){
                    s += space.join("");    
                }
                s += "</" + rootName + ">" + line;
            }
        }
        return s;
    }
    
   
    MapExt.toXml = function(rootName, jsObject) {
        ///	<summary>
        ///     将一个js对象转换为XML字符串
        ///	</summary>
    	///	<param name="rootName" type="String">
        ///		根节点元素名
        ///	</param>
    	///	<param name="jsObject" type="Object">
        ///		转换为XML字符串的对象
        ///	</param>
        ///	<returns type="String" />

        if (!rootName || !jsObject) {
            return "";
        }
        if (MapExt.trim(rootName) == "") {
            return "";
        }
        space = [];
        depth = 0;
        return _toXML(rootName, jsObject);
    }

    MapExt.trim = function(str) {
        ///	<summary>
        ///     过滤字符串前后空格
        ///	</summary>
        ///	<param name="str" type="String">
        ///		表示要被过滤空格的字符串
        ///	</param>
        ///	<returns type="String" />

        if (!str) {
            return "";
        }
        return str.replace(rtrim, "");
    }

    MapExt.parseJSON = function(strJson) {
        ///	<summary>
        ///     将JSON字符串转换为JSON对象
        ///     1.如果JSON字符串格式存在错误,本函数将产生异常
        ///     2.如果格式化服务端返回的HttpResult对象JSON格式.请使用parseJSONResult方法
        ///     3.如果格式化服务端返回的HttpResult对象XML格式,请使用parseXMLResult方法
        ///     4.以上两个方法不会产生异常,解析失败也会返回对象
        ///	</summary>
        ///	<param name="strJson" type="String">
        ///		表示被转换的JSON字符串
        ///	</param>
        ///	<returns type="Object" />

        if (!strJson) {
            return null;
        }
        return (new Function("return " + strJson))();
    }

    MapExt.parseJSONResult = function(strJSON) {
        ///	<summary>
        ///     将JSON字符串转换为HttpResult对象
        ///	</summary>
        ///	<param name="strJSON" type="String">
        ///		表示被转换的JSON字符串
        ///	</param>
        ///	<returns type="HttpResult" />

        var result = null;
        if (!strJSON) {
            result = { Result : false, ErrorMessage : "strJSON参数为null或undefined" };
        }
        try{
            result = (new Function("return " + strJSON))();
        }
        catch(e){
            var error = "";
			var start = strJSON.indexOf("<title>");
			var end = strJSON.indexOf("</title>");
			if( start > 0 && end > start ){
				error += "调用服务器失败。\r\n" + strJSON.substring(start + 7, end);
            }
            else{
                error += "转换json失败。\r\n" + e.description;
            }
            result = { Result : false, ErrorMessage : error };
        }
        return result;
    }

    //在appForm的afterSave函数中,可以通过appForm.returnxml拿到服务端返回的xml.
    MapExt.parseXMLResult = function(strXML) {
        ///	<summary>
        ///     将XML字符串转换为HttpResult对象
        ///	</summary>
        ///	<param name="strXML" type="String">
        ///		表示被转换的XML字符串
        ///	</param>
        ///	<returns type="HttpResult" />

        var result = null;

        if (!strXML) {
            result = { Result : false, ErrorMessage : "strXML参数为null或undefined" };
        }
        var doc = null;

        if(window.ActiveXObject){
            doc = new ActiveXObject("Microsoft.XMLDOM");
            if (!doc.loadXML(strXML)){
                return { Result : false, ErrorMessage : "加载XML失败,请检查XML文档格式" };
            }
        }
        else{
            doc = new DOMParser().parseFromString(strXML, "text/xml");
            var errNode = doc.getElementsByTagName("parsererror");
            if (errNode.length > 0){
                return { Result : false, ErrorMessage : "加载XML失败,请检查XML文档格式" };
            }
        }

        var node = doc.documentElement;
        var resultVal = node.getAttribute("result");
        var keyVal = node.getAttribute("keyvalue");
        var errVal = node.getAttribute("errormessage");

        var result = { Result : false, KeyValue : "", ErrorMessage : "" };

        if (resultVal && MapExt.trim(resultVal) == "true"){
            result.Result = true;
        }
        if (keyVal){
            result.KeyValue = keyVal;
        }
        if (errVal){
            result.ErrorMessage = errVal;
        }
        return result;
    }
})();
