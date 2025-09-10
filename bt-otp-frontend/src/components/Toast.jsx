import { useEffect, useState } from "react";
import Snackbar from "@mui/material/Snackbar";
import Alert from "@mui/material/Alert";
import IconButton from "@mui/material/IconButton";
import ContentCopyIcon from "@mui/icons-material/ContentCopy";

export default function Toast({ message, duration, onClose }) {
  const [timeLeft, setTimeLeft] = useState(duration);
  const [open, setOpen] = useState(true);
  const [feedback, setFeedback] = useState(null);

  useEffect(() => {
    if (timeLeft <= 0) {
      handleClose();
      return;
    }

    const timer = setInterval(() => {
      setTimeLeft((prev) => prev - 1);
    }, 1000);

    return () => clearInterval(timer);
  }, [timeLeft]);

  const handleClose = (event, reason) => {
    setOpen(false);
    if (onClose) onClose();
  };

  const handleFeedbackClose = () => setFeedback(null);

  const handleCopy = async () => {
    // Extract the 6-digit OTP from the message
    const otpMatch = message.match(/\d{6}/);
    const otp = otpMatch ? otpMatch[0] : null;

    if (!otp) {
      setFeedback({ severity: "error", text: "No OTP found to copy!" });
      return;
    }

    try {
      await navigator.clipboard.writeText(otp);
      setFeedback({ severity: "success", text: "OTP copied to clipboard!" });
      console.log("Copied OTP:", otp);
    } catch (err) {
      setFeedback({ severity: "error", text: "Failed to copy OTP." });
      console.error("Failed to copy OTP:", err);
    }
  };

  return (
    <>
      {/* Main OTP toast */}
      <Snackbar
        open={open}
        anchorOrigin={{ vertical: "top", horizontal: "right" }}
        onClose={handleClose}
      >
        <Alert
          severity="info"
          onClose={handleClose}
          sx={{ width: "100%", display: "flex", alignItems: "center" }}
          action={
            <IconButton
              aria-label="copy otp"
              color="inherit"
              size="small"
              onClick={handleCopy}
            >
              <ContentCopyIcon fontSize="small" />
            </IconButton>
          }
        >
          {message} <br />
          Expires in {Math.floor(timeLeft / 60)}:
          {String(timeLeft % 60).padStart(2, "0")} minutes
        </Alert>
      </Snackbar>

      {/* Feedback toast */}
      <Snackbar
        open={!!feedback}
        autoHideDuration={2000}
        onClose={handleFeedbackClose}
        anchorOrigin={{ vertical: "bottom", horizontal: "center" }}
      >
        {feedback && (
          <Alert
            onClose={handleFeedbackClose}
            severity={feedback.severity}
            sx={{ width: "100%" }}
          >
            {feedback.text}
          </Alert>
        )}
      </Snackbar>
    </>
  );
}
