# Stage 1: Build the Front-End
FROM node:20-alpine AS frontend-build
WORKDIR /app/frontend

# Copy package files
COPY Front-End/package*.json ./

# Install dependencies
RUN npm ci

# Copy the rest of the frontend source
COPY Front-End/ ./

# Build the frontend (outputs to ../Dev.Naamloos.Ducker/wwwroot/build)
RUN npm run build

# Stage 2: Build the ASP.NET Core application
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS backend-build
WORKDIR /app

# Copy csproj and restore dependencies
COPY Dev.Naamloos.Ducker/*.csproj ./Dev.Naamloos.Ducker/
RUN dotnet restore ./Dev.Naamloos.Ducker/Dev.Naamloos.Ducker.csproj

# Copy the rest of the backend source
COPY Dev.Naamloos.Ducker/ ./Dev.Naamloos.Ducker/

# Copy built frontend assets from previous stage
COPY --from=frontend-build /app/Dev.Naamloos.Ducker/wwwroot/build ./Dev.Naamloos.Ducker/wwwroot/build

# Build the application
WORKDIR /app/Dev.Naamloos.Ducker
RUN dotnet publish -c Release -o /app/publish

# Stage 3: Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Copy published application
COPY --from=backend-build /app/publish .

# Expose port
EXPOSE 8080
EXPOSE 8081

# Set environment variables
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

RUN printf '#!/bin/sh\nexec dotnet /app/Dev.Naamloos.Ducker.dll "$@"\n' > /usr/local/bin/ducker \
    && chmod +x /usr/local/bin/ducker

# Run the application
ENTRYPOINT ["ducker"]
