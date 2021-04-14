using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrianMiraiBot.Coms.FiveGames
{
    public abstract class IFiveGameStep : IGameStep
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async override Task Init(GameCommand command)
        {
            if(StepMessage != null)
                await command.GameInput.ReplyGroup(StepMessage);
            //针对组
            var msg = GetInitMessage(command);
            if (msg != null)
                await command.GameInput.ReplyGroup(GetInitMessage(command));

            if (IsEmpty(command))
            {
                await command.Empty(NextState);
            }
            else
            {
                command.IsRunNextState = false;
            }
        }

        /// <summary>
        /// 检测是否是参赛玩家
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<bool> CheckPlayer(GameCommand command)
        {
            var player = command.GameInput.Sender.Id;
            if (command.GameInfo.GamePlayers.Where(p => p.PlayerId == player).Count() == 0)
                return false;
            return true;
        }
    }
}
