using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MatrianMiraiBot.Coms.FiveGames.Steps
{
    public class InitStep : IFiveGameStep
    {

        public InitStep()
        {
            SelfState = FiveGameState.Init;
            NextState = FiveGameState.AddPlayer;
        }

        public async override Task DoAction(GameCommand command)
        {
            var commandItem = command.Commands[0];
            if (commandItem.Command.Equals("start"))
            {
                await command.GameInput.ReplyGroupImg(command.GetGameInfo<GameInfo>().GetImageStream());
                Next(command);
            }
        }

        public override string GetInitMessage(GameCommand command)
        {
            return "";
        }

        public override bool IsEmpty(GameCommand command)
        {
            return false;
        }
    }
}
