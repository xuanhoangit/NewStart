import React, { useState, useEffect } from "react";
import "./Loading.css"
const LoadingProgressBar = () => {
  const [percent, setPercent] = useState(1);

  useEffect(() => {
    const interval = setInterval(() => {
      setPercent(prev => {
        if (prev >= 100) {
          clearInterval(interval);
          return 100;
        }
        return prev + 1;
      });
    }, 10); // cập nhật mỗi 50ms

    return () => clearInterval(interval);
  }, []);

  return (
    // <div style={styles.container}>
    //   <div style={styles.text}>
    //     {percent < 100 ? `Loading... ${percent}%` : "100%"}
    //   </div>
    //   <div style={styles.bar}>
    //     <div style={{ ...styles.fill, width: `${percent}%` }} />
    //   </div>
    // </div>
    <div class="uploadloader" style={{position:"absolute",top:"50%",transform:"translateY(50%)"}}></div>
  );
};

const styles = {
  container: {
    display: "flex",
    flexDirection: "column",
    alignItems: "center",
    height: "100vh",
    justifyContent: "center",
    // backgroundColor: "#222",
    color: "#fff",
    fontFamily: "Arial, sans-serif"
  },
  text: {
    fontSize: "20px",
    marginBottom: "10px"
  },
  bar: {
    width: "300px",
    height: "25px",
    // backgroundColor: "#444",
    border: "2px solid #0f0",
    borderRadius: "8px",
    overflow: "hidden"
  },
  fill: {
    height: "100%",
    backgroundColor: "#0f0",
    transition: "width 0.1s ease-in-out"
  }
};

export default LoadingProgressBar;
