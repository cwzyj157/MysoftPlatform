
$(function() {
    $("select").combobox({ editable: false, panelHeight: 100});
    
	$('#grid1').datagrid({
		fit: true, nowrap: false, striped: true, collapsible:false,
		idField:'TaskGuid', rownumbers:true, singleSelect: true, border: false, pagination:true,
		pageList:[30, 50, 100], pageSize: 30,
		//url:'/ajax/Task/GetActiveTasks.aspx',
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
			{title:"PM", field:"AbuPM", width:60},
			{title:"ERP版本", field:"ErpVersion", width:80},
			{title:"MAP版本", field:"MapVersion", width:80},
					{ title: "开发人员", field: "Developer.FullName", width: 60, formatter: function(value, row) { return row.Developer ? row.Developer.FullName : ""; } },
			{ title: "测试人员", field: "Tester.FullName", width: 60, formatter: function(value, row) { return row.Tester ? row.Tester.FullName : ""; } },
			{title:"开始时间", field:"Start", width:80, formatter:function(value,row){ return row.Start.JsonDateToString(false);} },
			//{title:"预计完成时间", field:"ExpectEnd", width:80, formatter:function(value,row){ return row.ExpectEnd.JsonDateToString(false);} },
			{title:"实际完成时间", field:"ActualEnd", width:80, formatter:function(value,row){ return row.ActualEnd.JsonDateToString(false);} },
			{title:"工作量", field:"Workload", width:60, align:'center'},
			{title:"状态", field:"StatusText", width:80}
		]],
		toolbar: [{
		    id: 'btnExport', text: '导出Excel', iconCls: 'icon-add',
		    handler: ExportExcel
		}],
		onLoadSuccess: function(){
			$($('#grid1').datagrid("getPanel")).find('a.easyui-linkbutton').linkbutton()
				.filter(g_deleteButtonFilter).click( CommonDeleteRecord ).end()
				.filter("a[rowId]").click( ShowEditTaskDialog );
		}
	});
	
	var pager = $('#grid1').datagrid("getPager");
	$(pager).pagination({
		onSelectPage:function(pageNumber, pageSize){
			$('#grid1').datagrid('reload', {PageIndex: pageNumber, PageSize: pageSize});			
		}
	});	
	
	
	$("#btnQuery").click( btnQuery_Click );

});







function btnQuery_Click(){
	var setPaginationEvent = function(){
		var pager = $('#grid1').datagrid("getPager");
		$(pager).pagination({
			onSelectPage:function(pageNumber, pageSize){
				$('#grid1').datagrid('reload', {PageIndex: pageNumber, PageSize: pageSize});			
			}
		});	
	};
	
	
	var dateRange = GetDateRange("txtStartDate", "txtEndDate");
	if( dateRange == null ) return false;
	var url = '/Ajax/Task/Query.aspx?' + $.param({Start: dateRange.StartDate, End: dateRange.EndDate});
    
	$('#grid1').datagrid({url: url });
	setPaginationEvent();
	
    return false;
}

