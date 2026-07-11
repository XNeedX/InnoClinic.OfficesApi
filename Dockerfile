FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["OfficesAPI.Presentation/OfficesAPI.Presentation.csproj", "OfficesAPI.Presentation/"]
COPY ["OfficesAPI.Application/OfficesAPI.Application.csproj", "OfficesAPI.Application/"]
COPY ["OfficesAPI.Infrastructure/OfficesAPI.Infrastructure.csproj", "OfficesAPI.Infrastructure/"]
COPY ["OfficesAPI.Domain/OfficesAPI.Domain.csproj", "OfficesAPI.Domain/"]

RUN dotnet restore "./OfficesAPI.Presentation/OfficesAPI.Presentation.csproj"

COPY . .
WORKDIR "/src/OfficesAPI.Presentation"
RUN dotnet build "./OfficesAPI.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./OfficesAPI.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OfficesAPI.Presentation.dll"]