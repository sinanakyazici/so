#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

RUN ln -sf /usr/share/zoneinfo/Europe/Istanbul /etc/localtime 
RUN date

WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/OrderService/SO.OrderService/SO.OrderService.csproj", "src/OrderService/SO.OrderService/"]
COPY ["src/BuildingBlocks/SO.Application/SO.Application.csproj", "src/BuildingBlocks/SO.Application/"]
COPY ["src/BuildingBlocks/SO.Domain/SO.Domain.csproj", "src/BuildingBlocks/SO.Domain/"]
COPY ["src/BuildingBlocks/SO.Infrastructure.EventBus/SO.Infrastructure.EventBus.csproj", "src/BuildingBlocks/SO.Infrastructure.EventBus/"]
COPY ["src/BuildingBlocks/SO.Infrastructure.Data/SO.Infrastructure.Data.csproj", "src/BuildingBlocks/SO.Infrastructure.Data/"]
COPY ["src/BuildingBlocks/SO.Infrastructure.Logger/SO.Infrastructure.Logger.csproj", "src/BuildingBlocks/SO.Infrastructure.Logger/"]
RUN dotnet restore "src/OrderService/SO.OrderService/SO.OrderService.csproj"
COPY . .
WORKDIR "/src/src/OrderService/SO.OrderService"
RUN dotnet build "SO.OrderService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SO.OrderService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SO.OrderService.dll"]