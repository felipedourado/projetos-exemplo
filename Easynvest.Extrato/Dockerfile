FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app
COPY *.csproj ./
RUN dotnet restore
COPY . ./
RUN dotnet publish -c Release -o out

# Build image runtime!
FROM mcr.microsoft.com/dotnet/core/runtime:3.1
MAINTAINER felipedourado
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["./dotnetapp"]
