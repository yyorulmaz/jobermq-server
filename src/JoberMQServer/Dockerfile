#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/JoberMQServer/JoberMQServer.csproj", "src/JoberMQServer/"]
COPY ["src/JoberMQ/JoberMQ.csproj", "src/JoberMQ/"]
RUN dotnet restore "src/JoberMQServer/JoberMQServer.csproj"
COPY . .
WORKDIR "/src/src/JoberMQServer"
RUN dotnet build "JoberMQServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JoberMQServer.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY bin/Release/net7.0/publish /app/
ENTRYPOINT ["dotnet", "JoberMQServer.dll"]