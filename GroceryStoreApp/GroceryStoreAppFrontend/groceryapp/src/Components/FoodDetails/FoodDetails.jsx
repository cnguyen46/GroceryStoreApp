import React, { useState, useEffect } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { RiShoppingCart2Line } from "react-icons/ri";
import { Details } from "../FoodDetails/Details";
import "./FoodDetails.css";
import { IoIosHome } from "react-icons/io";


export const FoodDetails = () => {
  const { id } = useParams();
  const [productDetails, setProductDetails] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    const fetchData = async () => {
      const options = {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({ "productID": id }),
      };

      try {
        const response = await fetch('http://localhost:5213/api/product/getProductsById', options);
        if (response.ok) {
          const data = await response.json();
          setProductDetails(data);
        } else {
          throw new Error('Error fetching product details');
        }
      } catch (error) {
        console.error('Error fetching product details:', error);
      }
    };

    fetchData();
  }, [id]);

  if (!productDetails) {
    return <div>Loading...</div>;
  }

  const goToCart = () => {
    navigate("/cart");
  };

  const goBack = () => {
    navigate(-1);
  };

  return (
    <div className="food-details">
      <div className="topbar">
        <button className="back-button" onClick={goBack}>
          <IoIosHome size={30} />
        </button>
        <div className="cart-icon" onClick={goToCart}>
          <RiShoppingCart2Line size={30} />
        </div>
      </div>
      <div className="details">
        {productDetails.map((product) => (
          <Details 
            productId={product.ProductID}
            image={product.Image}
            category={product.Category}
            description={product.Description}
            manufacturer={product.Manufacturer}
            rating={product.Rating}
            SKU={product.SKU}
            weight={product.Weight}
            price={product.Price}
          />
        ))}
      </div>
    </div>
  );
};
