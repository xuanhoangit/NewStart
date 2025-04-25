import "./size.css"
import SelectSize from "./SelectSize"
function Size(props) {
    return (
        <>
            <ul className="list-sizes">
                <li>
                    <SelectSize type="checkbox" sizename="s" isUnavailable={false} value="s" name="s" />
                </li>
                <li>
                    <SelectSize type="checkbox" sizename="m" isUnavailable={false} value="m" name="m" />
                </li>
                <li>
                    <SelectSize type="checkbox" sizename="l" isUnavailable={false} value="l" name="l" />
                </li>
                <li>
                    <SelectSize type="checkbox" sizename="xl" isUnavailable={false} value="xl" name="xl" />
                </li>
                <li>
                    <SelectSize type="checkbox" sizename="xxl" isUnavailable={false} value="xxl" name="xxl" />
                </li>
            </ul>
        </>
    )
}
export default Size