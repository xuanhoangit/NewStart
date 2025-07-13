import React from 'react';
import './FancyButton.css'; // CSS riêng

const FancyButton = ({ children, onClick, loading = false, disabled = false }) => {
  return (
    <button
      className={`fancy-button ${loading ? 'loading' : ''}`}
      onClick={onClick}
      disabled={disabled || loading}
    >
      {loading ? <span className="loader" /> : children}
    </button>
  );
};

export default FancyButton;
