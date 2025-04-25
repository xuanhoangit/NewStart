import { GoogleOAuthProvider } from '@react-oauth/google';

import Input from "../Input"
import MainButton from "../../commons/mainButton/MainButton"
import "./login.css"
import googleIcon from "../../../assets/google-icon.png"
import facebookIcon from "../../../assets/facebook-icon.png"
const icons =[
    `${googleIcon}`,
    `${facebookIcon}`
]
function Login(params) {
    return (
        <>

        <section className="container-login">
            <div id="login">
            <h3>Bạn đã có tại khoản IVY</h3>
            <p>Nếu bạn đã có tài khoản, hãy đăng nhập để tích lũy điểm thành viên và nhận được những ưu đãi tốt hơn!</p>
            <form action="" id="form-login">
                <Input type="text" name="account" id="account" placeholder="Email/SĐT"></Input><br />
                <Input type="password" name="password" id="password" placeholder="Mật khẩu"></Input>
                <div className="link">
                    <div >
                        <Input type="checkbox"></Input> Ghi nhớ đăng nhập 
                    </div>
                    <div >
                        <a href="">Quên mật khẩu?</a>
                    </div>
                </div>
                <div className="link">
                    <div >
                        <a href="">Đăng nhập bằng QR</a>
                    </div>
                    <div >
                        <a href="">Đăng nhập bằng OTP</a>
                    </div>
                </div>
                <div id="submit">
                    <MainButton text="ĐĂNG NHẬP" />
                    <p style={{textDecorationLine:""}}>hoặc</p>
                    <div className='external-login'>
                        <img src={icons[0]}  alt="" srcset="" height="100%"/>
                        
                        <img src={icons[1]}  alt="" srcset="" height="100%"/>
                    </div>
                </div>
            </form>
            </div>
            <div id="register-link">
                <h3>Khách hàng mới của IVY moda</h3>
                <p>Nếu bạn chưa có tài khoản trên ivymoda.com, hãy sử dụng tùy chọn này để truy cập biểu mẫu đăng ký.</p>
                <p>Bằng cách cung cấp cho IVY moda thông tin chi tiết của bạn, quá trình mua hàng trên ivymoda.com sẽ là một trải nghiệm thú vị và nhanh chóng hơn!</p>
                <div className="link-to-register">
                    <a href="/register">
                        <MainButton text="ĐĂNG KÝ" />
                    </a>
                </div>
            </div>
        </section>
        </>
    )
}
export default Login