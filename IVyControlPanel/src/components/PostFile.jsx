import axios from "axios";

// /**
//  * Function dùng chung để gửi yêu cầu PATCH.
//  * @param {string} url - URL của endpoint API.
//  * @param {Array} data - Dữ liệu JSON Patch (danh sách các operation).
//  * @returns {Promise<Object>} - Trả về dữ liệu từ server hoặc lỗi.
//  */
const postFile = async (url, data) => {

  try {
    const response = await axios.post(url, data, {
        headers: {
            "Content-Type": "multipart/form-data",
          },
    });
    return response; // Trả về dữ liệu sau khi cập nhật thành công
  } catch (error) {
    console.error("Error in post request:", error);
    return error; // Quăng lỗi để xử lý ở chỗ gọi hàm
  }
};

export default postFile;
