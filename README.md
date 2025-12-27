API Web desarrollada con ASP.NET Core 9.  

## Tecnologías utilizadas

- ASP.NET Core 9 (Minimal APIs)
- C#
- Entity Framework Core
- SQL Server
- LINQ
- OpenAPI

## Funcionalidades

- Gestión de usuarios y tareas (relación uno a muchos)
- Endpoints REST implementados con Minimal APIs
- Persistencia de datos mediante Entity Framework Core
- Consultas LINQ con proyección a DTOs
- Operaciones asíncronas con async/await
- Migraciones para la creación y actualización del esquema de base de datos
- Datos iniciales de ejemplo para facilitar las pruebas
- Documentación y pruebas a través de OpenAPI

## Arquitectura y organización

El proyecto está organizado de forma sencilla:

- Models: entidades que representan las tablas de la base de datos
- DTOs: objetos usados como contratos de entrada y salida de la API
- DbContext: gestión del acceso a datos mediante EF Core
- Minimal APIs: definición de los endpoints sin controladores

Se utilizan proyecciones con LINQ para evitar devolver entidades directamente y controlar el formato de las respuestas.

