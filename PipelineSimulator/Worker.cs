using System;

namespace PipelineSimulator
{
    /// <summary>
    /// Base class for Operator and Scanner.  Keeps track of statistics
    /// as well as state for each.
    /// </summary>
    public class Worker
    {
        int totalMinsToSimulate;
        int minProcessTime;
        int maxProcessTime;
        int totalItemsProcessed;
        bool isWorking;
        int totalTimeForTask;
        int remainingTimeForTask;

        public int MinProcessTime
        {
            get { return minProcessTime; }
            set { minProcessTime = value < minProcessTime ? value : minProcessTime; }
        }

        public int MaxProcessTime
        {
            get { return maxProcessTime; }
            set { maxProcessTime = value > maxProcessTime ? value : maxProcessTime; }
        }

        public int TotalItemsProcessed
        {
            get { return totalItemsProcessed; }
        }

        public bool IsWorking
        {
            get { return isWorking; }
        }

        public int TotalTimeForTask
        {
            get { return totalTimeForTask; }
        }

        public Worker(int maxPossibleTime, int totalSimTime)
        {
            totalMinsToSimulate = totalSimTime;
            minProcessTime = maxPossibleTime+1;
            maxProcessTime = 0;
            totalItemsProcessed = 0;
            isWorking = false;
        }

        /// <summary>
        /// Returns the average number of items processed per hour for
        /// this worker.
        /// </summary>
        /// <returns></returns>
        public int getAvgItemsPerHour()
        {
            return totalItemsProcessed / (totalMinsToSimulate/60);
        }

        /// <summary>
        /// (Re-)initializes variables when a Worker starts a new task.
        /// </summary>
        /// <param name="taskTime">Randomly generated integer that represents
        /// the number of minutes the Worker will spend on its new task.</param>
        public void startNewTask(int taskTime)
        {
            totalTimeForTask = taskTime;
            remainingTimeForTask = totalTimeForTask;
            isWorking = true;
        }

        /// <summary>
        /// Resets values when a Worker finishes a task and readies the Worker
        /// to begin a new task.
        /// </summary>
        public void finishCurrentTask()
        {
            MinProcessTime = totalTimeForTask;
            MaxProcessTime = totalTimeForTask;
            totalItemsProcessed++;
            isWorking = false;
        }

        /// <summary>
        /// Ticks a minute away on the Worker's current task.
        /// </summary>
        /// <returns>true if the just ticked minute finishes the Worker's
        /// current task, false o/w.</returns>
        public bool doAStep()
        {
            if(--remainingTimeForTask == 0)
            {
                finishCurrentTask();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Prints statistics for this Worker.
        /// </summary>
        /// <param name="name">Unique identifier for this worker.</param>
        public void printStats(string name)
        {
            Console.WriteLine(name + " results:");
            Console.WriteLine("\tAvg boxes processed per hour: " +
                getAvgItemsPerHour().ToString());
            Console.WriteLine("\tMin box processing time: " +
                minProcessTime.ToString());
            Console.WriteLine("\tMax box processing time: " +
                maxProcessTime.ToString());
            Console.WriteLine("\tTotal boxes processed: " +
                totalItemsProcessed.ToString());
        }
    }
}
