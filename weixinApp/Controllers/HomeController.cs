using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using BTLF.BZHS.BLL;
using BTLF.BZHS.Model;
using BTLF.Core;
using CommonLibrary;

namespace weixinApp.Controllers
{
    //[HandlerLogin]
    public class HomeController : ControllerBase
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            TXH_CLBLL bll = new TXH_CLBLL();
            List<MTXH_CLSite> list = bll.WapQueryData(5,1);
            return View(list);
        }
        public ActionResult IndexPic()
        {
            
            return View();
        }

        public ActionResult Index2()
        {
            TXH_CLBLL bll = new TXH_CLBLL();
            List<MTXH_CLSite> list = bll.WapQueryData(5, 1);
            return View(list);
        }

        public ActionResult IndexHome()
        {
            TXH_CLBLL bll = new TXH_CLBLL();
            List<MTXH_CLSite> list = bll.WapQueryData(5, 1);
            return View(list);
        }

        public ActionResult getCl(int count)
        {
            TXH_CLBLL bll = new TXH_CLBLL();
            List<MTXH_CLSite> list = bll.WapQueryData(5,count);
            // LResult lResult = LResult.Success(list);
            //初始1：序列化json字符串,系统写法
            //初始1：string jsonData = new JavaScriptSerializer().Serialize(list);
            //返回方式1：return Content(jsonData);
            //进化2：返回json数组
            //进化2返回：return Json(list,JsonRequestBehavior.AllowGet);
            string jsonData = list.ToJson();
            return Success(jsonData);
        }

        [HttpGet]
        public ActionResult Detail(string clNo)
        {
            TXH_CLBLL bll = new TXH_CLBLL();
            List<MTXH_CLSite> list = bll.QueryData(clNo, "", 0);
            MTXH_CLSite model = null;
            if (list.Count > 0)
            {
                model = list[0];
            }
            return Success(model.ToJson());
        }

    }
}
