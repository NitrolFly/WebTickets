FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WebTickets.API/WebTickets.API.csproj", "WebTickets.API/"]
COPY ["src/WebTickets.Application/WebTickets.Application.csproj", "src/WebTickets.Application/"]
COPY ["src/WebTickets.Domain/WebTickets.Domain.csproj", "src/WebTickets.Domain/"]
COPY ["src/WebTickets.Infrastructure/WebTickets.Infrastructure.csproj", "src/WebTickets.Infrastructure/"]
RUN dotnet restore "WebTickets.API/WebTickets.API.csproj"
COPY . .
WORKDIR "/src/WebTickets.API"
RUN dotnet build "WebTickets.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "WebTickets.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebTickets.API.dll"]
