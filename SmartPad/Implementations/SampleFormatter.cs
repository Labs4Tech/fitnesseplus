using SmartPad.Interfaces;
using SmartPad.Shared;
using System.Text;
using System.Text.RegularExpressions;

namespace SmartPad.Implementations
{
    public class SampleFormatter: Formatter
    {
        public SampleFormatter(TextContent content) : base(content)
        {
        }

        protected override string FormatAction(TextContent content)
        {
            var input = content.RawContent;
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;

            string formattedOutput = string.Empty;

            var fixtures = Regex.Split(input.Trim(), @"(\r?\n){2,}");

            foreach (var fixture in fixtures)
            {
                // Step 1: Parse lines and split on '|'
                var rows = fixture
                    .Split('\n')
                    .Select(line =>
                        line.Trim()
                            .Trim('|') // remove outer pipes
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
                        colNumber < cellsInRow.Count ? cellsInRow[colNumber].Length : 0
                    );
                }

                // Step 5: Build formatted output
                var formattedFixture = rows.Select(cellsInRow =>
                {
                    var paddedCells = cellsInRow.Select((cell, i) =>
                    {
                        var paddingAmount =  colWidths[i];
                        if (cellsInRow.Count > maxColCount && i + 1 >= cellsInRow.Count)
                            paddingAmount = 0;
                        else if (cellsInRow.Count < maxColCount && i + 1 == cellsInRow.Count)
                            paddingAmount = colWidths.Skip(i+1).Sum() + 2*maxColCount;
                        return cell.PadRight(paddingAmount);
                    });
                    return "| " + string.Join(" | ", paddedCells) + " |";
                });

                formattedOutput += string.Join("\n", formattedFixture);
            }

            return formattedOutput;
        }
    }
}
