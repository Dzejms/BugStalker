using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugStalker.Domain;
using CommandLine;
using CommandLine.Text;

namespace BugStalker.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {

            var options = new Options();
            ICommandLineParser parser = new CommandLineParser();
            if (parser.ParseArguments(args, options))
            {
                ScreenCollector collector = new ScreenCollector(new ScreenGrabber(ImageFormat.Png), options.FramesPerSecond, options.Minutes * 60, options.FilePath);
                collector.Start();
                string input = Console.ReadLine();
                collector.Stop();
                Console.WriteLine("{0} frames collected", collector.NumberOfFrames);
            } else
            {
                Console.WriteLine("Invalid paramaters.");
                Console.WriteLine(options.GetUsage());
            }

        }
    }

    class Options
    {
        [Option("p", "file path", Required = false, DefaultValue = @"C:\Users\James\AppData\Local\Temp", HelpText = "Path where you want to store the screen shots")]
        public string FilePath { get; set; }

        [Option("f", "frames per second", Required = false, DefaultValue = 2, HelpText = "Rate at which the screenshots are captured")]
        public int FramesPerSecond { get; set; }

        [Option("m", "minutes", Required = false, DefaultValue = 1, HelpText = "Length of the video")]
        public int Minutes { get; set; }

        [Option("v", "verbose", Required = false, HelpText = "Print details during execution.")]
        public bool Verbose { get; set; }

        [HelpOption(HelpText = "Display this help screen")]
        public string GetUsage()
        {
            // this without using CommandLine.Text
            var usage = new StringBuilder();
            usage.AppendLine("Quickstart Application 1.0");
            usage.AppendLine("Read user manual for usage instructions...");
            return usage.ToString();
        }
    }
}
