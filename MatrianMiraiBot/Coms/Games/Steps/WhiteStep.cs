using MatrianMiraiBot.Coms.Games.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MatrianMiraiBot.Coms.Games.Steps
{
    public class WhiteStep : IStep
    {


        public WhiteStep()
        {
            IsToGroup = true;
            IdentityType = IdentityType.Hunter;
            NextState = GameState.WhiteStep;
            SelfState = GameState.HunterStep;
            StepMessage = "进入猎人阶段!";
        }

        public override Task DoAction(GameCommand command)
        {
            throw new NotImplementedException();
        }

        public override string GetInitMessage(GameCommand command)
        {
            throw new NotImplementedException();
        }

        public override bool IsEmpty(GameCommand command)
        {

        }
    }
}
