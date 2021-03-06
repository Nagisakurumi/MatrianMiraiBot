using MatrianMiraiBot.Coms.Games.Enums;
using MatrianMiraiBot.Coms.Games.Players;
using MatrianMiraiBot.Expends;
using Mirai_CSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrianMiraiBot.Coms.Games
{
    /// <summary>
    /// 游戏信息
    /// </summary>
    public class GameInfo : IGameInfo
    {
        /// <summary>
        /// 玩家列表
        /// </summary>
        public List<IPlayer> Players
        {
            get => this.GamePlayers.Cast<IPlayer>().ToList();
        }
        /// <summary>
        /// 身份集合
        /// </summary>
        public static List<IdentityType> Identities { get; set; } = new List<IdentityType>() {
             IdentityType.Farmer,IdentityType.Farmer,IdentityType.Farmer, IdentityType.Hunter, IdentityType.Propheter, IdentityType.Witcher, IdentityType.Wolfer
            , IdentityType.Wolfer, IdentityType.Wolfer
        };

        /// <summary>
        /// 可以被杀害的列表
        /// </summary>
        public List<IPlayer> CanKilledList { get; set; } = new List<IPlayer>();

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
        /// 日期
        /// </summary>
        public int Date { get; set; }


        public GameInfo()
        {
            GameState = Games.Enums.GameState.Closed;
            MaxPlayerCount = 9;
        }

        /// <summary>
        /// 初始化身份
        /// </summary>
        /// <returns></returns>
        public override async Task<bool> Init(GameInput gameInput)
        {
            if (!IsFullPlayer) return false;
            GamePlayers.Clear();
            List<IdentityType> values = Identities.ShuffleCopy(Random);
            int playerIndex = 0;
            for (int i = MaxPlayerCount - 1; i >= 0 ; i--)
            {
                int index = Random.Next(0, i + 1);
                IPlayer player = IPlayer.Factory(values[index]);
                GamePlayers.Add(player);
                player.Init(BaseInfos[playerIndex++]);
                values.RemoveAt(index);
            }

            foreach (var item in Players)
            {
                await gameInput.SendTemp("您的身份是 : {0}".Format(item.Identity), item.PlayerId);
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
        public IPlayer GetPlayerById(long id, bool? isAlive = true)
        {
            return Players.Where(p => p.PlayerId == id && (isAlive == null || isAlive == p.IsAlive)).FirstOrDefault();
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

        /// <summary>
        /// 初始化死亡
        /// </summary>
        public void InitKilleds()
        {
            WolferWillKilled = null;
            PoisonKilled = null;
            GunKilled = null;
        }

        /// <summary>
        /// 获取玩家
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public IPlayer GetPlayer(int index)
        {
            if (index >= CanKilledList.Count || index < 0)
                return null;
            return CanKilledList[index];
        }
        /// <summary>
        /// 获取当晚死的所有玩家
        /// </summary>
        /// <returns></returns>
        public List<IPlayer> GetAllKilledPlayer()
        {
            var list = new List<IPlayer>();
            if (WolferWillKilled != null && !list.Contains(WolferWillKilled))
            {
                list.Add(WolferWillKilled);
            }
            if (PoisonKilled != null && !list.Contains(PoisonKilled))
            {
                list.Add(PoisonKilled);
            }
            if (GunKilled != null && !list.Contains(GunKilled))
            {
                list.Add(GunKilled);
            }
            return list;
        }
        /// <summary>
        /// 重置投票
        /// </summary>
        public void ResetVoted()
        {
            Players.ForEach(p => p.VotedPlayer = null);
        }

        /// <summary>
        /// 获取得票率最高的玩家
        /// </summary>
        /// <returns></returns>
        public KeyValuePair<IPlayer, double> GetMaxVotedPlayer()
        {
            var dict = new Dictionary<IPlayer, double>();
            CanKilledList.ForEach(p => {
            if (p.VotedPlayer != null)
            {
                if (dict.ContainsKey(p.VotedPlayer))
                {
                    dict[p.VotedPlayer] += p.IsSheriff ? 1.5 : 1;
                }
                else
                {
                    dict.Add(p.VotedPlayer, p.IsSheriff ? 1.5 : 1);
                    }
                }
            });



            if (dict.Count == 0)
            {
                return new KeyValuePair<IPlayer, double>(null, 0);
            }
            else
            {
                var target = dict.GroupBy(p => p.Value).Select(p => new { Player = p.First().Key, Count = p.First().Value }).First();
                return new KeyValuePair<IPlayer, double>(target.Player, target.Count);
            }
        }

        /// <summary>
        /// 是否游戏结束
        /// </summary>
        /// <returns></returns>
        public bool? IsGameOver()
        {
            if(GetPlayerByIdentity(IdentityType.Wolfer).Count() == 0)
            {
                return true;
            }

            else if(GetPlayerByIdentity(IdentityType.Farmer).Count() == 0)
            {
                return false;
            }
            else if (GetPlayerByIdentity(IdentityType.Hunter).Count() == 0 &&
                GetPlayerByIdentity(IdentityType.Witcher).Count() == 0 &&
                GetPlayerByIdentity(IdentityType.Propheter).Count() == 0)
            {
                return false;
            }
            return null;
        }

        public override void Dispose()
        {
        }
    }
}
