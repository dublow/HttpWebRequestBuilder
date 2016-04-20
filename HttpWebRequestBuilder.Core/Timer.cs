using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace HttpWebRequestBuilder.Core
{
    public class Timer : IDisposable
    {
        private readonly Stopwatch _stopwatch;

        public Timer()
        {
            _stopwatch = Stopwatch.StartNew();
        }

        public TimeSpan Elapsed
        {
            get { return _stopwatch.Elapsed; }
        }
        public void Dispose()
        {
            _stopwatch.Stop();
        }
    }
}
