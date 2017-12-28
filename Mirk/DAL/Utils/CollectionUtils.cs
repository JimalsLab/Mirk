using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mirk.DAL.Utils
{
    public class CollectionUtils
    {
        public static bool IsList(object o)
        {
            if (o == null) return false;
            return o is IList &&
                   o.GetType().IsGenericType &&
                   o.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>));
        }

        public static bool IsDictionary(object o)
        {
            if (o == null) return false;
            return o is IDictionary &&
                   o.GetType().IsGenericType &&
                   o.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(Dictionary<,>));
        }

        public static bool isNotEmpty<X>(IEnumerable<X> list)
        {
            bool isNotEmpty = false;
            if (list != null && list.Count() > 0)
            {
                isNotEmpty = true;
            }

            return isNotEmpty;
        }

        public static List<X> GetEmptyList<X>() where X : class
        {
            return new List<X>();
        }
    }
}
