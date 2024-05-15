@echo off
setlocal enabledelayedexpansion

:: Set the directory where .sln files are located
set "directory=../"

:: Loop through each .sln file in the directory and set slnName variable
for /f "delims=" %%a in ('dir /b "%directory%\*.sln"') do (
    set "slnName=%%~na"
)

:: Check if slnName variable is set (i.e., if .sln file exists in the directory)
if not defined slnName (
    echo No .sln file found in the directory.
    exit /b 1
)

:: Prompt the user to enter the migration name
set /p "migrationName=Enter Migration Name: "

:: Print out the command that will be executed
echo Running the following command:
echo dotnet ef migrations add !migrationName! --context IdentityContext --project ..\Src\Infrastructure\!slnName!.Infrastructure.Identity --startup-project ..\Src\Presentation\!slnName!.WebApi

:: Check if the specified paths actually exist before running the dotnet command
if not exist "..\Src\Infrastructure\!slnName!.Infrastructure.Identity" (
    echo Error: Path '..\Src\Infrastructure\!slnName!.Infrastructure.Identity' does not exist.
    exit /b 1
)

if not exist "..\Src\Presentation\!slnName!.WebApi" (
    echo Error: Path '..\Src\Presentation\!slnName!.WebApi' does not exist.
    exit /b 1
)

:: Ask for user confirmation before executing the dotnet command
set /p "confirm=Do you want to proceed? (Y/N): "
if /i "%confirm%" neq "Y" exit /b 0

:: Execute the dotnet command
dotnet ef migrations add !migrationName! --context IdentityContext --project ..\Src\Infrastructure\!slnName!.Infrastructure.Identity --startup-project ..\Src\Presentation\!slnName!.WebApi

pause
