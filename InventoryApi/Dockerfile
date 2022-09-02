#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["InventoryApi/InventoryApi.csproj", "InventoryApi/"]
RUN dotnet restore "InventoryApi/InventoryApi.csproj"
COPY . .
WORKDIR "/src/InventoryApi"
RUN dotnet build "InventoryApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "InventoryApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InventoryApi.dll"]