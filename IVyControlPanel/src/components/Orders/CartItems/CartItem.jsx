import { Link } from "react-router-dom"
import ConvertToVND from "../../commons/ConvertToVND"
import "./CartItem.css"
import { useState ,useRef} from "react"
import { useEffect } from "react"
import FancyButton from "../../commons/FancyButton/FancyButton"



function QuantityItem(props) {
    return (

            <div className="cart_quantity-item">
                {props.quantity}
            </div>
    )
}

function CartItem({data=[]}) {
    const [isShow,setIsShow]=useState(false)
    const [items,setItems]=useState(data)
    // const [items,setItems]=useState([
    //     {   
    //         src:"https://pubcdn.ivymoda.com/files/product/thumab/400/2024/09/06/eea027c45f60f52707a3e59eaf98e5ed.webp",
    //         name:"Áo sơ mi Tencel Blossom",
    //         color:"Be",
    //         size:"M",
    //         price:1190000
    //     },
    //     {   
    //         src:"https://pubcdn.ivymoda.com/files/product/thumab/400/2024/09/06/eea027c45f60f52707a3e59eaf98e5ed.webp",
    //         name:"Áo sơ mi Burning out",
    //         color:"Be",
    //         size:"M",
    //         price:210000
    //     }
    // ])
    const cartPreviewRef=useRef(null)
    const totalPrice = items?.reduce((total, item) => total + item.price, 0);
    useEffect(()=>{
        if(isShow){
            cartPreviewRef.current.className ="cart-preview show"
        }else{
            cartPreviewRef.current.className = "cart-preview "
        }
    },[isShow])
    function Item(props) {
        return (
            <>  
                <img className="item-img" style={{width:"100px",aspectRatio:"3/4",objectFit:"cover"}} src={props.data.src} alt="" />
                <div className="item-info">
                    <div className="item-name">{props.data.name}</div>
                    <div>
                        <div className="item-color"><span className="key">Màu sắc: </span><span className="value">{props.data.color}</span></div>
                        <div className="item-size"><span className="key">Size:</span><span className="value">{props.data.size}</span></div>
                    </div>
                    <div className="item-bottom">
                        <div className="item-cartItem">
                            <div className="remove" onClick={() => setItems([...items?.slice(0, props.id), ...items.slice(props.id+1)])}>-</div>
                            <input type="number" name="quantity" defaultValue={1} />
                            <div className="add" onClick={()=>setItems([...items, props.data])}>+</div>
                        </div>
                        <div className="item-price">{ConvertToVND(props.data.price)}đ</div>
                    </div>
                </div>
            </>
        )
    }
    function PreviewCart(params) {
        return (
            <>
                <div className="cart-title">Giỏ hàng 
                    <span> </span> 
                    <QuantityItem quantity={items?.length}></QuantityItem>
                    <div className="hide" onClick={()=>setIsShow(change=>!change)}>
                        <i className="bi bi-x-lg"></i>
                    </div>
                </div>
                 {items?.length>0?
                 <>
                <div className="list-items">
                    {items?.map((item,index)=>(
                        <div className="item" key={index}>
                            <Item data={item} id={index}/>
                        </div>
                    ))}
                </div>
                <FancyButton>Thanh toán</FancyButton>
                <div className="total" style={{marginTop:"5px"}}>
                    <span className="key">Tổng cộng: </span> 
                    <span className="value">{ConvertToVND(totalPrice)}đ</span>
                </div><br /></>:<b></b>
    }
                {/* <div className="cart-cartItem">
                    <Link to="/cart" className="view-cart">Xem giỏ hàng</Link><br />
                    <Link to="/login" className="login">Đăng nhập</Link>
                </div> */}
            </>
        )
    }
    return (
        <div className="cartItem">
            <div  className="cart" onClick={()=>setIsShow(change=>!change)}>
                <div style={{ position: 'relative' }}>
                    <i className="fa-solid fa-cart-shopping fa-bounce" style={{fontSize:"24px"}}></i>
                    <QuantityItem quantity={items?.length} />
                  
                </div>
            </div>
            <div className="cart-preview" ref={cartPreviewRef}>
           
            <PreviewCart/>
           </div>
        </div>
    );
}
export default CartItem