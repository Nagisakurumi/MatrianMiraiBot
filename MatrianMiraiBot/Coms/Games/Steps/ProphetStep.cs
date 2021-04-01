using MatrianMiraiBot.Coms.Games.Enums;
using MatrianMiraiBot.Expends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrianMiraiBot.Coms.Games.Steps
{
    public class ProphetStep : IStep
    {

        /// <summary>
        /// 构造函数
        /// </summary>
        public ProphetStep()
        {
            IsToGroup = false;
            IdentityType = IdentityType.Propheter;
            NextState = GameState.WitchStep;
            SelfState = GameState.ProphetStep;
            StepMessage = "进入预言家环节!";

        }

        public override async Task DoAction(GameCommand command)
        {
            var prophet = command.GameInfo.GetPlayerByIdentity(IdentityType.Propheter).FirstOrDefault();
            
            if(prophet.PlayerId != command.GameInput.Sender.Id)
            {
                await command.GameInput.ReplyTemp("只有预言家才可以操作!");
                return;
            }
            var commandItem = command.GetCommandIndex(0);
            try
            {
                if (!commandItem.Command.Equals("check")) throw new Exception();
                int index = Convert.ToInt32(commandItem.Contents.First());
                IPlayer player = command.GameInfo.CanKilledList[index];
                await command.GameInput.ReplyTemp("这个人的身份是 : {0}".Format(player.Identity));
                command.GameState = GameState.WitchStep;
                command.IsRunNextState = true;
            }
            catch (Exception)
            {
                await command.GameInput.ReplyTemp("命令错误!");
            }
            
        }

        public override string GetInitMessage(GameCommand command)
        {
            var list = command.GameInfo.BuildCanKillList();
            string content = "请输入要查看的玩家的身份序号(-check {序号}) : \n" + list.ToIndexMessage();
            return content;
        }
    }
}
