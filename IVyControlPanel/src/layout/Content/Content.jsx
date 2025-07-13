import {Routes,Route,useNavigate } from "react-router-dom"

import "./Content.css";
import ContentTop from '../../components/ContentTop/ContentTop';
import ContentMain from '../../components/ContentMain/ContentMain';

import HomeProduct from "../../components/Products/Index/Index";
import HRM from "../../components/HRM/Index/Index"
import Me from "../../components/HRM/Me/Me";
import RequireRole from "../../RequireRole";
import { admin, humanSourceManager } from "../../components/Roles";
import { useAuth } from "../../AuthProvider";
import NotFound from "../../components/NotFound";
import EmployeeProfile from "../../components/HRM/HumanSource/UpdateProfile";
import UserNoAccess from "../../components/HRM/UserNoAccess/UserNoAccess";
import Orders from "../../components/Orders/Orders";
const Content = ({showPopup}) => {

  const {user}=useAuth();
  return (
    <div className='main-content'>
      <ContentTop />
      <Routes>
          <Route path="/" element={<ContentMain />}></Route>
          <Route path="/products/" element={<HomeProduct showPopup={showPopup}/>}></Route>
          <Route path="*" element={<NotFound/>} />
          {
          user?.roles.includes(admin) || user?.roles.includes(humanSourceManager)?
          <><Route path="/hrm" element={<HRM  showPopup={showPopup}/>}></Route>
          <Route path="/hrm/nhan-vien" element={<EmployeeProfile  showPopup={showPopup}/>}></Route>
          <Route path="/hrm/khong-chuc-vu" element={<UserNoAccess  showPopup={showPopup}/>}></Route>
          </>
          :null}
          <Route path="/me" element={<Me/>}></Route>
          <Route path="/order" element={<Orders/>}></Route>
      </Routes>
     
    </div>
  )
}

export default Content
