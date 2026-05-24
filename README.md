# ASP.NET Core MVC — Product Management Application

A full-stack web application built with **ASP.NET Core 8 MVC** for managing a product inventory. The project demonstrates core MVC patterns, Entity Framework Core with SQL Server, data annotation validation (including custom attributes), and the CRUD operations pattern.

---

## Table of Contents

- [Overview](#overview)
- [Tech Stack](#tech-stack)
- [Project Structure](#project-structure)
- [Features](#features)
- [Data Models](#data-models)
- [Validation](#validation)
- [Getting Started](#getting-started)
- [Database Setup](#database-setup)
- [Seed Data](#seed-data)
- [Author](#author)

---

## Overview

This application provides a product management interface where users can:

- Browse a paginated list of all products with category labels
- View detailed product information including pricing, stock count, and date metadata
- Create new products with full server-side and client-side validation
- Update existing product records
- Navigate a clean Bootstrap 5 UI

The project is structured around the MVC pattern with a clear separation between Models, Views, Controllers, ViewModels, and Data access logic.

---

## Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core 8 (MVC) |
| Language | C# (.NET 8.0) |
| ORM | Entity Framework Core 8 |
| Database | Microsoft SQL Server (LocalDB / SQL Express) |
| Frontend | Razor Views, Bootstrap 5.1, jQuery |
| Validation | Data Annotations, Remote Validation, Custom Attributes |

---

## Project Structure

```
Day04Lab/
├── Controllers/
│   ├── HomeController.cs          # Home and Privacy pages
│   └── ProductController.cs       # Product CRUD + Remote validation
│
├── Models/
│   ├── Products.cs                # Product entity
│   ├── Categories.cs              # Category entity
│   └── ErrorViewModel.cs
│
├── ViewModels/
│   ├── ProductsIndexVM.cs         # List view projection
│   ├── ProductsDetailsVM.cs       # Detail view projection
│   └── ProductsCreateVM.cs        # Create/Edit form model
│
├── Data/
│   ├── Context/
│   │   └── Context.cs             # DbContext with seed data
│   └── Configrations/
│       └── ProductConfigration.cs # Fluent API configuration
│
├── Validations/
│   ├── ExpireDateValidationAttribute.cs
│   └── ProductionDateValidationAttribute.cs
│
├── Migrations/
│   └── 20260524130701_Init.cs
│
└── Views/
    ├── Home/
    │   ├── Index.cshtml
    │   └── Privacy.cshtml
    ├── Product/
    │   ├── Index.cshtml
    │   ├── MoreDetails.cshtml
    │   ├── CreateNewProduct.cshtml
    │   └── UpdateProduct.cshtml
    └── Shared/
        └── _Layout.cshtml
```

---

## Features

### Product Listing
The `ProductController.Index()` action queries all products and projects them into `ProductsIndexVM`, displaying the title, description, price, and associated category name in a tabular view.

### Product Details
`ProductController.MoreDetails(int id)` fetches a single product with its related category using `Include()`, mapping to `ProductsDetailsVM` which also exposes production and expiry dates and stock count.

### Create Product
A two-action pattern (`[HttpGet]` / `[HttpPost]`) handles the create form. The GET action populates the `CategoriesList` dropdown via `SelectListItem`. The POST action validates the model — clearing `CategoriesList` from `ModelState` before checking `ModelState.IsValid` — then persists the entity.

### Remote Validation
The `Title` field uses `[Remote("IsTitleAvailable", "Product")]` to check for duplicate titles asynchronously via an AJAX call before form submission.

---

## Data Models

### `Products`
| Property | Type | Notes |
|---|---|---|
| `Id` | `int` | Primary key |
| `Title` | `string` | Required, min 10 chars |
| `Description` | `string` | Required, min 10 chars |
| `Price` | `int` | Required, min value 5 |
| `Count` | `int` | Stock quantity |
| `Date_Of_Production` | `DateOnly` | Custom validation applied |
| `Date_Of_Expire` | `DateOnly` | Custom validation applied |
| `CategoriesId` | `int` | Foreign key |
| `Category` | `Categories` | Navigation property |

### `Categories`
| Property | Type | Notes |
|---|---|---|
| `Id` | `int` | Primary key |
| `Name` | `string` | Required |
| `Product` | `List<Products>` | Navigation property |

---

## Validation

### Standard Annotations (on `ProductsCreateVM`)
- `[Required]` on all user-facing fields
- `[MinLength(10)]` on `Title` and `Description`
- `[Range(5, int.MaxValue)]` on `Price`
- `[Remote]` on `Title` for duplicate-title detection

### Custom Attributes

**`ProductionDateValidationAttribute`** — ensures the production date is not in the future and satisfies any business-logic constraints around when a product can be entered.

**`ExpireDateValidationAttribute`** — ensures the expiry date is after the production date and is a valid future date.

Both attributes inherit from `ValidationAttribute` and provide descriptive error messages.

---

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8)
- SQL Server or SQL Server Express (instance name configurable in `Context.cs`)
- Visual Studio 2022+ or VS Code with C# Dev Kit

### Clone & Run

```bash
git clone https://github.com/Ahmed-Magdy-Elzayady/ASP.NET-Core-MVC-Application.git
cd ASP.NET-Core-MVC-Application
```

---

## Database Setup

The connection string is configured directly in `Data/Context/Context.cs` inside `OnConfiguring`:

```csharp
string ConnectionString =
    "Data source=<YOUR_SERVER>\\SQLEXPRESS;" +
    "Initial catalog=Day04MVcLab;" +
    "Integrated security=true;" +
    "TrustServerCertificate=True";
```

Update `<YOUR_SERVER>` to match your local SQL Server instance name, then apply the migration:

```bash
dotnet ef database update
```

This will create the `Day04MVcLab` database and apply the `Init` migration.

> **Note:** If you prefer using `appsettings.json` for connection strings, move the connection string there and register the `DbContext` in `Program.cs` using `AddDbContext<Context>(options => options.UseSqlServer(...))`.

---

## Seed Data

The `OnModelCreating` method in `Context.cs` seeds the following reference data automatically on first migration:

**Categories:**
- Dry Groceries
- Dairy & Eggs
- Meat & Frozen Food
- Canned Goods

**Sample Products** (seeded for development):
- Premium Egyptian Rice — Dry Groceries
- Penne Pasta — Dry Groceries
- Sunflower Oil — Dry Groceries
- Full Cream Milk — Dairy & Eggs
- *(and more)*

---

## Author

**Ahmed Magdy Elzayady**
[GitHub](https://github.com/Ahmed-Magdy-Elzayady) · ahmed.m.elzayady@gmail.com

---

> This project was developed as part of a full-stack MVC learning track. Version 4.0.0.
