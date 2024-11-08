import React, { useState } from "react";
import "./RegisterForm.css";
import { FaEye, FaEyeSlash } from "react-icons/fa";
import { useNavigate } from "react-router-dom";

export const RegisterForm = ( { history } ) => {
  const navigate = useNavigate();
  /*sets up the initial state for your form data, where all form fields are 
  initially empty strings, and provides a function to update this state */
  const [formData, setFormData] = useState({
    firstName: "",
    lastName: "",
    email: "",
    phoneNumber: "",
    username: "",
    password: "",
  });

  const [showPassword, setShowPassword] = useState(false);

  const handleSubmit = (event) => {
    event.preventDefault();

    /*Verifies that the form fields are full with data */
    if (
      formData.firstName.trim() === "" ||
      formData.lastName.trim() === "" ||
      formData.email.trim() === "" ||
      formData.phoneNumber.trim() === "" ||
      formData.username.trim() === "" ||
      formData.password.trim() === ""
    ) {
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

    fetch("http://localhost:5213/api/user/addUser", options)
      .then((response) => {
        if (response.ok) {
          console.log("Registration successful!");
          console.log(formData)
          navigate("/");
        } else {
          console.error("Registration failed.");
        }
      })
      .catch((error) => console.error("Error:", error));

    setFormData({
      firstName: "",
      lastName: "",
      email: "",
      phoneNumber: "",
      username: "",
      password: "",
    });
  };

  /*This allows React to re-render the component with the updated form data.*/
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
    <div className="wrapper" id="navbar">
      <form onSubmit={handleSubmit} id="content">
        <h1>Create an Account</h1>
        <div className="input-box">
          <input
            type="text"
            name="firstName"
            placeholder="First Name"
            value={formData.firstName}
            onChange={handleChange}
            required
          />
        </div>
        <div className="input-box">
          <input
            type="text"
            name="lastName"
            placeholder="Last Name"
            value={formData.lastName}
            onChange={handleChange}
            required
          />
        </div>

        <div className="input-box">
          <input
            type="email"
            name="email"
            placeholder="Email"
            pattern="[a-z0-9._%+\-]+@[a-z0-9.\-]+\.[a-z]{2,}$"
            title="Please enter a valid email, ex: hi@gmail.com"
            value={formData.email}
            onChange={handleChange}
            required
          />
        </div>

        <div className="input-box">
          <input
            type="tel"
            name="phoneNumber"
            placeholder="Phone Number"
            pattern="[0-9]{3}-[0-9]{3}-[0-9]{4}"
            title="Ex: 123-123-4567"
            value={formData.phoneNumber}
            onChange={handleChange}
            required
          />
        </div>

        <div className="input-box">
          <input
            type="text"
            name="username"
            placeholder="Username"
            pattern=".{8,}"
            title="Eight or more characters"
            value={formData.username}
            onChange={handleChange}
            required
          />
        </div>

        <div className="input-box">
          <input
            type={showPassword ? "text" : "password"}
            name="password"
            placeholder="Password"
            pattern="(?=.*\d)(?=.*[a-z])(?=.*[A-Z]).{8,}"
            title="Must contain at least one  number and one uppercase and lowercase letter, and at least 8 or more characters"
            value={formData.password}
            onChange={handleChange}
            required
          />
          {showPassword ? (
            <FaEyeSlash className="icon" onClick={togglePasswordVisibility} />
          ) : (
            <FaEye className="icon" onClick={togglePasswordVisibility} />
          )}
        </div>

        <button type="submit">Register</button>
      </form>
    </div>
  );
};
