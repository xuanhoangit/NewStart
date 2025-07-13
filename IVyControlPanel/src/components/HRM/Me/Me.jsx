import { useState } from "react";
import { useAuth } from "../../../AuthProvider";

const Me=()=>{
    const {user}=useAuth();
   const labelStyle = {
  fontWeight: "bold",
  marginBottom: "6px",
  color: "#ccc",
};

const valueStyle = {
  backgroundColor: "#1e1e1e",
  padding: "10px 14px",
  borderRadius: "6px",
  border: "1px solid #333",
  color: "#fff",
  minHeight: "40px"
};

    return user?(
    <div style={{ 
  color: "#fff", 
  backgroundColor: "#121212", 
  padding: "30px", 
  borderRadius: "12px", 
  maxWidth: "900px", 
  margin: "40px auto",
  boxShadow: "0 0 12px rgba(0,0,0,0.4)"
}}>
  <h2 style={{ marginBottom: "24px", color: "#00ff88" }}>Thông tin nhân sự</h2>
    <div style={{
    display: "grid",
    gridTemplateColumns: "1fr 2fr",
    gap: "20px"
  }}>
    <div > 
        <label style={labelStyle} htmlFor=''>Ảnh chân dung <br />
        <img style={{objectFit:"cover",aspectRatio:"3/4",width:"100%",marginTop:"5px"}} src={user.avatar?user.avatar:"https://res.cloudinary.com/delq6xxku/image/upload/v1751179370/avatar-3_sagr5q.jpg"} alt="" srcset="" />
        </label>
    </div>
  <div >
    {/* Họ và tên */}
    <div style={{ display: "flex", flexDirection: "column" }}>
      <label style={labelStyle}>Họ và tên</label>
      <p style={valueStyle}>{user.fullName}</p>
    </div>

    {/* Email */}
    <div style={{ display: "flex", flexDirection: "column" }}>
      <label style={labelStyle}>Email</label>
      <p style={valueStyle}>{user.email}</p>
    </div>

    {/* Giới tính */}
    <div style={{ display: "flex", flexDirection: "column" }}>
      <label style={labelStyle}>Giới tính</label>
      <p style={valueStyle}>{user.gender}</p>
    </div>

    {/* Ngày sinh */}
    <div style={{ display: "flex", flexDirection: "column" }}>
      <label style={labelStyle}>Ngày sinh</label>
      <p style={valueStyle}>{user.dateOfBirth}</p>
    </div>

    {/* Phòng ban */}
    <div style={{ display: "flex", flexDirection: "column" }}>
      <label style={labelStyle}>Phòng ban</label>
      <p style={valueStyle}>{user.department}</p>
    </div>

    {/* Chức vụ */}
    <div style={{ display: "flex", flexDirection: "column" }}>
      <label style={labelStyle}>Chức vụ</label>
      <p style={valueStyle}>
        {user.roles.map((role, i) => (
          <span key={i} style={{ marginRight: "10px",background:"linear-gradient(135deg, #3b9400, #14cc14)" }}>{role}</span>
        ))}
      </p>
    </div>
  </div>
  </div>
</div>

):null;
}
export default Me