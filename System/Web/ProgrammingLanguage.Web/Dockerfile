FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["System/Web/ProgrammingLanguage.Web/ProgrammingLanguage.Web.csproj", "System/Web/ProgrammingLanguage.Web/ProgrammingLanguage.Web.csproj"]
RUN dotnet restore "System/Web/ProgrammingLanguage.Web/ProgrammingLanguage.Web.csproj"
COPY . .
WORKDIR "/src/System/Web/ProgrammingLanguage.Web"
RUN dotnet build "ProgrammingLanguage.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ProgrammingLanguage.Web.csproj" -c Release -o /app/publish

FROM nginx:alpine
WORKDIR /var/www/web

COPY --from=publish /app/publish/wwwroot .
COPY System/Web/ProgrammingLanguage.Web/nginx.conf /etc/nginx/nginx.conf
EXPOSE 80