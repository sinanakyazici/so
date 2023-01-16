#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

RUN ln -sf /usr/share/zoneinfo/Europe/Istanbul /etc/localtime 
RUN date

WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/ApiGateway/SO.ApiGateway/SO.ApiGateway.csproj", "src/ApiGateway/SO.ApiGateway/"]
RUN dotnet restore "src/ApiGateway/SO.ApiGateway/SO.ApiGateway.csproj"
COPY . .
WORKDIR "/src/src/ApiGateway/SO.ApiGateway"
RUN dotnet build "SO.ApiGateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SO.ApiGateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SO.ApiGateway.dll"]