FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine

WORKDIR /app

# Copy sln and project folders
COPY . .

# Restore deps for all projects
RUN dotnet restore

# Build & run tests
RUN ["sh"]
