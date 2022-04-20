#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["System/Worker/ProgrammingLanguages.Worker/ProgrammingLanguages.Worker.csproj", "System/Worker/ProgrammingLanguages.Worker/"]
COPY ["Data/ProgrammingLanguages.Db.Contex/ProgrammingLanguages.Db.Contex.csproj", "Data/ProgrammingLanguages.Db.Contex/"]
COPY ["Data/ProgrammingLanguages.Db.Entities/ProgrammingLanguages.Db.Entities.csproj", "Data/ProgrammingLanguages.Db.Entities/"]
COPY ["Services/ProgrammingLanguages.RabbitMqService/ProgrammingLanguages.RabbitMqService.csproj", "Services/ProgrammingLanguages.RabbitMqService/"]
COPY ["Services/ProgrammingLanguages.Settings/ProgrammingLanguages.Settings.csproj", "Services/ProgrammingLanguages.Settings/"]
COPY ["Services/ProgrammingLanguages.EmailService/ProgrammingLanguages.EmailService.csproj", "Services/ProgrammingLanguages.EmailService/"]
RUN dotnet restore "System/Worker/ProgrammingLanguages.Worker/ProgrammingLanguages.Worker.csproj"
COPY . .
WORKDIR "/src/System/Worker/ProgrammingLanguages.Worker"
RUN dotnet build "ProgrammingLanguages.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProgrammingLanguages.Worker.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ProgrammingLanguages.Worker.dll"]