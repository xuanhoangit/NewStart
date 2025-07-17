import { Link } from "react-router-dom";
import ConvertToVND from "../../commons/ConvertToVND";
import "./CartItem.css";
import { useState, useRef, useEffect } from "react";
import FancyButton from "../../commons/FancyButton/FancyButton";
import { useCart } from "../../../context/CartProvider";

function QuantityItem({ quantity }) {
  return <div className="cart_quantity-item">{quantity}</div>;
}

function CartItem() {
  const [isShow, setIsShow] = useState(false);
  const cartPreviewRef = useRef(null);

  const {
    cartItems,
    updateQuantity,
    totalPrice
  } = useCart();

  useEffect(() => {
    cartPreviewRef.current.className = isShow ? "cart-preview show" : "cart-preview";
  }, [isShow]);

  function Item({ item, index }) {
    return (
      <>
        <img
          className="item-img"
          style={{ width: "100px", aspectRatio: "3/4", objectFit: "cover" }}
          src={item.src}
          alt={item.name}
        />
        <div className="item-info">
          <div className="item-name">{item.name}</div>
          <div>
            <div className="item-color">
              <span className="key">Màu sắc: </span>
              <span className="value">{item.color}</span>
            </div>
            <div className="item-size">
              <span className="key">Size:</span>
              <span className="value">{item.size}</span>
            </div>
          </div>
          <div className="item-bottom">
            <div className="item-cartItem">
              <div className="remove" onClick={() => updateQuantity(index, -1)}>-</div>
              <input
                type="number"
                value={item.quantity}
                readOnly
                style={{ width: "40px", textAlign: "center" }}
              />
              <div className="add" onClick={() => updateQuantity(index, 1)}>+</div>
            </div>
            <div className="item-price">
              {ConvertToVND(item.price * item.quantity)}đ
            </div>
          </div>
        </div>
      </>
    );
  }

  function PreviewCart() {
    return (
      <>
        <div className="cart-title">
          Giỏ hàng
          <QuantityItem quantity={cartItems?.length} />
          <div className="hide" onClick={() => setIsShow((prev) => !prev)}>
            <i className="bi bi-x-lg"></i>
          </div>
        </div>

        {cartItems?.length > 0 ? (
          <>
            <div className="list-items">
              {cartItems.map((item, index) => (
                <div className="item" key={index}>
                  <Item item={item} index={index} />
                </div>
              ))}
            </div>
            <FancyButton>Thanh toán</FancyButton>
            <div className="total" style={{ marginTop: "5px" }}>
              <span className="key">Tổng cộng: </span>
              <span className="value">{ConvertToVND(totalPrice)}đ</span>
            </div>
            <br />
            <div className="cart-cartItem">
              <Link to="/cart" className="view-cart">
                Xem giỏ hàng
              </Link>
              <br />
              <Link to="/login" className="login">
                Đăng nhập
              </Link>
            </div>
          </>
        ) : (
          <div className="empty-cart">Giỏ hàng trống</div>
        )}
      </>
    );
  }

  return (
    <div className="cartItem">
      <div className="cart" onClick={() => setIsShow((prev) => !prev)}>
        <div style={{ position: "relative" }}>
          <i className="fa-solid fa-cart-shopping fa-bounce" style={{ fontSize: "24px" }}></i>
          <QuantityItem quantity={cartItems?.length} />
        </div>
      </div>
      <div className="cart-preview" ref={cartPreviewRef}>
        <PreviewCart />
      </div>
    </div>
  );
}

export default CartItem;
