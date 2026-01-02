namespace CleanArchitecture.FunctionalTests.Common;

public static class RandomDataExtensions
{
    public static string RandomString(int length)
    {
        Random random = new Random();
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
    }
    public static long RandomNumber(int length)
    {
        Random random = new Random();
        return random.Next(length);
    }
}
