<div align="center">

# 🛒 Product Management System

### A full-stack CRUD web application built with ASP.NET Core 8 MVC

[![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=flat-square&logo=dotnet)](https://dotnet.microsoft.com/download/dotnet/8)
[![ASP.NET Core MVC](https://img.shields.io/badge/ASP.NET_Core-MVC-512BD4?style=flat-square&logo=dotnet)](https://docs.microsoft.com/en-us/aspnet/core/mvc)
[![Entity Framework Core](https://img.shields.io/badge/EF_Core-8.0-512BD4?style=flat-square&logo=nuget)](https://docs.microsoft.com/en-us/ef/core/)
[![SQL Server](https://img.shields.io/badge/SQL_Server-LocalDB%2FExpress-CC2927?style=flat-square&logo=microsoftsqlserver)](https://www.microsoft.com/en-us/sql-server)
[![Bootstrap](https://img.shields.io/badge/Bootstrap-5.1-7952B3?style=flat-square&logo=bootstrap)](https://getbootstrap.com)

</div>

---

## 📋 Table of Contents

- [Overview](#-overview)
- [Tech Stack](#-tech-stack)
- [Architecture & Project Structure](#-architecture--project-structure)
- [Features](#-features)
- [Data Models](#-data-models)
- [Validation System](#-validation-system)
- [Getting Started](#-getting-started)
- [Database Setup](#-database-setup)
- [Seed Data](#-seed-data)
- [API Endpoints](#-api-endpoints)
- [Contributing](#-contributing)
- [Author](#-author)

---

## 🔍 Overview

**Product Management System** is a full-stack web application that provides a complete inventory management interface. Built on the **ASP.NET Core 8 MVC** framework, it demonstrates enterprise-grade patterns including the Repository-ready MVC architecture, Entity Framework Core with Fluent API configuration, layered ViewModel projection, and a robust dual-layer validation system combining standard Data Annotations with custom-built validation attributes.

The application is ideal as a reference implementation or learning project for developers working with the .NET ecosystem.

### What you can do

| Action | Description |
|---|---|
| 📦 **Browse Products** | Paginated product listing with category labels, pricing, and descriptions |
| 🔎 **View Details** | Full product detail page including stock count and date metadata |
| ➕ **Create Products** | Form-driven product creation with full server-side and AJAX remote validation |
| ✏️ **Update Products** | Edit existing product records with pre-populated form fields |
| 🗑️ **Delete Products** | Remove products from inventory with immediate redirect |

---

## 🛠 Tech Stack

| Layer | Technology | Version |
|---|---|---|
| **Framework** | ASP.NET Core MVC | 8.0 |
| **Language** | C# | 12 / .NET 8.0 |
| **ORM** | Entity Framework Core | 8.0.0 |
| **Database** | Microsoft SQL Server (LocalDB / SQL Express) | — |
| **Frontend** | Razor Views + Bootstrap | 5.1 |
| **Interactivity** | jQuery (Unobtrusive Validation) | — |
| **Validation** | Data Annotations, Remote Validation, Custom Attributes | — |

---

## 🏗 Architecture & Project Structure

This project follows a clean **MVC + ViewModel** pattern. Business entities (Models) are never exposed directly to views; instead, purpose-built ViewModels carry only the data each view requires.

```
ASP.NET-Core-MVC-Application/
├── Day04Lab.sln
└── Day04Lab/
    │
    ├── Controllers/
    │   ├── HomeController.cs              # Home & Privacy pages
    │   └── ProductController.cs           # Full CRUD + Remote validation endpoint
    │
    ├── Models/                            # Domain entities (EF Core mapped)
    │   ├── Products.cs                    # Product entity with navigation property
    │   ├── Categories.cs                  # Category entity with collection navigation
    │   └── ErrorViewModel.cs
    │
    ├── ViewModels/                        # View-specific projections (no over-fetching)
    │   ├── ProductsIndexVM.cs             # Lightweight list projection
    │   ├── ProductsDetailsVM.cs           # Full detail projection
    │   └── ProductsCreateVM.cs            # Create/Edit form model with validation
    │
    ├── Data/
    │   ├── Context/
    │   │   └── Context.cs                 # DbContext — OnConfiguring + seed data
    │   └── Configrations/
    │       └── ProductConfigration.cs     # Fluent API entity configuration
    │
    ├── Validations/                       # Custom ValidationAttribute implementations
    │   ├── ExpireDateValidationAttribute.cs
    │   └── ProductionDateValidationAttribute.cs
    │
    ├── Migrations/
    │   └── 20260524130701_Init.cs         # Initial schema migration
    │
    ├── Views/
    │   ├── Home/
    │   │   ├── Index.cshtml
    │   │   └── Privacy.cshtml
    │   ├── Product/
    │   │   ├── Index.cshtml               # Product listing table
    │   │   ├── MoreDetails.cshtml         # Product detail page
    │   │   ├── CreateNewProduct.cshtml    # Create form
    │   │   └── UpdateProduct.cshtml       # Edit form
    │   └── Shared/
    │       └── _Layout.cshtml             # Bootstrap 5 shell layout
    │
    ├── Program.cs                         # Application entry point & middleware pipeline
    ├── appsettings.json
    └── appsettings.Development.json
```

---

## ✨ Features

### Product Listing
`ProductController.Index()` queries all products and projects them into `ProductsIndexVM`, selecting only the fields required for the table view — title, description, price, and the related category name — avoiding over-fetching.

### Product Details
`ProductController.MoreDetails(int id)` performs an eager-loaded query using EF Core's `Include()` to fetch the related `Category` in a single round-trip, then maps the result to `ProductsDetailsVM`. Returns `404 Not Found` if the product does not exist.

### Create Product
Implements the standard **GET/POST** action pair. The GET action populates a `SelectList` of categories for the dropdown. The POST action removes `CategoriesList` from `ModelState` before validation (since the list is not submitted by the form) and persists the entity on success, redirecting to the index.

### Update Product
Mirrors the create pattern. The GET action fetches the existing record and pre-populates `ProductsCreateVM`. The POST action patches only the fields exposed by the form, using EF Core's change-tracking to persist the update.

### Delete Product
A single-action handler that fetches the entity, removes it from the `DbSet`, saves changes, and redirects to the index. Returns `404 Not Found` for unknown IDs.

### Remote (AJAX) Title Validation
The `Title` field on the create/edit form is decorated with `[Remote("IsTitleAvailable", "Product")]`. Before submission, jQuery Unobtrusive Validation fires an asynchronous `GET` request to `ProductController.IsTitleAvailable(string title, int id)`, which returns `true` if the title is unique (excluding the current product during an edit), or a human-readable error message string otherwise.

---

## 🗄 Data Models

### `Products`

| Property | Type | Constraints |
|---|---|---|
| `Id` | `int` | Primary key, auto-increment |
| `Title` | `string` | Required, min length 10 |
| `Description` | `string` | Required, min length 10 |
| `Price` | `int` | Required, minimum value 5 |
| `Count` | `int` | Stock quantity |
| `Date_Of_Production` | `DateOnly` | Custom: must not be a future date |
| `Date_Of_Expire` | `DateOnly` | Custom: must be after production date |
| `CategoriesId` | `int` | Foreign key → `Categories.Id` |
| `Category` | `Categories` | Virtual navigation property |

### `Categories`

| Property | Type | Notes |
|---|---|---|
| `Id` | `int` | Primary key, auto-increment |
| `Name` | `string` | Required |
| `Product` | `List<Products>` | Navigation property (one-to-many) |

---

## ✅ Validation System

The application uses a two-layer validation strategy to ensure data integrity on both client and server sides.

### Standard Data Annotations (on `ProductsCreateVM`)

| Annotation | Applied To | Rule |
|---|---|---|
| `[Required]` | All fields | Field must not be null or empty |
| `[MinLength(10)]` | `Title`, `Description` | Minimum 10 characters |
| `[Range(5, int.MaxValue)]` | `Price` | Must be at least 5 |
| `[Remote]` | `Title` | AJAX duplicate-title check |

### Custom Validation Attributes

**`ProductionDateValidationAttribute`**
Inherits from `ValidationAttribute` and overrides `IsValid`. Compares the submitted `DateOnly` value against `DateOnly.FromDateTime(DateTime.Today)` — if the production date is set in the future, validation fails with a descriptive error message.

```csharp
// Usage on ViewModel
[ProductionDateValidation]
public DateOnly Date_Of_Production { get; set; }
```

**`ExpireDateValidationAttribute`**
Also inherits from `ValidationAttribute`. Uses `ValidationContext.ObjectInstance` to access the parent `ProductsCreateVM` and cross-validates that the expiry date is not earlier than the production date.

```csharp
// Usage on ViewModel
[ExpireDateValidation]
public DateOnly Date_Of_Expire { get; set; }
```

---

## 🚀 Getting Started

### Prerequisites

Ensure the following are installed before running the application:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8)
- [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or SQL Server LocalDB
- [Visual Studio 2022+](https://visualstudio.microsoft.com/) (recommended) **or** VS Code with the C# Dev Kit extension

### Clone the Repository

```bash
git clone https://github.com/Ahmed-Magdy-Elzayady/ASP.NET-Core-MVC-Application.git
cd ASP.NET-Core-MVC-Application
```

### Restore Dependencies

```bash
dotnet restore
```

---

## 🗃 Database Setup

The connection string is configured directly in `Data/Context/Context.cs` inside the `OnConfiguring` override:

```csharp
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    string ConnectionString =
        "Data source=<YOUR_SERVER>\\SQLEXPRESS;" +
        "Initial catalog=Day04MVCLab;"           +
        "Integrated security=true;"              +
        "TrustServerCertificate=True";

    optionsBuilder.UseSqlServer(ConnectionString);
}
```

**Steps:**

1. Replace `<YOUR_SERVER>` with your local SQL Server instance name (e.g., `DESKTOP-ABC123`).
2. Apply the existing migration to create the database schema and seed data:

```bash
dotnet ef database update
```

This creates the `Day04MVCLab` database and applies the `Init` migration automatically.

> **💡 Tip:** For a more production-aligned setup, move the connection string to `appsettings.json` and register the `DbContext` in `Program.cs`:
>
> ```csharp
> builder.Services.AddDbContext<Context>(options =>
>     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
> ```

---

## 🌱 Seed Data

The `OnModelCreating` method in `Context.cs` automatically seeds reference categories and sample products on the first migration, so the application is immediately usable without manual data entry.

### Categories

| ID | Name |
|---|---|
| 1 | Dry Groceries |
| 2 | Dairy & Eggs |
| 3 | Meat & Frozen Food |
| 4 | Canned Goods |

### Sample Products

| Title | Category | Price |
|---|---|---|
| Premium Egyptian Rice | Dry Groceries | 35 EGP |
| Penne Pasta | Dry Groceries | 15 EGP |
| Sunflower Oil | Dry Groceries | 80 EGP |
| Full Cream Milk | Dairy & Eggs | 42 EGP |
| Feta White Cheese | Dairy & Eggs | 38 EGP |
| Natural Yogurt | Dairy & Eggs | 8 EGP |
| *(and more…)* | — | — |

---

## 🔌 API Endpoints

| Method | Route | Action | Description |
|---|---|---|---|
| `GET` | `/Product` | `Index` | Returns the product listing view |
| `GET` | `/Product/MoreDetails/{id}` | `MoreDetails` | Returns the product detail view |
| `GET` | `/Product/CreateNewProduct` | `CreateNewProduct` | Returns the create form |
| `POST` | `/Product/CreateNewProduct` | `CreateNewProduct` | Validates and persists a new product |
| `GET` | `/Product/UpdateProduct/{id}` | `UpdateProduct` | Returns the pre-populated edit form |
| `POST` | `/Product/UpdateProduct` | `UpdateProduct` | Validates and applies product updates |
| `GET` | `/Product/DeleteProduct/{id}` | `DeleteProduct` | Deletes the product and redirects |
| `GET` | `/Product/IsTitleAvailable` | `IsTitleAvailable` | Remote validation — returns `true` or an error string |

---

## 🤝 Contributing

Contributions, issues, and feature requests are welcome.

1. Fork the repository
2. Create a feature branch: `git checkout -b feature/your-feature-name`
3. Commit your changes: `git commit -m "feat: add your feature"`
4. Push to your branch: `git push origin feature/your-feature-name`
5. Open a Pull Request

---

## 👤 Author

**Ahmed Magdy Elzayady**

- GitHub: [@Ahmed-Magdy-Elzayady](https://github.com/Ahmed-Magdy-Elzayady)
- Email: [ahmed.m.elzayady@gmail.com](mailto:ahmed.m.elzayady@gmail.com)

---

<div align="center">

Developed as part of a full-stack ASP.NET Core MVC learning track · **v4.0.0**

</div>
