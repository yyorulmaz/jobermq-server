#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/JoberMQServerWeb/JoberMQServerWeb.csproj", "src/JoberMQServerWeb/"]
RUN dotnet restore "src/JoberMQServerWeb/JoberMQServerWeb.csproj"
COPY . .
WORKDIR "/src/src/JoberMQServerWeb"
RUN dotnet build "JoberMQServerWeb.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "JoberMQServerWeb.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY bin/Release/net7.0/publish /app/
ENTRYPOINT ["dotnet", "JoberMQServerWeb.dll"]