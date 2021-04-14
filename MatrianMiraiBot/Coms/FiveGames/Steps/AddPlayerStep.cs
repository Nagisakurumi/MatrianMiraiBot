using MatrianMiraiBot.Expends;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MatrianMiraiBot.Coms.FiveGames.Steps
{
    public class AddPlayerStep : IFiveGameStep
    {
        public AddPlayerStep()
        {
            SelfState = FiveGameState.AddPlayer;
            NextState = FiveGameState.Play;
        }


        public async override Task DoAction(GameCommand command)
        {
            //if (!await CheckPlayer(command)) return;
            if(command.GameInfo.IsFullPlayer)
            {
                Next(command);
                await command.GameInput.ReplyGroup("开始");
            }
            var player = command.GameInfo.GetPlayerById(command.GameInput.Sender.Id);
            var commandItem = command.Commands[0];
            if(player != null)
            {
                await command.GameInput.ReplyGroup("不能重复添加");
            }
            else if(commandItem.Command.Equals("add"))
            {
                command.GameInfo.AddBaseInfo(new GamePlayerBaseInfo() { Id = command.GameInput.Sender.Id, 
                    Name = command.GameInput.Sender.Name});
                await command.GameInput.ReplyGroup("{0}成功参加!".Format(command.GameInput.Sender.Name));
            }
            else
            {
                return;
            }
            if (command.GameInfo.IsFullPlayer)
            {
                Next(command);
                await command.GameInput.ReplyGroup("开始");
            }
        }

        public override string GetInitMessage(GameCommand command)
        {
            return "进入参赛阶段!(-add 参赛)";
        }



        public override bool IsEmpty(GameCommand command)
        {
            return false;
        }
    }
}
