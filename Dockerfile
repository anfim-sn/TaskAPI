FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TaskAPI/TaskAPI.Api.csproj", "TaskAPI/"]
COPY ["TaskAPI.Core/TaskAPI.Core.csproj", "TaskAPI.Core/"]
COPY ["TaskAPI.Infrastructure/TaskAPI.Infrastructure.csproj", "TaskAPI.Infrastructure/"]
RUN dotnet restore "TaskAPI/TaskAPI.Api.csproj"
COPY . .
WORKDIR "/src/TaskAPI"
RUN dotnet build "TaskAPI.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaskAPI.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaskAPI.Api.dll"]