using SmartPad.Interfaces;
using SmartPad.Shared;
using System.Text;
using System.Text.RegularExpressions;

namespace SmartPad.Implementations
{
    public static class PipeFormattingExtensions
    {
        public static string TrimStartingPipe(this string text)
        {
            if(text.Trim().StartsWith('|'))
                text = text.Remove(0,1);
            // if (text.Trim().EndsWith('|'))
            //     text = text.Remove(text.Length-2);

            return text;
        }
    }

    public class FitnesseFormatter: Formatter
    {
        public FitnesseFormatter(TextContent content) : base(content)
        {
        }

        void AddNewLines(ref string text, int count)
        {
            for (int i = 0; i < count; i++)
            {
                text += "\n";
            }
        }

        protected override string FormatAction(TextContent content)
        {
            var input = content.RawContent;
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;

            string formattedOutput = string.Empty;

            var fixtures = Regex.Split(input.Trim(), @"(?:\r?\n){2,}");

            foreach (var fixture in fixtures)
            {
                // Step 1: Parse lines and split on '|'
                var rows = fixture
                    .Split('\n')
                    .Select(line =>
                        line.Trim()
                            .TrimStartingPipe()
                            .Split('|')
                            .Select(cell => cell.Trim())
                            .ToList()
                    ).ToList();

                // Step 2: Determine maximum number of columns for the fixture
                int maxColCount = rows.Max(row => row.Count);

                // determine max width for each column of the fixture
                var colWidths = new int[maxColCount];
                for (int colNumber = 0; colNumber < maxColCount; colNumber++)
                {
                    colWidths[colNumber] = rows.Max(cellsInRow =>
                        colNumber < cellsInRow.Count ? cellsInRow[colNumber].Trim().Length : 0
                    );
                }

                // Step 5: Build formatted output
                var formattedLines= rows.Select(cellsInRow =>
                {
                    var paddedCells = cellsInRow.SkipLast(1).Select((cell, i) =>
                    {
                        var paddingAmount =  colWidths[i];
                        if (cellsInRow.Count > maxColCount && i + 2 >= cellsInRow.Count)
                            paddingAmount = cell.Length;
                        else if (cellsInRow.Count < maxColCount && i + 2 == cellsInRow.Count)
                            paddingAmount = colWidths.Skip(i).Sum() + ((maxColCount - i - 2) * 3);

                        return cell.PadRight(paddingAmount);
                    });
                    return "| " + string.Join(" | ", paddedCells) + " |";
                });

                formattedOutput += string.Join("\n", formattedLines);
                AddNewLines(ref formattedOutput, 3);
            }

            return formattedOutput;
        }
    }
}
