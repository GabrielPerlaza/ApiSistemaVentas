# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copiar archivos de proyecto y restaurar dependencias
COPY *.sln .
COPY SistemadeVentas.*/*.csproj ./
RUN for file in *.csproj; do mkdir -p $(basename $file .csproj) && mv $file $(basename $file .csproj)/; done
RUN dotnet restore

# Copiar todo el código y compilar
COPY . .
RUN dotnet publish SistemadeVentas.Api/SistemadeVentas.Api.csproj -c Release -o /out

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /out .
EXPOSE 80
ENTRYPOINT ["dotnet", "SistemadeVentas.Api.dll"]
