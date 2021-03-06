# Microservices sample

A small example for demo the microservice architecture.

## Techs

1. Asp Net Core
2. Docker + Docker Compose

### Steps
1. Create the services as simple as posible. (Add + Multiple Services)
2. Create Docker file for each service
3. Create Docker Compose file + set port for each service (project property => debug => ...)
4. [CRITICAL] without the override.xml, it won't public the port of container (the port in dockerfile of service is just expose when using it alone, isn't it?). So adding another docker-compose.override.yml for overriding the port + config
5. Add Ocelot. [CRITICAL] Beware of the  "DownstreamScheme": "http"
    - test by using "DownstreamPathTemplate": "/{everything}",
    - [CRITICAL] wrapping a try-catch in Program.cs => main for viewing the error of configuration of Ocelot
6. Ocelot in Prod use internal port.
7. Add RabbitMq : Remember set the port in both internal & external
    - 192.168.99.100:8080 : for the management 
    - pass : guest, user name : guest
    - script:
        https://stackoverflow.com/questions/28339116/not-allowed-to-load-assembly-from-network-location
    -  .\SetupRabbitMQ.bat
8. Ping in Docker
    - docker exec -it dockerContainerid bash
    - apt-get update && apt-get install -y iputils-ping
9. Clean Volume:
    docker system prune --volumes
    docker container rm cc3f2ff51cab

### Installing

- to be continue