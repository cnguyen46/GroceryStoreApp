import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import "./Homepage.css";
import { HomepageProducts } from "../Homepage/HomepageProducts";
import { RiShoppingCart2Line } from "react-icons/ri";
import { useUser } from "../UserContext/UserContext"; 

export const Homepage = () => {
  const [foodProducts, setFoodProducts] = useState([]);
  const [selectedCategory, setSelectedCategory] = useState("all");
  const navigate = useNavigate();
  const { setUserID } = useUser();

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await fetch(
          "http://localhost:5213/api/product/getProducts"
        );
        const data = await response.json();
        setFoodProducts(data);
      } catch (error) {
        console.error("Error fetching products:", error);
      }
    };

    fetchData();
  }, []);

  const handleImageClick = (category, ID) => {
    navigate(`/food/${category}/${ID}`);
  };

  const goToCart = () => {
    navigate("/cart");
  };

  const handleLogout = () => {
   localStorage.removeItem("userID");
   setUserID(null);
   navigate("/");
  };

  const filteredProducts = () => {
    if (selectedCategory === "all") {
      return foodProducts;
    } else {
      return foodProducts.filter(
        (product) => product.Category === selectedCategory
      );
    }
  };

  return (
    <div className="homepage">
      <h1>Grocery Store</h1>
      <div className="logout-button" onClick={handleLogout}>
        Logout
      </div>
      <div className="cart-icon" onClick={goToCart}>
        <RiShoppingCart2Line size={30} />
      </div>
      <div className="category-filter">
        <label>Filter by Category:</label>
        <select
          value={selectedCategory}
          onChange={(e) => setSelectedCategory(e.target.value)}
        >
          <option value="all">All</option>
          <option value="Vegetable">Vegetables</option>
          <option value="Fruit">Fruits</option>
          <option value="Frozen Food">Frozen Foods</option>
          <option value="Beverage">Beverages</option>
        </select>
      </div>
      <div className="food-products">
        {filteredProducts().map((product) => (
          <div key={product.productID} className="product-item">
            <img
              className="food-image"
              src={product.Image}
              alt={product.Name}
              onClick={() =>
                handleImageClick(product.Category, product.ProductID)
              }
            />
            <HomepageProducts name={product.Name} price={product.Price} productId={product.ProductID}/>
          </div>
        ))}
      </div>
    </div>
  );
};
