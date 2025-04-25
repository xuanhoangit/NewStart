import mb from "./MainButton.module.css"
function MainButton(props) {
    return (
        <button id={mb.mainbutton} type={props.type}>{props.text}</button>
    )
}
export default MainButton