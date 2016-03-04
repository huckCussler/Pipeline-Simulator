using System;

namespace PipelineSimulator
{
    /// <summary>
    /// An operator has a max processing time of 40 minutes.  All other
    /// logic for the class is located in the Worker class.
    /// </summary>
    class Operator : Worker
    {
        public Operator(int totalSimTime) : base(40, totalSimTime){ }

       
    }
}
