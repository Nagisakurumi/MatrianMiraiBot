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
    }
}
