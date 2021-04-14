using System;
using System.Collections.Generic;
using System.Text;

namespace MatrianMiraiBot.Coms
{
    public abstract class IGamePlayer
    {
        /// <summary>
        /// 是否存活
        /// </summary>
        public bool IsAlive { get; set; }
        /// <summary>
        /// 玩家id
        /// </summary>
        public long PlayerId { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string PlayerNickName { get; set; }
    }
}
