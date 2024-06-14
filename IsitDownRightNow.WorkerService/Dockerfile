FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["IsitDownRightNow.WorkerService/IsitDownRightNow.WorkerService.csproj", "IsitDownRightNow.WorkerService/"]
RUN dotnet restore "IsitDownRightNow.WorkerService/IsitDownRightNow.WorkerService.csproj"
COPY . .
WORKDIR "/src/IsitDownRightNow.WorkerService"
RUN dotnet build "IsitDownRightNow.WorkerService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "IsitDownRightNow.WorkerService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IsitDownRightNow.WorkerService.dll"]
