FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["pokedex-back/pokedex-back.csproj", "pokedex-back/"]
RUN dotnet restore "pokedex-back/pokedex-back.csproj"
COPY . .
WORKDIR "/src/pokedex-back"
RUN dotnet build "pokedex-back.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "pokedex-back.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "pokedex-back.dll"]
