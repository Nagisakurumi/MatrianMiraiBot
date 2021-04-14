using MatrianMiraiBot.Coms.FiveGames;
using MatrianMiraiBot.Coms.Games.Steps;
using Mirai_CSharp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MatrianMiraiBot.Coms
{
    public abstract class IGame : IDisposable
    {
        /// <summary>
        /// 起始命令
        /// </summary>
        public string CommandStart { get; set; }
        /// <summary>
        /// 销毁的命令
        /// </summary>
        public string CmmandDestory { get; set; }
        /// <summary>
        /// 游戏名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 群信息
        /// </summary>
        public IGroupInfo GroupInfo { get; set; }
        /// <summary>
        /// 玩家
        /// </summary>
        public IGameInfo GameInfo { get; set; }
        /// <summary>
        /// 步骤
        /// </summary>
        public Dictionary<IGameState, IGameStep> Steps { get; set; } = new Dictionary<IGameState, IGameStep>();
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="groupInfo"></param>
        public IGame(GameCommand gameCommand, IGroupInfo groupInfo)
        {
            GroupInfo = groupInfo;
        }

        /// <summary>
        /// 处理命令
        /// </summary>
        /// <param name="gameInput"></param>
        public virtual async Task DealCommand(GameInput gameInput)
        {
            if (gameInput.GroupInfo.Id != this.GroupInfo.Id) return;

            if (!gameInput.Command.StartsWith(CommandStart)) return;
            GameCommand gameCommand = null;
            try
            {
                //var command = gameInput.Command.Substring(CommandStart.Length);
                gameCommand = new GameCommand(gameInput.Command, gameInput, GameInfo);

                await Step(gameCommand);
            }
            catch (Exception e)
            {
                await gameInput.ReplyGroup(e.Message);
                return;
            }
        }

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="gameCommand"></param>
        public async Task Step(GameCommand gameCommand)
        {
            //步骤
            var step = Steps[gameCommand.GameState];

            if (gameCommand.IsRunNextState)
            {
                await step.Init(gameCommand);
            }
            else
            {
                await step.DoAction(gameCommand);
            }
            if (gameCommand.IsRunNextState)
            {
                await Step(gameCommand);
            }
        }
        /// <summary>
        /// 释放内存
        /// </summary>
        public abstract void Dispose();
    }
}
