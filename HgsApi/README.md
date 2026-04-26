# 🏨 Hostel Grievance System (HGS)

A full-stack Hostel Grievance Management System that collects, manages, and displays all hostel-related complaints in one place with full CRUD operations and role-based access control.

---

## 📋 Table of Contents

- [Overview](#overview)
- [Tech Stack](#tech-stack)
- [Architecture](#architecture)
- [Features](#features)
- [Database Schema](#database-schema)
- [API Endpoints](#api-endpoints)
- [Pages & Routes](#pages--routes)
- [Deployment](#deployment)
- [Getting Started](#getting-started)
- [Environment Variables](#environment-variables)
- [Screenshots](#screenshots)

---

## 🔍 Overview

The **Hostel Grievance System (HGS)** is a web-based application designed for hostel residents to submit and track their complaints/grievances. The system supports **role-based access** where:

- **Admin** — Can view, edit, and delete **all grievances** submitted by every user.
- **User** — Can only view, create, edit, and delete **their own grievances**.

---

## 🛠️ Tech Stack

### Frontend
| Technology       | Version     | Description                     |
|------------------|-------------|---------------------------------|
| React            | 18.x        | UI Library                      |
| Vite             | 5.x         | Build Tool & Dev Server         |
| React Router DOM | 6.x         | Client-side Routing             |
| Axios            | 1.x         | HTTP Client for API calls       |
| CSS/Tailwind     | -           | Styling                         |

### Backend
| Technology                        | Version   | Description                        |
|-----------------------------------|-----------|------------------------------------|
| .NET                              | 10.0      | Backend Framework                  |
| ASP.NET Core Web API              | 10.0      | RESTful API Framework              |
| Entity Framework Core             | 10.0.7    | ORM (Object-Relational Mapping)    |
| Npgsql.EntityFrameworkCore        | 10.0.1    | PostgreSQL Provider for EF Core    |
| Swashbuckle (Swagger UI)          | 10.1.7    | API Documentation                  |
| Microsoft.AspNetCore.OpenApi      | 10.0.6    | OpenAPI Support                    |

### Database
| Technology | Description                              |
|------------|------------------------------------------|
| Neon DB    | Serverless PostgreSQL Database (Cloud)   |

### Deployment
| Component  | Platform         | URL                              |
|------------|------------------|----------------------------------|
| Frontend   | Vercel           | `https://your-app.vercel.app`    |
| Backend    | Azure App Service| `https://your-api.azurewebsites.net` |
| Database   | Neon             | Serverless PostgreSQL (Cloud)    |

---

## 🏗️ Architecture

---

## ✨ Features

- ✅ **User Registration & Login** — Signup/Login with email & password
- ✅ **Role-Based Access Control** — Admin sees all data, Users see only their own
- ✅ **Create Grievance** — Submit a new hostel complaint
- ✅ **View Grievances** — List all grievances (filtered by role)
- ✅ **Edit Grievance** — Update an existing complaint
- ✅ **Delete Grievance** — Remove a complaint
- ✅ **RESTful API** — Clean API with Swagger documentation
- ✅ **CORS Enabled** — Cross-origin requests supported for frontend-backend communication
- ✅ **Serverless Database** — Neon DB for zero-config PostgreSQL

---

## 🗄️ Database Schema

### `Users` Table
| Column     | Type      | Description              |
|------------|-----------|--------------------------|
| `Id`       | `int`     | Primary Key (Auto-increment) |
| `Username` | `string`  | User's display name      |
| `Email`    | `string`  | User's email (unique)    |
| `Password` | `string`  | User's password          |

### `HgsInfos` Table (Grievances)
| Column          | Type      | Description                     |
|-----------------|-----------|---------------------------------|
| `Id`            | `int`     | Primary Key (Auto-increment)    |
| `Name`          | `string`  | Name of the complainant         |
| `Grievancetypes`| `string`  | Type/category of grievance      |
| `Room`          | `string`  | Room number                     |
| `Course`        | `string`  | Course of the student           |
| `Mobile`        | `string`  | Contact number                  |
| `Description`   | `string`  | Detailed description of issue   |
| `UserEmail`     | `string`  | Email of the user who submitted |

---

## 🔌 API Endpoints

### 👤 User Controller — `/api/User`

| Method   | Endpoint            | Description            | Access  |
|----------|---------------------|------------------------|---------|
| `GET`    | `/api/User`         | Get all users          | Admin   |
| `GET`    | `/api/User/{id}`    | Get user by ID         | Admin   |
| `POST`   | `/api/User`         | Register new user      | Public  |
| `PUT`    | `/api/User/{id}`    | Update user            | Auth    |
| `DELETE` | `/api/User/{id}`    | Delete user            | Admin   |

### 📋 Grievance Controller — `/api/HgsInfo`

| Method   | Endpoint                    | Description                        | Access  |
|----------|-----------------------------|------------------------------------|---------|
| `GET`    | `/api/HgsInfo`              | Get all grievances                 | Admin   |
| `GET`    | `/api/HgsInfo/{id}`         | Get grievance by ID                | Auth    |
| `GET`    | `/api/HgsInfo/user/{email}` | Get grievances by user email       | User    |
| `POST`   | `/api/HgsInfo`              | Create new grievance               | User    |
| `PUT`    | `/api/HgsInfo/{id}`         | Update grievance                   | Auth    |
| `DELETE` | `/api/HgsInfo/{id}`         | Delete grievance                   | Auth    |

### Sample Request Body — Create Grievance
````````

# Response

````````markdown
{

  "id": 1,
  "name": "John Doe",
  "grievancetypes": "Noise",
  "room": "101",
  "course": "B.Sc. Computer Science",
  "mobile": "1234567890",
  "description": "The noise from the next room is disturbing my studies.",
  "userEmail": "johndoe@example.com"

}

````````

## 📄 Pages & Routes (Frontend)

| Page               | Route              | Description                          | Access  |
|--------------------|--------------------|--------------------------------------|---------|
| **Login**          | `/login`           | User login page                      | Public  |
| **Signup**         | `/signup`          | New user registration                | Public  |
| **Grievance List** | `/grievances`      | View all grievances (role-based)     | Auth    |
| **Create Grievance** | `/create`       | Submit a new grievance               | User    |
| **Edit Grievance** | `/edit/:id`        | Edit an existing grievance           | Auth    |
| **Logout**         | —                  | Clears session and redirects to login| Auth    |

---

## 🚀 Deployment

### Frontend (Vercel)
1. Push React + Vite code to GitHub
2. Connect repository to [Vercel](https://vercel.com)
3. Set environment variable: `VITE_API_URL=https://your-api.azurewebsites.net`
4. Deploy — auto-deploys on every push ✅

### Backend (Azure App Service — Free Tier)
1. Publish .NET API to Azure App Service
2. Configure App Service settings:
   - `PORT` — `5000`
   - `ASPNETCORE_ENVIRONMENT` — `Production`
   - Connection string for Neon DB
3. Deploy — updates on every push to the main branch ✅

### Database (Neon)
1. Create a free project at [neon.tech](https://neon.tech)
2. Copy the connection string
3. Add to `appsettings.json` or Azure App Settings

---

## ⚡ Getting Started (Local Development)

### Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Node.js 18+](https://nodejs.org)
- [Neon DB Account](https://neon.tech)

### Database Setup (Local Development)
1. Create a new PostgreSQL database
2. Update connection string in `server/appsettings.Development.json`
3. Run migrations: