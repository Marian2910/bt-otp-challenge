# BT Code Crafter Challenge - Frontend

## Overview

This project is the **frontend** for the BT Code Crafter OTP challenge. It is a **React + Vite** application that allows users to:

* Request a one-time password (OTP) for a given user ID.
* Verify the OTP within a time-bound period.
* Display OTPs and error messages via **toast notifications**.

The application is designed to be **user-friendly, secure, and responsive**, interacting with the backend OTP API over HTTPS.

---

## Technologies

* **React 19**: UI library for building interactive components.
* **Vite**: Fast development server and build tool.
* **MUI (Material UI)**: Component library for styling and UI consistency.
* **Jest**: Unit testing framework.
* **@testing-library/react**: Utilities for testing React components.
* **@testing-library/jest-dom**: Custom jest matchers for DOM elements.
* **React Hot Toast**: For OTP notifications.

---

## Project Structure

```
bt-otp-frontend/
├── src/
│   ├── __tests__/             # Frontend unit tests
│   │   ├── OtpRequestForm.test.jsx
│   │   ├── OtpVerifyForm.test.jsx
│   │   └── Toast.test.jsx
│   ├── api/                   # Functions for API calls
│   │   └── otp.js
│   ├── components/            # React components
│   │   ├── OtpRequestForm.jsx
│   │   ├── OtpVerifyForm.jsx
│   │   └── Toast.jsx
│   ├── assets/                # Images, fonts, icons
│   ├── App.jsx                # Main app component
│   ├── main.jsx               # React entry point
│   └── index.css / App.css     # Styling
├── package.json
├── babel.config.js
├── jest.config.js
├── vite.config.js
└── README.md
```

---

## Getting Started

### Prerequisites

* Node.js 20+
* npm 9+

### Installation

Clone the repository and install dependencies:

```bash
cd bt-otp-frontend
npm install
```

### Development Server with HTTPS

Start the Vite development server:

```bash
npm run dev
```

Visit [https://localhost:5173](https://localhost:5173) to view the app in your browser.

#### Vite HTTPS Setup

The app runs over **HTTPS** using self-signed certificates located in `certs/`:

* `localhost+1-key.pem` → Private key
* `localhost+1.pem` → Public certificate

The Vite config (`vite.config.js`) includes a **proxy** to the backend:

```js
import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import fs from 'fs'

export default defineConfig({
  plugins: [react()],
  server: {
    https: {
      key: fs.readFileSync('./certs/localhost+1-key.pem'),
      cert: fs.readFileSync('./certs/localhost+1.pem'),
    },
    proxy: {
      '/api': {
        target: 'https://localhost:7124', // backend URL
        changeOrigin: true,
        secure: false, // disables SSL validation for local dev
      },
    },
  },
});
```

**Why `secure: false`?**

* The backend uses a **self-signed certificate**.
* Browsers and dev servers normally reject self-signed certificates as untrusted.
* `secure: false` allows Vite to proxy API requests to the backend without SSL validation errors.
* **Never use `secure: false` in production.**

---

### Build for Production

```bash
npm run build
```

Preview the production build:

```bash
npm run preview
```

---

## Application Features

### 1. Request OTP

* User enters a **User ID**.
* Clicks **Request OTP** button.
* OTP is displayed via a **toast notification**.
* OTP expires after a configurable time (default: 120 seconds).

### 2. Verify OTP

* User enters **User ID** and **OTP code**.
* Clicks **Verify OTP** button.
* Success or error messages are shown via **toast notifications**.

### 3. Toast Notifications

* Display OTP messages and expiry countdown.
* Display success or error messages for verification and copying actions.

---

## Frontend Unit Tests

### Running Tests

```bash
npm test
```

This runs **Jest** in watch mode with **jsdom** environment.

### Test Coverage

#### 1. `OtpRequestForm.test.jsx`

* Renders **input and button**.
* Successfully requests OTP and displays toast on success.
* Shows error messages if API fails.

#### 2. `OtpVerifyForm.test.jsx`

* Renders **User ID input** and **OTP fields**.
* Calls backend API and shows success toast on valid OTP.
* Shows error toast for invalid or expired OTP.

#### 3. `Toast.test.jsx`

* Renders **toast message and countdown timer**.
* Handles **copy OTP action** and displays success/error messages.
* Correctly handles errors if OTP is missing.

---

## Notes

* React **automatic JSX runtime** is enabled via Babel, so React import is optional but added for compatibility with Jest.
* All tests are independent and repeatable.
* The frontend communicates with the backend API at: `https://localhost:7241/api/` (configurable in the API utility files).
* HTTPS is enabled locally using self-signed certificates for secure communication during development.

---
