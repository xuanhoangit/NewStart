import { useState } from "react";
import "./ForgotPassword.css"
import { Link } from "react-router-dom";
import ReCAPTCHA from "react-google-recaptcha";
import post from "../components/PostData";
import { hostNameHttp } from "../components/commons/HostName";

const ForgotPassword=()=>{
    const [email,setEmail]=useState("");
    const [recaptcha,setRecaptcha]=useState("")
    const onCaptchaChange = (value) => {
    setRecaptcha(value)
  };
    const handleSubmit = async(e) => {
    e.preventDefault();
    const result= await post(`${hostNameHttp}/api/account/forgot-password`,{Email:email,RecaptchaToken:recaptcha})
    if(result.status==200){
      
    }
    // else if (result.status==401){
    //   showPopup("Tài khoản hoặc mật khẩu không đúng",'fail')
    // }else if(result.status==403){
    //   showPopup("Tài khoản không thể truy cập",'fail')
    // }
     
  };
    return (
       <div className="forgot-pw-container">
         <form action="" onSubmit={handleSubmit} id="forgot-pasword">
            <h2>Quên mật khẩu</h2>
            <label htmlFor="">
                Nhập email:
                <input type="email" value={email} onChange={(e)=>setEmail(e.target.value)} placeholder="example@gmail.com"/>
            </label>
             <ReCAPTCHA sitekey={"6LfBWGkrAAAAADXUbeYUOQ2XX5uagemmIx56WHTG"} onChange={onCaptchaChange}></ReCAPTCHA>
            <button type="submit">Yêu cầu</button>
            <Link  style={{float:"right",margin:"5px 5px 0 0"}} to="/login" className="link">Đăng nhập</Link>
        </form>
       </div>
    )
}
export default ForgotPassword;