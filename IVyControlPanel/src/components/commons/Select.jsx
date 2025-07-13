import { useState } from "react";
function ImageUpload(event) {
   
    const file = event.target.files[0];
    if (file) {
        return  URL.createObjectURL(file);
    }
    return ""
}
export default ImageUpload