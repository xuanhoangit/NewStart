import axios from "axios";

const post = async (url, data) => {

  try {
    const response = await axios.post(url, data, {
      withCredentials: true,
      // headers: {
      //   // Không set Content-Type thủ công!
      //   // Browser sẽ tự set `multipart/form-data` với boundary chính xác
      // }
  });
    // console.log(response)
    return response;
  } catch (error) {
    return error;
  }
};

export default post;
