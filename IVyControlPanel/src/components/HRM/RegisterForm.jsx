import React, { useState } from 'react';
import post from "../PostData"
import {hostNameHttp} from "../commons/HostName"
import './RegisterForm.css'; // CSS tách riêng
import {departments} from "./Department"

const RegisterForm = ({showPopup}) => {
  // console.log(showPopup)
  const [formData, setFormData] = useState({
    fullName: '',
    avatar: "",
    dateOfBirth: '',
    gender: 0,
    department: '',
    email: '',
    role: '',
    createDate:new Date().toISOString()
  });
  const [roles,setRoles]=useState([])
  const [message, setMessage] = useState('');
  const [prevImg,setPrevImg]=useState("")
  const handleChange = e => {
    const { name, value } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: value
    }));
  
  };
  const handleImageChange = (e) => {
    const file = e.target.files[0]
    setPrevImg(URL.createObjectURL(file))
    setFormData({
      ...formData,avatar:file
    })

    };
  const handleSetRole=(e)=>{
    console.log(e.target.value)
    const roles=departments.find(n => n.roomName === e.target.value);
    setRoles(roles.roles)
  }
  const convertToFormData = (data) => {
  const formDataToSend = new FormData();

  // Duyệt qua tất cả key trong object
  for (const key in data) {
    if (data[key] !== null && data[key] !== undefined) {
      formDataToSend.append(key, data[key]);
    }
  }

  return formDataToSend;
};
  const handleSubmit = async e => {
    e.preventDefault();
    const formDataToSend=convertToFormData(formData)


   
      const res=await post(`${hostNameHttp}/api/account/register`,formDataToSend)
      // Gửi tới API của bạn
      if(res.status==201){
        showPopup("Thêm người dùng thành công","success")
      }else if(res.status==409){
        showPopup("Người dùng đã tồn tại","fail")
      }else if(res.status==500){
        showPopup("Lỗi server thêm người dùng thất bại","fail")
      }else{
        showPopup("Thêm người dùng thất bại","fail")
      }
   
  };

  return (
    <form className="register-form" onSubmit={handleSubmit}>
      {/* <HeaderForm  title={"Đăng ký tài khoản nhân viên"}></HeaderForm> */}
      <h2>Đăng ký tài khoản nhân viên</h2>
      <div style={{display:"flex"}}>
        <div className="form-group" style={{flex:"1",width:"150px",aspectRatio:"3/4",marginRight:"20px"}}>
            <label htmlFor='avatar'>Ảnh chân dung <br />
            {
              prevImg?<img src={prevImg} alt="avatar" style={{marginTop:"12px",aspectRatio:"3/4",objectFit:"cover"}} srcSet="" width={"100%"} />:
              <div style={{width:"100%",marginTop:"12px",aspectRatio:"3/4",border:"1px dotted #ccc",textAlign:"center",alignContent:"center"}}>
                <i className="bi bi-plus-lg" style={{fontSize:"30px"}}></i>
              </div>
            }
            < input type="file" style={{visibility:"hidden"}} name="avatar" id='avatar' accept='image'  onChange={handleImageChange} required />
            </label>
          </div>
        <div className="form-grid" style={{flex:"3"}}>
          
          <div className="form-group">
            <label>Họ và tên</label>
            <input type="text" name="fullName" value={formData.fullName} onChange={handleChange} required />
          </div>

          <div className="form-group">
            <label>Ngày sinh</label>
            <input type="date" name="dateOfBirth" value={formData.dateOfBirth} onChange={handleChange} required />
          </div>

          <div className="form-group">
            <label>Giới tính</label>
            <select name="gender" value={formData.gender} onChange={handleChange}>
              <option value={0}>Nam</option>
              <option value={1}>Nữ</option>
            </select>
          </div>

          <div className="form-group">
            <label>Phòng ban</label>
            <select name="department" value={formData.department} onChange={(e)=>{handleChange(e),handleSetRole(e)}} required id="room">
              <option value="">Chọn</option>
              {departments.map((department,i)=>(
                  <option value={department.roomName} key={i}>{department.roomName}</option>
              ))}
            </select>

          </div>
          <div className="form-group">
            <label>Chức vụ</label>
            <select disabled={roles.length>0?false:!false} name="role" value={formData.role} onChange={handleChange} required id="roles">
              <option value="">Chọn</option>
              {roles.map((role,i)=>(
                  <option value={role} key={i}>{role}</option>
              ))}
            </select>

          </div>

          <div className="form-group">
            <label>Email</label>
            <input type="email" name="email" value={formData.email} onChange={handleChange} required />
          </div>

          {/* <div className="form-group">
            <label>Tên đăng nhập</label>
            <input type="text" name="userName" value={formData.userName} onChange={handleChange} required />
          </div>

          <div className="form-group">
            <label>Mật khẩu</label>
            <input type="password" name="password" value={formData.password} onChange={handleChange} required />
          </div>

          <div className="form-group">
            <label>Xác nhận mật khẩu</label>
            <input type="password" name="confirmPassword" value={formData.confirmPassword} onChange={handleChange} required />
          </div> */}
        </div>
      </div>
      <button type="submit">Đăng ký</button>
      {message && <p className="message">{message}</p>}
    </form>
  );
};

export default RegisterForm;
