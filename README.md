# 🛍️ ECommerce Website API

A modern and modular Web API for managing products, categories, carts, orders, payments, and users. Built using **ASP.NET Core**, **Onion Architecture**, **JWT Authentication**, and **Stripe Integration** for payment processing.

Created with clean principles, scalability, and real-world e-commerce workflows in mind.

---

## 🚀 Features

✅ JWT Authentication & Role Management (`Admin`, `User`)  
✅ Clean Onion Architecture (`Domain`, `Application`, `Infrastructure`, `Persistence`, `API`)  
✅ Repository Pattern with Read/Write Separation  
✅ CRUD operations for **Products** and **Categories**  
✅ Cart System with Add, Remove, and Total Calculation  
✅ Order System with OrderItems, Status Update, and User Filtering  
✅ Stripe Integration for Secure Payments  
✅ FluentValidation for Input Validation  
✅ Global Response Wrapper with `Result<T>`  
✅ Swagger UI with Bearer Token Authorization  
✅ AutoMapper for DTO Mapping  
✅ Seed Data for Roles, Admin User, and Categories

---

## 📁 Project Structure

ECommerceWebsite/ │ ├── API/ → ASP.NET Core Web API (Controllers, Middleware, Swagger) ├── Application/ → DTOs, Interfaces, Validators, Services, Utilities ├── Domain/ → Entity Models and Enums ├── Infrastructure/ → External Services (Stripe, JWT) ├── Persistence/ → EF Core, DbContext, Migrations, Repositories └── README.md → Project Documentation

---

## ⚙️ Getting Started

### 🔧 1. Clone the Repository

git clone https://github.com/JaFidAn/ECommerceWebsite.git
cd API
🛠️ 2. Configure appsettings.json
Update the following in API/appsettings.json:
"ConnectionStrings": {
  "DefaultConnection": "Your SQL Server connection string"
},
"JwtSettings": {
  "Key": "YourSuperSecretKey",
  "Issuer": "ECommerceAuthAPI",
  "Audience": "ECommerceUser",
  "DurationInMinutes": 60
},
"Stripe": {
  "SecretKey": "YourStripeSecretKey",
  "PublishableKey": "YourStripePublishableKey"
}

🛋️ 3. Apply Migrations & Seed Data
dotnet ef database update --project Persistence --startup-project API
▶️ 4. Run the Application
dotnet run --project API

🌐 5. Open Swagger UI
Visit:
https://localhost:5001/swagger

🔐 Authentication & Roles
🔸 JWT Bearer Authentication
Token-based login and register
Auto-assign User role during registration

🔸 Default Admin Credentials
Email: r.alagezov@gmail.com
Password: R@sim1984

🔸 Auth Endpoints
POST /api/auth/register
POST /api/auth/login
POST /api/auth/logout
GET  /api/auth/profile

🔸 Use JWT in Swagger
Click the Authorize 🔓 button in Swagger UI
Paste your token: Bearer eyJhbGci...
Test protected endpoints like Orders or Products

📦 Products & Categories
Full CRUD for both modules
Products contain: name, description, price, category, imageUrl, stock

Pagination support with ?PageNumber=1&PageSize=10
GET    /api/products
POST   /api/products
PUT    /api/products
DELETE /api/products/{id}

🛒 Cart System
Add/Remove items to/from cart
One cart per user
Auto-create cart if not exists

Endpoints:
POST   /api/cart      → Add product to cart
GET    /api/cart      → Get user's cart
DELETE /api/cart/{id} → Remove item from cart
GET    /api/cart/total → Calculate total price

📦 Orders
Cart → Order transformation
OrderItems created from CartItems
Cart is cleared after successful order
Admin can update order status

Endpoints:
POST   /api/orders         → Create new order
GET    /api/orders         → Admin: Get all orders
GET    /api/orders/user    → Get orders for logged-in user
PUT    /api/orders/status  → Admin: Update order status

💳 Stripe Payment Integration
Secure payments using Stripe test tokens
Stripe SDK integrated into backend
Supports real cards in live mode
Endpoint:
POST /api/payments
🔸 Example Input:
{
  "orderId": "a1b2c3d4-5678",
  "paymentMethod": "Credit Card",
  "token": "tok_visa"
}
Use Stripe test tokens: tok_visa, tok_mastercard, etc.
🧪 Testing Tips
Use Postman or Swagger for all endpoints
Use Bearer <your_token> for protected routes
Use tok_visa for successful payment simulation

📜 License
This project is open-source and free to use for educational or commercial purposes.
Licensed under the MIT License.

👨‍💻 Author
Created with ❤️ by Rasim Alagezov

📧 Email: r.alagezov@gmail.com
💻 GitHub: JaFidAn
🔗 LinkedIn: rasim-alagezov

✨ Have questions or want to collaborate? Feel free to reach out!
