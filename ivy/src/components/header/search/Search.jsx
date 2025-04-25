import "./search.css"
function Search(props) {
    return (
        <form action="" method="get" id="form-search">
                <i className="fa-solid fa-magnifying-glass fa-sm"></i>
                <input type="text" placeholder={props.placeholder.toUpperCase()} autoComplete='off' />
        </form>
    );
}
export default Search;