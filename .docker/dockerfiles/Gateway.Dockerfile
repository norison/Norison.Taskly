FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["src/back-end/gateway", "."]
RUN dotnet restore "Norison.Taskly.Gateway/Norison.Taskly.Gateway.csproj"
RUN dotnet publish -o out "Norison.Taskly.Gateway/Norison.Taskly.Gateway.csproj"

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /src/out ./
ENTRYPOINT ["dotnet", "Norison.Taskly.Gateway.dll"]