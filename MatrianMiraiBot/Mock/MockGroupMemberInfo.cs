using Mirai_CSharp.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MatrianMiraiBot.Mock
{

    public class MockGroupInfo : IGroupInfo
    {
        public GroupPermission Permission { get; set; }

        public long Id { get; set; }

        public string Name { get; set; }

        public MockGroupInfo(string name = "test")
        {
            Id = new Random().Next(0, 1000);
            Name = name;
        }

        public static MockGroupInfo TestMockGroupInfo = new MockGroupInfo("test");
    }


    class MockGroupMemberInfo : IGroupMemberInfo
    {
        public IGroupInfo Group { get; set; } = new MockGroupInfo();

        public GroupPermission Permission { get; set; }

        public long Id { get; set; }

        public string Name { get; set; }
    }
}
