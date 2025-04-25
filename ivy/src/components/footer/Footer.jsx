import LinkAndBrand from "../footer/left/LinkAndBrand"
import Center from "../footer/center/Center"
import RegisterAndDownload from "../footer/right/RegisterAndDownload"
import CopyRight from "./copyRight/CopyRight"
import footer from "./Footer.module.css"
function Footer() {
    return (
        <>  
        <footer className={footer.footerSection}>
        <div id={footer.footer}>
            <section className={footer.left_section}>
                <LinkAndBrand hotline="0246 662 3434" />
            </section>
            <section className={footer.center_section}>
                <Center></Center>
            </section>
            <section className={footer.right_section}>
                <RegisterAndDownload title="Nhận thông tin các chương trình của IVY moda"/> 
            </section>
            </div>
            <CopyRight />
        </footer>
        </>
    )
}
export default Footer