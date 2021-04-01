﻿using MatrianMiraiBot.Coms.Games.Enums;
using Mirai_CSharp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MatrianMiraiBot.Coms.Games.Players
{
    public class Wolfer : IPlayer
    {

        /// <summary>
        /// 期望杀害的玩家
        /// </summary>
        public IPlayer KillTargetPlayer { get; set; }


        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="command"></param>
        public override void DoAction(GameCommand command)
        {

        }

        public override void Init(GamePlayerBaseInfo info)
        {
            base.Init(info);
            Identity = IdentityType.Wolfer;
            KillTargetPlayer = null;
        }

        /// <summary>
        /// 重置
        /// </summary>
        public void Reset()
        {
            KillTargetPlayer = null;
        }
    }
}
