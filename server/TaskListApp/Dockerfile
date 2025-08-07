FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 10000
ENV ASPNETCORE_URLS=http://0.0.0.0:10000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar arquivos de projeto
COPY ["TaskList.API/TaskList.API.csproj", "TaskList.API/"]
COPY ["TaskList.Application/TaskList.Application.csproj", "TaskList.Application/"]
COPY ["TaskList.Domain/TaskList.Domain.csproj", "TaskList.Domain/"]
COPY ["TaskList.Infrastructure/TaskList.Infrastructure.csproj", "TaskList.Infrastructure/"]

# Restore dependências
RUN dotnet restore "TaskList.API/TaskList.API.csproj"

# Copiar todo o código
COPY . .
WORKDIR "/src/TaskList.API"

# Build
RUN dotnet build "TaskList.API.csproj" -c Release -o /app/build

# Publish
FROM build AS publish
RUN dotnet publish "TaskList.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Configurar para Render
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "TaskList.API.dll"]