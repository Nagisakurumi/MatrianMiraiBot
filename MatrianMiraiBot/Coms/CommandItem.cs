using System;
using System.Collections.Generic;
using System.Text;

namespace MatrianMiraiBot.Coms
{
    /// <summary>
    /// 命令项
    /// </summary>
    public class CommandItem
    {
        /// <summary>
        /// 命令
        /// </summary>
        public string Command { get; set; }
        /// <summary>
        /// /命令内容
        /// </summary>
        public List<string> Contents { get; set; } = new List<string>();
        /// <summary>
        /// 命令
        /// </summary>
        /// <param name="command"></param>
        public CommandItem(string command)
        {
            string[] cs = command.Split(' ');
            if(cs.Length >= 1)
            {
                if (cs[0].Equals("")) throw new Exception("命令格式错误");
                this.Command = cs[0];
                for (int i = 1; i < cs.Length; i++)
                {
                    var item = cs[i];
                    if (!item.Equals(""))
                        Contents.Add(item);
                }
            }
            else
            {
                throw new Exception("命令格式错误");
            }
        }
    }
}
