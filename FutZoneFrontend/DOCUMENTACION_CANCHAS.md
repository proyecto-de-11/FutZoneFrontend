# 🏟️ GESTIÓN DE CANCHAS - Documentación de Prototipo

## 📋 Visión General

La vista de **Gestión de Canchas** permite a los propietarios de empresas/complejos deportivos:
- ✅ **Crear** nuevas canchas con especificaciones completas
- ✅ **Editar** información de canchas existentes
- ✅ **Habilitar/Deshabilitar** canchas según su disponibilidad
- ✅ **Eliminar** canchas del sistema
- ✅ **Gestionar horarios** disponibles hora por hora (6:00 AM - 11:00 PM)
- ✅ **Asignar días** en los que la cancha está disponible
- ✅ **Ver estadísticas** rápidas sobre la actividad

---

## 🎨 Estructura Visual

### Header de la Página
```
┌─────────────────────────────────────────────────────────────────────┐
│                                                                     │
│  [Gradiente Púrpura: #667eea → #764ba2]                          │
│                                                                     │
│  ⚽ Gestión de Canchas                                            │
│  Administra tus canchas, horarios y disponibilidad               │
│                                                                     │
└─────────────────────────────────────────────────────────────────────┘
```

### Tarjetas de Estadísticas
```
┌──────────────┐  ┌──────────────┐  ┌──────────────┐  ┌──────────────┐
│              │  │              │  │              │  │              │
│ 📍 Canchas   │  │ 📅 Reservas  │  │ ⏰ Horas     │  │ 💰 Ingresos  │
│ Activas      │  │ Hoy          │  │ Libres       │  │ (Mes)        │
│              │  │              │  │              │  │              │
│      2       │  │      3       │  │     18       │  │   $2,500     │
│              │  │              │  │              │  │              │
└──────────────┘  └──────────────┘  └──────────────┘  └──────────────┘
```

### Tarjeta de Cancha (Layout)
```
┌─────────────────────────────────────────────┐
│ [GRADIENTE PÚRPURA]                         │
│ Cancha Principal        [ACTIVA] ✓          │
│ 📍 Calle Principal 123                      │
├─────────────────────────────────────────────┤
│                                             │
│ 📐 Dimensiones:        8x44 m               │
│ 💰 Precio/Hora:        $50                  │
│ 🕐 Horario:            06:00 - 23:00       │
│ 👥 Capacidad:          22 personas          │
│ 🏟️ Tipo Superficie:     Pasto Sintético     │
│                                             │
├─────────────────────────────────────────────┤
│ 📅 Horarios Disponibles                     │
│ ┌─────┬─────┬─────┬─────┐                 │
│ │ 06  │ 07  │ 08  │ 09  │  06:00 ✓        │
│ │ :00 │ :00 │ :00 │ :00 │  07:00 ✓        │
│ │ ✓   │ ✓   │ ✓   │ ✓   │  ...             │
│ └─────┴─────┴─────┴─────┘                 │
│ (Mostrando horarios de las 6 AM a las 11 PM)
│                                             │
├─────────────────────────────────────────────┤
│ 📆 Disponibles Estos Días                   │
│ ┌──┬──┬───┬──┬──┬───┬───┐                │
│ │Lu│Ma│Mié│Ju│Vi│Sáb│Dom│                │
│ │✓ │✓ │ ✓ │✓ │✓ │ ✓ │ ✓ │  (activos)      │
│ └──┴──┴───┴──┴──┴───┴───┘                │
│                                             │
├─────────────────────────────────────────────┤
│ [Editar] [Deshabilitar] [Eliminar]        │
│                                             │
└─────────────────────────────────────────────┘
```

---

## 📊 Características Principales

### 1. **Estadísticas Rápidas**
- Canchas Activas: Conteo automático de canchas con estado activo
- Reservas Hoy: Número de reservas programadas para hoy
- Horas Libres: Total de horas disponibles en el mes
- Ingresos: Monto total generado en el mes actual

### 2. **Gestión de Canchas**

#### Crear Nueva Cancha
```
Modal: "Crear Nueva Cancha"
├─ Nombre de la Cancha (texto)
├─ Ubicación (texto)
├─ Dimensiones (dropdown: 5x25, 6x36, 7x42, 8x44, 9x45)
├─ Tipo Superficie (dropdown: Cemento, Pasto Sintético, etc)
├─ Precio por Hora (número)
├─ Capacidad (número de jugadores)
├─ Hora Apertura (time picker)
├─ Hora Cierre (time picker)
├─ Días Disponibles (checkboxes: Lun-Dom)
├─ Horarios Disponibles (checkboxes: 06:00 hasta 23:00)
├─ Descripción (textarea)
└─ ☑ Cancha Activa (switch)
```

#### Editar Cancha
- Abre el modal con todos los campos pre-llenados
- Permite modificar cualquier aspecto de la cancha
- Los cambios se aplican inmediatamente

#### Deshabilitar/Habilitar
- Botón "Deshabilitar" (si está activa) → desactiva la cancha
- Botón "Habilitar" (si está inactiva) → activa la cancha
- La cancha aparecerá con opacidad si está deshabilitada

#### Eliminar Cancha
- Abre modal de confirmación
- Requiere confirmación explícita del usuario
- Acción irreversible (es una prueba prototipo)

### 3. **Gestión de Horarios**
- **18 horas disponibles** (06:00 - 23:00)
- Cada hora se puede marcar como disponible o no
- Se visualiza con badges de color:
  - 🟦 Azul claro: Disponible
  - 🟥 Rojo claro: Ocupado

### 4. **Gestión de Días**
- Seleccionar qué días de la semana la cancha está disponible
- Visualización rápida en la tarjeta con 7 badges (uno por día)
- Verde = Disponible, Gris = No disponible

---

## 🎯 Flujos de Usuario

### Flujo 1: Crear Nueva Cancha
```
Usuario hace clic en [+ Nueva Cancha]
    ↓
Se abre Modal "Crear Nueva Cancha"
    ↓
Usuario completa formulario:
  - Nombre, Ubicación
  - Dimensiones, Superficie
  - Precio, Capacidad
  - Horarios de Apertura/Cierre
  - Selecciona Días (checkboxes)
  - Selecciona Horarios (checkboxes)
  - Escribe Descripción
  - Marca "Activa"
    ↓
Usuario hace clic en [✓ Crear Cancha]
    ↓
Nuevo cancha se agrega a la lista
    ↓
Modal se cierra automáticamente
```

### Flujo 2: Editar Cancha
```
Usuario hace clic en [✏️ Editar] en una tarjeta
    ↓
Se abre Modal "Editar Cancha"
    ↓
Todos los campos están pre-llenados con datos actuales
    ↓
Usuario modifica los campos deseados
    ↓
Usuario hace clic en [✓ Actualizar Cancha]
    ↓
Cambios se aplican a la cancha
    ↓
Modal se cierra y tarjeta se actualiza
```

### Flujo 3: Deshabilitar Cancha
```
Usuario hace clic en [🔒 Deshabilitar]
    ↓
Cancha se marca como Deshabilitada
    ↓
Badge de estado cambia a "DESHABILITADA" (rojo)
    ↓
Tarjeta pierde opacidad visual
    ↓
Botón cambia a [🔓 Habilitar]
```

### Flujo 4: Eliminar Cancha
```
Usuario hace clic en [🗑️ Eliminar]
    ↓
Se abre Modal de Confirmación
    ↓
Muestra advertencia: "Esta acción no se puede deshacer"
    ↓
Usuario hace clic en [✓ Sí, Eliminar]
    ↓
Cancha se elimina del sistema
    ↓
La tarjeta desaparece de la lista
```

---

## 🎨 Sistema de Colores

| Elemento | Color | Uso |
|----------|-------|-----|
| Gradiente Header | #667eea → #764ba2 | Encabezado principal |
| Cancha Activa | Verde | Badge de estado ACTIVA |
| Cancha Deshabilitada | Rojo | Badge de estado DESHABILITADA |
| Horario Disponible | Azul claro | Horas disponibles |
| Horario Ocupado | Rojo claro | Horas ocupadas |
| Día Disponible | Verde degradado | Días seleccionados |
| Día No Disponible | Gris | Días no seleccionados |
| Stat Card | Azul/Verde/Naranja/Púrpura | Diferentes métricas |

---

## 📦 Modelo de Datos

```csharp
public class Cancha
{
    public int Id { get; set; }
    public string Nombre { get; set; }                    // "Cancha Principal"
    public string Ubicacion { get; set; }                 // "Calle Principal 123"
    public string Dimensiones { get; set; }               // "8x44"
    public string TipoSuperficie { get; set; }            // "Pasto Sintético"
    public decimal PrecioHora { get; set; }               // 50.00
    public int CapacidadJugadores { get; set; }           // 22
    public string HoraApertura { get; set; }              // "06:00"
    public string HoraCierre { get; set; }                // "23:00"
    public List<string> DiasDisponibles { get; set; }     // ["Lunes", "Martes", ...]
    public List<HorarioDisponible> HorariosDisponibles { get; set; }
    public bool Activa { get; set; }                      // true
    public string Descripcion { get; set; }               // "Cancha profesional con iluminación LED"
}

public class HorarioDisponible
{
    public string Hora { get; set; }                      // "06:00"
    public bool Disponible { get; set; }                  // true/false
}
```

---

## 🔧 Componentes Utilizados

### Bootstrap Components
- ✅ Grid system (container-fluid, row, col)
- ✅ Cards (div.card, card-header, card-body)
- ✅ Badges (badge, bg-success, bg-danger)
- ✅ Buttons (btn, btn-primary, btn-danger, btn-warning)
- ✅ Forms (form-control, form-label, form-check)
- ✅ Modals (modal, modal-content, modal-header, modal-body)
- ✅ Alerts (alert, alert-info)

### Custom Styling
- ✅ Gradientes lineales
- ✅ Hover effects (transform, box-shadow)
- ✅ Responsive grid layouts
- ✅ Badges personalizados
- ✅ Cards with border effects

---

## 📱 Responsividad

| Breakpoint | Comportamiento |
|------------|-----------------|
| **Desktop** (>1200px) | 3 columnas de canchas, vista completa |
| **Laptop** (992px - 1200px) | 2 columnas de canchas |
| **Tablet** (768px - 992px) | 2 columnas de canchas, stat cards apiladas |
| **Móvil** (<768px) | 1 columna, botones en columna, grid simplificado |

---

## 🔄 Estados Posibles

### Cancha Activa
- ✅ Disponible para reservas
- ✅ Visible en búsquedas
- ✅ Badge verde "ACTIVA"
- ✅ Botón "Deshabilitar" activo

### Cancha Deshabilitada
- ❌ No disponible para reservas
- ❌ No visible en búsquedas (opcional)
- ❌ Badge rojo "DESHABILITADA"
- ❌ Botón "Habilitar" disponible

---

## 🎯 Datos de Ejemplo

### Cancha 1: Principal
```
Nombre:             Cancha Principal
Ubicación:          Calle Principal 123
Dimensiones:        8x44 m
Tipo Superficie:    Pasto Sintético
Precio/Hora:        $50
Capacidad:          22 jugadores
Horario:            06:00 - 23:00
Días:               Lunes a Domingo
Horarios Libres:    Todos disponibles
Estado:             ✓ ACTIVA
Descripción:        Cancha profesional con iluminación LED
```

### Cancha 2: Secundaria
```
Nombre:             Cancha Secundaria
Ubicación:          Avenida 5 de Mayo 456
Dimensiones:        6x36 m
Tipo Superficie:    Cemento
Precio/Hora:        $35
Capacidad:          16 jugadores
Horario:            08:00 - 22:00
Días:               Martes a Sábado
Horarios Ocupados:  10:00, 14:00, 18:00
Estado:             ✓ ACTIVA
Descripción:        Cancha deportiva estándar
```

### Cancha 3: Entrenamiento
```
Nombre:             Cancha Entrenamiento
Ubicación:          Parque Central
Dimensiones:        5x25 m
Tipo Superficie:    Pasto Natural
Precio/Hora:        $25
Capacidad:          12 jugadores
Horario:            07:00 - 20:00
Días:               Lunes, Miércoles, Viernes, Domingo
Horarios Ocupados:  07:00, 17:00
Estado:             ❌ DESHABILITADA
Descripción:        Cancha pequeña para entrenamientos
```

---

## 🎛️ Variables de Control

```csharp
private List<Cancha> _canchas;              // Lista de todas las canchas
private Cancha? _editandoCancha;            // Cancha en edición
private Cancha _formCancha;                 // Formulario actual
private bool _mostrarModalCancha;           // Control de visibilidad del modal
private bool _mostrarConfirmEliminar;       // Control de modal de confirmación
private Cancha? _canchaPorEliminar;        // Cancha a eliminar
private int _reservasHoy;                   // Contador de reservas
private int _horasLibres;                   // Contador de horas disponibles
private decimal _ingresosMes;               // Monto de ingresos mensuales
```

---

## 🔗 Rutas de Navegación

| Ruta | Descripción | Icono |
|------|-------------|-------|
| `/empresa/canchas` | Gestión de canchas | ⚽ |
| `/admin/dashboard` | Dashboard administrativo | 📊 |
| `/admin/roles` | Gestión de roles | 👔 |
| `/admin/empresas` | Gestión de empresas | 🏢 |
| `/solicitudes/equipos` | Solicitudes de equipos | 📋 |
| `/solicitudes/torneos` | Solicitudes de torneos | 🏆 |

---

## 🚀 Funcionalidades Implementadas

✅ Crear cancha con todos los detalles
✅ Editar cancha existente
✅ Habilitar/Deshabilitar cancha
✅ Eliminar cancha (con confirmación)
✅ Gestionar horarios hora por hora
✅ Seleccionar días disponibles
✅ Mostrar estadísticas
✅ Modal para crear/editar
✅ Modal para confirmar eliminación
✅ Validación básica (campo nombre requerido)
✅ Diseño responsivo
✅ Gradientes y efectos visuales

---

## 📋 Pendientes para Backend

- [ ] Integrar con API REST
- [ ] Persistencia en base de datos
- [ ] Validación en servidor
- [ ] Autenticación y autorización
- [ ] Paginación de canchas
- [ ] Búsqueda y filtros
- [ ] Cálculo automático de ingresos
- [ ] Sincronización de horarios con reservas
- [ ] Exportar/Importar horarios
- [ ] Plantillas de horarios recurrentes

---

## 🎓 Notas Técnicas

### Binding Two-Way
```razor
@bind="_formCancha.Nombre"        // Actualización automática
@bind="_formCancha.PrecioHora"
@bind="_formCancha.HoraApertura"
```

### Event Handlers
```razor
@onclick="AbrirModalCrearCancha"
@onclick="() => AbrirModalEditar(cancha)"
@onchange="@((ChangeEventArgs e) => CambiarDia(dia, (bool)e.Value))"
```

### Condicionales de Renderizado
```razor
@if (cancha.Activa) { ... }
@if (_mostrarModalCancha) { ... }
```

---

**Documentación Actualizada:** 13 de Noviembre, 2025
**Versión:** 1.0 - Prototipo Funcional Completo
