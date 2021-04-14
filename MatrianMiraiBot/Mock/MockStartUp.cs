using MatrianMiraiBot.Coms;
using MatrianMiraiBot.Coms.Games;
using MatrianMiraiBot.Coms.Games.Enums;
using MatrianMiraiBot.Expends;
using MatrianMiraiBot.Plguins;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrianMiraiBot.Mock
{
    public class MockStartUp
    {
        /// <summary>
        /// 插件
        /// </summary>
        public GamePlugin Plugin { get; set; } = new GamePlugin();

        public WolfGameMock Session = new WolfGameMock();

        public string GroupStart = "group";

        public string TempStart = "temp";

        public GamePlayerBaseInfo[] players = new GamePlayerBaseInfo[9] {
            new GamePlayerBaseInfo(1, "p1"),new GamePlayerBaseInfo(2, "p2"),
            new GamePlayerBaseInfo(3, "p3"),new GamePlayerBaseInfo(4, "p4"),
            new GamePlayerBaseInfo(5, "p5"),new GamePlayerBaseInfo(6, "p6"),
            new GamePlayerBaseInfo(7, "p7"),new GamePlayerBaseInfo(8, "p8"),
            new GamePlayerBaseInfo(9, "p9"),
        };

        public string showMsg = null;
        public string ShowMessage { set => showMsg = value;
            get
            {
                if(showMsg == null)
                {
                    showMsg = "";
                    for (int i = 0; i < players.Length; i++)
                    {
                        showMsg += "序号 : {0}, Id : {1}, Name : {2} \n".Format(i, players[i].Id, players[i].Name);
                    }
                }
                return showMsg;
            }
        }

        public async Task MockStart()
        {
            int step = 0;

            while (true)
            {
                step++;
                string content = "";
                MockGroupMessageEventArgs mockGroup = null;
                MockTempMessageEventArgs mockTemp = null;
                int index = 0;
                int groupIndex = 0;
                if (step >= 4)
                {
                    if (await DoPlayer()) continue;

                    Console.WriteLine("请选择要操作的玩家 : \n" + ShowMessage);
                    Console.WriteLine("群消息还是私信(0 : 群消息, 1 : 私聊, -1 next) : ");
                    string[] message = Console.ReadLine().Split(' ');
                    if (message[0].Equals("-1"))
                    {
                        index = 0;
                        groupIndex = 0;
                        content = "-next";
                    }
                    else
                    {
                        index = Convert.ToInt32(message[1]);
                        groupIndex = Convert.ToInt32(message[0]);

                        Console.WriteLine("请输入消息内容 : ");
                        content = Console.ReadLine();
                    }
                    
                }
                else if (step == 1)
                {
                    content = "-create";
                }
                else if (step == 2)
                {
                    content = "-start";
                }
                else if (step == 3)
                {
                    content = "-add";
                }
                //自动参赛
                if (content.Equals("-add"))
                {
                    await Join();
                    continue;
                }
                content = "-game " + content;
                if (groupIndex == 0)
                {
                    mockGroup = new MockGroupMessageEventArgs(content, players[index].Id, players[index].Name);
                    await Plugin.GroupMessage(Session, mockGroup);
                }
                else
                {
                    mockTemp = new MockTempMessageEventArgs(content, players[index].Id, players[index].Name);
                    await Plugin.TempMessage(Session, mockTemp);
                }
            }
        }
        /// <summary>
        /// 参赛
        /// </summary>
        /// <returns></returns>
        public async Task Join()
        {
            foreach (var item in players)
            {
                MockGroupMessageEventArgs mockGroup = new MockGroupMessageEventArgs("-game -add", item.Id, item.Name);
                await Plugin.GroupMessage(Session, mockGroup);
            }
        }


        public async Task<bool> DoPlayer()
        {
            GameState state = Plugin.game.GameInfo.GameState as GameState;
            GameInfo gameInfo = Plugin.game.GameInfo as GameInfo;
            List<IPlayer> players = null;
            if(state == GameState.WolfKillStep)
            {
                players = gameInfo.GetPlayerByIdentity(IdentityType.Wolfer).ToList();
            }
            else if (state == GameState.WitchStep)
            {
                players = gameInfo.GetPlayerByIdentity(IdentityType.Witcher).ToList();
            }
            else if (state == GameState.HunterStep)
            {
                players = gameInfo.GetPlayerByIdentity(IdentityType.Hunter).ToList();
            }
            else if (state == GameState.ProphetStep)
            {
                players = gameInfo.GetPlayerByIdentity(IdentityType.Propheter).ToList();
            }
            else if (state == GameState.VotedStep || state == GameState.SheriffVotedStep)
            {
                players = gameInfo.Players;
                Console.WriteLine("请输入要投票的目标序号 : ");
                int index = Convert.ToInt32(Console.ReadLine());
                MockGroupMessageEventArgs mockGroup = null;
                foreach (var item in players)
                {
                    mockGroup = new MockGroupMessageEventArgs("-game -vote " + index, item.PlayerId, item.PlayerNickName);
                    await Plugin.GroupMessage(Session, mockGroup);
                }
                mockGroup = new MockGroupMessageEventArgs("-game -next " + index, players[0].PlayerId, players[0].PlayerNickName);
                await Plugin.GroupMessage(Session, mockGroup);

                return true;
            }

            if (players == null || players.Count == 0) return false;

            foreach (var item in players)
            {
                Console.WriteLine("玩家 : {0}, 请输入操作 : ".Format(item.PlayerNickName));
                string message = Console.ReadLine();
                MockTempMessageEventArgs mockTemp = new MockTempMessageEventArgs("-game " + message, item.PlayerId, item.PlayerNickName);
                await Plugin.TempMessage(Session, mockTemp);
            }

            return true;
        }
    }
}
