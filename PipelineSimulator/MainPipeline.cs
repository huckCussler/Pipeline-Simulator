using System;
using System.Collections.Generic;

namespace PipelineSimulator
{
    /// <summary>
    /// Primary Driver for the application.
    /// </summary>
    class MainPipeline
    {
        int totalMinutesToSimulate;
        List<Operator> Opers;
        Scanner sc;
        Queue<Box> scannerQ;
        Queue<Box> loadingDock;
        List<int> boxTimes;
        Queue<Truck> trucks;
        List<int> truckTimes;
        Random r;

        public MainPipeline()
        {
            totalMinutesToSimulate = 14400;
            Opers = new List<Operator>()
            {
                new Operator(totalMinutesToSimulate),
                new Operator(totalMinutesToSimulate)
            };
            sc = new Scanner(totalMinutesToSimulate);
            scannerQ = new Queue<Box>();
            loadingDock = new Queue<Box>();
            boxTimes = new List<int>();
            trucks = new Queue<Truck>();
            truckTimes = new List<int>();
            r = new Random();
        }

        /// <summary>
        /// Runs the pipeline for 14400 'minutes' by using an int counter to
        /// represent a single minute.  Checks each operator, the scanner, the trucks,
        /// and the boxes in case any need updating, and increments the timers for
        /// each.
        /// Once the 'week' (loop) is complete, prints results to the console.
        /// </summary>
        public void runPipeline()
        {
            for(int curMin = 0; curMin<=totalMinutesToSimulate; curMin++)
            {
                foreach(Operator Oper in Opers)
                {
                    if(!Oper.IsWorking)
                        Oper.startNewTask(r.Next(20,41));
                    if(Oper.doAStep())
                        scannerQ.Enqueue(new Box(Oper.TotalTimeForTask));
                }

                if(!sc.IsWorking && scannerQ.Count > 0)
                    sc.startNewTask(r.Next(20,61));
                if(sc.IsWorking && sc.doAStep())
                {
                    Box b = scannerQ.Dequeue();
                    b.TotalTimeInPipeline += sc.TotalTimeForTask;
                    loadingDock.Enqueue(b);
                }

                if(curMin > 0 && curMin % 1440 == 0)
                    trucks.Enqueue(new Truck());
                if(trucks.Count > 0 && loadingDock.Count >= 45)
                {
                    Truck t = trucks.Dequeue();
                    truckTimes.Add(t.TotalTimeAtDock);
                    for(int i = 0;i<45;i++)
                        boxTimes.Add(loadingDock.Dequeue().TotalTimeInPipeline);
                }
                foreach(Truck t in trucks)
                    t.TotalTimeAtDock += 1;

                foreach(Box b in loadingDock)
                    b.TotalTimeInPipeline += 1;
            }

            Opers[0].printStats("First Operator");
            Opers[1].printStats("Second Operator");
            sc.printStats("Scanner");
            printTruckStats();
            printBoxStats();
        }

        /// <summary>
        /// Prints results from the trucks to the console.
        /// </summary>
        void printTruckStats()
        {
            List<int> stats = getStats(truckTimes);
            Console.WriteLine("Truck results:");
            Console.WriteLine("\tAvg time waiting at loading dock: " +
                stats[0].ToString());
            Console.WriteLine("\tMin time at loading dock: " +
                stats[1].ToString());
            Console.WriteLine("\tMax time at loading dock: " +
                stats[2].ToString());
        }

        /// <summary>
        /// Prints results from the boxes to the console.
        /// </summary>
        void printBoxStats()
        {
            List<int> stats = getStats(boxTimes);
            Console.WriteLine("Box results:");
            Console.WriteLine("\tAvg time in the system: " +
                stats[0].ToString());
            Console.WriteLine("\tMin time in the system: " +
                stats[1].ToString());
            Console.WriteLine("\tMax time in the systems: " +
                stats[2].ToString());

        }

        /// <summary>
        /// Compiles statistics for trucks and boxes.
        /// </summary>
        /// <param name="lst">The list to find the avg, min, and max of.</param>
        /// <returns>A three-item list containing the average, min, and max
        /// of the elements in the parameter, in that order.</returns>
        List<int> getStats(List<int> lst)
        {
            lst.Sort();
            int sum = 0;
            foreach(int i in lst)
                sum += i;
            return new List<int>() { sum/lst.Count, lst[0], lst[lst.Count-1] };
        }
    }
}
