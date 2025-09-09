import { useEffect, useState } from "react";

export default function Toast({ message, duration, onClose }) {
  const [visible, setVisible] = useState(true);

  useEffect(() => {
    const timer = setTimeout(() => {
      setVisible(false);
      if (onClose) onClose();
    }, duration * 1000);

    return () => clearTimeout(timer);
  }, [duration, onClose]);

  if (!visible) return null;

  return (
    <div style={{
      position: "fixed",
      top: "20px",
      right: "20px",
      backgroundColor: "#333",
      color: "#fff",
      padding: "15px",
      borderRadius: "5px",
      zIndex: 9999,
    }}>
      {message}
    </div>
  );
}
