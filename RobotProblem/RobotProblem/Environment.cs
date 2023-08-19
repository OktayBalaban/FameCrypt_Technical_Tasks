using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotProblem
{
    public class Environment
    {
        private static Environment mInstance = null;

        public static Environment Instance
        {
            get
            {
                if (mInstance == null)
                {
                    mInstance = new Environment();
                }
                return mInstance;
            }
        }

        List<long> mParachuteLocations = new List<long>();
        List<long> mRobotLocations = new List<long>();

        public void AddLandingLocation(long location)
        {
            mParachuteLocations.Add(location);
            mRobotLocations.Add(location);
        }

        public bool CheckIfParachuteFound(int robotIndex, long robotLocation)
        {
            if (robotIndex == 0)
            {
                return (robotLocation == mParachuteLocations[1]); 
            }
            else
            {
                return (robotLocation == mParachuteLocations[0]);
            }
        }

        public bool CheckIfRobotFound() 
        {
            return (mRobotLocations[0] == mRobotLocations[1]);
        }

        public void UpdateRobotLocation(int robotIndex, long robotLocation)
        {
            mRobotLocations[robotIndex] = robotLocation;
        }
    }
}
