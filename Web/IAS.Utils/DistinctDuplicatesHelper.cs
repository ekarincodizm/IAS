using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IAS.Utils
{
    public class DistinctDuplicatesHelper
    {
        public static IEnumerable<T> Duplicates<T>(IEnumerable<T> source, bool distinct = true)
        {
            if (source == null)
            {
                return new List<T>();
            }

            // select the elements that are repeated
            IEnumerable<T> result = source.GroupBy(a => a).SelectMany(a => a.Skip(1));

            if (result.Count() > 0)
            {
                // distinct?
                if (distinct == true)
                {
                    return  result.Distinct();
                }
            }

            return source;
        }
    }
}
