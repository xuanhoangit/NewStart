import { iconsImgs } from "../../utils/images";
import "./ContentTop.css";
import { useContext } from "react";
import { SidebarContext } from "../../context/sidebarContext";
import {useAuth} from "../../AuthProvider";
import { useNavigate } from "react-router-dom";
import CartItem from "../Orders/CartItems/CartItem";

const ContentTop = () => {
  const { toggleSidebar } = useContext(SidebarContext);
  const {  logout } = useAuth();
  const navigate = useNavigate();

  const redirectTo=(url)=>{
    navigate(url)
  }
  const handleLogout = async () => {
    await logout();
    navigate("/login"); // Chuyển hướng về trang đăng nhập
  };
  return (
      <div  className="main-content-top">
        <div className="content-top-left">
            <button type="button" className="sidebar-toggler" onClick={() => toggleSidebar() }>
                <img src={ iconsImgs.menu } alt="" />
            </button>
            <h3 className="content-top-title">Home</h3>
        </div>
        <div className="content-top-btns" style={{display:"flex"}}>

            <div onClick={()=>redirectTo()} type="button" className="search-btn content-top-btn">
                <CartItem></CartItem>
            </div>
            <button type="button" className="search-btn content-top-btn">
                <img src={ iconsImgs.search } alt="" />
            </button>
            <button className="notification-btn content-top-btn" style={{marginRight:"40px"}}>
                <img src={ iconsImgs.bell } />
                <span className="notification-btn-dot"></span>
            </button>
            <button  type="button" onClick={handleLogout} title="Đăng xuất" className="logout content-top-btn">
                <i className="fa fa-sign-out"  style={{fontSize:"26px"}} aria-hidden="true"></i>
            </button>
                
        </div>
    </div>
  )
}

export default ContentTop
