using System;
using System.Collections.Generic;
using System.Text;

namespace GeneticMarket.Base.Helper
{
    public class NumStr
    {
        public static string ColonifyWeight(double n)
        {
            return ColonifyNumber(WeightToString(n), 0);
        }

        public static string ColonifyNumber(int n)
        {
            return ColonifyNumber(n.ToString(), 0);
        }

        public static string ColonifyNumber(string str,int precision)
        {            
            string prefix = "";
            string suffix = "";

            if (str.StartsWith("-"))
            {
                prefix = "-";
                str = str.Substring(1);
            }

            int dotIndex = str.IndexOf(".");
            if (dotIndex != -1)
            {
                suffix = str.Substring(dotIndex);
                str = str.Substring(0, dotIndex);
            }
            
            int rem = str.Length % 3;
            int partCount = (int)(str.Length / 3);

            string result = str.Substring(0, rem);

            for (int i = 0; i < partCount; i++)
            {
                if (i > 0 || rem != 0)
                {
                    result += ",";
                }
                result += str.Substring(rem + (3 * i), 3);
            }

            return prefix+result+suffix;
        }

        public static string WeightToString(double weight)
        {
            return weight.ToString("F2");
        }
    }
}
