import { lazy, useEffect, useState } from "react";


import Slider from "react-slick";
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";

import NextArrow from "../../commons/buttonNext_Prev/NextArrow";
import PrevArrow from "../../commons/buttonNext_Prev/PrevArrow";
import MainButton from "../../commons/mainButton/MainButton";

import Product from "../../commons/product/Product"

import dp from "./ProductDisplayEvent.module.css"
import  "./ProductDisplayEventOverride.css"

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

function ProductDisplay(props) {

    const settings = {
        speed: 500,         
        slidesToShow: 5,     
        slidesToScroll: 1,   
        infinite:false,
        prevArrow:<PrevArrow />,
        nextArrow:<NextArrow />,
        responsive: [
            {
              breakpoint: 600, 
              settings: {
                slidesToShow: 2, 
                slidesToScroll: 1, 
              }
            }
          ]
      };
    

    
    return (
        <section>
            <div className={dp.titleSection}>{props.titleSection}</div>
            <div className={dp.productsContainer}>
                    <div className={dp.slidesContainer} id="proc">
                    <Slider {...settings}>
                        {products.map((product,index)=>(
                        
                            <div key={index}>
                                    <Product product={product}></Product>
                            </div>
                        
                        ))}
                    </Slider>
                </div>
            </div>
            <div style={{textAlign:"center"}}>
                <MainButton text="Xem tất cả"></MainButton>
            </div>
        </section>
    )
}
export default ProductDisplay