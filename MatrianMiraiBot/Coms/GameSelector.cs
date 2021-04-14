using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatrianMiraiBot.Expends;

namespace MatrianMiraiBot.Coms
{
    /// <summary>
    /// 游戏选择器
    /// </summary>
    public class GameSelector
    {
        /// <summary>
        /// 游戏集合
        /// </summary>
        public List<GameBaseCommand> GameBases { get; set; } = new List<Coms.GameBaseCommand>() {
            new GameBaseCommand("-game", typeof(Coms.Games.GameStartUp)),new GameBaseCommand("-five", typeof(FiveGames.FiveGame))
        };
        /// <summary>
        /// 运行中的游戏
        /// </summary>
        public List<IGame> RunningGames { get; set; } = new List<IGame>();
        /// <summary>
        /// 游戏选择器
        /// </summary>
        public GameSelector()
        {

        }


        /// <summary>
        /// 处理命令
        /// </summary>
        /// <param name="gameInput"></param>
        public async Task DealCommand(GameInput gameInput)
        {
            var game = RunningGames.Where(p => p.GroupInfo.Id == gameInput.GroupInfo.Id);

            var command = gameInput.Command;
            var group = gameInput.GroupInfo;
            //如果是执行的话
            foreach (var item in game)
            {
                //如果匹配
                if (command.StartsWith(item.CommandStart))
                {
                    if (command.Equals(item.CmmandDestory))
                    {
                        await gameInput.ReplyGroup("群{0}, 停止游戏{1}".Format(item.GroupInfo.Name, item.Name));
                        item.Dispose();
                        RunningGames.Remove(item);
                        return;
                    }
                    else
                    {
                        await item.DealCommand(gameInput);
                        return;
                    }
                }
            }
            //匹配创建
            var gameBase = GameBases.Where(p => command.StartsWith(p.CreateCommand)).FirstOrDefault();
            if(gameBase != null)
            {
                var exist = game.Where(p => command.Equals(p.CommandStart)).FirstOrDefault();
                if (exist != null)
                {
                    await gameInput.ReplyGroup("群{0},已经创建了游戏{1}".Format(group.Name, exist.Name));
                    return;
                }
                else
                {
                    IGame createGame = gameBase.Create(new GameCommand(command, gameInput, null), group);
                    RunningGames.Add(createGame);
                    await gameInput.ReplyGroup("群{0},创建了游戏{1}".Format(group.Name, createGame.Name));
                    return;
                }
            }
        }
    }
}
