import React, { useState } from "react";
import "./LoginForm.css";
import { FaUser, FaEye, FaEyeSlash } from "react-icons/fa";
import { useNavigate }from "react-router-dom";
import { useUser } from "../UserContext/UserContext";

export const LoginForm = () => {
  const navigate = useNavigate();
  const { setUserID } = useUser();
  const [formData, setFormData] = useState({
    username: "",
    password: "",
  });

  const [showPassword, setShowPassword] = useState(false);

  const handleSubmit = async (event) => {
    event.preventDefault();
  
    if (formData.username.trim() === "" || formData.password.trim() === "") {
      alert("Please fill in all required fields.");
      return;
    }
  
    const options = {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(formData),
    };
  
    try {
      const response = await fetch("http://localhost:5213/api/user/getUser", options);
      
      if (response.ok) {
        const data = await response.json();
        setUserID(data.UserId);
        localStorage.setItem("userID", data.UserId); // Save userID to localStorage
        navigate("/Homepage");
      } else {
        alert("Invalid username or password.");
      }
    } catch (error) {
      console.error(error);
      alert("An error occurred. Please try again later.");
    }
  
    console.log("Form Data:", formData);
    setFormData({
      username: "",
      password: "",
    });
  };

  const handleChange = (event) => {
    const { name, value } = event.target;
    setFormData({
      ...formData,
      [name]: value,
    });
  };

  const togglePasswordVisibility = () => {
    setShowPassword(!showPassword);
  };

  return (
    <div className="wrapper">
      <form onSubmit={handleSubmit}>
        <h1>Login</h1>
        <div className="input-box">
          <input
            type="text"
            name="username"
            placeholder="Username"
            pattern=".{8,}"
            title="Reminder: eight or more characters"
            value={formData.username}
            onChange={handleChange}
            required
          />
          <FaUser className="icon" />
        </div>
        <div className="input-box">
          <input
            type={showPassword ? "text" : "password"}
            name="password"
            placeholder="Password"
            pattern="(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}"
            title="Reminder: must contain at least one number and one uppercase and lowercase letter, and at least 8 or more characters"
            value={formData.password}
            onChange={handleChange}
            required
          />
          {showPassword ? (
            <FaEyeSlash className="icon" id = "password-icon" onClick={togglePasswordVisibility} />
          ) : (
            <FaEye className="icon" id = "password-icon" onClick={togglePasswordVisibility} />
          )}
        </div>
        <button type="submit">Login</button>

        <div className="register-link">
          <p>
            Don't Have an Account? <a href="RegisterForm">Register</a>
          </p>
        </div>
      </form>
    </div>
  );
};
