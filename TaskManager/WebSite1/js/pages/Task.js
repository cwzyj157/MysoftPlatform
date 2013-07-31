

function ShowNewTaskDialog(){
	//$("#formEditTask").clearForm();
	$("#formEditTask :input").val('');
	ShowEditItemDialog('', 'formEditTask', 850, 300, function(j_dialog){
		if( ValidateEditTaskForm() == false ) return;
		

		$("#formEditTask").ajaxSubmit({
			url: "/Ajax/Task/Insert.aspx",
			success: function(responseText) {
				$.messager.alert(g_MsgBoxTitle, "操作成功。", "info", function(){ 
					j_dialog.hide().dialog('close');					
					$('#grid1').datagrid('reload');
				});
			}
		});
	});

}


function ShowEditTaskDialog(){
	var dom = this;
	var rowId = $(this).attr("rowId");

	// 首先获取指定的客户资料
	$.ajax({
	    url: "/Ajax/Task/GetOne.aspx?taskGuid=" + rowId, dataType: "json",
	    success: function(json) {
	        $("#hiddenTaskGuid").val(json.TaskGuid);
	        $("#txtTaskNo").val(json.TaskNo);
	        $("#txtCustomerName").val(json.CustomerName);
	        $("#txtPM").val(json.AbuPM);
	        $("#txtErpVersion").val(json.ErpVersion);
	        $("#txtMapVersion").val(json.MapVersion);
	        $("#txtWorkload").numberbox("setValue", json.Workload.toString());
	        $("#cboStatus").combobox("setValue", json.StatusText);

	        $("#cboDeveloper").combobox("setValue", json.Developer ? json.Developer.ShortName : "");
	        $("#cboTester").combobox("setValue", json.Tester ? json.Tester.ShortName : "");
	        $("#txtStart").datebox("setValue", json.Start.JsonDateToString(false));
	        $("#txtExpectEnd").datebox("setValue", json.ExpectEnd.JsonDateToString(false));
	        //$("#txtActualEnd").datebox(json.ActualEnd.JsonDateToString(false));
	        $("#txtTaskTitle").val(json.TaskTitle);
	        $("#txtComment").val(json.Comment);
	        $("#cboTaskType").combobox("setValue", json.TaskType);

	        //			if(  g__isAdmin ||  json.Status < 2 ) {
	        // 显示编辑对话框
	        ShowEditItemDialog(rowId, 'formEditTask', 850, 300, function(j_dialog) {
	            // 验证输入
	            if (ValidateEditTaskForm()) {
	                $("#formEditTask").ajaxSubmit({
	                    url: "/Ajax/Task/Update.aspx",
	                    success: function(responseText) {
	                        $.messager.alert(g_MsgBoxTitle, "操作成功。", "info", function() {
	                            j_dialog.hide().dialog('close');
	                            $('#grid1').datagrid('reload');
	                        });
	                    }
	                });
	            }
	        });
	        //			}
	        //			else{
	        //			    ShowViewDialog('formEditTask', '查看任务', 850, 300);
	        //			}
	    }
	});	
}

function ExportExcel() {
    alert('0');
}

function ValidateEditTaskForm(){
	if( ValidateControl("#txtCustomerName", "客户名称 不能为空。") == false ) return false;
	if( ValidateControl("#txtTaskTitle", "任务标题 不能为空。") == false ) return false;
	return true;
}

