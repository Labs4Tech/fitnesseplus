using SmartPad.Interfaces;
using SmartPad.Shared;
using System.Text.RegularExpressions;

namespace SmartPad.Implementations
{
    public class FitnesseSyntaxParser:Parser
    {
        private string GetColoredText(string text, string color)
        {
            return $"<text style='color:blue'>{text}</text>";
        }
        public override string Parse(string content)
        {
            var parsedContent = "";

            var fixtures = Regex.Split(content.Trim(), @"(?:\r?\n){2,}");

            foreach (var fixture in fixtures)
            {
                parsedContent += "<table class='table table-bordered'>";
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

                foreach (var row in rows)
                {
                    parsedContent += "<tr>";

                    foreach (var cell in row)
                    {
                        parsedContent += "<td>";
                        parsedContent += cell;
                        parsedContent += "</td>";
                    }

                    parsedContent += "</tr>";
                }
                parsedContent += "</table>";
                parsedContent += "<hr/>";
            }


            return parsedContent;
        }
    }
}
