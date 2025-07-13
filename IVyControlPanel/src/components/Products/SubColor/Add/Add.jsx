import { useEffect, useRef, useState } from "react"
import "./Add.css"
import { hostNameHttp } from "../../../commons/HostName"
import get from '../../../GetData';
import ColorSelected from "../../../commons/ColorSelected/ColorSelected";
import useFetch from "../../../UseFetch";
import HeaderForm from "../../../commons/HeaderForm/HeaderForm";
import postFile from '../../../PostFile';
const ValidateSubColor=(sc)=>{
    console.log(sc)
    const errors = {};
    if(sc.SubColor__Image==''){
        errors.SubColor__Image="Hãy chọn màu.";
    }
    if(sc.SubColor__Name==''){
        errors.SubColor__Name="Hãy chọn nhập tên màu.";
    }
    if(sc.ColorIds.length==0){
        errors.ColorIds="Hãy chọn nhóm màu cho màu cần thêm.";
    }
    return errors
}
const Add=({showPopup})=>{
    const uploadRef=useRef();
    const [colorgroups,setColorgroupsData]=useState([])
    const [subcolorsData,setSubColorsData]=useState([])
    const [error,setError]=useState({})
    
    const [colors_Selected,setColors_Selected]=useState([]);
    useEffect(()=>{
        const InitData=async ()=>{
            const colorgroupsData=await get(`${hostNameHttp}/api/nhom-mau`);
            const subcolorsData=await get(`${hostNameHttp}/api/mau`);
            setColorgroupsData(colorgroupsData);
            setSubColorsData(subcolorsData)
        }
        InitData()
    },[])
    //Add to colors
  const addColor = (newColor_Selected) => {
    setColors_Selected(prevState => [...prevState, newColor_Selected]);
  };

  //xóa color đã chọn
  const removeColorSelected=(indexToRemove)=>{
      setColors_Selected(prev => 
        prev.filter((_, index) => index !== indexToRemove)
      );
      subColor.ColorIds.splice( indexToRemove,1)
    //   console.log( subColor)
      // console.log(indexToRemove)
      // console.log(product.ColorIds)
  }
  //Xử lí khi chọn color
  const onChangeColorSelected=(event)=>{
    var et=event.target;
    // console.log(colorgroups)
    const color= colorgroups.find(item => item.color__Id == et.value)
    //  console.log("color", color)
    if(et.value!==0){
      //Kiểm tra xem có color hay chưa 
      const isExist=subColor.ColorIds.includes(color.color__Id)
      
      
      if(!isExist){
        subColor.ColorIds.push(color.color__Id)
        const newColor_Selected={
            Color__Name:`${color.color__Name}`,
            Color__Url:`${hostNameHttp}/images/colors/${color.color__Image}`
        }
        addColor(newColor_Selected)
        // console.log( product.ColorIds)
        // console.log( subColor)
      }
      et.value=0;
    }
  }     

    function handleFileChange(event) {
    const file = event.target.files[0]; // lấy file đầu tiên
    subColor.SubColor__Image=file;
    if (file) {
        const reader = new FileReader();
        reader.onload=(e)=>{
            uploadRef.current.style.display="block";
            uploadRef.current.style.backgroundImage = `url(${e.target.result})`
        }
        reader.readAsDataURL(file);
    }
    }

    const [subColor,setSubColor]=
        useState({
            SubColor__Name:"",
            SubColor__Image:"",
            ColorIds:[]
        })
        const handleChange = (e) => {
            const { name, value } = e.target;
            setSubColor((prevProduct) => ({
              ...prevProduct,
              [name]: value
            }));
            console.log(subColor)
          };
        const handleSubmit = async (e) => {
        e.preventDefault();
        
        const er=ValidateSubColor(subColor)
            setError(er);
            console.log(er)
            
            if(Object.keys(er).length === 0){
            const formData = new FormData();
            console.log(subColor.SubColor__Image instanceof File)
            formData.append("SubColor__Name",subColor.SubColor__Name)
            formData.append("SubColor__Image",subColor.SubColor__Image)
            formData.append("ColorIds",subColor.ColorIds)

            const result=await postFile(`${hostNameHttp}/api/mau/them-moi`,formData)
            console.log("res",result)
            if(result.status==200||result.status==201){
                showPopup("Thêm màu thành công","success");
                setSubColor({
                    SubColor__Name:"",
                    SubColor__Image:"",
                    ColorIds:subColor.ColorIds
                })
                uploadRef.current.style.display="none"
                setSubColorsData((prev)=>[result.data,...prev])
                console.log(result.data)
            }else{
                console.log(result.status==409)
                if(result.status==409){
                    showPopup("Màu đã tồn tại!","fail")
                }
                else if(result.status==500){
                    showPopup("Không thể thực hiện. Hệ thống lỗi!","fail")
                }
                else{
                    showPopup("Thêm màu không thành công!","fail")
                }
            }
           
      }
    
      };
    return (
        <form id="form-add-subcolor" onSubmit={handleSubmit} encType={"multipart/form-data"}>
            <HeaderForm title={"Thêm màu"}></HeaderForm>
            <div className="input-data">
                <div>
                    <label htmlFor="">
                        Tên màu:
                        <input type="text" name="SubColor__Name" value={subColor.SubColor__Name} onChange={handleChange}/>  
                        <p className="error" style={{marginTop:"5px"}}>{error.SubColor__Name}</p>
                    </label>
                </div>
                <div>
                        Chọn hình ảnh:
                    <label htmlFor="imageSelect" >
                        <div className="icon-upload">
                            <div>
                                <i className="fa-solid fa-upload"></i>
                            </div>
                            <div >
                                Chọn
                            </div>
                            <input  type="file"  onChange={handleFileChange} id="imageSelect" accept="image/*"/>
                        </div>
                            <div  ref={uploadRef} style={{border:"1px solid rgb(211, 227, 253)",width:"100%",height:"40px",marginTop:"5px",borderRadius:"4px",display:"none"}}>
                            </div>
                        <p className="error" style={{marginTop:"10px"}}>{error.SubColor__Image}</p>
                    </label>
                </div>
                <div>
                    <label htmlFor="">
                        Nhóm màu (vd: Vàng,Trắng,Đen...):
                        <select   onChange={(event)=>onChangeColorSelected(event)}  id="">
                            <option value="0">Chọn</option>
                            {colorgroups?.map((color,i)=>(
                                <option  key={i} value={color.color__Id}>{color.color__Name}</option>
                            ))}
                        </select>
                            <div className='' style={{display:"flex",flexWrap:"wrap"}}>
                            {colors_Selected?.map((color,index)=>(
                            <ColorSelected data={color} key={index} action={()=>{removeColorSelected(index)}}></ColorSelected>
                            ))}
                        </div>
                        <p className="error" style={{marginTop:"5px"}}>{error.ColorIds}</p>
                    </label>
                </div>
            </div>
            <h3 >Đã thêm gần đây</h3>
                            <p style={{margin:"10px 0",borderBottom:"2px solid rgb(12, 166, 120)"}}></p>
            <div style={{display:"flex",flexWrap:"wrap"}}>
                {subcolorsData?.map((subColor,i)=>(
                    <div key={i} style={{margin:"3px 4px",border:"1px solid rgb(211, 227, 253)",minWidth:"100px",textAlign:"center",padding:"5px 10px",borderRadius:"4px"}}>
                        <div  style={{margin:"auto",marginBottom:"5px",borderRadius:"50%",width:"20px", height:"20px",border:"1px solid rgb(211, 227, 253)",backgroundImage:`url(${subColor.subColor__Image})`}}></div>
                        <div>{subColor.subColor__Name}</div>
                    </div>
                ))}
            </div>
        </form>
    )
}
export default Add