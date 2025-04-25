import Black from "../../../../assets/images/colors-group-img/black.png"
import White from "../../../../assets/images/colors-group-img/white.png"
import Blue from "../../../../assets/images/colors-group-img/blue.png"
import Yellow from "../../../../assets/images/colors-group-img/yellow.png"
import Pink from "../../../../assets/images/colors-group-img/pink.png"
import Red from "../../../../assets/images/colors-group-img/red.png"
import Gray from "../../../../assets/images/colors-group-img/gray.png"
import Beige from "../../../../assets/images/colors-group-img/beige.png"
import Brown from "../../../../assets/images/colors-group-img/brown.png"
import Green from "../../../../assets/images/colors-group-img/green.png"
import Orange from "../../../../assets/images/colors-group-img/orange.png"
import Purple from "../../../../assets/images/colors-group-img/purple.png"

import SelectColor from "./SelectColor"
import "./color.css"
const list_colors =[
    { name: "black", src: `${Black}` },
    { name: "white", src: `${White}` },
    { name: "blue", src: `${Blue}` },
    { name: "yellow", src: `${Yellow}` },
    { name: "pink", src: `${Pink}` },
    { name: "red", src: `${Red}` },
    { name: "gray", src: `${Gray}` },
    { name: "beige", src: `${Beige}` },
    { name: "brown", src: `${Brown}` },
    { name: "green", src: `${Green}` },
    { name: "orange", src: `${Orange}` },
    { name: "purple", src: `${Purple}` },
]

function Color(props) {

    return (
        <ul className="list-colors">
            {list_colors.map((color, index) => (
                <li key={index} >
                    <SelectColor name={color.name} src={color.src} />
                </li>
            ))}
        </ul>
    )
}
export default Color
