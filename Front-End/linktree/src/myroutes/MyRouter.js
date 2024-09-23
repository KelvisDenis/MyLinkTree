import React from "react";
import { Routes, Route, useLocation } from 'react-router-dom';
import NavBar from "../components/NavBar";
import LoginForm from "../components/LoginForm";
import Links from "../Pages/Links.js"
import PageLinks from "../Pages/PagaeLinks.js";
import ProtectedRoute from "./ProtectRoutes.js";
import RegisterForm from "../components/RegisterForm.js";
import ForgotPassword from "../Pages/ForgotPasssword.js";
import Home from "../Pages/Home.js";
import LinkTree from "../Pages/LinkTree.js";

export function MyRoutes(){ 

    const location = useLocation();
    const hideNavBar = location.pathname.startsWith("/link/");

    return( 
        <>
             {!hideNavBar && <NavBar/>}
            <Routes>
                <Route path="/" element={<Home/>}/>
                <Route path="/sign in" element={<RegisterForm/>}/>
                <Route path="/login" element={<LoginForm/>}/>
                <Route path="/home" element={<ProtectedRoute element={<Links/>} />}/>
                <Route path="/links" element={<ProtectedRoute element={<PageLinks/>} />}/>
                <Route path="/link/:id/:idUser" element={<LinkTree/>} />
                <Route path="/forgot" element={<ProtectedRoute element={<ForgotPassword/>}/>}/>
            </Routes>
        </>
    ) 
}