using MatrianMiraiBot.Coms.Games.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using MatrianMiraiBot.Expends;

namespace MatrianMiraiBot.Coms.Games.Steps
{
    public class VotedStep : IStep
    {

        public VotedStep()
        {
            IdentityType = IdentityType.None;
            NextState = GameState.Night;
            SelfState = GameState.VotedStep;
            StepMessage = "进入投票阶段!";
        }


        public override async Task DoAction(GameCommand command)
        {
            var commandItem = command.GetCommandIndex(0);
            if (commandItem.Command.Equals("next"))
            {
                //获取投票结果
                var target = command.GameInfo.GetMaxVotedPlayer();
                if(target.Key != null)
                {
                    await command.GameInput.ReplyGroup("玩家{0}被白嫖死了!".Format(target.Key.PlayerNickName));
                    target.Key.IsAlive = false;
                }



                if (command.GameInfo.GetAllKilledPlayer().Where(p => p.IsSheriff).Count() > 0)
                {
                    command.GameState = GameState.SheriffMoveStep;
                    command.IsRunNextState = true;
                }
                else Next(command);
            }
            else if (commandItem.Command.Equals("vote"))
            {
                var index = Convert.ToInt32(commandItem.Contents.First());
                IPlayer player = command.GameInfo.GetPlayer(index);
                IPlayer self = command.GameInfo.GetPlayerById(command.GameInput.Sender.Id);
                if (self == null)
                {
                    await command.GameInput.ReplyTemp("您没有参与游戏，或则您已经出局!");
                    return;
                }
                self.VotedPlayer = player;
            }
        }

        public override string GetInitMessage(GameCommand command)
        {
            command.GameInfo.ResetVoted();
            return "开始进行投票 输入 (-next) 进入下一个阶段, (-vote {序号}) 进行投票.\n" + command.GameInfo.CanKilledList.ToIndexMessage();
        }

        public override bool IsEmpty(GameCommand command)
        {
            return false;
        }
    }
}
