using System;

namespace RobotProblem
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Started");

            bool isRobotsFound = false;

            Robot robot0 = new Robot(0);
            Robot robot1 = new Robot(1);

            List<long> robot0movements;
            List<long> robot1movements;

            Environment environment = Environment.Instance;

            // Check if the robots landed at the same location
            environment.CheckIfRobotFound();

            while (!isRobotsFound) 
            {
                robot0.Move();
                robot1.Move();

                isRobotsFound = robot0.CheckEnvironment();
                
                // A check is necessary as the second one may return false as it checks for the parachute first before checking the robot
                if (!isRobotsFound) 
                {
                    isRobotsFound = robot1.CheckEnvironment();
                }
            }

            robot0movements = robot0.GetMovementLocations();
            robot1movements = robot1.GetMovementLocations();

            // If there is already a .txt file, delete the old file first
            DataSaver.DeleteExistingFile();

            DataSaver.SaveRobotMovements(0, robot0movements);
            DataSaver.SaveRobotMovements(1, robot1movements);
        }
    }
}

