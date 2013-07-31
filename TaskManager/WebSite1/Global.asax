<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
		//try {
		TaskManagerLib.Common.AppInfo.Init();
		//}
		//catch( Exception ex ) {
		//    s_initException = ex;
		//}
    }

	protected void Application_BeginRequest(object sender, EventArgs e)
	{
		//if( s_initException != null )
		//    throw s_initException;
	}
    
    void Application_End(object sender, EventArgs e) 
    {
    }
        
    void Application_Error(object sender, EventArgs e) 
    {
		Exception ex = Server.GetLastError();

		TaskManagerLib.Log.LogHelper.SafeLogException(ex);
		


		// 判断是否为AJAX请求。
		// 如果是AJAX请求，我们可以不用做任何处理，
		// 因为前端已经有统一的全局处理逻辑。
		bool isAjaxRequest = string.Compare(
			Request.Headers["X-Requested-With"],
			"XMLHttpRequest", StringComparison.OrdinalIgnoreCase) == 0;

		if( isAjaxRequest == false ) {
			// 是一个页面请求，此时我们可以这样处理：
			// 1. 本机请求（调试），那就出现黄页。
			// 2. 来自其他用户的访问，显示自定义的错误显示页面

			if( Request.IsLocal == false ) {
				// 不是本机请求
				// 首先要清除异常，防止产生黄页。
				Server.ClearError();

				Response.StatusCode = 500;	// 继续设置500的响应，供IIS日志记录

				// 这里，我直接显示所有的异常信息，
				// 如果不希望这样显示，可以修改下面方法调用的第二个参数。
				IActionResult page = new PageResult("/Pages/ApplicationError.aspx", ex.ToString());
				page.Ouput(this.Context);
			}
		}	
    }


	

	
</script>
