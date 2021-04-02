using MatrianMiraiBot.Coms.Games.Enums;
using MatrianMiraiBot.Coms.Games.Steps;
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
        public string CommandStart { get; set; } = "-game";
        /// <summary>
        /// 群信息
        /// </summary>
        public GroupInfo GroupInfo { get; set; }
        /// <summary>
        /// 玩家
        /// </summary>
        public GameInfo GameInfo { get; set; } = new GameInfo();
        /// <summary>
        /// 步骤
        /// </summary>
        public Dictionary<GameState, IStep> Steps { get; set; } = new Dictionary<GameState, IStep>() {
            { GameState.AddPlayer, new AddPlayerStep() },{ GameState.Closed, new ClosedStep() },{ GameState.HunterStep, new HunterStep() },
            { GameState.Night, new NightStep() },{ GameState.Over, new OverStep() },{ GameState.ProphetStep, new ProphetStep() },{ GameState.SheriffMoveStep, new SheriffMoveStep() },
            { GameState.SheriffSpeekStep, new SheriffSpeekStep() },{ GameState.SheriffVotedStep, new SheriffVotedStep() },{ GameState.TalkAboutStep, new TalkAboutStep() },
            { GameState.VotedStep, new VotedStep() },{ GameState.WhiteStep, new WhiteStep() },{ GameState.WitchStep, new WitcherStep() },{ GameState.WolfKillStep, new WolfKillStep() },
            
        };
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
        public async Task DealCommand(GameInput gameInput)
        {
            if (!gameInput.Command.StartsWith(CommandStart)) return;
            GameCommand gameCommand = null;
            try
            {
                var command = gameInput.Command.Substring(CommandStart.Length);
                gameCommand = new GameCommand(command, gameInput, GameInfo);

                await Step(gameCommand);
            }
            catch(Exception e)
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
                if (gameCommand.IsRunNextState)
                {
                    await Step(gameCommand);
                }
            }
        }

    }
}
