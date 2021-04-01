using MatrianMiraiBot.Coms.Games.Enums;
using Mirai_CSharp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MatrianMiraiBot.Coms.Games.Players
{
    /// <summary>
    /// 女巫
    /// </summary>
    public class Witcher : IPlayer
    {

        /// <summary>
        /// 毒药
        /// </summary>
        public int Poison { get; set; } = 1;
        /// <summary>
        /// 解药
        /// </summary>
        public int Antidote { get; set; } = 1;



        public override void DoAction(GameCommand command)
        {
            throw new NotImplementedException();
        }

        public override void Init(GamePlayerBaseInfo info)
        {
            base.Init(info);
            Identity = IdentityType.Witcher;
            Poison = 1;
            Antidote = 1;
        }
    }
}
