import { useState, useRef,useEffect} from "react";
import { useAuth } from "../../../AuthProvider";
import "./Profile.css"
import api from "../../../AxiosInstance";
import EmployeeProfile from "../HumanSource/UpdateProfile";
import { admin } from "../../Roles";
import { use } from "react";
const Role=({role,email,style,roles})=>{
 
  const [isShowRemoveRole,setIsShowRemoveRole]=useState();
  const [isReturn,setIsReturn]=useState(true);
   const removeRoleRef=useRef(null);
   const handleRevokeAccess=async ()=>{
        const result= await api.post("/api/employee/revoke-access",{Email:email,Role:role});
        console.log(result)
        if(result.status==200){
          if(roles){
            roles = roles.filter(role => role !== role);
          }
          setIsReturn(false)
        }
   }
       useEffect(() => {
          const handleClickOutside = (event) => {
            if (removeRoleRef.current && !removeRoleRef.current.contains(event.target)) {
              setIsShowRemoveRole(false); // Click ngoài => ẩn
            }
          };
          // console.log(isShow)
          document.addEventListener('mousedown', handleClickOutside);
          
          return () => {
            document.removeEventListener('mousedown', handleClickOutside);
          };
        }, []);
  return !isReturn?null:(
    <div className="role" onClick={()=>setIsShowRemoveRole(!isShowRemoveRole)}>
          
         <div    style={{...style, marginRight: "0px",background:"linear-gradient(135deg, #3b9400, #14cc14)" }}>
            {role}
            {role!=admin?<i className="bi bi-three-dots" style={{float:"right"}}></i>:null}
         </div>
         {role!=admin?
         <ul  ref={removeRoleRef} style={{fontSize:"12px",display:isShowRemoveRole?"block":"none"}}>
          <li onClick={handleRevokeAccess}>Thu hồi quyền <b>{role}</b></li>
         </ul>:null}
         </div>
  )
}
const Profile=({user,setContent,setIsShow,showPopup})=>{
  const [isShowAction,setIsShowAction]=useState(false)
  const [profile,setProfile]=useState(user);
    const actionRef=useRef(null);
       useEffect(() => {
          const handleClickOutside = (event) => {
            if (actionRef.current && !actionRef.current.contains(event.target)) {
              setIsShowAction(false); // Click ngoài => ẩn
            }
          };
          // console.log(isShow)
          document.addEventListener('mousedown', handleClickOutside);
          
          return () => {
            document.removeEventListener('mousedown', handleClickOutside);
          };
        }, []);
   const labelStyle = {
  fontWeight: "bold",
  marginBottom: "6px",
  color: "#ccc",
};

const valueStyle = {
  backgroundColor: "#1e1e1e",
  padding: "4px 7px",
  borderRadius: "6px",
  border: "1px solid #333",
  color: "#fff",
  fontSize:"12px",
  minHeight: "20px"
};

    return profile?(
    <div style={{ 
  color: "#fff", 
  backgroundColor: "#121212", 
  padding: "30px", 
  borderRadius: "12px", 
  maxWidth: "900px", 
  margin: "10px auto",
  boxShadow: "0 0 12px rgba(0,0,0,0.4)",
  position:"relative"
}}>
  <div className="employee-action" ref={actionRef}>
    <div className="icon" onClick={()=>setIsShowAction(change=>!change)}>
        <i className="bi bi-three-dots"></i>
    </div>
    <ul style={{display:isShowAction?"block":"none"}}>
      <li onClick={()=>{setIsShow(true),setContent(<EmployeeProfile setNewProfile={setProfile} profile={profile} showPopup={showPopup}></EmployeeProfile>)}}><i className="bi bi-pencil-square"></i>Cập nhật thông tin</li>
      {/* <li>Sa thải</li> */}
    </ul>
  </div>
    <div style={{
    display: "grid",
    gridTemplateColumns: "1fr 2fr",
    gap: "20px"
  }}>
    <div > 
        <label style={labelStyle} htmlFor=''>Ảnh chân dung <br />
        <img style={{objectFit:"cover",aspectRatio:"3/4",width:"100%",marginTop:"5px"}} src={profile.avatar?profile.avatar:"https://res.cloudinary.com/delq6xxku/image/upload/v1751179370/avatar-3_sagr5q.jpg"} alt="" srcSet="" />
        </label>
    </div>
  <div style={{
    display: "grid",
    gridTemplateColumns: "1fr 1fr",
    gap: "10px"
  }}>
    {/* Họ và tên */}
    <div style={{ display: "flex", flexDirection: "column" }}>
      <label style={labelStyle}>Họ và tên</label>
      <p style={valueStyle}>{profile.fullName}</p>
    </div>

    {/* Email */}
    <div style={{ display: "flex", flexDirection: "column" }}>
      <label style={labelStyle}>Email</label>
      <p style={valueStyle}>{profile.email}</p>
    </div>

    {/* Giới tính */}
    <div style={{ display: "flex", flexDirection: "column" }}>
      <label style={labelStyle}>Giới tính</label>
      <p style={valueStyle}>{profile.gender}</p>
    </div>

    {/* Ngày sinh */}
    <div style={{ display: "flex", flexDirection: "column" }}>
      <label style={labelStyle}>Ngày sinh</label>
      <p style={valueStyle}>{profile.dateOfBirth}</p>
    </div>

    <div>
      {/* Phòng ban */}
    <div  style={{ display: "flex", flexDirection: "column" }}>
      <label style={labelStyle}>Phòng ban</label>
      <p style={valueStyle}>{profile.department}</p>
    </div>
   {/* Phòng ban */}
    <div  style={{ display: "flex", flexDirection: "column",marginTop:"5px" }}>
      <label style={labelStyle}>Ngày bắt đầu</label>
      <p style={valueStyle}>{profile.createDate}</p>
    </div>
    </div>
    {/* Chức vụ */}
    <div style={{ display: "flex", flexDirection: "column" }}>
      <label style={labelStyle}>Chức vụ</label>
      <div >
        {profile.roles.length>0?profile.roles.map((role, i) => (
        <Role key={i} role={role} email={profile.email} style={valueStyle}></Role>
      
        )):<p style={valueStyle}>Không</p>}
      </div>
    </div>
    
  </div>
  </div>
</div>

):null;
}
export default Profile