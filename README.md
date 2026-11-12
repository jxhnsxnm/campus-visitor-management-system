# Sistema de Gestión y Control de Visitas (Campus ITLA)

Aplicación de escritorio desarrollada bajo **Arquitectura Multicapas** para el registro, control y monitoreo de visitas en los edificios y aulas del Instituto Tecnológico de Las Américas (ITLA).

## Características Principales
* **Autenticación y Control de Roles:** Acceso diferenciado según el perfil del usuario (**Administrador** y **Visitante**).
* **Filtrado Dinámico:** Selección en cascada dependiente entre edificios y sus respectivas aulas disponibles.
* **Consulta de Historial:** Búsqueda y filtrado avanzado de visitas registradas por rango de fechas y ubicación específica.
* **Módulos Administrativos:** Gestión completa (CRUD) de usuarios, edificios y aulas del campus.

## Tecnologías y Arquitectura
* **Lenguaje:** C#
* **Framework UI:** Windows Forms (.NET)
* **Base de Datos:** Microsoft SQL Server
* **Patrón de Diseño:** Arquitectura Multicapas (Capa de Presentación, Capa de Negocio, Capa de Datos y Entidades)

## Instalación y Ejecución
1. Clona el repositorio en tu equipo local.
2. Ejecuta el script SQL incluido en **SQL Server Management Studio (SSMS)** para generar la base de datos y las tablas.
3. Abre el archivo `.sln` en **Visual Studio**.
4. Configura la cadena de conexión (`ConnectionString`) en el archivo `App.config` apuntando a tu servidor local.
5. Compila y ejecuta la solución (`F5`).

## Autor
**José Alberto Johnson Martínez**  
