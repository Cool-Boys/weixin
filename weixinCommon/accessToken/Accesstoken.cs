using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CommonLibrary;
using Utility;

namespace weixinCommon
{
    public class AccessToken
    {
        /// <summary>
        /// 获取access token的地址
        /// </summary>
        private const string urlForGettingAccessToken = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";

        public string GetAccesstoken()
        {
            string appid = Configs.GetValue("appid");
            string secret = Configs.GetValue("secret");
            string url = string.Format(urlForGettingAccessToken, appid, secret);
            string str = NetHelper.HttpGet(url);

            var strJson = UJson.ToObject<MAccesstoken>(str);
            return strJson.access_token;
        }
    }

    public class MAccesstoken
    {
        public string access_token { get; set; }
        public string expires_in { get; set; }
    }
}
