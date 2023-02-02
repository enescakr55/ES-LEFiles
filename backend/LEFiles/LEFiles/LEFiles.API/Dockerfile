#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["LEFiles.API/LEFiles.API.csproj", "LEFiles.API/"]
RUN dotnet restore "LEFiles.API/LEFiles.API.csproj"
COPY . .
WORKDIR "/src/LEFiles.API"
RUN dotnet build "LEFiles.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LEFiles.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LEFiles.API.dll"]