import axios from "axios";


const patchFile = async (url, data) => {
  try {
 
    const response = await axios.patch(url, data, {
      headers: {
        "Content-Type": "multipart/form-data",
      },
    });
    return response // Trả về dữ liệu sau khi cập nhật thành công
  } catch (error) {

    return error; // Quăng lỗi để xử lý ở chỗ gọi hàm
  }
};

export default patchFile;
