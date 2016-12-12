using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommonLibrary;
using CommonLibrary.Log;

namespace weixinApp
{
    /// <summary>
    /// 自定义controler，统一处理数据
    /// </summary>
    public class ControllerBase : Controller
    {
        public Log FileLog
        {
            get { return LogFactory.GetLogger(this.GetType().ToString()); }
        }
        protected virtual ActionResult Success(string message)
        {
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = message }.ToJson());
        }
        protected virtual ActionResult Success(string message, object data)
        {
            return Content(new AjaxResult { state = ResultType.success.ToString(), message = message, data = data }.ToJson());
        }
        protected virtual ActionResult Error(string message)
        {
            return Content(new AjaxResult { state = ResultType.error.ToString(), message = message }.ToJson());
        }
    }
}