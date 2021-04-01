using Mirai_CSharp;
using Mirai_CSharp.Models;
using Mirai_CSharp.Plugin.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MatrianMiraiBot.Plguins
{
    /// <summary>
    ///狼人杀插件
    /// </summary>
    public class WolfKillPlugin : IGroupMessage
    {
        /// <summary>
        /// 接受群消息接口
        /// </summary>
        /// <param name="session"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public async Task<bool> GroupMessage(MiraiHttpSession session, IGroupMessageEventArgs e)
        {
            IMessageBase message = new PlainMessage(".net 接口测试");
            await session.SendGroupMessageAsync(e.Sender.Group.Id, message);
            return false;
        }
    }
}
