import { lazy, Suspense } from "react"

import Product_detail_info from "./Product_detail_info";
import Breadcrumb from "../commons/breadcrumb/Breadcrumb";

const Slide =lazy(()=>import("./Slide"));

import "./productDetail.css"
import LazyLoadComponent from "../commons/lazy/LazyLoadComponent";



function ProductDetail(params) {
    return (
        <>
        <Breadcrumb></Breadcrumb>
        {/* <LazyLoadComponent>   */}
        <section className="product-detail">
                <Suspense>
                    <Slide></Slide>
                </Suspense>
                
            <div className="product-detail_info">
                <Product_detail_info></Product_detail_info>
            </div>
            </section>
            {/* </LazyLoadComponent> */}
        </>
    )
}
export default ProductDetail