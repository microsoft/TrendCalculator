using System;
using System.Collections.Generic;
using System.Text;
using TrendsCalculator.Library.Interfaces;

namespace TrendsCalculator.Library.Sorter
{
    internal class GlobalZSorter<T> : IComparer<T> where T : TInternal
    {
        public int Compare(T x, T y)
        {
            return (x.GlobalZ <= y.GlobalZ) ? 1 : -1;
        }
    }

    internal class SortingBothPositiveNegative<T> : IComparer<T> where T : TInternal
    {
        public int Compare(T x, T y)
        {
            return (x.GlobalZ <= y.GlobalZ) ? 1 : -1;
        }
    }
    internal class SortingAlternate<T> : IComparer<T> where T : TInternal
    {
        public int Compare(T x, T y)
        {
            return (x.LocalZ <= y.LocalZ) ? 1 : -1;
        }
    }
}
