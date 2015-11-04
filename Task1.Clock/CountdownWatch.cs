using System;
using System.Diagnostics;

namespace Task1.Clock {
    public class CountdownWatch {
        public event EventHandler FinishCountdown = delegate { };

        private void OnFinishCountdown() {
            var finishCountdown = FinishCountdown;
            finishCountdown?.Invoke(this, new EventArgs());
        }

        public void StartCount(int milliseconds) {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            while (stopWatch.Elapsed.Milliseconds < milliseconds);
            OnFinishCountdown();
        }
    }
}
