#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["NetCoreWebApiBoilerPlate/NetCoreWebApiBoilerPlate.csproj", "NetCoreWebApiBoilerPlate/"]
COPY ["NetCoreWebApiBoilerPlate.Data/NetCoreWebApiBoilerPlate.Data.csproj", "NetCoreWebApiBoilerPlate.Data/"]
COPY ["NetCoreWebApiBoilerPlate.Domain/NetCoreWebApiBoilerPlate.Domain.csproj", "NetCoreWebApiBoilerPlate.Domain/"]
RUN dotnet restore "NetCoreWebApiBoilerPlate/NetCoreWebApiBoilerPlate.csproj"
COPY . .
WORKDIR "/src/NetCoreWebApiBoilerPlate"
RUN dotnet build "NetCoreWebApiBoilerPlate.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NetCoreWebApiBoilerPlate.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NetCoreWebApiBoilerPlate.dll"]