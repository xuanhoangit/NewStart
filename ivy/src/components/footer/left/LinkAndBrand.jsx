import { Link } from 'react-router-dom';
import './left.css'
function LinkAndBrand(props) {
    return (
        <>	
			<div className='left'>
			<p></p>
				<div  className="topleft">
                        <div className="logo-footer">
                            <Link to="/">
                                <img src="https://pubcdn.ivymoda.com/ivy2/images/logo-footer.png" alt="logo-footer" className="lazy" loading="lazy" />
						</Link>
                        </div>
                        <Link to="https://www.dmca.com/Protection/Status.aspx?ID=0cfdeac4-6e7f-4fca-941f-57a0a0962777&amp;refurl=https://ivymoda.com/" target="_blank" title="DMCA.com Protection Status" className="dmca-badge">
                            <img src="https://pubcdn.ivymoda.com/ivy2/images/dmca.png" alt="DMCA.com Protection Status" className="lazy" loading="lazy" />
                        </Link>
                        <div className="logo-conthuong">
                            <Link target="_blank" to="http://online.gov.vn/Home/WebDetails/36596">
                                <img src="https://pubcdn.ivymoda.com/ivy2/images/img-congthuong.png" alt="img-congthuong" className="lazy" loading="lazy" />
                            </Link>
                        </div>
				</div>
				<div className="media">
					<ul className="listmedia">
						<li>
							<Link to="https://www.facebook.com/thoitrangivymoda/" target="_blank">
								<img src="https://pubcdn.ivymoda.com/ivy2/images/ic_fb.svg" alt="ic_fb" className="lazy" loading="lazy" />
							</Link>
						</li>
						<li>
							<Link to="https://ivymoda.com/" target="_blank">
								<img src="https://pubcdn.ivymoda.com/ivy2/images/ic_gg.svg" alt="ic_gg" className="lazy" loading="lazy" />
							</Link>
						</li>
						<li>
							<Link to="https://www.instagram.com/ivy_moda/" target="_blank">
								<img src="https://pubcdn.ivymoda.com/ivy2/images/ic_instagram.svg" alt="ic_instagram" style={{height:"30px"}} className="lazy" loading="lazy" />
							</Link>
						</li>
						<li>
							<Link to="https://www.pinterest.com/ivymoda/_saved/" target="_blank">
								<img src="https://pubcdn.ivymoda.com/ivy2/images/ic_pinterest.svg" alt="ic_pinterest" style={{height: "27px"}} className="lazy" loading="lazy" />
							</Link>
						</li>
						<li>
							<Link to="https://www.youtube.com/user/thoitrangivymoda" target="_blank">
								<img src="https://pubcdn.ivymoda.com/ivy2/images/ic_ytb.svg" alt="ic_ytb" className="lazy" loading="lazy" />
							</Link>
						</li>
					</ul>
					<div className="hotline">
						<Link to="tel:02466623434">Hotline: {props.hotline}</Link>
					</div>
				</div>
				</div>
        </>
    );
}
export default LinkAndBrand