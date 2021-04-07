using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MatrianMiraiBot.Expends
{
    public static class ArrayExpend
    {
        /// <summary>
        /// 打乱数组
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static List<T> ShuffleCopy<T>(this IEnumerable<T> data, Random r)
        {
            var arr = data.ToList();

            for (var i = arr.Count - 1; i > 0; --i)
            {
                int randomIndex = r.Next(i + 1);

                T temp = arr[i];
                arr[i] = arr[randomIndex];
                arr[randomIndex] = temp;
            }
            return arr;
        }

        /// <summary>
        /// 转换为字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ts"></param>
        /// <param name="split"></param>
        /// <param name="convert"></param>
        /// <returns></returns>
        public static string JoinToString<T>(this IEnumerable<T> ts, string split, Func<T, string> convert = null)
        {
            string content = "";
            var list = ts.ToList();
            for (int i = 0; i < list.Count(); i++)
            {
                string c = "";
                if(convert == null)
                {
                    c = list[i].ToString();
                }
                else
                {
                    c = convert(list[i]);
                }
                if(list.Count - 1 == i)
                {
                    content += c;
                }
                else
                {
                    content += c + split;
                }
            }
            return content;
        }
    }
}
