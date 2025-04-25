import { useEffect, useRef, useState } from "react"

import useFetch from "../../commons/UseFetch"
import Input from "../Input"
import MainButton from "../../commons/mainButton/MainButton"
import "./register.css"
function GetProvinces(){
    const provinces = useFetch("https://provinces.open-api.vn/api/?depth=2").data
    return provinces!=null? provinces: []
}
function CustomerInfo_form() {
        const provinces=GetProvinces()
        const [provinceId,setProvinceId]=useState(0)
        const [districts,setDistricts]=useState([])
        const [districtId,setDistrictId]=useState(0)
        const [wards,setWards]=useState([])

        const districtRef=useRef(null)
        const wardRef=useRef(null)
        useEffect(()=>{
            districtRef.current.value=0
            wardRef.current.value=0
            wardRef.current.disabled =true
            if(provinceId==0){
                setDistricts([])
                setWards([])
            }
            else{
            const fetchData = async () => {
                try {
                    const response = await fetch(`https://provinces.open-api.vn/api/p/${provinceId}/?depth=2`);
                    // Check if the response is OK
                    if (!response.ok) {
                        throw new Error('Network response was not ok');
                    }
                    const result = await response.json();
                    setDistricts(result.districts); // Set data to state
                } catch (error) {
                    throw new Error(error)
                } 
                console.log(districts);
                
            };

            fetchData(); // Call the fetch function
            }
        }, [provinceId])
        useEffect(()=>{
            wardRef.current.value=0
            if (districtId ==0){
                setWards([])
            }
            else{
            wardRef.current.disabled = false
            const fetchData = async () => {
                try {
                    const response = await fetch(`https://provinces.open-api.vn/api/d/${districtId}/?depth=2`);
                    // Check if the response is OK
                    if (!response.ok) {
                        throw new Error('Network response was not ok');
                    }
                    const result = await response.json();
                    setWards(result.wards); 
                } catch (error) {
                    throw new Error(error)
                } 
                console.log(wards);
                
            };

            fetchData(); // Call the fetch function
            }
        }, [districtId])
        
    return (
        <div className="customerInfo-form">
            <h4>Thông tin khách hàng</h4>
            <div>
                <div>
                    <label htmlFor="firstname">Họ<span>*</span></label>
                    <Input type="text" placeholder="Họ..." name="firstname" id="firstname"></Input>
                </div>
                <div>
                    <label htmlFor="lastname">Tên<span>*</span></label>
                    <Input type="text" placeholder="Tên..." name="lastname" id="lastname"></Input>
                </div>
            </div>
            <div>
                <div>
                    <label htmlFor="email">Email<span>*</span></label>
                    <Input type="email" placeholder="Email..." name="email" id="email"></Input>
                </div>
                <div>
                    <label htmlFor="phonenumber">Điện thoại<span>*</span></label>
                    <Input type="text" placeholder="Điện thoại..." name="phonenumber" id="phonenumber"></Input>
                </div>
            </div>
            <div>
                <div >
                    <label htmlFor="">Ngày sinh<span>*</span></label>
                    <Input type="date" placeholder="Ngày sinh..."></Input>
   
                </div>
                <div>
                    <label htmlFor="gender">Giới tính<span>*</span></label><br />
                    <select style={{padding:"12px",width:"100%"}}>
                        <option value="Nữ" name="gender" style={{fontSize:"5px"}}>Nữ</option>
                        <option value="Nam" name="gender" >Nam</option>
                        <option value="Khác" name="gender" >Khác</option>
                    </select>
                </div>
            </div>
           <div>
                <div>
                    <label htmlFor="provinces">Chọn tỉnh/thành phố<span>*</span></label><br />
                    
                    <select name="province" id="provinces" onChange={event=>setProvinceId(event.target.value)} style={{ padding: "12px", width: "100%" }}>
                        <option value={0}>Chọn tỉnh/thành phố</option>
                        {
                            provinces.map((opt,index)=>(
                                <option value={opt.code} key={index}>{opt.name}</option>
                            ))
                        }
                    </select>
                </div>
                <div>
                    <label htmlFor="districts">Chọn quận/huyện<span>*</span></label><br />
                    <select ref={districtRef} name="district" id="districts" onChange={event => setDistrictId(event.target.value)} style={{ padding: "12px", width: "100%" }}>
                        <option value={0}>Chọn quận/huyện</option>
                        {
                            districts.map((dist, index) => (
                                <option value={dist.code} key={index}>{dist.name}</option>
                            ))
                            }
    
                    </select>
                </div>
           </div>
           <div>
                <div>
                    <label htmlFor="wards">Chọn phường/xã<span>*</span></label><br />
                    <select ref={wardRef} name="ward" id="wards" style={{ padding: "12px", width: "100%" }}>
                        <option value={0}>Chọn phường/xã</option>
                        {
                            wards.map((ward, index) => (
                                <option value={ward.code} key={index}>{ward.name}</option>
                            ))
                        }

                    </select>
                </div>       
           </div>
           <div>
                <div>
                    <label htmlFor="address">Địa chỉ<span>*</span></label><br />
                    <textarea name="address" id="address" style={{ padding: "12px", width: "100%" }}></textarea>
                </div>
           </div>
        </div>
    )
}
function Password_form() {
    return (
        <div className="password-form">
            <h4>Thông tin mật khẩu</h4>
            <div>
                <div>
                    <label htmlFor="password">Mật khẩu<span>*</span></label>
                    <Input type="password" placeholder="Mật khẩu..." name="password" id="password"></Input>
                </div>
            </div>
            <div>
                <div>
                    <label htmlFor="re_password">Nhập lại mật khẩu<span>*</span></label>
                    <Input type="password" placeholder="Nhập lại mật khẩu..." name="re_password" id="re_password"></Input>
                </div>
            </div>
            <div>
                <Input type="checkbox"></Input>  Đồng ý với các điều khoản của IVY
            </div>
            <div className="button-register">
                <MainButton type="submit" text="ĐĂNG KÝ"></MainButton>
            </div>
        </div>
    )
}
function Register() {
    return (
        <section id="register">
            <h2 >ĐĂNG KÝ</h2>
            <form action=""  method="post" >

                    <CustomerInfo_form />

                    <Password_form></Password_form>

            </form>
        </section> 
    )
}
export default Register