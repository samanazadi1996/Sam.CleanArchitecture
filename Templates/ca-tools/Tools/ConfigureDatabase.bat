@echo off
setlocal enabledelayedexpansion

:: Prompt for database type and class library name
set /p userInput=Enter the type of database you want to use (SqlServer, PostgreSQL, Oracle, MySql, Sqlite):
set /p classLib=Enter the name of the ClassLibrary you want to configure (Identity, Persistence, FileManager):

:: Set the directory where .sln files are located
set "directory=../"

:: Loop through each .sln file in the directory and set slnName variable
for /f "delims=" %%a in ('dir /b "%directory%\*.sln"') do (
    set "slnName=%%~na"
)

echo Solution name detected: !slnName!

:: Function to add package and update ServiceRegistration.cs
if /i "%userInput%"=="PostgreSQL" (
    echo Configuring for PostgreSQL...
    call :AddPackageAndUpdate "UseNpgsql" "Npgsql.EntityFrameworkCore.PostgreSQL"
)

if /i "%userInput%"=="SqlServer" (
    echo Configuring for SQL Server...
    call :AddPackageAndUpdate "UseSqlServer" "Microsoft.EntityFrameworkCore.SqlServer"
)

if /i "%userInput%"=="Oracle" (
    echo Configuring for Oracle...
    call :AddPackageAndUpdate "UseOracle" "Oracle.EntityFrameworkCore"
)

if /i "%userInput%"=="MySql" (
    echo Configuring for Oracle...
    call :AddPackageAndUpdate "UseMySql" "Pomelo.EntityFrameworkCore.MySql"
)

if /i "%userInput%"=="Sqlite" (
    echo Configuring for Oracle...
    call :AddPackageAndUpdate "UseSqlite" "Microsoft.EntityFrameworkCore.Sqlite"
)

echo Configuration complete. Please perform the following steps:
echo 1. Set the connection string in appsettings.json.
echo 2. Delete existing migrations.
echo 3. Create new migrations.
echo 4. Run Project.

goto :EOF

:AddPackageAndUpdate
    echo Changing directory to: ..\Src\Infrastructure\!slnName!.Infrastructure.%classLib%
    pushd ..\Src\Infrastructure\!slnName!.Infrastructure.%classLib%
   
    echo Adding package %2...
    dotnet add package %2
   
    echo Updating ServiceRegistration.cs to use %1...
    powershell -Command "(Get-Content 'ServiceRegistration.cs') -replace 'UseSqlServer', '%1' | Set-Content 'ServiceRegistration.cs'"
    powershell -Command "(Get-Content 'ServiceRegistration.cs') -replace 'UseNpgsql', '%1' | Set-Content 'ServiceRegistration.cs'"
    powershell -Command "(Get-Content 'ServiceRegistration.cs') -replace 'UseOracle', '%1' | Set-Content 'ServiceRegistration.cs'"
    powershell -Command "(Get-Content 'ServiceRegistration.cs') -replace 'UseMySql', '%1' | Set-Content 'ServiceRegistration.cs'"
    powershell -Command "(Get-Content 'ServiceRegistration.cs') -replace 'UseSqlite', '%1' | Set-Content 'ServiceRegistration.cs'"

    echo Configuration for %1 complete.
    popd

goto :EOF
