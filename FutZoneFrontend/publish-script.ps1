# Script de publicación para AWS Windows Server
# Ejecutar desde el directorio raíz del proyecto

param(
    [string]$Configuration = "Release",
    [string]$OutputPath = "./publish"
)

Write-Host "========================================" -ForegroundColor Cyan
Write-Host "FutZoneFrontend - Publicación para AWS" -ForegroundColor Cyan
Write-Host "========================================" -ForegroundColor Cyan
Write-Host ""

# Verificar que estamos en el directorio correcto
if (!(Test-Path "FutZoneFrontend.csproj")) {
    Write-Host "Error: No se encontró FutZoneFrontend.csproj" -ForegroundColor Red
    Write-Host "Asegúrate de ejecutar este script desde el directorio raíz del proyecto" -ForegroundColor Red
    exit 1
}

# Limpiar publicaciones anteriores
Write-Host "Limpiando publicaciones anteriores..." -ForegroundColor Yellow
if (Test-Path $OutputPath) {
    Remove-Item -Path $OutputPath -Recurse -Force
}

# Restaurar dependencias
Write-Host "Restaurando dependencias NuGet..." -ForegroundColor Yellow
dotnet restore

if ($LASTEXITCODE -ne 0) {
    Write-Host "Error al restaurar dependencias" -ForegroundColor Red
    exit 1
}

# Publicar aplicación
Write-Host "Publicando aplicación en modo $Configuration..." -ForegroundColor Yellow
dotnet publish -c $Configuration -o $OutputPath

if ($LASTEXITCODE -ne 0) {
    Write-Host "Error al publicar la aplicación" -ForegroundColor Red
    exit 1
}

Write-Host ""
Write-Host "========================================" -ForegroundColor Green
Write-Host "Publicación completada exitosamente" -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Green
Write-Host ""
Write-Host "Archivos publicados en: $(Resolve-Path $OutputPath)" -ForegroundColor Green
Write-Host ""
Write-Host "Próximos pasos:" -ForegroundColor Cyan
Write-Host "1. Copia la carpeta '$OutputPath' a tu instancia AWS Windows Server" -ForegroundColor White
Write-Host "2. Actualiza appsettings.Production.json con tus URLs de API" -ForegroundColor White
Write-Host "3. Ejecuta la aplicación o registrala como servicio de Windows" -ForegroundColor White
Write-Host ""
