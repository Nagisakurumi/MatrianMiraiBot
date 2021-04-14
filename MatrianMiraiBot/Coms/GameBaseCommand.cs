using Mirai_CSharp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MatrianMiraiBot.Coms
{
    /// <summary>
    /// 游戏启停命令
    /// </summary>
    public class GameBaseCommand
    {

        public string CommandStart { get; set; }

        public string CreateCommand { get; set; }

        public string DestoryCmmand { get; set; }

        public Type Type { get; set; }

        public GameBaseCommand(string start, Type type, string create = "-create", string destory = "-destory")
        {
            this.CommandStart = start;
            this.CreateCommand = start + " " + create;
            this.DestoryCmmand = start + " " + destory;
            this.Type = type;
        }
        /// <summary>
        /// 构建
        /// </summary>
        /// <param name="groupInfo"></param>
        /// <returns></returns>
        public IGame Create(GameCommand gameCommand, IGroupInfo groupInfo)
        {
            return (IGame)Type.GetConstructor(new Type[] { typeof(GameCommand), typeof(IGroupInfo) }).Invoke(new object[] { gameCommand, groupInfo });
        }
    }
}
