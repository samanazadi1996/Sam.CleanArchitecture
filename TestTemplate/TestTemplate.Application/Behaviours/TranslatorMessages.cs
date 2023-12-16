namespace TestTemplate.Application.Behaviours
{
    public static class TranslatorMessages
    {
        public static class AccountMessages
        {
            public static Message Account_notfound_with_UserName(string userName) => new Message(nameof(Account_notfound_with_UserName), [userName]);
            public static Message Username_is_already_taken(string userName) => new Message(nameof(Username_is_already_taken), [userName]);
            public static string Invalid_password() => nameof(Invalid_password);
        }
        public static class ProductMessages
        {
            public static Message Product_notfound_with_id(long id)
                => new Message(nameof(Product_notfound_with_id), [id.ToString()]);
        }
    }

    public struct Message
    {
        public Message(string text, string[] args)
        {
            Text = text;
            Args = args;
        }
        public string Text { get; set; }
        public string[] Args { get; set; }
    }
}
