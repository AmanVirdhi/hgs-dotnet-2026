FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy only project files first (better caching)
COPY HgsApi/*.csproj ./HgsApi/
RUN dotnet restore ./HgsApi/HgsApi.csproj

# Copy remaining code
COPY HgsApi/. ./HgsApi/
WORKDIR /src/HgsApi

RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "HgsApi.dll"]