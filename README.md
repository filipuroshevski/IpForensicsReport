# IP Forensics Report

A full-stack application where an authenticated user can submit an IP address and
generate a forensic report. The backend calls two public APIs
([ip-api.com](http://ip-api.com) for geolocation and
[AbuseIPDB](https://www.abuseipdb.com) for reputation), combines the results into a
single report, encrypts the sensitive fields, and stores them in MySQL. Users can
then browse their own previously generated reports.


## Tech stack

- **Backend:** ASP.NET Core (.NET 10) Web API, EF Core, ASP.NET Core Identity, JWT auth
- **Database:** MySQL (via `MySql.EntityFrameworkCore`)
- **Frontend:** Angular + Bootstrap 5
- **Encryption at rest:** ASP.NET Core Data Protection

## Project structure

```
IpForensicsReport/        # ASP.NET Core Web API (startup project)
Service/                  # Business logic (auth, report generation, encryption)
Data/                     # EF Core DbContext, entities, repositories, migrations
Domain/                   # Models, DTOs, validators
angular/                  # Angular frontend
```

## 1. Clone

```bash
git clone https://github.com/filipuroshevski/IpForensicsReport.git
```

## 2. Configure the backend

Open `IpForensicsReport/appsettings.json` and set the values for your environment:

```jsonc
{
  "ConnectionStrings": {
    "Default": "server=localhost;port=3306;database=reportapplication;user=root;password=YOUR_DB_PASSWORD;"
  },
  "JWT": {
    "Secret": "a-long-random-secret-at-least-32-characters",
    "ValidIssuer": "https://localhost:7100",
    "ValidAudience": "http://localhost:4200"
  },
  "MS": { "TokenExpiredHours": "8" },
  "Apis": {
    "IpApiBaseUrl": "http://ip-api.com/",
    "AbuseIpDbBaseUrl": "https://api.abuseipdb.com/api/v2/",
    "AbuseIpDbApiKey": "YOUR_ABUSEIPDB_API_KEY"
  }
}
```

- Create a MySQL database (e.g. `reportapplication`) or let the migration create it.
- Get a free AbuseIPDB API key at https://www.abuseipdb.com/account/api.

> **Security note:** for anything beyond local dev, move `JWT:Secret`,
> `Apis:AbuseIpDbApiKey`, and the DB password out of `appsettings.json` into
> .NET User Secrets or environment variables (e.g. `Apis__AbuseIpDbApiKey`).
> No code changes are needed — they're read through `IConfiguration`.

## 3. Create the database and apply migrations

First create an empty MySQL database (the tables themselves are created by the
migrations in the next command):

```sql
CREATE DATABASE reportapplication;
```

Then, from the repository root, apply the EF Core migrations:

```bash
dotnet ef database update -p Data/Data.csproj -s IpForensicsReport/IpForensicsReport.csproj
```

This runs both migrations in order and builds the full schema — no manual SQL needed:

- `InitialIdentity` — the ASP.NET Identity tables (`User`, roles, etc.)
- `AddIpForensicsReport` — the `IpForensicsReport` table and its `UserId → User.Id` foreign key

Verify it worked:

```sql
USE reportapplication;
SHOW TABLES;                           -- includes IpForensicsReport and the AspNet*/User tables
SHOW CREATE TABLE IpForensicsReport;   -- shows FK_IpForensicsReport_User_UserId
```

## 4. Run the backend

```bash
dotnet run --project IpForensicsReport --launch-profile http
```

The API starts at **http://localhost:5126**, with Swagger UI at
**http://localhost:5126/swagger**. (CORS is already configured to allow the
Angular app at `http://localhost:4200`.)

## 5. Run the frontend

In a second terminal:

```bash
cd angular
npm install --force
ng serve
```

The app starts at **http://localhost:4200**. The frontend is configured to call
the API at `http://localhost:5126/api/`.

## Using the app

1. Open http://localhost:4200 and **Register** a new account, then **Log in**.
2. From the dashboard, choose **Generate New Report**, enter an IP address (e.g.
   `8.8.8.8`), and submit.
3. Open **My Reports** to see your generated reports (decrypted on retrieval,
   scoped to your account).

