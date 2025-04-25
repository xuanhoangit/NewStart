
import next from "./ButtonNext_Prev.module.css"
const NextArrow = ({ onClick }) => {
    return (
      <div  className={next.Next} id="buttonNext"  onClick={onClick}>
       <i className="bi bi-arrow-right"></i>
      </div>
    );
  };
export default NextArrow