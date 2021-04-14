using MatrianMiraiBot.Coms.Games.Enums;
using MatrianMiraiBot.Coms.Games.Players;
using MatrianMiraiBot.Expends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrianMiraiBot.Coms.Games.Steps
{
    public class WitcherStep : IStep
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        public WitcherStep()
        {
            IsToGroup = false;
            IdentityType = IdentityType.Witcher;
            NextState = GameState.HunterStep;
            SelfState = GameState.WitchStep;
            StepMessage = "进入女巫环节!";

        }

        public override async Task DoAction(GameCommand command)
        {
            var witcher = command.GetGameInfo<GameInfo>().GetPlayerByIdentity(IdentityType.Witcher).FirstOrDefault() as Witcher;
            if (!CheckIdentity(command))
            {
                await command.GameInput.ReplyTemp("只有女巫才可以操作!");
                return;
            }
            if (command.Commands.Where(p => p.Command.Equals("empty")).Count() > 0)
            {
                Next(command);
                return;
            }

            foreach (var item in command.Commands)
            {
                //救人命令
                if (item.Command.Equals("save") && witcher.Antidote > 0)
                {
                    witcher.Antidote--;
                    command.GetGameInfo<GameInfo>().WolferWillKilled = null;
                    await command.GameInput.ReplyTemp("成功救下!");
                }
                else if (item.Command.Equals("poison") && witcher.Poison > 0)
                {
                    int index = Convert.ToInt32(item.Contents.First());
                    //获取期望毒杀的目标
                    var player = command.GetGameInfo<GameInfo>().GetPlayer(index);
                    if (player != null)
                    {
                        command.GetGameInfo<GameInfo>().PoisonKilled = player;
                        witcher.Poison--;
                    }
                }
            }

            Next(command);
        }

        public override string GetInitMessage(GameCommand command)
        {
            var player = command.GetGameInfo<GameInfo>().GetPlayerByIdentity(IdentityType.Witcher).FirstOrDefault();
            if (player == null)
                return null;
            var witcher = player as Witcher;
            string content = "";
            var killed = command.GetGameInfo<GameInfo>().WolferWillKilled;
            if (witcher != null)
            {
                if (killed != null)
                {
                    content += "今天晚上被杀害的玩家是{0},".Format(killed.PlayerNickName);
                    if (witcher.Antidote > 0)
                    {
                        content += "是否使用解药 (-save),";
                    }
                }
                if (witcher.Poison > 0)
                {
                    var list = command.GetGameInfo<GameInfo>().BuildCanKillList();
                    content += "是否使用毒药 (-poison {序号})\n" + list.ToIndexMessage();
                }
            }
            content += "\n 使用命令 (-empty) 表示空过不操作!";
            return content;
        }

        public override bool IsEmpty(GameCommand command)
        {
            var player = command.GetGameInfo<GameInfo>().GetPlayerByIdentity(IdentityType.Witcher).FirstOrDefault() as Witcher;
            return player == null || (player.Antidote == 0 && player.Poison == 0);
        }
    }
}
