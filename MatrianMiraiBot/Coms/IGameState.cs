using System;
using System.Collections.Generic;
using System.Text;

namespace MatrianMiraiBot.Coms
{
    public abstract class IGameState
    {
        public int Value { get; set; }

        protected IGameState(int value) { this.Value = value; }
    }
}
