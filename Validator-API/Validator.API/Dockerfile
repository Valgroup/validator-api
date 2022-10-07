FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /

COPY . ./

RUN dotnet restore

RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /
COPY --from=build-env /out .
ENTRYPOINT ["dotnet", "Validator.API.dll"]