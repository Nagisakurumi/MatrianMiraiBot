using MatrianMiraiBot.Coms.Games.Enums;
using Mirai_CSharp;
using Mirai_CSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrianMiraiBot.Coms
{
    /// <summary>
    /// 游戏输入命令
    /// </summary>
    public class GameCommand
    {
        /// <summary>
        /// 命令
        /// </summary>
        public List<CommandItem> Commands { get; set; } = new List<CommandItem>();
        /// <summary>
        /// 游戏输入
        /// </summary>
        public GameInput GameInput { get; set; }
        /// <summary>
        /// 游戏玩家信息
        /// </summary>
        public IGameInfo GameInfo { get; set; }
        /// <summary>
        /// 随机
        /// </summary>
        private Random Random { get; set; } = new Random();
        /// <summary>
        /// 游戏当前状态
        /// </summary>
        public IGameState GameState { get => GameInfo.GameState; set => GameInfo.GameState = value; }
        /// <summary>
        /// 是否需要运行一下下一个阶段
        /// </summary>
        public bool IsRunNextState { get; set; } = false;
        /// <summary>
        /// 命令
        /// </summary>
        /// <param name="command"></param>
        /// <param name="Mirai"></param>
        public GameCommand(string command, GameInput mirai, IGameInfo gameInfo)
        {
            this.GameInput = mirai;
            this.GameInfo = gameInfo;

            this.Commands.AddRange(ConvertToCommands(command));

        }
        /// <summary>
        /// 转换为命令
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static List<CommandItem> ConvertToCommands(string command)
        {
            List<CommandItem> commands = new List<CommandItem>();
            List<string> cs = command.Split('-').ToList();
            if (cs.Count == 0) throw new Exception("命令错误!");

            if (cs[0].Equals("") || cs[0].Equals(" ")) cs.RemoveAt(0);

            foreach (var item in cs)
            {
                if (item.Equals("") || item.Equals(" ")) continue;
                commands.Add(new CommandItem(item));
            }
            return commands;
        }

        /// <summary>
        /// 读取指定序号的命令
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public CommandItem GetCommandIndex(int index)
        {
            if (index >= Commands.Count) return null;
            return Commands[index];
        }

        /// <summary>
        /// 等待随机时间
        /// </summary>
        /// <returns></returns>
        public async Task WaitRandomTime()
        {
            await Task.Delay(Random.Next(2, 7) * 1000);
        }

        /// <summary>
        /// 空过
        /// </summary>
        /// <returns></returns>
        /// <param name="state"></param>
        public async Task Empty(IGameState state)
        {
            await WaitRandomTime();
            this.GameState = state;
            this.IsRunNextState = true;
        }
        /// <summary>
        /// 获取指定类型的游戏信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetGameInfo<T>() where T : IGameInfo
        {
            return (T)this.GameInfo;
        }
        /// <summary>
        /// 获取指定类型的游戏状态
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetGameState<T>() where T : IGameState
        {
            return (T)this.GameState;
        }
    }
}
