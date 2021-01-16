FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["samples/dotnet-webserver/Dotnet-WebServer.csproj", "samples/dotnet-webserver/"]
RUN dotnet restore "samples/dotnet-webserver/Dotnet-WebServer.csproj"
COPY . .
WORKDIR "/src/samples/dotnet-webserver"
RUN dotnet build "Dotnet-WebServer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dotnet-WebServer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dotnet-WebServer.dll"]
