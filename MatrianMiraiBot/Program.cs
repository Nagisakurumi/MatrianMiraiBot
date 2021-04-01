using System;
using System.Threading.Tasks;
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
        }
    }
}
