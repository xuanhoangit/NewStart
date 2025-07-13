import React, { useEffect, useState } from 'react';
import './LoginForm.css';
import { Link, useNavigate  } from 'react-router-dom';
import post from '../components/PostData';
import { hostNameHttp } from '../components/commons/HostName';
import ReCAPTCHA from 'react-google-recaptcha';
import { useAuth } from '../AuthProvider';
import api from '../AxiosInstance';
import axios from 'axios';

const LoginForm = ({showPopup}) => {

  const [formData, setFormData] = useState({
    email: "",
    password: "",
    rememberMe:false,
    recaptchaToken:""
  });
  const [showPassword,setShowPassword]=useState();
  const {setUser}=useAuth();
  const onCaptchaChange = (value) => {
    formData.recaptchaToken=value
  };
  const navigate = useNavigate();

  const handleChange = (e) => {
    setFormData({...formData, [e.target.name]: e.target.value});
  };

const handleSubmit = async (e) => {
  e.preventDefault();

  try {
    // 1. Gửi login request
    const result = await axios.post(`${hostNameHttp}/api/account/login`,formData,{withCredentials:true})
    console.log(result)
    if (result.status === 200) {
        try {
          const resMe = await axios.get(`${hostNameHttp}/api/account/me`,{withCredentials:true});
          console.log(resMe.data)
          setUser(resMe.data); // ✅ Interceptor tự refresh nếu 401
          if(resMe.status==200){
            navigate("/")
          }
        } catch (err) {
          console.error("Không thể lấy thông tin người dùng:", err);
          setUser(null); // ❌ Nếu refresh thất bại
        } 
    }
  } catch (error) {
    // ✅ Bắt lỗi rõ ràng hơn
    if (error.response?.status === 401) {
      showPopup("Tài khoản hoặc mật khẩu không đúng", "fail");
    } else if (error.response?.status === 403) {
      showPopup("Tài khoản không thể truy cập", "fail");
    } else {
      showPopup("Đã có lỗi xảy ra", "fail");
      console.error("Lỗi không xác định:", error);
    }
  }
};


  return (
    <>
    <div className="login-container">
      <form className="login-form" onSubmit={handleSubmit}>
        <h2>Đăng nhập</h2>
        
        <label>Email:</label>
        <input
          type="email"
          name="email"
          value={formData.email} placeholder='example@gmail.com'
          onChange={handleChange}
          required
        />

        <label>Mật khẩu:</label>
        <div style={{position:"relative",color:"black"}}>
                  <input
          type={!showPassword?"password":"text"}
          name="password"
          value={formData.password}
          onChange={handleChange}
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
        <ReCAPTCHA sitekey="6LfBWGkrAAAAADXUbeYUOQ2XX5uagemmIx56WHTG" style={{marginTop:"20px"}} onChange={onCaptchaChange}></ReCAPTCHA>
        <button type="submit">Đăng nhập</button>
        <div style={{display:"flex",justifyContent:"space-between"}}>
          <label htmlFor="" style={{fontWeight:"normal"}}>
            <Link to="/forgot-password" className='link'>Quên mật khẩu</Link>
          </label>
          <label style={{display: "flex",fontWeight:"normal"}}>
          <div ><input style={{fontSize:"20px",height:"20px",width:"20px",marginRight:"5px"}} type="checkbox" checked={formData.rememberMe} onChange={(e) => {setFormData({...formData,rememberMe:e.target.checked})}} /></div>
          <div> Nhớ mật khẩu</div>
        </label>
        </div>
      </form>
    </div>
    <div style={{alignContent:"center",textAlign:"center",backgroundColor:"rgba(255,255,255,0.7)",width:"100%",height:"40px",position:"absolute",bottom:"0",left:"50%",transform:"translateX(-50%)"}}>
        Copyright by IVYModa 2025
    </div>
    </>
  );
};

export default LoginForm;
