using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotProblem
{
    
    public class Robot
    {
        bool mIsParachuteFound = false;

        public int mIndex;

        Environment mEnvironment;

        long mRobotLocation;
        List<long> mMovedLocations = new List<long>();

        public Robot(int index) 
        {
            mIndex = index;
            InitializeRobot();
            
        }
        
        private void InitializeRobot()
        {
            mEnvironment = Environment.Instance;

            Land();

            mMovedLocations.Add(mRobotLocation);
        }

        private void Land()
        {
            Random rand = new Random();

            // Using int.MinValue + 1, int.MaxValue - 1 makes the application run for a long time. Therefore, a small set for landing is used.

            //mRobotLocation = rand.Next(int.MinValue / 2 + 1, int.MaxValue / 2 - 1);
            mRobotLocation = rand.Next(-1000, 1000);

            mEnvironment.AddLandingLocation(mRobotLocation);
        }

        public void Move() 
        {
            if (mIsParachuteFound)
            {
                mRobotLocation = mRobotLocation + 2;
            }
            else
            {
                mRobotLocation++;
            }

            mMovedLocations.Add(mRobotLocation);
            mEnvironment.UpdateRobotLocation(mIndex, mRobotLocation);
        }

        public bool CheckEnvironment()
        {
            // Robots can not find each other without one of them finds the others parachute first
            // The only exception is that if they land to the same location they can find themselves immediatly which is checked just after landing
            if (!mIsParachuteFound)
            {
                mIsParachuteFound = mEnvironment.CheckIfParachuteFound(mIndex, mRobotLocation);
            }
            else
            {
                return mEnvironment.CheckIfRobotFound();
            }

            return false;
        }

        public List<long> GetMovementLocations()
        {
            return mMovedLocations;
        }
    }


}
