using System;
using System.Collections.Generic;
using System.Text;

namespace MatrianMiraiBot.Expends
{
    public static class StringExpend
    {

        /// <summary>
        /// 格式化
        /// </summary>
        /// <param name="format"></param>
        /// <param name="pams"></param>
        /// <returns></returns>
        public static string Format(this string format, params object [] pams)
        {
            return string.Format(format, pams);
        }
    }
}
