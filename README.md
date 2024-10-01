
# Product and Invoice Management System

This project is built using ASP.NET Core 8, Web API, MVC JavaScript (AJAX), and RDLC reporting. The system allows for managing products, customers, creating invoices, and viewing invoice lists with customer - wise filters.

## Features

1. **Create Product**
   - Add new products with fields like Name, Price, and Stock.
   - Products can be managed through the system (CRUD operations).
   
2. **Create Customer**
   - Register new customers with basic information (e.g., Name, ).
   - Includes validation for required fields and data consistency.
   
3. **Sell Products to Customers**
   - Create an invoice for customers by adding selected products.
   - The invoice includes product details, quantity, price, and total amounts.
   - The system generates invoices using RDLC reports.

4. **Show Invoice List**
   - Displays a list of invoices with customer-wise filters/search functionality.
   - View and manage existing invoices with CRUD operations.

## Technologies Used

- **ASP.NET Core 8 MVC**: Backend logic, controllers, and views.
- **ASP.NET Core Web API**: Exposes APIs for managing data.
- **Entity Framework Core**: Database access with migrations and relationships.
- **JavaScript AJAX**: Handles asynchronous calls to the Web API for CRUD operations.
- **RDLC**: Generates reports for invoices in the system.
- **Clean Architecture**: Ensures separation of concerns and maintainable code.

## Project Structure

The project follows Clean Architecture principles, dividing the codebase into different layers:

- **Domain Layer**: Holds core entities like `Customer`, `Product`, `Invoice`, and `InvoiceItem`.
- **Application Layer**: Contains services, DTOs, and interfaces.
- **Infrastructure Layer**: Implements database operations using Entity Framework.
- **Presentation Layer**: ASP.NET Core MVC controllers and views handle the user interface.
- **WebAPI Layer**: Exposes endpoints for external interaction using JavaScript AJAX.

## Setup

### Prerequisites

- [.NET SDK 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- SQL Server
- Visual Studio 2022 or JetBrains Rider (for development)
- RDLC Report Designer extension

### Installation

1. **Clone the repository:**
   ```bash
   git clone https://github.com/rifatur/product-invoice-management.git
   cd product-invoice-management
   ```

2. **Setup the database:**
   Update the connection string in `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER;Database=InvoiceDB;Trusted_Connection=True;"
     }
   }
   ```

3. **Run database migrations:**
   ```bash
   dotnet ef database update
   ```

4. **Run the application:**
   ```bash
   dotnet run
   ```

### Endpoints

- **Product Management**
  - `POST /api/products` - Create a new product.
  - `GET /api/products` - Retrieve a list of products.
  - `PUT /api/products/{id}` - Update an existing product.
  - `DELETE /api/products/{id}` - Delete a product.
  
- **Customer Management**
  - `POST /api/customers` - Create a new customer.
  - `GET /api/customers` - Retrieve a list of customers.

- **Invoice Management**
  - `POST /api/invoices` - Create an invoice.
  - `GET /api/invoices` - Retrieve a list of invoices with customer-wise filters.
  - `GET /api/invoices/{id}` - Get details of a specific invoice.

### RDLC Reports

- Invoices are generated using RDLC reports. The reports include customer details, product lists, total amounts, and can be exported in PDF or Excel formats.

### Frontend

- JavaScript AJAX is used for calling Web API endpoints.
- Basic Bootstrap UI is used for form design and displaying lists.

### Usage

1. **Create a Product:**
   - Go to the product creation page and add details like name, price, and stock.

2. **Create a Customer:**
   - Add a new customer with name and address details, ensuring all validations are met.

3. **Create Invoice:**
   - Select a customer and add products to create an invoice. The invoice is generated and displayed using RDLC.

4. **View Invoice List:**
   - Go to the invoice list page and filter invoices by customer or view all.




