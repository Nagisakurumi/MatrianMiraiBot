using MatrianMiraiBot.Coms.FiveGames.Steps;
using Mirai_CSharp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MatrianMiraiBot.Coms.FiveGames
{
    public class FiveGame : IGame
    {
        /// <summary>
        /// 命令
        /// </summary>
        public static string GameStartCommand = "-five";
        /// <summary>
        /// 命令
        /// </summary>
        public static string GameDestoryCommand = "-five -destory";
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public FiveGame(int row, int col, IGroupInfo groupInfo) : base(groupInfo)
        {
            GameInfo = new GameInfo(row, col);
            Steps = new Dictionary<IGameState, IGameStep>()
            {
                { FiveGameState.AddPlayer, new AddPlayerStep() }, { FiveGameState.Play, new PlayStep() },
                { FiveGameState.Over, new OverStep() }, { FiveGameState.Init, new InitStep() }
            };
            CommandStart = GameStartCommand;
        }

        public override void Dispose()
        {
            GameInfo.Dispose();
        }
    }
}
