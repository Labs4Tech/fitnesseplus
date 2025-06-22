using SmartPad.Shared;

namespace SmartPad.Interfaces
{
    public abstract class Formatter
    {
        private readonly TextContent _content;

        protected Formatter(TextContent content)
        {
            _content = content;
        }

        protected abstract string FormatAction(TextContent content);

        public void Format()
        {
            var formattedRawContent = FormatAction(_content);
            _content.UpdateContent(formattedRawContent);
        }
    }
}
