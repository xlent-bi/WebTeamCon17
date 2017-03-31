using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Render
{
    static class LinqExtensions
    {
        /// <summary>
        /// Groups an enumerable sequence into groups of width elements.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> Group<T>(this IEnumerable<T> source, int width)
        {
            var count = 0;
            T[] list = null;

            foreach (var element in source)
            {
                if (list == null)
                    list = new T[width];

                list[count++] = element;

                if (count == width)
                {
                    yield return list;
                    count = 0;
                    list = null;
                }
            }

            if (list != null)
                yield return list;
        }
    }
}
