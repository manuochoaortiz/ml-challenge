FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 as base
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1  as build
WORKDIR /
COPY . .
RUN dotnet publish IpTracker.Api/IpTracker.Api.csproj -c Release -o /app  --disable-parallel
WORKDIR /app
ENTRYPOINT ["dotnet", "IpTracker.Api.dll"]

FROM base as final
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "IpTracker.Api.dll"]