using MatrianMiraiBot.Expends;
using Mirai_CSharp;
using Mirai_CSharp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MatrianMiraiBot.Coms.Games
{
    /// <summary>
    /// 游戏启动容器
    /// </summary>
    public class GameStartUp
    {

        /// <summary>
        /// 起始命令
        /// </summary>
        public string CommandStart { get; set; } = "-wolfer";
        /// <summary>
        /// 群信息
        /// </summary>
        public GroupInfo GroupInfo { get; set; }
        /// <summary>
        /// 玩家
        /// </summary>
        public GameInfo GameInfo { get; set; } = new GameInfo();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="groupInfo"></param>
        public GameStartUp(GroupInfo groupInfo)
        {
            GroupInfo = groupInfo;
        }

        /// <summary>
        /// 处理命令
        /// </summary>
        /// <param name="gameInput"></param>
        public void DealCommand(GameInput gameInput)
        {
            if (!gameInput.Command.StartsWith(CommandStart)) return;
            GameCommand gameCommand = null;
            try
            {
                var command = gameInput.Command.Substring(CommandStart.Length);
                gameCommand = new GameCommand(command, gameInput, GameInfo);
            }
            catch(Exception e)
            {
                gameInput.ReplyGroup(e.Message);
                return;
            }


        } 

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="gameCommand"></param>
        public void Step(GameCommand gameCommand)
        {

        }

    }
}
