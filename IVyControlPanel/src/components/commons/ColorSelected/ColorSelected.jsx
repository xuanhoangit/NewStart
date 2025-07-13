import "./ColorSelected.css"
const ColorSelected = ({action,data})=>{
    // console.log(data)
    return (
        <div className="color_selected" onClick={action}>
            <div className="color_img" style={{backgroundImage:`url(${data.Color__Url})`}}></div>
            <div className="color_name">{data.Color__Name}</div>
            <div className="remove"><i className="bi bi-x-lg"></i></div>
        </div>
    )
}
export default ColorSelected