FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /workspace
COPY DisasterReliefCoordination.sln Directory.Build.props ./
COPY src/Core/DisasterRelief.Domain/DisasterRelief.Domain.csproj src/Core/DisasterRelief.Domain/
COPY src/Core/DisasterRelief.Application/DisasterRelief.Application.csproj src/Core/DisasterRelief.Application/
COPY src/Infrastructure/DisasterRelief.Infrastructure/DisasterRelief.Infrastructure.csproj src/Infrastructure/DisasterRelief.Infrastructure/
COPY src/Presentation/DisasterRelief.Api/DisasterRelief.Api.csproj src/Presentation/DisasterRelief.Api/
RUN dotnet restore DisasterReliefCoordination.sln
COPY . .
RUN dotnet publish src/Presentation/DisasterRelief.Api/DisasterRelief.Api.csproj --configuration Release --output /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "DisasterRelief.Api.dll"]