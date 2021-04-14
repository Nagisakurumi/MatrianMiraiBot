using MatrianMiraiBot.Coms.Games;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MatrianMiraiBot.Coms
{
    /// <summary>
    /// 游戏步骤
    /// </summary>
    public abstract class IGameStep
    {
        /// <summary>
        /// 自身状态
        /// </summary>
        public IGameState SelfState { get; protected set; }
        /// <summary>
        /// 下一个状态
        /// </summary>
        public IGameState NextState { get; protected set; }
        /// <summary>
        /// 是否针对群 发送初始化消息
        /// </summary>
        public bool IsToGroup { get; protected set; } = true;
        /// <summary>
        /// 步骤消息
        /// </summary>
        public string StepMessage { get; protected set; }
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
        public abstract Task Init(GameCommand command);

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

    }
}
