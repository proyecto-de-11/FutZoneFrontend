# 📱 Prototipos de FutZone Frontend - Documentación Completa

## 🎯 Descripción General
Este documento describe los 3 prototipos principales del sistema FutZone Frontend para gestión de reservas de canchas de fútbol.

---

## 1️⃣ DASHBOARD ADMINISTRATIVO
**Ruta:** `/admin/dashboard`

### 🎨 Características Visuales:
- **Header Gradient**: Gradiente púrpura (667eea → 764ba2)
- **Resolución:** Responsive (Desktop, Tablet, Mobile)

### 📊 Componentes Principales:

#### A. Tarjetas de Estadísticas (4 tarjetas)
```
┌─────────────────┐  ┌─────────────────┐  ┌─────────────────┐  ┌─────────────────┐
│ 🏢 Empresas     │  │ 👥 Usuarios     │  │ 📅 Reservas     │  │ 💰 Ingresos     │
│ Activas         │  │ Totales         │  │ Este Mes        │  │ Mensuales       │
│      8          │  │     156         │  │     245         │  │    $12,500      │
└─────────────────┘  └─────────────────┘  └─────────────────┘  └─────────────────┘
```

**Colores:**
- Empresas: Gradiente púrpura
- Usuarios: Gradiente rosa
- Reservas: Gradiente cian
- Ingresos: Gradiente naranja

#### B. Tabla de Empresas (8 columnas)
```
┌─────────────────────────────────────────────────────────────────┐
│ ID │ Nombre          │ RUC         │ Empleados │ Estado │ Acciones │
├─────────────────────────────────────────────────────────────────┤
│ 1  │ Tech Solutions  │ 20123456789 │    50     │ Activa │ ✏️ 🗑️   │
│ 2  │ SportZone       │ 20987654321 │    35     │ Activa │ ✏️ 🗑️   │
│ 3  │ FutAcademy      │ 20555666777 │    25     │ Activa │ ✏️ 🗑️   │
│ 4  │ EventCenter     │ 20111222333 │    40     │ Inactiva│ ✏️ 🗑️  │
└─────────────────────────────────────────────────────────────────┘
```

#### C. Panel Lateral Derecho
**Actividad Reciente:**
- 📌 Nueva empresa registrada (Hace 2 horas)
- ✅ Reserva confirmada (Hace 4 horas)
- 👤 Usuario nuevo (Hace 1 día)
- 💳 Pago procesado (Hace 1 día)

**Roles del Sistema:**
- 🔐 Administrador (5 usuarios)
- 👔 Gerente (12 usuarios)
- 👥 Usuario (139 usuarios)

#### D. Modales
1. **Crear/Editar Empresa**
   - Nombre de Empresa
   - Descripción
   - Número de Empleados
   - Checkbox Empresa Activa

2. **Crear Rol**
   - Nombre del Rol
   - Descripción

---

## 2️⃣ GESTIÓN DE ROLES
**Ruta:** `/admin/roles`

### 🎨 Características Visuales:
- **Header Color:** Azul oscuro
- **Tabla Responsiva**

### 📋 Estructura:
```
┌──────────────────────────────────────┐
│ Gestión de Roles │ + Nuevo Rol       │
├──────────────────────────────────────┤
│ ID │ Nombre      │ Descripción       │ Acciones │
├──────────────────────────────────────┤
│ 1  │ Admin       │ Administrador... │ ✏️ 🗑️   │
│ 2  │ Usuario     │ Usuario regular   │ ✏️ 🗑️   │
└──────────────────────────────────────┘
```

### 🎯 Funcionalidades:
- ✅ Ver lista de roles
- ✅ Crear nuevo rol (Modal)
- ✅ Editar rol
- ✅ Eliminar rol
- ✅ Validaciones básicas

---

## 3️⃣ GESTIÓN DE EMPRESAS
**Ruta:** `/admin/empresas`

### 🎨 Características Visuales:
- **Header Color:** Verde oscuro
- **Tabla Responsiva**

### 📋 Estructura:
```
┌────────────────────────────────────────────────────────┐
│ Gestión de Empresas │ + Nueva Empresa                 │
├────────────────────────────────────────────────────────┤
│ ID │ Nombre    │ RUC        │ Dirección │ Teléfono │ Estado │ Acciones │
├────────────────────────────────────────────────────────┤
│ 1  │ Empresa 1 │ 20123456789│ Av. 123  │ 999777888│ ✅    │ ✏️ 🔌 🗑️ │
└────────────────────────────────────────────────────────┘
```

### 🎯 Funcionalidades:
- ✅ Ver lista de empresas
- ✅ Crear nueva empresa (Modal)
- ✅ Editar empresa
- ✅ Cambiar estado (Activa/Inactiva)
- ✅ Eliminar empresa
- ✅ Mostrar estado con badges

---

## 4️⃣ SOLICITUDES DE EQUIPOS A TORNEOS
**Ruta:** `/solicitudes/equipos`

### 🎨 Características Visuales:
- **Header Gradient:** Verde (10b981 → 059669)
- **Tarjetas con bordes de color**
- **Sistema de Tabs para filtrado**

### 📊 Estados de Solicitudes:
| Estado | Color | Icono |
|--------|-------|-------|
| Pendiente | Naranja | ⏳ |
| Aprobada | Verde | ✅ |
| Rechazada | Rojo | ❌ |

### 📋 Layout de Tarjetas:
```
┌─────────────────────────────────────────┐
│ 📋 Solicitudes de Equipos a Torneos    │
│ + Nueva Solicitud                       │
├─────────────────────────────────────────┤
│
│ [Pendientes: 2] [Aprobadas: 1] [Rechazadas: 1] [Todas: 4]
│
│  ┌──────────────────┐  ┌──────────────────┐  ┌──────────────────┐
│  │ Los Campeones    │  │ Juventud Deport. │  │ FC Fuerte        │
│  │ Torneo Nacional  │  │ Copa Libertad.   │  │ Camp. Regional   │
│  │ Mayores - 18 jug │  │ Sub-20 - 16 jug  │  │ Sub-17 - 14 jug  │
│  │ Pendiente ⏳    │  │ Aprobada ✅      │  │ Rechazada ❌     │
│  │ [Aprobar][Rech.]│  │ [Ver] [Elim.]    │  │ [Ver] [Elim.]    │
│  └──────────────────┘  └──────────────────┘  └──────────────────┘
│
└─────────────────────────────────────────┘
```

### 🎯 Campos de Solicitud:
- Nombre del Equipo
- Nombre del Torneo
- Categoría (Sub-17, Sub-20, Mayores)
- Número de Jugadores
- Comentario
- Fecha de Solicitud
- Estado (Pendiente/Aprobada/Rechazada)

### 🔘 Acciones:
- **Pendientes:** [Aprobar] [Rechazar] [Ver] [Eliminar]
- **Aprobadas/Rechazadas:** [Ver] [Eliminar]

---

## 5️⃣ SOLICITUDES DE TORNEOS A EQUIPOS
**Ruta:** `/solicitudes/torneos`

### 🎨 Características Visuales:
- **Header Gradient:** Azul (3b82f6 → 1e40af)
- **Tarjetas con bordes de color**
- **Sistema de Tabs para filtrado**

### 📊 Estados de Convocatorias:
| Estado | Color | Icono |
|--------|-------|-------|
| Pendiente | Naranja | ⏳ |
| Aceptada | Azul | 👍 |
| Rechazada | Rojo | 👎 |

### 📋 Layout de Tarjetas:
```
┌─────────────────────────────────────────┐
│ 🏆 Solicitudes de Torneos a Equipos    │
│ + Nueva Convocatoria                    │
├─────────────────────────────────────────┤
│
│ [Pendientes: 1] [Aceptadas: 2] [Rechazadas: 1] [Todas: 4]
│
│  ┌──────────────────┐  ┌──────────────────┐  ┌──────────────────┐
│  │ 🏆 Torneo Nac.   │  │ 🏆 Copa Libertad │  │ 🏆 Camp. Regional│
│  │ para Los Campeon │  │ para Juventud    │  │ para FC Fuerte   │
│  │ Mayores - 20 cuo │  │ Sub-20 - 16 cuo  │  │ Sub-17 - 14 cuo  │
│  │ Pendiente ⏳    │  │ Aceptada 👍     │  │ Aceptada 👍      │
│  │ [Aceptar][Rech.]│  │ [Ver] [Elim.]    │  │ [Ver] [Elim.]    │
│  └──────────────────┘  └──────────────────┘  └──────────────────┘
│
└─────────────────────────────────────────┘
```

### 🎯 Campos de Convocatoria:
- Nombre del Torneo
- Equipo a Convocar
- Categoría (Sub-17, Sub-20, Mayores)
- Cupos Disponibles
- Fecha del Torneo
- Descripción
- Fecha de Convocatoria
- Estado (Pendiente/Aceptada/Rechazada)

### 🔘 Acciones:
- **Pendientes:** [Aceptar] [Rechazar] [Ver] [Eliminar]
- **Aceptadas/Rechazadas:** [Ver] [Eliminar]

---

## 🗺️ Mapa de Navegación

```
┌─────────────────────────────────────────────────────────────────┐
│                      MENÚ PRINCIPAL                             │
├─────────────────────────────────────────────────────────────────┤
│
│  🏠 Home ─────────────────→ Página de Inicio
│
│  ⚙️  Dashboard ────────────→ /admin/dashboard
│      │
│      ├─→ 📊 Estadísticas (4 tarjetas)
│      ├─→ 📋 Tabla Empresas
│      ├─→ 📅 Actividad Reciente
│      └─→ 👔 Roles del Sistema
│
│  👔 Roles ────────────────→ /admin/roles
│      │
│      ├─→ 📋 Lista de Roles
│      ├─→ ✏️  Crear Rol (Modal)
│      └─→ 🗑️  Editar/Eliminar
│
│  🏢 Empresas ─────────────→ /admin/empresas
│      │
│      ├─→ 📋 Lista de Empresas
│      ├─→ ✏️  Crear Empresa (Modal)
│      └─→ 🗑️  Editar/Eliminar
│
│  📋 Solicitudes Equipos ──→ /solicitudes/equipos
│      │
│      ├─→ ⏳ Pendientes
│      ├─→ ✅ Aprobadas
│      ├─→ ❌ Rechazadas
│      └─→ ➕ Nueva Solicitud (Modal)
│
│  🏆 Solicitudes Torneos ──→ /solicitudes/torneos
│      │
│      ├─→ ⏳ Pendientes
│      ├─→ 👍 Aceptadas
│      ├─→ 👎 Rechazadas
│      └─→ ➕ Nueva Convocatoria (Modal)
│
│  🔐 Iniciar Sesión ───────→ Login Page
│
│  📝 Registro ─────────────→ Register Page
│
└─────────────────────────────────────────────────────────────────┘
```

---

## 🎨 Paleta de Colores

### Primarios:
- **Púrpura Dashboard:** #667eea → #764ba2
- **Verde Equipos:** #10b981 → #059669
- **Azul Torneos:** #3b82f6 → #1e40af

### Estados:
- **Pendiente:** #f59e0b (Naranja)
- **Aprobada/Aceptada:** #10b981 (Verde)
- **Rechazada:** #ef4444 (Rojo)

### Neutrales:
- **Fondo:** #f8f9fa (Gris muy claro)
- **Texto Principal:** #374151 (Gris oscuro)
- **Bordes:** #e5e7eb (Gris claro)

---

## 📱 Características de Diseño

### Responsive:
- ✅ Desktop (1200px+)
- ✅ Tablet (768px - 1199px)
- ✅ Mobile (< 768px)

### Efectos:
- ✅ Hover en tarjetas (translateY -4px)
- ✅ Sombras suave
- ✅ Transiciones smooth (0.3s)
- ✅ Gradientes profesionales

### Componentes UI:
- ✅ Modales funcionales
- ✅ Badges con colores
- ✅ Tabs de filtrado
- ✅ Botones agrupados
- ✅ Tablas responsivas

---

## 🚀 Instrucciones de Uso

### Acceder a cada módulo:

1. **Dashboard Administrativo:**
   ```
   http://localhost:5176/admin/dashboard
   ```

2. **Gestión de Roles:**
   ```
   http://localhost:5176/admin/roles
   ```

3. **Gestión de Empresas:**
   ```
   http://localhost:5176/admin/empresas
   ```

4. **Solicitudes de Equipos:**
   ```
   http://localhost:5176/solicitudes/equipos
   ```

5. **Solicitudes de Torneos:**
   ```
   http://localhost:5176/solicitudes/torneos
   ```

---

## 📝 Datos de Ejemplo

### Empresas Demo:
- Tech Solutions (RUC: 20123456789, 50 empleados)
- SportZone (RUC: 20987654321, 35 empleados)
- FutAcademy (RUC: 20555666777, 25 empleados)
- EventCenter (RUC: 20111222333, 40 empleados)

### Solicitudes de Equipos Demo:
- Los Campeones → Torneo Nacional 2025 (Pendiente)
- Juventud Deportiva → Copa Libertadores (Aprobada)
- FC Fuerte → Campeonato Regional (Rechazada)
- Atlético Unido → Torneo Nacional 2025 (Pendiente)

### Convocatorias Demo:
- Torneo Nacional 2025 → Los Campeones (Pendiente)
- Copa Libertadores → Juventud Deportiva (Aceptada)
- Campeonato Regional → FC Fuerte (Aceptada)
- Torneo Amistoso → Atlético Unido (Rechazada)

---

## ✅ Estado de Implementación

| Feature | Estado | Notas |
|---------|--------|-------|
| Dashboard Admin | ✅ Completo | Todas las estadísticas y modales |
| Gestión Roles | ✅ Completo | CRUD básico |
| Gestión Empresas | ✅ Completo | CRUD básico |
| Solicitudes Equipos | ✅ Completo | Filtrado, modales |
| Solicitudes Torneos | ✅ Completo | Filtrado, modales |
| Responsive Design | ✅ Sí | Para todos los dispositivos |
| Modales | ✅ Sí | Con validaciones básicas |
| Datos Demo | ✅ Sí | Pre-cargados |

---

## 🔄 Funcionalidades CRUD

### Crear (CREATE):
- ✅ Modal con formulario
- ✅ Validación de campos
- ✅ Agregar a lista

### Leer (READ):
- ✅ Visualizar en tablas
- ✅ Ver detalles en modales
- ✅ Filtrado por estado

### Actualizar (UPDATE):
- ✅ Modal de edición
- ✅ Cambiar estado
- ✅ Actualizar en lista

### Eliminar (DELETE):
- ✅ Botón de eliminar
- ✅ Remover de lista
- ✅ Confirmación visual

---

**Versión:** 1.0  
**Fecha:** 11 de Noviembre, 2025  
**Framework:** Blazor Server .NET 8.0  
**Estilos:** Bootstrap 5 + CSS personalizado
