using MatrianMiraiBot.Coms.Games.Enums;
using Mirai_CSharp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MatrianMiraiBot.Coms.Games.Players
{
    /// <summary>
    /// 预言家
    /// </summary>
    public class Prophet : IPlayer
    {

        public override void Death(DeathInfo deathInfo)
        {
            throw new NotImplementedException();
        }

        public override void DoAction(GameCommand command)
        {
            throw new NotImplementedException();
        }

        public override void Init(GamePlayerBaseInfo info)
        {
            base.Init(info);
            Identity = IdentityType.Propheter;
        }
    }
}
