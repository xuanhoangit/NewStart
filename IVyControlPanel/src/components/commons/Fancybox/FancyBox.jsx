import React, { useState } from 'react';
import LoadingProgressBar from './LoadingProgressBar';
import './fancyBox.css'; // Đừng quên import CSS

const FancyBox = ({content}) => {
    const [isVisible, setIsVisible] = useState(false); // Quản lý trạng thái hiển thị
    const [isLoading,setIsLoading]=useState(false)
    // Hàm để mở FancyBox
    const openFancyBox = () => {
        setIsVisible(true);
    };

    // Hàm để đóng FancyBox khi click ra ngoài
   
     const AlertMessage=()=>{
    return (
      <div style={{zIndex:"1",position:"absolute",width:"300px",backgroundColor:"white",color:"black",padding:"10px",textAlign:"center",
      left:"50%",top:"30%",transform:"translateX(-50%)",borderRadius:"5px",border:"2px solid rgb(211, 227, 253)",
      
      }}>
        <p style={{marginBottom:"10px",fontSize:"12px"}}>Sẽ không thể khôi phục. Bạn có chắc chắn khi thực hiện các thay đổi này</p>
        <div style={{display:"flex",justifyContent:"space-evenly",color:"white"}}><span onClick={()=>{content(setIsVisible),setIsLoading(true)}} style={{cursor:"pointer",padding:"5px",backgroundColor:"green"}}>Đồng ý</span><span style={{cursor:"pointer",padding:"5px",backgroundColor:"red"}} onClick={()=>setIsVisible(false)}>Không</span></div>
      </div>
    )
  }
    return (
        <>
        {/* Nút để mở FancyBox */}
            <button className='save'  onClick={openFancyBox}>
                Lưu
            </button>
        {isVisible && (
                <div className="fancybox-overlay" >    
                       {isLoading?<LoadingProgressBar/> :<AlertMessage/>}
                        
                </div>
           )}
        </>
    );
};

export default FancyBox;
