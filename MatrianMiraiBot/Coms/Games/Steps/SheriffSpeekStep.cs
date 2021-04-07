using MatrianMiraiBot.Coms.Games.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MatrianMiraiBot.Coms.Games.Steps
{
    public class SheriffSpeekStep : IStep
    {

        public SheriffSpeekStep()
        {
            IsToGroup = true;
            IdentityType = IdentityType.None;
            NextState = GameState.SheriffVotedStep;
            SelfState = GameState.SheriffSpeekStep;
            StepMessage = "进入竞选警长演讲阶段!";
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
            return "输入命令 (-next) 进入下一个阶段!";
        }

        public override bool IsEmpty(GameCommand command)
        {
            return false;
        }
    }
}
