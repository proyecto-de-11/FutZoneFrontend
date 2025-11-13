
# 🎯 DIAGRAMAS VISUALES DE PROTOTIPOS - FutZone

## 1. FLUJO GENERAL DEL SISTEMA

```
                          ┌─────────────────┐
                          │   USUARIO ADMIN │
                          └────────┬────────┘
                                   │
                    ┌──────────────┼──────────────┐
                    │              │              │
                    ▼              ▼              ▼
          ┌──────────────────┐  ┌──────────┐  ┌──────────┐
          │  DASHBOARD ADMIN │  │SOLICITUD │  │SOLICITUD │
          │    /dashboard    │  │ EQUIPOS  │  │TORNEOS   │
          │                  │  │/equipos  │  │/torneos  │
          └──────┬───────────┘  └────┬─────┘  └────┬─────┘
                 │                   │             │
         ┌───────┼───────┐      ┌────┴────┐       │
         │       │       │      │         │       │
         ▼       ▼       ▼      ▼         ▼       ▼
      ┌─────┐┌─────┐┌──────┐┌──────┐┌────────┐┌──────┐
      │ROLES││EMPR.││STATS.││CREAR ││FILTRAR ││CREAR │
      │CRUD ││CRUD ││      ││SOL.  ││ESTADOS ││CONV. │
      └─────┘└─────┘└──────┘└──────┘└────────┘└──────┘
```

---

## 2. ESTRUCTURA DE DASHBOARD ADMINISTRATIVO

```
┌─────────────────────────────────────────────────────────────────────┐
│                  DASHBOARD ADMINISTRATIVO                           │
│              /admin/dashboard                                        │
├─────────────────────────────────────────────────────────────────────┤
│
│ ╔════════════════════════════════════════════════════════════════╗
│ ║ 🎨 HEADER GRADIENT (Púrpura)                                  ║
│ ║ "⚽ Dashboard de Propietario de Publicidad"                   ║
│ ║ [+ Nueva Empresa]                                            ║
│ ╚════════════════════════════════════════════════════════════════╝
│
│ ┌──────────────┐  ┌──────────────┐  ┌──────────────┐  ┌──────────────┐
│ │   TARJETA    │  │   TARJETA    │  │   TARJETA    │  │   TARJETA    │
│ │   Empresas   │  │   Usuarios   │  │   Reservas   │  │   Ingresos   │
│ │   Activas    │  │   Totales    │  │  Este Mes    │  │  Mensuales   │
│ │      8       │  │      156     │  │      245     │  │   $12,500    │
│ │   [🏢]       │  │   [👥]       │  │   [📅]       │  │   [💰]       │
│ └──────────────┘  └──────────────┘  └──────────────┘  └──────────────┘
│
│ ┌──────────────────────────────────┐  ┌──────────────────────────────┐
│ │                                  │  │                              │
│ │   TABLA: EMPRESAS REGISTRADAS    │  │   PANEL LATERAL              │
│ │                                  │  │                              │
│ │  ┌──────────────────────────┐   │  │  ┌──────────────────────┐   │
│ │  │ Nombre│RUC│Empl│Estado  │   │  │  │ ACTIVIDAD RECIENTE   │   │
│ │  ├──────────────────────────┤   │  │  ├──────────────────────┤   │
│ │  │Tech Sol│20123..│50│✓Activa│  │  │  │📌 Nueva empresa      │   │
│ │  │SportZone│20987..│35│✓Activa│  │  │  │✅ Reserva confirmada│   │
│ │  │FutAcademy│20555..│25│✓Activa│ │  │  │👤 Usuario nuevo      │   │
│ │  │EventCenter│20111..│40│✗Inact│  │  │  │💳 Pago procesado     │   │
│ │  │┌──────────────────────────┐  │  │  └──────────────────────┘   │
│ │  │ [Editar] [Eliminar]      │  │  │                              │
│ │  └──────────────────────────┘  │  │  ┌──────────────────────┐   │
│ │                                  │  │  │ ROLES DEL SISTEMA    │   │
│ │  [+ Nueva Empresa]               │  │  ├──────────────────────┤   │
│ │                                  │  │  │🔐 Administrador (5)  │   │
│ │                                  │  │  │👔 Gerente (12)       │   │
│ └──────────────────────────────────┘  │  │👥 Usuario (139)      │   │
│                                        │  │[+ Nuevo Rol]         │   │
│                                        │  └──────────────────────┘   │
│                                        │                              │
│                                        └──────────────────────────────┘
│
└─────────────────────────────────────────────────────────────────────┘
```

---

## 3. ESTRUCTURA DE SOLICITUDES DE EQUIPOS

```
┌────────────────────────────────────────────────────────────────┐
│            SOLICITUDES DE EQUIPOS A TORNEOS                    │
│              /solicitudes/equipos                              │
├────────────────────────────────────────────────────────────────┤
│
│ ╔════════════════════════════════════════════════════════════╗
│ ║ 🎨 HEADER GRADIENT (Verde)                                ║
│ ║ "📋 Solicitudes de Equipos a Torneos"                     ║
│ ║ [+ Nueva Solicitud]                                      ║
│ ╚════════════════════════════════════════════════════════════╝
│
│ ┌──────────────────────────────────────────────────────────┐
│ │ TAB NAVIGATION:                                          │
│ │ ⏳ Pendientes (2) │ ✅ Aprobadas (1) │ ❌ Rechazadas (1) │ ➕ Todas (4)
│ └──────────────────────────────────────────────────────────┘
│
│ ┌──────────────────┐  ┌──────────────────┐  ┌──────────────────┐
│ │                  │  │                  │  │                  │
│ │ ╔════════════════╗  │ ╔════════════════╗  │ ╔════════════════╗
│ │ ║ Los Campeones ║  │ ║ Juventud Deport║  │ ║ FC Fuerte      ║
│ │ ║ Torneo Nacional║  │ ║ Copa Libertad. ║  │ ║ Camp. Regional ║
│ │ ╚════════════════╝  │ ╚════════════════╝  │ ╚════════════════╝
│ │                  │  │                  │  │                  │
│ │ 🏆 Categoría:    │  │ 🏆 Categoría:    │  │ 🏆 Categoría:    │
│ │    Mayores       │  │    Sub-20        │  │    Sub-17        │
│ │                  │  │                  │  │                  │
│ │ 👥 Jugadores: 18 │  │ 👥 Jugadores: 16 │  │ 👥 Jugadores: 14 │
│ │ 📅 Fecha: XX/XX  │  │ 📅 Fecha: XX/XX  │  │ 📅 Fecha: XX/XX  │
│ │                  │  │                  │  │                  │
│ │ Estado:          │  │ Estado:          │  │ Estado:          │
│ │ ⏳ PENDIENTE    │  │ ✅ APROBADA      │  │ ❌ RECHAZADA     │
│ │                  │  │                  │  │                  │
│ │ ┌────────────────┐ │ ┌────────────────┐ │ ┌────────────────┐
│ │ │[Aprobar]       │ │ │[Ver Detalles]  │ │ │[Ver Detalles]  │
│ │ │[Rechazar]      │ │ │[Eliminar]      │ │ │[Eliminar]      │
│ │ │[Ver]           │ │ │                │ │ │                │
│ │ │[Eliminar]      │ │ │                │ │ │                │
│ │ └────────────────┘ │ └────────────────┘ │ └────────────────┘
│ │                  │  │                  │  │                  │
│ └──────────────────┘  └──────────────────┘  └──────────────────┘
│
└────────────────────────────────────────────────────────────────┘
```

---

## 4. ESTRUCTURA DE SOLICITUDES DE TORNEOS

```
┌────────────────────────────────────────────────────────────────┐
│            SOLICITUDES DE TORNEOS A EQUIPOS                    │
│              /solicitudes/torneos                              │
├────────────────────────────────────────────────────────────────┤
│
│ ╔════════════════════════════════════════════════════════════╗
│ ║ 🎨 HEADER GRADIENT (Azul)                                 ║
│ ║ "🏆 Solicitudes de Torneos a Equipos"                     ║
│ ║ [+ Nueva Convocatoria]                                   ║
│ ╚════════════════════════════════════════════════════════════╝
│
│ ┌──────────────────────────────────────────────────────────┐
│ │ TAB NAVIGATION:                                          │
│ │ ⏳ Pendientes (1) │ 👍 Aceptadas (2) │ 👎 Rechazadas (1) │ ➕ Todas (4)
│ └──────────────────────────────────────────────────────────┘
│
│ ┌──────────────────┐  ┌──────────────────┐  ┌──────────────────┐
│ │                  │  │                  │  │                  │
│ │ ╔════════════════╗  │ ╔════════════════╗  │ ╔════════════════╗
│ │ ║ 🏆 Torneo Nac. ║  │ ║ 🏆 Copa Libertad║  │ ║ 🏆 Camp. Reg.  ║
│ │ ║ para Los Campeón║ │ ║ para Juventud D.║  │ ║ para FC Fuerte ║
│ │ ╚════════════════╝  │ ╚════════════════╝  │ ╚════════════════╝
│ │                  │  │                  │  │                  │
│ │ 🏟️ Categoría:    │  │ 🏟️ Categoría:    │  │ 🏟️ Categoría:    │
│ │    Mayores       │  │    Sub-20        │  │    Sub-17        │
│ │                  │  │                  │  │                  │
│ │ 🎟️ Cupos: 20    │  │ 🎟️ Cupos: 16    │  │ 🎟️ Cupos: 14    │
│ │ 📅 Fecha: XX/XX  │  │ 📅 Fecha: XX/XX  │  │ 📅 Fecha: XX/XX  │
│ │                  │  │                  │  │                  │
│ │ Estado:          │  │ Estado:          │  │ Estado:          │
│ │ ⏳ PENDIENTE    │  │ 👍 ACEPTADA      │  │ 👍 ACEPTADA      │
│ │                  │  │                  │  │                  │
│ │ ┌────────────────┐ │ ┌────────────────┐ │ ┌────────────────┐
│ │ │[Aceptar]       │ │ │[Ver Detalles]  │ │ │[Ver Detalles]  │
│ │ │[Rechazar]      │ │ │[Eliminar]      │ │ │[Eliminar]      │
│ │ │[Ver]           │ │ │                │ │ │                │
│ │ │[Eliminar]      │ │ │                │ │ │                │
│ │ └────────────────┘ │ └────────────────┘ │ └────────────────┘
│ │                  │  │                  │  │                  │
│ └──────────────────┘  └──────────────────┘  └──────────────────┘
│
└────────────────────────────────────────────────────────────────┘
```

---

## 5. FLUJO DE MODALES

```
┌─────────────────────────────────────────────────────────────────┐
│                    MODALES DEL SISTEMA                          │
└─────────────────────────────────────────────────────────────────┘

DASHBOARD ADMIN - MODAL EMPRESA:
┌──────────────────────────────────────────┐
│ Crear Nueva Empresa                 [X]  │
├──────────────────────────────────────────┤
│                                          │
│ Nombre de la Empresa:                    │
│ ┌──────────────────────────────────────┐ │
│ │ Ej. Tech Solutions                   │ │
│ └──────────────────────────────────────┘ │
│                                          │
│ Descripción:                             │
│ ┌──────────────────────────────────────┐ │
│ │ Describe brevemente tu empresa...    │ │
│ └──────────────────────────────────────┘ │
│                                          │
│ Número de Empleados:                     │
│ ┌──────────────────────────────────────┐ │
│ │ Ej. 50                               │ │
│ └──────────────────────────────────────┘ │
│                                          │
│ ☑ Empresa Activa                         │
│                                          │
│ ┌──────────────┐  ┌──────────────────┐  │
│ │   Cancelar   │  │ Crear Empresa    │  │
│ └──────────────┘  └──────────────────┘  │
│                                          │
└──────────────────────────────────────────┘

SOLICITUDES EQUIPOS - MODAL SOLICITUD:
┌──────────────────────────────────────────┐
│ Crear Nueva Solicitud               [X]  │
├──────────────────────────────────────────┤
│                                          │
│ Nombre del Equipo:                       │
│ ┌──────────────────────────────────────┐ │
│ │ Ej. Los Campeones                    │ │
│ └──────────────────────────────────────┘ │
│                                          │
│ Torneo:                                  │
│ ┌──────────────────────────────────────┐ │
│ │▼ -- Seleccionar Torneo --           │ │
│ │  • Torneo Nacional 2025             │ │
│ │  • Copa Libertadores                │ │
│ │  • Campeonato Regional              │ │
│ └──────────────────────────────────────┘ │
│                                          │
│ Categoría:                               │
│ ┌──────────────────────────────────────┐ │
│ │▼ -- Seleccionar Categoría --        │ │
│ │  • Sub-17                           │ │
│ │  • Sub-20                           │ │
│ │  • Mayores                          │ │
│ └──────────────────────────────────────┘ │
│                                          │
│ Número de Jugadores:                     │
│ ┌──────────────────────────────────────┐ │
│ │ 11                                   │ │
│ └──────────────────────────────────────┘ │
│                                          │
│ Comentario:                              │
│ ┌──────────────────────────────────────┐ │
│ │ Ingresa comentarios adicionales...   │ │
│ └──────────────────────────────────────┘ │
│                                          │
│ ┌──────────────┐  ┌──────────────────┐  │
│ │   Cancelar   │  │ Enviar Solicitud │  │
│ └──────────────┘  └──────────────────┘  │
│                                          │
└──────────────────────────────────────────┘
```

---

## 6. ESTADOS Y TRANSICIONES

```
SOLICITUD DE EQUIPO - CICLO DE VIDA:

    ┌──────────────┐
    │   CREADA     │
    └──────┬───────┘
           │
           ▼
    ┌──────────────┐
    │  PENDIENTE   │◄───────────────────────────────┐
    │ (Naranja)    │                                 │
    └──────┬───────┘                                 │
           │                                         │
    ┌──────┴──────────────────────────┐             │
    │                                  │             │
    ▼                                  ▼             │
┌──────────────┐              ┌──────────────┐      │
│  APROBADA    │              │ RECHAZADA    │      │
│  (Verde)     │              │  (Rojo)      │      │
└──────────────┘              └──────────────┘      │
                                     │              │
                                     │ Recurso      │
                                     └──────────────┘

CONVOCATORIA DE TORNEO - CICLO DE VIDA:

    ┌──────────────┐
    │   CREADA     │
    └──────┬───────┘
           │
           ▼
    ┌──────────────┐
    │  PENDIENTE   │◄───────────────────────────────┐
    │ (Naranja)    │                                 │
    └──────┬───────┘                                 │
           │                                         │
    ┌──────┴──────────────────────────┐             │
    │                                  │             │
    ▼                                  ▼             │
┌──────────────┐              ┌──────────────┐      │
│  ACEPTADA    │              │ RECHAZADA    │      │
│  (Azul)      │              │  (Rojo)      │      │
└──────────────┘              └──────────────┘      │
                                     │              │
                                     │ Reintentar   │
                                     └──────────────┘
```

---

## 7. COMPONENTES REUTILIZABLES

```
┌─────────────────────────────────────────┐
│   TARJETA DE ESTADÍSTICAS               │
├─────────────────────────────────────────┤
│                                         │
│  [Icono]    Título                      │
│             Descripción Pequeña         │
│             Número Grande               │
│                                         │
│  • Colores: Gradientes personalizados   │
│  • Efecto: Hover con elevación          │
│  • Tamaño: Responsivo                   │
│                                         │
└─────────────────────────────────────────┘

┌─────────────────────────────────────────┐
│   TARJETA DE SOLICITUD/CONVOCATORIA     │
├─────────────────────────────────────────┤
│                                         │
│  [Borde Color] Título Principal         │
│                Subtítulo                │
│  [Estado Badge]                         │
│                                         │
│  • Campo 1: Valor 1                     │
│  • Campo 2: Valor 2                     │
│  • Campo 3: Valor 3                     │
│                                         │
│  Descripción o Comentario...            │
│                                         │
│  [Botón 1] [Botón 2] [Botón 3]         │
│                                         │
│  • Border-left: Color según estado      │
│  • Efecto: Hover con sombra             │
│  • Tamaño: Flexible                     │
│                                         │
└─────────────────────────────────────────┘

┌─────────────────────────────────────────┐
│   MODAL ESTÁNDAR                        │
├─────────────────────────────────────────┤
│                                         │
│  Título del Modal            [X]        │
│  ├─ Header Color                        │
│  ├─ Header Text White                   │
│                                         │
│  • Campos de Formulario                 │
│  • Input, Select, Textarea              │
│  • Validaciones                         │
│                                         │
│  [Cancelar] [Guardar/Enviar]           │
│                                         │
│  • Backdrop: rgba(0,0,0,0.5)           │
│  • Z-index: Alto                        │
│  • Centrado: Verticalmente              │
│                                         │
└─────────────────────────────────────────┘
```

---

## 8. PALETA DE COLORES COMPLETA

```
GRADIENTES PRIMARIOS:
┌─────────────────────────────────────────────────────┐
│ Dashboard:    #667eea ──→ #764ba2 (Púrpura)      │
│ Equipos:      #10b981 ──→ #059669 (Verde)        │
│ Torneos:      #3b82f6 ──→ #1e40af (Azul)         │
└─────────────────────────────────────────────────────┘

ESTADOS:
┌─────────────────────────────────────────────────────┐
│ ⏳ Pendiente:     #f59e0b (Naranja)                │
│ ✅ Aprobada:     #10b981 (Verde)                  │
│ 👍 Aceptada:     #3b82f6 (Azul)                   │
│ ❌ Rechazada:    #ef4444 (Rojo)                   │
│ 🔄 En Proceso:   #8b5cf6 (Púrpura)               │
└─────────────────────────────────────────────────────┘

NEUTRALES:
┌─────────────────────────────────────────────────────┐
│ Fondo Principal:  #f8f9fa (Gris Muy Claro)        │
│ Fondo Secundario: #f3f4f6 (Gris Claro)            │
│ Texto Principal:  #374151 (Gris Oscuro)           │
│ Texto Secundario: #6b7280 (Gris Medio)            │
│ Bordes:          #e5e7eb (Gris Ligero)            │
│ Blanco:          #ffffff (Puro)                    │
└─────────────────────────────────────────────────────┘
```

---

## 9. ICONOGRAFÍA UTILIZADA

```
Navegación:
🏠 Home                    📋 Solicitudes
⚙️ Dashboard               🔐 Seguridad
👔 Roles                   📝 Formularios
🏢 Empresas

Estados:
⏳ Pendiente               ❌ Rechazada
✅ Aprobada                👍 Aceptada
👎 Rechazada              🔄 En Proceso

Acciones:
✏️ Editar                  🗑️ Eliminar
👁️ Ver Detalles            ➕ Agregar
🔌 Cambiar Estado          💾 Guardar
❌ Cancelar                ✓ Confirmar

Datos:
🏆 Torneo                  👥 Usuarios
👤 Usuario                 💰 Dinero
📅 Fecha                   📞 Teléfono
📍 Ubicación               🎯 Categoría
🎟️ Cupos                   📊 Estadísticas
📌 Información             💡 Idea
```

---

**Documentación Actualizada:** 11 de Noviembre, 2025
**Versión:** 1.0 - Prototipos Completos
