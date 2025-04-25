import { useRef, useState,useEffect, lazy, Suspense } from "react"
import "./tab.css"

function Toggle(props) {
    const toggleRef = useRef(null);
    const iconRef = useRef(null)
    const [isShow, setIsShow] = useState(props.show);
    useEffect(() => {
        if (isShow) {
            toggleRef.current.className = "toggle-content__product_detail open"
            toggleRef.current.display="block"
            iconRef.current.className = "fa-solid fa-angle-up"
        } else {
            toggleRef.current.className = "toggle-content__product_detail"
            toggleRef.current.display = "none"
            iconRef.current.className = "fa-solid fa-angle-down"
        }
    }, [isShow])
    return (
        <>
            <div ref={toggleRef} className="toggle-content__product_detail">
                {props.toggle}
            </div>
            <p className="toggle-icon" onClick={() => setIsShow(change => !change)}>
                <i ref={iconRef} className="fa-solid fa-angle-down"></i>
            </p>
        </>
    )
}

function Gioi_thieu(){
    return (
        <div>
            <p>Mang đậm vẻ đẹp tinh tế và thanh thoát, áo sơ mi Tencel Blossom là lựa chọn hoàn hảo cho nàng công sở hiện đại yêu thích phong cách nhẹ nhàng, nữ tính nhưng vẫn đầy thanh lịch.</p>
            <p> Chất liệu Tencel mềm mại, thoáng mát, thân thiện với môi trường giúp áo vừa mang đến cảm giác thoải mái, vừa có khả năng thấm hút mồ hôi tốt và thích hợp diện trong nhiều tiết trời khác nhau.</p>
            <p>Điểm nhấn đặc biệt của chiếc áo chính là nền họa tiết thêu hoa chìm tỉ mỉ, tạo sự tinh tế, sang trọng mà không quá cầu kỳ. Thiết kế dễ dàng kết hợp với quần tây, chân váy hoặc jeans, phù hợp cho cả những ngày đi làm hay đi dạo phố.</p>
        </div>
    )
}
function Chi_tiet_san_pham(params) {
    return (
        <>
            <div className="product-detail__decription">
                <div className="key">
                    <p>Dòng sản phẩm</p>
                    <p>Nhóm sản phẩm</p>
                    <p>Cổ áo</p>
                    <p>Tay áo</p>
                    <p>Kiểu dáng</p>
                    <p>Độ dài</p>
                    <p>Họa tiết</p>
                    <p>Chất liệu</p>
                </div>
                <div className="value">
                    <p>Ladies</p>
                    <p>Ladies</p>
                    <p>Ladies</p>
                    <p>Ladies</p>
                    <p>Ladies</p>
                    <p>Ladies</p>
                    <p>Ladies</p>
                    <p>Ladies</p>
                </div>
            </div>
        </>
    )
}
function Bao_quan(params) {
    return (
        <div>
            <p>Chi tiết bảo quản sản phẩm : </p>
            <p><b>* Các sản phẩm thuộc dòng cao cấp (Senora) và áo khoác (dạ, tweed, lông, phao) chỉ giặt khô, tuyệt đối không giặt ướt.</b></p>
            <p>* Vải dệt kim: sau khi giặt sản phẩm phải được phơi ngang tránh bai giãn.</p>
            <p>* Vải voan, lụa, chiffon nên giặt bằng tay.</p>
            <p>* Vải thô, tuytsi, kaki không có phối hay trang trí đá cườm thì có thể giặt máy.</p>
            <p>* Vải thô, tuytsi, kaki có phối màu tương phản hay trang trí voan, lụa, đá cườm thì cần giặt tay.</p>
            <p>* Đồ Jeans nên hạn chế giặt bằng máy giặt vì sẽ làm phai màu jeans. Nếu giặt thì nên lộn trái sản phẩm khi giặt, đóng khuy, kéo khóa, không nên giặt chung cùng đồ sáng màu, tránh dính màu vào các sản phẩm khác. </p>
            <p>* Các sản phẩm cần được giặt ngay không ngâm tránh bị loang màu, phân biệt màu và loại vải để tránh trường hợp vải phai. Không nên giặt sản phẩm với xà phòng có chất tẩy mạnh, nên giặt cùng xà phòng pha loãng.</p>
            <p>* Các sản phẩm có thể giặt bằng máy thì chỉ nên để chế độ giặt nhẹ, vắt mức trung bình và nên phân các loại sản phẩm cùng màu và cùng loại vải khi giặt.</p>
            <p>* Nên phơi sản phẩm tại chỗ thoáng mát, tránh ánh nắng trực tiếp sẽ dễ bị phai bạc màu, nên làm khô quần áo bằng cách phơi ở những điểm gió sẽ giữ màu vải tốt hơn.</p>
            <p>* Những chất vải 100% cotton, không nên phơi sản phẩm bằng mắc áo mà nên vắt ngang sản phẩm lên dây phơi để tránh tình trạng rạn vải.</p>
            <p>* Khi ủi sản phẩm bằng bàn là và sử dụng chế độ hơi nước sẽ làm cho sản phẩm dễ ủi phẳng, mát và không bị cháy, giữ màu sản phẩm được đẹp và bền lâu hơn. Nhiệt độ của bàn là tùy theo từng loại vải. </p>
        </div>
    )
}

function Tab(params) {
    const tab_item_1=useRef(null)
    const tab_item_2=useRef(null)
    const tab_item_3=useRef(null)
    const tab_content_1=useRef(null)
    const tab_content_2=useRef(null)
    const tab_content_3=useRef(null)
    function Reqtab(tab_item, tab_content) {
        tab_item_1.current.style.borderBottom="none"
        tab_item_2.current.style.borderBottom ="none"
        tab_item_3.current.style.borderBottom ="none"

        tab_content_1.current.style.display="none"
        tab_content_2.current.style.display="none"
        tab_content_3.current.style.display="none"

        
        tab_item.style.borderBottom ="1px solid"
        tab_content.style.display="block"
    }
    return (
        <>
            <div className="tab">
                <div className="tab-header ">
                    <div className="tab-item active" ref={tab_item_1} onClick={() => Reqtab(tab_item_1.current, tab_content_1.current)}>Giới thiệu</div>
                    <div className="tab-item" ref={tab_item_2} onClick={() => Reqtab(tab_item_2.current, tab_content_2.current)}>Chi tiết sản phẩm</div>
                    <div className="tab-item" ref={tab_item_3} onClick={() => Reqtab(tab_item_3.current, tab_content_3.current)}>Bảo quản</div>
                </div>
                <div className="tab-body">
                    <div ref={tab_content_1} className="tab-content active">
                        <Toggle show={false} toggle={<Gioi_thieu></Gioi_thieu>}/>
                    </div>
                    <div ref={tab_content_2} className="tab-content">
                        <Toggle show={false} toggle={<Chi_tiet_san_pham></Chi_tiet_san_pham>} />
                    </div>
                    <div ref={tab_content_3} className="tab-content">
                        <Toggle show={false} toggle={<Bao_quan></Bao_quan>} />
                    </div>
                </div>
            </div>
        </>
    )
}
export default Tab