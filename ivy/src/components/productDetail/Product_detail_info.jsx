import {  lazy, Suspense, useState } from "react"
import ConvertToVND from "../commons/ConvertToVND";
import SizeSelect from "../commons/filter/size/SelectSize"
import SelectColor from "../commons/filter/color/SelectColor";
import MainButton from "../commons/mainButton/MainButton"
import TableSize from"./TableSize" ;
import FancyBox from"../commons/fancybox/FancyBox";
const Tab =lazy(()=>import("./Tab")) 

import "./product_detail_info.css"
import LazyLoadComponent from "../commons/lazy/LazyLoadComponent";


function Product_detail_info() {
    const [quantity, setQuantity] = useState(1)
    const [isPopupError, setisPopupError] = useState(true)
    const [action, setAction] = useState("")
    const [error, setError] = useState("")
    if (quantity < 1) {
        setQuantity(1)
    }
    const handleSubmit = (event) => {
        event.preventDefault(); // Ngăn form gửi dữ liệu đi

        const formData = new FormData(event.target); // Lấy dữ liệu từ form
        // Chuyển đổi formData thành đối tượng đơn giản
        const data = Object.fromEntries(formData.entries());
        if (data.m == "m" || data.s == "s" || data.l == "l" || data.xl == "xl" || data.xxl == "xxl") {
            setisPopupError(true)
            window.location.href = action
        } else {
            setisPopupError(false)
            setTimeout(() => {
                setisPopupError(true)
            }, 3000)
        }
        if (action == "action3") {
            setError("Bạn cần đăng nhập để thực hiện chức năng này")
        } else if (action != null) {
            setError("Bạn chưa chọn size")
        }
        console.log(data)
    };
    function Error(props) {
        return (
            <div className="error_msg"><i className="bi bi-info-circle-fill" style={{ fontSize: "20px" }}></i> {error}!</div>
        )
    }
    return (
        <>
            <form onSubmit={handleSubmit} action="" className="product-detail_form">
                {!isPopupError ? <Error></Error> : ""}
                <h1 className="product-name">Áo sơ mi tay kiểu Sleeves</h1>
                <p className="skucode">SKU: <span>skucode1234</span></p>
                <div className="price">{ConvertToVND(1190000)}đ</div>
                <div className="color-name">Màu sắc: <span>Be</span></div>
                <div className="colors" style={{ position: "relative" }}>
                    <SelectColor name="be" src="https://pubcdn.ivymoda.com/ivy2/images/color/004.png"></SelectColor>
                </div>
                <div className="">
                    <ul className="list-sizes product-detail_sizes" style={{ display: "flex" }}>
                        <li>
                            <SizeSelect type="checkbox" sizename="s" name="s" value="s" isUnavailable={true} />
                        </li>
                        <li>
                            <SizeSelect type="checkbox" sizename="m" name="m" value="m" isUnavailable={false} />
                        </li>
                        <li>
                            <SizeSelect type="checkbox" sizename="l" name="l" value="l" isUnavailable={false} />
                        </li>
                        <li>
                            <SizeSelect type="checkbox" sizename="xl" name="xl" value="xl" isUnavailable={false} />
                        </li>
                        <li>
                            <SizeSelect type="checkbox" sizename="xxl" name="xxl" value="xxl" isUnavailable={false} />
                        </li>
                    </ul>
                    
                    <FancyBox content={<Suspense><TableSize></TableSize></Suspense>}></FancyBox>
                </div>
                <div className="quantity">
                    Số lượng
                    <div className="quantity-change">
                        <div className="decrease" onClick={() => setQuantity(quantity - 1)}>-</div>
                        <input type="number" name="quantity" onInput={event => setQuantity(parseInt(event.target.value))} min={0} value={quantity.toString()} />
                        <div className="increase" onClick={() => setQuantity(quantity + 1)}>+</div>
                    </div>
                </div>
                <div className="action_product-detail">
                    <div className="add-to-cart" onClick={() => setAction("action1")}>
                        <MainButton text="Thêm vào giỏ"></MainButton>
                    </div>
                    <div className="buy" onClick={() => setAction("action2")}>
                        <MainButton text="Mua hàng"></MainButton>
                    </div>
                    <div className="add-to-favorite" onClick={() => setAction("action3")}>
                        <MainButton text={<i className="fa-regular fa-heart"></i>}></MainButton>
                    </div>
                </div>
            </form>
            <Suspense>
                <Tab></Tab>
            </Suspense>
        </>
    )
}
export default Product_detail_info