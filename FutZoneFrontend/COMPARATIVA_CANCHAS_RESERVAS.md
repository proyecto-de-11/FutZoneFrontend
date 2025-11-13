# 🔄 COMPARATIVA: MIS CANCHAS vs MIS RESERVAS

## 📊 Tabla Comparativa Rápida

| Aspecto | Mis Canchas | Mis Reservas |
|---------|-----------|--------------|
| **Ruta** | `/empresa/canchas` | `/empresa/reservas` |
| **Propósito** | Gestionar canchas (CRUD) | Gestionar reservas (Aprobar/Rechazar) |
| **Qué Gestionas** | Tus canchas | Solicitudes de clientes |
| **Datos Principales** | Nombre, ubicación, horarios | Cliente, fecha, monto |
| **Acciones Principales** | Crear, Editar, Eliminar | Aprobar, Rechazar |
| **Estadísticas** | Canchas activas, horas libres, ingresos | Pendientes, aprobadas, rechazadas, ingresos |
| **Filtros** | No tiene | 3 filtros avanzados |
| **Modals** | Sí (crear/editar) | No (solo lectura) |
| **Edición Inline** | No (modal) | No (estado fijo) |
| **Reversible** | Sí (edita después) | No (bloqueada tras respuesta) |

---

## 🎯 Flujo de Uso

### Proceso Completo en Tu Negocio

```
┌─────────────────────────────────────────────────────────┐
│  1. CREAR TUS CANCHAS                                   │
│     └─> Ve a "MIS CANCHAS"                             │
│         └─> [+ Nueva Cancha]                           │
│             └─> Llena todos los datos                  │
│                 └─> Selecciona horarios y días         │
│                     └─> Publica                        │
└─────────────────────────────────────────────────────────┘
                           ⬇️
┌─────────────────────────────────────────────────────────┐
│  2. CLIENTES VEN TUS CANCHAS Y RESERVAN                 │
│     └─> Tu cancha aparece en el sistema                │
│         └─> Clientes hacen solicitudes                 │
│             └─> Tú recibes notificaciones              │
└─────────────────────────────────────────────────────────┘
                           ⬇️
┌─────────────────────────────────────────────────────────┐
│  3. RESPONDER A RESERVAS                                │
│     └─> Ve a "MIS RESERVAS"                            │
│         └─> Ves todas las solicitudes                  │
│             └─> [✅ APROBAR] o [❌ RECHAZAR]          │
│                 └─> Estado cambia automáticamente      │
└─────────────────────────────────────────────────────────┘
                           ⬇️
┌─────────────────────────────────────────────────────────┐
│  4. CLIENTE RECIBE RESPUESTA                            │
│     └─> Notificación automática                        │
│         └─> Ve si fue aprobada o rechazada             │
│             └─> Si fue aprobada: procede al pago       │
└─────────────────────────────────────────────────────────┘
                           ⬇️
┌─────────────────────────────────────────────────────────┐
│  5. TENER INGRESOS                                      │
│     └─> En "MIS RESERVAS" ves: 💰 Ingresos             │
│         └─> Dinero de reservas aprobadas               │
│             └─> En "MIS CANCHAS" ves: 💰 Ingresos      │
└─────────────────────────────────────────────────────────┘
```

---

## 🏗️ Estructura Técnica Comparada

### MIS CANCHAS

```
┌─────────────────────────────────────────┐
│ COMPONENTE: MisCanchas.razor            │
├─────────────────────────────────────────┤
│ Header                                  │
├─────────────────────────────────────────┤
│ 4 Stat Cards                            │
│ ├─ Canchas Activas                      │
│ ├─ Reservas Hoy                         │
│ ├─ Horas Libres                         │
│ └─ Ingresos (Mes)                       │
├─────────────────────────────────────────┤
│ GRID DE CANCHAS (3 columnas)            │
│ ├─ Card Cancha 1                        │
│ ├─ Card Cancha 2                        │
│ └─ Card Cancha 3                        │
├─────────────────────────────────────────┤
│ Modal: CREAR/EDITAR CANCHA              │
│ Modal: CONFIRMAR ELIMINACIÓN            │
└─────────────────────────────────────────┘
```

### MIS RESERVAS

```
┌─────────────────────────────────────────┐
│ COMPONENTE: MisReservas.razor           │
├─────────────────────────────────────────┤
│ Header                                  │
├─────────────────────────────────────────┤
│ 4 Stat Cards                            │
│ ├─ Pendientes                           │
│ ├─ Aprobadas                            │
│ ├─ Rechazadas                           │
│ └─ Ingresos                             │
├─────────────────────────────────────────┤
│ 3 FILTROS                               │
│ ├─ Por Estado                           │
│ ├─ Por Cancha                           │
│ └─ Por Ordenamiento                     │
├─────────────────────────────────────────┤
│ GRID DE RESERVAS (1-3 columnas)         │
│ ├─ Card Reserva 1                       │
│ ├─ Card Reserva 2                       │
│ └─ Card Reserva 3                       │
└─────────────────────────────────────────┘
```

---

## 📈 Objetos Mostrados

### MIS CANCHAS: Objeto "Cancha"

```
Cancha
├─ Id: int
├─ Nombre: string
├─ Ubicacion: string
├─ Dimensiones: string (ej: "8x44")
├─ TipoSuperficie: string
├─ PrecioHora: decimal
├─ Capacidad: int
├─ HoraApertura: string
├─ HoraCierre: string
├─ Descripcion: string
├─ Activa: bool
├─ Horarios: List<HorarioDisponible>
│  └─ Hora: string
│  └─ Disponible: bool
└─ DiasDisponibles: List<string>
```

**Total de propiedades por cancha: 11**

### MIS RESERVAS: Objeto "Reserva"

```
Reserva
├─ Id: int
├─ CanchaId: int
├─ CanchaNombre: string
├─ NombreCliente: string
├─ EmailCliente: string
├─ TelefonoCliente: string
├─ Fecha: DateTime
├─ HoraInicio: string
├─ HoraFin: string
├─ Duracion: int
├─ CantidadJugadores: int
├─ PrecioHora: decimal
├─ Descuento: decimal
├─ PrecioTotal: decimal
├─ NotasCliente: string
├─ Estado: string (Pendiente/Aprobada/Rechazada)
├─ FechaSolicitud: DateTime
└─ FechaRespuesta: string
```

**Total de propiedades por reserva: 17**

---

## 🔧 Métodos Disponibles

### MIS CANCHAS

| Método | Qué Hace |
|--------|----------|
| `CargarCanchas()` | Carga datos iniciales |
| `GenerarHorarios()` | Genera 18 horas disponibles |
| `AbrirModalCrearCancha()` | Abre modal para crear |
| `AbrirModalEditar(cancha)` | Abre modal con datos precargados |
| `GuardarCancha()` | Crea o actualiza |
| `CancelarEdicion()` | Cierra modal |
| `DeshabilitarCancha(cancha)` | Cambia estado a deshabilitada |
| `HabilitarCancha(cancha)` | Cambia estado a habilitada |
| `AbrirModalConfirmacion(cancha)` | Abre diálogo de confirmación |
| `ConfirmarEliminar()` | Ejecuta eliminación |
| `EliminarCancha(cancha)` | Elimina de la lista |
| `CambiarDia(dia)` | Toggle disponibilidad día |
| `CambiarHorario(hora)` | Toggle disponibilidad hora |

**Total de métodos: 13**

### MIS RESERVAS

| Método | Qué Hace |
|--------|----------|
| `CargarDatos()` | Carga canchas y reservas |
| `FiltrarPorEstado(ChangeEventArgs)` | Aplica filtro de estado |
| `FiltrarPorCancha(ChangeEventArgs)` | Aplica filtro de cancha |
| `OrdenarReservas(ChangeEventArgs)` | Aplica ordenamiento |
| `AplicarFiltros()` | Combina filtros y ordena |
| `AprobarReserva(int id)` | Aprueba la reserva |
| `RechazarReserva(int id)` | Rechaza la reserva |
| `GetEstadoIcono(string estado)` | Retorna icono del estado |

**Total de métodos: 8**

---

## 🎨 Estilos Comparados

### MIS CANCHAS

- **Archivo:** `/wwwroot/css/canchas.css`
- **Líneas:** 220
- **Colores Principales:**
  - Gradiente: #667eea → #764ba2
  - Activa: #10b981 (verde)
  - Deshabilitada: #ef4444 (rojo)

### MIS RESERVAS

- **Archivo:** `/wwwroot/css/reservas.css`
- **Líneas:** 450
- **Colores Principales:**
  - Gradiente: #667eea → #764ba2
  - Pendiente: #f59e0b (naranja)
  - Aprobada: #10b981 (verde)
  - Rechazada: #ef4444 (rojo)
  - Ingresos: #8b5cf6 (púrpura)

---

## 📱 Responsividad Comparada

### MIS CANCHAS

```
Desktop (>1200px)     → 3 columnas de canchas
Laptop (992px)        → 2 columnas
Tablet (768px)        → 2 columnas
Móvil (<768px)        → 1 columna
```

### MIS RESERVAS

```
Desktop (>1200px)     → 3 columnas de reservas
Laptop (992px)        → 2 columnas
Tablet (768px)        → 2 columnas
Móvil (<768px)        → 1 columna
Extra pequeño (<480px) → Optimizado
```

Ambas son **100% responsivas.**

---

## 🚀 Acciones Disponibles

### MIS CANCHAS

```
CREAR  → [+ Nueva Cancha]
LEER   → Ver todas las canchas
EDITAR → [✏️ Editar]
DELETE → [🗑️ Eliminar]
TOGGLE → [🔒 Deshabilitar] / [🔓 Habilitar]
```

**CRUD Completo**

### MIS RESERVAS

```
LEER   → Ver todas las reservas
UPDATE → [✅ APROBAR] o [❌ RECHAZAR]
FILTRAR → 3 filtros diferentes
ORDENAR → 4 opciones de orden
```

**Operaciones Read + Update enfocadas**

---

## 📊 Datos de Ejemplo

### MIS CANCHAS

```
3 canchas precargadas:
├─ Cancha Principal (ACTIVA)
├─ Cancha Secundaria (ACTIVA)
└─ Cancha Entrenamiento (DESHABILITADA)
```

### MIS RESERVAS

```
5 reservas precargadas:
├─ Juan García (PENDIENTE)
├─ Carlos Martínez (APROBADA)
├─ Ana López (RECHAZADA)
├─ David Sánchez (PENDIENTE)
└─ Miguel Rodríguez (APROBADA)
```

---

## 🔗 Relación Entre Ambas

```
┌──────────────────────────────────────────────┐
│ MIS CANCHAS (Lo que posees)                  │
│                                              │
│  Cancha Principal ──────────┐               │
│  Cancha Secundaria ─────┐   │               │
│  Cancha Entrenamiento   │   │               │
└────────────────────────┼──┼─────────────────┘
                         │  │
                         │  │ Las canchas
                         │  │ generan
                         │  │ reservas
                         │  │
                         ▼  ▼
┌──────────────────────────────────────────────┐
│ MIS RESERVAS (Lo que reciben)                │
│                                              │
│  Reserva 1 (para Cancha Principal)         │
│  Reserva 4 (para Cancha Secundaria)        │
│  ...                                        │
└──────────────────────────────────────────────┘
```

---

## ⚙️ Integración en el Sistema

### Rutas Registradas

```
✅ /empresa/canchas    → MisCanchas.razor
✅ /empresa/reservas   → MisReservas.razor
```

### CSS Enlazados

```
✅ /wwwroot/css/canchas.css   → App.razor
✅ /wwwroot/css/reservas.css  → App.razor
```

### Navegación

```
NavMenu.razor
├─ ⚽ Mis Canchas    → /empresa/canchas
└─ 📅 Mis Reservas  → /empresa/reservas
```

---

## 🎯 Casos de Uso

### Usar MIS CANCHAS Cuando:

- Quieras crear una nueva cancha
- Necesites editar información de una cancha
- Quieras cambiar horarios y días
- Necesites deshabilitar una cancha temporalmente
- Quieras eliminar una cancha
- Necesites ver cuántas canchas tienes activas

### Usar MIS RESERVAS Cuando:

- Tengas solicitudes de reserva pendientes
- Necesites responder a un cliente
- Quieras ver quién está reservando
- Necesites verificar si un cliente puede pagar
- Quieras conocer tus ingresos
- Necesites contactar a un cliente

---

## 📈 Estadísticas en Ambas

### MIS CANCHAS

```
Card 1: Canchas Activas
        └─ Total de canchas habilitadas

Card 2: Reservas Hoy
        └─ Cuántas reservas hay HOY

Card 3: Horas Libres
        └─ Total de horas sin reservar

Card 4: Ingresos (Mes)
        └─ Dinero estimado del mes
```

### MIS RESERVAS

```
Card 1: Pendientes de Aprobación
        └─ Espera tu respuesta

Card 2: Reservas Aprobadas
        └─ Confirmadas

Card 3: Reservas Rechazadas
        └─ Denegadas

Card 4: Ingresos (Aprobadas)
        └─ Dinero de reservas confirmadas
```

---

## 🔄 Flujo de Datos

```
┌─────────────────────────────────────┐
│ MIS CANCHAS                         │
│                                     │
│ Define tu oferta:                  │
│ - Cuántas canchas tienes           │
│ - Cuándo están disponibles         │
│ - A qué precio                     │
│ - Con qué especificaciones         │
└─────────────────────────────────────┘
              ▼
┌─────────────────────────────────────┐
│ SISTEMA (Backend)                   │
│                                     │
│ Publica tus canchas                │
│ Clientes pueden verlas             │
│ Clientes hacen solicitudes         │
└─────────────────────────────────────┘
              ▼
┌─────────────────────────────────────┐
│ MIS RESERVAS                        │
│                                     │
│ Recibes solicitudes                │
│ Evaluás cada una                   │
│ Apruebas o rechazas                │
│ Generás ingresos                   │
└─────────────────────────────────────┘
```

---

## 💡 Tips Combinados

### Para Maximizar Ingresos:

1. **En MIS CANCHAS:**
   - Crea todas tus canchas
   - Configura horarios estratégicos
   - Establece precios competitivos
   - Actualiza disponibilidad según demanda

2. **En MIS RESERVAS:**
   - Responde RÁPIDO (en <1 hora)
   - Aprueba clientes de confianza
   - Rechaza con educación cuando no puedas
   - Monitorea tus ingresos totales

3. **Resultado:**
   - Más clientes satisfechos
   - Mejor reputación
   - Más ingresos
   - Mejor ocupación de canchas

---

## 📚 Documentación

| Documento | Relacionado con |
|-----------|-----------------|
| DOCUMENTACION_CANCHAS.md | Mis Canchas |
| DOCUMENTACION_RESERVAS.md | Mis Reservas |
| GUIA_USO_CANCHAS.md | Mis Canchas |
| GUIA_USO_RESERVAS.md | Mis Reservas |
| LAYOUT_VISUAL_CANCHAS.txt | Mis Canchas |
| LAYOUT_VISUAL_RESERVAS.txt | Mis Reservas |

---

## ✅ Checklist de Uso Recomendado

```
□ 1. Entra a "Mis Canchas"
□ 2. Crea al menos 1 cancha
□ 3. Configura horarios y días
□ 4. Ve a "Mis Reservas"
□ 5. Observa las 5 reservas de ejemplo
□ 6. Aprueba 1 reserva
□ 7. Rechaza 1 reserva
□ 8. Usa los filtros
□ 9. Ordena por precio mayor
□ 10. Verifica que las estadísticas cambien
```

---

**Documento Generado:** 13 de Noviembre, 2025  
**Versión:** 1.0 - Comparativa Completa  
**Audiencia:** Desarrolladores y Usuarios Avanzados
