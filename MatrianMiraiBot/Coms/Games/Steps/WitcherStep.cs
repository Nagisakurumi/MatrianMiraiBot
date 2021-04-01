using MatrianMiraiBot.Coms.Games.Enums;
using MatrianMiraiBot.Coms.Games.Players;
using MatrianMiraiBot.Expends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrianMiraiBot.Coms.Games.Steps
{
    public class WitcherStep : IStep
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        public WitcherStep()
        {
            IsToGroup = false;
            IdentityType = IdentityType.Witcher;
            NextState = GameState.HunterStep;
            SelfState = GameState.WitchStep;
            StepMessage = "进入女巫环节!";

        }


        public override async Task DoAction(GameCommand command)
        {
            var witcher = command.GameInfo.GetPlayerByIdentity(IdentityType.Witcher).FirstOrDefault() as Witcher;
            if(witcher != null && witcher.PlayerId != command.GameInput.Sender.Id)
            {
                await command.GameInput.ReplyTemp("只有女巫才能操作!");
                return;
            }
            try
            {
                if(command.Commands.Where(p => p.Command.Equals("empty")).Count() > 0)
                {
                    command.GameState = NextState;
                    command.IsRunNextState = true;
                    return;
                }

                foreach (var item in command.Commands)
                {
                    //救人命令
                    if (item.Command.Equals("save") && witcher.Antidote > 0)
                    {
                        witcher.Antidote--;
                        command.GameInfo.WolferWillKilled = null;
                        //await command.GameInput.ReplyTemp("成功救下!");
                    }
                    else if (item.Command.Equals("poison") && witcher.Poison > 0)
                    {
                        try
                        {
                            int index = Convert.ToInt32(item.Contents.First());
                            //获取期望毒杀的目标
                            var player = command.GameInfo.CanKilledList[index];
                            command.GameInfo.PoisonKilled = player;
                            witcher.Poison--;
                        }
                        catch(Exception)
                        {
                            //await command.GameInput.ReplyTemp("毒杀命令错误!");
                        }
                    }
                }

                command.GameState = NextState;
                command.IsRunNextState = true;
            }
            catch (Exception)
            {
                await command.GameInput.ReplyTemp("命令异常");
            }
        }

        public override string GetInitMessage(GameCommand command)
        {
            var player = command.GameInfo.GetPlayerByIdentity(IdentityType.Witcher);
            if (player == null)
                return null;
            var witcher = player as Witcher;
            string content = "";
            var killed = command.GameInfo.WolferWillKilled;
            if(killed != null)
            {
                content += "今天晚上被杀害的玩家是{0},".Format(killed.PlayerNickName);
                if(witcher.Antidote > 0)
                {
                    content += "是否使用解药 (-save {序号}),";
                }
            }
            if (witcher.Poison > 0)
            {
                var list = command.GameInfo.BuildCanKillList();
                content += "是否使用毒药 (-poison {序号})\n" + list.ToIndexMessage();
            }
            content += "\n 使用命令 (-empty) 表示空过不操作!";
            return content;
        }
    }
}
