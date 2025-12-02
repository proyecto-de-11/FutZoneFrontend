# Documentación FutZone

Este directorio contiene la documentación oficial del sistema FutZone.

## Contenido

### Manual de Usuario

**Archivo:** `Manual_Usuario_FutZone.pdf`

El manual de usuario completo del sistema FutZone/gofit, que incluye:

1. **Introducción al Sistema**: Descripción general y características principales
2. **Requisitos del Sistema**: Requisitos técnicos y de seguridad
3. **Acceso al Sistema**: Guías de registro, login y recuperación de contraseña
4. **Roles de Usuario**: Descripción detallada de cada rol (Usuario, Empresa, Administrador)
5. **Funcionalidades Principales**: 
   - Gestión de canchas
   - Sistema de reservas
   - Gestión de equipos
   - Torneos
   - Membresías
6. **Guías por Rol**: Instrucciones específicas para cada tipo de usuario
7. **Preguntas Frecuentes**: Respuestas a las dudas más comunes
8. **Soporte Técnico**: Información de contacto y procedimientos

## Formato de Entrega

✔ **Documento .pdf** con el desarrollo del manual de usuario en base a lineamientos proporcionados

El manual fue elaborado siguiendo las indicaciones del docente y contiene:
- Estructura organizada por secciones
- Tabla de contenidos navegable
- Guías paso a paso para cada funcionalidad
- Mejores prácticas y recomendaciones
- Preguntas frecuentes
- Información de soporte técnico
- Glosario de términos
- Apéndices útiles

## Generación del PDF

El PDF fue generado usando Pandoc con las siguientes características:
- Tabla de contenidos automática con 3 niveles
- Márgenes de 1 pulgada
- Tamaño de fuente de 11pt
- Formato profesional y legible

## Actualización

Para regenerar el PDF desde el archivo Markdown:

```bash
cd docs
pandoc Manual_Usuario_FutZone.md -o Manual_Usuario_FutZone.pdf \
  --pdf-engine=wkhtmltopdf \
  --toc \
  --toc-depth=3 \
  -V geometry:margin=1in \
  -V fontsize=11pt \
  -V documentclass=article \
  --highlight-style=tango
```

## Contacto

Para preguntas sobre la documentación, contacte al equipo de desarrollo.

---

**Fecha de creación:** Diciembre 2024  
**Versión:** 1.0  
**Estado:** Completo
