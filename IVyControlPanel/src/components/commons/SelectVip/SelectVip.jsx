
import "./SelectVip.css";
import { useRef, useState ,useEffect} from "react";
const SelectVip=({data,action})=>{
  const [isShow,setIsShow]=useState(false)
  const contentRef=useRef(null)
  useEffect(() => {
    const handleClickOutside = (event) => {
      if (contentRef.current && !contentRef.current.contains(event.target)) {
        setIsShow(false); // Click ngoài => ẩn
      }
    };
    console.log(isShow)
    document.addEventListener('mousedown', handleClickOutside);
    
    return () => {
      document.removeEventListener('mousedown', handleClickOutside);
    };
  }, []);
  return (
    <div ref={contentRef}  className="selectvip" onClick={()=>setIsShow(change=>!change)}>
        <div style={{display:"flex",justifyContent:"space-between"}}><div style={{flex:1}}>Chọn</div> <div style={{paddingRight:"3px"}}><i className="fa-solid fa-angle-down fa-sm"  ></i></div></div>
      <ul style={{display:isShow?"block":"none"}} className="cates">
        <li value={0} style={{textTransform:"capitalize"}}>Chọn</li>
        {
          data?.map((category,i)=>(
            <li key={i}>{category.category__Name}
              <ul className="subcates">
                  {category.subCategories?.map((subcate,j)=>(
                    <li key={j} value={subcate.subCategory__Id} onClick={(e)=>{action(e.target.value,category.category__Name+" > "+subcate.subCategory__Name)}}>{subcate.subCategory__Name}</li>
                  ))}
              </ul>
            </li>
          ))
        }
      </ul>
    </div>
  )
}
export default SelectVip;