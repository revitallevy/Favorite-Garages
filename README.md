# Migdal – Favorite Garages

Full-stack assignment for Migdal Technologies.

The application allows users to:
- View official garages from a government dataset
- Select multiple garages
- Save them as favorites
- Prevent duplicate favorites
- Toggle between all garages and favorites only

---

## Tech Stack

**Backend**
- .NET 10 Web API
- EF Core
- SQL Server LocalDB
- Layered architecture (Controller → Service → Repository)
- Unique index on `ExternalGarageId`
- Government API consumed server-side

**Frontend**
- Angular 21 (Standalone, Zoneless)
- Signals (`signal`, `computed`, `effect`)
- Tailwind CSS v4

---

## Run the Application

### Backend

```bash
cd Migdal.Garages.Api
dotnet run
```

Runs on:

https://localhost:7218

---

### Frontend

```bash
cd garage-app
npm install
ng serve
```

Runs on:

http://localhost:4200

Frontend expects API at:

https://localhost:7218/api  
(configured in `environment.development.ts`)

---

## Notes

- Database stores **favorites only** (per assignment requirement).
- Duplicate prevention implemented at service level and database level.
- No direct client calls to the government API.
- Focused on clean, minimal enterprise architecture.
