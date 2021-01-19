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

### 3. Data playground

      git clone https://github.com/stvansolano/dotnet-docker-playground.git

      cd ~/dotnet-docker-playground/
      
      docker-compose -f data-playground.yml up -d 

      clear

      docker ps

      docker exec -it [container-id] bash

- SQL Server connection string:

```
Server=YOUR-IP,1433;Database=AdventureWorks;Integrated Security=False;User Id=sa;Password=YOUR-PASSWORD;MultipleActiveResultSets=True
```
> Linux/SQL Server commands:
      ```
      cat /etc/*release
      export PATH="$PATH:/opt/mssql-tools/bin"
      sqlcmd -S localhost -U SA -P Password.123
      select getdate();
      ```

## Docker commands

```
docker ps
docker stop
docker logs
```

Build & publish 

```
      1. Run 
            docker login --username stvansolano
      2. Pasted obtained secret
            71d70601-4232-4d76-a00e-c8f05958098b
      3. Build the local image
            cd samples/sql-server
            docker build --rm -t stvansolano/adventure-works:latest .
      4. Push to Docker Hub
            docker push stvansolano/adventure-works:latest
      5. Delete from your Docker Hub account
```

### Run it from Code-Servrer

1) Run the container

```
docker-compose -f data-playground.yml up -d 
```

or

```
docker run --name code-server \
  -p 8080:8080 \
  -p 8443:8443 \
  -v "$PWD/samples/:/~/root/samples" \
  -u "$(id -u):$(id -g)" \
  -e "DOCKER_USER=$USER" \
  -d \
  stvansolano/code-server:latest
```

2) Attach bash
`docker exec -it code-server bash`

3) Get the password and run it!

`cat ~/.config/code-server/config.yaml`

Optionally, build the image

`docker build -f ".devcontainer/code-server.Dockerfile" -t stvansolano/code-server-docker:latest .`

## Queries / Requests

      ```
      curl -o - "http://localhost:5000/api/hello"

      curl -o - "http://localhost:5000/api/products"

      curl -o - "http://localhost:5000/api/{topic}"
      ```

## Linux processes

      ```
      ps aux
      killall dotnet
      ```