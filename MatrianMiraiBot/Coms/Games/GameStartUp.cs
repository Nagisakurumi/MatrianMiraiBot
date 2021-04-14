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
    public class GameStartUp : IGame
    {
        /// <summary>
        /// 命令
        /// </summary>
        public static string GameStartCommand = "-game";
        /// <summary>
        /// 命令
        /// </summary>
        public static string GameDestoryCommand = "-game -destory";
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="groupInfo"></param>
        public GameStartUp(IGroupInfo groupInfo) : base(groupInfo)
        {
            GameInfo = new GameInfo();
            Steps = new Dictionary<IGameState, IGameStep>() {
                { GameState.AddPlayer, new AddPlayerStep() },{ GameState.Closed, new ClosedStep() },{ GameState.HunterStep, new HunterStep() },
                { GameState.Night, new NightStep() },{ GameState.Over, new OverStep() },{ GameState.ProphetStep, new ProphetStep() },{ GameState.SheriffMoveStep, new SheriffMoveStep() },
                { GameState.SheriffSpeekStep, new SheriffSpeekStep() },{ GameState.SheriffVotedStep, new SheriffVotedStep() },{ GameState.TalkAboutStep, new TalkAboutStep() },
                { GameState.VotedStep, new VotedStep() },{ GameState.WhiteStep, new WhiteStep() },{ GameState.WitchStep, new WitcherStep() },{ GameState.WolfKillStep, new WolfKillStep() },

            };
            CommandStart = GameStartCommand;
        }

        public override void Dispose()
        {
        }
    }
}
