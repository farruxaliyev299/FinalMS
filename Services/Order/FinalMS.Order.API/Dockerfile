FROM mcr.microsoft.com/dotnet/sdk:7.0-bullseye-slim as build
WORKDIR /app
EXPOSE 80
COPY Shared/*.csproj Shared/

COPY Services/Order/FinalMS.Order.Application/*.csproj Services/Order/FinalMS.Order.Application/

COPY Services/Order/FinalMS.Order.Domain/*.csproj Services/Order/FinalMS.Order.Domain/

COPY Services/Order/FinalMS.Order.Domain.Core/*.csproj Services/Order/FinalMS.Order.Domain.Core/

COPY Services/Order/FinalMS.Order.Infrastructure/*.csproj Services/Order/FinalMS.Order.Infrastructure/

COPY Services/Order/FinalMS.Order.API/*.csproj Services/Order/FinalMS.Order.API/

RUN dotnet restore Services/Order/FinalMS.Order.API/*.csproj
COPY . .
RUN dotnet publish Services/Order/FinalMS.Order.API/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:7.0-bullseye-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","FinalMS.Order.API.dll" ]
