using System;
using System.Collections.Generic;

namespace gsharp
{
    public class CommandLineParser
    {
        public class Options
        {
            public string? InputFile { get; set; }
            public string? OutputDirectory { get; set; }
            public string? AssemblyName { get; set; }
            public string? WrapAssembly { get; set; }
            public bool Verbose { get; set; }
            public bool Help { get; set; }
        }

        public static Options Parse(string[] args)
        {
            var options = new Options();
            var arguments = new Queue<string>(args);

            while (arguments.Count > 0)
            {
                string arg = arguments.Dequeue();

                switch (arg)
                {
                    case "-o":
                        if (arguments.Count > 0)
                        {
                            options.OutputDirectory = arguments.Dequeue();
                        }
                        else
                        {
                            Console.WriteLine("Error: Missing output directory after -o");
                            options.Help = true;
                            return options;
                        }
                        break;
                    case "-assembly":
                        if (arguments.Count > 0)
                        {
                            options.AssemblyName = arguments.Dequeue();
                        }
                        else
                        {
                            Console.WriteLine("Error: Missing assembly name after -assembly");
                            options.Help = true;
                            return options;
                        }
                        break;
                    case "-wrap":
                        if (arguments.Count > 0)
                        {
                            options.WrapAssembly = arguments.Dequeue();
                        }
                        else
                        {
                            Console.WriteLine("Error: Missing assembly path after -wrap");
                            options.Help = true;
                            return options;
                        }
                        break;
                    case "-verbose":
                        options.Verbose = true;
                        break;
                    case "-help":
                    case "--help":
                        options.Help = true;
                        break;
                    default:
                        if (string.IsNullOrEmpty(options.InputFile))
                        {
                            options.InputFile = arg;
                        }
                        else
                        {
                            Console.WriteLine($"Error: Invalid argument: {arg}");
                            options.Help = true;
                            return options;
                        }
                        break;
                }
            }

            return options;
        }

        public static void PrintUsage()
        {
            Console.WriteLine("Usage: gshc <input_file.gsh> [options]");
            Console.WriteLine("Options:");
            Console.WriteLine("  -o <output_directory>   Specify the output directory for the generated C# code.");
            Console.WriteLine("  -assembly <output_assembly.dll>  Compile the generated C# code into an assembly.");
            Console.WriteLine("  -wrap <assembly_path.dll> Generate G# wrappers for the specified C# assembly.");
            Console.WriteLine("  -verbose                 Enable verbose logging.");
            Console.WriteLine("  -help, --help            Show help information.");
        }
    }
}
