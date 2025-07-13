import { useEffect, useRef, useState,Suspense } from "react"
import { useNavigate } from "react-router-dom";
import { hostNameHttp } from "../../commons/HostName";
import "./Add.css"

import post from "../../PostData";


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
  
 

function ProductSubColorAdd({showPopup,product,actionAfterAddProductSubColor}) {
    const [error,setError]=useState({});
    console.log(product.product__Id)
    const subColors= useFetch(`${hostNameHttp}/api/mau`).data;
    const subcolorselectedref=useRef(null);
    const [isShowSubcolor,setIsShowSubcolor]=useState(false);
        const  [productsubcolor,setProductsubColor]=useState({
      ProductSubColor__Price:0,
      ProductSubColor__Discount:0,
      ProductSubColor__ProductId: product.product__Id,
      ProductSubColor__SubColorId: 0,
    })
    //PRoduct
    // const [product_Name, setProductName] = useState(<i className="bi bi-three-dots"></i>);
    // const [text,setText]=useState("");
    // const [productSearchResults,setProductSearchResults]=useState(null);
    // const [isShowResult,setIsShowResult]=useState(false);
    const searchResultRef=useRef(null);
    useEffect(() => {
      // const handleClickOutside = (event) => {
      //   if (searchResultRef.current && !searchResultRef.current.contains(event.target)) {
      //     setIsShowResult(false); // Click ngoài => ẩn
      //   }
      // };
      // // console.log(isShow)
      // document.addEventListener('mousedown', handleClickOutside);
      
      // return () => {
      //   document.removeEventListener('mousedown', handleClickOutside);
      // };
      setProductsubColor({
      ProductSubColor__Price:0,
      ProductSubColor__Discount:0,
      ProductSubColor__ProductId: product.product__Id,
      ProductSubColor__SubColorId: 0,
    })
    }, [product.product__Id]);
   
  //   const searchProductResult=async (searchString)=>{
  //     setText(searchString)
  //     const result= await get(`${hostNameHttp}/api/san-pham/tim-kiem?text=${searchString}`)
  //       setProductSearchResults(result);    
  // };
  // const setProductSelected=(value,content)=>{
  //   if(value!==0){
  //     //Kiểm tra xem có product hay chưa 
  //     productsubcolor.ProductSubColor__ProductId=value;
  //     setProductName(content);
  //     setText(content)
  //     setIsShowResult(false)
  //     console.log(productsubcolor.ProductSubColor__ProductId)
  //     }
  //   }

    
    //

    const action=(isShow,item)=>{
      setIsShowSubcolor(isShow);
      productsubcolor.ProductSubColor__SubColorId=item.subColor__Id
      // console.log("hhehe",subcolor_Id)
      subcolorselectedref.current.querySelector(":first-child").style.backgroundImage=`url(${item.subColor__Image})`;
      subcolorselectedref.current.querySelector(":last-child").innerText=item.subColor__Name;
    }




    useEffect(()=>{
      console.log(productsubcolor)
    },[productsubcolor])

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
          const result=await post(`${hostNameHttp}/api/mau-san-pham/them-moi`,productsubcolor)
    
          if(result.status==200||result.status==201){
            console.log("sc",result.data)
            showPopup("Thêm màu sản phẩm thành công","success")
            actionAfterAddProductSubColor(result.data)
            console.log(result.status)
            
          }else{
              if(result.status==409){
                showPopup("Màu sản phẩm đã tồn tại!","fail")
              }
              else if(result.status==500){
                showPopup("Không thể thực hiện. Hệ thống lỗi!","fail")
              }
              else{
                showPopup("Thêm màu sản phẩm không thành công!","fail")
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
                {/* <div className="search-products">
                      
                      <label htmlFor="search">
                          <div style={{display:"flex",justifyContent:"space-between"}}>
                              Chọn sản phẩm <i className="bi bi-search"></i>
                          </div>
                          <input autoComplete="off" placeholder="tên sản phẩm" type="search" value={text} id="search" onChange={(event)=>{searchProductResult(event.target.value);setIsShowResult(true)}}/>
                      </label>
                      
                      {isShowResult?
                      <Suspense fallback={<i className="fa-brands fa-slack fa-spin"></i>}>
                      <ul className="search-result" ref={searchResultRef}>
                          {(productSearchResults==null||productSearchResults.length==0)
                          ? text!=""?<li>Không có kết quả</li>:setIsShowResult(false)
                          :productSearchResults?.map((product,index)=>(
                              <li value={product.product__Id} onClick={(event)=>setProductSelected(event.target.value,product.product__Name)} key={index}
                              >{product.product__Name}</li>
                          ))}
                      </ul></Suspense>:""}
                </div> */}
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
                  <div style={{display:productsubcolor.ProductSubColor__SubColorId!=0?"block":"none"}}> 
                        Màu đã chọn:
                        <div  className="subcolor-selected" ref={subcolorselectedref} >
                              <div ></div>
                              <div></div>
                        </div>
                  </div>
                          <p className="error" style={{marginTop:"5px"}}>{error.ProductSubColor__SubColorId}</p>
            </div>
              <div>
                    <label htmlFor="" >
                        Giá: 
                        <input type="number"  onChange={handleChange} value={productsubcolor.ProductSubColor__Price} name="ProductSubColor__Price"/>
                    </label>
                     <p className="error" style={{marginTop:"5px"}}>{error.ProductSubColor__Price}</p>
              </div>
            
              <div> 
                    Giảm giá (%):
                    <label htmlFor="">
                        <input type="number" onChange={handleChange} value={productsubcolor.ProductSubColor__Discount} name="ProductSubColor__Discount"/>
                    </label>
                     <p className="error" style={{marginTop:"5px"}}>{error.ProductSubColor__Discount}</p>
              </div>
        </div>
            </form>
        </div>
    )
}
export default ProductSubColorAdd