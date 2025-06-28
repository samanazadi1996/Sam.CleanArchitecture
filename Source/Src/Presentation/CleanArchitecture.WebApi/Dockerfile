# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Src/Presentation/CleanArchitecture.WebApi/CleanArchitecture.WebApi.csproj", "Src/Presentation/CleanArchitecture.WebApi/"]
COPY ["Src/Core/CleanArchitecture.Application/CleanArchitecture.Application.csproj", "Src/Core/CleanArchitecture.Application/"]
COPY ["Src/Core/CleanArchitecture.Domain/CleanArchitecture.Domain.csproj", "Src/Core/CleanArchitecture.Domain/"]
COPY ["Src/Infrastructure/CleanArchitecture.Infrastructure.Identity/CleanArchitecture.Infrastructure.Identity.csproj", "Src/Infrastructure/CleanArchitecture.Infrastructure.Identity/"]
COPY ["Src/Infrastructure/CleanArchitecture.Infrastructure.Persistence/CleanArchitecture.Infrastructure.Persistence.csproj", "Src/Infrastructure/CleanArchitecture.Infrastructure.Persistence/"]
COPY ["Src/Infrastructure/CleanArchitecture.Infrastructure.Resources/CleanArchitecture.Infrastructure.Resources.csproj", "Src/Infrastructure/CleanArchitecture.Infrastructure.Resources/"]
RUN dotnet restore "./Src/Presentation/CleanArchitecture.WebApi/CleanArchitecture.WebApi.csproj"
COPY . .
WORKDIR "/src/Src/Presentation/CleanArchitecture.WebApi"
RUN dotnet build "./CleanArchitecture.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CleanArchitecture.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CleanArchitecture.WebApi.dll"]