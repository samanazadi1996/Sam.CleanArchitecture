@echo off

set /p migrationName=Enter Migration Name : 

dotnet ef migrations add %migrationName% --context IdentityContext --project ..\Src\Infrastructure\CleanArchitecture.Infrastructure.Identity --startup-project ..\Src\Presentation\CleanArchitecture.WebApi
