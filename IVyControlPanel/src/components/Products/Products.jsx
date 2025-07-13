
import { Link } from "react-router-dom";
import { iconsImgs } from "../../utils/images";
import "./Products.css";

const Products = () => {
  return (
      <div style={{ border: "2px solid white" }} className="grid-one-item grid-common grid-c1">
        <Link  to="/products/">
        <div className="grid-c-title">
            <h3 className="grid-c-title-text">Products</h3>
            <button className="grid-c-title-icon">
                <img src={ iconsImgs.plus } />
            </button>
        </div>
        </Link>
        <div className="grid-c1-content">
            product
        </div>
        {/* <div className="grid-c1-content">
            <p>Balance</p>
            <div className="lg-value">$ 22,000</div>
            <div className="card-wrapper">
                <span className="card-pin-hidden">**** **** **** </span>
                <span>1234</span>
            </div>
            <div className="card-logo-wrapper">
                <div>
                    <p className="text text-silver-v1 expiry-text">Expires</p>
                    <p className="text text-sm text-white">12/22</p>
                </div>
                <div className="card-logo">
                    <div className="logo-shape1"></div>
                    <div className="logo-shape2"></div>
                </div>
            </div>
        </div> */}
    </div>
  )
}

export default Products
