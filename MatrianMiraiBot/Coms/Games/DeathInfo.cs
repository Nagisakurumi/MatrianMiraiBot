using MatrianMiraiBot.Coms.Games.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace MatrianMiraiBot.Coms.Games
{
    /// <summary>
    /// 死亡信息
    /// </summary>
    public class DeathInfo
    {
        /// <summary>
        /// 是否是晚上
        /// </summary>
        public bool IsBlack { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public int Date { get; set; }
        /// <summary>
        /// 死亡方式
        /// </summary>
        public DeathType DeathType { get; set; }

    }
}
