using SmartPad.Interfaces;

namespace SmartPad.Shared
{
    public class TextContent
    {
        public string RawContent { get; private set; } = "";
        public string ParsedContent { get; set; }

        private List<Parser> _parsersToRun;
       
        public TextContent(List<Parser>? parsers = null)
        {
            _parsersToRun = parsers ?? [];
        }

        //TODO: Make this public and on change with a debounce?
        public void ParseContent()
        {
            var parsedContent = RawContent;
            foreach (var parser in _parsersToRun)
            {
                parsedContent = parser.Parse(parsedContent);
            }
            ParsedContent = parsedContent;
        }

        public void UpdateContent(string updatedRawContent)
        {
            RawContent = updatedRawContent;
            ParsedContent = updatedRawContent;
        }
    }
}
