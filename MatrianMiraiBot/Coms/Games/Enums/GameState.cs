using System;
using System.Collections.Generic;
using System.Text;

namespace MatrianMiraiBot.Coms.Games.Enums
{
    /// <summary>
    /// 当前进行的游戏状态
    /// </summary>
    public enum GameState
    {
        /// <summary>
        /// 参赛阶段
        /// </summary>
        AddPlayer,

        /// <summary>
        /// 游戏结束
        /// </summary>
        Closed,

        /// <summary>
        /// 夜晚阶段
        /// </summary>
        Night,
        /// <summary>
        /// 白天阶段
        /// </summary>
        WhiteStep,
        /// <summary>
        /// 狼人杀人阶段
        /// </summary>
        WolfKillStep,

        /// <summary>
        /// 女巫阶段
        /// </summary>
        WitchStep,
        /// <summary>
        /// 猎人阶段
        /// </summary>
        HunterStep,
        /// <summary>
        /// 预言家阶段
        /// </summary>
        ProphetStep,
        /// <summary>
        /// 发言阶段
        /// </summary>
        TalkAboutStep,
        /// <summary>
        /// 白天投票阶段
        /// </summary>
        VotedStep,
        /// <summary>
        /// 警长发言阶段
        /// </summary>
        SheriffSpeekStep,
        /// <summary>
        /// 警长投票阶段
        /// </summary>
        SheriffVotedStep,
        /// <summary>
        /// 移交警徽
        /// </summary>
        SheriffMoveStep,
    }
}
