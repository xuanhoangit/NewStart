
import prev from "./ButtonNext_Prev.module.css"
const PrevArrow = ({ onClick }) => {
    return (
      <div  className={prev.Prev} id="buttonPrev" onClick={onClick}>
        <i className="bi bi-arrow-left" ></i>
      </div>
    );
  };
export default PrevArrow