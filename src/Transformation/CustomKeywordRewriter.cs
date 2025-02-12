using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using gsharp.Keywords;
using gsharp.Logging;
using System;

namespace gsharp.Transformation
{
    public class CustomKeywordRewriter : CSharpSyntaxRewriter
    {
        private readonly KeywordMapping _keywordMapping;
        private readonly ILogger _logger;

        public CustomKeywordRewriter(KeywordMapping keywordMapping)
        {
            _keywordMapping = keywordMapping ??
                throw new ArgumentNullException(nameof(keywordMapping));
            _logger = new ConsoleLogger(nameof(CustomKeywordRewriter));
        }

        public override SyntaxToken VisitToken(SyntaxToken token)
        {
            try
            {
                if (token.IsKind(SyntaxKind.IdentifierToken))
                {
                    string tokenText = token.ValueText;

                    if (_keywordMapping.GetAllMappings().TryGetValue(tokenText, out string? mappedKeyword))
                    {
                        if (string.IsNullOrEmpty(mappedKeyword))
                        {
                            _logger.Log($"Warning: Empty mapping for token: {tokenText}");
                            return token;
                        }

                        _logger.Log($"Found mapping: {tokenText} -> {mappedKeyword}");

                        return SyntaxFactory.Identifier(
                            token.LeadingTrivia,
                            mappedKeyword,
                            token.TrailingTrivia
                        );
                    }
                }

                return base.VisitToken(token);
            }
            catch (Exception ex)
            {
                _logger.Log($"Error processing token {token.ValueText}: {ex.Message}");
                return token;
            }
        }
    }
}
