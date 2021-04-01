using MatrianMiraiBot.Expends;
using Mirai_CSharp;
using Mirai_CSharp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MatrianMiraiBot.Coms.Games
{
    /// <summary>
    /// 游戏输入内容
    /// </summary>
    public class GameInput
    {
        /// <summary>
        /// mirai控制台
        /// </summary>
        public MiraiHttpSession Mirai { get; private set; }
        /// <summary>
        /// 命令内容
        /// </summary>
        public string Command { get; private set; }
        /// <summary>
        /// 发送者
        /// </summary>
        public GroupMemberInfo Sender { get; private set; }

        /// <summary>
        /// 组信息
        /// </summary>
        public GroupInfo GroupInfo { get; private set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="mirai"></param>
        /// <param name="command"></param>
        /// <param name="info"></param>
        /// <param name="groupInfo"></param>
        public GameInput(MiraiHttpSession mirai, string command, GroupMemberInfo info, GroupInfo groupInfo)
        {
            this.Mirai = mirai;
            this.Command = command;
            this.GroupInfo = groupInfo;
            this.Sender = info;
        }
        /// <summary>
        /// 回复组消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task<int> ReplyGroup(string message)
        {
            if (GroupInfo != null)
                return Mirai.SendGroupMessageAsync(GroupInfo.Id, message.ToMessage());
            else
                return null;
        }

        /// <summary>
        /// 回复个人消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task<int> Reply(string message)
        {
            if (Sender != null)
                return Mirai.SendFriendMessageAsync(GroupInfo.Id, message.ToMessage());
            else
                return null;
        }

        /// <summary>
        /// 回复个人消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task<int> ReplyTemp(string message)
        {
            if (Sender != null && GroupInfo != null)
                return Mirai.SendTempMessageAsync(Sender.Id, GroupInfo.Id, message.ToMessage());
            else
                return null;
        }
        /// <summary>
        /// 发送临时消息
        /// </summary>
        /// <param name="message"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<int> SendTemp(string message, long id)
        {
            return Mirai.SendTempMessageAsync(id, GroupInfo.Id, message.ToMessage());
        }
    }
}
