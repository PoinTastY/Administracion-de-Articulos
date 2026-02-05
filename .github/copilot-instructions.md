# Copilot instructions

## Big picture
- Solution is split into API (src/backEnd/ArticleManagement.API), application layer (src/backEnd/Core.Application), domain (src/backEnd/Core.Domain), and infrastructure (src/backEnd/Infrastructure).
- API wires dependencies via ServiceDescriptors/ServiceDescriptor.cs and exposes controllers in ArticleManagement.API/Controllers.
- EF Core + Npgsql are used, with migrations applied on API startup (Program.cs).
- Frontend is a Next.js app in src/fronten/article-management-cucei (note folder name is "fronten").

## Backend architecture and patterns
- Controllers are thin and use [Route("[controller]")] (e.g., Controllers/StudentController.cs).
- Application services are interfaces in Core.Application.Interfaces and implemented in Infrastructure.Service (e.g., IStudentService -> StudentService).
- Repositories are interfaces in Core.Domain.Interfaces and implemented in Infrastructure.Data.Repos (e.g., IStudentRepo -> StudentRepo).
- DTOs live in Core.Application.DTOs; use records for immutable payloads (StudentDto, GenericCategoryDto) and classes for mutable inputs (ExtendRequestDto).
- Domain entities are in Core.Domain.Entities; Student has unique StudentCode and ExtendRequest FK maps to StudentCode (see Infrastructure/Data/ArticleManagementDbContext.cs).
- Enums expose helper methods for dropdowns: ArticleExtension.GetAll(), RequestStatusExtension.GetAll().

## Configuration and integration points
- Database connection string comes from env var ARTICLE_MANAGEMENT_DB_CONNECTION; startup throws if missing.
- Kestrel HTTP endpoint defaults to http://localhost:2121 from appsettings.json.
- Serilog is configured from appsettings.json and writes rolling files to ArticleManagement.API/Logs/log-.txt.

## Frontend
- Next.js app uses the App Router; default entry is app/page.tsx.
- Scripts are in package.json (dev/build/start/lint) under src/fronten/article-management-cucei.

## Developer workflows
- Run API from ArticleManagement.API: dotnet run (expects ARTICLE_MANAGEMENT_DB_CONNECTION set).
- EF Core migrations are applied automatically on startup (no manual migration step in code).
- Run frontend from src/fronten/article-management-cucei: npm run dev (or yarn/pnpm/bun).

## Project-specific behavior to preserve
- Student creation uses "add then update" flow: StudentService.AddAsync returns 0 on existing code, controller attempts UpdateAsync.
- When mapping enums for dropdowns, use GenericCategoryDto over raw enum values.
