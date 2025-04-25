import { Link } from 'react-router-dom';
import { lazy, Suspense } from 'react';

import Slider from "react-slick";
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";

const LazyImg =lazy(()=>import( '../../commons/product/LazyImg'));


import "./banner.css"
import NextArrow from "../../commons/buttonNext_Prev/NextArrow";
import PrevArrow from "../../commons/buttonNext_Prev/PrevArrow";


// images
import banner1 from "../../../assets/images/banners/banner-1.webp"
import banner2 from "../../../assets/images/banners/banner-2.webp"


import banner_sm_1 from "../../../assets/images/banners/banner-sm-1.jpg"
import banner_sm_2 from "../../../assets/images/banners/banner-sm-2.jpg"
const images = [
  { id: 1, src: `${banner1}`, href: "https://www.youtube.com/" },
  { id: 2, src: `${banner2}`, href: "https://www.facebook.com/" },
];
const images_sm = [
  { src: `${banner_sm_1}`, href: "" },
  { src: `${banner_sm_2}`, href: "" },
]
  const Banner = (props) => {
    if (props.data.length==1){
      return (
        <div className="slick-list slick-slider">
          <Link to={props.data[0].href}>
          <Suspense>
            <LazyImg src={props.data[0].src} alt={`Slide ${props.data[0].id}`} style={{ width: "100%" }} />
              </Suspense>
        </Link>
      </div>
      )
    }
    const settings = {
      dots: true,          // Hiển thị các chấm tròn chỉ số
      infinite: true,      // Vòng lặp vô tận
      speed: 500,          // Thời gian chuyển tiếp giữa các slide
      slidesToShow: 1,     // Số lượng slide hiển thị cùng lúc
      slidesToScroll: 1,   // Số lượng slide chuyển tiếp khi click
      autoplay: true,      // Tự động chuyển tiếp
      autoplaySpeed: 3000, // Thời gian mỗi slide được hiển thị
      nextArrow:<NextArrow />,
      prevArrow:<PrevArrow />,
    };
  
    return (
      <section className="banner">
        <Slider {...settings}>
          {props.data.map((image,index) => (
            <div key={index}>
              <Link to={image.href}>
              <Suspense>
                <LazyImg src={image.src} alt={`Slide ${image.id}`} srcSet={image.src} style={{ width: "100%" }} />
              </Suspense>
              </Link>
            </div>
          ))}
        </Slider>
      </section>
    );
  };

function ContainerBanner() {
  return(
    <>
    <div className="banner-lg">
        <Banner data={images}></Banner>
    </div>
    <div className="banner-sm">
        <Banner data={images_sm}></Banner>
    </div>
    </>
  )
}
export default ContainerBanner