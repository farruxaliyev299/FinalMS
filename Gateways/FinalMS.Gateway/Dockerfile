FROM mcr.microsoft.com/dotnet/sdk:7.0-bullseye-slim as build
WORKDIR /app
EXPOSE 80
COPY Gateways/FinalMS.Gateway/*.csproj Gateways/FinalMS.Gateway/
RUN dotnet restore Gateways/FinalMS.Gateway/*.csproj
COPY . .
RUN dotnet publish Gateways/FinalMS.Gateway/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:7.0-bullseye-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","FinalMS.Gateway.dll" ]
