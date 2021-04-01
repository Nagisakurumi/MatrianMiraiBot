using MatrianMiraiBot.Coms.Games.Enums;
using Mirai_CSharp;
using Mirai_CSharp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrianMiraiBot.Coms.Games
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
        public GameInfo GameInfo { get; set; }
        /// <summary>
        /// 随机
        /// </summary>
        private Random Random { get; set; } = new Random();
        /// <summary>
        /// 游戏当前状态
        /// </summary>
        public GameState GameState { get; set; }
        /// <summary>
        /// 是否需要运行一下下一个阶段
        /// </summary>
        public bool IsRunNextState { get; set; } = false;
        /// <summary>
        /// 命令
        /// </summary>
        /// <param name="command"></param>
        /// <param name="Mirai"></param>
        public GameCommand(string command, GameInput mirai, GameInfo gameInfo)
        {
            this.GameInput = mirai;
            this.GameInfo = gameInfo;

            List<string> cs = command.Split('-').ToList();
            if (cs.Count == 0) throw new Exception("命令错误!");

            if (cs[0].Equals("")) cs.RemoveAt(0);

            foreach (var item in cs)
            {
                if (item.Equals("")) continue;
                Commands.Add(new CommandItem(item));
            }

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
            await Task.Delay(Random.Next(2, 7));
        }

        /// <summary>
        /// 空过
        /// </summary>
        /// <returns></returns>
        /// <param name="state"></param>
        public async Task Empty(GameState state)
        {
            await WaitRandomTime();
            this.GameState = state;
            this.IsRunNextState = true;
        }
    }
}
