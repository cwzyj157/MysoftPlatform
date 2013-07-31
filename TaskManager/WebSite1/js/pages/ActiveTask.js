
$(function() {
    $("select").combobox({ editable: false });
    
	$('#grid1').datagrid({
		title:"活动任务列表", fit: true, nowrap: false, striped: true, collapsible:false,
		idField:'TaskGuid', rownumbers:true, singleSelect: true, border: false, pagination:false,
		url:'/ajax/Task/GetActiveTasks.aspx',
		columns:[[
			{title:'', field:'xx', align:'center', width: 40,
				formatter:function(value,row){ return '<a href="/ajax/Task/Delete.aspx?taskGuid=' + row.TaskGuid + '" title="删除" class="easyui-linkbutton" plain="true"><img src="/Images/delete2.gif" alt="删除" /></a>'; }
			},
			{title:"类别", field:"TaskType", width:40 },
			{title:"任务编号", field:"TaskNo", width:100},
			{title:'任务标题',field:'TaskTitle',width:260,
				formatter:function(value,row){ return '<a href="javascript:void(0);" class="easyui-linkbutton" rowId="' + row.TaskGuid + '" plain="true">' + row.TaskTitle.HtmlEncode() + '</a>'; }
			},
			{title:"客户名称", field:"CustomerName", width:100},
			{title:"PM", field:"AbuPM", width:80},
			{title:"ERP版本", field:"ErpVersion", width:80},
			{ title: "MAP版本", field: "MapVersion", width: 80 },			
			{title:"开发人员", field:"Developer.FullName",width:60, formatter:function(value,row){  return row.Developer ? row.Developer.FullName : "";}},
			{title:"测试人员", field:"Tester.FullName", width:60, formatter:function(value,row){ return row.Tester? row.Tester.FullName : "" ;}},
			{title:"开始时间", field:"Start", width:80, formatter:function(value,row){ return row.Start.JsonDateToString(false);} },
			{title:"预计完成时间", field:"ExpectEnd", width:80, formatter:function(value,row){ return row.ExpectEnd.JsonDateToString(false);} },
			{title:"工作量", field:"Workload", width:60, align:'center'},
			{title:"状态", field:"StatusText", width:80}
		]],	
		toolbar:[{
			id:'btnAdd',	text:'新增任务',	iconCls:'icon-add',
			handler: ShowNewTaskDialog
		},{
			id:'btnRefresh',	text:'刷新列表',	iconCls:'icon-reload',
			handler: function(){ $('#grid1').datagrid('reload'); }
		},{
			id:'btnSetFinished',	text:'标记完成',	iconCls:'icon-ok',
			handler: SetFinished
		}],
		onLoadSuccess: function(){
			$($('#grid1').datagrid("getPanel")).find('a.easyui-linkbutton').linkbutton()
				.filter(g_deleteButtonFilter).click( CommonDeleteRecord ).end()
				.filter("a[rowId]").click(ShowEditTaskDialog);

//			$("#grid1").datagrid("selectRow", 0);
		}
	});
	
	var pager = $('#grid1').datagrid("getPager");
	$(pager).pagination({
		onSelectPage:function(pageNumber, pageSize){
			$('#grid1').datagrid('reload', {PageIndex: pageNumber, PageSize: pageSize});			
		}
	});
});


function SetFinished(){
    var currentRow = $("#grid1").datagrid("getSelected");
    if( currentRow == null ){
        alert("请选择要设置的任务记录。");
        return;
    }
    
    $("#labTaskTitle").text(currentRow.TaskTitle);

    ShowNormalDialog("formSetStatus", 600, 360, function(j_dialog) {
        $.ajax({
            type: "POST",
            url: "/ajax/Task/SetFinished.aspx",
            data: {taskGuid: currentRow.TaskGuid},
            success: function(){
                $.messager.alert(g_MsgBoxTitle, "操作成功。", "info", function(){ 
						 j_dialog.hide().dialog('close');
						 $('#grid1').datagrid('reload');
					});
            }
        });
    });
	
}