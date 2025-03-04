FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WeatherApi.csproj", "."]
RUN dotnet restore "./WeatherApi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./WeatherApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WeatherApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

USER root
RUN apt-get update && apt-get install -y curl unzip && rm -rf /var/lib/apt/lists/*

RUN curl -sSfL https://github.com/open-telemetry/opentelemetry-dotnet-instrumentation/releases/latest/download/otel-dotnet-auto-install.sh -O \
    && sh ./otel-dotnet-auto-install.sh \
    && chmod +x $HOME/.otel-dotnet-auto/instrument.sh

COPY --from=publish /app/publish .

ENTRYPOINT ["/bin/sh", "-c", ". $HOME/.otel-dotnet-auto/instrument.sh && OTEL_SERVICE_NAME=WeatherApi OTEL_RESOURCE_ATTRIBUTES=deployment.environment=staging,service.version=1.0.0 dotnet WeatherApi.dll"]
