import React from "react";
import { useState } from "react";
import "../styles/RegisterForm.css";
import { useNavigate } from "react-router-dom";


export default function RegisterForm(){
    const [email, setEmail] = useState("");
    const [username, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [err, setErr] = useState("");
    const [errusername, setErrUsername] = useState("");
    const [erremail, setErrEmail] = useState("");
    const [Repeatpassword, setRepeatPassword] = useState("");
    const nav = useNavigate();



    const usernameverify = async(e) => {
      e.preventDefault();
      console.clear();
      const newusername = e.target.value;
      try{
        const response =  await fetch(`http://192.168.100.59:5290/api/UserApi/User/Get-User-return-user/${newusername}`);
        if(response.status === 400){
          setErrUsername(""); 
        }
        else{
          setErrUsername("Username already exist");
        }
      }catch(err){

      }
    }

    const passChange = (e) => {
      const change = e.target.value;
      setRepeatPassword(change);
      console.log(change);
      console.log(change!== password);
      console.log(err);

      if(change !== password){
        setErr("different passwords");
      }
      else{
        setErr("");
      }

    }
    const handleSubmit = async(e) => {

      e.preventDefault();
      if(err !== "") {
        setErr("password incorrect!")
        return;
      }
      if( erremail !== "" ){
        setErrEmail("Email already exist");
        return
      }
      if(errusername !== ""){
        setErrEmail("Username already exist");
        return;
      }
      try {
        // Criação do usuário
        const createResponse = await fetch("http://192.168.100.59:5290/api/UserApi/User/Create-User", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ email, password, userName: username })
        });

        if (!createResponse.ok) {
            throw new Error("Failed to create user");
        }
         const loginResponse = await fetch("http://192.168.100.59:5290/api/UserApi/User/Login-User", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ email:email, password:password })
        });
        if (!loginResponse.ok) {
            throw new Error("Failed in login");
        }

        const data = await loginResponse.json();
        localStorage.setItem("username", data.userName);
        localStorage.setItem("token", data.token);
        localStorage.setItem("expired", 10);
        localStorage.setItem("time", Date.now());
        localStorage.setItem("id", data.id);
        nav("/home");
    } catch (error) {
        console.error('Error:', error);
        setErr(error.message);
    }
    };


    return(
        <div className="register-content">
        <form className="form-register" onSubmit={handleSubmit}>
        <h2 className="register-title">sign user</h2>
        <div className="input-group-register">
          <label htmlFor="email">Email:</label>
          <input
            type="email"
            id="email"
            value={email}
            onChange={(e) => {setEmail(e.target.value)}}
            required
          />
        </div>
        {errusername && <p className="login-error">{err}</p>}
        <div className="input-group-register">
          <label htmlFor="email">Username:</label>
          <input
            type="text"
            id="username"
            value={username}
            onChange={(e) => {setUsername(e.target.value); usernameverify(e)}}
            required
          />
        </div>
        <div className="input-group-register">
          <label htmlFor="password">Password:</label>
          <input
            type="password"
            id="password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
        </div>
        <div className="input-group-register">
          <label htmlFor="password">Repeat Password:</label>
          <input
            type="password"
            id="repeatpassword"
            value={Repeatpassword}
            onChange={(e) => passChange(e)}
            required
          /> 
        </div>
        {err && <p className="login-error">{err}</p>}
        <button type="submit" className="login-button-register">
          Enter
        </button>
      </form>
    </div>
    )
}