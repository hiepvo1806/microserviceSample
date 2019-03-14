FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["QueueListener/QueueListener.csproj", "QueueListener/"]
RUN dotnet restore "QueueListener/QueueListener.csproj"
COPY . .
WORKDIR "/src/QueueListener"
RUN dotnet build "QueueListener.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "QueueListener.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "QueueListener.dll"]