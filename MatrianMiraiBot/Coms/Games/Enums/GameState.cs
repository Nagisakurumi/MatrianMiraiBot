using System;
using System.Collections.Generic;
using System.Text;

namespace MatrianMiraiBot.Coms.Games.Enums
{
    /// <summary>
    /// 当前进行的游戏状态
    /// </summary>
    public class GameState : IGameState
    {

        private GameState(int value):base(value) { }
        /// <summary>
        /// 参赛阶段
        /// </summary>
        public static GameState AddPlayer = new GameState(1);

        /// <summary>
        /// 游戏结束
        /// </summary>
        public static GameState Closed = new GameState(2);

        /// <summary>
        /// 夜晚阶段
        /// </summary>
        public static GameState Night = new GameState(3);
        /// <summary>
        /// 白天阶段
        /// </summary>
        public static GameState WhiteStep = new GameState(4);
        /// <summary>
        /// 狼人杀人阶段
        /// </summary>
        public static GameState WolfKillStep = new GameState(5);

        /// <summary>
        /// 女巫阶段
        /// </summary>
        public static GameState WitchStep = new GameState(6);
        /// <summary>
        /// 猎人阶段
        /// </summary>
        public static GameState HunterStep = new GameState(7);
        /// <summary>
        /// 预言家阶段
        /// </summary>
        public static GameState ProphetStep = new GameState(8);
        /// <summary>
        /// 发言阶段
        /// </summary>
        public static GameState TalkAboutStep = new GameState(9);
        /// <summary>
        /// 白天投票阶段
        /// </summary>
        public static GameState VotedStep = new GameState(10);
        /// <summary>
        /// 警长发言阶段
        /// </summary>
        public static GameState SheriffSpeekStep = new GameState(11);
        /// <summary>
        /// 警长投票阶段
        /// </summary>
        public static GameState SheriffVotedStep = new GameState(12);
        /// <summary>
        /// 移交警徽
        /// </summary>
        public static GameState SheriffMoveStep = new GameState(13);
        /// <summary>
        /// 游戏结束
        /// </summary>
        public static GameState Over = new GameState(14);
    }
}
