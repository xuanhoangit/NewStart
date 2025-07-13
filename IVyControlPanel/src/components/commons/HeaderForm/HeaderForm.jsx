import "./HeaderForm.css"
const HeaderForm=({title})=>{
    return (
            <div className='header-form'>      
                <button type="submit"><i className="bi bi-check-lg"></i></button>
                <h2>{title}</h2>
            </div>
    )
}
export default HeaderForm