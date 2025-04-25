import { Suspense,useRef ,useState,useEffect} from 'react';
import useOnScreen from './useOnScreen';
import "./lazy.css"
const LazyLoadComponent = ({ children }) => {
    const ref = useRef();
    const isVisible = useOnScreen(ref, "-80px");
    const [hasLoaded, setHasLoaded] = useState(false);

    // Chỉ cập nhật `hasLoaded` khi component xuất hiện lần đầu
    useEffect(() => {
        if (isVisible && !hasLoaded) {
            setHasLoaded(true);
        }
    }, [isVisible, hasLoaded]);

    return (
        <div
            ref={ref}
            className={`lazy-load-component ${isVisible ? 'visible' : ''}`}
        >
            {/* Chỉ render khi component lần đầu hiển thị */}
            {hasLoaded && (
                <Suspense fallback={<div>Đang tải...</div>}>
                    {children}
                </Suspense>
            )}
        </div>
    );
};
export default LazyLoadComponent