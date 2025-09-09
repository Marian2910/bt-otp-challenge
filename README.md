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
