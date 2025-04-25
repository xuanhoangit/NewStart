import { useEffect, useRef, useState } from "react"

import Slider from "react-slick";
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";
import "./slide.css"
const imgs = [
    "https://pubcdn.ivymoda.com/files/product/thumab/1400/2024/09/06/c08a05c96975a5c46b40bda7098dfcb7.webp",
    "https://pubcdn.ivymoda.com/files/product/thumab/400/2024/09/06/eea027c45f60f52707a3e59eaf98e5ed.webp",
    "https://pubcdn.ivymoda.com/files/product/thumab/400/2024/09/06/ca289d3431dbb7068581eeae6e108fa8.webp",
    "https://pubcdn.ivymoda.com/files/product/thumab/400/2024/09/06/66e6710b489f06ecf3d5f66e7906a085.webp",
]
function NextArrow({ onClick }) {
    return (
        <div onClick={onClick} className="next"><i className="bi bi-caret-down-fill"></i></div>
    )
}
function PrevArrow({ onClick }) {
    return (
        <div onClick={onClick} className="prev"><i className="bi bi-caret-up-fill"></i></div>
    )
}
function RightArrow({ onClick }) {
    return (
        <div onClick={onClick} className="right"><i className="bi bi-caret-right-fill"></i></div>
    )
}
function LeftArrow({ onClick }) {
    return (
        <div onClick={onClick} className="left"><i className="bi bi-caret-left-fill"></i></div>
    )
}
function Slide(params) {
    const [itemZoom, setItemZoom] = useState(imgs[0])
    const zoomItemRef = useRef(null)
    const imgMainRef = useRef(null)

    useEffect(() => {
        imgMainRef.current.addEventListener('mousemove', (e) => {
            const rect = imgMainRef.current.getBoundingClientRect();
            const x = e.clientX - rect.left;
            const y = e.clientY - rect.top;

            const xPercent = (x / imgMainRef.current.offsetWidth) * 100;
            const yPercent = (y / imgMainRef.current.offsetHeight) * 100;

            zoomItemRef.current.style.transformOrigin = `${xPercent}% ${yPercent}%`;
        });

        imgMainRef.current.addEventListener('mouseenter', () => {
            zoomItemRef.current.className = 'zoomed';
        });

        imgMainRef.current.addEventListener('mouseleave', () => {
            zoomItemRef.current.className = "";
        });
    }, [])
    const settings = {
        // dots: true,
        infinite: false,
        speed: 500,
        slidesToShow: 3,
        slidesToScroll: 1,
        vertical: true, // Slider theo chiều dọc
        verticalSwiping: true, // Cho phép vuốt theo chiều dọc
        nextArrow: <NextArrow />, // Nút Next tùy chỉnh
        prevArrow: <PrevArrow />  // Nút Prev tùy chỉnh
    };
    const settings_sm = {
        // dots: true,
        infinite: false,
        speed: 500,
        slidesToShow: 3,
        slidesToScroll: 1,
        nextArrow: <RightArrow />, // Nút Next tùy chỉnh
        prevArrow: <LeftArrow />  // Nút Prev tùy chỉnh
    };
    const [isVisible, setIsVisible] = useState(true);
    const Timing = (src) => {
        setIsVisible(false);
        setTimeout(() => {
            setItemZoom(src)
            setIsVisible(true)
        }, 50);
    }
    const handle = (e) => {
        Timing(e.target.src)
    }
    function Next(params) {
        const index = GetIndexOf()
        if (index < imgs.length - 1) {
            Timing(imgs[index + 1])
        }
    }
    function Prev(params) {
        const index = GetIndexOf()
        if (index > 0) {
            Timing(imgs[index - 1])
        }
    }
    function GetIndexOf() {
        return imgs.indexOf(zoomItemRef.current.src);
    }
    return (

        <div className="show-img-detail">
            <div className="slide-banner">
                <div className="img_main" ref={imgMainRef}>
                    <div className="next" onClick={Next}><i className="bi bi-caret-right-fill"></i></div>
                    <div className="prev" onClick={Prev}><i className="bi bi-caret-left-fill"></i></div>
                    <img style={{
                        filter: isVisible ? "blur(0px)" : "blur(5px)",
                        opacity: isVisible ? 1 : 0.5,
                        transition: 'opacity 0.2s ease-in-out'
                    }} src={itemZoom} ref={zoomItemRef} alt="" width={"100%"} height={"100%"} id="zoom-img" />

                </div>
            </div>
            <div className="slideshow">
                <div className="slide-container">
                    <Slider {...settings}>
                        {imgs.map((src, index) => (
                            // <div key={index}>
                            <img key={index} onClick={handle} src={src} alt={index} width={"100%"} srcSet="" />
                            // </div>
                        ))}
                    </Slider>
                </div>
                <div className="slide-container__sm">
                    <Slider {...settings_sm}>
                        {imgs.map((src, index) => (
                            // <div key={index}>
                            <img key={index} onClick={handle} src={src} alt={index} width={"100%"} srcSet="" />
                            // </div>
                        ))}
                    </Slider>
                </div>
            </div>
        </div>
    )
}
export default Slide
