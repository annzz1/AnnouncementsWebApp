# Announcements Web Application

## Overview

The Announcements Web Application is a full-stack ASP.NET Core solution for managing announcements. Users can create, view, update, and delete announcements, each containing a title, description, image, and phone number. The application features a modern web UI, RESTful API, and a layered architecture for maintainability and scalability.

## Technologies Used

- **ASP.NET Core 8**: Web API and MVC framework for building the backend.
- **Entity Framework Core 8**: ORM for database access and migrations.
- **SQL Server**: Relational database for persistent storage.
- **AutoMapper**: Object mapping between DTOs and domain models.
- **Swashbuckle/Swagger**: API documentation and testing UI.
- **JavaScript, HTML, CSS**: Frontend technologies for interactive UI.
- **Dependency Injection**: For service and repository management.
- **IFormFile**: For handling image uploads.
- **Visual Studio Solution Structure**: Multi-project architecture.

## Solution Structure

```
AnnouncementsWebApp.sln
├── AnnouncementsWebApp/         # ASP.NET Core Web API & UI
│   ├── Controllers/             # API controllers
│   ├── wwwroot/                 # Static files (HTML, JS, CSS, images)
│   ├── Properties/              # Launch settings
│   ├── Program.cs               # App startup
│   ├── appsettings.json         # Configuration
│   └── ...
├── Application/                 # Application layer (services, DTOs, mapping)
│   ├── Services/
│   ├── DTOs/
│   ├── Interfaces/
│   ├── MapperProfile/
│   └── ...
├── Domain/                      # Domain layer (models, repository interfaces)
│   ├── Models/
│   ├── IRepository/
│   └── ...
├── Infrastructure/              # Infrastructure layer (EF DbContext, repositories, migrations)
│   ├── Data/
│   ├── Repository/
│   ├── Migrations/
│   └── ...
```

## Features

- **CRUD Operations**: Create, read, update, and delete announcements.
- **Image Upload**: Store images in `wwwroot/images` and display in UI.
- **Search**: Filter announcements by title.
- **Responsive UI**: Modern, user-friendly interface.
- **API Documentation**: Swagger UI for testing endpoints.
- **Validation**: Server-side validation for input data.
- **Layered Architecture**: Separation of concerns for maintainability.

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Visual Studio 2022+](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)

### Setup

1. **Clone the repository**
   ```sh
   git clone <your-repo-url>
   cd AnnouncementsWebApp
   ```

2. **Configure the database**
   - Update the connection string in [`AnnouncementsWebApp/appsettings.json`](AnnouncementsWebApp/appsettings.json) under `ConnectionStrings:AnnouncementsCnn` to match your SQL Server instance.

3. **Apply migrations**
   ```sh
   dotnet ef database update --project Infrastructure --startup-project AnnouncementsWebApp
   ```

4. **Run the application**
   ```sh
   dotnet run --project AnnouncementsWebApp
   ```
   - The app will be available at [https://localhost:7160/index.html](https://localhost:7160/index.html) or [http://localhost:5174/index.html](http://localhost:5174/index.html).

5. **Access Swagger API docs**
   - Navigate to `/swagger` (e.g., [https://localhost:7160/swagger](https://localhost:7160/swagger)).

### Usage

- **Web UI**: Use the browser interface to add, edit, delete, and search announcements.
- **API**: Use Swagger or HTTP clients to interact with endpoints under `/api/announcements`.

### Project Highlights

- **DTOs**: Used for request/response models ([`Application/DTOs`](Application/DTOs)).
- **Services**: Business logic ([`Application/Services`](Application/Services)).
- **Repositories**: Data access ([`Infrastructure/Repository`](Infrastructure/Repository)).
- **DbContext**: EF Core context ([`Infrastructure/Data/AppDbContext.cs`](Infrastructure/Data/AppDbContext.cs)).
- **AutoMapper**: Mapping profiles ([`Application/MapperProfile/MapperProfile.cs`](Application/MapperProfile/MapperProfile.cs)).
- **Controllers**: API endpoints ([`AnnouncementsWebApp/Controllers/AnnouncementsController.cs`](AnnouncementsWebApp/Controllers/AnnouncementsController.cs)).
- **Frontend**: HTML, CSS, JS ([`AnnouncementsWebApp/wwwroot`](AnnouncementsWebApp/wwwroot)).

## API Endpoints

| Method | Endpoint                       | Description                      |
|--------|-------------------------------|----------------------------------|
| POST   | `/api/announcements`          | Add new announcement             |
| GET    | `/api/announcements`          | Get all announcements (search)   |
| GET    | `/api/announcements/{id}`     | Get announcement by ID           |
| PUT    | `/api/announcements/{id}`     | Update announcement by ID        |
| DELETE | `/api/announcements/{id}`     | Delete announcement by ID        |

## Contributing

1. Fork the repository.
2. Create your feature branch (`git checkout -b feature/YourFeature`).
3. Commit your changes.
4. Push to the branch.
5. Open a pull request.

## License

This project is licensed under the MIT License.

## Acknowledgements

- [Microsoft Docs](https://docs.microsoft.com/)
- [AutoMapper](https://automapper.org/)
- [Swagger](https://swagger.io/)
