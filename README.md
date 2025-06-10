# Letras Libres - Sistema de Gestión de Biblioteca 📚

Este proyecto es una API RESTful desarrollada con ASP.NET Core que permite gestionar libros, usuarios, préstamos y devoluciones de una biblioteca.

## Tecnologías utilizadas

- ASP.NET Core 8
- Entity Framework Core
- SQL Server
- Swagger
- C#

---

## 🚀 Instrucciones para ejecutar el proyecto localmente

### 1. Clona el repositorio

```bash
git clone https://github.com/P1POX/eva2.git
cd eva2
```
### 2. Restaura los paquetes NuGet
```cmd
dotnet restore
```
### 3. Configura la base de datos
Edita el archivo `appsettings.json` si deseas usar otra base:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=OMEN16;Database=LetrasLibres;Trusted_Connection=True;TrustServerCertificate=True"
}
```
### 4. Aplica las migraciones
```cmd
dotnet ef database update
```
### 5. Ejecuta el proyecto
```cmd
dotnet run
```

### 📌 Funcionalidades
- CRUD de libros (/api/libros)
- CRUD de usuarios (/api/usuarios)
- Registro de préstamos (/api/prestamos)
- Registro de devoluciones (/api/devoluciones)
- Validaciones:
    - Stock de libros
    - Formato de RUT, correo y teléfono
    - ISBN requerido
- Historial de préstamos por usuario (/api/usuarios/{id}/prestamos) 

### ✅ Pruebas
- Creación de un libro
![image](https://i.imgur.com/cpEIuWS.png)
![image](https://i.imgur.com/SgHtSMg.png)

- Creación de un usuario
![image](https://i.imgur.com/o7XUFfP.png)
![image](https://i.imgur.com/3Ys1HIH.png)

- Creación de préstamo
![image](https://i.imgur.com/F13bUmv.png)

### 🧑‍💻 Autor
José Flores