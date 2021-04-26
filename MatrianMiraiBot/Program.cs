using System;
using System.Threading.Tasks;
using MatrianMiraiBot.Mock;
using MatrianMiraiBot.Plguins;
using Mirai_CSharp;
using Mirai_CSharp.Models;

namespace MatrianMiraiBot
{
    class Program
    {
        static void Main(string [] argv)
        {
            Task task = StartUp.Main();
            task.Wait();

            //MockStartUp mockStart = new MockStartUp();
            //Task task = mockStart.MockStart();
            //task.Wait();


            //var info = new Coms.FiveGames.GameInfo(10, 10);
            //info.AddBaseInfo(new Coms.GamePlayerBaseInfo() { Id = 32322, Name = "t12est" });
            //info.AddBaseInfo(new Coms.GamePlayerBaseInfo() { Id = 112121, Name = "test" });
            //info.Init(null).Wait();
            //info.SetLayout(5, 1).Wait();
            ////info.SetLayout(2, 1).Wait();
            ////info.SetLayout(3, 1).Wait();
            ////info.SetLayout(2, 4).Wait();
            //info.IsOver();
            //info.LayoutMap.Save("test.jpg");

        }
    }
}
