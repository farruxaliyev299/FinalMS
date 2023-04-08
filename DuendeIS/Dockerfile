FROM mcr.microsoft.com/dotnet/sdk:6.0 as build
WORKDIR /app
EXPOSE 80
COPY Shared/*.csproj Shared/
COPY DuendeIS/*.csproj DuendeIS/
RUN dotnet restore DuendeIS/*.csproj
COPY . .
RUN dotnet publish DuendeIS/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:6.0 as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","FinalMS.DuendeIS.dll" ]
