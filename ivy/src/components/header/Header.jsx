import Logo from './logo/Logo';
import Nav from "./nav/Nav";
import Search from "./search/Search";
import Action from './action/Action'
import header from './Header.module.css'

function Header() {

    return (
    <section className={header.sectionHeader}>
        <header className={header.header}>
            <div className={header.nav_section}>
                <Nav login="Đăng nhập" products="Sản phẩm" sales="Sale" collections="Bộ sưu tập" about="Về chúng tôi"/>
            </div>
            <div className={header.logo_section}>
                <Logo />
            </div>
            <div className={header.search_and_action}>
                <div >
                    <Search placeholder="tìm kiếm sản phẩm"/>
                </div>
                <div className={header.action_section}>
                    <Action></Action>
                </div>
            </div>
        </header>
    </section>
    );
}
export default Header;