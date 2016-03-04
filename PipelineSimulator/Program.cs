using System;


namespace PipelineSimulator
{
    /// <summary>
    /// Entry point for the application.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            MainPipeline MP = new MainPipeline();
            MP.runPipeline();
            Console.ReadLine();
        }
    }
}
