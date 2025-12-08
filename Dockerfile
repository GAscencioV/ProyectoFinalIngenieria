# 1. IMAGEN BASE (SDK para compilar)
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# 2. COPIAR Y RESTAURAR
# Copiamos el csproj respetando la estructura de carpetas
COPY ["ProyectoFinalIngenieria/ProyectoFinalIngenieria.csproj", "ProyectoFinalIngenieria/"]
RUN dotnet restore "ProyectoFinalIngenieria/ProyectoFinalIngenieria.csproj"

# 3. COPIAR TODO EL RESTO Y PUBLICAR
COPY . .
WORKDIR "/src/ProyectoFinalIngenieria"
RUN dotnet publish "ProyectoFinalIngenieria.csproj" -c Release -o /app/publish /p:UseAppHost=false

# 4. IMAGEN FINAL (Runtime ligero)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# 5. CONFIGURACIÓN DE PUERTOS PARA RAILWAY
# Esto asegura que escuche en el puerto que Railway espera (o 8080 por defecto)
ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "ProyectoFinalIngenieria.dll"]