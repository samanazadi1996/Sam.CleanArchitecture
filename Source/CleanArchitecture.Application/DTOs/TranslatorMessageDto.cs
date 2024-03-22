namespace CleanArchitecture.Application.DTOs
{
    public struct TranslatorMessageDto
    {
        public TranslatorMessageDto(string text, string[] args)
        {
            Text = text;
            Args = args;
        }
        public string Text { get; set; }
        public string[] Args { get; set; }
    }
}
