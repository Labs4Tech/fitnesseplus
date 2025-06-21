using SmartPad.Shared;

namespace SmartPad.Interfaces
{
    public abstract class Parser
    {
        private readonly TextContent _content;

        protected Parser(TextContent content)
        {
            _content = content;
        }

        protected abstract string ParseAction(TextContent content);

        public virtual void Parse()
        {
            var parsedRawContent = ParseAction(_content);
            _content.RawContent = parsedRawContent;
        }
    }
}
