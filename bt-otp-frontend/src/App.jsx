import OtpRequestForm from "./components/OtpRequestForm";
import OtpVerifyForm from "./components/OtpVerifyForm";

function App() {
  return (
    <div style={{ padding: "30px", fontFamily: "Arial" }}>
      <h1>Banking OTP System</h1>
      <OtpRequestForm />
      <hr style={{ margin: "20px 0" }} />
      <OtpVerifyForm />
    </div>
  );
}

export default App;
