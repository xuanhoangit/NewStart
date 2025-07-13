import axios from "axios";


const patch = async (url, data) => {
  try {
       console.log("adad",data)
    const response = await axios.patch(url, data, {
      headers: {
        "Content-Type": "application/json-patch+json",
      },
    });
    return response // Trả về dữ liệu sau khi cập nhật thành công
  } catch (error) {
    console.error("Error in PATCH request:", error);
    return error; // Quăng lỗi để xử lý ở chỗ gọi hàm
  }
};

export default patch;
