using MatrianMiraiBot.Coms.Games.Enums;
using Mirai_CSharp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MatrianMiraiBot.Coms.Games.Players
{
    public class Farmer : IPlayer
    {


        public override void DoAction(GameCommand command)
        {
        }

        public override void Init(GamePlayerBaseInfo info)
        {
            base.Init(info);
            Identity = IdentityType.Farmer;
        }
    }
}
