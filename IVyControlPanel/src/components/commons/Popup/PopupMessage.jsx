import React, { useEffect, useState } from "react";
import "./PopupMessage.css"; // Đảm bảo bạn có file CSS hoặc dùng inline style

const PopupMessage = ({ message, type, onClose }) => {
  const [isVisible, setIsVisible] = useState(true);

  useEffect(() => {
    // Ẩn sau 3 giây
    const timeout = setTimeout(() => {
      setIsVisible(false); // bắt đầu mờ dần
    }, 3000);

    // Xóa hoàn toàn sau 3.5s
    const removeTimeout = setTimeout(() => {
      onClose();
    }, 3500);

    return () => {
      clearTimeout(timeout);
      clearTimeout(removeTimeout);
    };
  }, [onClose]);

  return (
    <div className={`popup-message ${type} ${isVisible ? "show" : "fade-out"}`}>
       {message}{type==="success"?<span>🥳🥳</span>:<span>😖</span>}
    </div>
  );
};

export default PopupMessage;
