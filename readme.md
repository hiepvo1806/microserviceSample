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

### Installing

- to be continue