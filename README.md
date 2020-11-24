# dotnet-docker-playground

[![Try in PWD](https://raw.githubusercontent.com/play-with-docker/stacks/master/assets/images/button.png)](https://labs.play-with-docker.com/?stack=https://raw.githubusercontent.com/stvansolano/dotnet-docker-playground/main/docker-compose.yml)

# Commands

      git clone https://github.com/stvansolano/dotnet-docker-playground.gi

      cd dotnet-docker-playground/.devcontainer
      
      docker build -t dev:latest -f Dockerfile .
      
      docker run -p 80:5001 -t dev:latest -d
