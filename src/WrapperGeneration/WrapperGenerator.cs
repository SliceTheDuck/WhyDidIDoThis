using gsharp.Logging;

namespace gsharp.WrapperGeneration
{
    public class WrapperGenerator
    {
        private readonly ILogger _logger;

        public WrapperGenerator(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void GenerateWrappers(string[] assemblyPaths)
        {
            // Generate wrappers for the given assemblies
        }
    }
}
