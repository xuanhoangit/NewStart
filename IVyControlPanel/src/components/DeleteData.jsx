import axios from 'axios';

const deleteData = async (url) => {
  try {
    await axios.delete(url);
    console.log('Xóa thành công');
  } catch (error) {
    console.error('Lỗi khi xóa:', error.response?.data || error.message);
  }
};

export default deleteData;
