# TimeTrackingApp & NotificationApp

This repository contains two microservices: **TimeTrackingApp** and **NotificationApp**, which interact via **RabbitMQ** for message exchange.

## 📌 Description

- **TimeTrackingApp** — a microservice for tracking employee activities. It allows adding, editing, and deleting activities, as well as retrieving data via a RESTful API.
- **NotificationApp** — a microservice that receives messages from **TimeTrackingApp** via **RabbitMQ**, processes them, and generates appropriate notifications (logging or email sending).

## 🏗 Architecture

### TimeTrackingApp:
- **.NET 6+**
- **Entity Framework Core / Dapper**
- **RabbitMQ** for message exchange
- **RESTful API** with **Swagger/OpenAPI**
- **ILogger** for logging
- **Input data validation**

### NotificationApp:
- **.NET 6+**
- **RabbitMQ** for receiving messages
- **Service for generating notifications** (logging or email)

## 📂 Project Structure

```plaintext
TimeTrackingApp/
├── TimeTrackingApp.sln
├── TimeTrackingApp.API/
│   ├── Controllers/
│   ├── Models/
│   ├── Services/
│   └── Startup.cs
├── TimeTrackingApp.Domain/
│   ├── Entities/
│   ├── Interfaces/
│   └── Services/
├── TimeTrackingApp.Infrastructure/
│   ├── Repositories/
│   ├── RabbitMQ/
│   └── Configuration/
└── README.md

NotificationApp/
├── NotificationApp.sln
├── NotificationApp.Worker/
│   ├── appsettings.json
│   ├── Worker.cs
│   ├── Services/
│   │   └── NotificationService.cs
│   ├── Models/
│   │   └── ActivityNotification.cs
│   └── Logs/
│       └── LogService.cs
└── README.md
```

## 🚀 Running the Projects

### 1️⃣ Start RabbitMQ using Docker
```sh
docker run -d --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:management
```

### 2️⃣ Start TimeTrackingApp
```sh
cd TimeTrackingApp.API
dotnet run
```
> The API will be available at: `http://localhost:5000`

### 3️⃣ Start NotificationApp
```sh
cd NotificationApp.Worker
dotnet run
```
> **NotificationApp** will connect to **RabbitMQ** and begin processing messages.

## 🔄 API Usage

### ▶️ Add an Activity in TimeTrackingApp
```http
POST /api/activities
```
#### 🔹 Example Request:
```json
{
  "id": "guid",
  "date": "2025-03-23T12:00:00",
  "hours": 8,
  "employeeId": "guid",
  "activityTypeId": "guid"
}
```

### 📨 Processing Messages in NotificationApp
Once an activity is added, **TimeTrackingApp** sends a message to **RabbitMQ**, which **NotificationApp** then receives and processes. Notifications can be viewed in the console or logs.

## 🛠 Technologies & Tools

### **TimeTrackingApp:**
- **.NET 8**
- **RabbitMQ**
- **Swagger**
- **Entity Framework Core**
- ** SQL Server**
- **ILogger** for logging

### **NotificationApp:**
- **.NET 8**
- **RabbitMQ**
- **ILogger** for logging notifications
- **Service for generating notifications** (email or console log)

## ❗ Potential Issues
- **RabbitMQ startup errors**: Ensure that ports **5672** and **15672** are not occupied by other applications.
- **Connection issues with RabbitMQ**: Verify the **appsettings.json** configuration in **NotificationApp**.
