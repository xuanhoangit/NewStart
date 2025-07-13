import { Link } from "react-router-dom";
import './NotFound.css';

export default function NotFound() {
  return (
    <div className="notfound-container">
      <h1 className="notfound-title">404</h1>
      <p className="notfound-text">Trang bạn tìm không tồn tại</p>
      <p className="notfound-sub">Có thể đường dẫn sai hoặc trang đã bị xoá.</p>
      <Link to="/" className="notfound-button">
        Về trang chủ
      </Link>
    </div>
  );
}
