

namespace PipelineSimulator
{
    /// <summary>
    /// It's a Box.  The class is used to keep track of statistics
    /// about each Box.
    /// </summary>
    class Box
    {
        int totalTimeInPipeline;

        public int TotalTimeInPipeline
        {
            get { return totalTimeInPipeline; }
            set { totalTimeInPipeline = value; }
        }

        public Box(int timeInPipeline)
        {
            totalTimeInPipeline = timeInPipeline;
        }
    }
}
