using System;
using System.Collections.Generic;
using System.Text;

namespace MatrianMiraiBot.Coms.FiveGames
{
    public class FiveGamePlayer : IGamePlayer
    {
        /// <summary>
        /// 出手次数
        /// </summary>
        public int Number { get; set; }


        public int Index { get; set; }
    }
}
