# Script para crear un servicio de Windows para FutZoneFrontend
# Ejecutar con permisos de administrador en la instancia AWS

param(
    [string]$AppPath = "C:\Apps\FutZoneFrontend",
    [string]$ServiceName = "FutZoneFrontend"
)

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "Configurando Servicio de Windows" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# Verificar permisos de administrador
$isAdmin = [Security.Principal.WindowsIdentity]::GetCurrent().Groups -contains "S-1-5-32-544"
if (-not $isAdmin) {
    Write-Host "Error: Este script debe ejecutarse como administrador" -ForegroundColor Red
    exit 1
}

# Verificar que la carpeta existe
if (!(Test-Path $AppPath)) {
    Write-Host "Error: Carpeta no encontrada: $AppPath" -ForegroundColor Red
    exit 1
}

# Verificar que el ejecutable existe
$exePath = Join-Path $AppPath "FutZoneFrontend.exe"
if (!(Test-Path $exePath)) {
    Write-Host "Error: Ejecutable no encontrado: $exePath" -ForegroundColor Red
    exit 1
}

# Verificar si NSSM está instalado
$nssmPath = "C:\Program Files\nssm\nssm.exe"
if (!(Test-Path $nssmPath)) {
    Write-Host "Warning: NSSM no encontrado en $nssmPath" -ForegroundColor Yellow
    Write-Host ""
    Write-Host "Para instalar NSSM:" -ForegroundColor Yellow
    Write-Host "1. Descarga NSSM desde: https://nssm.cc/download" -ForegroundColor White
    Write-Host "2. Extrae los archivos a: C:\Program Files\nssm\" -ForegroundColor White
    Write-Host "3. Ejecuta este script nuevamente" -ForegroundColor White
    Write-Host ""
    exit 1
}

# Detener servicio si existe
Write-Host "Verificando si el servicio ya existe..." -ForegroundColor Yellow
$service = Get-Service -Name $ServiceName -ErrorAction SilentlyContinue

if ($service) {
    Write-Host "Deteniendo servicio existente..." -ForegroundColor Yellow
    & $nssmPath stop $ServiceName
    Start-Sleep -Seconds 2
    
    Write-Host "Eliminando servicio existente..." -ForegroundColor Yellow
    & $nssmPath remove $ServiceName confirm
}

# Crear nuevo servicio
Write-Host "Creando nuevo servicio: $ServiceName" -ForegroundColor Yellow
& $nssmPath install $ServiceName $exePath

if ($LASTEXITCODE -ne 0) {
    Write-Host "Error al crear el servicio" -ForegroundColor Red
    exit 1
}

# Configurar variables de entorno para el servicio
Write-Host "Configurando variables de entorno..." -ForegroundColor Yellow
& $nssmPath set $ServiceName AppDirectory $AppPath
& $nssmPath set $ServiceName AppEnvironmentExtra "ASPNETCORE_ENVIRONMENT=Production"
& $nssmPath set $ServiceName AppExit Default Restart

# Iniciar servicio
Write-Host "Iniciando servicio..." -ForegroundColor Yellow
& $nssmPath start $ServiceName

Start-Sleep -Seconds 2

# Verificar estado del servicio
$service = Get-Service -Name $ServiceName -ErrorAction SilentlyContinue
if ($service -and $service.Status -eq "Running") {
    Write-Host ""
    Write-Host "========================================" -ForegroundColor Green
    Write-Host "Servicio creado exitosamente" -ForegroundColor Green
    Write-Host "========================================" -ForegroundColor Green
    Write-Host ""
    Write-Host "Nombre del servicio: $ServiceName" -ForegroundColor Green
    Write-Host "Ruta de la aplicación: $AppPath" -ForegroundColor Green
    Write-Host "Estado: $($service.Status)" -ForegroundColor Green
    Write-Host ""
    Write-Host "El servicio se iniciará automáticamente al reiniciar el servidor" -ForegroundColor Green
} else {
    Write-Host ""
    Write-Host "========================================" -ForegroundColor Red
    Write-Host "Error: No se pudo iniciar el servicio" -ForegroundColor Red
    Write-Host "========================================" -ForegroundColor Red
    Write-Host ""
    Write-Host "Verifica los logs en Event Viewer" -ForegroundColor Yellow
    Write-Host "Comando para ver logs del servicio:" -ForegroundColor Yellow
    Write-Host "Get-EventLog -LogName Application -Source $ServiceName" -ForegroundColor White
}

Write-Host ""
Write-Host "Comandos útiles:" -ForegroundColor Cyan
Write-Host "Ver estado: Get-Service $ServiceName" -ForegroundColor White
Write-Host "Detener: Stop-Service $ServiceName" -ForegroundColor White
Write-Host "Iniciar: Start-Service $ServiceName" -ForegroundColor White
Write-Host "Reiniciar: Restart-Service $ServiceName" -ForegroundColor White
Write-Host ""
