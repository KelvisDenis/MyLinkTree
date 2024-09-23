import React from "react";
import "../styles/ForgotPassword.css"
import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";


export default function ForgotPassword(){
    const [newpassword, setNewPassword] = useState("");
    const [email, setEmail] = useState("");
    const [repeatpassword, setRepeatPassword] = useState("");
    const [error, setError] = useState("");
    const nav = useNavigate();


useEffect(() => {
    const onChangePassword = () => {
        if(repeatpassword !== newpassword){
            setError("Incompatible password!");
        }else{
            setError("");
        }
    }; 
    onChangePassword();
}, [repeatpassword])

    const handleSubmit = async (e) => {
        e.preventDefault();
    
        const userUpdateData = {
          email: email,
          username: "",
          newPassword: newpassword
        };
    
        try {
          const response = await fetch(`http://localhost:5290/api/UserApi/User/Update-User/`, {
            method: 'PUT',
            headers: {
              'Content-Type': 'application/json'
            },
            body: JSON.stringify(userUpdateData)
          });
    
          if (!response.ok) {
            throw new Error('Erro ao atualizar o usuário');
          }
    
          const result = await response.text();
          console.log('Usuário atualizado com sucesso:', result);
          nav("/");

        } catch (error) {
          console.error('Erro:', error);
        }
      };
    


    return(
        <div className="forgot-container">
        <form className="forgot-form" onSubmit={handleSubmit}>
          <h2 className="forgot-title">New password</h2>
          <div className="input-group">
            <label htmlFor="email">Email:</label>
            <input
              type="email"
              id="email"
              value={email}
              onChange={(e) => setEmail(e.target.value)}
              required
            />
          </div>
          <div className="input-group">
            <label htmlFor="password">New Password:</label>
            <input
              type="password"
              id="password"
              value={newpassword}
              onChange={(e) => setNewPassword(e.target.value)}
              required
            />
          </div>
          <div className="input-group">
            <label htmlFor="password">Repeat New Password:</label>
            <input
              type="password"
              id="repeatpassword"
              value={repeatpassword}
              onChange={(e) => setRepeatPassword(e.target.value)}
              required
            />
          </div>
          {error && <p className="login-error">{error}</p>}
          <button type="submit" className="login-button">
            save
          </button>
         
        </form>
      </div>
    )
}