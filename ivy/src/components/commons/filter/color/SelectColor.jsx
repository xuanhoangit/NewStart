import { useRef } from "react"
function SelectColor(props) {
    const ref = useRef(null)
    function HandleChecked(event) {
        console.log(event.target.checked)
        if (event.target.checked) {
            ref.current.className = "bi bi-check2 active"
        } else {
            ref.current.className = ""
        }
    }
    return (
        <div>
            <label htmlFor={props.name} className="color">
                <div className={props.name}>
                    <img src={props.src} alt="" srcSet="" />
                    <div >
                        <input type="checkbox" onClick={HandleChecked} value="" name={props.name} id={props.name} />
                        <i ref={ref} className=""></i>
                    </div>
                </div>
            </label>
        </div>
    )
}
export default SelectColor