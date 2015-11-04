using System;
using System.Collections.Generic;
using static System.Console;

namespace Task3.Queue.ConsoleUI {
    class Program {
        static void Main(string[] args) {
            WriteLine("Start Queue test");
            CustomQueue<int> queue = new CustomQueue<int>(new List<int> {
                0, 1, 2, 4, 5, -5, -8, 95
            });
            
            WriteLine("\nTest foreach");
            foreach(var i in queue) {
                Write($"{i} .. ");
            }

            queue.Enqueue(57);
            queue.Enqueue(-57);
            queue.Enqueue(0);

            WriteLine("\nTest Enqueue /57, -57, 0/");
            foreach (var i in queue) {
                Write($"{i} .. ");
            }

            WriteLine("\nTest Error when enumerate and modify");
            try {
                foreach(var i in queue) {
                    Write($"{i} .. ");
                    WriteLine($"Try to dequeue: {queue.Dequeue()}");
                }
            } catch(InvalidOperationException ex) {
                WriteLine(ex);
            }

            WriteLine("\nTest Dequeue 6 elements");
            WriteLine($"Dequeue: {queue.Dequeue()}");
            WriteLine($"Dequeue: {queue.Dequeue()}");
            WriteLine($"Dequeue: {queue.Dequeue()}");
            WriteLine($"Dequeue: {queue.Dequeue()}");
            WriteLine($"Dequeue: {queue.Dequeue()}");
            WriteLine($"Dequeue: {queue.Dequeue()}");
            WriteLine("\nWrite after dequeue");
            foreach (var i in queue) {
                Write($"{i} .. ");
            }

            WriteLine("\nCheck Peek");
            WriteLine($"Peek: {queue.Peek()}");

            WriteLine("\nTest Clear");
            queue.Clear();
            WriteLine($"Count: {queue.Count}");

            ReadLine();
        }
    }
}
