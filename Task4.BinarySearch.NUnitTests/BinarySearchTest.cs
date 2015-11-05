using System;
using System.Collections.Generic;
using NUnit.Framework;
using Task4.BinarySearch;

namespace Task4.BinarySearch.NUnitTests {

    public class ComparableComparer<T> : IComparer<T> where T: IComparable {
        public int Compare(T x, T y) => x.CompareTo(y);
    }

    [TestFixture]
    public class BinarySearchTest {
        private IEnumerable<TestCaseData> TestDatas {
            get {
                yield return new TestCaseData(new[] { 15 }, 15, 0);
                yield return new TestCaseData(new[] { 15 }, 12, -1);

                yield return new TestCaseData(new[] { 12, 15 }, 12, 0);
                yield return new TestCaseData(new[] { 12, 15 }, 15, 1);

                yield return new TestCaseData(new[] { -58, -35, -28, -15, -1, 0, 2, 8, 12, 27, 35, 49, 59, 87, 98 }, 0, 5);
                yield return new TestCaseData(new[] { -58, -35, -28, -15, -1, 0, 2, 8, 12, 27, 35, 49, 59, 87, 98 }, 98, 14);
                yield return new TestCaseData(new[] { -58, -35, -28, -15, -1, 0, 2, 8, 12, 27, 35, 49, 59, 87, 98 }, -58, 0);

                yield return new TestCaseData(new[] { -58, -1, 0, 0, 0, 2, 98 }, 0, 2);

                yield return new TestCaseData(new[] { 0, 0, 0, 0, 0 }, 0, 0);
                yield return new TestCaseData(new[] { -1, 0, 0, 1 }, 0, 1);
                yield return new TestCaseData(new[] {-1, 0, 0, 0, 1}, 0, 1);

                yield return new TestCaseData(new[] { 15, 12 }, 12, 1);
                yield return new TestCaseData(new[] { 15, 12 }, 15, 0);
                yield return new TestCaseData(new[] { 58, 35, 28, 15, 1, 0, -2, -8, -12, -27, -35, -49, -59, -87, -98 }, 1, 4);
                yield return new TestCaseData(new[] { 58, 35, 28, 15, 1, 0, -2, -8, -12, -27, -35, -49, -59, -87, -98 }, -98, 14);
                yield return new TestCaseData(new[] { 58, 35, 28, 15, 1, 0, -2, -8, -12, -27, -35, -49, -59, -87, -98 }, 58, 0);
                yield return new TestCaseData(new[] { 58, 1, 0, 0, 0, -2, -98 }, 0, 2);
                yield return new TestCaseData(new[] { 0, 0, 0, 0, 0 }, 0, 0);
                yield return new TestCaseData(new[] { 1, 0, 0, -1 }, 0, 1);
                yield return new TestCaseData(new[] { 1, 0, 0, 0, -1 }, 0, 1);

                yield return new TestCaseData(new int[] { }, 15, -1).Throws(typeof(ArgumentException));
            }
        }
        [Test, TestCaseSource(nameof(TestDatas))]
        public void BinarySearch_Delegate_Test<T>(T[] array, T value, int result) {
            Assert.AreEqual(array.Search(value, (a,b) => ((IComparable)a).CompareTo(b)), result);
        }

        [Test, TestCaseSource(nameof(TestDatas))]
        public void BinarySearch_IComparer_Test<T>(T[] array, T value, int result) where T : IComparable {
            Assert.AreEqual(array.Search(value, new ComparableComparer<T>()), result);
        }
    }
}