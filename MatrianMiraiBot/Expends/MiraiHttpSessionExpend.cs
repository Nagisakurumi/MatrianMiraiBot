using MatrianMiraiBot.Coms.Games;
using Mirai_CSharp;
using Mirai_CSharp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MatrianMiraiBot.Expends
{
    public static class MiraiHttpSessionExpend
    {

        /// <summary>
        /// 转换到消息内容
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static IMessageBase ToMessage(this string message)
        {
            return new PlainMessage(message);
        }
        /// <summary>
        /// 转换到带序号的字符串
        /// </summary>
        /// <param name="players"></param>
        /// <returns></returns>
        public static string ToIndexMessage(this IEnumerable<IPlayer> players)
        {
            string content = "";

            int index = 0;
            foreach (var item in players)
            {
                content += "{0} : {1}\n".Format(index ++, item.PlayerNickName);
            }

            return content;
        }
    }
}
