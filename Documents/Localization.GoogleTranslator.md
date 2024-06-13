# [ASP Dotnet Core Clean Architecture](../README.md) - GoogleTranslator

## Introduction

In today's interconnected world, software development increasingly demands support for multiple languages and geographical regions. This need is particularly crucial for businesses aiming to expand into global markets. One of the key challenges in this endeavor is managing and implementing localization and translation systems effectively.

In this article, we will guide you through the process of adding translation and localization capabilities to your projects using the Clean Architecture pattern and versatile translation services. By leveraging 'ITranslator' and 'GoogleTranslator', you'll be able to easily translate messages and texts into various languages. We'll also explore how to read configurations from appsettings.json to dynamically select your desired translation service.

This step-by-step tutorial will empower you to enhance your application's capabilities in supporting multiple languages, thereby delivering a better user experience for your global audience. Join us as we delve into the implementation steps of these features with thorough explanations and practical insights.

## Getting Started

#### To get started, you need to complete the 'ResourceMessages.resx' and 'ResourceGeneral.resx' files with content in English (Culture 'en'). These files serve as the base texts from which translations to other languages will be derived.

---

### Step 1: Define the 'GoogleTranslator' Class
 Create a new class file named 'GoogleTranslator.cs' within your project's 'Services' folder (assuming 'Services' is inside 'Resources')
```c#
using CleanArchitecture.Application.DTOs;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Infrastructure.Resources.ProjectResources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Resources;
using System.Text.Json;

namespace CleanArchitecture.Infrastructure.Resources.Services;

public class GoogleTranslator : ITranslator
{
    private Dictionary<string, string> googleMessages;
    private const string baseCulture = "en";
    private readonly ResourceManager resourceMessages;
    private readonly ResourceManager resourceGeneral;
    private readonly HttpClient httpClient;

    public string this[string text] => GetString(text, resourceGeneral) ?? text;

    public GoogleTranslator()
    {
        resourceMessages = new ResourceManager(typeof(ResourceMessages).FullName, typeof(ResourceMessages).Assembly);
        resourceGeneral = new ResourceManager(typeof(ResourceGeneral).FullName, typeof(ResourceGeneral).Assembly);
        googleMessages = new();
        httpClient = new();
    }
    public string GetString(string text)
    {
        return GetString(text, resourceMessages);
    }

    public string GetString(TranslatorMessageDto input)
    {
        var result = GetString(input.Text, resourceMessages);
        return string.Format(result, input.Args);
    }

    #region private methods
    string GetString(string name, ResourceManager rm)
    {
        var currentCulture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
        var value = rm.GetString(name, CultureInfo.GetCultureInfo(baseCulture)) ?? name;

        if (currentCulture == baseCulture)
            return value;

        return TranslateWithGoogle(value, currentCulture);
    }

    string TranslateWithGoogle(string text, string targetCulture)
    {
        var cacheKey = $"{targetCulture}.{text}";
        if (googleMessages.TryGetValue(cacheKey, out var translatedText))
        {
            return translatedText;
        }

        try
        {
            var url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl={baseCulture}&tl={targetCulture}&dt=t&dj=1&q={Uri.EscapeDataString(text)}";
            var response = httpClient.GetStringAsync(url).Result;
            var translationResponse = JsonSerializer.Deserialize<TranslationResponse>(response,
                new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            translatedText = translationResponse?.Sentences?.FirstOrDefault()?.Trans ?? text;
            googleMessages.TryAdd(cacheKey, translatedText);

            return translatedText;
        }
        catch (Exception)
        {
            // Log the exception or handle accordingly
            return text;
        }
    }
    #endregion
}

#region Models
public class TranslationResponse
{
    public List<Sentence> Sentences { get; set; }
}

public class Sentence
{
    public string Trans { get; set; }
}
#endregion
```


### Step 2: Register 'GoogleTranslator' in Dependency Injection

Once GoogleTranslator is implemented, register it as a singleton service in the [ServiceRegistration](../Source/Src/Infrastructure/CleanArchitecture.Infrastructure.Resources/ServiceRegistration.cs) class (if not already done)

``` c#
public static class ServiceRegistration
{
    public static void AddResourcesInfrastructure(this IServiceCollection services)
    {
        // services.AddSingleton<ITranslator, Translator>();
        services.AddSingleton<ITranslator, GoogleTranslator>();
    }
}
```
---
### Step 3: Add Desired Cultures to 'appsettings.json'

You need to add the desired cultures to your 'appsettings.json' file to configure the supported languages in your application.

```json
{
  "Localization": {
    "DefaultRequestCulture": "en",
    "SupportedCultures": [ "en", "fa", "ru", "ku", "ar" ] // add more
  }
}
```
---
### Step 4: Running the Application and Calling APIs

Now that the localization system is set up, you can run your application and make API calls. To specify the desired language for the response, you need to include the 'Accept-Language' header in your request.

Here is an example of how to make a POST request to the Authenticate API with the 'Accept-Language' header set to Russian (ru):

Example Request
```sh
curl -X 'POST' \
  'https://localhost:5001/api/v1/Account/Authenticate' \
  -H 'accept: text/plain' \
  -H 'Content-Type: application/json' \
  -H 'Accept-Language: ru' \
  -d '{
  "userName": "string",
  "password": "string"
}'
```
### Additional Information

- Changing the Language: You can change the value of the Accept-Language header to any of the supported cultures listed in your appsettings.json file (e.g., fa for Persian, ru for Russian, ku for Kurdish, ar for Arabic).

- Testing Other Endpoints: Similarly, you can test other API endpoints by changing the URL and the request body as needed, ensuring to include the Accept-Language header for localization.


## Conclusion

In this guide, we successfully set up a localization system in an ASP.NET Core application. By completing the necessary configurations and implementing the GoogleTranslator service, your application can now support multiple languages and dynamically provide localized content based on user preferences. This enhances user experience and accessibility, allowing you to cater to a global audience effectively.





