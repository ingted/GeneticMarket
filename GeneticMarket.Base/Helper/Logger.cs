using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace GeneticMarket.Base.Helper
{
    public static class Logger
    {
        public static string LogFilePath = @"e:\Persistent\MyDev\GeneticMarket\Solution\Log\log.txt";
        public static ListBox LogList = null;
        public static bool LogToVS = false;

        private static StreamWriter sw = null;

        public static void Log(Exception exc)
        {
            Log(exc.Message + "Target Site:{0}, Stack Trace: {1}", exc.TargetSite,exc.StackTrace);
        }

        public static void LogLine()
        {
            Log("",false);
            Log("---------------------------------------------------------------------------------------",false);
            Log("",false);
        }

        public static void Log(string message, params object[] data)
        {
            Log(message, true, data);
        }

        public static void Log(string message, bool addDate, params object[] data)
        {
            return;
            string msg = string.Format(message, data);

            string line = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss");
            line += "\t" + msg;

            if (false == addDate)
            {
                line = msg;
            }

            if (LogFilePath != null)
            {
                if (sw == null)
                {
                    sw = new StreamWriter(LogFilePath);
                }

                sw.WriteLine(line);
                sw.Flush();
            }

            if (LogList != null)
            {
                LogList.Invoke(new MethodInvoker(delegate()
                    {
                        LogList.Items.Add(line);
                    }));
            }

            if (LogToVS)
            {
                Debug.WriteLine(line);
            }
        }
    }
}
