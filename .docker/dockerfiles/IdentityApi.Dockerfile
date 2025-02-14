FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["src/back-end/identity", "."]
RUN dotnet restore "Norison.Taskly.Identity.Api/Norison.Taskly.Identity.Api.csproj"
RUN dotnet publish -o out "Norison.Taskly.Identity.Api/Norison.Taskly.Identity.Api.csproj"

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /src/out ./
ENTRYPOINT ["dotnet", "Norison.Taskly.Identity.Api.dll"]