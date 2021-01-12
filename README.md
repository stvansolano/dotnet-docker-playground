# dotnet-docker-playground

1) Play with Docker
[![Try in PWD](https://raw.githubusercontent.com/play-with-docker/stacks/master/assets/images/button.png)](https://labs.play-with-docker.com/?stack=https://raw.githubusercontent.com/stvansolano/dotnet-docker-playground/main/docker-compose.yml)

2) .NET Playground
[![Try in PWD](https://raw.githubusercontent.com/play-with-docker/stacks/master/assets/images/button.png)](https://labs.play-with-docker.com/?stack=https://raw.githubusercontent.com/stvansolano/dotnet-docker-playground/main/docker-compose.yml)

3) Data Playground
[![Try in PWD](https://raw.githubusercontent.com/play-with-docker/stacks/master/assets/images/button.png)](https://labs.play-with-docker.com/?stack=https://raw.githubusercontent.com/stvansolano/dotnet-docker-playground/main/data-playground.yml)

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
      
      docker build -t playground:latest -f Dockerfile . 

      docker run -d -p 5000:5000 -p 5001:5001 -p 80:80 -p 8080:8080 -v $(pwd)/samples:/src playground:latest

### Linux/SQL Server commands 
cat /etc/*release
export PATH="$PATH:/opt/mssql-tools/bin"
sqlcmd -S localhost -U SA -P Password.123
select getdate();