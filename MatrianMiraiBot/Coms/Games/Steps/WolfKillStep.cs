﻿using MatrianMiraiBot.Coms.Games.Enums;
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
            IsToGroup = false;
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
            var wolfers = command.GameInfo.GetWolfers(true);
           
            var commandItem = command.GetCommandIndex(0);
            if (commandItem == null || !commandItem.Command.Equals("kill"))
            {
                await command.GameInput.ReplyTemp("命令错误!");
                return;
            }

            try
            {
                IPlayer self = command.GameInfo.GetPlayerById(command.GameInput.Sender.Id);
                if(self.Identity != IdentityType.Wolfer)
                {
                    await command.GameInput.ReplyTemp("只有狼人才能操作!");
                    return;
                }
                int index = Convert.ToInt32(commandItem.Contents.FirstOrDefault());
                IPlayer player = command.GameInfo.CanKilledList[index];
                (self as Wolfer).KillTargetPlayer = player;
            }
            catch (Exception)
            {
                await command.GameInput.ReplyTemp("命令错误!");
                return;
            }

            var list = new List<IPlayer>();
            wolfers.ForEach(p => {
                if ((p as Wolfer).KillTargetPlayer != null)
                    list.Add((p as Wolfer).KillTargetPlayer);
            }); 
            ///如果狼人操作完成
            if (list.Count() == wolfers.Count)
            {
                command.GameState = GameState.ProphetStep;
                command.GameInfo.WolferWillKilled = (from item in list group item by item into gro orderby gro.Count() descending select gro.Key).FirstOrDefault();
                wolfers.ForEach(p => (p as Wolfer).Reset());
                command.IsRunNextState = true;
            }
            
        }

        public override string GetInitMessage(GameCommand command)
        {
            var killlist = command.GameInfo.BuildCanKillList();
            return "进入狼人杀人阶段, 狼人请把要杀害的玩家信息通过临时会话发送给我! (-kill {序号}) : \n请输入序号选择要杀害的玩家:\n" + killlist.ToIndexMessage();
        }
    }
}
