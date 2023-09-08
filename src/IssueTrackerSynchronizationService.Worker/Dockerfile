#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/IssueTrackerSynchronizationService.Worker/IssueTrackerSynchronizationService.Worker.csproj", "src/IssueTrackerSynchronizationService.Worker/"]
RUN dotnet restore "src/IssueTrackerSynchronizationService.Worker/IssueTrackerSynchronizationService.Worker.csproj"
COPY . .
WORKDIR "/src/src/IssueTrackerSynchronizationService.Worker"
RUN dotnet build "IssueTrackerSynchronizationService.Worker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IssueTrackerSynchronizationService.Worker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IssueTrackerSynchronizationService.Worker.dll"]