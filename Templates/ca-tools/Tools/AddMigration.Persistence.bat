@echo off

set /p migrationName=Enter Migration Name : 

dotnet ef migrations add %migrationName% --context Applicationdbcontext --project ..\Src\Infrastructure\CleanArchitecture.Infrastructure.Persistence --startup-project ..\Src\Presentation\CleanArchitecture.WebApi
