#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy solution file
COPY ["*.sln", "./"]

# Copy project files and restore dependencies
COPY ["N5.Domain/N5.Domain.csproj", "N5.Domain/"]
COPY ["N5.Application/N5.Application.csproj", "N5.Application/"]
COPY ["N5.Infrastructure/N5.Infrastructure.csproj", "N5.Infrastructure/"]
COPY ["N5.WebApi/N5.WebApi.csproj", "N5.WebApi/"]

# Restore dependencies
RUN dotnet restore "N5.WebApi/N5.WebApi.csproj"

# Copy all source code
COPY N5.Domain/ N5.Domain/
COPY N5.Application/ N5.Application/
COPY N5.Infrastructure/ N5.Infrastructure/
COPY N5.WebApi/ N5.WebApi/

# Build and publish
WORKDIR "/src/N5.WebApi"
RUN dotnet build "N5.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "N5.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "N5.WebApi.dll"]