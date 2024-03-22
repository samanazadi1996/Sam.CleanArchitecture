using CleanArchitecture.Application.DTOs;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Infrastructure.Resources.ProjectResources;
using System.Globalization;
using System.Resources;

namespace CleanArchitecture.Infrastructure.Resources.Services
{
    public class Translator : ITranslator
    {

        private readonly ResourceManager resourceMessages;
        private readonly ResourceManager propertyName;

        public string this[string name] => propertyName.GetString(name, CultureInfo.CurrentCulture) ?? name;

        public Translator()
        {
            resourceMessages = new ResourceManager(typeof(ResourceMessages).FullName, typeof(ResourceMessages).Assembly);
            propertyName = new ResourceManager(typeof(ResourceGeneral).FullName, typeof(ResourceGeneral).Assembly);
        }
        public string GetString(string name)
        {
            return resourceMessages.GetString(name, CultureInfo.CurrentCulture) ?? name;
        }

        public string GetString(TranslatorMessageDto input)
        {
            var value = resourceMessages.GetString(input.Text, CultureInfo.CurrentCulture) ?? input.Text.Replace("_", " ");
            return string.Format(value, input.Args);
        }
    }
}
