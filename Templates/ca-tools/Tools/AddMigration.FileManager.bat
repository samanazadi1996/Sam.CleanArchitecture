@echo off

set /p migrationName=Enter Migration Name : 

dotnet ef migrations add %migrationName% --context FileManagerDbContext --project ..\Src\Infrastructure\CleanArchitecture.Infrastructure.FileManager --startup-project ..\Src\Presentation\CleanArchitecture.WebApi
