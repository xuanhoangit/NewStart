import { useRef, useState ,useEffect} from "react"
import "./Message.css"
function Message(props) {
    const [className, setClassName] = useState('msg'); // Khởi tạo className rỗng

    useEffect(() => {
      // Sau 2 giây, đặt className thành "msg"
      const timer1 = setTimeout(() => {
        setClassName('msg msg-popup');
      }, 300);
  
    //   Sau 4 giây (2 giây tiếp theo), đặt className thành "msg popup"
      const timer2 = setTimeout(() => {
        setClassName('msg msg-popup  msg-popleft');
      }, 2000);
      const timer3 = setTimeout(() => {
        setClassName('msg');
      }, 2300);
  
      // Dọn dẹp timer khi component unmount để tránh rò rỉ bộ nhớ
      return () => {
        clearTimeout(timer1);
        clearTimeout(timer2);
        clearTimeout(timer3);
      };
    }, []);
    return (
        <div className={className} style={{backgroundColor:props.color}}>
            {props.content}
        </div>
    )
}
export default Message