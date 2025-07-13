import axios from "axios";
import { useState } from "react";

const get =async  (url) => {
  try {
    const response =await axios.get(url,{withCredentials: true});
    // console.log(response)
    return response // Cập nhật dữ liệu
    // console.log(response.data)
  } catch (err) {
    return err;
  }
};

export default get;
