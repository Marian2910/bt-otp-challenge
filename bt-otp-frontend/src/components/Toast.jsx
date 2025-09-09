import { useEffect, useState } from "react";
import Snackbar from "@mui/material/Snackbar";
import Alert from "@mui/material/Alert";

export default function Toast({ message, duration, onClose }) {
  const [timeLeft, setTimeLeft] = useState(duration);
  const [open, setOpen] = useState(true);

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
    if (reason === "clickaway") return;

    setOpen(false);
    if (onClose) onClose();
  };

  return (
    <Snackbar
      open={open}
      anchorOrigin={{ vertical: "top", horizontal: "right" }}
      onClose={handleClose}
    >
      <Alert
        severity="info"
        onClose={handleClose}
        sx={{ width: "100%" }}
      >
        {message} <br />
        Expires in {Math.floor(timeLeft / 60)}:
        {String(timeLeft % 60).padStart(2, "0")} minutes
      </Alert>
    </Snackbar>
  );
}
