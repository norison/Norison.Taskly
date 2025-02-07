FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["src/back-end/todos", "."]
RUN dotnet restore "Norison.Taskly.Todos.Api/Norison.Taskly.Todos.Api.csproj"
RUN dotnet publish -o out "Norison.Taskly.Todos.Api/Norison.Taskly.Todos.Api.csproj"

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /src/out ./
ENTRYPOINT ["dotnet", "Norison.Taskly.Todos.Api.dll"]