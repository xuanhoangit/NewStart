import "./ItemSelected.css"
const ItemSelected = ({data,action})=>{
    return (
        <div className="item_selected" onClick={action}>
            <div>{data}</div>
            <div><i className="bi bi-x-lg"></i></div>
        </div>
    )
}
export default ItemSelected
