#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["System/Identity/ProgrammingLanguages.Identity/ProgrammingLanguages.Identity.csproj", "System/Identity/ProgrammingLanguages.Identity/"]
COPY ["Services/ProgrammingLanguages.Settings/ProgrammingLanguages.Settings.csproj", "Services/ProgrammingLanguages.Settings/"]
COPY ["Data/ProgrammingLanguages.Db.Contex/ProgrammingLanguages.Db.Contex.csproj", "Data/ProgrammingLanguages.Db.Contex/"]
COPY ["Data/ProgrammingLanguages.Db.Entities/ProgrammingLanguages.Db.Entities.csproj", "Data/ProgrammingLanguages.Db.Entities/"]
COPY ["Shared/ProgrammingLanguages.Shared.Common/ProgrammingLanguages.Shared.Common.csproj", "Shared/ProgrammingLanguages.Shared.Common/"]
RUN dotnet restore "System/Identity/ProgrammingLanguages.Identity/ProgrammingLanguages.Identity.csproj"
COPY . .
WORKDIR "/src/System/Identity/ProgrammingLanguages.Identity"
RUN dotnet build "ProgrammingLanguages.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProgrammingLanguages.Identity.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProgrammingLanguages.Identity.dll"]