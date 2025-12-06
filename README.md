# RHManager · Sistema de Gestión de Recursos Humanos  
Aplicación web desarrollada en **ASP.NET Core MVC**, enfocada en la gestión integral de Recursos Humanos: empleados, puestos, departamentos y permisos laborales.  
Incluye un **Dashboard analítico interactivo**, CRUD completos optimizados, arquitectura limpia por servicios y un diseño moderno UI/UX.

---

## 🚀 Tecnologías utilizadas
- **ASP.NET Core MVC 8**
- **Entity Framework Core**
- **SQL Server**
- **Bootstrap 5 + estilos personalizados Pro UI**
- **Chart.js para analítica**
- **AutoMapper (opcional para expansión futura)**
- **Patrón Service Layer**
- **GitHub Workflow + Pull Requests protegidos**

---

## 📌 Funcionalidades principales

### 👥 Gestión de Empleados
- Crear, editar, eliminar y ver empleados
- Asignación de puestos y departamentos
- Activación / inactivación
- Validaciones automáticas

### 🏢 Gestión de Departamentos y Puestos
- Estructura organizacional completa  
- Distribución visual en gráficos  
- CRUD con UX optimizada

### 📝 Gestión de Permisos (Leave Requests)
- Solicitud de permisos por empleado  
- Aprobación, rechazo o estado pendiente  
- Cálculo automático de días solicitados  
- Historial detallado  
- Validación de rangos de fechas  
- **En edición se modifica el permiso original (no crea uno nuevo)**

### 📊 Dashboard Analítico
- KPIs ejecutivos:
  - Empleados activos
  - Rotación estimada
  - Puestos definidos
  - Promedio de días de permiso
  - Crecimiento mensual
- Gráficos interactivos:
  - Permisos por mes
  - Distribución por puesto
  - Distribución por departamento
- Últimas contrataciones
- Últimos permisos registrados

---

## 🧩 Arquitectura
El proyecto utiliza una arquitectura basada en:

Controllers → Services → Repository/DbContext → Models → Views

Ventajas:
- Código mantenible
- Separación de responsabilidades
- Fácil escalabilidad
- Ideal para portafolio profesional

---

## 🔐 Configuración de repositorio (Branch Protection)
La rama **master/main** está configurada con:
- ❌ No permite pushes directos
- ✔ Pull Requests obligatorios
- ✔ 1 aprobación requerida (excepto el owner)
- ✔ No se puede saltar revisión sin permisos administrativos

Esto garantiza prácticas de desarrollo profesional, similares a entornos empresariales.

---

## 🖥 Capturas
> Agregar aquí screenshots del Dashboard y CRUD (opcional para portafolio)

---

## 📦 Instalación
1. Clonar el repositorio  
2. Crear base de datos en SQL Server  
3. Actualizar `appsettings.json` con tu connection string  
4. Ejecutar:  
```bash
update-database

Correr el proyecto:
dotnet run

📄 Licencia
Este proyecto es de uso educativo y portafolio.

👤 Autor

Jervis — Analista de Sistemas / Desarrollador Jr / Data Analyst BI
LinkedIn: www.linkedin.com/in/jervis-calvo-09b045254
Email: jervis344.89@gmail.com
