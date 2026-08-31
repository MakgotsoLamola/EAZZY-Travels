# EaZZy-Travels — Hotel & Entertainment Provider pages (ASP.NET Web Forms)

Two Web Forms pages you can drop into your existing ASP.NET Web Forms project,
matching the fields and rules from your Phase 2 (Functional Requirements) and
Phase 3 (Data Model / ERD) documents.

## What's in here

| File | Purpose |
|---|---|
| `HotelProviders.aspx` / `.aspx.cs` / `.aspx.designer.cs` | Hotel Provider CRUD page |
| `EntertainmentProviders.aspx` / `.aspx.cs` / `.aspx.designer.cs` | Entertainment Provider CRUD page |
| `DbHelper.cs` | One shared class that opens the MySQL connection |
| `Styles/EazzyTheme.css` | The purple/pink/black/white look, shared by both pages |
| `schema.sql` | Creates the two provider tables (+ a minimal Employee table for the FK) |
| `WebConfig_ConnectionString_Snippet.xml` | The connection string to add to your Web.config |

## Setup steps

1. **Install the MySQL driver.**
   In Visual Studio: right-click your project → Manage NuGet Packages → search
   `MySql.Data` → Install.

2. **Copy the files into your project.**
   Drag all the `.aspx`, `.aspx.cs`, `.aspx.designer.cs` files, `DbHelper.cs`,
   and the `Styles` folder into your Web Forms project in Solution Explorer
   (or use "Add Existing Item").

3. **Run the SQL script.**
   Open `schema.sql` in MySQL Workbench (or your MySQL client of choice) and
   run it against your database. It creates `HotelProvider`,
   `EntertainmentProvider`, and a small `Employee` table (skip that last part
   if you already have a fuller Employee table — just make sure the
   `EmployeeID` column is `VARCHAR(10)` to match).

4. **Add the connection string.**
   Open `WebConfig_ConnectionString_Snippet.xml`, copy the
   `<connectionStrings>` block into your project's `Web.config` (as a direct
   child of `<configuration>`), and update the server/username/password to
   match your MySQL setup.

5. **Build and run.**
   Right-click `HotelProviders.aspx` (or `EntertainmentProviders.aspx`) →
   "Set As Start Page", then run the project (F5).

## Notes

- **Staff ID / Role box** (top right of each page) stands in for a real login
  system. Whatever Staff ID is set becomes the `EmployeeID` foreign key on
  any record added while it's active. Switching Role to "Administrator"
  unlocks the Delete button — matching the non-functional requirement that
  only admins can delete records.
- **Validation, duplicate checks, and success messages** follow the
  input/processing/output tables in your Phase 2 document exactly: missing
  fields are called out by name, a duplicate type+location (or
  activity+location) blocks the save, and every add/update/delete confirms
  with a message.
- The `.aspx.designer.cs` files are included so the project compiles as soon
  as you add the files — normally Visual Studio generates these
  automatically the first time you open the `.aspx` file in the designer. If
  you ever get a "control does not exist" error after editing the markup,
  just delete the designer file and re-save the `.aspx` in Visual Studio to
  regenerate it.
