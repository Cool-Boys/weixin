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
        private const string urlForGettingAccessToken = "https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code";
        public ActionResult GetOpenId(string code)
        {
            weixinUser wuUser = new weixinUser();
            string appid = Configs.GetValue("appid");
            string secret = Configs.GetValue("secret");
            string url = string.Format(urlForGettingAccessToken, appid, secret, code);
            string str = NetHelper.HttpGet(url);
            var strJson1 = UJson.ToObject<MAccesstoken>(str);
            string strJson = wuUser.GetUserInfo(strJson1.openid);
            var userInfo = UJson.ToObject<MWeixinUser>(strJson);
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
                str = "绑定工号成功！";
                return Success(str);
            }

        }
    }
}
