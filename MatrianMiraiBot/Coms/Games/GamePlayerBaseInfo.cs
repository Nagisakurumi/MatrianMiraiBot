using System;
using System.Collections.Generic;
using System.Text;

namespace MatrianMiraiBot.Coms.Games
{
    public class GamePlayerBaseInfo
    {

        public long Id;


        public string Name;

        public GamePlayerBaseInfo() { }

        public GamePlayerBaseInfo(long id, string name) { Id = id; Name = name; }




    }
}
