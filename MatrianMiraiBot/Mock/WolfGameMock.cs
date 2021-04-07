using Mirai_CSharp;
using Mirai_CSharp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MatrianMiraiBot.Expends;

namespace MatrianMiraiBot.Mock
{
    /// <summary>
    /// 模拟输入输出
    /// </summary>
    public class WolfGameMock : MiraiHttpSession
    {




        public new Task<int> SendGroupMessageAsync(long groupNumber, params IMessageBase[] chain)
        {
            return Task.Factory.StartNew<int>(msg => {
                IMessageBase data = msg as IMessageBase;
                Console.WriteLine("Group -> " + data.ToString());
                return 1;
            }, chain[0]);
        }

        public new Task<int> SendTempMessageAsync(long qqNumber, long groupNumber, params IMessageBase[] chain)
        {
            return Task.Factory.StartNew<int>(msg => {
                object[] data = msg as object[];
                Console.WriteLine("Private -> [Target : {0}] : {1}".Format(data[0].ToString() , data[1] == null ? "" : data[1].ToString()));
                return 1;
            }, new object[] { qqNumber, chain[0].ToString() });
        }
    }
}
