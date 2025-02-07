const fs = require('fs');

function escapeRegExp(string) {
  return string.replace(/[.*+?^${}()|[\]\\]/g, '\\$&');
}

function translateCode(inputFile, outputFile, mappingsFile) {
  try {
    const mappings = {};
    const lines = fs.readFileSync(mappingsFile, 'utf8').trim().split('\n');
    lines.forEach(line => {
      const trimmedLine = line.trim();

      if (trimmedLine.startsWith('#') || trimmedLine === '') {
        return;
      }

      const [kword, csharp] = trimmedLine.split(':');
      if (kword && csharp) {
        mappings[kword.trim()] = csharp.trim();
      }
    });

    let code = fs.readFileSync(inputFile, 'utf8');

    const sortedKeywords = Object.keys(mappings).sort((a, b) => b.length - a.length);

    sortedKeywords.forEach(Keyword => {
      const csharpKeyword = mappings[Keyword];
      const escapedKeyword = escapeRegExp(Keyword);

      const regex = new RegExp(`(?<=\\P{L}|^)${escapedKeyword}(?=\\P{L}|$)(?=\\s*\\()|(?<=\\P{L}|^)${escapedKeyword}(?=\\P{L}|$)`, 'giu');
      code = code.replace(regex, csharpKeyword);
    });

    fs.writeFileSync(outputFile, code, 'utf8');

    console.log(`Transformed code written to: ${outputFile}`);
  } catch (error) {
    console.error('An error occurred:', error);
  }
}

const inputFile = process.argv[2];
const outputFile = process.argv[3];
const mappingsFile = process.argv[4];

if (!inputFile || !outputFile || !mappingsFile) {
  console.error('Usage: node translate.js <input_file> <output_file> <mappings_file>');
  process.exit(1);
}

translateCode(inputFile, outputFile, mappingsFile);