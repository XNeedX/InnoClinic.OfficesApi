FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine AS base
WORKDIR /app
EXPOSE 8080

USER root
RUN apk add --no-cache icu-libs
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false
USER app

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION=Release

ARG GITHUB_USERNAME
ARG GITHUB_PASSWORD
ENV GITHUB_USERNAME=$GITHUB_USERNAME
ENV GITHUB_PASSWORD=$GITHUB_PASSWORD

WORKDIR /src

COPY ["Offices.Presentation/Offices.Presentation.csproj", "Offices.Presentation/"]
COPY ["Offices.Application/Offices.Application.csproj", "Offices.Application/"]
COPY ["Offices.Infrastructure/Offices.Infrastructure.csproj", "Offices.Infrastructure/"]
COPY ["Offices.Domain/Offices.Domain.csproj", "Offices.Domain/"]
COPY ["nuget.config", "./"]

RUN dotnet nuget update source "github" \
    --username "$GITHUB_USERNAME" \
    --password "$GITHUB_PASSWORD" \
    --store-password-in-clear-text \
    --configfile nuget.config

RUN dotnet restore "./Offices.Presentation/Offices.Presentation.csproj"

COPY . .
WORKDIR "/src/Offices.Presentation"
RUN dotnet build "./Offices.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Offices.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Offices.Presentation.dll"]