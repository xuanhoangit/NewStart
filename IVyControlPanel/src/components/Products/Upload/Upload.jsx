import React, { useEffect, useRef, useState,Suspense } from "react";
import { base64 } from "../../commons/Base64"
import ConvertToVND from "../../commons/ConvertToVND"
import Slide from "./Slide";
import postFile from "../../PostFile";
import "./Upload.css"
import get from "../../GetData";
import deleteData from "../../DeleteData";
import Message from "../../commons/Message/Message";
import { hostNameHttp } from "../../commons/HostName";
import FancyBox from "../../commons/Fancybox/FancyBox";
import post from "../../PostData";
import SortableList from "./SortableList";



 function Upload({product,showPopup,actionAfterUpdateFile}) {
  const [undo, setUndo] = useState([{
    images:[],
    trashImages:[]
  }]);

  const  [currentIndex,setCurrentIndex]=useState(0);

  const [error, setError] = useState({});

  const inputFileRef=useRef();

//Khi có sự thay đổi hình ảnh: lập tức tạo 1 bản ghi và index ++
// back (index > 0) => index-- ,next (index< undo.length-1) =>index ++
// Khi back next phải set image
//SỬa lại
//Đây là khi back next

  const createNewRecord=(newRecord)=>{
      if(newRecord.trashImages==null){
          newRecord.trashImages=undo[currentIndex].trashImages
      }
     const newRecords = [
      ...undo.slice(0, currentIndex + 1), // từ đầu đến ngay sau phần tử hiện tại
      newRecord,
      ...undo.slice(currentIndex + 1), // phần còn lại
    ];

    setUndo(newRecords);
    setCurrentIndex(currentIndex+1)
    console.log("undo",undo)
    console.log("currentIndex",currentIndex)
    }

  const Back=()=>{
     if(currentIndex>=1){
      setCurrentIndex(currentIndex-1)
       console.log("undo",undo)
       console.log("currentIndex",currentIndex)
    }
  }
  const Next=()=>{
     if(currentIndex<undo.length-1){
      console.log("currentIndex",currentIndex)
      setCurrentIndex(currentIndex+1)
       console.log("undo",undo)
       console.log("currentIndex",currentIndex)
    }
  }
   useEffect( ()=>{
     setError({})
     const getImages=async()=>{
       const result=await get(`${hostNameHttp}/api/hinh-anh-san-pham/${product.productSubColor__Id}`);
       setUndo([result?.length>0?{
        images:result,
        trashImages:[]
      }:{
        images:[],
        trashImages:[]
      }])
    } 
    getImages()
  },[product.productSubColor__Id])

  const [isDragging, setIsDragging] = useState(false);
  const handleImageChange = (e) => {
    const files = Array.from(e.target.files || e.dataTransfer.files);
    const newCurrentImages = files.map((file) => ({
      file,
      productSubColorFile__Name: URL.createObjectURL(file),
    }));

    const currentRecord = undo[currentIndex];
    const newImages = [...currentRecord.images, ...newCurrentImages];

    const newRecord = {
      images: newImages,
      trashImages: currentRecord.trashImages,
    };

    createNewRecord(newRecord);

    // Reset input để chọn lại ảnh giống nhau nếu muốn
    if (e.target.value) e.target.value = null;
  };

  const handleDrop = (e) => {
    e.preventDefault();
    setIsDragging(false);
    handleImageChange(e);
  };
  // const handleImageChange = (e) => {
  //   const files = Array.from(e.target.files);
  //   const newCurrentImages = files.map((file) => ({
  //     file,
  //     productSubColorFile__Name: URL.createObjectURL(file),
  //      // Tạo URL để hiển thị preview
  //   }));
  
  //   const newImages= [...undo[currentIndex].images, ...newCurrentImages]; // thêm cuối
  //   const newRecord={
  //       images:newImages,
  //       trashImages:undo[currentIndex].trashImages
  //     }
  //   createNewRecord(newRecord)
 
  // };

  const resetUndo=(file)=>{
    console.log("file",file)
    setCurrentIndex(0)
    setUndo(file!=undefined?[{
        images:file,
        trashImages:[]
      }]:[undo[currentIndex-currentIndex]])
  }
  //THực hiện xóa hình ảnh
  const handleRemoveImage = (removeIndex) => {
        const newImages=undo[currentIndex].images.filter((_, index) => index !== removeIndex);
        //nếu không sử dụng trashImage thì
        const newRecord={
            images:newImages
          }
        if(undo[currentIndex].images[removeIndex].productSubColorFile__Index!=undefined){
            const remove = [...undo[currentIndex].trashImages, undo[currentIndex].images[removeIndex]]; // thêm cuối
            console.log("remove",remove)
            newRecord.trashImages=remove
          }
          console.log("mewee",newImages)
          createNewRecord(newRecord)
          
  };
  const submit=async ()=>{

        const fileAdds=[];
        const fileUpdates=[]

       undo[currentIndex].images.forEach((img,i)=>{
        // console.log(img)
        img.productSubColorFile__Index=i+1
        //THêm mới
        if(img.productSubColorFile__Id==undefined){
            fileAdds.push({
              FileImage:img.file,
              ProductSubColorFile__Index:img.productSubColorFile__Index,
              ProductSubColorFile__ProductSubColorId:product.productSubColor__Id,
            })
          
        }
        //Cập nhật
        if(img.productSubColorFile__Id>0){
            fileUpdates.push(img)
        }
      })
      
      const formData=new FormData();
      fileAdds.forEach((item, i) => {
        formData.append(`FileAdds[${i}].FileImage`, item.FileImage);
        formData.append(`FileAdds[${i}].ProductSubColorFile__ProductSubColorId`, product.productSubColor__Id);
        formData.append(`FileAdds[${i}].ProductSubColorFile__Index`, item.ProductSubColorFile__Index);
      });
      fileUpdates.forEach((item,i)=>{
        formData.append(`FileUpdates[${i}].productSubColorFile__Id`,item.productSubColorFile__Id)
        formData.append(`FileUpdates[${i}].productSubColorFile__Index`,item.productSubColorFile__Index)
      })
      undo[currentIndex].trashImages.map(item => item.productSubColorFile__Id).forEach((item,i)=>{
        formData.append(`RemoveIds[${i}]`,item)
      })
      formData.append("Psc_Id",product.productSubColor__Id)
      console.log("add",fileAdds)
      console.log("update",fileUpdates)


     
     
      const result= await postFile(`${hostNameHttp}/api/hinh-anh-san-pham/cap-nhat`,formData)
      resetUndo(result.data?.files)
      // setIsVisible(false)

       if(result.status==200||result.status==201){
            if(result.data.files.length>1)
            {
              actionAfterUpdateFile([result.data?.files[0],result.data?.files[1]])
            }else{
              actionAfterUpdateFile([])
            }
            showPopup("Cập nhật hình ảnh thành công","success")
            console.log(result.status)
        }else{
            if(result.status!=500){
                showPopup("Cập nhật hình ảnh không thành công","fail")
            }else{
                showPopup("Không thể thực hiện. Hệ thống lỗi!","fail")
            }
        }
  }
  const handleSubmit=async (e)=>{
    e.preventDefault();
    
    // setImages(result.fileUrls);
  }

  return (
            <>  
            <form action="" onSubmit={handleSubmit} id="form-upload-file"  encType="multipart/form-data">
                {/* <input type="hidden" name="productSubColor__Id" value={productFile.productSubColor__Id}/> */}
                <div className="header">
                     <div className="d-product">
                        <div>
                            <div>Tên sản phẩm: {product.product__Name}</div> 
                        </div>
                        <div className="d-color">
                            <div>Màu sắc: <img style={{borderRadius:"50%",margin:"0 5px"}} width={"20px"} height={"20px"} src={product.subColorGetDTO.subColor__Image} alt="" />  ({product.subColorGetDTO.subColor__Name})</div>
                        </div>
                    </div>
                    <h2 style={{textDecoration:"underline"}}>Cập nhật hình ảnh</h2>
                   
                </div>
                <div>
          
          <div style={{display:"flex",justifyContent:"space-between"}}>
            <div style={{display:"flex",alignItems:"end"}}>Demo: 
            </div>
              
          <div className="file-action" >
            
            <button onClick={Back} type="button" className={currentIndex<=0?"disabled":""}>
              <i className="bi bi-arrow-left"></i>
            </button>
            <button  onClick={Next} type="button" className={currentIndex>=undo.length-1?"disabled":""}>  
              <i className="bi bi-arrow-right"></i>
            </button>
            <button className={currentIndex==0?"disabled":""} onClick={()=>{currentIndex!=0?resetUndo():null}} >
              <i className="bi bi-arrow-clockwise"></i>
            </button>
            {currentIndex==0?<button className="disabled">Lưu</button>:<FancyBox content={submit} ></FancyBox>}
            
            </div>
          </div>
          <div className="error">{error.file}</div>
              <div className="preview" style={{position:"relative"}}>
         
                    <div className="slide">
                      <Suspense>
                        {undo[currentIndex]?.images.length>0?<Slide imgs={undo[currentIndex]?.images}></Slide>:""}
                      </Suspense>
                    </div>
                    <div className="list-image" >
                      <Suspense>
                        {undo[currentIndex]?.images.length>0?<SortableList remove={handleRemoveImage} images={undo[currentIndex]?.images} createNewRecord={createNewRecord}></SortableList>:"Không có hình ảnh"}
                      </Suspense>
                  <button className="btn-upload" onClick={() => inputFileRef.current.click()}
                  onDrop={handleDrop}
                  onDragOver={(e) => {
                    e.preventDefault();
                    setIsDragging(true);
                  }}
                  onDragLeave={() => setIsDragging(false)}
                  style={{
                    border: '2px dashed #aaa',
                    borderRadius: '8px',
                    padding: '20px',
                    width:"100%",
                    textAlign: 'center',
                    backgroundColor: isDragging ? '#f0f0f0' : '#fff',
                    cursor: 'pointer',
                    transition: 'background-color 0.2s ease-in-out',
                    color:"green",
                    margin:"5px"
                  }} disabled 
                  // style={{padding:"0",cursor:"pointer"}}
                  >                      
                <label htmlFor="selectFile" >    
                        <div style={{padding:"5px 10px"}}>
                         Tải lên <i  className="bi bi-upload"></i>
                        </div>
                        <input ref={inputFileRef} type="file" style={{visibility:"hidden"}} accept="image/*" id="selectFile" multiple onChange={(e)=>{handleImageChange(e)}}/>
                </label>
              </button> 
                      </div>
                </div>
          </div>
            </form>
            </>
   
  );
}

export default Upload;
