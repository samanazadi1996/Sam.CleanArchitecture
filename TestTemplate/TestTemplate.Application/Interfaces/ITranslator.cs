using TestTemplate.Application.Behaviours;

namespace TestTemplate.Application.Interfaces
{
    public interface ITranslator
    {
        string this[string name]
        {
            get;
        }

        string GetString(string name);
        string GetString(Message input);
    }
}