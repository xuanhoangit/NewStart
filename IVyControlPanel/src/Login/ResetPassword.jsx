import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Link, useNavigate } from 'react-router-dom';
import "./ResetPassword.css"
import { hostNameHttp } from '../components/commons/HostName';
import post from '../components/PostData';
const ResetPassword = ({showPopup}) => {
  const [email, setEmail] = useState('');
  const [token, setToken] = useState('');
  const [newPassword, setNewPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [showPassword, setShowPassword] = useState(false);
  const [showNewPassword, setShowNewPassword] = useState(false);
  const navigate=useNavigate()
  // Lấy email và token từ query URL
  useEffect(() => {
    const params = new URLSearchParams(window.location.search);
    setEmail(params.get('email') || '');
    setToken(params.get('token') || '');
  }, []);

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (newPassword !== confirmPassword) {
      showPopup("Mật khẩu mới không khớp","fail")
      return;
    }


      const response = await post(`${hostNameHttp}/api/account/reset-password`, {
        email,
        token,
        newPassword
      });
      console.log(response)
      if(response.status==200){
        showPopup("Đặt lại mật khẩu thành công!","success");
        for (let index = 0; index <= 5; index++) {
            await new Promise(resolve => setTimeout(resolve, 1000));
            showPopup(`Chuyển đến trang đăng nhập sau ${(5-index)} giây.`,"success");
        }
        setTimeout(()=>{
          navigate("/login")
        },6000)
      }
      else{
        showPopup("Đặt lại mật khẩu thất bại!","fail");
      }
  };

  return (
    <div className='reset-pw-container'>
     
      <form onSubmit={handleSubmit} id='reset-pw'>
      <h2>Đặt lại mật khẩu</h2>
       <div style={{position:"relative",color:"black"}}
        > <input
            type={showPassword?"text":"password"}
            placeholder="Mật khẩu mới"
            value={newPassword}
            onChange={(e) => setNewPassword(e.target.value)}
            required
            />
            <span
            onClick={() => setShowPassword(!showPassword)}
            style={{
            position: 'absolute',
            top: '50%',
            zIndex:"2",
            right: '8px',
            transform: 'translateY(-50%)',
            cursor: 'pointer',
            fontSize: '14px',
            userSelect: 'none'
            }}
        >
            {showPassword ? <i className="bi bi-eye-slash"></i> : <i className="bi bi-eye"></i>}
        </span>
      </div>
        <div style={{position:"relative",color:"black"}}>
            <input style={{height:"40px"}}
            type={!showNewPassword?'password':"text"}
            placeholder="Xác nhận mật khẩu"
            value={confirmPassword}
            onChange={(e) => setConfirmPassword(e.target.value)}
            required
            />
            <span
                onClick={() => setShowNewPassword(!showNewPassword)}
                style={{
                position: 'absolute',
                top: '50%',
                zIndex:"2",
                right: '8px',
                transform: 'translateY(-50%)',
                cursor: 'pointer',
                fontSize: '14px',
                userSelect: 'none'
                }}
            >
            {showNewPassword ? <i className="bi bi-eye-slash"></i> : <i className="bi bi-eye"></i>}
        </span>
        </div>
        <button type="submit">Xác nhận</button>
        <Link  style={{float:"right",margin:"5px 5px 0 0"}} to="/login" className="link">Đăng nhập</Link>
        
      </form>
    </div>
  );
};

export default ResetPassword;
