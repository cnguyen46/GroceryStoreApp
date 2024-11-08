import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { IoIosHome } from "react-icons/io";
import { useUser } from "../UserContext/UserContext";
import "./Cart.css";

export const Cart = () => {
  const [cartProducts, setCartProducts] = useState([]);
  const [cartProductDetails, setCartProductDetails] = useState([]);
  const [price, setPrice] = useState(0);
  const [loading, setLoading] = useState(true);
  const navigate = useNavigate();
  const { userID,setUserID } = useUser();

  useEffect(() => {
    const storedUserID = localStorage.getItem("userID");
    if (storedUserID) {
      setUserID(storedUserID);
    }
  }, [setUserID]);

  // When userID changes, update localStorage
  useEffect(() => {
    if (userID) {
      localStorage.setItem("userID", userID);
    }
  }, [userID]);

  useEffect(() => {
    const fetchCartData = async () => {
      try {
        const queryParams = new URLSearchParams({ userId: userID }).toString();
        const url = `http://localhost:5213/api/cart/getCartItems?${queryParams}`;
        const response = await fetch(url);

        if (response.ok) {
          const cartData = await response.json();
          setCartProducts(cartData);
        } else {
          console.error("Failed to fetch cart products:", response.status);
        }
      } catch (error) {
        console.error("Error fetching cart products:", error);
      }
    };

    fetchCartData();
  }, [userID]);

  useEffect(() => {
    const fetchProductDetails = async () => {
      try {
        const productDetails = await Promise.all(
          cartProducts.map(async (cartItem) => {
            const options = {
              method: 'POST',
              headers: {
                'Content-Type': 'application/json',
              },
              body: JSON.stringify({ "productID": cartItem.ProductID }),
            };
            const response = await fetch('http://localhost:5213/api/product/getProductsById', options);
            if (response.ok) {
              const productDetail = await response.json();
              return productDetail;
            } else {
              throw new Error('Error fetching product details');
            }
          })
        );
        setCartProductDetails(productDetails);
        setLoading(false); // Set loading to false once product details are fetched
      } catch (error) {
        console.error('Error fetching product details:', error);
      }
    };

    if (cartProducts.length > 0) {
      fetchProductDetails();
    }
  }, [cartProducts]);

  useEffect(() => {
    const fetchPriceData = async () => {
      try {
        const queryParams = new URLSearchParams({ userId: userID }).toString();
        const url = `http://localhost:5213/api/cart/getPrice?${queryParams}`;
        const response = await fetch(url);

        if (response.ok) {
          const priceData = await response.json();
          setPrice(priceData);
          setLoading(false); // Set loading to false once price data is fetched
        } else {
          console.error("Failed to fetch cart products:", response.status);
        }
      } catch (error) {
        console.error("Error fetching cart products:", error);
      }
    };

    fetchPriceData();
  }, [userID]);

  const goBack = () => {
    navigate("/Homepage");
  };

  const removeFromCart = async (productDetail) => {
    const formData = {
      productId: productDetail[0].ProductID,
    };
  
    const queryParams = new URLSearchParams({ userId: userID }).toString();
    const url = `http://localhost:5213/api/cart/removeItemFromCart?${queryParams}`;
  
    const options = {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(formData),
    };
  
    try {
      console.log("Removing item from cart:", formData);
      const response = await fetch(url, options);
  
      if (response.ok) {
        console.log("Item removed from cart successfully");
        const updatedCartProducts = cartProducts.map(item => {
          if (item.ProductID === productDetail[0].ProductID && item.Quantity > 1) {
            return { ...item, Quantity: item.Quantity - 1 };
          } 
          return item;
        });
        setCartProducts(updatedCartProducts);
        window.location.reload(); // Reload the page
      } else {
        console.error("Failed to remove item from cart:", response.status);
        alert("Failed to remove item from cart");
      }
    } catch (error) {
      console.error("Error removing item from cart:", error);
      alert("Error removing item from cart. Please try again later.");
    }
    
  };

  const GoToCheckout = async () => {
    try {
      const queryParams = new URLSearchParams({ userId: userID }).toString();
      const url = `http://localhost:5213/api/cart/checkout?${queryParams}`;
      const response = await fetch(url, { method: 'POST' });

      if (response.ok) {
        console.log('Checkout successful');
        navigate("/Checkout");
      } else {
        console.error("Checkout failed:", response.status);
      }
    } catch (error) {
      console.error("Error checking out:", error);
    }
  };

  return (
    <div>
      <button className="back-button" onClick={goBack}>
        <IoIosHome size={30} />
      </button>
      <div className="cart-container">
        <h1>Cart</h1>
        {loading ? (
          <p>Loading...</p>
        ) : (
          <div className="cart-products">
            {cartProductDetails.map((productDetail, index) => (
              <div key={index} className="cart-item">
                <h3>{productDetail[0].Name}</h3>
                <p>Price: ${productDetail[0].Price}</p> 
                <p>Quantity: {cartProducts[index].Quantity}</p>
                <button className="remove-button" onClick={() => removeFromCart(productDetail, cartProducts[index])}>Remove</button>
              </div>
            ))}
          </div>
        )}
        <div className="cart-total">
          <h2>Total Price: ${price.price}</h2>
        </div>
        <button className="checkout-button" onClick={GoToCheckout}>Checkout</button>
      </div>
    </div>
  );
};
