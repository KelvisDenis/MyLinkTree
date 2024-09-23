import { useEffect, useState } from "react";
import React from "react";
import "../styles/NavBar.css"
import { BiUser } from "react-icons/bi";
import { FiMenu } from "react-icons/fi"; // Ícone do menu hambúrguer
import { Link, useNavigate } from "react-router-dom";

export default function NavBar(){
    const isAuthenticated = localStorage.getItem("token");
    const nav = useNavigate();
    const [dropdownOpen, setDropdownOpen] = useState(false);
    const [menuOpen, setMenuOpen] = useState(false); 
    const [isMobile, setIsMobile] = useState(window.innerWidth <= 768); 

    useEffect(() => {
        const handleResize = () => {
            setIsMobile(window.innerWidth <= 768); // Considera mobile se a largura for menor que 768px
        };

        window.addEventListener("resize", handleResize);
        return () => window.removeEventListener("resize", handleResize);
    }, []);

    useEffect(() => {
        const timelogin =  localStorage.getItem("time");
        const timeNOw =  Date.now();
        const expired = localStorage.getItem("expired");

        const timeNowInMinutes = Math.floor(timeNOw / 1000 / 60); // Converte o tempo atual para minutos
        const loginTimeInMinutes = Math.floor(parseInt(timelogin) / 1000 / 60); // Converte o tempo de login para minutos
        const expirationDuration = parseInt(expired); // Tempo de expiração (em minutos)

        if(timeNowInMinutes - loginTimeInMinutes > expirationDuration){
            alert("Session expired!");
            localStorage.clear();
            window.location.reload();
            nav("/");
        }
    }, [nav]);

    const submit = () => {
        localStorage.clear();
        window.location.reload();
        nav("/");
    };

    const toggleDropdown = () => {
        setDropdownOpen(!dropdownOpen);
    };

    const toggleMenu = () => {
        setMenuOpen(!menuOpen); // Alterna o estado do menu mobile
    };

    return (
        <nav className="navbar">
            <div className="navbar-container">
                <li className="navbar-logo">Linktree</li>
                {/* Ícone do menu hambúrguer para mobile */}
                <div className="hamburger" onClick={toggleMenu}>
                    <FiMenu />
                </div>
                {/* Menu mobile colapsável */}
                <ul className={`navbar-menu ${menuOpen ? "active" : ""}`}>
                    {isAuthenticated ? ( 
                        <>
                            <li className="navbar-item">
                                <Link to="/home" className="navbar-link">MyLinks</Link>
                            </li>
                            <li className="navbar-item">
                                <Link to={`/links`} className="navbar-link">Your Links</Link>
                            </li>
                            {/* Só mostra o dropdown se não for mobile */}
                            {!isMobile ? (
                                <li className="navbar-item">
                                    <div className="dropdown" onMouseEnter={toggleDropdown} onMouseLeave={toggleDropdown}>
                                        <span className="navbar-link dropdown-toggle">
                                            <BiUser />
                                        </span>
                                        {dropdownOpen && (
                                            <div className="dropdown-menu">
                                                <Link to="/settings" className="dropdown-item">Perfil</Link>
                                                <Link type="submit" onClick={submit} className="dropdown-item">Log out</Link>
                                            </div>
                                        )}  
                                    </div>
                                </li>
                            ): (
                                <li className="navbar-item">
                                    <Link to="/settings" className="dropdown-item">update you Perfil</Link>
                                    <Link type="submit" onClick={submit} className="dropdown-item">Log out</Link>
                                </li>
                            )}
                        </>
                    ) : (
                        <>
                            <li className="navbar-item"><Link to="/login" className="navbar-link">Sign In</Link></li>
                            <li className="navbar-item"><Link to="/sign" className="navbar-link">Sign up</Link></li>
                        </>
                    )}
                </ul>
            </div>
        </nav>
    );
}
