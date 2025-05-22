# Online Order Management System

A **simplified** online order management system built with ASP.NET Core 9 and Entity Framework Core 9, following a Clean Architecture approach.

---

## 📂 Project Structure

```
OnlineOrderManagement/            # Solution root
├─ OnlineOrderManagement.Domain   # Domain: entities & repository interfaces
│  └─ Entities                     # Customer, Product, Order, OrderItem, OrderStatusHistory, AuditableEntity
├─ OnlineOrderManagement.Infrastructure
│  ├─ Data                         # EF Core AppDbContext, configuration, SaveChanges override
│  ├─ Migrations                   # EF Core migrations snapshots and scripts
│  └─ Repositories                 # Generic Repository<T>, UnitOfWork
├─ OnlineOrderManagement.Application
│  ├─ Common                       # PagedResult<T>
│  ├─ DTOs                         # Create/Update/Response record types
│  ├─ Services                     # ICustomerService, IProductService, IOrderService + implementations
│  └─ MappingProfile.cs            # AutoMapper configuration
└─ OnlineOrderManagement.Api       # ASP.NET Core Web API
   ├─ Controllers                  # REST endpoints (Customers, Products, Orders)
   ├─ Middlewares                  # ExceptionMiddleware
   ├─ Models                       # DTO definitions
   ├─ Program.cs                   # DI, middleware, Swagger setup
   ├─ appsettings.json             # Connection strings + logging
   └─ launchSettings.json          # Launch profile
```

---

## ⚙️ Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download)
- SQL Server (LocalDB or full instance)

---

## 🚀 Getting Started

1. **Clone the repository**
   ```bash
   git clone https://github.com/DevMohamedSayed/OnlineOrderManagement.git
   cd OnlineOrderManagement
   ```

2. **Configure the database**
   - Open `OnlineOrderManagement.Api/appsettings.json` and update the connection string:
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Server=.;Database=OnlineOrderDb;Trusted_Connection=True;TrustServerCertificate=True;"
     }
     ```

3. **Apply EF Core migrations**
   ```Package manger console
  dotnet ef database update  --project OnlineOrderManagement.Infrastructure/OnlineOrderManagement.Infrastructure.csproj --startup-project OnlineOrderManagement.Api/OnlineOrderManagement.Api.csproj
   ```

4. **Run the Web API**
   ```bash
   cd ../OnlineOrderManagement.Api
   dotnet run
   ```
   By default, Swagger UI launches at the root `/` without needing `/swagger`.

5. **Explore the API**
   Open your browser and navigate to:
   ```
 (https://localhost:44384/swagger/index.html)
   ```
   You’ll see interactive documentation for Customers, Products, and Orders, including CRUD operations with paging, filtering, and sorting.

---

## 🛠 Key Features

- **Clean Architecture**: Separate Domain, Infrastructure, Application, and API layers for maintainability.
- **DTOs & AutoMapper**: Explicit Create/Update/Response record types to prevent over-posting and avoid circular references.
- **Validation** via data annotations:
  - `Quantity ≥ 1` (integer only)
  - `StockQuantity ≥ 1`
  - `Price > 0`
  - Valid `EmailAddress`
  - `Phone` format (10–11 digits, Egyptian prefixes)
  - `Name` without digits
  - `SerialNumber`: eight uppercase letters or digits, unique
- **Business Logic**:
  - **Order creation**: verifies stock availability, computes subtotal, and decrements inventory.
  - **Order status updates**: records a timestamped history in `OrderStatusHistory`.
- **Paging, Filtering & Sorting**: Supports `page`, `pageSize`, `search`, `minPrice`, `maxPrice`, `sortBy`, and `sortDesc` query parameters.
- **Global Exception Handling & Logging**: Centralized middleware catches unhandled exceptions and logs structured error responses.
- **Swagger UI** at the root for interactive API exploration.

---

## 🔗 Publishing to GitHub

1. On GitHub, **create** a new repository named `OnlineOrderManagement`.
2. In your local project folder:
   ```bash
   git init                         # if not already a Git repo
   git add .
   git commit -m "Initial import"
   git branch -M main               # optionally rename master to main
   git remote add origin https://github.com/DevMohamedSayed/OnlineOrderManagement.git
   git push -u origin main
   ```
3. Visit your repo at:
   ```
   https://github.com/DevMohamedSayed/OnlineOrderManagement
   ```

---

## 🔭 Future Improvements

- Add **authentication/authorization** (e.g., JWT, role-based access).
- Implement **soft deletes** (`IsDeleted`) if logical removal is needed.
- Enhance **dynamic querying** with libraries like `System.Linq.Dynamic.Core`.
- Add **unit and integration tests**.
- **Containerize** with Docker (`Dockerfile` and `docker-compose.yml`).

---

© 2025 Mohamed Sayed | OnlineOrderManagement Implementation
