using MatrianMiraiBot.Coms.Games.Enums;
using MatrianMiraiBot.Expends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrianMiraiBot.Coms.Games.Steps
{
    public class SheriffMoveStep : IStep
    {

        public SheriffMoveStep()
        {
            IdentityType = IdentityType.None;
            NextState = GameState.Night;
            SelfState = GameState.SheriffMoveStep;
            StepMessage = "进入警徽移交阶段!";
        }


        public override async Task DoAction(GameCommand command)
        {
            var commandItem = command.GetCommandIndex(0);
            if (commandItem.Command.Equals("move"))
            {
                var index = Convert.ToInt32(commandItem.Contents.First());
                IPlayer target = command.GameInfo.GetPlayer(index);
                if(target == null && command.GameInfo.GetAllKilledPlayer().Contains(target))
                {
                    await command.GameInput.ReplyGroup("请输入正确的移交对象!");
                    return;
                }
                else
                {
                    target.IsSheriff = true;
                    Next(command);
                }
            }
        }

        public override string GetInitMessage(GameCommand command)
        {
            return "请选择警徽的移交对象! 输入 (-move {序号}) : \n " + command.GameInfo.BuildCanKillList().ToIndexMessage();
        }

        public override bool IsEmpty(GameCommand command)
        {
            return false;
        }
    }
}
