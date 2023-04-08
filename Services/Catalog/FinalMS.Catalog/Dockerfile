FROM mcr.microsoft.com/dotnet/sdk:7.0-bullseye-slim as build
WORKDIR /app
EXPOSE 80
COPY Shared/*.csproj Shared/
COPY Services/Catalog/FinalMS.Catalog/*.csproj Services/Catalog/FinalMS.Catalog/
RUN dotnet restore Services/Catalog/FinalMS.Catalog/*.csproj
COPY . .
RUN dotnet publish Services/Catalog/FinalMS.Catalog/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:7.0-bullseye-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","FinalMS.Catalog.dll" ]
