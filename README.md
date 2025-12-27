
# TripMatch – Workstation Backend (C# / .NET)

API REST desarrollada en **C# / .NET** para la plataforma **TripMatch** del ecosistema **Turisfera**. Este backend gestiona la información de experiencias turísticas, consultas, usuarios, reservas y servicios internos utilizados por agentes de turismo.

El sistema implementa una arquitectura escalable basada en **Domain-Driven Design (DDD)**, asegurando la seguridad y eficiencia necesarias para el flujo operativo de los agentes turísticos.

---

##  Objetivo del Backend

- Proveer endpoints seguros y optimizados para el flujo de trabajo del agente.
- Gestionar experiencias turísticas y su información detallada.
- Recibir consultas y solicitudes de clientes.
- Manejar reservas, disponibilidad y datos operativos.
- Autenticar usuarios mediante JWT.
- Integrarse directamente con la UI TripMatch Workstation.

---

##  Tecnologías utilizadas

- **.NET 7+ / .NET 8**
- **C# 12**
- **Entity Framework Core**
- **SQL Server**
- **AutoMapper**
- **FluentValidation**
- **Swagger / Swashbuckle**
- **Dependency Injection**
- **JWT Authentication**

---

## Algunos Endpoints Esenciales 

### Auth
- `POST /api/v1/iam/auth/signup`
- `POST /api/v1/iam/auth/signin`

### Agency
- `GET /api/v1/profile/user/agency/{userId}`
- `PUT /api/v1/profile/user/agency/{userId}`

### Experiences
- `GET /api/v1/design/experience`
- `GET /api/v1/design/experience/{id}`
- `POST /api/v1/design/experience`
- `PUT /api/v1/design/experience/{id}`
- `DELETE /api/v1/design/experience/{id}`
---


##  Configuración Local

1.  **Clonar el repositorio:**
    ```bash
    git clone [https://github.com/Turisfera/workstation-back-end.git](https://github.com/Turisfera/workstation-back-end.git)
    ```

2.  **Configuración de Base de Datos:**
    * Modificar la cadena de conexión `DefaultConnection` en el archivo `appsettings.json`.
    * Ejecutar migraciones:
    ```bash
    dotnet ef database update
    ```

3.  **Ejecutar la API:**
    ```bash
    dotnet run
    ```
    * Swagger UI: `https://localhost:7XXX/swagger/index.html`
