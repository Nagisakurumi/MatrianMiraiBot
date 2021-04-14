using MatrianMiraiBot.Coms;
using MatrianMiraiBot.Coms.FiveGames;
using MatrianMiraiBot.Coms.Games;
using MatrianMiraiBot.Expends;
using Mirai_CSharp;
using Mirai_CSharp.Models;
using Mirai_CSharp.Plugin.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrianMiraiBot.Plguins
{
    /// <summary>
    ///狼人杀插件
    /// </summary>
    public class GamePlugin : IGroupMessage, ITempMessage
    {

        public GameStartUp game;
        /// <summary>
        /// 游戏列表
        /// </summary>
        public List<IGame> Games { get; set; } = new List<IGame>();
        /// <summary>
        /// 接受群消息接口
        /// </summary>
        /// <param name="session"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        public async Task<bool> GroupMessage(MiraiHttpSession session, IGroupMessageEventArgs e)
        {

            foreach (var item in e.Chain)
            {
                if(item.Type.Equals("Plain"))
                {
                    string message = item.ToString();
                    if (message.StartsWith("-game"))
                    {
                        GameInput gameInput = new GameInput(session, message, e.Sender, e.Sender.Group);
                        if (message.Equals("-game -create"))
                        {
                            if (game == null)
                            {
                                game = new GameStartUp(e.Sender.Group);
                                await gameInput.ReplyGroup("群{0}成功创建狼人杀游戏!".Format(e.Sender.Group.Name));
                            }
                            else
                                await gameInput.ReplyGroup("该群已经开始游戏了!");
                        }
                        else if (message.Equals(GameStartUp.GameDestoryCommand))
                        {
                            game.Dispose();
                            game = null;
                            await gameInput.ReplyGroup("游戏被关闭!");
                        }
                        else if (game != null && message.StartsWith(GameStartUp.GameStartCommand))
                        {
                            await game.DealCommand(gameInput);
                        }
                    }
                    else if (message.StartsWith(FiveGame.GameStartCommand))
                    {
                        try
                        {
                            GameInput gameInput = new GameInput(session, message, e.Sender, e.Sender.Group);
                            if (message.StartsWith("-five -create"))
                            {
                                var five = Games.Where(p => p.GroupInfo.Id == e.Sender.Id).FirstOrDefault();
                                if (five != null)
                                {
                                    await gameInput.ReplyGroup("不能重复创建!");
                                }
                                else
                                {
                                    message = gameInput.Command.Substring(FiveGame.GameStartCommand.Length);
                                    CommandItem command = GameCommand.ConvertToCommands(message).First();
                                    int row = Convert.ToInt32(command.Contents[0]);
                                    int col = Convert.ToInt32(command.Contents[1]);
                                    Games.Add(new FiveGame(row, col, e.Sender.Group));
                                    await gameInput.ReplyGroup("创建五子棋成功!");
                                }
                            }
                            else if (message.Equals(FiveGame.GameDestoryCommand))
                            {
                                var five = Games.Where(p => p.GroupInfo.Id == gameInput.GroupInfo.Id).FirstOrDefault();
                                five.Dispose();
                                Games.Remove(five);
                                await gameInput.ReplyGroup("游戏被关闭!");
                            }
                            else
                            {
                                var five = Games.Where(p => p.GroupInfo.Id == gameInput.GroupInfo.Id).FirstOrDefault();
                                await five.DealCommand(gameInput);
                            }
                        }
                        catch (Exception)
                        {

                        }
                    }
                }
            }
            return false;
        }

        public async Task<bool> TempMessage(MiraiHttpSession session, ITempMessageEventArgs e)
        {
            foreach (var item in e.Chain)
            {
                if (item.Type.Equals("Plain"))
                {
                    string message = item.ToString();
                    if (game != null && message.StartsWith(game.CommandStart))
                    {
                        await game.DealCommand(new GameInput(session, message, e.Sender, e.Sender.Group));
                    }
                }
            }
            return false;
        }
    }
}
