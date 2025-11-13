# 📖 GUÍA DE USO - MIS RESERVAS

## Acceso a la Vista

### Opción 1: Desde el Menú
1. Abre la aplicación en `http://localhost:5176`
2. En el menú lateral izquierdo, busca **"📅 Mis Reservas"**
3. Haz clic en el enlace
4. Serás redirigido a `/empresa/reservas`

### Opción 2: URL Directa
Copia y pega en tu navegador:
```
http://localhost:5176/empresa/reservas
```

---

## 🎯 Qué Ves al Abrir

### 1. Encabezado (Header)
```
📅 Gestión de Reservas
Visualiza, acepta o rechaza las reservas de tus canchas
```
Fondo en gradiente púrpura bonito.

### 2. Cuatro Tarjetas de Estadísticas
En la parte superior verás 4 números grandes:

```
⏳ PENDIENTES          ✅ APROBADAS
2 reservas            2 reservas

❌ RECHAZADAS         💰 INGRESOS
1 reserva             $145.00
```

Estos números se actualizan automáticamente según aceptes o rechaces reservas.

### 3. Sección de Filtros
Tres desplegables para filtrar las reservas:

```
[▼ Filtrar por Estado]  [▼ Filtrar por Cancha]  [▼ Ordenar por]
```

### 4. Lista de Reservas
Una serie de **tarjetas (cards)** con la información de cada reserva.

---

## 🔍 Filtrando Reservas

### Filtro 1: Por Estado
```
"Todos los estados"    → Muestra todas las reservas
"Pendientes"           → Solo las que necesitan respuesta
"Aprobadas"            → Las que ya aceptaste
"Rechazadas"           → Las que ya rechazaste
```

**Ejemplo:** Si quieres ver solo las pendientes:
1. Haz clic en `[▼ Filtrar por Estado]`
2. Selecciona `"Pendientes"`
3. La lista se actualiza automáticamente

### Filtro 2: Por Cancha
```
"Todas las canchas"        → Muestra reservas de todas
"Cancha Principal"         → Solo de esta cancha
"Cancha Secundaria"        → Solo de esta cancha
"Cancha Entrenamiento"     → Solo de esta cancha
```

**Ejemplo:** Si solo quieres ver reservas de tu cancha principal:
1. Haz clic en `[▼ Filtrar por Cancha]`
2. Selecciona `"Cancha Principal"`
3. Se muestran solo esas reservas

### Filtro 3: Ordenamiento
```
"Más recientes"        → Las nuevas primero (por defecto)
"Más antiguas"         → Las viejas primero
"Mayor precio"         → Las más caras primero
"Menor precio"         → Las más baratas primero
```

**Ejemplo:** Para ver cuál te paga más:
1. Haz clic en `[▼ Ordenar por]`
2. Selecciona `"Mayor precio"`
3. Las reservas se reordenan de mayor a menor monto

### Combinar Filtros
¡Puedes usar los 3 filtros a la vez!

**Ejemplo completo:**
- Estado: "Pendientes" (solo las que debo responder)
- Cancha: "Cancha Principal" (solo de esa cancha)
- Ordenamiento: "Mayor precio" (empiezo por la más rentable)

---

## 📋 Qué Información VES en Cada Reserva

Cada tarjeta de reserva contiene:

### Encabezado (Parte Superior)
```
Cancha Principal                    ⏳ Pendiente
```
- **Nombre de la cancha**
- **Estado:** Con icono y color (⏳ naranja, ✅ verde, ❌ rojo)

### Sección 1: Datos del Cliente
```
👤 Información del Cliente
Nombre:     Juan García López
Email:      juan.garcia@email.com
Teléfono:   +34 612 345 678
```

Puedes usar esta información para contactar al cliente.

### Sección 2: Detalles de la Reserva
```
⏰ Detalles de la Reserva
Fecha:              15/11/2025
Hora:               18:00 - 20:00
Duración:           2 horas
Cantidad Jugadores: 22 personas
```

Toda la información sobre cuándo quieren la cancha.

### Sección 3: Información Financiera
```
💵 Información Financiera
Precio/Hora:   $50
Descuento:     -$0
Total a Pagar: $100
```

La parte financiera está **destacada en azul** porque es importante.

### Sección 4: Notas del Cliente (Si existen)
```
📝 Notas del Cliente
"Necesitamos la cancha con balón incluido. Gracias."
```

Aparece solo si el cliente dejó un mensaje. Está en una caja gris.

### Sección 5: Historial
```
📅 Historial
Solicitada el:  13/11/2025 10:30
Respondida el:  13/11/2025 14:15
```

Muestra cuándo pidió la reserva y cuándo respondiste (si ya respondiste).

---

## ✅ Cómo Aceptar una Reserva

### Paso a Paso

1. **Busca la reserva en la lista**
   - Filtra si es necesario
   - Lee la información del cliente
   - Verifica que todo esté bien

2. **Mira el estado**
   - Debe estar en **⏳ Pendiente** (naranja)
   - Si está ✅ Aprobada o ❌ Rechazada, no puedes cambiar

3. **Haz clic en [✅ APROBAR]**
   - El botón está al final de la tarjeta
   - De color verde

4. **Verifica el cambio**
   - El estado cambia a ✅ **Aprobada**
   - El color del badge cambia a verde
   - Los botones se deshabilitan

5. **Los números se actualizan**
   - Las "Pendientes" disminuye en 1
   - Las "Aprobadas" aumenta en 1
   - Los "Ingresos" aumentan con el monto

**¡LISTO! La reserva fue aceptada.**

---

## ❌ Cómo Rechazar una Reserva

### Paso a Paso

1. **Busca la reserva en la lista**
   - Filtra si es necesario
   - Lee toda la información

2. **Asegúrate que esté en ⏳ Pendiente**
   - Si está bloqueada, ya fue respondida

3. **Haz clic en [❌ RECHAZAR]**
   - El botón está al lado del verde
   - De color rojo

4. **Verifica el cambio**
   - El estado cambia a ❌ **Rechazada**
   - El color del badge cambia a rojo
   - Los botones se deshabilitan

5. **Los números se actualizan**
   - Las "Pendientes" disminuye en 1
   - Las "Rechazadas" aumenta en 1
   - Los ingresos NO cambian (no es aprobada)

**¡LISTO! La reserva fue rechazada.**

---

## 🔒 Cuando Ya Respondiste

Una vez que apruebes o rechaces una reserva:

```
[🔒 APROBADA]          (si la aprobaste)
[🔒 RECHAZADA]         (si la rechazaste)
```

El botón está:
- ✅ Visible
- ✅ Deshabilitado (gris, no se puede hacer clic)
- ✅ Muestra el estado actual

**Esto es para evitar que cambies accidentalmente tu respuesta.**

---

## 📊 Entendiendo las Estadísticas

### Tarjeta 1: ⏳ Pendientes de Aprobación
```
Número: 2
```
**Qué significa:** Tienes 2 reservas esperando tu respuesta.  
**Qué hacer:** Aprueba o rechaza para que queden a 0.

### Tarjeta 2: ✅ Reservas Aprobadas
```
Número: 2
```
**Qué significa:** Ya confirmaste 2 reservas.  
**Qué representa:** Tus compromisos con clientes.

### Tarjeta 3: ❌ Reservas Rechazadas
```
Número: 1
```
**Qué significa:** Rechazaste 1 reserva.  
**Razones comunes:** Cancha ocupada, no disponible, cliente problemático.

### Tarjeta 4: 💰 Ingresos (Aprobadas)
```
Número: $145.00
```
**Qué significa:** Dinero que recibirás por las reservas aprobadas.  
**Cálculo:** Suma de (Precio/hora × Duración - Descuento) de todas las aprobadas.  
**Nota:** Solo cuenta las APROBADAS, no las rechazadas.

---

## 🎯 Escenarios Comunes

### Escenario 1: Nueva Reserva Llega
```
1. Ves una tarjeta nueva con ⏳ Pendiente
2. Lees los detalles del cliente
3. Verificas que tu cancha esté disponible
4. Haces clic en [✅ APROBAR]
5. El cliente recibirá una notificación (en el futuro)
```

### Escenario 2: Cancha No Disponible
```
1. Ves una reserva pendiente
2. Revisar y ver que tu cancha está ocupada ese horario
3. Haces clic en [❌ RECHAZAR]
4. El estado cambia a rechazada
5. El cliente puede ver el rechazo en su perfil
```

### Escenario 3: Cliente con Malas Reseñas
```
1. Ves una reserva de un cliente problemático
2. Lees las notas (si tiene malas referencias)
3. Decides rechazar por seguridad
4. Haces clic en [❌ RECHAZAR]
5. Proteges tu negocio
```

### Escenario 4: Revisar Historial
```
1. Filtra por "Aprobadas"
2. Ordena por "Mayor precio"
3. Ves cuál cliente te paga más
4. Puedes usar esta info para futuras decisiones
```

---

## 💡 Consejos

### ✅ Haz
- ✅ Responde rápido a las solicitudes (cliente espera)
- ✅ Lee las notas del cliente cuidadosamente
- ✅ Usa los filtros para gestionar mejor
- ✅ Verifica que la fecha/hora sean correctas
- ✅ Ten en cuenta el número de jugadores

### ❌ No Hagas
- ❌ No aceptes si tu cancha ya está reservada
- ❌ No rechaces sin razón válida
- ❌ No ignores las reservas (responde siempre)
- ❌ No confundas hora de inicio con fin
- ❌ No olvides guardar los cambios (se guardan automáticamente)

---

## ❓ Preguntas Frecuentes

### P: ¿Se guardan automáticamente los cambios?
**R:** Sí. Cuando hagas clic en Aprobar o Rechazar, el cambio es inmediato.

### P: ¿Puedo cambiar mi respuesta después?
**R:** No, una vez que respondas, queda bloqueada. Será necesario contactar al administrador.

### P: ¿Qué pasa si no respondo a una reserva?
**R:** Queda en "Pendiente" indefinidamente. El cliente verá que aún no respondiste.

### P: ¿Dónde veo el teléfono del cliente?
**R:** En la sección "👤 Información del Cliente" de cada reserva.

### P: ¿Cómo contacto al cliente?
**R:** Puedes usar el email o teléfono que aparecen en la tarjeta.

### P: ¿Qué significa el descuento en la reserva?
**R:** Es un descuento que el cliente aplicó (ej: descuento por primera compra).

### P: ¿Cómo se calcula el total?
**R:** (Precio/Hora × Duración) - Descuento

**Ejemplo:**
- Precio: $50/hora
- Duración: 2 horas
- Descuento: $5
- **Total: (50 × 2) - 5 = $95**

### P: ¿Esos números de ejemplo son reales?
**R:** No, son datos de demostración. Cuando conectemos la API real, verás tus reservas verdaderas.

---

## 🔄 Flujo Completo

```
1. CLIENTE SOLICITA
   └─> Aparece en tu lista como ⏳ Pendiente

2. TÚ REVISAS
   └─> Lees toda la información

3. TÚ DECIDES
   └─> ✅ Aprobar    O    ❌ Rechazar

4. ESTADO CAMBIA
   └─> ✅ Aprobada   O    ❌ Rechazada

5. CLIENTE VE RESPUESTA
   └─> Recibe notificación del resultado

6. ESTADÍSTICAS ACTUALIZAN
   └─> Los números se recalculan automáticamente
```

---

## 🎨 Entendiendo los Colores

```
⏳ NARANJA (#f59e0b)  = Espera tu acción (Pendiente)
✅ VERDE (#10b981)    = Confirmado, todo bien (Aprobada)
❌ ROJO (#ef4444)     = Denegado (Rechazada)
💰 PÚRPURA (#8b5cf6)  = Dinero (Ingresos)
```

---

## 📱 En Móvil

La vista se adapta perfectamente a tu teléfono:

```
- Las tarjetas ocupan toda la pantalla
- Los filtros están apilados uno arriba del otro
- Los botones son más grandes (fáciles de tocar)
- Todo es scrollable verticalmente
- Los estadísticos se ven claros
```

**No necesitas hacer nada especial, solo abre en tu celular.**

---

## 🚀 Próximas Funcionalidades

En el futuro, podrás:
- 📧 Enviar mensajes a clientes desde aquí
- 🔔 Recibir notificaciones cuando lleguen reservas
- 📅 Ver un calendario visual
- 💬 Chat en vivo con clientes
- 📊 Exportar reportes
- 🎯 Ver analytics detallados

---

**Guía Generada:** 13 de Noviembre, 2025  
**Versión:** 1.0 - Para Usuarios  
**Nivel:** Principiante a Intermedio
