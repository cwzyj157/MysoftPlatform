
var g_MsgBoxTitle = "Task Manager";
var g_deleteButtonFilter = "a[title='删除']";
var __waitHTML = '<div style="padding: 20px;"><img src="/Images/progress_loading.gif" /><span style="font-weight: bold;padding-left: 10px; color: #FF66CC;">请稍后......</span></div>';


$(function(){
	// 设置Ajax操作的默认设置
	$.ajaxSetup({
	    cache: false,
	    beforeSend: function() { $("#labAjaxMessage").show(); },
		complete: function() { $("#labAjaxMessage").hide(); },
		error: function (XMLHttpRequest, textStatus, errorThrown) {
			if( XMLHttpRequest.responseText ){
			    var error = "<b style='color: #f00'>" + XMLHttpRequest.status + "  " + XMLHttpRequest.statusText + "</b>";
				var start = XMLHttpRequest.responseText.indexOf("<title>");
				var end = XMLHttpRequest.responseText.indexOf("</title>");
				if( start > 0 && end > start )
					error += "<br /><br />" + XMLHttpRequest.responseText.substring(start + 7, end);
					
				$.messager.alert(g_MsgBoxTitle, "调用服务器失败。<br />" + error ,'error');
			}
			else{
				$.messager.alert(g_MsgBoxTitle, "调用服务器失败。" ,'error');
			}
		}
	});
	

	// 让EasyUI的菜单看起来更舒服。
	$("#mainMenuBar span.m-btn-downarrow").remove();
	$("div.menu-item").css("height", "25px")
		.find("div.menu-text").css({"display": "block", "width": "100%"})
		.find("a").css({"display": "block", "padding-top": "4px", "color": "#444"});
});


function CommonDeleteRecord(){
	if( confirm('确定要删除此记录吗？？') ) {
		$.ajax({
			type: "POST",
			url: $(this).attr("href"),
			success: function(responseText){
				$('#grid1').datagrid('reload');
			}
		});
	}
	// 无论如何，都返回false
	return false;
}

function UrlCombine(str1, str2){
	var flag = (str1.indexOf('?') >= 0 ? '&' : '?');
	return (str1 + flag + str2);
}



// 将文本框“改造”成“有搜索对话框功能”的控件
function SetSearchTextbox(textboxId, hiddenId, pickButtonClick) {
    var j_text = $('#' + textboxId);
	if( j_text.attr("readonly") == "readonly" || j_text.attr("disabled") == "disabled" ) 
		return false;
	
	var width = j_text.width();
	var height = j_text.height() - 2;
	
    var j_div = $("<div></div>").insertBefore(j_text).addClass(j_text.attr("class")).css("width", width).css("padding", "1px");
    
    j_text.removeClass("myTextbox").addClass("myTextboxReadonly").css("width", (width-42)).css("float", "left").css("border", "0px").css("height", height).attr("readonly", "readonly");
    j_div.append(j_text);
    
    $("<a></a>").attr("title", "选择").addClass("floatButton").addClass("searchButton").appendTo(j_div).click(pickButtonClick);
    $("<a></a>").attr("title", "清除").addClass("floatButton").addClass("clearButton").appendTo(j_div).click(function(){
		j_text.val("").change();
		$("#" + hiddenId).val("");
		return false;
	});
}




// 解析一个字符串中的日期
function parseDate(str){
  if(typeof(str) == 'string'){
    var results = str.match(/^\s*0*(\d{4})-0?(\d{1,2})-0?(\d{1,2})\s*$/);
    if(results && results.length >3)
      return new Date(parseInt(results[1]), parseInt(results[2]) -1, parseInt(results[3]));
  }
  return null;
}



// 根据二个字符串，返回一个日期范围。
function GetDateRange(txtStart, txtEnd){
	var _date1 = $("#" + txtStart).datebox("getValue");
	var _date2 = $("#" + txtEnd).datebox("getValue");
	
	var _d1 = parseDate(_date1);
	var _d2 = parseDate(_date2);
	if( _date1.length > 0 && _d1 == null ){
		alert("日期格式输入无效。"); $("#" + txtStart).focus(); return null;
	}
	if( _date2.length > 0 && _d2 == null ){	
		alert("日期格式输入无效。"); $("#" + txtEnd).focus(); return null;
	}
	if( _date1.length > 0 && _date2.length > 0 && _d1 > _d2 ){
		alert("日期范围输入无效。"); $("#" + txtEnd).focus(); return null;
	}
	var obj = {StartDate: _date1, EndDate: _date2};
	return obj;
}


function ValidateControl(expression, message){
	if( $.trim($(expression).val()).length == 0 ){
		$.messager.alert(g_MsgBoxTitle, message, 'warning');
		return false;
	}
	return true;
}



String.prototype.HtmlEncode = function(){
    var div = document.createElement("div");
    div.appendChild(document.createTextNode(this));
    return div.innerHTML;
};

String.prototype.JsonDateToString = function( showFullTime ){
	var date = new Date(parseInt(this.substr(6)));
	//return date.toString();
	
	var year = date.getFullYear();
	if( year < 1000 )
	    return "";
	
	var month = (date.getMonth()+1) + "";
	if( month.length < 2 )
		month = "0" + month;
	var day = date.getDate() + "";
	if( day.length < 2 )
		day = "0" + day;
	
	if( ! showFullTime   )
	    return year + '-' + month + '-' + day;
	
	var hour = date.getHours() + "";
	if( hour.length < 2 )
		hour = "0" + hour;
	var minute = date.getMinutes() + "";
	if( minute.length < 2 )
		minute = "0" + minute;
	var second = date.getSeconds() + "";
	if( second.length < 2 )
		second = "0" + second;
	
	return year + '-' + month + '-' + day + ' ' + hour + ':' + minute + ':' + second;
};
