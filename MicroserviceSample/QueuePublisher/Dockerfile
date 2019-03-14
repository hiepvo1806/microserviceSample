FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["QueuePublisher/QueuePublisher.csproj", "QueuePublisher/"]
RUN dotnet restore "QueuePublisher/QueuePublisher.csproj"
COPY . .
WORKDIR "/src/QueuePublisher"
RUN dotnet build "QueuePublisher.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "QueuePublisher.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "QueuePublisher.dll"]