#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["StealTheCats/StealTheCats.csproj", "StealTheCats/"]
COPY ["CatsLibrary/CatsLibrary.Extensions/CatsLibrary.Extensions.csproj", "CatsLibrary/CatsLibrary.Extensions/"]
COPY ["CatsLibrary/CatsLibrary.Controller/CatsLibrary.Controller.csproj", "CatsLibrary/CatsLibrary.Controller/"]
COPY ["CatsLibrary/CatsLibrary.Interface/CatsLibrary.Interface.csproj", "CatsLibrary/CatsLibrary.Interface/"]
COPY ["DatabaseContext.DBHelper/DatabaseContext.DBHelper.csproj", "DatabaseContext.DBHelper/"]
COPY ["CatsLibrary/CatsLibrary.Services/CatsLibrary.Services.csproj", "CatsLibrary/CatsLibrary.Services/"]
COPY ["Extensions.HttpClient/Extensions.HttpClient.csproj", "Extensions.HttpClient/"]
RUN dotnet restore "./StealTheCats/StealTheCats.csproj"
COPY . .
WORKDIR "/src/StealTheCats"
RUN dotnet build "./StealTheCats.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./StealTheCats.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StealTheCats.dll"]