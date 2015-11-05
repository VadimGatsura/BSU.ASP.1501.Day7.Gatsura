using System;
using System.Collections.Generic;

namespace Task4.BinarySearch {

    public static class BinarySearch {

        #region Public Methods

        #region Delegate
        public static int Search<T>(this T[] array, T value, int leftIndex, int rightIndex, Comparison<T> Compare) {
            if (array == null)
                throw new ArgumentNullException(nameof(array));
            if (value == null)
                throw new ArgumentNullException(nameof(value));
            if (array.Length == 0)
                throw new ArgumentException("Array is empty");
            if (leftIndex < 0 || leftIndex > array.Length)
                throw new ArgumentOutOfRangeException(nameof(leftIndex));
            if (leftIndex > rightIndex || rightIndex < 0 || rightIndex > array.Length)
                throw new ArgumentOutOfRangeException(nameof(rightIndex));

            if (array.Length == 1)
                return Compare(array[0], value) == 0 ? 0 : -1;

            int result = Compare(array[0], array[rightIndex]);
            if (result == 0 && leftIndex < rightIndex)
                return 0;
            bool asc = result < 0;
            if (asc && (Compare(value, array[0]) < 0 || Compare(value, array[rightIndex]) > 0))
                return -1;
            if (!asc && (Compare(value, array[0]) > 0 || Compare(value, array[rightIndex]) < 0))
                return -1;

            return Search(array, value, asc, leftIndex, rightIndex, Compare);
        }
        public static int Search<T>(this T[] array, T value, Comparison<T> Compare) => Search(array, value, 0, array.Length - 1, Compare);
        #endregion

        #region IComparer<T>
        public static int Search<T>(this T[] array, T value, int leftIndex, int rightIndex, IComparer<T> comparator)
            => Search(array, value, leftIndex, rightIndex, comparator.Compare);
        public static int Search<T>(this T[] array, T value, IComparer<T> comparator) => Search(array, value, 0, array.Length - 1, comparator.Compare);
        #endregion
        #endregion

        #region Private Methods
        private static int Search<T>(T[] array, T value, bool asc, int left, int right, Comparison<T> Compare) {
            if (left > right)
                return -1;
            if (left == right)
                return Compare(array[left], value) == 0 ? left : -1;

            int middle = left + (right - left) / 2;
            if (Compare(value, array[middle]) == 0) {
                return middle - left < 1 ? middle : Search(array, value, asc, left, middle, Compare);
            }

            if ((Compare(value, array[middle]) < 0 ^ !asc))//&& asc
                return Search(array, value, asc, left, middle, Compare);
            return Search(array, value, asc, middle + 1, right, Compare);
        }
        #endregion
    }
}
