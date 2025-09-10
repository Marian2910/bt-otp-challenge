# BT Code Crafter Challenge - Backend Tests

## Overview

This project includes **unit tests** for the backend OTP API. The tests are implemented using **xUnit** and **Moq** to ensure that both the service layer and controller logic function correctly.

The tests aim to cover **OTP generation, validation, and error handling**, achieving high reliability and maintainability.

---

## Test Project Structure

```
BtOtp.Tests/
├── OtpControllerTests.cs       # Tests for OtpController endpoints
├── OtpServiceTests.cs          # Tests for OtpService business logic
├── SecureOtpGeneratorTests.cs  # Tests for SecureOtpGenerator random OTP generation
├── BtOtp.Tests.csproj          # Test project file
└── bin/ / obj/                 # Compiled test outputs
```

---

## Running the Tests

### Using the Command Line

Navigate to the test project folder:

```bash
cd BtOtp.Tests
```

Restore dependencies (if not done already):

```bash
dotnet restore
```

Run all tests:

```bash
dotnet test
```

Expected output will show the number of tests passed and failed. All tests should pass if the API is functioning correctly.

---

### Test Coverage

The tests cover the following areas:

#### 1. `OtpControllerTests.cs`

* **Request OTP**

    * Returns `Ok` with OTP and expiry when `userId` is valid.
    * Returns `BadRequest` when `userId` is missing or empty.
    * Handles `null` request body properly.

* **Verify OTP**

    * Returns `Ok` when OTP is valid.
    * Returns `BadRequest` for missing `userId` or `code`.
    * Returns `NotFound` if OTP does not exist.
    * Returns `BadRequest` if OTP is expired or invalid.

#### 2. `OtpServiceTests.cs`

* **IssueOtp**

    * Returns a 6-digit OTP with correct expiry.
    * Throws `ArgumentException` for invalid `userId`.

* **ValidateOtp**

    * Returns `true` when OTP is correct.
    * Throws `Exception` if OTP is invalid.
    * Throws `Exception` if OTP is expired.
    * Throws `KeyNotFoundException` if OTP does not exist or has already been validated.

#### 3. `SecureOtpGeneratorTests.cs`

* Generates a 6-digit OTP by default.
* Supports custom-length OTP generation.
* Produces unique OTPs for successive calls.

#### 4. `ErrorHandlingMiddlewareTests.cs`

* **Successful request**

  * Passes through the middleware without exceptions.
  * Returns 200 OK and response body when no errors occur.
  * Verifies that no logs are written.

* **Generic exceptions**

   * Returns 500 Internal Server Error.
   * Response body includes the exception message.
   * Logs the error with LogLevel.Error.

* **Argument exceptions**

  * Returns 400 Bad Request.
  * Response body contains the exception message.
  
* **KeyNotFound exceptions**
  
   * Returns 404 Not Found.
   * Response body contains the exception message.

---

## Notes

* Tests use **Moq** to mock the `IOtpService` in controller tests, ensuring isolated unit testing.
* OTP expiry times can be configured when instantiating `OtpService` in tests, which allows testing edge cases like expiration.
* The test suite aims for **>70% coverage**, meeting the challenge requirement.
* The tests are independent and can be run repeatedly without affecting the API or database state.

---

