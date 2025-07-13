import React, { useState } from 'react';
import './SortableList.css';

const SortableList = ({images,createNewRecord,remove}) => {
 

  const [dragIndex, setDragIndex] = useState(null);
  const [dragging, setDragging] = useState(false);

  const handleDragStart = (index) => {
    setDragIndex(index);
    setDragging(true);
  };

  const handleDragOver = (e) => {
    e.preventDefault();
  };

  const handleDrop = (dropIndex) => {
    if (dragIndex === null || dragIndex === dropIndex) return;

    const updatedItems = [...images];
    const draggedItem = updatedItems.splice(dragIndex, 1)[0];
    updatedItems.splice(dropIndex, 0, draggedItem);

    const newRecord={
      images:updatedItems,
      trashImages:null
    }
    createNewRecord(newRecord);
    setDragIndex(null);
    setDragging(false);
  };

  const handleDragEnd = () => {
    setDragging(false);
  };

  // const handleRemoveImage = (index) => {
  //   const updatedItems = items.filter((_, i) => i !== index);
  //   setImages(updatedItems);
  // };

  return (
    < >
      {images.map((item, index) => (
        <div
          key={index}
          draggable
          onDragStart={() => handleDragStart(index)}
          onDragOver={handleDragOver}
          onDrop={() => handleDrop(index)}
          onDragEnd={handleDragEnd}
          className={`image-item ${dragging && dragIndex === index ? 'dragging' : ''}`}
        >
          <img
            src={item.productSubColorFile__Name}
            alt={`Preview ${index}`}
            className="image"
          />
          <div className="image-index">{index + 1}</div>
          <div  className="btn-rm-img" onClick={() => remove(index)}>
            <i className="bi bi-x"></i>
          </div>
        </div>
      ))}
    </>
  );
};

export default SortableList;
