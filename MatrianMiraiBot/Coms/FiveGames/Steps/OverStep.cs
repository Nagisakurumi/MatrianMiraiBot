using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MatrianMiraiBot.Coms.FiveGames.Steps
{
    public class OverStep : IFiveGameStep
    {
        public async override Task DoAction(GameCommand command)
        {

        }

        public override string GetInitMessage(GameCommand command)
        {
            return "游戏结束";
        }

        public override bool IsEmpty(GameCommand command)
        {
            return false;
        }
    }
}
