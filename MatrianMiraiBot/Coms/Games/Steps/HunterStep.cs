using MatrianMiraiBot.Coms.Games.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrianMiraiBot.Coms.Games.Steps
{
    public class HunterStep : IStep
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public HunterStep()
        {
            IsToGroup = false;
            IdentityType = IdentityType.Hunter;
            NextState = GameState.WitchStep;
            SelfState = GameState.HunterStep;
            StepMessage = "进入猎人阶段!";
        }

        public override Task DoAction(GameCommand command)
        {
            throw new NotImplementedException();
        }

        public override string GetInitMessage(GameCommand command)
        {
            
        }
        /// <summary>
        /// 是否需要空过
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public override bool IsEmpty(GameCommand command)
        {
            var hunter = command.GameInfo.GetPlayerByIdentity(IdentityType.Hunter).FirstOrDefault();
            if(command.GameInfo.PoisonKilled == hunter || command.GameInfo.WolferWillKilled == hunter)
            {
                return false;
            }
            else
                return true;
        }
    }
}
