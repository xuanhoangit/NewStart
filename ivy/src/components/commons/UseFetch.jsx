import { useState, useEffect } from 'react';

const useFetch = (url = 'hehehe', options = null) => {
    const [data, setData] = useState(null);
    const [error, setError] = useState(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const fetchData = async () => {
            try {
                setLoading(true); // Bắt đầu quá trình tải dữ liệu
                const response = await fetch(url, options);
                console.log(response)
                console.log(url)
                // console.log(options)
                if (!response.ok) {
                    throw new Error(`Lỗi HTTP! Trạng thái: ${response.status}`);
                }

                const result = await response.json();
                setData(result); // Đặt dữ liệu vào state
            } catch (error) {
                setError(error.message+" have no data"); // Xử lý lỗi nếu có
                
            } finally {
                setLoading(false); // Kết thúc quá trình tải
            }
        };

        fetchData();
    }, [url, options]);

    return { data, error, loading };
};

export default useFetch;
