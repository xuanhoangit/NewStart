import { useEffect, useState } from "react"
import HeaderForm from "../../commons/HeaderForm/HeaderForm"
import put from "../../PutData"
import { hostNameHttp } from "../../commons/HostName"
import "./Size.css"

const  ValidateSize=(size)=>{
    const errors = {};
    if(size.size__S<0){
        errors.size__S="Số lượng phải lớn hơn hoặc bằng 0"
    }
    if(size.size__M<0){
        errors.size__M="Số lượng phải lớn hơn hoặc bằng 0"
    }
    if(size.size__L<0){
        errors.size__L="Số lượng phải lớn hơn hoặc bằng 0"
    }
    if(size.size__XL<0){
        errors.size__XL="Số lượng phải lớn hơn hoặc bằng 0"
    }
    if(size.size__S<0){
        errors.size__XXl="Số lượng phải lớn hơn hoặc bằng 0"
    }
    return errors
}
const SizeUpdate = ({showPopup,product,updateSize})=>{
    const [size,setSize]=useState(product.size);
    const [error,setError]=useState({});
    // console.log(product)
    useEffect(()=>
        setSize(product.size)
    ,[product.size])
    const handleChange = (e) => {
            const { name, value } = e.target;
            setSize((prevProduct) => ({
              ...prevProduct,
              [name]: value
            }));
    };
    const handleSubmit = async (e) => {
    e.preventDefault();
    const er=ValidateSize(size)
    setError(er);

    console.log(size)
    if(Object.keys(er).length === 0){
        const result=await put(`${hostNameHttp}/api/size/cap-nhat`,size)

        if(result.status==200){
        updateSize(size)
        showPopup("Cập nhật số lượng thành công","success")
        console.log(result.status)
        }else{
            if(result.status==500){
            showPopup("Không thể thực hiện. Hệ thống lỗi!","fail")
            }
            else{
            showPopup("Cập nhật số lượng không thành công!","fail")
            }
        }
    }
    
      };
    return (
        <form onSubmit={handleSubmit} id="form-update-size">
            <HeaderForm title={"Cập nhật số lượng"}></HeaderForm>
            <div className="header">
                <div className="d-product">
                <div>
                    <div>Tên sản phẩm: {product.product__Name}</div> 
                </div>
                <div className="d-color">
                    <div>Màu sắc: <img style={{borderRadius:"50%",margin:"0 5px"}} width={"20px"} height={"20px"} src={product.subColorGetDTO.subColor__Image} alt="" />  ({product.subColorGetDTO.subColor__Name})</div>
                </div>
                </div>   
            </div>
            <h3>Số lượng:</h3><br />
            <div className="size-update">
                 <label>
                    Size S
                    <input type="number" onChange={handleChange}  name="size__S" value={size.size__S} />
                    <p className="error">{error.size__S}</p>
                </label>

                <label>
                    Size M
                    <input type="number" onChange={handleChange}  name="size__M" value={size.size__M}/>
                    <p className="error">{error.size__M}</p>
                </label>
                <label>
                    Size L
                    <input type="number" onChange={handleChange}  name="size__L" value={size.size__L}/>
                    <p className="error">{error.size__L}</p>
                </label>
                <label>
                    Size XL
                    <input type="number" onChange={handleChange}  name="size__XL" value={size.size__XL}/>
                    <p className="error">{error.size__XL}</p>
                </label>
                <label>
                    Size XXL
                    <input type="number" onChange={handleChange} name="size__XXl" value={size.size__XXl}/>
                    <p className="error">{error.size__XXl}</p>
                </label>
            </div>
        </form>
    )
}
export default SizeUpdate