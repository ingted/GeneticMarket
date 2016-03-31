using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TestApp
{
    public delegate void testEvent(int x);
    
    public class Class1
    {
        public event testEvent test;

        public void testCall()
        {
            Thread t1 = new Thread(new ThreadStart(caller1));
            Thread t2 = new Thread(new ThreadStart(caller2));

            t1.Start();
            t2.Start();

        }

        private void caller1()
        {
            test(1);
        }

        private void caller2()
        {
            test(2);
        }
    }
}
