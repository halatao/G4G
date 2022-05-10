import axios from "axios";
import React, { useEffect } from "react";
import CollapseCategory from "./CollapseCategory.js";

const Category = (props) => {
  const fetchData = () => {
    axios.get("https://localhost:7022/api/Categories").then((response) => {
      props.setCategories(response.data);
    });
  };

  useEffect(() => {
    fetchData();
  }, []);

  return (
    <div>
      {props.categories.map((category, index) => (
        <CollapseCategory key={index} category={category} setCategory={props.setCategory} />
      ))}
    </div>
  );
};

export default Category;
