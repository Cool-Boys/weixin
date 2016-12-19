using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTLF.BZHS.BLL;
using BTLF.BZHS.Model;
using CommonLibrary;
using Utility;
using weixinCommon;

namespace weixinApp.Controllers
{
    public class GHBindController : ControllerBase
    {
        //
        // GET: /GHBind/

        public ActionResult Index()
        {
            ViewData["name"] = Request.Params["nickname"];
            return View();
        }
        public ActionResult GetOpenId(string code)
        {
            weixinUser wuUser = new weixinUser();
            //获取页面授权
            var model = AccessToken.GetOauthAccesstoken(code);
            //
            string strJson = wuUser.GetUserInfo(model.openid);
            var userInfo = UJson.ToObject<MWeixinUser>(strJson);
            TZGBLL bll = new TZGBLL();
            MTZG mtzg = bll.GetModelByWhere("OPENID='" + model.openid + "'");
            userInfo.isExist = "0";
            if (mtzg != null)
            {
                userInfo.isExist = "1";
            }
            return Success("", userInfo);
        }

        public ActionResult GhBind(string openId, string nickname, string zgno)
        {
            TZGBLL bll = new TZGBLL();
            MTZG mtzg = bll.GetModelByWhere("ZG_NO='" + zgno + "'");
            string str = "";
            if (mtzg == null)
            {
                str = "没有查到用户！";
                return Error(str);
            }
            else
            {
                MTZG nmtzg = new MTZG();
                nmtzg.ZG_ID = mtzg.ZG_ID;
                nmtzg.SUBSCRIBE = "0";
                nmtzg.NICKNAME = nickname;
                nmtzg.OPENID = openId;
                bll.Update(nmtzg);
                str = "绑定工号成功！";
                return Success(str);
            }

        }
    }
}
