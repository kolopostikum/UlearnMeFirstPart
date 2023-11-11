using System;
using System.Collections.Generic;
using System.Linq;

namespace Autocomplete
{
    public class RightBorderTask
    {
        public static int GetRightBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
        {
            if (prefix == "")
                return right;
            if (phrases.Count == 0)
                return right;
            
            while (left < right)
            {
                ulong m = ((ulong)left + (ulong)right) / 2;

                var index = (int)m;

                if (index == -1)
                    return 0;
                if ((string.Compare(prefix, phrases[index], StringComparison.OrdinalIgnoreCase) > 0)
                    || phrases[index].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                    left = index + 1;
                else right = index;
            }
            return right;
        }
    }
}