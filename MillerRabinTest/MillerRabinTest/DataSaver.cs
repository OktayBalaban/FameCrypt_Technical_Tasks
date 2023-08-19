using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RobotProblem
{
    public static class DataSaver
    {
        private static string filePath = "PrimeNumbers.txt";

        public static void SaveResults(List<string> results)
        {
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                for (int i = 0; i < results.Count; i++)
                {
                    writer.WriteLine(results[i]);
                }

                // An empty line for better readability between robot data
                writer.WriteLine();
                writer.WriteLine();
            }
        }
    }
}

