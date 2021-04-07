using MatrianMiraiBot.Coms.Games.Enums;
using MatrianMiraiBot.Expends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrianMiraiBot.Coms.Games.Steps
{
    public class SheriffVotedStep : IStep
    {
        public SheriffVotedStep()
        {
            IsToGroup = true;
            IdentityType = IdentityType.None;
            NextState = GameState.TalkAboutStep;
            SelfState = GameState.SheriffVotedStep;
            StepMessage = "进入竞选警长投票阶段!";
        }


        public override async Task DoAction(GameCommand command)
        {
            var commandItem = command.GetCommandIndex(0);
            if (commandItem.Command.Equals("next"))
            {
                var list = new List<IPlayer>();
                command.GameInfo.CanKilledList.ForEach(p => {
                    if (p.VotedPlayer != null)
                        list.Add(p.VotedPlayer); });
                //获取票数最多的
                var target = (from item in list group item by item into gro orderby gro.Count() descending select new { Player = gro.Key,Count = gro.Count()}).FirstOrDefault();
                if(target == null)
                {
                    await command.GameInput.ReplyGroup("竞选失败请认真投票!");
                    return;
                }
                target.Player.IsSheriff = true;
                await command.GameInput.ReplyGroup("恭喜玩家 {0}, 被选为警长, 荣获票数 : {1} ".Format(target.Player.PlayerNickName, target.Count));
                Next(command);
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
                await command.GameInput.ReplyGroup("玩家 : {0} -> {1}".Format(self.PlayerNickName, player == null ? "" : player.PlayerNickName));
            }
        }

        public override string GetInitMessage(GameCommand command)
        {
            command.GameInfo.ResetVoted();
            return "输入命令 (-next) 进入下一个阶段, 输入命令 (-vote {序号}) 进行投票 : \n" + command.GameInfo.BuildCanKillList().ToIndexMessage();
        }

        public override bool IsEmpty(GameCommand command)
        {
            return false;
        }
    }
}
