using System;
using gsharp.Logging;
using gsharp.Keywords;
using gsharp.Transformation;
using Microsoft.CodeAnalysis.CSharp;

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
            new Program().Run(args); // Create an instance and call the Run method
        }

        public void Run(string[] args)
        {
            try
            {
                var keywordMapping = new KeywordMapping();

                // Example usage of CustomKeywordRewriter
                string customSource = @"
                    using System;
                    namensraum Demo
                    {
                        klasse Program
                        {
                            statisch leer Main(zeichenkette[] args)
                            {
                                wenn (true)
                                {
                                    Console.WriteLine(""Hello!"");
                                }
                            }
                        }
                    }";

                var tree = CSharpSyntaxTree.ParseText(customSource);
                var root = tree.GetRoot();

                var rewriter = new CustomKeywordRewriter(keywordMapping);
                var newRoot = rewriter.Visit(root);

                string transformedSource = newRoot.ToFullString();

                _logger.Log("Transformation completed successfully.");
                _logger.Log("Transformed Source Code:");
                _logger.Log(transformedSource);
            }
            catch (Exception ex)
            {
                _logger.Log($"Fatal error: {ex.Message}");
                Environment.Exit(1);
            }
        }
    }
}
