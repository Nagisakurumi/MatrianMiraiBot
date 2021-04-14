using MatrianMiraiBot.Coms.Games.Enums;
using MatrianMiraiBot.Expends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrianMiraiBot.Coms.Games.Steps
{
    public abstract class IStep : IGameStep
    {
        /// <summary>
        /// 操作当前步骤的玩家的身份
        /// </summary>
        public IdentityType IdentityType { get; set; }
        /// <summary>
        /// 阶段初始化
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public override async Task Init(GameCommand command)
        {
            var players = command.GetGameInfo<GameInfo>().GetPlayerByIdentity(IdentityType);
            await command.GameInput.ReplyGroup(StepMessage);
            //针对组
            if (IsToGroup)
            {
                var msg = GetInitMessage(command);
                if (msg != null)
                    await command.GameInput.ReplyGroup(GetInitMessage(command));
            }
            else
            {
                //await command.GameInput.ReplyTemp(GetInitMessage());
                string content = GetInitMessage(command);
                foreach (var item in players)
                {
                    await command.GameInput.SendTemp(content, item.PlayerId);
                }
            }

            if ((IdentityType != IdentityType.None && players.Count() == 0) || IsEmpty(command))
            {
                await command.Empty(NextState);
            }
            else
            {
                command.IsRunNextState = false;
            }
        }
        /// <summary>
        /// 检测身份是否对上
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public bool CheckIdentity(GameCommand command)
        {
            IPlayer player = command.GetGameInfo<GameInfo>().GetPlayerById(command.GameInput.Sender.Id);
            if (player == null) return false;
            return command.GetGameInfo<GameInfo>().GetPlayerByIdentity(IdentityType).Contains(player);
        }
    }
}
