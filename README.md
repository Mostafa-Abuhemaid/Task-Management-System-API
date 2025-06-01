# Task Management System-API


[.Net]: https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white
[C#]: https://custom-icon-badges.demolab.com/badge/C%23-%23239120.svg?logo=csharp&style=for-the-badge&logoColor=white
[SQLServer]: https://img.shields.io/badge/Microsoft%20SQL%20Server-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white
[Redis]: https://img.shields.io/badge/Redis-DC382D?style=for-the-badge&logo=redis&logoColor=white
[SignalR]: https://img.shields.io/badge/SignalR-FF6F00?style=for-the-badge&logo=signalr&logoColor=white
[Hangfire]: https://img.shields.io/badge/Hangfire-5E0F59?style=for-the-badge&logo=hangfire&logoColor=white

![.Net] ![C#] ![SQLServer] ![Redis] ![SignalR] ![Hangfire]

<p align="center">
  <b>The Task Management System is a web application that allows users to create, manage, and track
tasks efficiently. It enables users to categorize tasks, assign due dates, update task statuses, and
receive real-time notifications</b>
</p>


## 🌟 Features

- **Secure Authentication** with JWT & Refresh Tokens
- **Real-Time notifications** using SignalR
- **Task Management** Tasks can be categorized (e.g., "Work", "Personal", etc.) for easy management
- **In-memory Caching** for high-performance operations
- **Auto-Mapping** with Mapster
- **Background Job** with Hangfire
## 🛠 Tech Stack

- **Framework**: .NET 8
- **Database**: SQL Server + Entity Framework Core
- **Real-Time**: SignalR
- **Caching**: In-memory Cache
- **Mapping**: Mapster
- **Background Job** Hangfire
- **Architecture**: Clean Architecture

## 🧩 Design Patterns

- **Clean Architecture** - Separation of concerns
- **Repository Pattern** - Abstracted data access



## 📚 API Documentation

Explore endpoints interactively via Swagger UI:
```
https://doclink.runasp.net/swagger/index.html
```


Features:
- allows users to create, manage, and track tasks efficiently
- Real Time notifications
- enables users to categorize tasks



## 📡 API Endpoints

### Authentication

| Method | Endpoint                       | Description                   |
|--------|--------------------------------|-------------------------------|
| POST   | `/api/Account/Register`        | User registration             |
| POST   | `/api/Account/Login`           | JWT authentication            |
| POST   | `/api/Account/forget-password` | Forgot password               |
| PUT    | `/api/Account/Reset-Password`  | Reset password                |
| PUT    | `/api/Account/Change-Password` | Change Password               |
| POST   | `/api/Account/VerifyOTP`       | VerifyOTP code                |
| POST   | `/api/Account/SignIn-Google`   | Sign in with Google           |
| POST   | `/api/Account/SignIn-Facebook` | Sign in with Facebook         |


### Categories
| Method | Endpoint                        | Description               |
|--------|---------------------------------|---------------------------|
| POST   | `/api/Category`                 | Create new Category       |
| PUT    | `/api/Category/Id`              | Update Category           |
| DELETE | `/api/Category/Id`              | Delete Category           |
| GET    | `/api/Category/Id`              | Get Category details      |
| GET    | `/api/Category`                 | Get all Categories        |

### Taskes
| Method | Endpoint                        | Description               |
|--------|---------------------------------|---------------------------|
| POST   | `/api/Task`                     | Create new Task           |
| PUT    | `/api/Task/Id`                  | Update Task               |
| DELETE | `/api/Task/Id`                  | Delete Task               |
| GET    | `/api/Task/Id`                  | Get Task details          |
| GET    | `/api/Task`                     | Get all Taskes            |

### Users
| Method | Endpoint                        | Description               |
|--------|---------------------------------|---------------------------|
| POST   | `/api/User/lock`                |Lock user by its email     |
| POST   | `/api/User/Unlock`              |UnLock user by its email   |
| PUT    | `/api/User/EditUser`            | EditUser Details          |
| DELETE | `/api/User/delete`              | Delete  user by its email |
| GET    | `/api/User/userId`              | Get User details          |
| GET    | `/api/User`                     | Get all Users             |
