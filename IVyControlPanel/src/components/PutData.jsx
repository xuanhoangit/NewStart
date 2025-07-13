import axios from "axios";


const put = async (url, data) => {
  try {
       console.log("adad",data)
    const response = await axios.put(url, data);
    return response // Trả về dữ liệu sau khi cập nhật thành công
  } catch (error) {
    console.error("Error in Put request:", error);
    return error; // Quăng lỗi để xử lý ở chỗ gọi hàm
  }
};

export default put;
