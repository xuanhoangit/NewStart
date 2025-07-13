import { Link } from "react-router-dom"
import { Suspense, useRef, useState,useEffect } from "react"

import Cart from "../cart/Cart"
import ConvertToVND from "../ConvertToVND"
import Discount from "../Discount"

import productModule from "./Product.module.css"
import LazyImg from "./LazyImg"
import "./product.css"
import Upload from "../../Products/Upload/Upload"
import ProductSubColorAdd from "../../Products/ProductSubColor/Add"
import SizeUpdate from "../../Products/Size/SizeUpdate"
import ProductSubColorUpdate from "../../Products/ProductSubColor/Update"
import { hostName } from "../HostName"
import patch from "../../PatchData"
import api from "../../../AxiosInstance"
import RequireRole from "../../../RequireRole"
import { staff } from "../../Roles"
// import Update from "../../Products/Add/Update"
function getRandomInt(number) {
  return Math.floor(Math.random() * number);
}

function Product({product,setAction,showPopup,setProducts}) {
    const [isShowAction,setIsShowAction]=useState(false);
    const nullValue="Chưa thêm màu"
    // console.log("0",product.productSubColorGetHomeShowDTOs)
    // console.log("p",product)
    const [index,setIndex]=useState(getRandomInt(product.productSubColorGetHomeShowDTOs.length));
    // const [productSubColorGetHomeShowDTOs,setProductSubColorGetHomeShowDTOs]=useState(product.productSubColorGetHomeShowDTOs.length==0?null:product.productSubColorGetHomeShowDTOs[index]);
    // console.log("p",product.productSubColorGetHomeShowDTOs)
    const removeProduct = async ()=>{
        const result=await api.patch ('/api/san-pham/`+product.product__Id');
        console.log(result.status)
         if (result.status==200) { // giả sử API trả về result.success = true khi xóa thành côngsubColorGetDTO

            showPopup("Đã xóa sản phẩm: "+product.product__Name,"success")
            setProducts(prevItems => prevItems.filter(item => item.product__Id !== product.product__Id))
        } else {
            showPopup("Không thể xóa","fail")
        }
        setIsShowAction(false)
    }
    const removeProductSubColor = async (id)=>{
        const result=await api.patch (`/api/mau-san-pham/`+id)
        // console.log(product.productSubColorGetHomeShowDTOs[index])
        if (result.status==200) { // giả sử API trả về result.success = true khi xóa thành côngsubColorGetDTO
            
            console.log("psc",product.productSubColorGetHomeShowDTOs[index])
            showPopup("Đã xóa: "+product.product__Name+"/"+product.productSubColorGetHomeShowDTOs[index]?.subColorGetDTO.subColor__Name,"success")
            product.productSubColorGetHomeShowDTOs=product.productSubColorGetHomeShowDTOs.filter(item => item.productSubColor__Id != id)
            setIndex(0)
        } else {
            showPopup("Không thể xóa","fail")
        }
        setIsShowAction(false)
        //  console.log(result)
         
    }
    useEffect(()=>{
        // setProductSubColorGetHomeShowDTOs(product.productSubColorGetHomeShowDTOs.length==0?null:product.productSubColorGetHomeShowDTOs[index])
        // console.log("aaaa",productSubColorGetHomeShowDTOs)
    },[index])

    const actionAfterAddProductSubColor=(psc)=>{
        product.productSubColorGetHomeShowDTOs.push(psc)
        setIndex(product.productSubColorGetHomeShowDTOs.length-1)
        // product.productSubColorGetHomeShowDTOs.length==0?null:product.productSubColorGetHomeShowDTOs[index]
        console.log("adawwe",product.productSubColorGetHomeShowDTOs)
    } 
    const setSize=(size)=>{
        product.productSubColorGetHomeShowDTOs[index].sizeDTO=size
    }
    const actionAfterUpdateFile=(images)=>{
        product.productSubColorGetHomeShowDTOs[index].productSubColorFileGetFileDTOs=images
    }
    const actionAfterUpdateProductSubColor=(pscolor,sc)=>{
        product.productSubColorGetHomeShowDTOs[index].subColorGetDTO=sc;
        product.productSubColorGetHomeShowDTOs[index]. productSubColor__CreateAt= pscolor.productSubColor__CreateAt,
        product.productSubColorGetHomeShowDTOs[index].   productSubColor__Discount= pscolor.productSubColor__Discount,
        product.productSubColorGetHomeShowDTOs[index].  productSubColor__Id= pscolor.productSubColor__Id,
        product.productSubColorGetHomeShowDTOs[index].   productSubColor__Price= pscolor.productSubColor__Price,
        product.productSubColorGetHomeShowDTOs[index].   productSubColor__Status= pscolor.productSubColor__Status,
        product.productSubColorGetHomeShowDTOs[index].   productSubColor__ProductId=  pscolor.productSubColor__ProductId,
        product.productSubColorGetHomeShowDTOs[index].   productSubColor__OutfitKey= pscolor.productSubColor__OutfitKey,
        product.productSubColorGetHomeShowDTOs[index].   productSubColor__UpdateAt=  pscolor.productSubColor__UpdateAt,
        product.productSubColorGetHomeShowDTOs[index].  productSubColor__SubColorId= sc.subColor__Id
    }
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
    return (
        <div className={productModule.product}>
            {
            product.productSubColorGetHomeShowDTOs[index]?.productSubColor__Discount>0?
            <Discount discount={product.productSubColorGetHomeShowDTOs[index].productSubColor__Discount}></Discount> :
            ""
            }
             {product.productSubColorGetHomeShowDTOs[index]?.productSubColorFileGetFileDTOs!=null&&product.productSubColorGetHomeShowDTOs[index]?.productSubColorFileGetFileDTOs?.length!=0?  
            <div className="productImage" style={{aspectRatio: "2/3",overflow:"hidden"}}>
                <Link to={product.href}>
                    <Suspense>
                        <LazyImg style={{objectFit:"contain"}} alt={"Seashell Dress - Đầm cổ vuông phối bèo"}  src={product.productSubColorGetHomeShowDTOs[index].productSubColorFileGetFileDTOs[0].productSubColorFile__Name} />
                    </Suspense >
                    <Suspense>
                        <LazyImg  style={{objectFit:"contain"}} alt={"Seashell Dress - Đầm cổ vuông phối bèo"} className={"hoverImg"} src={product.productSubColorGetHomeShowDTOs[index].productSubColorFileGetFileDTOs[1]?.productSubColorFile__Name}/>
                    </Suspense>
                </Link>
            </div>:
            
            <div  onClick={()=>
                setAction(product.productSubColorGetHomeShowDTOs[index]!=null?
                 <Upload showPopup={showPopup} product={{
                                product__Name:product.product__Name,
                                product__Id:product.product__Id,
                                productSubColor__Id:product.productSubColorGetHomeShowDTOs[index].productSubColor__Id,
                                subColorGetDTO:product.productSubColorGetHomeShowDTOs[index]?.subColorGetDTO
                            }} actionAfterUpdateFile={actionAfterUpdateFile}></Upload> 
                :  
                <ProductSubColorAdd showPopup={showPopup} product={{
                                product__Name:product.product__Name,
                                product__Id:product.product__Id,
                            }} actionAfterAddProductSubColor={actionAfterAddProductSubColor}></ProductSubColorAdd>
                    ,true)} style={{cursor:"pointer",textAlign:"center",alignContent:"center",textDecoration:"underline",padding:"5px",color:"blue",fontWeight:"bold",aspectRatio: "2/3",border:"1px solid white",textTransform:"uppercase"}}>
                {product.productSubColorGetHomeShowDTOs[index]!=null?"Thêm hình ảnh":"Thêm màu sản phẩm"}
            </div>
            } 
            {product.productSubColorGetHomeShowDTOs[index]?.productSubColor__Status==2?
            <p style={{position:"absolute",top:"5px",left:"5px",backgroundColor:"rgb(211, 227, 253)",color:"blue",padding:"2px 3px",borderRadius:"4px"}}>Chưa phát hành</p>:
            <p style={{position:"absolute",top:"5px",left:"5px",color:"green",backgroundColor:"rgb(211, 227, 253)",padding:"2px 3px",borderRadius:"4px"}}>Đang bán</p>}
            <div className={productModule.productInfo}>
           
                <div className="xxxxxxxxx">
                    <ul  className="sssssss">
                        {product.productSubColorGetHomeShowDTOs?.map((c,i)=>(
                          
                            <li title={c.subColorGetDTO?.subColor__Name} key={i} onClick={()=>{
                               if(i!=index){
                                    // setProductSubColorGetHomeShowDTOs(c);
                                    setIndex(i)
                               }
                            }}> 
                                <div className={index==i?"active":"unactive"}><i className="bi bi-check-lg"></i></div>
                                {/* index==i?productSubColorGetHomeShowDTOs?.subColorGetDTO?.subColor__Image: */}
                                <img src={c.subColorGetDTO?.subColor__Image} alt="" />
                            </li>
                        ))}
                        <li>
                           
                        </li>
                    </ul>
                    <RequireRole allowedRoles={['Quản lý sản phẩm']}>
                    <div ref={actionRef} className="action">
                        <div  onClick={()=>setIsShowAction(change=>!change)}>
                            <i className="bi bi-three-dots"></i>
                        </div>
                        <ul style={{display:isShowAction?"block":"none"}}>
                            {/* <li onClick={()=>setAction(<Update product={{
                                product__Id:product__Id,
                                product__Name:product__Name
                            }} showPopup={showPopup}></Update>)}><i  className="bi bi-pencil-square"></i> Chỉnh sửa sản phẩm</li> */}
                            <li onClick={()=>{removeProduct()}}><i style={{color:"red"}} className="bi bi-trash-fill"></i> Xóa sản phẩm</li>
                            <li onClick={()=>setAction(<ProductSubColorAdd showPopup={showPopup} product={{
                                product__Name:product.product__Name,
                                product__Id:product.product__Id,
                            }} actionAfterAddProductSubColor={actionAfterAddProductSubColor}></ProductSubColorAdd>,true)}><i className="bi bi-plus-lg"></i> Thêm màu</li>
                            {product.productSubColorGetHomeShowDTOs[index]!=null?<>
                            <li onClick={()=>removeProductSubColor(product.productSubColorGetHomeShowDTOs[index]?.productSubColor__Id)}><i style={{color:"red"}} className="bi bi-trash-fill"></i> Xóa màu</li>
                            <li  onClick={()=>setAction(<ProductSubColorUpdate
                            showPopup={showPopup} product={{
                                product__Name:product.product__Name,
                                product__Id:product.product__Id,
                                productSubColor:{
                                        productSubColor__CreateAt : product.productSubColorGetHomeShowDTOs[index]?. productSubColor__CreateAt,
                                        productSubColor__Discount : product.productSubColorGetHomeShowDTOs[index]?.   productSubColor__Discount,
                                        productSubColor__Id : product.productSubColorGetHomeShowDTOs[index]?.  productSubColor__Id ,
                                        productSubColor__Price : product.productSubColorGetHomeShowDTOs[index]?.   productSubColor__Price,
                                        productSubColor__Status : product.productSubColorGetHomeShowDTOs[index]?.   productSubColor__Status,
                                        productSubColor__ProductId : product.productSubColorGetHomeShowDTOs[index]?.   productSubColor__ProductId,
                                        productSubColor__OutfitKey : product.productSubColorGetHomeShowDTOs[index]?.   productSubColor__OutfitKey,
                                        productSubColor__UpdateAt : product.productSubColorGetHomeShowDTOs[index]?.   productSubColor__UpdateAt,
                                        productSubColor__SubColorId : product.productSubColorGetHomeShowDTOs[index]?.  subColorGetDTO?.subColor__Id,
                                },
                                subColorGetDTO:product.productSubColorGetHomeShowDTOs[index].subColorGetDTO
                            }} actionAfterUpdateProductSubColor={actionAfterUpdateProductSubColor}></ProductSubColorUpdate>,true)}><i  style={{color:"blue"}} className="bi bi-pencil-square"></i> Chỉnh sửa màu</li>
                            
                            <li onClick={()=>setAction(<Upload showPopup={showPopup} product={{
                                product__Name:product.product__Name,
                                product__Id:product.product__Id,
                                productSubColor__Id:product.productSubColorGetHomeShowDTOs[index].productSubColor__Id,
                                subColorGetDTO:product.productSubColorGetHomeShowDTOs[index]?.subColorGetDTO
                            }} actionAfterUpdateFile={actionAfterUpdateFile}></Upload>,true)}><i style={{color:"green"}} className="bi bi-file-earmark-image"></i>Cập nhật hình ảnh</li>
                            <li onClick={()=>setAction(
                                <SizeUpdate showPopup={showPopup} product={{
                                    product__Name:product.product__Name,
                                    product__Id:product.product__Id,
                                    productSubColor__Id:product.productSubColorGetHomeShowDTOs[index].productSubColor__Id,
                                    subColorGetDTO:product.productSubColorGetHomeShowDTOs[index]?.subColorGetDTO,
                                    size:product.productSubColorGetHomeShowDTOs[index]?.sizeDTO
                                }} updateSize={setSize}></SizeUpdate>,true
                            )}><i className="bi bi-123"></i>Cập nhật số lượng</li>
                            </>:null}
                            </ul>
                    </div>
                    </RequireRole>
                </div>
                <p title={product.product__Name} className={productModule.productName}>{product.product__Name}</p>
                {product.productSubColorGetHomeShowDTOs[index]!=null?
                <div className={productModule.salef}>
                    <div className={productModule.productPrice}>
                        <div className={productModule.sale}>{
                            ConvertToVND(product.productSubColorGetHomeShowDTOs[index]?.productSubColor__Price-
                                product.productSubColorGetHomeShowDTOs[index]?.productSubColor__Discount/100
                                *product.productSubColorGetHomeShowDTOs[index]?.productSubColor__Price)}đ
                        </div>
                        {product.productSubColorGetHomeShowDTOs[index]?.productSubColor__Discount>0? <del className={productModule.originPrice}>{ ConvertToVND(product.productSubColorGetHomeShowDTOs[index]?.productSubColor__Price)}đ</del>:""
                        }
                    </div>
                    {product.productSubColorGetHomeShowDTOs[index]?.sizeDTO!=undefined?<RequireRole allowedRoles={staff}><Cart size={product.productSubColorGetHomeShowDTOs[index]?.sizeDTO}></Cart></RequireRole>:""}
                </div>:<div style={{bottom:0}} className={productModule.salef}>{nullValue}</div>
                }
            </div>
        </div>
    )
}
export default Product