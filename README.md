# dotnet-docker-playground

[![Try in PWD](https://raw.githubusercontent.com/play-with-docker/stacks/master/assets/images/button.png)](https://labs.play-with-docker.com/?stack=https://raw.githubusercontent.com/stvansolano/dotnet-docker-playground/main/docker-compose.yml)

## Commands

### 1. Run in Docker

      docker run -d -p 5000:5000 -p 5001:5001 -p 80:80 -p 8080:8080 stvansolano/dotnet-codespace:3.1

      docker ps

      docker exec -it <NAME> bash
      
      cd home

      dotnet --version

      dotnet new mvc --name Frontend

      cd Frontend

      dotnet run --urls="http://+:5000"

### 2. Build & run

      git clone https://github.com/stvansolano/dotnet-docker-playground.git

      cd dotnet-docker-playground/.devcontainer
      
      docker build -t local-dev:latest -f Dockerfile . 

      docker run -d -p 5000:5000 -p 5001:5001 -p 80:80 -p 8080:8080 local-dev:latest

