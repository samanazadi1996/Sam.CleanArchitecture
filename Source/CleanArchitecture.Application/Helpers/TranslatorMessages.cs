using CleanArchitecture.Application.DTOs;

namespace CleanArchitecture.Application.Helpers
{
    public static class TranslatorMessages
    {
        public static class AccountMessages
        {
            public static TranslatorMessageDto Account_notfound_with_UserName(string userName) => new(nameof(Account_notfound_with_UserName), [userName]);
            public static TranslatorMessageDto Username_is_already_taken(string userName) => new(nameof(Username_is_already_taken), [userName]);
            public static string Invalid_password() => nameof(Invalid_password);
        }
        public static class ProductMessages
        {
            public static TranslatorMessageDto Product_notfound_with_id(long id)
                => new(nameof(Product_notfound_with_id), [id.ToString()]);
        }
    }
}
