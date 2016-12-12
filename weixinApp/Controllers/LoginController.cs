using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BTLF.BZHS.BLL;
using BTLF.BZHS.DAL;
using BTLF.BZHS.Model;
using CommonLibrary;
using Utility;

namespace weixinApp.Controllers
{
    public class LoginController : ControllerBase
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 登录用户验证
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="code">登录令牌</param>
        /// <returns></returns>
        public ActionResult CheckLogin(string username, string password, string code)
        {
            TUSERSDAL bll = new TUSERSDAL();
            OperatorModel muserModel = new OperatorModel();
            if (string.IsNullOrEmpty(username))
            {
                return Error("用户名为空！");
            }
            MValidateUserSite user = null;
            try
            {
                user = bll.GetUserByUserName(username);
                if (user==null)
                {
                    return Error("用户名不存在！"); 
                }
                if (user.PASSWORD!=password)
                {
                    return Error("密码错误！"); 
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            muserModel.UserId =Ext.ToString(user.USER_ID);
            muserModel.UserName = user.ZGNAME == "" ? username : user.ZGNAME;
            muserModel.UserPwd = user.PASSWORD;
            muserModel.RoleId = user.ROLE_ID;
            muserModel.deptId = Ext.ToString(user.DEPT_ID);
            muserModel.deptIdName = user.DEPT_IDNAME;
            muserModel.LoginIPAddress = Net.Ip;
            muserModel.LoginTime =DateTime.Now;
            muserModel.IsSystem = muserModel.UserName == "admin" ? true : false;
            //写入到系统session或cookie中
            OperatorProvider.Provider.AddCurrent(muserModel);
            return Success("登录成功！");
        }


    }
}
