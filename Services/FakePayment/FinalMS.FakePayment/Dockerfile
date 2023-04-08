FROM mcr.microsoft.com/dotnet/sdk:7.0-bullseye-slim as build
WORKDIR /app
EXPOSE 80
COPY Shared/*.csproj Shared/
COPY Services/FakePayment/FinalMS.FakePayment/*.csproj Services/FakePayment/FinalMS.FakePayment/
RUN dotnet restore Services/FakePayment/FinalMS.FakePayment/*.csproj
COPY . .
RUN dotnet publish Services/FakePayment/FinalMS.FakePayment/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:7.0-bullseye-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","FinalMS.FakePayment.dll" ]
