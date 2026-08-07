# =========================
# Stage 1: Build
# =========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# Copy project file first
COPY MyApi.csproj ./

# Restore NuGet dependencies
RUN dotnet restore

# Copy remaining source code
COPY . .

# Build and publish the application
RUN dotnet publish -c Release -o /app/publish --no-restore


# =========================
# Stage 2: Runtime
# =========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_HTTP_PORTS=5035

# ASP.NET Core container listens here
EXPOSE 5035

ENTRYPOINT ["dotnet", "MyApi.dll"]