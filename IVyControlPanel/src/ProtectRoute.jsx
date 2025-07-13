// ProtectedRoute.jsx
import { Navigate } from "react-router-dom";
import { useAuth } from "./AuthProvider";
import "./ProtectRouter.css"
import { useState } from "react";
function ProtectedRoute({ children }) {
    const { user, loading } = useAuth();
  
    if (loading) {
        return  <div>
        <div style={{position:"absolute",top:"50%",left:"50%",transform:"translate(-50%,-50%)"}}>
            <div className="sun-homeloader" style={{width:"600px",position:"absolute"}}></div>
            <div className="sun-homeloader" style={{width:"500px",position:"absolute"}}></div>
            <div className="sun-homeloader" style={{width:"400px",position:"absolute"}}></div>
            <div className="homeloader" ></div>
        </div>
            <div className="sun-homeloader" style={{position:"absolute",left:"0",top:"20px"}}></div>
        </div>
    }

    if (!user) {
            return <Navigate to="/login" replace />;
    }
    console.log(user)
    return children;
}

export default ProtectedRoute;
