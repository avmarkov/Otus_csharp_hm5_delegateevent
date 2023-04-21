using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hm5_delegateevent
{
    public static class MyExtention
    {
        //Обобщённая функция расширения, находящая и возвращающая максимальный элемент коллекции
        public static T? GetMax<T>(this IEnumerable e, Func<T, float> getParameter) where T : class
        {
            if (e == null)
            {
                return default(T);
            }

            T max = default(T);

            foreach (T item in e)
            {
                if (
                        (max == null) ||
                        (getParameter(item) > getParameter(max))
                    )
                {
                    max = item;
                }
            }

            return max;
        }
    }
       
}
