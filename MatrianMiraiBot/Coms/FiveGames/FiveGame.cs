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
        public FiveGame(GameCommand gameCommand, IGroupInfo groupInfo) : base(gameCommand, groupInfo)
        {
            int row = Convert.ToInt32(gameCommand.Commands[0].Contents[0]);
            int col = Convert.ToInt32(gameCommand.Commands[0].Contents[1]);
            GameInfo = new GameInfo(row, col);
            Steps = new Dictionary<IGameState, IGameStep>()
            {
                { FiveGameState.AddPlayer, new AddPlayerStep() }, { FiveGameState.Play, new PlayStep() },
                { FiveGameState.Over, new OverStep() }, { FiveGameState.Init, new InitStep() }
            };
            CommandStart = GameStartCommand;
            CmmandDestory = GameDestoryCommand;
            Name = "五子棋";
        }

        public override void Dispose()
        {
            GameInfo.Dispose();
        }
    }
}
