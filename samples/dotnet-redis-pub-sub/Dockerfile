FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["samples/dotnet-redis-pub-sub/Dotnet-Backend.csproj", "samples/dotnet-redis-pub-sub/"]
RUN dotnet restore "samples/dotnet-redis-pub-sub/Dotnet-Backend.csproj"
COPY . .
WORKDIR "/src/samples/dotnet-backend"
RUN dotnet build "Dotnet-Backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dotnet-Backend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dotnet-Backend.dll"]

# docker build -t dotnet-redis -f "samples/dotnet-redis-pub-sub/Dockerfile" .
# docker exec -it [CONTAINER-ID] redis-cli 
# >https://redis.io/topics/rediscli
# incr mycounter