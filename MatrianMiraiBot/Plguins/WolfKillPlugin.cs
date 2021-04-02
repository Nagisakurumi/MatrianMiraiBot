using MatrianMiraiBot.Coms.Games;
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
    public class WolfKillPlugin : IGroupMessage, ITempMessage
    {

        //public GameStartUp game = new GameStartUp();

        /// <summary>
        /// 接受群消息接口
        /// </summary>
        /// <param name="session"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public async Task<bool> GroupMessage(MiraiHttpSession session, IGroupMessageEventArgs e)
        {

            foreach (var item in e.Chain)
            {
                await session.SendGroupMessageAsync(e.Sender.Group.Id, item);
            }
            return false;
        }

        public Task<bool> TempMessage(MiraiHttpSession session, ITempMessageEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
