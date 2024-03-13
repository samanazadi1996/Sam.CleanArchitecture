# [ASP Dotnet Core Clean Architecture](../README.md) - Healthchecks

## Introduction

Healthchecks is a powerful tool that allows you to continuously and automatically monitor the health status of your systems. This tool enables you to easily check the health status of various components of your system and take necessary actions if needed.

## Implementing Healthchecks

**Steps**

1. Adding Healthchecks to services
    ``` c#
    builder.Services.AddHealthChecks();
    ```
2. Using Healthchecks in the pipeline
    ``` c#
    app.UseHealthChecks("/health");
    ```

## Testing Healthchecks

After implementing Healthchecks, you can test it by accessing the "/health" endpoint in your browser. In this endpoint, you can receive a text response with the health status of your system.

## Conclusion

Healthchecks in this project are implemented according to this document.

If necessary, you can also implement your own monitoring page. For this, it is recommended to read the main documents related to Healthchecks.