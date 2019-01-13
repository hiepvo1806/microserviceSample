FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["MultipleService/MultipleService.csproj", "MultipleService/"]
RUN dotnet restore "MultipleService/MultipleService.csproj"
COPY . .
WORKDIR "/src/MultipleService"
RUN dotnet build "MultipleService.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "MultipleService.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "MultipleService.dll"]