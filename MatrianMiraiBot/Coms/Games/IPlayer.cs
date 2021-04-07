using MatrianMiraiBot.Coms.Games.Enums;
using MatrianMiraiBot.Coms.Games.Players;
using MatrianMiraiBot.Expends;
using Mirai_CSharp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MatrianMiraiBot.Coms.Games
{
    /// <summary>
    /// 玩家
    /// </summary>
    public abstract class IPlayer
    {
        /// <summary>
        /// 身份
        /// </summary>
        public IdentityType Identity { get; protected set; }
        /// <summary>
        /// 投票目标玩家
        /// </summary>
        public IPlayer VotedPlayer { get; set; }
        /// <summary>
        /// 是否是警长
        /// </summary>
        public bool IsSheriff { get; set; }
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

        /// <summary>
        /// 票数
        /// </summary>
        public double GetVotedNumber { get => IsSheriff ? 1.5 : 1; }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="info"></param>
        public virtual void Init(GamePlayerBaseInfo info)
        {
            PlayerId = info.Id;
            PlayerNickName = info.Name;
            VotedPlayer = null;
            IsAlive = true;
            IsSheriff = false;
        }
        /// <summary>
        /// 操作
        /// </summary>
        /// <param name="command"></param>
        public abstract void DoAction(GameCommand command);
        /// <summary>
        /// 死亡
        /// </summary>
        /// <param name="deathInfo"></param>
        public virtual void Death(DeathInfo deathInfo)
        {
            this.IsAlive = false;
        }

        /// <summary>
        /// 工厂
        /// </summary>
        /// <param name="identityType"></param>
        /// <returns></returns>
        public static IPlayer Factory(IdentityType identityType)
        {
            switch (identityType)
            {
                case IdentityType.Farmer:
                    return new Farmer();
                case IdentityType.Wolfer:
                    return new Wolfer();
                case IdentityType.Witcher:
                    return new Witcher();
                case IdentityType.Propheter:
                    return new Prophet();
                case IdentityType.Hunter:
                    return new Hunter();
                default:
                    break;
            }
            return null;
        }

        public new string ToString()
        {
            return "玩家 : {0}".Format(PlayerNickName);
        }
    }
}
