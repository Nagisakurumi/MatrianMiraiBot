using MatrianMiraiBot.Coms.Games.Enums;
using MatrianMiraiBot.Expends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrianMiraiBot.Coms.Games.Steps
{
    public class AddPlayerStep : IStep
    {

        public AddPlayerStep()
        {
            IsToGroup = false;
            IdentityType = IdentityType.None;
            NextState = GameState.Night;
            SelfState = GameState.AddPlayer;
            StepMessage = "进入参赛阶段!";
        }

        public override async Task DoAction(GameCommand command)
        {
            if(command.GameInfo.BaseInfos.Count >= GameInfo.MaxPlayerCount)
            {
                await command.GameInput.ReplyGroup("人数已经满了!");
            }

            var commandItem = command.GetCommandIndex(0);
            if (commandItem.Command.Equals("next"))
            {
                command.GameInfo.InitIdentity();
                Next(command);
                return;
            }
            var player = command.GameInfo.BaseInfos.Where(p => p.Id == command.GameInput.Sender.Id).FirstOrDefault();
            if (player == null)
            {
                command.GameInfo.AddBaseInfo(new GamePlayerBaseInfo() { Id = command.GameInput.Sender.Id, Name = command.GameInput.Sender.Name } );
            }
            else
            {
                await command.GameInput.ReplyGroup("不能重复参加!");
            }
        }

        public override string GetInitMessage(GameCommand command)
        {
            return "输入 (-add) 参加, (-next)开始,目标人数 {0}".Format(GameInfo.MaxPlayerCount);
        }

        public override bool IsEmpty(GameCommand command)
        {
            return false;
        }
    }
}
