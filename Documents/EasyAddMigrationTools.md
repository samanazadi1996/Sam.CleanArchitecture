# [ASP Dotnet Core Clean Architecture](../README.md) - Easy Add Migration 

## Introduction

In every software project, managing the database and making changes to it is a crucial and often frequent task. As projects grow, the need to create migrations to alter the database structure and add new changes without causing issues typically increases. However, manually creating migrations can be time-consuming and error-prone.

To address this challenge and establish an efficient and automated process for managing migrations, we've developed a set of tools that allows developers to easily and confidently create the necessary migrations.

In this article, we'll introduce and demonstrate the usage of these useful tools for creating migrations in three different sections of a .NET project:

- AddMigration.Identity.bat : For creating migrations related to the Identity section of the project.
- AddMigration.FileManager.bat : For creating migrations related to the FileManager section of the project.
- AddMigration.Persistence.bat : For creating migrations related to the Persistence section (e.g., creating tables and relationships in the main database) of the project.


We'll delve into detailed explanations of how to use these tools, the required configurations, and practical examples to empower you to utilize these tools effectively and enhance the database migration process of your project.



## Getting Started

### Adding the Tools to Your Project

To streamline the process of managing migrations in your project, we've provided a convenient way to add the necessary tools directly to your project directory. Follow these steps:

Open Command Prompt or Terminal: Navigate to the directory where your solution file (CleanArchitecture.sln) is located using the command prompt (Windows) or terminal (macOS/Linux).

Execute the Command: Run the following command to add the tools to your project:

```sh
dotnet new ca-tools
```
> This command will create a directory named "Tools" adjacent to your solution file and populate it with the batch files necessary for managing migrations.

Verify the Tools: Once the command execution is complete, navigate to the project directory to ensure that the "Tools" directory has been created and contains the required batch files.

---
### Using the Tools

With the tools added to your project, you can now easily manage migrations using the provided batch files. Here's how to use them:

Open Command Prompt or Terminal: Navigate to the directory where your solution file (CleanArchitecture.sln) is located.

Execute the Batch Files: To create a migration, execute the desired batch file by typing its name followed by pressing Enter. For example:

- #### Windows 
    -Rub 'bat' file. for example:
    ```sh
    ./AddMigration.Persistence.bat
    ```
- #### Linux
    - Installing Wine (if you don't have it installed)
        ```sh
        sudo apt install wine
        ```
    - Rub 'bat' filewith 'Wine'. for example:
        ```sh
        wine AddMigration.Persistence.bat
        ```

Follow the prompts to enter the migration name, and the migration will be created for the corresponding section of your project.

Verify the Migration: After executing the batch file, verify that the migration has been successfully created by checking the Migrations folder in your respective project directory.

> Note: Ensure that you have the necessary permissions and configurations set up to execute batch files and run Entity Framework Core commands.

## Conclusion

In this article, we introduced and demonstrated the usage of effective tools for managing migrations in .NET projects. These tools, comprised of .bat Batch files, allow developers to easily create and manage the necessary migrations.

By executing the relevant commands in the .bat files, developers can seamlessly and confidently create migrations for various sections of their projects and enjoy a streamlined database management process.

Using these tools, you can save considerable time and effort in migration creation and management, leading to significant improvements in your development team's efficiency and productivity.

Therefore, we recommend leveraging these effective tools for managing migrations in your .NET projects and reaping the benefits they offer.

