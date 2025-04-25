
import { useRef } from "react"
import "./size.css"
function SelectSize(props) {
    const ref = useRef(null)

    function HandleChecked(event) {
        console.log(event.target.checked)
        if (event.target.checked) {
            ref.current.className = "active"
        } else {
            ref.current.className = ""
        }
    }
    
    return (
        <div className="size">
            <label htmlFor={props.name}  >
                <div ref={ref} >
                    <div className="size-name">{props.sizename.toUpperCase()} </div>
                    
                    <div className={props.isUnavailable ? "slash" : ""}></div>
                    {props.isUnavailable ? "" :<input type={props.type} onClick={HandleChecked} value={props.value} name={props.name} id={props.name} />}
                </div>
            </label>
        </div>
    )
}
export default SelectSize