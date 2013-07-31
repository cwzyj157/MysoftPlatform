

function ShowEditItemDialog(itemId, divId, width, height, okFunc, shownFunc){
	if( typeof(width) != "number") width = 600;
	if( typeof(height) != "number") height = 430;
	
	var isEdit = ( itemId.length > 0 );	
	var j_dialog = $("#" + divId);
	
	if( j_dialog.attr("srcTitle") == undefined )
		j_dialog.attr("srcTitle", j_dialog.attr("title"));	// title属性会在创建对话框后被清除！
	
	var dlgTitle = (isEdit ? "编辑" : "添加" ) + j_dialog.attr("srcTitle");
	
	j_dialog.show().dialog({
        width: width, height: height, modal: true, resizable: true , title: dlgTitle , closable: true,
        buttons: [
            { text: (isEdit ? "保存" : "创建"), iconCls: 'icon-ok', plain: true,
                handler: function() {
					if( typeof(okFunc) == "function")
						okFunc(j_dialog);
                }
            }, 
            { text: '取消', iconCls: 'icon-cancel',  plain: true,
                handler: function() { 
					j_dialog.dialog('close');
			    }
            }],
		onOpen: function() { 
			if( typeof(shownFunc) == "function")
				shownFunc(j_dialog);
				
			j_dialog.find(":text.myTextbox").first().focus(); 
		}
	});
}



function ShowViewDialog(divId, dlgTitle, width, height) {
	if( typeof(width) != "number") width = 850;
	if( typeof(height) != "number") height = 530;
	
    $("#" + divId).show().dialog({
        height: height, width: width, modal: true, resizable: true, title: dlgTitle ,
        buttons: [
            { text: '关闭', iconCls: 'icon-cancel',  plain: true,
                handler: function() { 
				    $("#" + divId).dialog('close');
			    }
            }]
    });
}

function CloseDialog(divId){
	$("#" + divId).hide().dialog('close');
}

function ShowNormalDialog(divId, width, height, okButtonHanlder) {
	if( typeof(width) != "number") width = 850;
	if( typeof(height) != "number") height = 530;
	
    $("#" + divId).show().dialog({
        height: height, width: width, modal: true, resizable: true, 
        buttons: [
            { text: '确定', iconCls: 'icon-ok', plain: true,
                handler: function() {
                    if (okButtonHanlder ) {
						okButtonHanlder($("#" + divId));
					}
					else
						$("#" + divId).dialog('close');
                }
            }, 
            { text: '取消', iconCls: 'icon-cancel',  plain: true,
                handler: function() { 
				    $("#" + divId).dialog('close');
			    }
            }]
    });
}



function ShowSelectedItem(divId, txtName, hiddenId) {
    var jselected = $("#" + divId + " table.GridView :radio").filter(":checked");
    if (jselected.length != 1) return false;

    var itemId = jselected.attr("sid").substring(7);
    var name = $("#" + divId + " span[sid=itemName_" + itemId + "]").text();
    $("#" + txtName).val($.trim(name));
    $("#" + hiddenId).val(itemId);	
    return true;
}


