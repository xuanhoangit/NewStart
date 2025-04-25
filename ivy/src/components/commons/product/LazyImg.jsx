
function LazyImg(props) {
    return (
            <img style={props.style} src={props.src} id={props.id} className={props.className} alt={props.alt} width={props.width} height={props.height} srcSet={props.srcSet}/>
    )
}
export default LazyImg