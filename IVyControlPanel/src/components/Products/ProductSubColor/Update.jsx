import { useEffect, useRef, useState,Suspense } from "react"
import { useNavigate } from "react-router-dom";
import { hostNameHttp } from "../../commons/HostName";
import "./Add.css"

import put from "../../PutData";
import get from '../../GetData';

import ListSubColor from "../SubColor/Get/Get";

import HeaderForm from "../../commons/HeaderForm/HeaderForm";
import useFetch from "../../UseFetch";

const ValidateProductSubColor =async (psc) => {
    const errors = {};
   
    if (psc.ProductSubColor__SubColorId===0) {
      errors.ProductSubColor__SubColorId = 'Vui lòng chọn \'Màu sản phẩm\'';
    }
    // Kiểm tra trường Product__Id
    if (psc.ProductSubColor__ProductId === 0) {
      errors.ProductSubColor__ProductId = 'Vui lòng chọn \'Sản phẩm\'';
    }
    if (psc.ProductSubColor__Price < 0) {
      errors.ProductSubColor__Price = `Vui lòng nhập lại!. Giá sản phẩm = ${psc.ProductSubColor__Price} bạn tặng tiền cho khách ư?`;
    }
    if (psc.ProductSubColor__Discount > 100) {
      errors.ProductSubColor__Discount = `Vui lòng nhập lại!. Giảm giá = ${psc.ProductSubColor__Discount} bạn tặng tiền cho khách ư?`;
    }
    if (psc.ProductSubColor__Discount < 0) {
      errors.ProductSubColor__Discount = `Vui lòng nhập lai!. Giảm giá = ${psc.ProductSubColor__Discount} là một cách tăng giá trá hình?`;
    }
    console.log(psc)
    console.log(errors)
    return errors;
  };
  
 

function ProductSubColorUpdate({showPopup,product,actionAfterUpdateProductSubColor}) {
 
    const [error,setError]=useState({});
    const subColors= useFetch(`${hostNameHttp}/api/mau`).data;
    const subcolorselectedref=useRef(null);
    const [subColor,setSubColor]=useState(product.subColorGetDTO);
    const [isShowSubcolor,setIsShowSubcolor]=useState(false);
    const  [productsubcolor,setProductsubColor]=useState(product.productSubColor)
    
    useEffect(() => {
      console.log("update",product.productSubColor)
      setProductsubColor(product.productSubColor);
      action(false,product.subColorGetDTO)
    }, [product]);
   
 
    const action=(isShow,item)=>{
      setIsShowSubcolor(isShow);
    
      setSubColor(item)
      console.log(subColor)
      productsubcolor.productSubColor__SubColorId=item.subColor__Id
      // console.log("hhehe",subcolor_Id)
      subcolorselectedref.current.querySelector(":first-child").style.backgroundImage=`url(${item.subColor__Image})`;
      subcolorselectedref.current.querySelector(":last-child").innerText=item.subColor__Name;
    }



    const handleChange = (e) => {
        const { name, value } = e.target;
        setProductsubColor((prevColor) => ({
          ...prevColor,
          [name]: value
        }));
      };

      const handleSubmit = async (e) => {
        e.preventDefault();
        const er=await ValidateProductSubColor(productsubcolor)
        setError(er);
        if(Object.keys(er).length === 0){
          // console.log("data",productsubcolor)
          const result=await put(`${hostNameHttp}/api/mau-san-pham/cap-nhat`,productsubcolor)
    
          if(result.status==200){
            showPopup("Cập nhật màu sản phẩm thành công","success")
            console.log(result.data)
            actionAfterUpdateProductSubColor(productsubcolor,subColor)
            console.log(result.status)
          }else{
              if(result.status==409){
                showPopup("Cập nhật màu sản phẩm đã tồn tại!","fail")
              }
              else if(result.status==500){
                showPopup("Không thể thực hiện. Hệ thống lỗi!","fail")
              }
              else{
                showPopup("Cập nhật màu sản phẩm không thành công!","fail")
              }
            }
      }
    
      };

    return (
        <div >
            <form onSubmit={handleSubmit}  id="form-add-color">

            <HeaderForm title={"Thêm Màu Sản Phẩm"}></HeaderForm>
      <div className="input_data" style={{display:"flex"}}>
                {/* Search */}
            <div className="" >
               
                <div>
                            Sản phẩm đã chọn: 
                            <div className="product_name">
                              {product.product__Name}
                            </div>
                  </div>
                    <p className="error" style={{marginTop:"5px"}}>{error.ProductSubColor__ProductId}</p>
            </div>
            <div>
                    <div>
                        Chọn màu
                        <div >
                          <div className="select-subcolor" onClick={()=>setIsShowSubcolor(change=>!change)}>Chọn</div>
                          
                          <div className="container-subcolor" style={{display:isShowSubcolor?"block":"none"}}>
                            <div>
                                {/* <div className="select" onClick={()=>setIsShowSubcolor(false)}>ok</div> */}
                                <div className="hide-btn" onClick={()=>setIsShowSubcolor(false)}>Đóng</div>
                            </div>
                            {<ListSubColor data={subColors} action={action}></ListSubColor>}
                          </div>
                        </div>
               </div>
                  <div style={{display:productsubcolor?.productSubColor__SubColorId!=0?"block":"none"}}> 
                        Màu đã chọn:
                        <div  className="subcolor-selected" ref={subcolorselectedref} >
                              <div  style={{backgroundImage:`url(${product.subColorGetDTO.subColor__Image})`}}></div>
                              <div>{product.subColorGetDTO.subColor__Name}</div>
                        </div>
                  </div>
                          <p className="error" style={{marginTop:"5px"}}>{error.ProductSubColor__SubColorId}</p>
            </div>
              <div>
                    <label htmlFor="" >
                        Giá: 
                        <input type="number"  onChange={handleChange} value={productsubcolor?.productSubColor__Price} name="productSubColor__Price"/>
                    </label>
                     <p className="error" style={{marginTop:"5px"}}>{error.productSubColor__Price}</p>
              </div>
            
              <div> 
                    Giảm giá (%):
                    <label htmlFor="">
                        <input type="number" onChange={handleChange} value={productsubcolor?.productSubColor__Discount} name="productSubColor__Discount"/>
                    </label>
                     <p className="error" style={{marginTop:"5px"}}>{error.ProductSubColor__Discount}</p>
              </div>
        </div>
            </form>
        </div>
    )
}
export default ProductSubColorUpdate