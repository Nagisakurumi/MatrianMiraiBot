using MatrianMiraiBot.Coms.Games.Enums;
using MatrianMiraiBot.Expends;
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

        public override async Task DoAction(GameCommand command)
        {
            var hunter = command.GameInfo.GetPlayerByIdentity(IdentityType.Hunter).FirstOrDefault();

            var commandItem = command.GetCommandIndex(0);
            if (commandItem.Command.Equals("gun"))
            {
                int index = Convert.ToInt32(commandItem.Contents.FirstOrDefault());
                var player = command.GameInfo.CanKilledList[index];
                command.GameInfo.GunKilled = player;
                command.IsRunNextState = true;
                command.GameState = NextState;
            }
            else
            {
                await command.GameInput.ReplyTemp("命令错误!");
            }
        }

        public override string GetInitMessage(GameCommand command)
        {
            if (!IsEmpty(command))
            {
                return "您已经被杀害，是否使用技能。请选择目标(-gun {序号}) : \n" + command.GameInfo.BuildCanKillList().ToIndexMessage();
            }
            return null;
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
