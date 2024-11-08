import React from "react";
import "./Details.css";
import { useUser } from "../UserContext/UserContext";

export const Details = ({ productId, category, description, image, manufacturer, name, price, rating, SKU, weight}) => {
    const { userID } = useUser();

    // Retrieve userID from localStorage if available
    const storedUserID = localStorage.getItem("userID");
    const userIdToUse = storedUserID ? storedUserID : userID;

    const addToCart = async () => {
        const formData = {
            productId: productId
        };

        const queryParams = new URLSearchParams({ userId: userIdToUse }).toString();
        const url = `http://localhost:5213/api/cart/addItemToCart?${queryParams}`;

        const options = {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(formData),
        };

        try {
            console.log("Adding item to cart:", formData, options)
            const response = await fetch(url, options);

            if (response.ok) {
                console.log("Item added to cart successfully");
                alert("Item added to cart successfully");
            } else {
                console.error("Failed to add item to cart:", response.status);
                alert("Failed to add item to cart");
            }
        } catch (error) {
            console.error("Error adding item to cart:", error);
            alert("Error adding item to cart. Please try again later.");
        }
    };

    return (
        <div className="detail-wrapper">
            <div className="information">
                <img className="food-image" src={image} alt={name} />
                <p>Category: {category}</p>
                <h2>{name}</h2>
                <p>Description: {description}</p>
                <p>Manufacturer: {manufacturer}</p>
                <p>Rating: {rating}</p>
                <p>SKU: {SKU}</p>
                <p>Weight: {weight}</p>
                <p>Price: {price}</p>
                <button onClick={addToCart}>Add to Cart</button>
            </div>
        </div>
    );
};
