using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Text;
using System.Web.Security;
using System.Xml;
using BTLF.BZHS.BLL;
using BTLF.BZHS.IBLL;
using BTLF.BZHS.Model;
using Utility;
using weixinCommon;

namespace weixinApp
{
    /// <summary>
    /// 接受/发送消息帮助类
    /// </summary>
    public class messageHelp
    {
        //返回消息
        public string ReturnMessage(string postStr)
        {
            string responseContent = "";
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(new System.IO.MemoryStream(System.Text.Encoding.GetEncoding("GB2312").GetBytes(postStr)));
            XmlNode MsgType = xmldoc.SelectSingleNode("/xml/MsgType");
            if (MsgType != null)
            {
                switch (MsgType.InnerText)
                {
                    case "event":
                        responseContent = EventHandle(xmldoc);//事件处理
                        break;
                    case "text":
                        responseContent = TextHandle(xmldoc);//接受文本消息处理
                        break;
                    default:
                        break;
                }
            }
            return responseContent;
        }
        //事件
        public string EventHandle(XmlDocument xmldoc)
        {
            string responseContent = "";
            XmlNode Event = xmldoc.SelectSingleNode("/xml/Event");
            XmlNode EventKey = xmldoc.SelectSingleNode("/xml/EventKey");
            XmlNode ToUserName = xmldoc.SelectSingleNode("/xml/ToUserName");
            XmlNode FromUserName = xmldoc.SelectSingleNode("/xml/FromUserName");
            if (Event != null)
            {
                //菜单单击事件
                if (Event.InnerText.Equals("CLICK"))
                {
                    weixinUser wuUser = new weixinUser();
                    string strJson = wuUser.GetUserInfo(FromUserName.InnerText);
                    TZGBLL bll = new TZGBLL();
                    MTZG mtzg = bll.GetModelByWhere("ZG_NO='10029040'");
                    var userInfo = strJson.ToObject<MWeixinUser>();
                    if (EventKey.InnerText.Equals("V1001_GZ"))//click_one
                    {


                        responseContent = string.Format(ReplyType.Message_Text,
                            FromUserName.InnerText,
                            ToUserName.InnerText,
                            DateTime.Now.Ticks,
                            "你点击的是今日工资" + userInfo.nickname + mtzg.ZG_NO);
                    }
                    else if (EventKey.InnerText.Equals("V1001_GH"))//click_two
                    {
                        responseContent = string.Format(ReplyType.Message_Text,
                            FromUserName.InnerText,
                            ToUserName.InnerText,
                            DateTime.Now.Ticks,
                            "你点击的是绑定工号" + userInfo.nickname + mtzg.ZG_NO );
                    }
                    else if (EventKey.InnerText.Equals("V1001_GHurl"))//click_two
                    {
                        responseContent = string.Format(ReplyType.Message_Text,
                            FromUserName.InnerText,
                            ToUserName.InnerText,
                            DateTime.Now.Ticks,
                            "你点击的是绑定工号" + userInfo.nickname + mtzg.ZG_NO + "\r\n 网址 <a href=\"http://q5sdn.free.natapp.cc\">点击进入</a>");
                    }

                }
                else if (Event.InnerText.Equals("subscribe"))
                {


                    responseContent = string.Format(ReplyType.Message_Text,
                    FromUserName.InnerText,
                    ToUserName.InnerText,
                    DateTime.Now.Ticks,
                    "欢迎使用微信公共账号 腊月的肉\r\n");
                }

            }
            return responseContent;
        }
        //接受文本消息
        public string TextHandle(XmlDocument xmldoc)
        {
            string responseContent = "";
            XmlNode ToUserName = xmldoc.SelectSingleNode("/xml/ToUserName");
            XmlNode FromUserName = xmldoc.SelectSingleNode("/xml/FromUserName");
            XmlNode Content = xmldoc.SelectSingleNode("/xml/Content");

            if (Content != null)
            {
                weixinUser wuUser = new weixinUser();
                string strJson = wuUser.GetUserInfo(FromUserName.InnerText);
                var userInfo = strJson.ToObject<MWeixinUser>();
                TZGBLL bll = new TZGBLL();
                MTZG mtzg = bll.GetModelByWhere("ZG_NO='" + Content.InnerText + "'");
                
                string str = "";
                //需要进行输入内容的验证
                if (mtzg==null)
                {
                    str = "没有查到用户！";
                }
                else
                {
                    str = "查到用户姓名为" + mtzg.SNAME;
                    MTZG nmtzg = new MTZG();
                    nmtzg.ZG_ID = mtzg.ZG_ID;
                    nmtzg.SUBSCRIBE = "0";
                    nmtzg.NICKNAME = userInfo.nickname;
                    nmtzg.OPENID = userInfo.openid;
                    try
                    {
                        bll.UpdateZg(nmtzg);
                        str += "-绑定成功！";
                    }
                    catch (Exception ex)
                    {
                        str=str + ex.Message.ToString();
                        //throw new Exception(ex.Message);
                    }
                }
                responseContent = string.Format(ReplyType.Message_Text,
                    FromUserName.InnerText,
                    ToUserName.InnerText,
                    DateTime.Now.Ticks,
                    "您输入的工号为：" + Content.InnerText + " 绑定微信号为：" + userInfo.nickname + "\r\n" + str);
            }
            return responseContent;
        }

        //写入日志
        public void WriteLog(string text)
        {
            StreamWriter sw = new StreamWriter(HttpContext.Current.Server.MapPath(".") + "\\log.txt", true);
            sw.WriteLine(text);
            sw.Close();//写入
        }
    }

    //回复类型
    public class ReplyType
    {
        /// <summary>
        /// 普通文本消息
        /// </summary>
        public static string Message_Text
        {
            get
            {
                return @"<xml>
                            <ToUserName><![CDATA[{0}]]></ToUserName>
                            <FromUserName><![CDATA[{1}]]></FromUserName>
                            <CreateTime>{2}</CreateTime>
                            <MsgType><![CDATA[text]]></MsgType>
                            <Content><![CDATA[{3}]]></Content>
                            </xml>";
            }
        }
        /// <summary>
        /// 图文消息主体
        /// </summary>
        public static string Message_News_Main
        {
            get
            {
                return @"<xml>
                            <ToUserName><![CDATA[{0}]]></ToUserName>
                            <FromUserName><![CDATA[{1}]]></FromUserName>
                            <CreateTime>{2}</CreateTime>
                            <MsgType><![CDATA[news]]></MsgType>
                            <ArticleCount>{3}</ArticleCount>
                            <Articles>
                            {4}
                            </Articles>
                            </xml> ";
            }
        }
        /// <summary>
        /// 图文消息项
        /// </summary>
        public static string Message_News_Item
        {
            get
            {
                return @"<item>
                            <Title><![CDATA[{0}]]></Title> 
                            <Description><![CDATA[{1}]]></Description>
                            <PicUrl><![CDATA[{2}]]></PicUrl>
                            <Url><![CDATA[{3}]]></Url>
                            </item>";
            }
        }
    }
}