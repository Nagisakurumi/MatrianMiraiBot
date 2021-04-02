using MatrianMiraiBot.Coms.Games.Enums;
using MatrianMiraiBot.Expends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrianMiraiBot.Coms.Games.Steps
{
    public abstract class IStep
    {
        /// <summary>
        /// 自身状态
        /// </summary>
        public GameState SelfState { get; protected set; }
        /// <summary>
        /// 下一个状态
        /// </summary>
        public GameState NextState { get; protected set; }
        /// <summary>
        /// 是否针对群 发送初始化消息
        /// </summary>
        public bool IsToGroup { get; protected set; } = true;
        /// <summary>
        /// 步骤消息
        /// </summary>
        public string StepMessage { get; protected set; }
        /// <summary>
        /// 操作当前步骤的玩家的身份
        /// </summary>
        public IdentityType IdentityType { get; set; }
        /// <summary>
        /// 执行动作
        /// </summary>
        /// <param name="command"></param>
        public abstract Task DoAction(GameCommand command);
        /// <summary>
        /// 获取初始化消息
        /// </summary>
        /// <returns></returns>
        public abstract string GetInitMessage(GameCommand command);
        /// <summary>
        /// 阶段初始化
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public virtual async Task Init(GameCommand command)
        {
            var players = command.GameInfo.GetPlayerByIdentity(IdentityType);
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
        /// 是否需要空过
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public abstract bool IsEmpty(GameCommand command);

        /// <summary>
        /// 进入下一个阶段
        /// </summary>
        public virtual void Next(GameCommand command)
        {
            command.GameState = NextState;
            command.IsRunNextState = true;
        }
        /// <summary>
        /// 检测身份是否对上
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public bool CheckIdentity(GameCommand command)
        {
            IPlayer player = command.GameInfo.GetPlayerById(command.GameInput.Sender.Id);
            if (player == null) return false;
            return command.GameInfo.GetPlayerByIdentity(IdentityType).Contains(player);
        }
    }
}
