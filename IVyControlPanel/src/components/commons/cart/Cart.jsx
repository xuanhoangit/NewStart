
import { useEffect, useRef, useState } from "react"

import cart from "./Cart.module.css"
import { useCart } from "../../../context/CartProvider"


function Cart({product}){
    const size=product.sizeDTO;
    // console.log(product)
    const sizeRef=useRef(null)
    const buttonRef=useRef(null)
    const {addToCart}= useCart()
    // console.log(size)
    const [isShow,setIsShow]=useState(false)
    useEffect(() => {
        const handleClickOutside = (event) => {
        if (sizeRef.current && !sizeRef.current.contains(event.target)) {
            setIsShow(false); // Click ngoài => ẩn
        }
        };
    // console.log(isShow)
    document.addEventListener('mousedown', handleClickOutside);
    
    return () => {
      document.removeEventListener('mousedown', handleClickOutside);
    };
  }, []);
    useEffect(()=>{
        const element=sizeRef.current
        if(!isShow){
            element.className=cart.sizes
        }else{
            element.className=`${cart.sizes} ${cart.open}`
        }
    },[isShow])
    return (
        <div style={{position:"relative"}}>
        <ul className={cart.sizes} ref={sizeRef}>
                {size.size__S>0?<li onClick={()=>addToCart({
                    product__Name:product.product__Name,
                    size:"S"
                })} className={cart.active}>
                    <p>S</p>
                </li>:<li style={{color:"rgb(211, 227, 253)",cursor: "not-allowed"}} className="outofstock">
                    <p>S</p>
                </li>
                }
                {size.size__M>0?<li className={cart.active} onClick={()=>alert("hehe")}>
                    <p>M</p>
                </li>:<li style={{color:"rgb(211, 227, 253)",cursor: "not-allowed"}} className="outofstock">
                    <p>M</p>
                </li>
                }
                {size.size__L>0?<li className={cart.active}>
                    <p>L</p>
                </li>:<li style={{color:"rgb(211, 227, 253)",cursor: "not-allowed"}} className="outofstock">
                    <p>L</p>
                </li>
                }
                {size.size__XL>0?<li className={cart.active}>
                    <p>XL</p>
                </li>:<li style={{color:"rgb(211, 227, 253)",cursor: "not-allowed"}} className="outofstock">
                    <p>XL</p>
                </li>
                }
                {size.size__XXl>0?<li className={cart.active}>
                    <p>XXL</p>
                </li>:<li style={{color:"rgb(211, 227, 253)",cursor: "not-allowed"}} className="outofstock">
                    <p>XXL</p>
                </li>
                }
               
          </ul> 
            <button className={cart.addToCart} ref={buttonRef} onClick={()=>setIsShow(change=>!change)} >
                <div >
                  <i className="bi bi-cart4"></i>
                </div>  
            </button>
            </div>
    )
}
export default Cart
 