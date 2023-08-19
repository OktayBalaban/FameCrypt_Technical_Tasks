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
        private static string filePath = "robotMovements.txt";

        // Delete the old file
        public static void DeleteExistingFile()
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        public static void SaveRobotMovements(int index, List<long> movements)
        {
            using (StreamWriter writer = new StreamWriter(filePath, true)) 
            {
                writer.WriteLine($"Robot {index} locations:");

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < movements.Count; i++)
                {
                    sb.Append(movements[i] + ", ");
                }

                // Remove the last ", "
                sb.Length -= 2;

                writer.WriteLine(sb.ToString());

                // An empty line for better readability between robot data
                writer.WriteLine();
                writer.WriteLine();
            }
        }
    }
}
