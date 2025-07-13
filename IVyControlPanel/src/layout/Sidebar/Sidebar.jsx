import { useEffect, useState } from 'react';
import { personsImgs } from '../../utils/images';
import { navigationLinks } from '../../data/data';
import "./Sidebar.css";
import { useContext } from 'react';
import { SidebarContext } from '../../context/sidebarContext';
import { Link } from 'react-router-dom';
import { useAuth } from '../../AuthProvider';
import { admin ,humanSourceManager} from '../../components/Roles';

const Sidebar = () => {
  const [activeLinkIdx,setActiveLinkIdx] = useState(1);
  const [sidebarClass, setSidebarClass] = useState("");
  const { isSidebarOpen } = useContext(SidebarContext);
  const {user}=useAuth()

  useEffect(() => {
    if(isSidebarOpen){
      setSidebarClass('sidebar-change');
    } else {
      setSidebarClass('');
    }
  }, [isSidebarOpen]);

  return (
    <div className={ `sidebar ${sidebarClass}` }>
      <Link to="/me">
      <div className="user-info">
          <div className="info-img img-fit-cover">
              <img src={ user?.avatar} alt="profile image" />
          </div>
          <span className="info-name">{user?.fullName}</span>
          
      </div>
      </Link>
      {/* <div>{user?.roles.map((role,i)=>(
          <span key={i}>{role}</span>
      ))}</div> */}
      <nav className="navigation">
          <ul className="nav-list">
            {
              navigationLinks.map((navigationLink) => (

                <li className="nav-item" key = { navigationLink.id } onClick={()=>setActiveLinkIdx(navigationLink.id)}>
                  {!user?.roles.includes(admin) && !user?.roles.includes(humanSourceManager) && navigationLink.id==3?null:
                  <Link to={navigationLink.link}  className={ `nav-link ${ navigationLink.id === activeLinkIdx ? 'active' : null }` }>
                      <img src={ navigationLink.image } className="nav-link-icon" alt = { navigationLink.title } />
                      <span className="nav-link-text">{ navigationLink.title }</span>
                  </Link>}
                </li>
              ))
            }
          </ul>
      </nav>
    </div>
  )
}

export default Sidebar
