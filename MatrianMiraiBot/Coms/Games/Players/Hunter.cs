using MatrianMiraiBot.Coms.Games.Enums;
using Mirai_CSharp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MatrianMiraiBot.Coms.Games.Players
{
    public class Hunter : IPlayer
    {
        /// <summary>
        /// 枪
        /// </summary>
        public int Gun { get; set; } = 1;


        public override void DoAction(GameCommand command)
        {
            throw new NotImplementedException();
        }

        public override void Init(GamePlayerBaseInfo info)
        {
            Gun = 1;
            base.Init(info);
            Identity = IdentityType.Hunter;

        }
    }
}
