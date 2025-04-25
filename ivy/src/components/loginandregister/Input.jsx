import ip from"./Input.module.css"
function Input(prop) {

   
    // if(prop.id=="dateofbirth"){
    //     const changeInputDate=(e)=>{
    //         e.target.type="date"
    //         console.log(e.target.value)
    //         if(e.target.value==""){
    //             e.target.type = "text"
    //         }
    //     }
    //     return (
    //         <div>
    //             <input onFocus={changeInputDate} className={ip.input} type={prop.type} name={prop.name} placeholder={prop.placeholder} value={prop.value} id={prop.id} autoComplete={prop.autoComplete}/>
    //         </div>
    //     )
    // }
    return (
        <input className={ip.input} type={prop.type} name={prop.name} placeholder={prop.placeholder} value={prop.value} id={prop.id} autoComplete={prop.autoComplete}/>
    )
}
export default Input