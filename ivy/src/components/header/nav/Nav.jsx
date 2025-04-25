import React, {useState,useEffect, useRef} from 'react';
import { Link } from "react-router-dom";
import "./nav.css"
import useFetch from '../../commons/UseFetch';

function Menu() {
    const menu= useFetch("http://localhost:5249/api/Product/menu").data 
    console.log(menu)

    // console.log(menu.events)
    return (
        <div className="menu">
            <ul className='event'> 
                {   menu?.collections?.map((collection,index)=>(
                        <li key={index}><Link to="" className={index==0?"head":""}>{collection.collection__Name}</Link></li> 
                    ))
                }
            </ul>
            <div className='normal'>
            { menu?.categories?.map((category,index)=>(
                <ul className=''  key={index}>
                <li><Link to="">{category.category__Name}</Link></li>
                {category.productTypes.map((type,index)=>(
                     <li key={index}><Link to="">{type.productType__Name}</Link></li>
                ))}
            </ul>
            ))}
            </div>
        </div>
    )
}
function LgScreen(props) {
    return (
        <ul className="lg-screen">
                <li >
                    <Link to="/list-products">{props.data.products.toUpperCase()}</Link>
                    <Menu></Menu>
                </li>
                <li>
                    <Link to="" className="head">{props.data.sales.toUpperCase()}</Link>
                </li>
                <li>
                    <Link to="">{props.data.collections.toUpperCase()}</Link>
                </li>
                <li>
                    <Link to="">{props.data.about.toUpperCase()}</Link>
                </li>
        </ul>
    )
}

function SmScreen(props){
    const navRef = useRef(null);
    const [isShowMenu,setIsShowMenu]=useState(true)
    const showMenu=()=>{
        navRef.current.style.left="-110%"
    }
    const closeMenu=()=>{
        navRef.current.style.left="0"
    }
    useEffect(()=>{
        if(isShowMenu){
            showMenu()
        }else{
            closeMenu()
        }
        return () => {
          };
    },[isShowMenu])
    return (
        <>
        <button onClick={()=>setIsShowMenu(change=>!change)} id="button_show"><svg xmlns="http://www.w3.org/2000/svg" height="24px" viewBox="0 -960 960 960" width="24px" fill="#5f6368"><path d="M120-120v-80h720v80H120Zm0-320v-80h720v80H120Zm0-320v-80h720v80H120Z"/></svg></button>
        <div className="sm-screen">
            <div ref={navRef} className="nav">
                    <button onClick={()=>setIsShowMenu(change=>!change)} id="button_hide" className="bi bi-x-lg"></button>
                    <Link to=""><li style={{textAlign:'center'}} className="login_text">{props.data.login}</li></Link>
                    <Link to="/list-products"><li>{props.data.products.toUpperCase()}</li></Link>

                    <Link to="" ><li className="head">{props.data.sales.toUpperCase()}</li></Link>

                    <Link to=""><li>{props.data.collections.toUpperCase()}</li></Link>
                    <Link to=""><li>{props.data.about.toUpperCase()}</li></Link>
                    
            </div>
        </div>
        <div className="action-sm"> 
            <Link to="">
                <i className="fa-solid fa-magnifying-glass"></i><br />
                Tìm kiếm
            </Link>
            <Link to="/login">
                <i className="fa-solid fa-user"></i><br />
                Đăng nhập
            </Link>
            <Link to="">
                <i className="fa-solid fa-phone"></i><br />
                Trợ giúp
            </Link>
           </div>
           </>
    )
}
function Nav(props) {

    return (
        <>
        <LgScreen data={props}></LgScreen>
        <SmScreen data={props}></SmScreen>
        </>
    );
}
export default Nav;