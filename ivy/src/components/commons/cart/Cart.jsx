
import { useEffect, useRef, useState } from "react"

import cart from "./Cart.module.css"


function Cart(){

    const sizeRef=useRef(null)
    const [isShow,setIsShow]=useState(true)

    useEffect(()=>{
        const element=sizeRef.current
        if(isShow){
            element.className=cart.listsSize
        }else{
            element.className=`${cart.listsSize} ${cart.open}`
        }
    },[isShow])
    return (
        <>
        <ul className={cart.listsSize} ref={sizeRef}>
                <li>
                <a href=""><p>S</p></a>
                </li>
                <li><a href=""><p>M</p></a></li>
                <li><a href=""><p>L</p></a></li>
                <li><a href=""><p>XL</p></a></li>
                <li><a href=""><p>XXL</p></a></li>
          </ul> 
            <button className={cart.addToCart} onClick={()=>setIsShow(change=>!change)} >
                <div >
                  <i className="bi bi-cart4"></i>
                </div>  
            </button>
            </>
    )
}
export default Cart
 