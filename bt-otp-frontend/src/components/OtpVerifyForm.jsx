import { useState } from "react";
import { verifyOtp } from "../api/otp";

export default function OtpVerifyForm() {
  const [userId, setUserId] = useState("");
  const [code, setCode] = useState("");
  const [message, setMessage] = useState(null);

  const handleVerify = async (e) => {
    e.preventDefault();
    setMessage(null);
    try {
      const res = await verifyOtp(userId, code);
      if (res.success) setMessage("OTP verified successfully!");
      else setMessage(res.errorMessage || "Verification failed.");
    } catch (err) {
      setMessage("Failed to verify OTP.");
    }
  };

  return (
    <div>
      <h2>Verify OTP</h2>
      <form onSubmit={handleVerify}>
        <input
          type="text"
          placeholder="User ID"
          value={userId}
          onChange={(e) => setUserId(e.target.value)}
          required
        />
        <input
          type="text"
          placeholder="OTP Code"
          value={code}
          onChange={(e) => setCode(e.target.value)}
          required
        />
        <button type="submit">Verify OTP</button>
      </form>
      {message && <p>{message}</p>}
    </div>
  );
}
