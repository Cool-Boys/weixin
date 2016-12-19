using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Utility;

namespace weixinCommon
{

    public class weixinUser
    {
        /// <summary>
        /// 获取微信用户的地址
        /// </summary>
        private const string urlForGettingUser = "https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang=zh_CN";

        public string GetUserInfo(string openid)
        {
            AccessToken accessToken = new AccessToken();
            string at = accessToken.GetAccesstoken();
            string url = string.Format(urlForGettingUser, at, openid);
            string str = NetHelper.HttpGet(url);

            return str;
        }

    }
    public class MWeixinUser
    {
        /*
         subscribe	用户是否订阅该公众号标识，值为0时，代表此用户没有关注该公众号，拉取不到其余信息。
         openid	用户的标识，对当前公众号唯一
         nickname	用户的昵称
         sex	用户的性别，值为1时是男性，值为2时是女性，值为0时是未知
         city	用户所在城市
         country	用户所在国家
         province	用户所在省份
         language	用户的语言，简体中文为zh_CN
         headimgurl	用户头像，最后一个数值代表正方形头像大小（有0、46、64、96、132数值可选，0代表640*640正方形头像），用户没有头像时该项为空。若用户更换头像，原有头像URL将失效。
         subscribe_time	用户关注时间，为时间戳。如果用户曾多次关注，则取最后关注时间
         unionid	只有在用户将公众号绑定到微信开放平台帐号后，才会出现该字段。详见：获取用户个人信息（UnionID机制）
         remark	公众号运营者对粉丝的备注，公众号运营者可在微信公众平台用户管理界面对粉丝添加备注
         groupid	用户所在的分组ID
         */

        /// <summary>
        /// 用户是否订阅该公众号标识，值为0时，代表此用户没有关注该公众号，拉取不到其余信息。
        /// </summary>
        public string subscribe { get; set; }
        /// <summary>
        /// 用户的标识，对当前公众号唯一
        /// </summary>
        public string openid { get; set; }
        /// <summary>
        /// 用户的昵称
        /// </summary>
        public string nickname { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string province { get; set; }
        public string headimgurl { get; set; }
        public string subscribe_time { get; set; }
        public string unionid { get; set; }
        public string remark { get; set; }
        public string groupid { get; set; }
       /// <summary>
       /// 是否已经绑定
       /// </summary>
        public string isExist { get; set; }


    }
}
