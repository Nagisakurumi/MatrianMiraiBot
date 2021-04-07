using Mirai_CSharp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MatrianMiraiBot.Mock
{
    public class MockGroupMessageEventArgs : IGroupMessageEventArgs
    {
        public IGroupMemberInfo Sender { get; set; }

        public IMessageBase[] Chain { get; set; } = new IMessageBase[1];


        public MockGroupMessageEventArgs(string message, long id, string name) {
            Chain[0] = new PlainMessage(message);
            Sender = new MockGroupMemberInfo() { Group = MockGroupInfo.TestMockGroupInfo, Name = name, Id = id };
        }
    }
}
