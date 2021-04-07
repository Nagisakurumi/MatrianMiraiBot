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
            IdentityType = IdentityType.None;
            NextState = GameState.TalkAboutStep;
            SelfState = GameState.WhiteStep;
            StepMessage = "天亮了!";
        }

        public override async Task DoAction(GameCommand command)
        {
            //第一天
            if(command.GameInfo.Date == 1)
            {
                command.GameState = GameState.SheriffSpeekStep;
            }
            else
            {
                command.GameState = GameState.TalkAboutStep;
            }
            command.IsRunNextState = true;
        }

        public override string GetInitMessage(GameCommand command)
        {
            return "输入(-next) 进入下一步!";
        }

        public override bool IsEmpty(GameCommand command)
        {
            return false;
        }
    }
}
