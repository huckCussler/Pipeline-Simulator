using System.Collections.Generic;

namespace PipelineSimulator
{
    /// <summary>
    /// Represents a truck.  The total time spent at the dock
    /// needs to be kept for each truck.
    /// </summary>
    class Truck
    {
        int totalTimeAtDock;

        public int TotalTimeAtDock
        {
            get { return totalTimeAtDock; }
            set { totalTimeAtDock = value; }
        }
        public Truck()
        {
            totalTimeAtDock = 0;
        }
    }
}
