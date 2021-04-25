using Mirai_CSharp.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrianMiraiBot.Coms.Joke
{
    public class GetJoke : IGame
    {
        /// <summary>
        /// 命令
        /// </summary>
        public static string GameStartCommand = "-joke";
        /// <summary>
        /// 命令
        /// </summary>
        public static string GameDestoryCommand = "-joke -destory";

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        public GetJoke(GameCommand gameCommand, IGroupInfo groupInfo) : base(gameCommand, groupInfo)
        {
            CommandStart = GameStartCommand;
            CmmandDestory = GameDestoryCommand;
            Name = "笑话";
        }

        public override async Task DealCommand(GameInput gameInput)
        {
            if (gameInput.GroupInfo.Id != this.GroupInfo.Id) return;

            if (!gameInput.Command.StartsWith(CommandStart)) return;
            GameCommand gameCommand = null;
            try
            {
                gameCommand = new GameCommand(gameInput.Command, gameInput, GameInfo);
                string count = gameCommand.Commands[0].Contents[0];

                HttpHelper helper = new HttpHelper("https://autumnfish.cn/");
                var json = helper.Get(new Dictionary<string, string> { { "num", count }, }, "/api/joke/list");
                var listJoke = json.ToJObject().GetValue("jokes").ToList();
                string joker = listJoke.ToString();

                await gameInput.ReplyGroup(joker);
                return;
            }
            catch (Exception e)
            {
                await gameInput.ReplyGroup(e.Message);
                return;
            }
        }

        public override void Dispose()
        {
            GameInfo.Dispose();
        }
    }
}
