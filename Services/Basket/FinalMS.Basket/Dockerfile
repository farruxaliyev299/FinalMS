FROM mcr.microsoft.com/dotnet/sdk:7.0-bullseye-slim as build
WORKDIR /app
EXPOSE 80
COPY Shared/*.csproj Shared/
COPY Services/Basket/FinalMS.Basket/*.csproj Services/Basket/FinalMS.Basket/
RUN dotnet restore Services/Basket/FinalMS.Basket/*.csproj
COPY . .
RUN dotnet publish Services/Basket/FinalMS.Basket/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:7.0-bullseye-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","FinalMS.Basket.dll" ]
