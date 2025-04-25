import { Link } from "react-router-dom";
import { lazy, Suspense } from "react";
import Slider from "react-slick";
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";

import "./gallery.css"

import gallery1 from "../../../assets/images/gallery/gallery-1.webp"
import gallery2 from "../../../assets/images/gallery/gallery-2.webp"
import gallery3 from "../../../assets/images/gallery/gallery-3.webp"
import gallery4 from "../../../assets/images/gallery/gallery-4.webp"
import gallery5 from "../../../assets/images/gallery/gallery-5.webp"

const LazyImg =lazy(()=>import( '../../commons/product/LazyImg'));

const images =[
    `${gallery1}`,
    `${gallery2}`,
    `${gallery3}`,
    `${gallery4}`,
    `${gallery5}`,
]

function Gallery() {
    const settings = {
        infinite: true,      // Vòng lặp vô tận
        speed: 500,          // Thời gian chuyển tiếp giữa các slide
        slidesToShow: 5,     // Số lượng slide hiển thị cùng lúc
        slidesToScroll: 1,   // Số lượng slide chuyển tiếp khi click
        autoplay: true,      // Tự động chuyển tiếp
        autoplaySpeed: 4000, // Thời gian mỗi slide được hiển thị

        responsive: [
            {
              breakpoint: 600, 
              settings: {
                slidesToShow: 2, 
                slidesToScroll: 1, 
              }
            }
          ]
      };
    

    
    return (
        <section className="gallery-section">
            <h3 className="gallery-title">gallery</h3>
            <div className="galleries-container">
                    <div className="slides-container">
                    <Slider {...settings}>
                        {images.map((image,index)=>(
                            <div key={index+1}>
                                <Link to="">
                                <Suspense>
                                    <LazyImg src={image} alt="" srcSet={image} style={{width:"100%"}}/>
                                    </Suspense>
                                </Link>
                            </div>
                        
                        ))}
                    </Slider>
                </div>
            </div>

        </section>
    )
}
export default Gallery