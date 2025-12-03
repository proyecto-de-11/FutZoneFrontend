# Guía de Despliegue en AWS Windows Server - FutZoneFrontend

## Requisitos Previos
- Visual Studio 2022 o posterior
- .NET 8.0 SDK instalado
- Acceso a instancia de Windows Server en AWS
- AWS Systems Manager Session Manager (o RDP para conectarse)

## Pasos de Despliegue Local

### 1. Publicar la Aplicación
En la terminal de Visual Studio o PowerShell, ejecuta:

```powershell
dotnet publish -c Release -o ./publish
```

Este comando creará una carpeta `publish` con la aplicación compilada en modo Release.

## Pasos de Despliegue en AWS Windows Server

### 1. Conectarse a la Instancia
Usa AWS Systems Manager Session Manager o RDP para conectarte a tu instancia de Windows Server.

### 2. Preparar el Servidor
```powershell
# Instalar .NET 8.0 Runtime (si no está instalado)
# Descarga desde: https://dotnet.microsoft.com/download/dotnet/8.0

# Crear carpeta para la aplicación
New-Item -ItemType Directory -Path "C:\Apps\FutZoneFrontend" -Force
```

### 3. Copiar Archivos
Copia la carpeta `publish` completa a la instancia EC2:
- Opción 1: Usar AWS Systems Manager Session Manager para copiar archivos
- Opción 2: Usar RDP y copiar mediante drag-and-drop
- Opción 3: Usar AWS DataSync o S3

### 4. Configurar appsettings.json
En la instancia, edita `C:\Apps\FutZoneFrontend\appsettings.Production.json`:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ApiBaseUrl": "https://tu-api-backend-url",
  "ApiEndpoints": {
    "Autentificacion": {
      "BaseUrl": "https://tu-api-backend-url"
    },
    "CanchasYReservas": {
      "BaseUrl": "https://tu-api-canchasyreservas-url"
    },
    "EmpresaPublicidad": {
      "BaseUrl": "https://tu-api-empresa-url"
    }
  }
}
```

### 5. Crear Servicio de Windows (Recomendado)
Para que la aplicación se inicie automáticamente:

```powershell
# Instalar NSSM (Non-Sucking Service Manager)
# Descargar desde: https://nssm.cc/download

# Registrar la aplicación como servicio
nssm install FutZoneFrontend "C:\Apps\FutZoneFrontend\FutZoneFrontend.exe"

# Iniciar el servicio
nssm start FutZoneFrontend
```

### 6. Configurar IIS (Alternativo)
Si prefieres usar IIS en lugar de ejecutar la aplicación directamente:

```powershell
# Instalar IIS
Install-WindowsFeature -Name Web-Server -IncludeManagementTools

# Instalar ASP.NET Core Hosting Bundle
# Descargar desde: https://dotnet.microsoft.com/download/dotnet/8.0

# Crear application pool y sitio en IIS
# (Consultar documentación de IIS para configuración detallada)
```

### 7. Configurar Security Groups
En AWS Console:
- Abre los Security Groups de tu instancia
- Añade regla de entrada para puerto 80 (HTTP) y 443 (HTTPS)
- Fuente: 0.0.0.0/0 o tu IP específica

### 8. Verificar Instalación
```powershell
# Verificar que la aplicación está corriendo
Get-Service FutZoneFrontend

# Ver logs
nssm status FutZoneFrontend
```

## Configuración de HTTPS (SSL/TLS)

### Opción 1: AWS Certificate Manager + Application Load Balancer
1. Solicita certificado en AWS Certificate Manager
2. Crea un Application Load Balancer
3. Configura listener HTTPS con el certificado
4. Apunta al puerto 8080 de tu instancia

### Opción 2: Let's Encrypt en Windows Server
```powershell
# Instalar Certbot para Windows
# Descargar desde: https://certbot.eff.org/

# Configurar certificado auto-renovable
```

## Variables de Entorno
Configura estas variables en tu instancia:

```powershell
[Environment]::SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Production", "Machine")
[Environment]::SetEnvironmentVariable("ASPNETCORE_URLS", "http://0.0.0.0:8080", "Machine")
```

## Monitoreo y Mantenimiento

### CloudWatch
Configura CloudWatch Agent para monitorear:
- Uso de CPU
- Uso de Memoria
- Logs de la aplicación

### Auto-scaling
- Crea una AMI (Amazon Machine Image) con la aplicación
- Configura Auto Scaling Group para escalabilidad

## Solución de Problemas

### La aplicación no inicia
```powershell
# Ver logs
Get-Content "C:\Apps\FutZoneFrontend\logs\*.log"

# Reiniciar manualmente
C:\Apps\FutZoneFrontend\FutZoneFrontend.exe
```

### Puerto ocupado
```powershell
# Encontrar proceso usando el puerto 8080
Get-NetTCPConnection -LocalPort 8080

# Cambiar puerto en launchSettings.json
```

## Checklist Final
- [ ] .NET 8.0 Runtime instalado
- [ ] Carpeta publish copiada
- [ ] appsettings.Production.json configurado
- [ ] URLs de APIs correctas
- [ ] Security Groups configurados
- [ ] Certificado SSL/TLS instalado
- [ ] Servicio de Windows creado
- [ ] Aplicación iniciada correctamente
- [ ] CloudWatch monitoring configurado
