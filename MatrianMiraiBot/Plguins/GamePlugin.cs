using MatrianMiraiBot.Coms;
using MatrianMiraiBot.Coms.FiveGames;
using MatrianMiraiBot.Coms.Games;
using MatrianMiraiBot.Expends;
using Mirai_CSharp;
using Mirai_CSharp.Models;
using Mirai_CSharp.Plugin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrianMiraiBot.Plguins
{
    /// <summary>
    ///狼人杀插件
    /// </summary>
    public class GamePlugin : IGroupMessage, ITempMessage
    {

        /// <summary>
        /// 游戏选择器
        /// </summary>
        public GameSelector GameSelector { get; set; } = new GameSelector();
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
                    string message = item.ToString();
                    GameInput gameInput = new GameInput(session, message, e.Sender, e.Sender.Group);
                    try
                    {
                        await GameSelector.DealCommand(gameInput);
                    }
                    catch (Exception ex)
                    {
                        await gameInput.ReplyGroup("命令错误!" + ex.Message);    
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
                    GameInput gameInput = new GameInput(session, message, e.Sender, e.Sender.Group);
                    try
                    {
                        await GameSelector.DealCommand(gameInput);
                    }
                    catch (Exception ex)
                    {
                        await gameInput.ReplyGroup("命令错误!" + ex.Message);
                    }
                }
            }
            return false;
        }
    }
}
