# ⚡ GUÍA RÁPIDA - VISTA DE USUARIOS ADMIN

## 🚀 INICIO RÁPIDO

### 1. ACCEDER A LA VISTA
- **URL:** `https://localhost:5176/admin/usuarios`
- **Menú:** Dashboard → Usuarios (👥)

### 2. PRIMERA VEZ
La vista cargará automáticamente los usuarios del endpoint.

---

## 📊 ¿QUÉ VES?

### SECCIÓN 1: ENCABEZADO
- Título: "Gestión de Usuarios"
- Botón "Actualizar" para refrescar datos manualmente

### SECCIÓN 2: 4 TARJETAS DE ESTADÍSTICAS
```
[Total de Usuarios] [Usuarios Activos] [Usuarios Inactivos] [Administradores]
        42                  35                    7                   5
```

### SECCIÓN 3: FILTROS (3 SELECTORES)
```
🔍 Buscar Usuario          Filtrar por Rol        Filtrar por Estado
[Escribe aquí...]         [Todos los roles]      [Todos los estados]
```

### SECCIÓN 4: TABLA DE USUARIOS
```
ID | Nombre | Email | Teléfono | Rol | Estado | Registro | Acciones
---|--------|-------|----------|-----|--------|----------|----------
1  | Juan P | juan@ | +34612.. | Ad- | Activo | 15/01..  | 👁 ✏️ 🔒
2  | María G| maria | -        | Emp | Activo | 20/02..  | 👁 ✏️ 🔒
```

---

## 🔍 CÓMO FILTRAR

### BUSQUEDA EN TIEMPO REAL
1. Escribe en el campo "Buscar Usuario"
2. Busca por: Nombre, Email, Teléfono
3. Se actualiza automáticamente

**Ejemplo:**
- Escribir: "Juan" → muestra solo usuarios con "Juan" en nombre
- Escribir: "@gmail.com" → muestra solo gmail

### POR ROL
1. Haz click en "Filtrar por Rol"
2. Selecciona:
   - Admin
   - Empresa
   - Cliente
3. Se actualiza la tabla

### POR ESTADO
1. Haz click en "Filtrar por Estado"
2. Selecciona:
   - Activos
   - Inactivos
3. Se actualiza la tabla

### COMBINAR FILTROS
Puedes combinar los 3 filtros:
- Buscar: "juan"
- Rol: "Admin"
- Estado: "Activos"
→ Muestra solo admins activos con "juan" en el nombre

---

## 👁️ VER DETALLES DE UN USUARIO

1. Localiza el usuario en la tabla
2. Haz click en el botón 👁️ (ojo) en la columna "Acciones"
3. Se abre un **MODAL** con:
   - ID completo
   - Nombre
   - Email
   - Teléfono
   - Rol
   - Estado
   - Fecha de Registro
   - Último Acceso

4. Botones:
   - "Cerrar" → cierra el modal
   - "Editar" → abre editor (futuro)

---

## ✏️ EDITAR USUARIO

1. Haz click en el botón ✏️ (lápiz)
2. Abre editor de usuario (futuro)

**Nota:** Actualmente es un placeholder, se implementará en la siguiente versión.

---

## 🔒 BLOQUEAR / DESBLOQUEAR

### BLOQUEAR UN USUARIO ACTIVO
1. Busca el usuario en la tabla
2. Haz click en el botón 🔒 (candado cerrado)
3. El estado cambia de "Activo" a "Inactivo"
4. El botón cambia a 🔓 (abierto)

### DESBLOQUEAR UN USUARIO INACTIVO
1. Busca el usuario en la tabla
2. Haz click en el botón 🔓 (candado abierto)
3. El estado cambia de "Inactivo" a "Activo"
4. El botón cambia a 🔒 (cerrado)

---

## 📱 EN MÓVIL

- Tarjetas de estadísticas apiladas verticalmente
- Tabla con scroll horizontal
- Filtros optimizados
- Todo sigue funcionando igual

---

## ❌ SOLUCIONAR ERRORES

### ERROR: "Error de conexión"
**Causa:** La API no está disponible

**Solución:**
1. Verifica que la API esté ejecutándose en `https://localhost:7174`
2. Verifica que el endpoint sea `/api/usuarios`
3. Haz click en "Actualizar" para reintentar

### ERROR: "No hay usuarios en el sistema"
**Causa:** La API no devuelve datos

**Posibles soluciones:**
1. Verifica que hay usuarios en la base de datos
2. Verifica que el endpoint devuelve datos válidos
3. Comprueba la respuesta JSON

### ERROR: "CORS bloqueado"
**Causa:** Seguridad de navegador

**Solución:**
1. Configura CORS en el backend
2. O accede desde el mismo dominio

---

## 📊 ESTADÍSTICAS

Las tarjetas se actualizan automáticamente:

| Tarjeta | Qué Cuenta | Color |
|---------|-----------|-------|
| Total de Usuarios | Todos los usuarios | Azul |
| Usuarios Activos | Solo con estado "Activo" | Verde |
| Usuarios Inactivos | Solo con estado "Inactivo" | Naranja |
| Administradores | Solo rol "Admin" | Cian |

---

## 🎨 COLORES Y SIGNIFICADOS

### BADGES DE ROL
- 🔴 **Admin** (Rojo) - Administrador del sistema
- 🟠 **Empresa** (Naranja) - Propietario de cancha
- 🔵 **Cliente** (Azul) - Usuario cliente

### BADGES DE ESTADO
- 🟢 **Activo** (Verde) - Usuario puede acceder
- 🔴 **Inactivo** (Rojo) - Usuario bloqueado

---

## 💡 TIPS Y TRUCOS

1. **Limpiar filtros:** Selecciona "-- Todos los Roles --" o "-- Todos los Estados --"
2. **Búsqueda exacta:** Usa el email completo para resultados precisos
3. **Actualizar datos:** Click en botón "Actualizar" para refrescar
4. **Ordenar:** Haz click en encabezados de columna (próxima versión)
5. **Exportar:** Puedes hacer screenshot de la tabla (próxima versión)

---

## 📋 CAMPOS DE UN USUARIO

| Campo | Tipo | Ejemplo | Notas |
|-------|------|---------|-------|
| ID | Número | 1, 2, 3... | Único |
| Nombre | Texto | Juan Pérez | Requerido |
| Email | Texto | juan@gmail.com | Requerido, único |
| Teléfono | Texto | +34612345678 | Opcional |
| Rol | Combo | Admin, Empresa, Cliente | Requerido |
| Activo | Booleano | Sí/No | Sí por defecto |
| Fecha Registro | Fecha | 15/01/2024 | Automática |
| Último Acceso | Fecha | 17/11/2025 | Puede ser null |

---

## 🔄 FLUJO TÍPICO

```
1. Acceder a /admin/usuarios
   ↓
2. Sistema carga usuarios del endpoint
   ↓
3. Se muestran 4 tarjetas con estadísticas
   ↓
4. Se muestran todos los usuarios en tabla
   ↓
5. Usuario busca/filtra usando controles
   ↓
6. Tabla se actualiza dinámicamente
   ↓
7. Usuario clickea en Ver/Editar/Bloquear
   ↓
8. Sistema ejecuta acción (Ver Modal, Editar, Cambiar Estado)
   ↓
9. Datos se actualizan en pantalla
```

---

## ⚙️ CONFIGURACIÓN

### CAMBIAR URL DEL ENDPOINT

**Archivo:** `Usuarios.razor`
**Línea:** ~220 (método CargarUsuarios)

```csharp
// ANTES:
var response = await HttpClient.GetAsync("https://localhost:7174/api/usuarios");

// DESPUÉS:
var response = await HttpClient.GetAsync("https://tu-api.com/api/usuarios");
```

---

## 🆘 CONTACTO Y SOPORTE

Si tienes problemas:
1. Revisa la documentación completa: `DOCUMENTACION_USUARIOS_ADMIN.md`
2. Verifica que la API esté corriendo
3. Comprueba la respuesta JSON del endpoint
4. Revisa la consola del navegador (F12)

---

## ✅ CHECKLIST

- [ ] Puedo acceder a /admin/usuarios
- [ ] Se cargan usuarios automáticamente
- [ ] Veo 4 tarjetas de estadísticas
- [ ] Puedo buscar usuarios
- [ ] Puedo filtrar por rol
- [ ] Puedo filtrar por estado
- [ ] Puedo ver detalles de usuario
- [ ] Puedo bloquear/desbloquear usuario
- [ ] Los filtros funcionan juntos
- [ ] La vista es responsive en móvil

---

**¡Disfruta gestionar usuarios! 🎉**

Generado: 17 de Noviembre, 2025
