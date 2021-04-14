using MatrianMiraiBot.Coms.Games.Enums;
using MatrianMiraiBot.Expends;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MatrianMiraiBot.Coms.Games.Steps
{
    public class OverStep : IStep
    {

        public OverStep()
        {
            IsToGroup = true;
            IdentityType = IdentityType.None;
            NextState = GameState.Over;
            SelfState = GameState.Over;
            StepMessage = "游戏结束!";
        }

        public override async Task DoAction(GameCommand command)
        {

        }

        public override string GetInitMessage(GameCommand command)
        {
            bool res = command.GetGameInfo<GameInfo>().IsGameOver().Value;
            return "游戏结束 狼人 : {0}, 神民 : {1}".Format(res ? "失败" : "胜利", res ? "胜利" : "失败");
        }

        public override bool IsEmpty(GameCommand command)
        {
            return false;
        }
    }
}
