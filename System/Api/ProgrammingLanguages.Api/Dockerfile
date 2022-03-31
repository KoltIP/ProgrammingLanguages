#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["System/Api/ProgrammingLanguages.Api/ProgrammingLanguages.Api.csproj", "System/Api/ProgrammingLanguages.Api/"]
COPY ["Services/ProgrammingLanguages.Settings/ProgrammingLanguages.Settings.csproj", "Services/ProgrammingLanguages.Settings/"]
COPY ["Shared/ProgrammingLanguages.Shared.Common/ProgrammingLanguages.Shared.Common.csproj", "Shared/ProgrammingLanguages.Shared.Common/"]
COPY ["Shared/ProgrammingLanguages.Shared.Settings/ProgrammingLanguages.Shared.Settings.csproj", "Shared/ProgrammingLanguages.Shared.Settings/"]
RUN dotnet restore "System/Api/ProgrammingLanguages.Api/ProgrammingLanguages.Api.csproj"
COPY . .
WORKDIR "/src/System/Api/ProgrammingLanguages.Api"
RUN dotnet build "ProgrammingLanguages.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProgrammingLanguages.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProgrammingLanguages.Api.dll"]