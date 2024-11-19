# [ASP Dotnet Core Clean Architecture](../README.md) - Configure Database

## Guide to Using the Batch File for Configuring a Clean Architecture Project
This guide will help you use the ConfigureDatabase.bat batch file to configure your Clean Architecture project. This batch file allows you to select the desired database type and automatically configure the corresponding class libraries.

## Introduction
Clean Architecture projects are designed to separate business logic from technical details and infrastructure. This pattern includes various layers such as Presentation, Application, Domain, and Infrastructure. The ConfigureDatabase.bat file helps you quickly configure your database and class libraries according to your requirements.

## Steps to Use

### Method 1: Using the dotnet new ca-tools Command
> **_NOTE:_** (Recommended for Versions **8.4.1** and Higher)

- Run the Command in the Project Root In the root directory of your project, next to the solution file, run the following command:
    ```sh
    dotnet new ca-tools
    ```
    This command will create a Tools folder containing several batch files, including `ConfigureDatabase.bat`.


### Method 2: Manually Adding the Batch File

If you are using a version lower than 8.4.1 or prefer manual installation, download the `ConfigureDatabase.bat` file from the following link:

[Download ConfigureDatabase.bat](../Templates/ca-tools/Tools/ConfigureDatabase.bat)

Save the file in a folder named Tools located in the root directory of your project, where the solution file (.sln) is located.

## Using the Batch File for Configuring Database

1. Navigate to the Tools Folder
After running the command, navigate to the Tools folder:
    ```sh
    cd Tools
    ```

2. Run the ConfigureDatabase.bat File

    To run the batch file, execute the following command:
    ```sh
    .\ConfigureDatabase.bat
    ```

3. Select Database Type and Class Library

    After running the batch file, you will be prompted to enter the desired database type and the class library name:
    ```plaintext
    Enter the type of database you want to use (SqlServer, PostgreSQL, Oracle, MySql, Sqlite):
    ```
    And
    ```plaintext
    Enter the name of the ClassLibrary you want to configure (Identity, Persistence, FileManager):
    ```

4. Final Steps

    After selecting the database type and class library, the batch file will automatically add the necessary packages and update the `ServiceRegistration.cs` file.

5. Final Steps After Running the Batch File

    Once the batch file has completed the initial configuration, follow these manual steps:

    1. Set the connection string in the appsettings.json file.
    2. Delete existing migrations.
    3. Create new migrations.
    4. Run the project.

## Conclusion

The `ConfigureDatabase.bat` batch file is a powerful tool for quickly and automatically configuring class libraries based on the desired database type in Clean Architecture projects. By using this tool, you can efficiently set up your .NET projects and get them ready for development. For versions **8.4.1** and higher, you can use the `dotnet new ca-tools` command to automatically generate the necessary tools and batch files, including `ConfigureDatabase.bat`. If you are using an older version or prefer manual setup, you can download and add the batch file manually to the `Tools` folder in your project.
