using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace IAS.DataServiceTest
{
    [TestClass]
    public class TestTimerInterval
    {
        [TestMethod]                             
        public void TestCanSetTimer()
        {
            Thread thread = new Thread(new ThreadStart(ThreadFunc));
            thread.IsBackground = true;
            thread.Name = "ThreadFunc";
            thread.Start();
        }

        protected void ThreadFunc()
        {
            System.Timers.Timer t = new System.Timers.Timer();
            t.Elapsed += new System.Timers.ElapsedEventHandler(TimerWorker);
            t.Interval = 10000;
            t.Enabled = true;
            t.AutoReset = true;
            t.Start();
        }

        protected void TimerWorker(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (true)
            {
                int i = 0;
            }
        }
    }
}
