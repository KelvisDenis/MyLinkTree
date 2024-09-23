import React, { useState } from "react";
import "../styles/LoginForm.css";
import {Link, useNavigate} from "react-router-dom";



export default function LoginForm(){
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState('');

  const nav = useNavigate();

  const handleSubmit = async (e) => {
    e.preventDefault();
    fetch("http://192.168.100.59:5290/api/UserApi/User/Login-User", {
      method: "POST",
      headers: {"Content-Type": "application/json"},
      body: JSON.stringify({email: email, password: password})
    })
    .then(res => {if(!res.ok){
      throw new Error("Failed in login");
    } return res.json()})
    .then(
      data => {localStorage.setItem("username", data.userName);
      localStorage.setItem("token", data.token);
      localStorage.setItem("expired", 10);
      localStorage.setItem("time", Date.now());
      localStorage.setItem("id", data.id);
      nav("/home")
  }).catch(err => {
    console.error('Error:', err);
    setError(err.message);
      nav("/")
  })
  };

  return (
    <div className="login-container">
        <form className="login-form" onSubmit={handleSubmit}>
        <h2 className="login-title">Login</h2>
        <div className="input-group-login">
          <label htmlFor="email">Email:</label>
          <input
            type="email"
            id="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />
        </div>
        <div className="input-group-login">
          <label htmlFor="password">Password:</label>
          <input
            type="password"
            id="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
        </div>
        <Link to="/forgot">forgot password?</Link>
        {error && <p className="login-error">{error}</p>}
        <button type="submit" className="login-button">
          Enter
        </button>
        <button type="submit" className="login-button"> 
          Register
        </button>
      </form>      
    </div>
  );
};
