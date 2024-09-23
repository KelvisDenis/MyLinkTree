import React, { useState, useEffect } from "react";
import "../styles/PageLinks.css"




export default function PageLinks(){
  const id = localStorage.getItem("id");
  const [links, setLinks] = useState([]);
  const user = localStorage.getItem("username") 

  useEffect(() => {
    const fetchLinks = async () => { 
      try {
        const response = await fetch(`http://192.168.100.59:5290/api/LinkApi/Links/Get-Links/${id}`);
        if (!response.ok) {
          throw new Error("Erro ao buscar links");
        }
        const data = await response.json();
        setLinks(data.links); // Acessando a propriedade "links" do retorno da API
      } catch (err) {
        console.log(err);
      }
    }; 

    fetchLinks();
  }, [id]); // Executa o efeito apenas quando o 'id' muda
 
    return(
      <div className="links-container">
        <img 
          src="/image-teste.webp" 
          alt="Descrição da imagem" 
          className="profile-image" 
        />
 
        <h5>@{user}</h5>
        <div className="input-group-links">
          {links.map((link)=> (
            <div className="input-group-links">
               <input  
                 type="url"
                 id={link.id}
                 value={link.name}
                 readOnly
               />
             </div>
          ))}
        <h5>LinkTree</h5>
        </div>
      </div>
    )
}