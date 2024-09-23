import React, { useEffect, useState } from "react";
import "../styles/Links.css";
import CardLinks from "../components/CardLinks";
import AddLink from "../components/AddLink";


export default function Links() {
  const id = localStorage.getItem("id");
  const [links, setLinks] = useState([]);
  const [isaddlink, setIsAddLink] = useState(false);

 

  const addLink = () => {
     setIsAddLink(true)
  }
  const copyUrl = () => {
    const url = `http://localhost:3000/link/${id}/${id}`; // Obtém a URL atual
    console.log(url)
    navigator.clipboard.writeText(url) // Copia a URL para o clipboard

  }

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

  return ( 
    <div className="grades-container">
      <button type="button" className="copy-button-add" onClick={copyUrl}>Copy your URL</button>
      {!isaddlink && (
        <button type="button" className="login-button-add" onClick={addLink}>+ add link</button>
      )}
        {isaddlink ? (
          <AddLink state={setIsAddLink}/>
        ) : (
          links.map((link) => {
            return(
            <CardLinks key={link.id} idLink={link.id} title={link.name} url={link.url}/>
            )
          })
        )}
        
    </div>
  );
} 


/*
<button onClick={() => editChange(link.id, link.url, link.name, link.idUser)} type="button" className="login-button-edit">
EDIT
</button>
<button  onClick={() => removeChange(link.id, link.url, link.name, link.idUser)} type="button" className="login-button-delete">
DELETE
</button>
*/