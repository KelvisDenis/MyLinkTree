import React, { useState, useEffect } from "react";
import "../styles/LinkTree.css"
import { useParams } from "react-router-dom";




export default function LinkTree(){
  const {id} = useParams();
  const {idUser} = useParams();
  const [links, setLinks] = useState([]);
  const [username, setUsername] = useState("");

  useEffect(() => {
    const fetchLinks = async () => { 
      try {
        const response = await fetch(`http://192.168.100.59:5290/api/LinkApi/Links/Get-Links/${id}`);
        if (!response.ok) {
          throw new Error("Erro ao buscar links");
        }
        const data = await response.json();
        setLinks(data.links); // Acessando a propriedade "links" do retorno da API
        const userResponse = await fetch(`http://192.168.100.59:5290/api/UserApi/User/Get-User-id/${idUser}`);
        if (!userResponse.ok) {
            throw new Error("Erro ao buscar links");
          }
        const dataUsername = await userResponse.json();
        setUsername(dataUsername.userName);

      } catch (err) {
        console.log(err);
      }
    }; 

    fetchLinks();
  }, [id, idUser]); 
 
    return(
      <div className="links-containers">
        <img 
          src="/image-teste.webp" 
          alt="Descrição da imagem" 
          className="profile-image-link" 
        />
 
        <h5>@{username}</h5>
        <div className="input-group-links-tree">
          {links.map((link)=> (
            <div className="input-group-links-tree">
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