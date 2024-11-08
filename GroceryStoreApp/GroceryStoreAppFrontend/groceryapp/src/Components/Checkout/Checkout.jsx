import React from "react";
import "./Checkout.css";
import { useNavigate } from "react-router-dom";
import { useUser } from "../UserContext/UserContext";

export const Checkout = ({ history }) => {
  const navigate = useNavigate();
  const { setUserID } = useUser();


  const goHome = () => {
    navigate("/Homepage");
  };

  const logout = () => {
    setUserID(null);
    navigate("/");
  };

  return (
    <div className="checkout">
      <h1>Thanks for your order!</h1>
      <button className="continue" onClick={goHome}>Continue Shopping?</button>
      <button className="logout" onClick={logout}>Logout</button>
      </div>
  );
};
