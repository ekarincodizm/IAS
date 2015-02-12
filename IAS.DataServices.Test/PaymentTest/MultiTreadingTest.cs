using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace IAS.DataServices.Test.PaymentTest
{
    [TestClass]
    public class MultiTreadingTest
    {
        //private StringWriter _consoleOutput = new StringWriter();
        //private TextWriter _originalConsoleOutput;

        [TestMethod]
        public void TestMultiTreadingTasks()     
        {

            Debug.WriteLine("AppStart");
            //this._originalConsoleOutput = Console.Out;
            //Console.SetOut(_consoleOutput);

            Task<Int32[]> parent = new Task<Int32[]>(() =>
                                        {
                                            var results = new Int32[3];   // Create an array for the results

                                            // This tasks creates and starts 3 child tasks
                                            new Task(() => results[0] = Sum(100),
                                                TaskCreationOptions.AttachedToParent).Start();
                                            new Task(() => results[1] = Sum(200),
                                                TaskCreationOptions.AttachedToParent).Start();
                                            new Task(() => results[2] = Sum(300),
                                                TaskCreationOptions.AttachedToParent).Start();

                                            // Returns a reference to the array
                                            // (even though the elements may not be initialized yet)
                                            Debug.WriteLine(results.ToString());
                                            return results;
                                          
                                        });

            // When the parent and its children have
            // run to completion, display the results
            var cwt = parent.ContinueWith(parentTask =>
                                Array.ForEach(parentTask.Result, Console.WriteLine));

            // Start the parent Task so it can start its children
            parent.Start();

            cwt.Wait(); // For testing purposes
        }
        private static Int32 Sum(Int32 n)
        {
            Int32 sum = 0;
            for (; n > 0; n--)
                checked { sum += n; }
            return sum;
        }
    }
}
