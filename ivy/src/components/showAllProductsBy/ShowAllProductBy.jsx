import { lazy, useEffect, useRef, useState } from "react"

import "./showAllProductBy.css"
import Breadcrumb from "../commons/breadcrumb/Breadcrumb"
import Size from "../commons/filter/size/Size"
import Color from "../commons/filter/color/Color"
import LazyLoadComponent from "../commons/lazy/LazyLoadComponent"
const Product = lazy(()=>import("../commons/product/Product"))
const products=[
    {
        id:1,name:"Đầm đẹppppppppp ppppppppp ppppppp ppppppp ppppp ppppp  aaa bbbb cccc", price:2800,origin_price:3000, percent_of_discount:50,
        srcs:[
            "https://cotton4u.vn/files/product/thumab/400/2024/04/12/7453f9807d8bf411d9833bb1980993b4.webp",
            "https://cotton4u.vn/files/product/thumab/400/2024/04/12/725aa0022a86342058fe3fb95628e47d.webp"
        ],
        href:"/product-detail"
    },
    {
        id:2,name:"Đầm xấu  aaa a a a a â a a a a â â a  â a a a a a a", price:1300,origin_price:2000, percent_of_discount:50,
        srcs:[
            "https://cotton4u.vn/files/product/thumab/400/2024/04/12/7453f9807d8bf411d9833bb1980993b4.webp",
            "https://cotton4u.vn/files/product/thumab/400/2024/04/12/725aa0022a86342058fe3fb95628e47d.webp"
        ], href:"/product-detail"
    },
    {
        id:3,name:"Đầm xấu", price:1300,origin_price:2000, percent_of_discount:50,
        srcs:[
            "https://cotton4u.vn/files/product/thumab/400/2024/04/12/7453f9807d8bf411d9833bb1980993b4.webp",
            "https://cotton4u.vn/files/product/thumab/400/2024/04/12/725aa0022a86342058fe3fb95628e47d.webp"
        ], href:"/product-detail"
    },
    {
        id:4,name:"Đầm xấu", price:1300,origin_price:2000, percent_of_discount:50,
        srcs:[
            "https://cotton4u.vn/files/product/thumab/400/2024/04/12/7453f9807d8bf411d9833bb1980993b4.webp",
            "https://cotton4u.vn/files/product/thumab/400/2024/04/12/725aa0022a86342058fe3fb95628e47d.webp"
        ], href:"/product-detail"
    },
    {
        id:5,name:"Đầm xấu", price:1300,origin_price:2000, percent_of_discount:50,
        srcs:[
            "https://cotton4u.vn/files/product/thumab/400/2024/04/12/7453f9807d8bf411d9833bb1980993b4.webp",
            "https://cotton4u.vn/files/product/thumab/400/2024/04/12/725aa0022a86342058fe3fb95628e47d.webp"
        ], href:"/product-detail"
    },
    {
        id:6,name:"Đầm xấu", price:1300,origin_price:2000, percent_of_discount:50,
        srcs:[
            "https://cotton4u.vn/files/product/thumab/400/2024/04/12/7453f9807d8bf411d9833bb1980993b4.webp",
            "https://cotton4u.vn/files/product/thumab/400/2024/04/12/725aa0022a86342058fe3fb95628e47d.webp"
        ], href:"/product-detail"
    },
    {
        id:7,name:"Đầm xấu", price:1300,origin_price:2000, percent_of_discount:50,
        srcs:[
            "https://cotton4u.vn/files/product/thumab/400/2024/04/12/7453f9807d8bf411d9833bb1980993b4.webp",
            "https://cotton4u.vn/files/product/thumab/400/2024/04/12/725aa0022a86342058fe3fb95628e47d.webp"
        ], href:"/product-detail"
    },
    {
        id:8,name:"Đầm xấu", price:1300,origin_price:2000, percent_of_discount:50,
        srcs:[
            "https://cotton4u.vn/files/product/thumab/400/2024/04/12/7453f9807d8bf411d9833bb1980993b4.webp",
            "https://cotton4u.vn/files/product/thumab/400/2024/04/12/725aa0022a86342058fe3fb95628e47d.webp"
        ], href:"/product-detail"
    },
]



function SelectPrice(props){
    return (
        <div>
            <label htmlFor={props.type}>
                <div className="title">
                    <input type="radio"  name="range_price" id={props.type} value={props.type}/>
                    <div>{props.title}</div>
                </div>
            </label>
        </div>
    )
}
function Price() {
    const list_prices=[
        {title:"Dưới 0đ - 500.000đ",min:0,max:5e5},
        {title:"Từ 500.000đ - 1.500.000đ",min:5e5,max:1.5e6},
        {title:"Từ 1.500.000đ - 2.500.000đ",min:1.5e6,max:2.5e6},
        {title:"Từ 2.500.000đ - 5.000.000đ",min:2.5e6,max:5e6},
        {title:"Từ 5.000.000đ",min:5e6,max:1e7},
        // {title:"Giá đặc biệt",min:0,max:0},
    ]
    return (
        <>  
            <ul className="list-prices">
               {list_prices.map((price,index)=>(
                    <li key={index}>
                            <SelectPrice title={price.title} type={"typePrice"+index}/>
                    </li>
               ))}
                
            </ul>
        </>
    )
}
function SelectDiscount(props){
    return (
        <div>
            <label htmlFor={props.type}>
                    <div className="title">
                    <input type="radio"  name="range_discount" id={props.type} value={props.type}/>
                    <div >{props.title}</div>
                </div>
            </label>
        </div>
    )
}
function Discount() {
    const list_discounts=[
        {title:"Dưới 30%",min:0,max:30},
        {title:"Từ 30% - 50%",min:30,max:50},
        {title:"Từ 50% - 70%",min:0,max:30},
        {title:"Từ 70%",min:70,max:100},
        {title:"Giá đặc biệt",min:0,max:0},
    ]
    return (
        <>  
            <ul className="list-discounts">
                {list_discounts.map((discount,index)=>(
                    <li key={index}>
                        <SelectDiscount title={discount.title} type={"typeDiscount"+index}/>
                    </li>
                ))}
            </ul>
        </>
    )
}


function Toggle(props) {
    const toggleRef=useRef(null);
    const iconRef=useRef(null)
    const [isShow,setIsShow]=useState(false);
    useEffect(()=>{
        if(isShow){
            toggleRef.current.className="toggle-content open"
            iconRef.current.className="bi bi-dash"
        }else{
            toggleRef.current.className="toggle-content"
            iconRef.current.className="bi bi-plus"
        }
    },[isShow])
    return (
        <>  
            <div className="toggle-title" onClick={()=>setIsShow(change=>!change)}>{props.title}
                <i ref={iconRef}  className="bi bi-plus"></i>
            </div>
            <div ref={toggleRef} className="toggle-content">
                {props.toggle}
            </div>
        </>
    )
}
function Filter(params) {
    const resetRef=useRef(null)
    const resetForm=()=>{
        resetRef.current.querySelectorAll(".active").forEach(x => {
            x.className=""
        });
    }
    return (
        <form id="form_filter" ref={resetRef} action="filte" method="get">
            <div>
                <Toggle title="Size" toggle={<Size setBorderRadius={true} />}/>
           </div>
           <div>
                <Toggle title="Màu" toggle={<Color/>}/>
           </div>
           <div>
                <Toggle title="Mức giá" toggle={<Price/>}/>
           </div>
           <div>
                <Toggle title="Mức chiết khấu" toggle={<Discount/>}/>
           </div>
           <div style={{display:"flex",justifyContent:"space-evenly",marginTop:"20px"}}>
                <input type="reset" onClick={resetForm} value="BỎ LỌC" />
                <input type="submit" value="LỌC" />
           </div>
        </form>
    )
}
function ShowAllProductBy(props) {
    const listOrderRef=useRef(null);
    const iconRef=useRef(null)
    const orderByRef=useRef(null)
    const ReName=(event)=>{
        orderByRef.current.textContent=event.target.textContent
    }
    const [isShow,setIsShow]=useState(true);
    useEffect(()=>{
        // console.log(isShow)
        if(isShow){
            listOrderRef.current.className="list-ordersby"
            iconRef.current.style.transform="rotate(0)"
        }else{
            listOrderRef.current.className="list-ordersby open"
            iconRef.current.style.transform="rotate(180deg)"
        }
    },[isShow])
    return (
        <>
        <div>
                <Breadcrumb></Breadcrumb>
            <div className="list-product-page">
                <Filter/>
                <section >
                    <div className="header-product-page">
                        <h2 className="title">Tiêu đề</h2>
                        <div className="order-by">
                            <div onClick={()=>setIsShow(change=>!change)} className="title"><span ref={orderByRef}>Sắp xếp theo</span> <i id="iconRotate" ref={iconRef} className="fa-solid fa-angle-down"></i></div>
                            <ul className="list-ordersby" ref={listOrderRef}>
                                <li onClick={ReName}>Mặc định</li>
                                <li onClick={ReName}>Mới nhất</li>
                                <li onClick={ReName}>Được mua nhiều nhất</li>
                                <li onClick={ReName}>Được yêu thích nhất</li>
                                <li onClick={ReName}>Giá cao đến thấp</li>
                                <li onClick={ReName}>Giá thấp đến cao</li>
                            </ul>
                        </div>
                    </div>
                    <LazyLoadComponent>
                    <div className="banner-page">
                        <img src="https://pubcdn.ivymoda.com/files/news/2024/09/09/web-LB%20copy.jpg" alt="" style={{width:"100%"}}/>
                    </div>
                    <div className="container_list-products">
                        
                            {products.map((product,index)=>(
                                <div  key={index} className="product">
                                    <LazyLoadComponent>
                                        <Product product={product}/>
                                    </LazyLoadComponent>
                                </div>
                            ))}
                        
                    </div>
                    </LazyLoadComponent>
                </section>
            </div>
        </div>
        </>
    )
}
export default ShowAllProductBy