using MatrianMiraiBot.Coms.Games;
using MatrianMiraiBot.Expends;
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

        public GameStartUp game;

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
                if(item.Type.Equals("Plain"))
                {
                    //await session.SendGroupMessageAsync(e.Sender.Group.Id, item);
                    string message = item.ToString();
                    GameInput gameInput = new GameInput(session, message, e.Sender, e.Sender.Group);
                    if (message.Equals("-game -create"))
                    {
                        if (game == null)
                        {
                            game = new GameStartUp(e.Sender.Group);
                            await gameInput.ReplyGroup("群{0}成功创建狼人杀游戏!".Format(e.Sender.Group.Name));
                        }
                        else
                            await gameInput.ReplyGroup("该群已经开始游戏了!");
                    }
                    else if (message.Equals("-game -destory"))
                    {
                        game = null;
                        await gameInput.ReplyGroup("游戏被关闭!");
                    }
                    else if (game != null && message.StartsWith(game.CommandStart))
                    {
                        await game.DealCommand(gameInput);
                    }
                }
            }
            return false;
        }

        public async Task<bool> TempMessage(MiraiHttpSession session, ITempMessageEventArgs e)
        {
            foreach (var item in e.Chain)
            {
                if (item.Type.Equals("Plain"))
                {
                    string message = item.ToString();
                    if (game != null && message.StartsWith(game.CommandStart))
                    {
                        await game.DealCommand(new GameInput(session, message, e.Sender, e.Sender.Group));
                    }
                }
            }
            return false;
        }
    }
}
