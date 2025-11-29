FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER root
WORKDIR /app
EXPOSE 8081

RUN apt-get update && \
    apt-get install -y ffmpeg && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/*

USER app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ProcessFileService.csproj", "."]
RUN dotnet restore "./ProcessFileService.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./ProcessFileService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ProcessFileService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app

USER root
RUN apt-get update && apt-get install -y curl unzip && rm -rf /var/lib/apt/lists/*

RUN curl -sSfL https://github.com/open-telemetry/opentelemetry-dotnet-instrumentation/releases/latest/download/otel-dotnet-auto-install.sh -O \
    && sh ./otel-dotnet-auto-install.sh \
    && chmod +x $HOME/.otel-dotnet-auto/instrument.sh

COPY --from=publish /app/publish .
ENTRYPOINT ["/bin/sh", "-c", ". $HOME/.otel-dotnet-auto/instrument.sh && OTEL_SERVICE_NAME=ProcessFileService OTEL_RESOURCE_ATTRIBUTES=deployment.environment=staging,service.version=1.0.0 dotnet ProcessFileService.dll"]