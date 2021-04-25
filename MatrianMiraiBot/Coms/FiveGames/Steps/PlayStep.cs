﻿using MatrianMiraiBot.Expends;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MatrianMiraiBot.Coms.FiveGames.Steps
{
    public class PlayStep : IFiveGameStep
    {
        /// <summary>
        /// 最后的行
        /// </summary>
        public int LastR { get; set; }
        /// <summary>
        /// 最后的列
        /// </summary>
        public int LastC { get; set; }

        public PlayStep()
        {
            SelfState = FiveGameState.Play;
            NextState = FiveGameState.Over;
        }

        public async override Task DoAction(GameCommand command)
        {
            if(!await CheckPlayer(command))
            {
                return;
            }
            var gameInfo = command.GetGameInfo<GameInfo>();
            var player = gameInfo.GetPlayerById(command.GameInput.Sender.Id);
            var commandItem = command.Commands[0];

            if (player != gameInfo.DoPlayer)
            {
                if (commandItem.Command.Equals("reset"))
                {
                    await gameInfo.ResetLast(LastR, LastC);
                    await command.GameInput.ReplyGroupImg(gameInfo.GetImageStream());
                    return;
                }

                await command.GameInput.ReplyGroup("当前不是你的回合!");
                return;
            }
            if (commandItem.Command.Equals("p"))
            {
                var r = Convert.ToInt32(commandItem.Contents[0]);
                var c = Convert.ToInt32(commandItem.Contents[1]);
                var res = await gameInfo.SetLayout(r, c);
                if (!res)
                {
                    await command.GameInput.ReplyGroup("坐标错误!");
                    return;
                }
                LastR = r;
                LastC = c;
                await command.GameInput.ReplyGroupImg(gameInfo.GetImageStream());
                var vectorer = gameInfo.IsOver();
                if(vectorer != null)
                {
                    await command.GameInput.ReplyGroup("游戏结束 -> 胜利者 : {0}".Format(vectorer.PlayerNickName));
                    Next(command);
                }
            }
            
        }

        public override string GetInitMessage(GameCommand command)
        {
            command.GameInfo.Init(command.GameInput);
            return "请输入 -p x y 来下子, -reset 来悔棋, 当前选手是  : {0}".Format(command.GetGameInfo<GameInfo>().DoPlayer.PlayerNickName);
        }


        public override bool IsEmpty(GameCommand command)
        {
            return false;
        }
    }
}
