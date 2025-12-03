# 🚀 Guía Completa de Despliegue en AWS Windows Server

## 📋 Índice
1. [Requisitos Previos](#requisitos-previos)
2. [Preparación Local](#preparación-local)
3. [Configuración de AWS](#configuración-de-aws)
4. [Despliegue en Instancia](#despliegue-en-instancia)
5. [Configuración de Producción](#configuración-de-producción)
6. [Monitoreo y Mantenimiento](#monitoreo-y-mantenimiento)

---

## 📦 Requisitos Previos

### En tu máquina local:
- **Visual Studio 2022** o posterior
- **.NET 8.0 SDK** instalado
- **Git** para control de versiones
- **AWS CLI** (opcional pero recomendado)

### En AWS:
- Cuenta activa en AWS
- Instancia **EC2 Windows Server** (2019 o 2022 recomendado)
- **Security Group** configurado
- **IAM Role** con permisos básicos (opcional)

---

## 🛠️ Preparación Local

### Paso 1: Verificar Configuración de Proyecto
```powershell
# Verificar que .NET 8.0 está instalado
dotnet --version

# Restaurar dependencias
dotnet restore
```

### Paso 2: Publicar la Aplicación
Ejecuta el script de publicación:
```powershell
# Navega a la carpeta del proyecto
cd C:\Users\Otaku\OneDrive\Escritorio\despliegue\FutZoneFrontend\FutZoneFrontend

# Ejecuta el script
.\publish-script.ps1
```

O manualmente:
```powershell
dotnet publish -c Release -o ./publish
```

**Resultado:** Se creará una carpeta `publish` con toda la aplicación compilada.

### Paso 3: Preparar Archivos para AWS
La carpeta `publish` contiene:
- Ejecutables compilados
- Dependencias necesarias
- Archivos de configuración

---

## ☁️ Configuración de AWS

### Paso 1: Crear Instancia EC2 Windows

1. **Abre AWS Console** → EC2 → Instances → Launch Instance
2. **Selecciona AMI:** Windows Server 2022 Base (o 2019)
3. **Tipo de instancia:** t3.medium (o superior según necesidad)
4. **Storage:** 50 GB mínimo
5. **Security Group:** Crear nuevo o usar existente

### Paso 2: Configurar Security Group

Abre **Inbound rules** y añade:

| Protocolo | Puerto | Fuente | Propósito |
|-----------|--------|--------|-----------|
| HTTP | 80 | 0.0.0.0/0 | Acceso web |
| HTTPS | 443 | 0.0.0.0/0 | Acceso seguro |
| RDP | 3389 | Tu IP | Conexión remota |

### Paso 3: Obtener Credenciales

1. Ve a tu instancia en EC2
2. Click en "Connect" → "RDP client"
3. Descarga archivo RDP
4. Click en "Get Windows password"
5. Sube tu clave privada .pem para obtener la contraseña

---

## 📱 Despliegue en Instancia

### Paso 1: Conectarse a la Instancia

Usa **Remote Desktop Connection:**
1. Abre el archivo RDP descargado
2. Ingresa usuario: `Administrator`
3. Ingresa contraseña (obtenida anteriormente)

### Paso 2: Instalar .NET 8.0 Runtime

En la instancia Windows Server:

```powershell
# Descargar .NET 8.0 Runtime
$url = "https://dotnet.microsoft.com/download/dotnet/thank-you/runtime-desktop-8.0.0-windows-x64-installer"

# Ejecutar instalador y seguir pasos en GUI
```

O instala automáticamente:
```powershell
# Instalar desde Chocolatey (si está disponible)
choco install dotnet-runtime -y
```

### Paso 3: Crear Estructura de Carpetas

```powershell
# Crear carpeta para la aplicación
New-Item -ItemType Directory -Path "C:\Apps\FutZoneFrontend" -Force

# Verificar creación
Get-ChildItem -Path "C:\Apps\"
```

### Paso 4: Copiar Archivos Publicados

**Opción A: Usando RDP (Drag & Drop)**
1. En tu máquina local: Abre carpeta `publish`
2. En RDP: Abre `C:\Apps\FutZoneFrontend`
3. Arrastra archivos desde local a remoto

**Opción B: Usando AWS Systems Manager Session Manager**
```powershell
# En tu máquina local
aws s3 cp ./publish s3://tu-bucket/futzonefrontent/ --recursive

# En la instancia (desde Systems Manager)
aws s3 cp s3://tu-bucket/futzonefrontent/ C:\Apps\FutZoneFrontend\ --recursive
```

**Opción C: Comprimir y transferir**
```powershell
# En local
Compress-Archive -Path ./publish -DestinationPath FutZoneFrontend.zip

# Copiar ZIP vía RDP, luego en instancia:
Expand-Archive -Path "C:\Users\Administrator\Downloads\FutZoneFrontend.zip" -DestinationPath "C:\Apps\FutZoneFrontend"
```

### Paso 5: Verificar Instalación

```powershell
# Ver archivos copiados
Get-ChildItem -Path "C:\Apps\FutZoneFrontend" -Recurse | Select-Object -First 20

# Buscar archivo de configuración
Test-Path "C:\Apps\FutZoneFrontend\appsettings.json"
```

---

## ⚙️ Configuración de Producción

### Paso 1: Configurar appsettings.Production.json

En la instancia, abre `C:\Apps\FutZoneFrontend\appsettings.Production.json`:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ApiBaseUrl": "https://tu-api-autentificacion.com",
  "ApiEndpoints": {
    "Autentificacion": {
      "BaseUrl": "https://tu-api-autentificacion.com"
    },
    "CanchasYReservas": {
      "BaseUrl": "https://tu-api-canchasyreservas.com"
    },
    "EmpresaPublicidad": {
      "BaseUrl": "https://tu-api-empresa.com"
    }
  }
}
```

**⚠️ IMPORTANTE:** Reemplaza las URLs con tus APIs reales.

### Paso 2: Configurar Variables de Entorno

```powershell
# Establecer variables de entorno permanentes
[Environment]::SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Production", "Machine")
[Environment]::SetEnvironmentVariable("ASPNETCORE_URLS", "http://0.0.0.0:80", "Machine")

# Verificar
[Environment]::GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Machine")
```

### Paso 3: Crear Servicio de Windows (Recomendado)

**Instalar NSSM:**
1. Descarga NSSM desde: https://nssm.cc/download
2. Extrae a: `C:\Program Files\nssm\`

**Crear servicio:**
```powershell
# Ejecuta con permisos de administrador
.\setup-windows-service.ps1
```

**Verificar:**
```powershell
Get-Service FutZoneFrontend
```

---

## 🔒 Configuración de HTTPS

### Opción 1: AWS Application Load Balancer (Recomendado)

1. **Solicita certificado en AWS Certificate Manager**
   - Ve a AWS Console → Certificate Manager
   - Click en "Request a certificate"
   - Ingresa dominio: `tudominio.com`
   - Valida por email o DNS

2. **Crea Application Load Balancer**
   - EC2 → Load Balancers → Create
   - Configurar listener HTTPS con certificado
   - Target: Instancia Windows Server puerto 80

3. **Apunta dominio a ALB**
   - Obtén DNS del ALB
   - En tu registrador de dominio, crea registro CNAME

### Opción 2: Self-signed Certificate (Testing)

```powershell
# En la instancia
New-SelfSignedCertificate -CertStoreLocation cert:\LocalMachine\My `
  -DnsName "localhost" -FriendlyName "FutZoneFrontend"

# Cambiar ASPNETCORE_URLS a HTTPS
[Environment]::SetEnvironmentVariable("ASPNETCORE_URLS", "https://0.0.0.0:443", "Machine")
```

---

## 📊 Monitoreo y Mantenimiento

### CloudWatch Agent

1. **Descargar e instalar CloudWatch Agent**
```powershell
$url = "https://s3.amazonaws.com/amazoncloudwatch-agent/windows/amd64/latest/amazon-cloudwatch-agent-windows-amd64-msi.exe"
Invoke-WebRequest -Uri $url -OutFile "C:\Temp\CloudWatch-Agent.msi"
```

2. **Crear configuración JSON**
Archivo: `C:\Program Files\Amazon\AmazonCloudWatchAgent\config.json`

3. **Iniciar agente**
```powershell
& "C:\Program Files\Amazon\AmazonCloudWatchAgent\amazon-cloudwatch-agent-ctl.ps1" `
  -a fetch-config -m ec2 -c file:C:\Program Files\Amazon\AmazonCloudWatchAgent\config.json -s
```

### Ver Logs

```powershell
# Logs de aplicación
Get-Content "C:\Apps\FutZoneFrontend\logs\*.log" -Tail 100

# Eventos de Windows
Get-EventLog -LogName Application -Source "FutZoneFrontend" -Newest 50

# PowerShell (en tiempo real)
Get-Service FutZoneFrontend | Select-Object Status, DisplayName
```

---

## 🐛 Solución de Problemas

### La aplicación no inicia

```powershell
# 1. Verificar estado del servicio
Get-Service FutZoneFrontend

# 2. Ver logs detallados
Get-EventLog -LogName Application -Newest 20

# 3. Intentar ejecutar directamente
cd C:\Apps\FutZoneFrontend
.\FutZoneFrontend.exe

# 4. Ver errores en la terminal
```

### Puerto ya está en uso

```powershell
# Encontrar proceso usando puerto 80
netstat -ano | findstr :80

# Matar proceso
taskkill /PID <PID> /F

# O cambiar puerto en variable de entorno
[Environment]::SetEnvironmentVariable("ASPNETCORE_URLS", "http://0.0.0.0:8080", "Machine")
```

### APIs no responden

```powershell
# Verificar conectividad
Test-NetConnection -ComputerName "apiautentificacion.onrender.com" -Port 443

# Verificar DNS
nslookup apiautentificacion.onrender.com
```

---

## ✅ Checklist Final de Despliegue

- [ ] Carpeta `publish` generada correctamente
- [ ] Archivos copiados a `C:\Apps\FutZoneFrontend`
- [ ] .NET 8.0 Runtime instalado
- [ ] `appsettings.Production.json` configurado con URLs correctas
- [ ] Variables de entorno establecidas
- [ ] NSSM instalado (si se usa servicio)
- [ ] Servicio de Windows creado e iniciado
- [ ] Security Group permite tráfico HTTP/HTTPS
- [ ] Certificado SSL/TLS configurado
- [ ] CloudWatch monitoring configurado
- [ ] Logs visibles en aplicación
- [ ] Prueba de acceso desde navegador

---

## 📞 Comandos Útiles Rápidos

```powershell
# Ver estado de servicio
Get-Service FutZoneFrontend | Select-Object Status, Name

# Reiniciar servicio
Restart-Service FutZoneFrontend

# Ver últimas líneas de log
Get-Content "C:\Apps\FutZoneFrontend\logs\latest.log" -Tail 50 -Wait

# Encontrar puerto escuchando
netstat -ano | findstr :80 | findstr LISTENING

# Ver variables de entorno
[Environment]::GetEnvironmentVariables("Machine") | Select-Object -Property * | Format-Table
```

---

**Última actualización:** Diciembre 2025
**Versión:** 1.0
