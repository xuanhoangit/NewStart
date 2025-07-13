import { useEffect, useRef, useState } from "react"
import Slider from "react-slick";
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";
import RequireRole from "../../../RequireRole";
import "./index.css"

import Add from "../Add/Add"
import ProductSubColorAdd from "../ProductSubColor/Add";
import FancyButton from "../../commons/FancyButton/FancyButton";
import SubColorAdd from "../SubColor/Add/Add";

import useFetch from "../../UseFetch";
import get from "../../GetData";
import { hostNameHttp } from "../../commons/HostName";
import Product from "../../commons/product/Product";
import { useAuth } from "../../../AuthProvider";

const toQueryString = (obj, prefix = '') => {
    const query = [];

    for (const key in obj) {
        if (obj[key] === null || obj[key] === undefined || obj[key] === '') continue;

        const value = obj[key];
        const prefixedKey = prefix ? `${prefix}.${key}` : key;

        if (Array.isArray(value)) {
            value.forEach(v => query.push(`${encodeURIComponent(prefixedKey)}=${encodeURIComponent(v)}`));
        } else if (typeof value === 'object') {
            query.push(toQueryString(value, prefixedKey));
        } else {
            query.push(`${encodeURIComponent(prefixedKey)}=${encodeURIComponent(value)}`);
        }
    }

    return query.length ? '?' + query.join('&') : '';
};

export default function HomeProduct({ showPopup }){
    const {user}=useAuth();
    const [isShow,setIsShow]=useState(false);
    const [content,SetContent]=useState()
    const [products,setProducts]=useState([])
    const [pages,setPages]=useState([]);
    const [currentPage,setCurrentPage]=useState(1)
    const sliderRef=useRef(null);
    const [missingProducts,setMissingProducts]=useState();
    const [loadding,setLoadding]=useState(true)
    const [productFilter,setProductFilter]=useState(
        {
                SearchString: null,
                FromDateTo: {
                    From: null,
                    To: null
                },
                ColorIds: [],
                RangePrice: {
                    MinPrice: null,
                    MaxPrice: null
                },
                OrderByDatetime: null,
                SubCategory__Id: null,
                Page:currentPage,
                roleRequest:user.roles[0]
        }
    )
    const activeAfterAddProduct=(p)=>{
        setProducts((prev)=>[p,...prev])
    }
    const PrevButton=({style,className })=>{
        return productFilter.Page!=1?(
            <button href="#gotoheader" style={{position:"absolute",left:"0",top:"0",transform:"translateX(-100%)"}} className={`page prev`} onClick={(e)=>{if(pages.length>4){sliderRef.current.slickGoTo(productFilter.Page-2)} ;setProductFilter(prev => ({
                            ...prev,
                            Page: productFilter.Page-1
                        }))}}><i className="bi bi-caret-left"></i></button>
        ):null
    }
    const NextButton=({onClick})=>{
        return productFilter.Page<pages.length?(
            <button href="#gotoheader" style={{position:"absolute",top:"0",right:"0",transform:"translateX(100%)"}}  className={` page next`} onClick={()=>{if(pages.length>4)sliderRef.current.slickGoTo(productFilter.Page+1) ;setProductFilter(prev => ({
                            ...prev,
                            Page: productFilter.Page+1
                        }))}}><i className="bi bi-caret-right"></i></button>
        ):null
    }
        const goToSlide = (page) => {
            setProductFilter(prev => ({
                                ...prev,
                                Page: page
                            }))
            sliderRef.current.slickGoTo(page-1);
        };
     const settings = {
        dots: false,
        infinite: false,
        speed: 500,
        slidesToShow: 4,
        slidesToScroll: 1,
        arrows: true, // hiện nút prev/next
        prevArrow: <PrevButton></PrevButton>    ,
        nextArrow:<NextButton></NextButton>
    };
    const setAction=(upload,isShow)=>{
        SetContent(upload);
        setIsShow(isShow)
    }

        
         const filterProducts=async ()=>{

            const query = toQueryString(productFilter);
            const result=await get(`${hostNameHttp}/api/san-pham/loc${query}`);
            
           if(result.status==200){
                setProducts(result.data.products);
            }else{
                setProducts([]);
            }
           
            const missing=4-(result.data.products.length%4)
            //set elenment còn thiếu cho đủ 1 hàng 4 sản phẩm
                const elements=[]
                for (let index = 0; index < missing; index++) {
                    elements.push(<div key={index}></div>);
                    // console.log(elements)
                }
                setMissingProducts(elements)
                //end
            // console.log(missingProducts)
         
           const totalPages = Math.ceil(result.data.count / 16);
        //    console.log("number item",result.count)
             for (let index = 1; index <= totalPages; index++) {
                    if(pages.length<totalPages){
                        pages.push(index)
                    }
                }
             
       }
 
    useEffect(()=>{
        setProducts([])
        filterProducts()
             
                setLoadding(false)
             
    },[productFilter])
    return (
        <>
        <div className="product-page" id="gotoheader">
            <div className="product-functions">
                <h2>Quản lý sản phẩm</h2>
                <RequireRole allowedRoles={['Quản lý sản phẩm']}>
                    <FancyButton onClick={()=>{ SetContent(<Add showPopup={showPopup} setProducts={activeAfterAddProduct}></Add>); setIsShow(true)}}>
                    Tạo sản phẩm</FancyButton>
                    <FancyButton onClick={()=>{SetContent(<SubColorAdd showPopup={showPopup}></SubColorAdd>); setIsShow(true)}}>
                    Tạo màu</FancyButton>
                </RequireRole>
                    {/* <button onClick={async ()=>await fetProducts()}>Refresh</button> */}
                    {/* <FancyButton onClick={()=>{SetContent(<Upload showPopup={showPopup}></Upload>);setIsShow(true)}}>
                    Hình ảnh sản phẩm
                    </FancyButton> */}

                
            </div>
            {loadding===true?<div className="productloader"></div>:
            <div className="product-content">
                <div className="content" >
                    <div style={{display:isShow?"block":"none"}}>
                        <button title='Ẩn' onClick={()=>setIsShow(false)} className='hide'><i className="bi bi-x-lg"></i></button>
                        {content}
                    </div>
                </div>
                <div style={{color:"white"}} className="container-products">
                    {   
                        <>
                        {
                            products?.map((product,i)=>(
                        
                            <Product key={i} showPopup={showPopup} setProducts={setProducts} setAction={setAction} product={product}></Product>
                            // <div key={i}>{product.product__Name}</div>
                        ))
                        }
                        {missingProducts}
                        </>
                        
                    }
                </div>
            </div>}
        </div>
        <div style={{width:"100%"}}>
            <div className="paging">
                {productFilter.Page!=pages[0] &&productFilter.Page>4?
                   <>
                        <button className="first" href="#gotoheader" onClick={()=>goToSlide(pages[0])}>Trang đầu</button>
                    
                   </>:""
                }
                {pages.length>4?
                    <Slider {...settings} ref={sliderRef} style={{width:"200px",position:"relative"}}>
                        {pages?.map((page) => (
                        
                            <button  key={page}
                            href="#gotoheader"
                            onClick={() => {
                                if (productFilter.Page !== page) {
                                setProductFilter((prev) => ({
                                    ...prev,
                                    Page: page,
                                }));
                                }
                            }}
                            className={
                                productFilter.Page === page ? "page active" : "page"
                            }
                           
                            >
                            {page}
                            </button>
                        ))}
                    </Slider>:
                    <div style={{position:"relative"}}>
                        <PrevButton></PrevButton>
                        {pages?.map((page) => (
                        
                            <button  key={page}
                            href="#gotoheader"
                            onClick={() => {
                                if (productFilter.Page !== page) {
                                setProductFilter((prev) => ({
                                    ...prev,
                                    Page: page,
                                }));
                                }
                            }}
                            className={
                                productFilter.Page === page ? "page active" : "page"
                            }
                           
                            >
                            {page}
                            </button>
                        ))}
                        <NextButton></NextButton>
                        </div>}
                
                {pages.length>4 && productFilter.Page<pages[pages.length-1]?
                   <>
                       
                        <button href="#gotoheader" className="last" onClick={()=>goToSlide(pages[pages.length-1])}>Trang cuối</button>
                   </>:""
                }
                
            </div>
        </div>
        </>
    )
}