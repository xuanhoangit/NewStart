import { useState ,useRef, useEffect} from 'react'
import './center.css'
import { Link } from 'react-router-dom'

const left_center=[
	{ 	
		id:1,
		//href:"https://ivymoda.com/about/gioi-thieu",
		textContent:"Về IVY moda",
		target:""
	},
	{ 	id:2,
		//href:"https://tuyendung.ivy.com.vn",
		textContent:"Tuyển dụng",
		target:"_blank"
	},
	{ 	
		id:3,
		//href:"https://ivymoda.com/page/cuahang",
		textContent:"Hệ thống cửa hàng",
		target:""
	},
]

const main_center=[
	{ 	
		id:1,
		//href:"https://ivymoda.com/about/chinhsach-dieukhoan",
		textContent:"Chính sách điều khoản",
		target:""
	},
	{ 	
		id:2,
		//href:"https://ivymoda.com/about/huong-dan-mua-hang",
		textContent:"Hướng dẫn mua hàng",
		target:""
	},
	{ 	
		id:3,
		//href:"https://ivymoda.com/about/chinh-sach-thanh-toan",
		textContent:"Chính sách thanh toán",
		target:""
	},
	{ 	
		id:4,
		//href:"https://ivymoda.com/about/chinh-sach-doi-tra",
		textContent:"Chính sách đổi trả",
		target:""
	},
	{ 	
		id:5,
		//href:"https://ivymoda.com/about/chinh-sach-bao-hanh",
		textContent:"Chính sách bảo hành",
		target:""
	},
	{ 	
		id:6,
		//href:"https://ivymoda.com/about/chinh-sach-giao-nhan-van-chuyen",
		textContent:"Chính sách giao nhận vận chuyển",
		target:""
	},
	{ 	
		id:7,
		//href:"https://ivymoda.com/about/chinh-sach-the-thanh-vien",
		textContent:"Chính sách thẻ thành viên",
		target:""
	},
	{ 	
		id:8,
		//href:"https://ivymoda.com/about/qa",
		textContent:"Q&A",
		target:""
	},
]

const right_center=[
	{	id:1,
		//href:"tel:02466623434",
		textContent:"Hotline",
		target:""
	},
				
	{	
		id:2,
		//href:"mailto:saleadmin@ivy.com.vn",
		textContent:"Email",
		target:""
	},
	{	
		id:3,
		//href:"javascript:openCsChatBox()",
		textContent:"Live chat",
		target:""
	},
	{	
		id:4,
		//href:"http://messenger.com/t/thoitrangivymoda",
		textContent:"Messenger",
		target:""
	},
	{	
		id:5,
		//href:"https://ivymoda.com/lien-he",
		textContent:"Liên hệ",
		target:""
	},
]

function Toggle(props) {
    const toggleRef=useRef(null);
    const iconRef=useRef(null)
    const [isShow,setIsShow]=useState(props.isShow);
    useEffect(()=>{
        if(isShow){
            toggleRef.current.className="center-content open"
            iconRef.current.style.transform="rotate(90deg)"
        }else{
            toggleRef.current.className="center-content"
            iconRef.current.style.transform="rotate(0)"
        }
    },[isShow])
    return (
        <>  
            <p className='title' style={{display:"flex",justifyContent:"space-between",alignItems:"baseline"}} onClick={()=>setIsShow(change=>!change)}><span>{props.title}</span>
                <i ref={iconRef}  className="fa-solid fa-angle-right"></i>
            </p>
            <ul ref={toggleRef} className='center-content'>
				{
				props.toggle.map((link,index)=>(
					<li key={index}>
						<Link target={link.target} to>{link.textContent}</Link>
					</li>
				))
				}
            </ul>
        </>
    )
}
function Center(props) {

    return (
			<>
			<div className='center'>
				<div>
					<Toggle isShow={true} title="Giới thiệu" toggle={left_center}/>
				</div>
				<div className="main-center-ft item-center-ft">
					<Toggle isShow={false} title="Dịch vụ khách hàng" toggle={main_center}></Toggle>
				</div>
				<div className="right-center-ft item-center-ft">
					<Toggle isShow={false} title="Liên hệ" toggle={right_center}></Toggle>
				</div>
			</div>
				</>
   
    )
}
export default Center