using MatrianMiraiBot.Coms.Games.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MatrianMiraiBot.Coms.Games.Steps
{
    public class NightStep : IStep
    {

        public NightStep()
        {
            IdentityType = IdentityType.None;
            NextState = GameState.WolfKillStep;
            SelfState = GameState.Night;
            StepMessage = "天黑请闭眼!";
        }

        /// <summary>
        /// 夜晚阶段执行
        /// </summary>
        /// <param name="command"></param>
        public override async Task DoAction(GameCommand command)
        {
            var commandItem = command.GetCommandIndex(0);
            if (commandItem.Command.Equals("next"))
            {
                command.GameInfo.GetAllKilledPlayer().ForEach(p => p.IsAlive = false);
                command.GameInfo.InitKilleds();
                command.GameInfo.Date++;

                Next(command);
            }
        }

        public override string GetInitMessage(GameCommand command)
        {
            return "输入 (-next) 进入下一步";
        }

        public override bool IsEmpty(GameCommand command)
        {
            return false;
        }
    }
}
