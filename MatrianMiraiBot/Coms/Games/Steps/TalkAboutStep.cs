using MatrianMiraiBot.Coms.Games.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MatrianMiraiBot.Expends;
using System.Linq;

namespace MatrianMiraiBot.Coms.Games.Steps
{
    public class TalkAboutStep : IStep
    {

        List<IPlayer> deaths = new List<IPlayer>();

        public TalkAboutStep()
        {
            IdentityType = IdentityType.None;
            NextState = GameState.VotedStep;
            SelfState = GameState.TalkAboutStep;
            StepMessage = "进入讨论阶段!";
        }

        public override async Task DoAction(GameCommand command)
        {
            var commandItem = command.GetCommandIndex(0);
            if (commandItem.Command.Equals("next"))
            {
                Next(command);
            }
        }

        public override string GetInitMessage(GameCommand command)
        {
            var players = "";
            deaths.Clear();
            deaths.AddRange(command.GetGameInfo<GameInfo>().GetAllKilledPlayer());
            players = deaths.JoinToString(",", p => p.PlayerNickName);
            return "昨晚死亡的玩家有 : {0}\n".Format(players) +
                "开始尽情讨论， 输入(-next) 进入下一个阶段!\n";
        }

        public override bool IsEmpty(GameCommand command)
        {
            return false;
        }
    }
}
