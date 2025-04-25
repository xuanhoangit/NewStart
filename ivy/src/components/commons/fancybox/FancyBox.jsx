import React, { useState } from 'react';
import './fancyBox.css'; // Đừng quên import CSS

const FancyBox = (props) => {
    const [isVisible, setIsVisible] = useState(false); // Quản lý trạng thái hiển thị

    // Hàm để mở FancyBox
    const openFancyBox = () => {
        setIsVisible(true);
    };

    // Hàm để đóng FancyBox khi click ra ngoài
    const closeFancyBox = (event) => {
        console.log(event.target)
        if (event.target.className === 'fancybox-overlay' || event.target.id === 'button-close') {
            setIsVisible(false);
        }
    };

    return (
        <div>
            {/* Nút để mở FancyBox */}
            <div className="open-fancybox-btn" onClick={openFancyBox}>
                <i className="fa-solid fa-ruler"></i> Kiểm tra size của bạn
            </div>

            {/* Overlay chứa FancyBox */}
            {isVisible && (
                <div className="fancybox-overlay" onClick={closeFancyBox}>
                    <div className="fancybox">
                        <div ><i id="button-close" onClick={closeFancyBox} className="bi bi-x"></i></div>
                        {props.content}
                    </div>
                </div>
            )}
        </div>
    );
};

export default FancyBox;
