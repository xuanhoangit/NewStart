import { Link } from 'react-router-dom';
import './logo.css'
function Logo(params) {
    return(
        <div className='logo'>
            <Link to="/">
                <img src="https://pubcdn.ivymoda.com/ivy2/images/logo.png" />
            </Link>
        </div>
    );
}
export default Logo;