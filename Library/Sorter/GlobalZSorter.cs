using System;
using System.Collections.Generic;
using System.Text;
using TrendsCalculator.Library.Interfaces;

namespace TrendsCalculator.Library.Sorter
{
    internal class GlobalZSorter<T> : IComparer<(T item, double localZ, double globalZ)> where T : TModel
    {
        public int Compare((T item, double localZ, double globalZ) x, (T item, double localZ, double globalZ) y)
        {
            return (x.globalZ <= y.globalZ) ? 1 : -1;
        }
    }

    internal class SortingBothPositiveNegative<T> : IComparer<(T item, double localZ, double globalZ)> where T : TModel
    {
        public int Compare((T item, double localZ, double globalZ) x, (T item, double localZ, double globalZ) y)
        {
            return (x.globalZ <= y.globalZ) ? 1 : -1;
        }
    }
    internal class SortingAlternate<T> : IComparer<(T item, double localZ, double globalZ)> where T : TModel
{
        public int Compare((T item, double localZ, double globalZ) x, (T item, double localZ, double globalZ) y)
        {
            return (x.localZ <= y.localZ) ? 1 : -1;
        }
    }
}
