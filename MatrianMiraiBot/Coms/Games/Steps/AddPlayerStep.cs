using MatrianMiraiBot.Coms.Games.Enums;
using MatrianMiraiBot.Expends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrianMiraiBot.Coms.Games.Steps
{
    public class AddPlayerStep : IStep
    {

        public AddPlayerStep()
        {
            IsToGroup = true;
            IdentityType = IdentityType.None;
            NextState = GameState.Night;
            SelfState = GameState.AddPlayer;
            StepMessage = "进入参赛阶段!";
        }

        public override async Task DoAction(GameCommand command)
        {
            if(command.GameInfo.BaseInfos.Count >= command.GameInfo.MaxPlayerCount)
            {
                await command.GameInput.ReplyGroup("人数已经满了!");
            }

            var commandItem = command.GetCommandIndex(0);
            if (commandItem.Command.Equals("next"))
            {
                await command.GetGameInfo<GameInfo>().Init(command.GameInput);
                Next(command);
                return;
            }
            var player = command.GameInfo.BaseInfos.Where(p => p.Id == command.GameInput.Sender.Id).FirstOrDefault();
            if (player == null)
            {
                GamePlayerBaseInfo baseInfo = new GamePlayerBaseInfo() { Id = command.GameInput.Sender.Id, Name = command.GameInput.Sender.Name };
                command.GetGameInfo<GameInfo>().AddBaseInfo(baseInfo);
                await command.GameInput.ReplyGroup("玩家{0},成功参与游戏,请耐心等待其他玩家的加入!".Format(baseInfo.Name));
            }
            else
            {
                await command.GameInput.ReplyGroup("不能重复参加!");
            }
            //进入下一个环节
            if(command.GameInfo.BaseInfos.Count == command.GameInfo.MaxPlayerCount)
            {
                await command.GetGameInfo<GameInfo>().Init(command.GameInput);
                Next(command);
                return;
            }
        }

        public override string GetInitMessage(GameCommand command)
        {
            return "输入 (-add) 参加, (-next)开始,目标人数 {0}".Format(command.GameInfo.MaxPlayerCount);
        }

        public override bool IsEmpty(GameCommand command)
        {
            return false;
        }
    }
}
