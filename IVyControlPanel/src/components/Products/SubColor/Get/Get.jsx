import { useState } from "react"
import "./Get.css"

const ListSubColor= ({action,data})=>{
    const [selectedIndex,setSelectedIndex]=useState();
    const handleChange=(item,index)=>{
        // e.className="active"
        setSelectedIndex(index)
        action(false,item)
    }
    return (
        
       <ul className="subcolors">
            {data?.map((item,i)=>(
                <li key={i} value={item.subColor__Id} 
                
                style={{
                    cursor: 'pointer',
                    border: selectedIndex === i ? '1px solid rgb(109, 105, 105)' : '',
                    borderRadius: '4px'
                }}
                onClick={()=>handleChange(item,i)}>
                    <div >
                        <img src={item.subColor__Image}  alt="" srcSet="" />
                    </div>
                    <p>{item.subColor__Name}</p>
                </li>
            ))}
       </ul>
    )
}
export default ListSubColor