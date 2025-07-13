import { useState,useRef,useEffect } from "react";
import { useAuth } from "../../../AuthProvider";
import RequireRole from "../../../RequireRole"
import "./UpdateProfile.css"
import {admin, humanSourceManager} from "../../Roles"
import { departments } from "../Department";
import HeaderForm from "../../commons/HeaderForm/HeaderForm";
import api from "../../../AxiosInstance";
const Role=({role,email,removeRole,showPopup})=>{
  const [isShowRemoveRole,setIsShowRemoveRole]=useState();
  
  // const [newUser,setNewUser]=useUser
   const removeRoleRef=useRef(null);
   const handleRevokeAccess=async ()=>{
        const result= await api.post("/api/employee/revoke-access",{Email:email,Role:role});
        console.log(result)
        if(result.status==200){
          removeRole(role)
          showPopup("Đã thu hồi chức vụ " +role,"success")
        }else{
          showPopup("Thu hồi chức vụ thất bại " +role,"fail")
        }
   }
       useEffect(() => {
          const handleClickOutside = (event) => {
            if (removeRoleRef.current && !removeRoleRef.current.contains(event.target)) {
              setIsShowRemoveRole(false); // Click ngoài => ẩn
            }
          };

          document.addEventListener('mousedown', handleClickOutside);
          
          return () => {
            document.removeEventListener('mousedown', handleClickOutside);
          };
        }, []);
  return (
    <div className="role" style={{marginBottom:"5px"}} onClick={()=>setIsShowRemoveRole(!isShowRemoveRole)}>
          
          <div    style={{ marginRight: "0px",background:"linear-gradient(135deg, #3b9400, #14cc14)" }}>
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
const UpdateProfile=({profile,showPopup,setNewProfile})=>{
     const [day, month, year] =profile.dateOfBirth.split('/');
  
    const [newUser,setNewUser]=useState({...profile,gender:profile.gender=="Nam"?"0":"1",dateOfBirth:`${year}-${month}-${day}`});
    // console.log(newUser.gender)
    const [roles,setRoles]=useState([]);
    const [message, setMessage] = useState('');
    const [prevImg,setPrevImg]=useState("")
    const [disable,setDisable]=useState(false);
  const removeRole=(role)=>{
      setNewUser({...newUser,roles:newUser.roles.filter(x=>x!=role)})
      const [day, month, year] =newUser.dateOfBirth.split('-');
      setNewProfile({...newUser,gender:newUser.gender=="0"?"Nam":"Nữ",dateOfBirth:`${year}/${month}/${day}`,roles:newUser.roles.filter(x=>x!=role)})
  }
    useEffect(()=>{
      setPrevImg(profile.avatar)

      setNewUser({...profile,gender:profile.gender=="Nam"?"0":"1",dateOfBirth:`${year}-${month}-${day}`})
      setRoles(departments.find(n => n.roomName === profile.department)?.roles);
    },[profile])
    const handleChange = (e) => {
        const { name, value } = e.target;
      
        setNewUser(prev=>({...prev,[name]:value}))
      };
  const handleImageChange = (e) => {
    const file = e.target.files[0]
    setPrevImg(URL.createObjectURL(file))
    setNewUser({
      ...newUser,avatar:file
    })

    };

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
 const handleSetRole=(e)=>{
    console.log(e.target.value)
    const roles=departments.find(n => n.roomName === e.target.value);
    console.log("rol",roles)
    setRoles(roles.roles)
  }

  
    const handleSubmit = async e => {
    e.preventDefault();
    const newUserToSend=convertToFormData(newUser)


   
      const res=await api.put(`/api/employee/update-profile`,newUserToSend)
      // Gửi tới API của bạn
      if(res.status==200){
        const data=res.data;
        console.log(data)
         const [day, month, year] =data.dateOfBirth.split('/');
         setNewProfile(data);
        setNewUser({...newUser,roles:data.roles})
        showPopup("Cập nhật thành công","success")
      }else if(res.status==409){
        showPopup("Người dùng đã tồn tại","fail")
      }else if(res.status==500){
        showPopup("Lỗi server cập nhật thất bại","fail")
      }else{
        showPopup("Cập nhật thất bại","fail")
      }
   
  };

    return newUser?(
      <form onSubmit={handleSubmit} style={{ 
    color: "#fff", 
    backgroundColor: "#121212", 
    padding: "10px 30px", 
    borderRadius: "12px", 
    maxWidth: "900px", 
    margin: "10px auto",
    boxShadow: "0 0 12px rgba(0,0,0,0.4)",
    position:"relative"
  }}>
    <HeaderForm title={"Cập nhật hồ sơ nhân viên"}></HeaderForm>
      <div style={{
      display: "grid",
      gridTemplateColumns: "1fr 2fr",
      gap: "20px"
    }}>
      <div > 
           <label htmlFor='avatar'>Ảnh chân dung <br />
            {
              prevImg?<img src={prevImg} alt="avatar" style={{marginTop:"12px",aspectRatio:"3/4",objectFit:"cover"}} srcSet="" width={"100%"} />:
              <div style={{width:"100%",marginTop:"12px",aspectRatio:"3/4",border:"1px dotted #ccc",textAlign:"center",alignContent:"center"}}>
                <i className="bi bi-plus-lg" style={{fontSize:"30px"}}></i>
              </div>
            }
            < input type="file" disabled={disable} style={{visibility:"hidden"}} name="avatar" id='avatar' accept='image'  onChange={handleImageChange} />
            </label>
      </div>
    <div style={{
      display: "grid",
      gridTemplateColumns: "1fr 1fr",
      gap: "10px"
    }}>
      {/* Họ và tên */}
      <div style={{ display: "flex", flexDirection: "column" }}>
        <label >Họ và tên</label>
        <input type="text" readOnly={disable} name="fullName" value={newUser.fullName} onChange={handleChange}/>
      </div>
  
      {/* Email */}
      <div style={{ display: "flex", flexDirection: "column" }}>
        <label >Email</label>
        <input type="email" readOnly={disable} name="email" value={newUser.email}  onChange={handleChange}/>
      </div>
  
      {/* Giới tính */}
      <div style={{ display: "flex", flexDirection: "column" }}>
        <label >Giới tính</label>
        <select name="gender"  value={String(newUser.gender)}  onChange={handleChange}>
          <option value="0">Nam</option>
          <option value="1">Nữ</option>
        </select>
      </div>
  
      {/* Ngày sinh */}
      <div style={{ display: "flex", flexDirection: "column" }}>
        <label >Ngày sinh</label>
        <input type="date" readOnly={disable} name="dateOfBirth" value={newUser.dateOfBirth} onChange={handleChange}/>
      </div>
  
      <div>
        {/* Phòng ban */}
      <div  style={{ display: "flex", flexDirection: "column" }}>
        <label >Phòng ban</label>
        <select name="department" disabled={disable} value={newUser.department}  onChange={(e)=>{handleSetRole(e),handleChange(e)}}>
          {departments.map((department,i)=>(
            <option value={department.roomName} key={i}>{department.roomName}</option>
          ))}
        </select>
      </div>
    
      <div style={{ display: "flex", flexDirection: "column" }}>
        <label >Chức vụ</label>
        <div >
          {newUser.roles?.length>0?newUser.roles.map((role, i) => (
          <Role showPopup={showPopup}  key={i} role={role} email={newUser.email} removeRole={removeRole}></Role>
         
          )):"Không"}
        </div>
      </div>
      
    </div>
      {/*  */}
      <div  style={{ display: "flex", flexDirection: "column" }}>
        <label >Thêm chức vụ</label>
        <select name="role" disabled={roles==null?true:false} value={newUser.role}  onChange={handleChange}>
          <option value="">Chọn</option>
          {roles?.map((role,i)=>(
            <option value={role} key={i}>{role}</option>
          ))}
        </select>
      </div>
    </div>
    </div>
  </form>
  
  ):null;
}
export default UpdateProfile