FROM node:22 as frontend
WORKDIR /frontend
COPY ../../../frontend/client/package*.json .
RUN npm install
COPY ../../../frontend/client .
RUN npm run build

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS buildenv
WORKDIR /reactivities
COPY ../../../backend .
RUN dotnet restore ./WebApp/WebApp.csproj
RUN dotnet publish ./WebApp/WebApp.csproj --configuration Release --output release

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=buildenv /reactivities/release .
COPY --from=frontend /backend/webapp/wwwroot ./wwwroot
EXPOSE 8080
EXPOSE 443
# An ENTRYPOINT allows you to configure a container that will run as an executable.
ENTRYPOINT ["dotnet","WebApp.dll"]

#run from parent directory of backend and front end
#docker build --progress=plain -t reactivities1 -f ./backend/WebApp/Dockerfile .