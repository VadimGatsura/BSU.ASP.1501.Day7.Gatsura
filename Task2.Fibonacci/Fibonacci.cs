using System;
using System.Collections.Generic;

namespace Task2.Fibonacci {
    public static class Fibonacci {
        /// <summary>Counting the numbers of the Fibonacci sequence</summary>
        /// <param name="amount">Amount of the  Fibonacci's sequence numbers</param>
        /// <returns><see cref="IEnumerable{T}"/>, which contains the Fibonacci sequence</returns>
        public static IEnumerable<int> GetSequence(int amount) {
            if(amount <= 0)
                throw new ArgumentException(nameof(amount));
            int current = 0, previous = 1;
            
            for(int i = 0; i < amount; i++) {
                yield return current;
                current += previous;
                previous = current - previous;
            }
        }
    }
}
