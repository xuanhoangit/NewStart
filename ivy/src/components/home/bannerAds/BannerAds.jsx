
import { Link } from "react-router-dom";
import { Suspense ,lazy} from "react";

import Slider from "react-slick";
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";

import "./bannerAds.css"
import NextArrow from "../../commons/buttonNext_Prev/NextArrow";
import PrevArrow from "../../commons/buttonNext_Prev/PrevArrow";
// images

import bannerAds1 from "../../../assets/images/banners/banner-ads-1.webp"
import bannerAds2 from "../../../assets/images/banners/banner-ads-2.webp"
const LazyImg =lazy(()=>import( '../../commons/product/LazyImg'));

const images = [
    { id: 1, src: `${bannerAds1}`,href:"https://www.youtube.com/" },
    { id: 2, src: `${bannerAds2}`,href: "https://www.facebook.com/" },
  ];
  
  const BannerAds = () => {
    // if(images.length==1){
    //   return (
    //     <div key={images[0].id} className="slick-list slick-slider">
    //     <a href={images[0].href}>
    //       <img src={images[0].src} alt={`Slide ${images[0].id}`} style={{ width: "100%" }} />
    //     </a>
    //   </div>
    //   )
    // }
    const settings = {
    //   dots: true,          // Hiển thị các chấm tròn chỉ số
      infinite: true,      // Vòng lặp vô tận
      speed: 500,          // Thời gian chuyển tiếp giữa các slide
      slidesToShow: 2,     // Số lượng slide hiển thị cùng lúc
      slidesToScroll: 1,   // Số lượng slide chuyển tiếp khi click
      autoplay: true,      // Tự động chuyển tiếp
      autoplaySpeed: 4000, // Thời gian mỗi slide được hiển thị
      nextArrow:<NextArrow />,
      prevArrow:<PrevArrow />,
    };
  
    return (
      <section className="bannerAds">
        <div className="slides-container">
        <Slider {...settings}>
          {images.map((image) => (
            <div key={image.id}>
              <Link to={image.href}>
              <Suspense>
                <LazyImg src={image.src} alt={`Slide ${image.id}`} srcSet={image.src} style={{ width: "100%" }} />
                </Suspense>
              </Link>
            </div>
          ))}
        </Slider>

        </div>
      </section>
    );
  };
export default BannerAds