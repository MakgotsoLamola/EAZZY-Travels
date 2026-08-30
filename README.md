# EaZZy-Travels – Plane Provider & Transport Provider Modules

ASP.NET Web Forms (C#) CRUD interfaces for the **Plane Provider** and
**Transport Provider** entities from the EaZZy-Travels data model (Phase 3),
matching the Add / Update / Delete / duplicate-warning requirements from
Phase 2's functional requirements.

## Files
- `SQL/CreateTables.sql` – MySQL script that creates `Employee`,
  `PlaneProvider`, and `TransportProvider`, with seed data.
- `App_Code/DBConnection.cs` – shared MySQL connection helper.
- `PlaneProvider.aspx` / `PlaneProvider.aspx.cs` – Maintain Plane Provider
  Records screen (Add, Update, Delete, list with GridView).
- `TransportProvider.aspx` / `TransportProvider.aspx.cs` – Maintain
  Transport Provider Records screen (same pattern).
- `Web.config` – MySQL connection string and target framework.

## Setup in Visual Studio
1. Create a new **ASP.NET Web Application (.NET Framework) – Web Forms**
   project, then copy these files into it (keeping the `App_Code` folder).
2. Install the MySQL connector via NuGet:
   ```
   Install-Package MySql.Data
   ```
3. Run `SQL/CreateTables.sql` against your MySQL server (e.g. via MySQL
   Workbench or the `mysql` CLI) to create the database and tables.
4. In `Web.config`, update the `Server`, `Uid`, and `Pwd` values to match
   your MySQL instance.
5. Set `PlaneProvider.aspx` or `TransportProvider.aspx` as the start page
   and run the project (F5).

## Features implemented
- **Add**: validates all required fields client-side, checks for a
  duplicate (same Location + Type + Tickets/Insurance) before inserting,
  and shows a success/warning message — as specified in the Phase 2
  functional requirements table.
- **Update**: select a row in the grid to load it into the form, edit,
  then click Update (also duplicate-checked, excluding the current row).
- **Delete**: select a row, click Delete, confirm the browser prompt.
- **View**: a `GridView` lists all records and doubles as the row
  selector for editing.
- Both providers link to an `Employee` foreign key via a dropdown list,
  matching the ERD's `EmployeeID` foreign key on both provider tables.

## Notes
- The proposal mentions MySQL specifically, so this uses
  `MySql.Data.MySqlClient`. If your team prefers SQL Server instead, the
  SQL script and `DBConnection.cs` are the only two files that would need
  to change (swap to `System.Data.SqlClient` and T-SQL syntax).
- Parameterised queries are used throughout to prevent SQL injection.
