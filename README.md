# Aeroméxico Prueba Técnica - .NET

API REST desarrollada en **ASP.NET Core (.NET 8)** para resolver la prueba técnica solicitada.  
La solución implementa los servicios requeridos para:

- Login
- Consulta de vuelos por rango de fechas
- Alta de pasajeros
- Generación de reservaciones

La implementación respeta el requerimiento de usar una **lista en memoria viva en un Singleton** como almacenamiento, y valida los requests para responder con **400 Bad Request** cuando no cumplen las condiciones indicadas en el enunciado.

## Tecnologías utilizadas

- ASP.NET Core Web API
- .NET 8
- Swagger / OpenAPI (Para facilidad de revison)
- Inyección de dependencias
- Data Annotations
- Almacenamiento en memoria con Singleton

## Arquitectura de la solución

La API fue organizada con separación de responsabilidades para mantener el código limpio y fácil de mantener:

```text
Controllers -> Services -> InMemoryStore
```

### Capas implementadas

- **Controllers**
  - Exponen los endpoints HTTP.
  - Reciben requests y regresan respuestas HTTP.
  - Delegan la lógica a los services.

- **Services**
  - Contienen la lógica de negocio.
  - Validan reglas funcionales.
  - Interactúan con el almacenamiento en memoria.

- **Store**
  - `InMemoryStore` simula la base de datos.
  - Se registra como `Singleton` para que la información permanezca viva mientras la aplicación está corriendo.

- **Contracts**
  - Contienen los DTOs de entrada y salida.
  - Separan el contrato externo de la API del modelo interno.

- **Domain**
  - Contiene los modelos internos de la aplicación.

- **Validation**
  - Incluye validaciones reutilizables, por ejemplo para el formato exacto de fecha.

## Decisiones técnicas importantes

### 1. Singleton en memoria
El enunciado solicita que la base de datos sea una lista viva en memoria en un Singleton.  
Por eso se implementó `InMemoryStore` y se registró en `Program.cs` como:

```csharp
builder.Services.AddSingleton<InMemoryStore>();
```

### 2. Service Layer
Aunque la prueba puede resolverse de manera más simple, se añadió una capa de servicios para:
- separar responsabilidades
- dejar controllers delgados
- facilitar mantenimiento
- acercar la solución a una API real de producción

### 3. Validaciones automáticas
Se usa `[ApiController]` junto con Data Annotations para que cuando un request no cumpla las reglas, ASP.NET Core responda automáticamente con **400 Bad Request**.

### 4. Códigos HTTP consistentes
Se buscó mantener respuestas alineadas con buenas prácticas REST:

- `200 OK` para operaciones exitosas
- `201 Created` para alta de pasajeros
- `400 Bad Request` para requests inválidos
- `401 Unauthorized` para login incorrecto
- `500 Internal Server Error` para errores no controlados en reservaciones

## Requerimientos del enunciado cubiertos

La solución implementa lo solicitado en la prueba técnica: servicio de login, consulta GET de vuelos con parámetros de fecha inicio y fecha fin, generación de reservación por POST, alta de pasajeros por POST, validación de propiedades de request con `BadRequest` y persistencia en memoria mediante un Singleton. También se respetaron las restricciones de longitud y formato de fecha indicadas en el enunciado.  

## Cómo ejecutar el proyecto

### 1. Clonar o descargar el proyecto

```bash
git clone <URL_DEL_REPOSITORIO>
cd AeromexicoPrueba_clean
```

### 2. Instalar SwashbuckleRestaurar paquetes

```bash
dotnet add package Swashbuckle.AspNetCore
dotnet restore
```

### 3. Ejecutar la API

```bash
dotnet run
```

### 4. Abrir Swagger

Cuando la aplicación levante, abrir en el navegador:

```text
http://localhost:5000/swagger
```

> Usar el puerto que aparezca en la consola al ejecutar `dotnet run`.

## Estructura del proyecto

```text
AeromexicoPrueba_clean
├── Contracts
│   ├── Requests
│   └── Responses
├── Controllers
├── Domain
├── Services
├── Store
├── Validation
├── Program.cs
└── README.md
```

## Configuración principal

En `Program.cs` se registran:

- Controllers
- Swagger
- `InMemoryStore` como Singleton
- Services con DI

Ejemplo:

```csharp
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<InMemoryStore>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IFlightService, FlightService>();
builder.Services.AddScoped<IPassengerService, PassengerService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
```

## Endpoints disponibles

---

### 1. Login

**POST** `/api/auth/login`

#### Request

```json
{
  "userName": "admin",
  "password": "12345"
}
```

#### Respuesta exitosa

**200 OK**

```json
{
  "authorized": true,
  "message": "Autorizado"
}
```

#### Respuesta con credenciales inválidas

**401 Unauthorized**

```json
{
  "authorized": false,
  "message": "Credenciales inválidas"
}
```

---

### 2. Consulta de vuelos

**GET** `/api/flights?startDate=2026/03/28 08:00:00&endDate=2026/03/30 23:59:59`

#### Parámetros

- `startDate`
- `endDate`

Formato requerido:

```text
yyyy/MM/dd HH:mm:ss
```

#### Ejemplo

```text
http://localhost:5000/api/flights?startDate=2026/03/28 08:00:00&endDate=2026/03/30 23:59:59
```

#### Respuesta exitosa

**200 OK**

```json
[
  {
    "id": 1,
    "flightNumber": "AM01",
    "origin": "MT",
    "destination": "CD",
    "departureDate": "2026/03/28 10:30:00"
  }
]
```

#### Validaciones aplicadas

- Número de vuelo con longitud máxima de 4 caracteres
- Origen con longitud máxima de 2 caracteres
- Destino con longitud máxima de 2 caracteres
- Fecha con formato exacto requerido

---

### 3. Alta de pasajeros

**POST** `/api/passengers`

#### Request

```json
{
  "firstName": "Cristian",
  "lastName": "Castaneda"
}
```

#### Respuesta exitosa

**201 Created**

```json
{
  "id": 1,
  "firstName": "Cristian",
  "lastName": "Castaneda"
}
```

#### Validaciones aplicadas

- `firstName` no puede ser nulo
- `lastName` no puede ser nulo

---

### 4. Generación de reservación

**POST** `/api/reservations`

#### Request

```json
{
  "flightIds": [1, 2],
  "passengerIds": [1]
}
```

#### Respuesta exitosa

**200 OK**

```json
{
  "reservationId": 1,
  "flightIds": [1, 2],
  "passengerIds": [1],
  "message": "Reservación generada correctamente"
}
```

#### Respuestas posibles

- `200 OK` cuando la reservación se genera correctamente
- `400 Bad Request` cuando el request es inválido o contiene referencias inexistentes
- `500 Internal Server Error` ante un error inesperado

## Flujo recomendado para probar la API

1. Ejecutar la aplicación con `dotnet run`
2. Abrir Swagger en `http://localhost:0000/swagger`
3. Probar el login
4. Consultar vuelos
5. Dar de alta un pasajero
6. Crear una reservación usando IDs válidos

## Ejemplo de prueba completa

### Login

```json
{
  "userName": "admin",
  "password": "12345"
}
```

### Crear pasajero

```json
{
  "firstName": "Angel",
  "lastName": "Avalos"
}
```

### Crear reservación

```json
{
  "flightIds": [1],
  "passengerIds": [1]
}
```



## Autor

Angel Avalos
