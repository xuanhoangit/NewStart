// AuthContext.js
import { createContext, useContext, useEffect, useState } from "react";

import api from "./AxiosInstance";


// ✅ Khởi tạo context
const AuthContext = createContext(null);

// ✅ Khai báo function trước, rồi mới export
function AuthProvider({ children }) {
    const [user, setUser] = useState(null);
    const [loading, setLoading] = useState(true);
    
     // Hàm logout
    const logout = async () => {
        try {
        await api.post('/api/account/logout'); // Gọi API xóa cookie
        } catch (err) {
        console.error('Logout error', err);
        } finally {
        setUser(null); // Xóa user trong context
        }
    };
    useEffect(() => {
    const checkLogin = async () => {
        try {
        const resMe = await api.get('/api/account/me');
        console.log(resMe.data)
        setUser(resMe.data); // ✅ Interceptor tự refresh nếu 401
        } catch (err) {
        console.error("Không thể lấy thông tin người dùng:", err);
        setUser(null); // ❌ Nếu refresh thất bại
        } finally {
        setLoading(false);
        }
    };

    checkLogin();
    }, []);

    return (
        <AuthContext.Provider value={{ user, setUser, loading,logout }}>
             { children}
        </AuthContext.Provider>
    );
}


function useAuth() {
    return useContext(AuthContext);
}

// ✅ Named export theo chuẩn Vite HMR
export { AuthProvider, useAuth };
