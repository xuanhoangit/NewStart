// import React, { useEffect, useRef, useState } from 'react';
// import Select from "react-select";
// import useFetch from '../../UseFetch';
// import { hostName } from '../../commons/HostName';
// import ItemSelected from '../../commons/ItemSelected/ItemSelected';
// import SelectVip from '../../commons/SelectVip/SelectVip';
// import "./Add.css"
// import put from '../../PutData';
// import HeaderForm from '../../commons/HeaderForm/HeaderForm';



// const ValidateProduct =async (product) => {
//   const errors = {};

//   if (!product.Product__Name) {
//     errors.Product__Name = 'Tên sản phẩm không được để trống';
//   }

//   // Kiểm tra các trường khác nếu cần thiết
//   if (product.Product__SeasonId ===0) {
//     errors.Product__SeasonId = 'Vui lòng chọn \'Mùa\'';
//   }

//   if (product.SubCategoryIds.length ===0) {
//     errors.SubCategoryIds = 'Vui lòng chọn \'Danh mục\'';
//   }

//   // if (product.CollectionIds.length ===0) {
//   //   errors.CollectionIds = 'Vui lòng chọn \'Bộ sưu tập\'';
//   // }

//   return errors;
// };

// const Update = ({showPopup,product}) => {
  
//   const categories=useFetch(`${hostName}/api/danh-muc`).data;
//   const collections=useFetch(`${hostName}/api/bo-suu-tap`).data;
 
//   const [collections_selected,setCollections_Selected]=useState([]);
//   const [subCategories_selected,setSubCategories_Selected]=useState([]);

//   const [message,setMessage]=useState();
//   const [errors,setError]=useState({})


//   // Khởi tạo các trạng thái để lưu trữ dữ liệu sản phẩm
//   const productDefault = {
//     Product__Name: "",
//     CollectionIds:[],
//     SubCategoryIds:[],
//     Product__SeasonId:0
//   };
//   const [product, setProduct]=useState(productDefault);
//   //Add to collection
//   const addCollection = (newCollection_Selected) => {
//     setCollections_Selected(prevState => [...prevState, newCollection_Selected]);
//   };

//   //xóa collection đã chọn
//   const removeCollectionSelected=(indexToRemove)=>{
//       setCollections_Selected(prev => 
//         prev.filter((_, index) => index !== indexToRemove)
//       );
//       product.CollectionIds.splice( indexToRemove,1)
//       // console.log(collections_selected)
//       // console.log(indexToRemove)
//       // console.log(product.CollectionIds)
//   }
//   //Xử lí khi chọn collection
//   const onChangeCollectionSelected=(event)=>{
//     var et=event.target;
//     if(et.value!==0){
//       //Kiểm tra xem có collection hay chưa 
//       const isExist=product.CollectionIds.includes(et.value)
//       if(!isExist){
//         product.CollectionIds.push(et.value)
//         const collection_name=et.options[et.selectedIndex].text;
//         addCollection(collection_name)
//         // console.log( product.CollectionIds)
//         // console.log( collections_selected)
//       }
//       et.value=0;
//     }
//   }
//   //Add to subcate
//   const addSubCate = (newSubCate_Selected) => {
//     setSubCategories_Selected(prevState => [...prevState, newSubCate_Selected]);
//   };
//   // xóa subcate
//   const removeSubCategorySelected=(indexToRemove)=>{
//       setSubCategories_Selected(prev => 
//         prev.filter((_, index) => index !== indexToRemove)
//       );
//       product.SubCategoryIds.splice( indexToRemove,1)
//       // console.log(subCategories_selected)
//       // console.log(indexToRemove)
//       // console.log(product.SubCategoryIds)
//   }
//   //Xử lí khi chọn subcate
//   const onChangeSubCategorySelected=(value,content)=>{

//     if(value!==0){
//       //Kiểm tra xem có collection hay chưa 
//       const isExist=product.SubCategoryIds.includes(value)
//       // console.log(isExist)
//       if(!isExist){
//         product.SubCategoryIds.push(value)
//         const subCate_name=content;
//         addSubCate(subCate_name)

//       }
//     }
//   }
//   // Hàm xử lý khi có sự thay đổi trong input

//   const handleChange = (e) => {
//     const { name, value } = e.target;
//     setProduct((prevProduct) => ({
//       ...prevProduct,
//       [name]: value
//     }));
//   };

//   // Hàm xử lý submit form
//   const handleSubmit = async (e) => {
//     e.preventDefault();
//     const er= await ValidateProduct(product)
//     setError(er);
//     // console.log(product)
//     if(Object.keys(er).length === 0){

//       const result=await post(`${hostName}/api/san-pham/them-moi`,product)
//       if(result.status==200||result.status==201){
//         setProducts(result.data)
//         showPopup("Thêm sản phẩm thành công","success")
//         console.log(result.data)
//       }else{
//           if(result.status==409){
//             showPopup("Sản phẩm đã tồn tại!","fail")
//           }
//           else if(result.status==500){
//             showPopup("Không thể thực hiện. Hệ thống lỗi!","fail")
//           }
//           else{
//             showPopup("Thêm sản phẩm không thành công!","fail")
//           }
//       }
//   }

//   };
  
//   return (
//     <>
    
//     <div>
//     <div>
//     {message}
//     </div>
//     <form onSubmit={handleSubmit}  id="form-add-product">
//     <HeaderForm title={"Thêm sản phẩm"}></HeaderForm>
//     <div className='layout-input'>
//     {/* <input type="hidden" name="product__Sold" value={0}/> */}

//     <div>
//         <label>
//             Tên sản phẩm:
//             <input  type="text"  name="Product__Name"  value={product.Product__Name}  onChange={handleChange}  />
//             <p className='error'>{errors.Product__Name}</p>
//         </label>
//     </div>
//     <div>
//         <label>
//             Danh mục:
//             <SelectVip data={categories} action={onChangeSubCategorySelected}/>
//             <div className=''>
//                 {subCategories_selected?.map((subcate,index)=>(
//                   <ItemSelected data={subcate} key={index} action={()=>{removeSubCategorySelected(index)}}></ItemSelected>
//                 ))}
//             </div>
//             <p className='error'>{errors.SubCategoryIds}</p>
//         </label>
       
//     </div>
//     <div>
//         <label>
//             Bộ sưu tập:
//               <select name="" id="" onChange={onChangeCollectionSelected}>
//                 <option value={0}>Chọn</option>
//                 {collections?.map((data,index)=>(
//                   <option key={index} value={data.collection__Id}>{data.collection__Name.toUpperCase()}</option>
//                 ))}
//               </select>
//               {/* Thẻ chứa các option vừa chọn */}
//               <div className=''>
//                 {collections_selected?.map((collection,index)=>(
//                   <ItemSelected data={collection} key={index} action={()=>{removeCollectionSelected(index)}}></ItemSelected>
//                 ))}
//               </div>
//             <p className='error'>{errors.CollectionIds}</p>
//         </label>
//     </div>
//     <div>
//     <label>
//          Mùa: 
//             <select  name="Product__SeasonId" value={product.Product__SeasonId} onChange={(event)=>{ handleChange(event)}} id="" >
//                 <option value={0}>Chọn</option>
//                 <option value={1}>Mùa THU ĐÔNG
//                 </option>
//                 <option value={2}>Mùa XUÂN HÈ</option>
//             </select>
//             <p className='error'>{errors.Product__SeasonId}</p>
//         </label>
//     </div>
//     </div>

//     </form>
//     </div>
    
//     </>
//   );
// };

// export default Update;
