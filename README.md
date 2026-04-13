# n5b-ejemplos-de-clase

## Levantar el proyecto desde cero


### 1) Restaurar herramientas y paquetes

```bash
cd Cine
dotnet tool restore
dotnet restore Cine.sln
```

### 2) Aplicar migraciones (base nueva)

```bash
dotnet ef database update --project Cine.Repository/Cine.Repository.csproj --startup-project CineWebApi/CineWebApi.csproj --context CineDbContext
```

Este comando crea la base `cine` y aplica las migraciones pendientes.

### 3) Ejecutar la API

```bash
dotnet run --project CineWebApi/CineWebApi.csproj
```

Swagger queda disponible en la URL que muestre la consola (normalmente algo como `https://localhost:xxxx/swagger`).

## Comandos de migraciones (cuando cambies el modelo)

### Crear una nueva migracion

```bash
cd Cine
dotnet ef migrations add NombreDeLaMigracion \
	--project Cine.Repository/Cine.Repository.csproj  --startup-project CineWebApi/CineWebApi.csproj --context CineDbContext
```

## Credenciales de prueba

### SQL Server (base de datos)

- Usuario: sa
- Contrasena: YourStrong!Passw0rd
- Base: cine

### API (seed para endpoints protegidos)

- Usuario de prueba: demo.user
- Contrasena: demo123
- Token de sesion: demo-session-token-123
- Header recomendado: X-Session-Id: demo-session-token-123

Login de ejemplo:

```json
{
	"username": "demo.user",
	"password": "demo123"
}
```

## Connection string actual

Configurada en:

- [Cine/CineWebApi/appsettings.json](Cine/CineWebApi/appsettings.json)
- [Cine/CineWebApi/appsettings.Development.json](Cine/CineWebApi/appsettings.Development.json)

Valor:

```txt
Server=localhost,1433;Database=cine;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True;Encrypt=False;
```
