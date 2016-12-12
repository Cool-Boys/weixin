﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace weixinCommon
{
    /// <summary>
    /// 发给用户消息类型：公众号发给用户的消息类型
    /// </summary>
    public enum  MsgTypeSendEnum
    {
        /// <summary>
        /// 文本
        /// </summary>
        text,
        /// <summary>
        /// 图片
        /// </summary>
        image,
        /// <summary>
        /// 语音
        /// </summary>
        voice,
        /// <summary>
        /// 视频
        /// </summary>
        video,
        /// <summary>
        /// 音乐
        /// </summary>
        music,
        /// <summary>
        /// 图文
        /// </summary>
        news,
        /// <summary>
        /// 转发客服
        /// </summary>
        transfer_customer_service
    }
}
