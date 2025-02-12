using System;
using gsharp.Logging;
using gsharp.Keywords;
using gsharp.Transformation;
using Microsoft.CodeAnalysis.CSharp;
using System.IO;

namespace gsharp
{
    public class Program
    {
        private readonly ILogger _logger;

        public Program()
        {
            _logger = new ConsoleLogger(nameof(Program));
        }

        static void Main(string[] args)
        {
            new Program().Run(args);
        }

        public void Run(string[] args)
        {
            var options = CommandLineParser.Parse(args);

            if (options.Help)
            {
                CommandLineParser.PrintUsage();
                Environment.Exit(0);
            }

            if (string.IsNullOrEmpty(options.InputFile) && string.IsNullOrEmpty(options.WrapAssembly))
            {
                _logger.Log("Error: No input file or assembly to wrap specified.");
                CommandLineParser.PrintUsage();
                Environment.Exit(1);
            }

            try
            {
                var keywordMapping = new Mapping();

                if (!string.IsNullOrEmpty(options.InputFile))
                {
                    CompileGSharp(options, keywordMapping);
                }
                else if (!string.IsNullOrEmpty(options.WrapAssembly))
                {
                    GenerateWrappers(options);
                }
            }
            catch (Exception ex)
            {
                _logger.Log($"Fatal error: {ex.Message}");
                Environment.Exit(1);
            }
        }

        private void CompileGSharp(CommandLineParser.Options options, Mapping keywordMapping)
        {
            if (string.IsNullOrEmpty(options.OutputDirectory))
            {
                _logger.Log("Error: Output directory must be specified.");
                Environment.Exit(1);
            }

            string gsharpSource="";
            try
            {
                if (options.InputFile == null)
                {
                    throw new ArgumentNullException(nameof(options.InputFile), "Input file path cannot be null.");
                }
                gsharpSource = File.ReadAllText(options.InputFile);
            }
            catch (Exception ex)
            {
                _logger.Log($"Error reading input file: {ex.Message}");
                Environment.Exit(1);
            }

            var tree = CSharpSyntaxTree.ParseText(gsharpSource);
            var root = tree.GetRoot();

            var rewriter = new KeywordRewriter(keywordMapping);
            var newRoot = rewriter.Visit(root);

            string csharpSource = newRoot.ToFullString();

            string outputFilePath = Path.Combine(options.OutputDirectory, Path.GetFileNameWithoutExtension(options.InputFile) + ".cs");

            try
            {
                Directory.CreateDirectory(options.OutputDirectory);
                File.WriteAllText(outputFilePath, csharpSource);
                _logger.Log($"G# code translated to C# and saved to: {outputFilePath}");
            }
            catch (Exception ex)
            {
                _logger.Log($"Error writing output file: {ex.Message}");
                Environment.Exit(1);
            }

            if (!string.IsNullOrEmpty(options.AssemblyName))
            {
                // Implement C# compilation here (using Process class to call csc.exe or Roslyn)
                _logger.Log("C# compilation is not yet implemented.");
            }
        }

        private void GenerateWrappers(CommandLineParser.Options options)
        {
            // Implement wrapper generation logic here
            _logger.Log("Wrapper generation is not yet implemented.");
        }
    }
}
