using MatrianMiraiBot.Coms.Games.Enums;
using MatrianMiraiBot.Coms.Games.Players;
using Mirai_CSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MatrianMiraiBot.Coms.Games
{
    /// <summary>
    /// 游戏信息
    /// </summary>
    public class GameInfo
    {
        /// <summary>
        /// 玩家列表
        /// </summary>
        public List<IPlayer> Players { get; set; } = new List<IPlayer>();
        /// <summary>
        /// 基础玩家信息
        /// </summary>
        public List<GamePlayerBaseInfo> BaseInfos { get; set; } = new List<GamePlayerBaseInfo>();

        /// <summary>
        /// 最大玩家数量
        /// </summary>
        public static int MaxPlayerCount = 8;
        /// <summary>
        /// 是否满足玩家
        /// </summary>
        public bool IsFullPlayer { get => BaseInfos.Count == MaxPlayerCount; }

        /// <summary>
        /// 可以被杀害的列表
        /// </summary>
        public List<IPlayer> CanKilledList { get; set; } = new List<IPlayer>();
        /// <summary>
        /// 随机
        /// </summary>
        private Random Random { get; set; } = new Random();
        /// <summary>
        /// 即将被狼人杀害的对象
        /// </summary>
        public IPlayer WolferWillKilled { get; set; }
        /// <summary>
        /// 毒杀目标
        /// </summary>
        public IPlayer PoisonKilled { get; set; }
        /// <summary>
        /// 被枪杀
        /// </summary>
        public IPlayer GunKilled { get; set; }
        /// <summary>
        /// 添加基础玩家信息
        /// </summary>
        /// <param name="baseInfo"></param>
        public bool AddBaseInfo(GamePlayerBaseInfo baseInfo)
        {
            if (IsFullPlayer) return false;
            BaseInfos.Add(baseInfo);
            return true;
        }
        /// <summary>
        /// 初始化身份
        /// </summary>
        /// <returns></returns>
        public bool InitIdentity()
        {
            if (!IsFullPlayer) return false;

            List<IdentityType> values = new List<IdentityType>();
            foreach (IdentityType item in Enum.GetValues(typeof(IdentityType)))
            {
                values.Add(item);
            }
            int playerIndex = 0;
            for (int i = MaxPlayerCount - 1; i >= 0 ; i--)
            {
                int index = Random.Next(0, i + 1);
                IPlayer player = IPlayer.Factory(values[index]);
                Players.Add(player);
                player.Init(BaseInfos[playerIndex++]);
            }

            return true;
        }

        /// <summary>
        /// 获取存活的狼人数量
        /// </summary>
        /// <returns></returns>
        public int GetAliveWolferCount()
        {
            return Players.Where(p => p.IsAlive && p.Identity == IdentityType.Wolfer).Count();
        }
        /// <summary>
        /// 获取狼人列表
        /// </summary>
        /// <returns></returns>
        public List<IPlayer> GetWolfers(bool? isAlive = true) => Players.Where(p => (isAlive == null || p.IsAlive == isAlive) && p.Identity == IdentityType.Wolfer).ToList();
        /// <summary>
        /// 构建可以被杀害的列表
        /// </summary>
        /// <returns></returns>
        public List<IPlayer> BuildCanKillList()
        {
            this.CanKilledList = Players.Where(p => p.IsAlive).ToList();
            return this.CanKilledList;
        }
        /// <summary>
        /// 根据id 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IPlayer GetPlayerById(long id)
        {
            return Players.Where(p => p.PlayerId == id).FirstOrDefault();
        }

        /// <summary>
        /// 根据身份获取玩家
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="isAlive"></param>
        /// <returns></returns>
        public IEnumerable<IPlayer> GetPlayerByIdentity(IdentityType identity, bool? isAlive = true)
        {
            return Players.Where(p => p.Identity == identity && (isAlive == null || p.IsAlive == isAlive));
        }

        /// <summary>
        /// 根据id 获取
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public IPlayer GetPlayerByName(string name)
        {
            return Players.Where(p => p.PlayerNickName.Equals(name)).FirstOrDefault();
        }
    }
}
