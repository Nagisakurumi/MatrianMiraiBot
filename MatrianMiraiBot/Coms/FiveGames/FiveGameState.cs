using System;
using System.Collections.Generic;
using System.Text;

namespace MatrianMiraiBot.Coms.FiveGames
{
    public class FiveGameState : IGameState
    {
        private FiveGameState(int value) : base(value) { }


        public static FiveGameState AddPlayer = new FiveGameState(1);
        public static FiveGameState Play = new FiveGameState(2);
        public static FiveGameState Over = new FiveGameState(3);
        public static FiveGameState Init = new FiveGameState(4);
    }
}
