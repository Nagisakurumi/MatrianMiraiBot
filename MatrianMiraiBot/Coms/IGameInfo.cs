using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrianMiraiBot.Coms
{
    public abstract class IGameInfo : IDisposable
    {
        /// <summary>
        /// 游戏状态
        /// </summary>
        public IGameState GameState { get; set; }

        /// <summary>
        /// 最大玩家数量
        /// </summary>
        public int MaxPlayerCount = 9;
        /// <summary>
        /// 随机
        /// </summary>
        protected Random Random { get; set; } = new Random();
        /// <summary>
        /// 是否满足玩家
        /// </summary>
        public bool IsFullPlayer { get => BaseInfos.Count == MaxPlayerCount; }
        /// <summary>
        /// 游戏玩家
        /// </summary>
        public List<IGamePlayer> GamePlayers { get; set; } = new List<IGamePlayer>();
        /// <summary>
        /// 玩家基础信息
        /// </summary>
        public List<GamePlayerBaseInfo> BaseInfos { get; set; } = new List<GamePlayerBaseInfo>();

        /// <summary>
        /// 根据id 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IGamePlayer GetPlayerById(long id)
        {
            return GamePlayers.Where(p => p.PlayerId == id).FirstOrDefault();
        }
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
        /// 初始化
        /// </summary>
        /// <param name="gameInput"></param>
        /// <returns></returns>
        public abstract Task<bool> Init(GameInput gameInput);

        public abstract void Dispose();
    }
}
