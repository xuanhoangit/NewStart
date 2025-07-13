import { useState } from "react"

function Display(params) {
    const [isShow,setisShow]=useState(true);
    const [color,setColor]=useState("hahaha");
    return isShow? (
            <>
            <div>AHHAHAH</div>
            <button onClick={()=>setisShow(change=>!change)}>Click Me</button>
            </>
    ):color
}
export default Display