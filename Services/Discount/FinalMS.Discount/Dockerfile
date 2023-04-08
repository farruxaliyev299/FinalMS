FROM mcr.microsoft.com/dotnet/sdk:7.0-bullseye-slim as build
WORKDIR /app
EXPOSE 80
COPY Shared/*.csproj Shared/
COPY Services/Discount/FinalMS.Discount/*.csproj Services/Discount/FinalMS.Discount/
RUN dotnet restore Services/Discount/FinalMS.Discount/*.csproj
COPY . .
RUN dotnet publish Services/Discount/FinalMS.Discount/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:7.0-bullseye-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","FinalMS.Discount.dll" ]
