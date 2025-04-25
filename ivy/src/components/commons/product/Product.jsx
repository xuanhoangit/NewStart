import { Link } from "react-router-dom"
import { Suspense } from "react"

import Cart from "../cart/Cart"
import ConvertToVND from "../ConvertToVND"
import Discount from "../Discount"

import product from "./Product.module.css"
import LazyImg from "./LazyImg"
import "./product.css"
function Product(props) {
    return (
        <div className={product.product}>
            <Discount discount="-50"></Discount>
            <div className="productImage">
                <Link to={props.product.href}>
                    <Suspense>
                        <LazyImg  alt={"Seashell Dress - Đầm cổ vuông phối bèo"}  src={props.product.srcs[0]} />
                    </Suspense >
                    <Suspense>
                        <LazyImg  alt={"Seashell Dress - Đầm cổ vuông phối bèo"} className={"hoverImg"}  src={props.product.srcs[1]}/>
                    </Suspense>
                </Link>
            </div>
            <div className={product.cnf}>
                {/* listCOlor */}
                <ul>
                    <li></li>
                </ul>
                {/* favorite */}
                <div className={product.favorite}><i className="bi bi-suit-heart"></i></div>
            </div>
            <div className={product.productInfo}>
                <p className={product.productName}>{props.product.name}</p>
                <div className={product.salef}>
                    <div className={product.productPrice}>
                        <div className={product.sale}>{ConvertToVND(props.product.price)}đ</div>
                        <div className={product.originPrice}>{ConvertToVND(props.product.origin_price)}đ</div>
                    </div>
                <Cart></Cart>
                </div>
            </div>
        </div>
    )
}
export default Product