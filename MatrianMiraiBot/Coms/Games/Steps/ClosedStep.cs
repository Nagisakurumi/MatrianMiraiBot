using MatrianMiraiBot.Coms.Games.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MatrianMiraiBot.Coms.Games.Steps
{
    public class ClosedStep : IStep
    {

        public ClosedStep()
        {
            IsToGroup = true;
            IdentityType = IdentityType.None;
            NextState = GameState.AddPlayer;
            SelfState = GameState.Closed;
            StepMessage = "进入游戏开始阶段!";
        }

        public override async Task DoAction(GameCommand command)
        {
            if (command.GetCommandIndex(0).Command.Equals("start"))
            {
                Next(command);
            }
        }

        public override string GetInitMessage(GameCommand command)
        {
            return "输入 (-start) 进入参赛阶段!";
        }

        public override bool IsEmpty(GameCommand command)
        {
            return false;
        }
    }
}
