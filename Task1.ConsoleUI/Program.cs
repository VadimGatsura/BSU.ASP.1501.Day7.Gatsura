using System;
using System.Threading;
using Task1.Clock;

namespace Task1.ConsoleUI {

    class Timer {
        public void Register(CountdownWatch watch) {
            if(watch == null)
                throw new ArgumentNullException(nameof(watch));
            watch.FinishCountdown += TimerCall;
        }

        public void UnRegister(CountdownWatch watch) {
            if (watch == null)
                throw new ArgumentNullException(nameof(watch));
            watch.FinishCountdown -= TimerCall;
        }

        void TimerCall(object sender, EventArgs e) {
            Console.WriteLine("Timer call");    
        }
    }

    class Alarm {

        public void Register(CountdownWatch watch) {
            if (watch == null)
                throw new ArgumentNullException(nameof(watch));
            watch.FinishCountdown += AlarmCall;
        }

        public void UnRegister(CountdownWatch watch) {
            if (watch == null)
                throw new ArgumentNullException(nameof(watch));
            watch.FinishCountdown -= AlarmCall;
        }

        void AlarmCall(object sender, EventArgs e) {
            Console.WriteLine("Alarm call");
        }
    }
    class Program {
        static void Main(string[] args) {
            CountdownWatch watch = new CountdownWatch();
            Timer timer = new Timer();
            Alarm alarm = new Alarm();

            Console.WriteLine("First call");
            watch.StartCount(5);
            Thread.Sleep(6);
            timer.Register(watch);

            Console.WriteLine("\nSecond call");
            watch.StartCount(5);
            Thread.Sleep(6);
            alarm.Register(watch);

            Console.WriteLine("\nThird call");
            watch.StartCount(5);
            Thread.Sleep(6);
            timer.UnRegister(watch);

            Console.WriteLine("\nFourth call");
            watch.StartCount(5);
            Thread.Sleep(6);
            alarm.UnRegister(watch);

            Console.WriteLine("\nFifth call");
            watch.StartCount(5);
            Thread.Sleep(6);
            timer.UnRegister(watch);
            alarm.Register(watch);

            Console.WriteLine("\nSixth call");
            watch.StartCount(5);
            Thread.Sleep(6);
            alarm.UnRegister(watch);
            Console.ReadLine();
        }
    }
}
