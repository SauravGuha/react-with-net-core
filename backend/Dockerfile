FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /app
COPY . /app/backend
RUN dotnet restore backend/backend.sln
RUN dotnet publish backend/WebApp/WebApp.csproj -c Release -o /app/release --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/release .
EXPOSE 8080
EXPOSE 443
ENTRYPOINT [ "dotnet", "WebApp.dll" ]

