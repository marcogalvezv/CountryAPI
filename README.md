# 🌍 Country API

A lightweight, modular API for managing country data using layered architecture and Entity Framework with SQL Server.

## 🧱 Architecture Overview

This project follows a **layered architecture**:

- **Host**: Entry point for HTTP requests (controllers, middleware, routing).
- **Manager**: Core business logic and orchestration between layers.
- **Access**: Data access layer using Entity Framework for SQL Server.

📬 How to Use the API
Once the API is running locally (http://localhost:5074), you can interact with it using tools like Swagger UI, Postman, or direct HTTP requests. Here's how:
🔎 Swagger UI (Recommended for Quick Exploration)
- Navigate to: http://localhost:5074/swagger
  
DB Setup: 
- Please create a new Database called, "CountryCityDB" and use windows authentication
- Run migrations, Update-Database -Context AppDbContext -Project CountryCityAPI.DataAccess

Example Call
It is required to use Authentication and Generate a Token, 
url: /api/Auth/login
body: {
  "username": "admin",
  "password": "admin"
} 

Response: Response contains a Token that need to added to the Authentication Header, using the following format: Bearer <TOKEN>

POST Call:
curl --location 'http://localhost:5074/api/Countries' \
--header 'accept: */*' \
--header 'Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYWRtaW4iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbiIsImV4cCI6MTc1MTU5NzQ2MCwiaXNzIjoiQ291bnRyeUNpdHlBUEkiLCJhdWQiOiJVc2VycyJ9.DaHLwbgulMapXhvA_HvPdpA6ig1wwxjRSzxjFqMBiJk' \
--header 'Content-Type: application/json' \
--data '{
    "name": "Spain",
    "cities": [
        {"name": "Madrid", "population": 9000000},
        {"name": "Barcelona", "population": 1500000}
    ]
}'

