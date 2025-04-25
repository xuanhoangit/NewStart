import { lazy} from 'react';
import LazyLoadComponent from '../commons/lazy/LazyLoadComponent';
const ContainerBanner = lazy(() => import('./banner/ContainerBanner'));
const ProductDisplayEvent=lazy(()=>import("./productDisplayEvent/ProductDisplayEvent"))
const BannerAds=lazy(()=>import("./bannerAds/BannerAds"))
const Gallery=lazy(()=>import("./gallery/Gallery"))


function Home(params) {
    
    return (
        <>  
        <LazyLoadComponent>
            <ContainerBanner />
        </LazyLoadComponent>

      <LazyLoadComponent>
        <ProductDisplayEvent titleSection="Product 1" />
      </LazyLoadComponent>

      <LazyLoadComponent>
        <BannerAds />
      </LazyLoadComponent>

      <LazyLoadComponent>
        <Gallery />
      </LazyLoadComponent>
        </>
    )
}
export default Home