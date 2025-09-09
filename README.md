
````markdown
# 🔐 Secure OTP Systems

A **secure one-time password (OTP) system** built with an **ASP.NET Core backend** and a **React (Vite) frontend**, complete with **unit tests** and **code coverage**.

---

## 📦 Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js 18+](https://nodejs.org/en/)
- [Git](https://git-scm.com/)

---

## 📥 Clone & Setup

```bash
git clone <repo-url>
cd <repo-folder>
```
````

---

## 📂 Project Structure

| Folder/File          | Description           |
| -------------------- | --------------------- |
| `BtOtp.Api`          | ASP.NET Core backend  |
| `bt-otp-frontend`    | React frontend (Vite) |
| `BtOtp.Tests`        | Unit tests            |
| `.idea`              | IDE config (optional) |
| `BtOtpChallenge.sln` | Solution file         |

---

## ⚙️ Development

### 🔹 Backend

```bash
cd BtOtp.Api
dotnet run
```

Runs on ➡️ [https://localhost:5xxx](https://localhost:5xxx)

---

### 🔹 Frontend

In a separate terminal:

```bash
cd bt-otp-frontend
npm install
npm run dev
```

Runs on ➡️ [http://localhost:5173](http://localhost:5173)

> The frontend proxies API calls to the backend.

---

## 🚀 Production Build

### Frontend

```bash
cd bt-otp-frontend
npm run build
```

Build output → `BtOtp.Api/wwwroot`

### Backend

```bash
cd BtOtp.Api
dotnet publish
dotnet BtOtp.Api.dll
```

---

## 🧪 Testing

```bash
cd BtOtp.Tests
dotnet test
```

✔️ Includes coverage via **Coverlet**

---

## 🎯 Features

- ✅ ASP.NET Core Web API backend
- ✅ React + Vite frontend
- ✅ Secure OTP generation & validation
- ✅ Unit tests with coverage
- ✅ Production-ready build pipeline

---
=======
# Technical Challenge

## The problem

Develop a system that can generate a one-time password for a banking application. The OTP system must be secure, efficient, and user-friendly to enhance the user experience and protect customers' data.

## Business Requirements

1. The OTP system must be secure to protect the confidential data of the customers. It must ensure that OTPs are generated randomly and are not predictable. Encryption during transmission of the OTPs should also be ensured.
2. The OTPs should be time-bound. Once generated, an OTP should not be valid indefinitely. The system should automatically invalidate the OTP after a certain period of time that can be easily customized.
3. The OTP input interface should be user-friendly. It should allow users to input the OTP easily without any confusion.
4. The system should have good error handling. It should inform the user about any issues in a clear and understandable way.
5. For the purpose of this exercise, the user should receive the OTP in a toast message that will be visible as long as the OTP is valid.

## Techincal requirements

1. The solution must be a web application developed on the latest .NET framework.
2. For the frontend part you can use any javascript framework you want.
3. Unit tests must be performed with a test coverage of at least 70%.

## Evaluation
Your solution will be evaluated based on coding and testability standards, naming conventions, project structure and the meeting of requirements. It must be uploaded in a public github repository that can be accessed by us for validation.


