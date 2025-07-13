
import { useEffect, useRef, useState } from "react"
import Slider from "react-slick";
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";
import "./Index.css"
import api from "../../../AxiosInstance";

import FancyButton from "../../commons/FancyButton/FancyButton";
// import SubColorAdd from "../SubColor/Add/Add";
import RegisterForm from "../RegisterForm";
import Profile from "../Profile/Profile";
import RequireRole from "../../../RequireRole";
import { admin } from "../../Roles";
import { useAuth } from "../../../AuthProvider";
import { Link } from "react-router-dom";
import UserNoAccess from "../UserNoAccess/UserNoAccess";


const toQueryString = (obj, prefix = '') => {
    const query = [];

    for (const key in obj) {
        if (obj[key] === null || obj[key] === undefined || obj[key] === '') continue;

        const value = obj[key];
        const prefixedKey = prefix ? `${prefix}.${key}` : key;

        if (Array.isArray(value)) {
            value.forEach(v => query.push(`${encodeURIComponent(prefixedKey)}=${encodeURIComponent(v)}`));
        } else if (typeof value === 'object') {
            query.push(toQueryString(value, prefixedKey));
        } else {
            query.push(`${encodeURIComponent(prefixedKey)}=${encodeURIComponent(value)}`);
        }
    }

    return query.length ? '?' + query.join('&') : '';
};

export default function HRM({ showPopup }){
    const {user}=useAuth()
    const [isShow,setIsShow]=useState(false);
    const [content,SetContent]=useState()
    const [employees,setEmployees]=useState([])
    const [pages,setPages]=useState([]);
    const [currentPage,setCurrentPage]=useState(1)
    const sliderRef=useRef(null);
    const [missingEmployees,setMissingEmployees]=useState();

    const [employeeFilter,setEmployeeFilter]=useState(
        {
                FullName: null,
                FromDateTo: {
                    From: null,
                    To: null
                },
                RoleName: null,
                Gender:null,
                CreateDate: null,
                Page:currentPage
        }
    )
    const activeAfterAddProduct=(p)=>{
        setEmployees((prev)=>[p,...prev])
    }
    const PrevButton=({style,className })=>{
        return employeeFilter.Page!=1?(
            <button href="#gotoheader" style={{position:"absolute",left:"0",top:"0",transform:"translateX(-100%)"}} className={`page prev`} onClick={(e)=>{if(pages.length>4){sliderRef.current.slickGoTo(employeeFilter.Page-2)} ;setProductFilter(prev => ({
                            ...prev,
                            Page: employeeFilter.Page-1
                        }))}}><i className="bi bi-caret-left"></i></button>
        ):null
    }
    const NextButton=({onClick})=>{
        return employeeFilter.Page<pages.length?(
            <button href="#gotoheader" style={{position:"absolute",top:"0",right:"0",transform:"translateX(100%)"}}  className={` page next`} onClick={()=>{if(pages.length>4)sliderRef.current.slickGoTo(employeeFilter.Page+1) ;setProductFilter(prev => ({
                            ...prev,
                            Page: employeeFilter.Page+1
                        }))}}><i className="bi bi-caret-right"></i></button>
        ):null
    }
        const goToSlide = (page) => {
            setProductFilter(prev => ({
                                ...prev,
                                Page: page
                            }))
            sliderRef.current.slickGoTo(page-1);
        };
     const settings = {
        dots: false,
        infinite: false,
        speed: 500,
        slidesToShow: 4,
        slidesToScroll: 1,
        arrows: true, // hiện nút prev/next
        prevArrow: <PrevButton></PrevButton>    ,
        nextArrow:<NextButton></NextButton>
    };
    const setAction=(upload,isShow)=>{
        SetContent(upload);
        setIsShow(isShow)
    }

        
         const filterEmployees=async ()=>{
            const query = toQueryString(employeeFilter);
            const result=await api.get(`/api/employee${query}`)
            console.log("result",result)
            const missing=2-(result.data.length%2)
            //set elenment còn thiếu cho đủ 1 hàng 3 sản phẩm
                const elements=[]
                for (let index = 0; index < missing; index++) {
                    elements.push(<div key={index}></div>);
                    // console.log(elements)
                }
                setMissingEmployees(elements)
                //end
            // console.log(missingEmployees)
         
           const totalPages = Math.ceil(result.count / 12);
           console.log("number item",result.count)
             for (let index = 1; index <= totalPages; index++) {
                    if(pages.length<totalPages){
                        pages.push(index)
                    }
                }
            setEmployees(result.data)
       }
 
    useEffect(()=>{
        console.log(employeeFilter.Page)
            setEmployees([])
            filterEmployees()
    },[employeeFilter])
    return (
        <>
        <div className="employee-page" id="gotoheader">
            <div className="employee-functions">
                <h2>Quản lý nhân sự</h2>
                    <FancyButton onClick={()=>{ SetContent(<RegisterForm showPopup={showPopup} setHumanResources={activeAfterAddProduct}></RegisterForm>); setIsShow(true)}}>
                    Thêm nhân sự</FancyButton>
                    <FancyButton onClick={()=>{ }}>
                    <Link to="/hrm/khong-chuc-vu">Không chức vụ</Link> </FancyButton>
                   
            </div>
            <div className="employee-content">
                <div className="content" >
                    <div style={{display:isShow?"block":"none"}}>
                        <button title='Ẩn' onClick={()=>setIsShow(false)} className='hide'><i className="bi bi-x-lg"></i></button>
                        {content}
                    </div>
                </div>
                <div style={{color:"white"}} className="container-employees"  >
                    {   
                        <>
                        {
                            employees?.map((emp,i)=>(
                               
                                emp.roles?.length>0 && !user.roles.includes(admin) && emp.roles.includes(admin)?null
                                : <Profile user={emp} setContent={SetContent} showPopup={showPopup} setIsShow={setIsShow} key={i}></Profile>
                        ))
                        }
                        {missingEmployees}
                        </>
                        
                    }
                </div>
            </div>
        </div>
        <div style={{width:"100%"}}>
            <div className="paging">
                {employeeFilter.Page!=pages[0] &&employeeFilter.Page>4?
                   <>
                        <button className="first" href="#gotoheader" onClick={()=>goToSlide(pages[0])}>Trang đầu</button>
                    
                   </>:""
                }
                {pages.length>4?
                    <Slider {...settings} ref={sliderRef} style={{width:"200px",position:"relative"}}>
                        {pages?.map((page) => (
                        
                            <button  key={page}
                            href="#gotoheader"
                            onClick={() => {
                                if (employeeFilter.Page !== page) {
                                setProductFilter((prev) => ({
                                    ...prev,
                                    Page: page,
                                }));
                                }
                            }}
                            className={
                                employeeFilter.Page === page ? "page active" : "page"
                            }
                           
                            >
                            {page}
                            </button>
                        ))}
                    </Slider>:
                    <div style={{position:"relative"}}>
                        <PrevButton></PrevButton>
                        {pages?.map((page) => (
                        
                            <button  key={page}
                            href="#gotoheader"
                            onClick={() => {
                                if (employeeFilter.Page !== page) {
                                setProductFilter((prev) => ({
                                    ...prev,
                                    Page: page,
                                }));
                                }
                            }}
                            className={
                                employeeFilter.Page === page ? "page active" : "page"
                            }
                           
                            >
                            {page}
                            </button>
                        ))}
                        <NextButton></NextButton>
                        </div>}
                
                {pages.length>4 && employeeFilter.Page<pages[pages.length-1]?
                   <>
                       
                        <button href="#gotoheader" className="last" onClick={()=>goToSlide(pages[pages.length-1])}>Trang cuối</button>
                   </>:""
                }
                
            </div>
        </div>
        </>
    )
}