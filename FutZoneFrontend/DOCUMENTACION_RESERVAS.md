# 📅 DOCUMENTACIÓN TÉCNICA - MIS RESERVAS

## 1. Descripción General

**Nombre:** Gestión de Reservas  
**Ruta:** `/empresa/reservas`  
**Componente:** `MisReservas.razor`  
**Rendermode:** `InteractiveServer`  
**Objetivo:** Permitir a los propietarios de canchas ver, aceptar y rechazar las reservas de sus canchas.

---

## 2. Características Implementadas

### 2.1 Visualización de Reservas
- ✅ **Lista completa** de todas las reservas asociadas a las canchas del propietario
- ✅ **Cards responsivos** con información detallada de cada reserva
- ✅ **Estados visuales** (Pendiente, Aprobada, Rechazada)
- ✅ **Grid adaptativo** (1-3 columnas según tamaño de pantalla)

### 2.2 Información del Cliente
- ✅ **Nombre del cliente**
- ✅ **Email para contacto**
- ✅ **Teléfono**
- ✅ **Notas del cliente** (con formato destacado)

### 2.3 Detalles de la Reserva
- ✅ **Fecha de la reserva**
- ✅ **Hora de inicio y fin**
- ✅ **Duración en horas**
- ✅ **Cantidad de jugadores**
- ✅ **Nombre de la cancha**

### 2.4 Información Financiera
- ✅ **Precio por hora**
- ✅ **Descuento aplicado**
- ✅ **Monto total a pagar** (destacado)

### 2.5 Historial
- ✅ **Fecha y hora de solicitud**
- ✅ **Fecha y hora de respuesta** (si aplica)
- ✅ **Tipo de respuesta** (Aprobada/Rechazada)

### 2.6 Filtrados Avanzados
- ✅ **Filtrar por estado** (Todos, Pendientes, Aprobadas, Rechazadas)
- ✅ **Filtrar por cancha** (Todas o específica)
- ✅ **Ordenamiento dinámico**
  - Por fecha (más recientes/antiguas)
  - Por monto (mayor/menor)

### 2.7 Estadísticas
- ✅ **Tarjeta 1:** Reservas pendientes de aprobación
- ✅ **Tarjeta 2:** Reservas aprobadas
- ✅ **Tarjeta 3:** Reservas rechazadas
- ✅ **Tarjeta 4:** Ingresos totales (solo reservas aprobadas)

### 2.8 Acciones Disponibles

#### Para Reservas Pendientes (⏳):
- ✅ **[✅ APROBAR]** - Cambia estado a "Aprobada" y registra fecha/hora
- ✅ **[❌ RECHAZAR]** - Cambia estado a "Rechazada" y registra fecha/hora

#### Para Reservas Ya Respondidas:
- ✅ **[🔒 ESTADO]** - Botón deshabilitado (estado bloqueado)

---

## 3. Modelos de Datos

### 3.1 Clase Reserva

```csharp
public class Reserva
{
    public int Id { get; set; }                          // ID único
    public int CanchaId { get; set; }                    // ID de la cancha
    public string CanchaNombre { get; set; }             // Nombre cancha
    public string NombreCliente { get; set; }            // Nombre cliente
    public string EmailCliente { get; set; }             // Email cliente
    public string TelefonoCliente { get; set; }          // Teléfono
    public DateTime Fecha { get; set; }                  // Fecha reserva
    public string HoraInicio { get; set; }               // Ej: "18:00"
    public string HoraFin { get; set; }                  // Ej: "20:00"
    public int Duracion { get; set; }                    // Horas
    public int CantidadJugadores { get; set; }           // Número de personas
    public decimal PrecioHora { get; set; }              // $ por hora
    public decimal Descuento { get; set; }               // $ descuento
    public decimal PrecioTotal { get; set; }             // Total a pagar
    public string NotasCliente { get; set; }             // Notas adicionales
    public string Estado { get; set; }                   // Pendiente/Aprobada/Rechazada
    public DateTime FechaSolicitud { get; set; }         // Cuándo solicitó
    public string FechaRespuesta { get; set; }           // Cuándo respondimos
}
```

### 3.2 Clase Cancha

```csharp
public class Cancha
{
    public int Id { get; set; }
    public string Nombre { get; set; }
}
```

---

## 4. Métodos del Componente

### 4.1 Ciclo de Vida
- **`OnInitialized()`** - Carga datos iniciales y aplica filtros

### 4.2 Carga de Datos
- **`CargarDatos()`** - Carga canchas y reservas de ejemplo (5 reservas precargadas)

### 4.3 Filtrado
- **`FiltrarPorEstado(ChangeEventArgs e)`** - Filtra por estado de reserva
- **`FiltrarPorCancha(ChangeEventArgs e)`** - Filtra por cancha específica
- **`OrdenarReservas(ChangeEventArgs e)`** - Aplica ordenamiento
- **`AplicarFiltros()`** - Combina todos los filtros y ordenamientos

### 4.4 Acciones
- **`AprobarReserva(int id)`** - Aprueba una reserva pendiente
- **`RechazarReserva(int id)`** - Rechaza una reserva pendiente

### 4.5 Utilidad
- **`GetEstadoIcono(string estado)`** - Retorna icono según estado (⏳/✅/❌)

---

## 5. Datos de Ejemplo Precargados

Se incluyen **5 reservas de ejemplo** para demostración:

### Reserva 1: Juan García López - Pendiente
- Cancha: Cancha Principal
- Fecha: 2 días en el futuro
- Hora: 18:00 - 20:00 (2 horas)
- Jugadores: 22
- Total: $100
- Notas: "Necesitamos la cancha con balón incluido. Gracias."
- **Estado: PENDIENTE ⏳**

### Reserva 2: Carlos Martínez Ruiz - Aprobada
- Cancha: Cancha Principal
- Fecha: 5 días en el futuro
- Hora: 19:00 - 21:00 (2 horas)
- Jugadores: 18
- Descuento: $5
- Total: $95
- **Estado: APROBADA ✅**

### Reserva 3: Ana López González - Rechazada
- Cancha: Cancha Secundaria
- Fecha: 1 día en el pasado
- Hora: 20:00 - 22:00 (2 horas)
- Jugadores: 16
- Total: $70
- Notas: "¿Hay estacionamiento disponible?"
- **Estado: RECHAZADA ❌**

### Reserva 4: David Sánchez Torres - Pendiente
- Cancha: Cancha Secundaria
- Fecha: 7 días en el futuro
- Hora: 17:00 - 19:00 (2 horas)
- Jugadores: 14
- Descuento: $7
- Total: $63
- Notas: "Torneo local. Necesitamos iluminación extra si es posible."
- **Estado: PENDIENTE ⏳**

### Reserva 5: Miguel Rodríguez Pérez - Aprobada
- Cancha: Cancha Entrenamiento
- Fecha: 10 días en el futuro
- Hora: 16:00 - 18:00 (2 horas)
- Jugadores: 12
- Total: $50
- **Estado: APROBADA ✅**

---

## 6. Estructura HTML

### 6.1 Header
```
┌─────────────────────────────────────┐
│ 📅 Gestión de Reservas              │
│ Visualiza, acepta o rechaza...      │
└─────────────────────────────────────┘
```

### 6.2 Estadísticas (4 Cards)
```
[⏳ Pendientes] [✅ Aprobadas] [❌ Rechazadas] [💰 Ingresos]
```

### 6.3 Filtros (3 Selects)
```
[Estado] [Cancha] [Ordenamiento]
```

### 6.4 Lista de Reservas (Grid adaptativo)
```
Grid de cards, cada una con:
- Header (Cancha + Estado)
- Body (Información detallada)
- Footer (Acciones)
```

---

## 7. Estilos CSS

### 7.1 Archivo
**Ubicación:** `/wwwroot/css/reservas.css`  
**Líneas:** 450+  
**Separado:** Sí (archivo independiente)

### 7.2 Clases Principales

| Clase | Descripción |
|-------|-------------|
| `.misreservas-container` | Contenedor principal |
| `.header-gradient-reservas` | Header con gradiente |
| `.stats-container` | Grid de estadísticas |
| `.stat-card` | Card individual de stat |
| `.filters-section` | Sección de filtros |
| `.reservas-section` | Sección de reservas |
| `.reservas-grid` | Grid de cards |
| `.reserva-card` | Card de reserva |
| `.reserva-header` | Header del card |
| `.estado-badge` | Badge de estado |
| `.reserva-body` | Body del card |
| `.info-section` | Sección de información |
| `.info-item` | Item de información |
| `.notes-box` | Caja de notas |
| `.reserva-actions` | Botones de acción |
| `.btn-action` | Botón individual |

### 7.3 Colores

```css
Gradiente Header: #667eea → #764ba2
Pendiente: #f59e0b (Naranja)
Aprobada: #10b981 (Verde)
Rechazada: #ef4444 (Rojo)
Ingresos: #8b5cf6 (Púrpura)
Fondo: #f8f9fa (Gris claro)
```

### 7.4 Media Queries

- **>1200px:** 3 columnas en stats, grid de reservas normal
- **992-1200px:** 2 columnas stats, 2 filtros por fila
- **768-992px:** 2 columnas reservas, filtros responsive
- **<768px:** 1 columna, cards full-width, mobile optimizado
- **<480px:** Extra pequeño, padding y fonts reducidos

---

## 8. Responsividad

### Desktop (>1200px)
```
4 stat cards en una fila
3 filtros en una fila
Grid de reservas: 3 columnas
Ancho máximo: 1400px
```

### Laptop (992-1200px)
```
2 stat cards por fila
2 filtros por fila
Grid de reservas: 2 columnas
```

### Tablet (768-992px)
```
Stat cards: 2 por fila
Filtros: 2 por fila
Reservas: 2 columnas
Encabezados: flex-direction column
```

### Móvil (<768px)
```
Stat cards: 1 por fila
Filtros: 1 por fila
Reservas: 1 columna
Botones: full-width
Modales: ajustados
```

---

## 9. Integración en el Proyecto

### 9.1 Rutas
```csharp
@page "/empresa/reservas"
```

### 9.2 Navigation
Se agregó línea en `NavMenu.razor`:
```html
<NavLink class="nav-link" href="empresa/reservas">
    <span class="bi bi-calendar-check"></span> Mis Reservas
</NavLink>
```

### 9.3 CSS
Se agregó enlace en `App.razor`:
```html
<link rel="stylesheet" href="css/reservas.css" />
```

---

## 10. Datos Precargados en OnInitialized()

```csharp
- 3 canchas (Principal, Secundaria, Entrenamiento)
- 5 reservas con datos variados
- 2 pendientes, 2 aprobadas, 1 rechazada
- Mezcla de fechas pasadas y futuras
- Algunos con descuentos, otros sin
- Algunos con notas, otros vacías
```

---

## 11. Funcionalidades Operativas

### Lectura ✅
- Mostrar todas las reservas
- Ver estado actual
- Ver detalles completos
- Ver información financiera
- Ver notas

### Filtrado ✅
- Por estado (3 opciones)
- Por cancha (4 opciones)
- Ordenamiento (4 opciones)
- Combinable

### Actualización ✅
- Cambiar de Pendiente a Aprobada
- Cambiar de Pendiente a Rechazada
- Registrar fecha/hora de respuesta
- Bloquear cambios posteriores

### Estadísticas ✅
- Contar pendientes
- Contar aprobadas
- Contar rechazadas
- Calcular ingresos

---

## 12. Validación

### Cliente
- Solo permite aprobar/rechazar si estado es "Pendiente"
- Botones se deshabilitan automáticamente
- Filtros se aplican en tiempo real
- Ordenamientos se aplican inmediatamente

### Servidor (Pendiente)
- Validar pertenencia de cancha al usuario
- Validar datos de reserva
- Registrar cambios en BD
- Enviar notificaciones al cliente

---

## 13. Próximos Pasos

### Inmediatos
1. [ ] Conectar API backend para obtener reservas reales
2. [ ] Implementar autenticación real
3. [ ] Persistir cambios en BD
4. [ ] Enviar notificaciones por email

### Mejoras
1. [ ] Modal para rechazar con motivo
2. [ ] Historial de cambios
3. [ ] Exportar reportes
4. [ ] Calendario visual
5. [ ] Chat con cliente
6. [ ] Confirmación por SMS
7. [ ] Recordatorios automáticos

---

## 14. Archivos Involucrados

```
Creados:
├── Components/Pages/Empresa/MisReservas.razor (700+ líneas)
└── wwwroot/css/reservas.css (450+ líneas)

Modificados:
├── Components/Layout/NavMenu.razor
└── Components/App.razor
```

---

**Documento Generado:** 13 de Noviembre, 2025  
**Versión:** 1.0 - Documentación Técnica Completa
