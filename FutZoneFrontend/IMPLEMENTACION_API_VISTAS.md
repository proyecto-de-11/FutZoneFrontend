# Implementación de Consumo de API en Vistas

## Resumen General
Se ha completado la implementación de consumo de API externa en las tres vistas principales del módulo de Empresas/Propietarios. Todas las vistas ahora consumen datos reales de las APIs externas en lugar de datos hardcodeados.

## Vistas Actualizadas

### 1. MisCanchas.razor ✅ COMPLETADO
**Ubicación:** `Components/Pages/Empresa/MisCanchas.razor`

**Cambios Implementados:**
- ✅ Inyección de `ICanchasService`
- ✅ Carga asincrónica de canchas desde API en `CargarCanchas()`
- ✅ Método `GuardarCancha()` actualizado para crear/actualizar canchas vía API
- ✅ Métodos `DeshabilitarCancha()` y `HabilitarCancha()` ahora llaman a API
- ✅ Método `EliminarCancha()` ahora llama a `CanchasService.DeleteCanchaAsync()`
- ✅ Adición de estados de cargando y error en UI
- ✅ Cambio de modelo Cancha a CanchaViewModel para evitar conflictos

**Endpoints Consumidos:**
- GET `/canchas` - Obtener todas las canchas
- POST `/canchas` - Crear nueva cancha
- PUT `/canchas/{id}` - Actualizar cancha
- DELETE `/canchas/{id}` - Eliminar cancha
- PUT `/canchas/{id}/habilitar` - Habilitar cancha
- PUT `/canchas/{id}/deshabilitar` - Deshabilitar cancha

**Variables de Estado:**
- `_cargando`: Indica si se está cargando desde la API
- `_mensajeError`: Almacena mensajes de error para mostrar en UI

---

### 2. MisReservas.razor ✅ COMPLETADO
**Ubicación:** `Components/Pages/Empresa/MisReservas.razor`

**Cambios Implementados:**
- ✅ Inyección de `ICanchasService` y `IReservasService`
- ✅ Carga asincrónica de canchas desde `CanchasService`
- ✅ Carga asincrónica de reservas desde `ReservasService` en `CargarDatos()`
- ✅ Método `AprobarReserva()` ahora llama a `ReservasService.ProcessReservaAsync()`
- ✅ Método `RechazarReserva()` ahora llama a `ReservasService.ProcessReservaAsync()`
- ✅ Mapeo de datos de API a modelo `ReservaViewModel`
- ✅ Adición de estados de cargando y error en UI
- ✅ Cambio de modelos Reserva y Cancha a ViewModel

**Endpoints Consumidos:**
- GET `/canchas` - Obtener todas las canchas (via CanchasService)
- GET `/reservas` - Obtener todas las reservas
- POST `/reservas/{id}/procesar` - Aprobar/Rechazar reserva

**Variables de Estado:**
- `IsLoading`: Indica si se está cargando desde la API
- `ErrorMessage`: Almacena mensajes de error

---

### 3. ReservasCanchas.razor ✅ COMPLETADO
**Ubicación:** `Components/Pages/ReservasCanchas.razor`

**Cambios Implementados:**
- ✅ Inyección de `ICanchasService` y `IReservasService`
- ✅ Carga asincrónica de canchas disponibles en `CargarCanchas()`
- ✅ Carga asincrónica de próximas reservas en `CargarProximasReservas()`
- ✅ Método `HandleReserva()` ahora crea reservas vía `ReservasService.CreateReservaAsync()`
- ✅ Adición de mensajes de éxito, error y cargando en UI
- ✅ Filtrado de reservas futuras solamente

**Endpoints Consumidos:**
- GET `/canchas` - Obtener todas las canchas
- GET `/reservas` - Obtener todas las reservas
- POST `/reservas` - Crear nueva reserva

**Variables de Estado:**
- `IsLoading`: Indica si se está cargando desde la API
- `ErrorMessage`: Almacena mensajes de error
- `SuccessMessage`: Mensaje de éxito después de crear una reserva

---

## Servicios Utilizados

### ICanchasService
- `GetAllCanchasAsync()` - Obtener todas las canchas
- `GetCanchaByIdAsync(id)` - Obtener cancha por ID
- `CreateCanchaAsync(cancha)` - Crear nueva cancha
- `UpdateCanchaAsync(id, cancha)` - Actualizar cancha existente
- `DeleteCanchaAsync(id)` - Eliminar cancha
- `DisableCanchaAsync(id)` - Deshabilitar cancha
- `EnableCanchaAsync(id)` - Habilitar cancha

### IReservasService
- `GetAllReservasAsync()` - Obtener todas las reservas
- `GetReservaByIdAsync(id)` - Obtener reserva por ID
- `CreateReservaAsync(reserva)` - Crear nueva reserva
- `ProcessReservaAsync(id, estado)` - Procesar/cambiar estado de reserva
- `DeleteSolicitudAsync(id)` - Eliminar solicitud
- `DeleteReservaAsync(id)` - Eliminar reserva

---

## API Base Endpoint
**Base URL:** `https://apicanchasyreservas.onrender.com`

---

## Estados de UI Implementados

Todas las vistas implementan los siguientes estados:

### 1. Estado de Carga
```html
@if (_cargando)
{
    <div class="alert alert-info">
        <div class="spinner-border spinner-border-sm"></div>
        <strong>Cargando...</strong>
    </div>
}
```

### 2. Estado de Error
```html
@if (!string.IsNullOrEmpty(_mensajeError))
{
    <div class="alert alert-danger alert-dismissible">
        <i class="bi bi-exclamation-triangle-fill"></i>
        <strong>Error:</strong> @_mensajeError
    </div>
}
```

### 3. Estado de Éxito (ReservasCanchas)
```html
@if (!string.IsNullOrEmpty(SuccessMessage))
{
    <div class="alert alert-success alert-dismissible">
        <i class="bi bi-check-circle-fill"></i>
        <strong>Éxito:</strong> @SuccessMessage
    </div>
}
```

---

## Patrones de Error Handling

Todas las vistas implementan try-catch-finally para manejar errores:

```csharp
try
{
    // Operación que puede fallar
    var resultado = await CanchasService.DeleteCanchaAsync(id);
}
catch (Exception ex)
{
    _mensajeError = $"Error: {ex.Message}";
}
```

---

## Datos Mapeados en MisReservas

La API de reservas no proporciona toda la información necesaria. Se mapea de la siguiente forma:

| Campo en Vista | Fuente | Valor Actual |
|---|---|---|
| NombreCliente | Generado | "Cliente {UsuarioId}" |
| EmailCliente | Generado | "email@ejemplo.com" |
| TelefonoCliente | Generado | "+34 000 000 000" |
| Fecha | API (FechaInicio) | FechaInicio.Date |
| HoraInicio | API (FechaInicio) | TimeOfDay.ToString("hh:mm") |
| HoraFin | API (FechaFin) | TimeOfDay.ToString("hh:mm") |
| Duracion | Calculado | (FechaFin - FechaInicio).TotalHours |
| CantidadJugadores | N/A | 0 (No disponible en API) |

**Nota:** Para mejorar, se puede solicitar a backend que expanda estos datos en la respuesta de reservas.

---

## Notas Importantes

### 1. Autenticación
Las vistas actualmente no implementan validación de usuario autenticado. Los userId/propietarioId están hardcodeados:
- MisReservas: Usa propietario del usuario actual (via PropietarioService)
- ReservasCanchas: Usa userId = 1 (TODO: obtener del usuario autenticado)

### 2. Validación de Datos
- Las vistas validan datos en cliente (required, typeof checking)
- No hay validación en servidor de parte de estas vistas
- El servidor (API) debe validar permiso de propietario al actualizar/eliminar

### 3. Modelos ViewModel
Se crearon modelos ViewModel locales en cada vista para:
- Evitar conflictos de naming (Cancha local vs Cancha del servicio)
- Mapear datos de API a formato necesario para UI
- Agregar campos computados si es necesario

### 4. Llamadas Asincrónicas
- Todos los métodos de servicio son async
- Los manejadores de eventos UI que llaman a estos métodos son async
- Se usa `StateHasChanged()` cuando es necesario forzar re-render

---

## Compilación
✅ Proyecto compila sin errores
✅ No hay warnings críticos
✅ Todas las referencias de servicio inyectadas correctamente

---

## Testing Recomendado

### MisCanchas
1. [ ] Cargar página - debe traer canchas de API
2. [ ] Crear nueva cancha - debe llamar POST /canchas
3. [ ] Editar cancha - debe llamar PUT /canchas/{id}
4. [ ] Deshabilitar cancha - debe cambiar estado visualmente
5. [ ] Habilitar cancha - debe cambiar estado visualmente
6. [ ] Eliminar cancha - debe llamar DELETE /canchas/{id}

### MisReservas
1. [ ] Cargar página - debe traer reservas de API
2. [ ] Filtrar por estado - debe filtrar localmente
3. [ ] Filtrar por cancha - debe filtrar localmente
4. [ ] Aprobar reserva - debe llamar POST /reservas/{id}/procesar
5. [ ] Rechazar reserva - debe llamar POST /reservas/{id}/procesar

### ReservasCanchas
1. [ ] Cargar página - debe traer canchas disponibles
2. [ ] Crear reserva - debe llamar POST /reservas
3. [ ] Listar próximas reservas - debe actualizar después de crear

---

## Cambios Futuros Recomendados

1. **Paginación** - Agregar paginación a listas de reservas (ahora carga todas)
2. **Búsqueda** - Agregar búsqueda/filtrado más avanzado
3. **Datos Completos** - Solicitar datos adicionales a API (nombres cliente, emails, etc.)
4. **Validación Real-time** - Validar disponibilidad de cancha antes de reservar
5. **Notificaciones** - Notificar usuario cuando su reserva es aprobada/rechazada
6. **Caché** - Cachear canchas que no cambian frecuentemente

---

**Fecha de Implementación:** Noviembre 2024
**Estado:** ✅ COMPLETADO
