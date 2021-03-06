using MatrianMiraiBot.Coms.Games.Enums;
using MatrianMiraiBot.Coms.Games.Players;
using MatrianMiraiBot.Expends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrianMiraiBot.Coms.Games.Steps
{
    public class WolfKillStep : IStep
    {
        public WolfKillStep()
        {
            IsToGroup = true;
            IdentityType = IdentityType.Wolfer;
            NextState = GameState.ProphetStep;
            SelfState = GameState.WolfKillStep;
            StepMessage = "进入狼人环节!";
        }
        /// <summary>
        /// 狼人杀人阶段
        /// </summary>
        /// <param name="command"></param>
        public override async Task DoAction(GameCommand command)
        {
            var wolfers = command.GetGameInfo<GameInfo>().GetWolfers(true);
           
            var commandItem = command.GetCommandIndex(0);
            
            IPlayer self = command.GetGameInfo<GameInfo>().GetPlayerById(command.GameInput.Sender.Id);
            if(!CheckIdentity(command))
            {
                await command.GameInput.ReplyTemp("只有狼人才能操作!");
                return;
            }
            if (commandItem.Command.Equals("kill"))
            {
                int index = Convert.ToInt32(commandItem.Contents.FirstOrDefault());
                IPlayer player = command.GetGameInfo<GameInfo>().GetPlayer(index);
                (self as Wolfer).SetTarget(player);
                if(player != null)
                    await command.GameInput.ReplyTemp("您投票 -> {0} 玩家成功!".Format(player.PlayerNickName));
            }
            if (commandItem.Command.Equals("next") || wolfers.Where(p => (p as Wolfer).IsOptioned == false).Count() == 0)
            {
                var list = new List<IPlayer>();
                wolfers.ForEach(p =>
                {
                    if ((p as Wolfer).KillTargetPlayer != null)
                        list.Add((p as Wolfer).KillTargetPlayer);
                });
                ///如果狼人操作完成
                command.GetGameInfo<GameInfo>().WolferWillKilled = (from item in list group item by item into gro orderby gro.Count() descending select gro.Key).FirstOrDefault();
                wolfers.ForEach(p => (p as Wolfer).Reset());
                Next(command);
            }
        }

        public override string GetInitMessage(GameCommand command)
        {
            var killlist = command.GetGameInfo<GameInfo>().BuildCanKillList();
            return "进入狼人杀人阶段 输入(-next 进入下一个阶段), 狼人请把要杀害的玩家信息通过临时会话发送给我! (-kill {序号}) : \n请输入序号选择要杀害的玩家:\n" + killlist.ToIndexMessage();
        }

        public override bool IsEmpty(GameCommand command)
        {
            return false;
        }
    }
}
