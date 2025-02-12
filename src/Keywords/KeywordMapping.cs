using System.Collections.Generic;
using gsharp.Logging;
using System.Reflection;
using Newtonsoft.Json;
using System.IO;
using System;

namespace gsharp.Keywords
{
    public class KeywordMapping
    {
        private readonly Dictionary<string, string> _keywordMap =
            new Dictionary<string, string>();
        private readonly ILogger _logger;
        private const string ResourceName = "gsharp.src.Keywords.gsharp.json";

        public KeywordMapping()
        {
            _logger = new ConsoleLogger(nameof(KeywordMapping));
            LoadKeywordMappingsFromResource();
        }

        private void LoadKeywordMappingsFromResource()
        {
            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                using Stream? stream = assembly.GetManifestResourceStream(ResourceName);

                if (stream == null)
                {
                    throw new FileNotFoundException($"Resource '{ResourceName}' not found.");
                }

                using StreamReader reader = new(stream);
                string json = reader.ReadToEnd();

                var mapping = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                if (mapping != null)
                {
                    _keywordMap.Clear();
                    foreach (var item in mapping)
                    {
                        _keywordMap[item.Key] = item.Value;
                    }
                    _logger.Log("Keywords loaded successfully.");
                }
                else
                {
                    throw new InvalidOperationException("Failed to deserialize mapping.");
                }
            }
            catch (Exception ex)
            {
                _logger.Log($"Error loading keyword mapping: {ex.Message}");
                throw;
            }
        }

        public string? GetCSharpKeyword(string germanKeyword)
        {
            if (_keywordMap.TryGetValue(germanKeyword, out string? csharpKeyword))
            {
                return csharpKeyword;
            }
            return null;
        }

        public Dictionary<string, string> GetAllMappings()
        {
            return new Dictionary<string, string>(_keywordMap); // Return a copy to prevent external modification
        }
    }
}
