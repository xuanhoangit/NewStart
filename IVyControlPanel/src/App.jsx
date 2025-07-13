import './App.css';
import { BrowserRouter as Router, Routes, Route,Navigate  } from "react-router-dom";
import LoginForm from './Login/LoginForm';
import ForgotPassword from './Login/ForgotPassword';
import PopupMessage from './components/commons/Popup/PopupMessage';
import Sidebar from './layout/Sidebar/Sidebar';
import Content from './layout/Content/Content'
import { useState,useEffect } from 'react';
import { v4 as uuidv4 } from 'uuid'; 
import ResetPassword from './Login/ResetPassword';
import { AuthProvider } from './AuthProvider';
import ProtectedRoute from './ProtectRoute';
// import Content from './layout/Content/Content';
const App=({showPopup})=>{

  return (
    <div className='app'>
        <Sidebar></Sidebar>
        <Content showPopup={showPopup}></Content>
    </div>
  )
}
function AppRouter() {
  const getToken=()=>{
    return localStorage.getItem("token")||sessionStorage.getItem("token")
  }
    const token = getToken();
  // console.log("aa",token)

    const [popups, setPopups] = useState([]);

  const showPopup = (message, type = 'success') => {
    const id = uuidv4(); // ID duy nhất cho mỗi popup
    console.log(id,message,type)
    setPopups(prev => [...prev, { id, message, type }]);
    // Tự động gỡ popup sau 3.5s (3s hiển thị + 0.5s fade)
    setTimeout(() => {
      setPopups(prev => prev.filter(p => p.id !== id));
    }, 3500);
  };

  return (<>
          <AuthProvider>
          <Routes>
            <Route path="/login" element={<LoginForm showPopup={showPopup}/>} />
            <Route path="/forgot-password" element={<ForgotPassword showPopup={showPopup}/>} />
            <Route path="/reset-password" element={<ResetPassword showPopup={showPopup}/>} />
            <Route path="/*" element={
              <ProtectedRoute>
                <App showPopup={showPopup}/>
              </ProtectedRoute>} />
                
          </Routes>
          </AuthProvider>
           {popups.map((popup) => (
          <PopupMessage
            key={popup.id}
            message={popup.message}
            type={popup.type}
            onClose={() => {
              setPopups((prev) => prev.filter((p) => p.id !== popup.id));
            }}
          />
        ))}</>
  )
}

export default AppRouter
