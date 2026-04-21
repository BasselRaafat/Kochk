FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Kochk.API/Kochk.API.csproj", "./Kochk.API/"]
COPY ["src/Kochk.Domain/Kochk.Domain.csproj", "./Kochk.Domain/"]
COPY ["src/Kochk.Infrastructure/Kochk.Infrastructure.csproj", "./Kochk.Infrastructure/"]
COPY ["src/Kochk.Application/Kochk.Application.csproj", "./Kochk.Application/"]
RUN dotnet restore "src/Kochk.API/Kochk.API.csproj"
COPY . .
WORKDIR "/src/Kochk.API"
RUN dotnet build "./Kochk.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Kochk.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kochk.API.dll"]
