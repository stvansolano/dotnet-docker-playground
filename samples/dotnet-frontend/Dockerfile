FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build-env

COPY ["samples/dotnet-frontend/DotNet-Frontend.csproj", "samples/dotnet-frontend/"]
RUN dotnet restore "samples/dotnet-frontend/DotNet-Frontend.csproj"
COPY "samples/dotnet-frontend/" "samples/dotnet-frontend/"
RUN dotnet build "samples/dotnet-frontend/DotNet-Frontend.csproj" -c Release -o /build

FROM build-env AS publish
RUN dotnet publish "samples/dotnet-frontend/DotNet-Frontend.csproj" -c Release -o /publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /publish/wwwroot /usr/local/webapp/nginx/html
COPY --from=build-env "samples/dotnet-frontend/ngnix.conf" /etc/nginx/nginx.conf
EXPOSE 80