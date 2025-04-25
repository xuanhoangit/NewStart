
function Discount(props) {
    const discount={
        width: "40px",
        height: "40px",
        position: "absolute",
        borderRadius: "50% 50% 0 50%",
        backgroundColor: "#e26234",
        fontSize: "12px",
        fontWeight: "bold",
        color: "white",
        display: "flex",
        zIndex: "1",
        right: "10px",
        top:"10px",
    }
    const child={
        margin: "auto",
        display: "flex",
        alignItems:"flex-start",
    }
    const child_span={
        fontSize: "8px",
    }
    return (
        <div style={discount}>
            <div style={child}>{props.discount}<span style={child_span}>%</span></div>
        </div>
    )    
}
export default Discount