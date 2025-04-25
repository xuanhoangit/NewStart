import { Link } from "react-router-dom"
import "./right.css"
import MainButton from '../../commons/mainButton/MainButton'
function RegisterAndDownload(props) {
    return (
			<>
			<div className="right">
			<p></p>
				<div className="register-contact">
					<p className="title">{props.title}</p>
					<form id="formSubcribe">
						<input id="emailSubcribe" type="text" name="email" placeholder="Nhập địa chỉ email" required="required" autoComplete='off' />
						<div className="btn-submit">
								<MainButton type="submit" text="Đăng ký"></MainButton>
						</div>
					</form>
					<div id="subscribe_error"></div>
				</div>
				<div className="linkDownload">
					<p className="title">Download App</p>
					<ul>
						<li>
							<Link id="app_ios" to="http://ios.ivy.vn" className="link-white" target="_blank" title="Tải App IVYmoda trên App Store">
								<img src="https://pubcdn.ivymoda.com/ivy2/images/appstore.png" className="img-fluid lazy" alt="" loading="lazy" />
							</Link>
						</li>
						<li>
							<Link id="app_android" to="http://android.ivy.vn" className="link-white" target="_blank" title="Tải App IVYmoda trên Google Play">
								<img src="https://pubcdn.ivymoda.com/ivy2/images/googleplay.png" className="img-fluid lazy" alt="" loading="lazy" />
							</Link>
						</li>
					</ul>
				</div>
				</div>
			</>
    )
}
export default RegisterAndDownload