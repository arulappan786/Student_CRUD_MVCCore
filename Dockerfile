# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY StudentMVCCoreWb/StudentMVCCoreWb.csproj StudentMVCCoreWb/
RUN dotnet restore StudentMVCCoreWb/StudentMVCCoreWb.csproj

COPY . ./
WORKDIR /src/StudentMVCCoreWb
RUN dotnet publish -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 80
ENTRYPOINT ["dotnet", "StudentMVCCoreWb.dll"]