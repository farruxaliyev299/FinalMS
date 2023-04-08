FROM mcr.microsoft.com/dotnet/sdk:7.0-bullseye-slim as build
WORKDIR /app
EXPOSE 80
COPY Shared/*.csproj Shared/
COPY Services/PhotoStock/FinalMS.PhotoStock/*.csproj Services/PhotoStock/FinalMS.PhotoStock/
RUN dotnet restore Services/PhotoStock/FinalMS.PhotoStock/*.csproj
COPY . .
RUN dotnet publish Services/PhotoStock/FinalMS.PhotoStock/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:7.0-bullseye-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","FinalMS.PhotoStock.dll" ]
