

namespace PipelineSimulator
{
    /// <summary>
    /// A scanner has a max processing time of 60 minutes.  All other
    /// logic for the class is located in the Worker class.
    /// </summary>
    class Scanner : Worker
    {
        public Scanner(int totalSimTime) : base(60, totalSimTime){ }
    }
}
