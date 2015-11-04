using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace Task2.Fibonacci.NUnitTests {
    [TestFixture]
    public class FibonacciTest {
        //0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, 233, 377, 610, 987, 1597, 2584, 4181, 6765, 10946
        public IEnumerable<TestCaseData> TestDatas {
            get {
                yield return new TestCaseData(0, null).Throws(typeof(ArgumentException));
                yield return new TestCaseData(1, new[] {0});
                yield return new TestCaseData(2, new[] { 0, 1 });
                yield return new TestCaseData(3, new[] { 0, 1, 1 });
                yield return new TestCaseData(22, new[] { 0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, 233, 377, 610, 987, 1597, 2584, 4181, 6765, 10946 });
            }
        }
        
        [Test, TestCaseSource(nameof(TestDatas))]
        public void TestFibonacci(int amount, IEnumerable<int> testResult) {
            IEnumerable<int> fibonacci = Fibonacci.GetSequence(amount);
            CollectionAssert.AreEquivalent(fibonacci, testResult);
        }
    }
}
