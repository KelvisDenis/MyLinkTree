import { useState } from 'react';
import React from 'react';
import '../styles/CardLink.css';
import { FaPen } from "react-icons/fa";
import { VscTrash } from "react-icons/vsc";


export default function CardLinks ({ idLink, title, url }){
    const [isEditingTitle, setIsEditingTitle] = useState(false); // Estado para modo de edição do título
    const [isEditingUrl, setIsEditingUrl] = useState(false); // Estado para modo de edição do URL
    const [url_, setUrl ] = useState(url) // estado da url completa
    const [urlview, setUrlview ] = useState(url.substring(0, 25) + "...") // estado para url modificada
    const [title_, setTitle ] = useState(title) // estado para titulo 

    const handleTitleChange = async(e) => {
        const newTitle = e.target.value;
        setTitle(newTitle);
        e.preventDefault();
        const linkupdate = {
            id: idLink,
            name: newTitle,
            url: url_,
            idUser: localStorage.getItem("id")
        }
        try {
            const response = await fetch(`http://localhost:5290/api/LinkApi/Links/Update-Links/`, {
              method: 'PUT',
              headers: {
                'Content-Type': 'application/json'
              },
              body: JSON.stringify(linkupdate)
            });
      
            if (!response.ok) {
              throw new Error('Erro ao atualizar link');
            }
      
            const result = await response.text();
            console.log('title atualizado com sucesso:', result);
  
          } catch (error) {
            console.error('Erro:', error);
          }

    }

    const handleUrlChange = async(e) => {
        const newUrl = e.target.value;
        setUrl(newUrl);
        const spliturl = newUrl.substring(0, 25 + "...");
        setUrlview(spliturl);
        e.preventDefault();
        const linkupdate = {
            id: idLink,
            name: title_,
            url: newUrl,
            idUser: localStorage.getItem("id")
        }
        try {
            const response = await fetch(`http://localhost:5290/api/LinkApi/Links/Update-Links/`, {
              method: 'PUT',
              headers: {
                'Content-Type': 'application/json'
              },
              body: JSON.stringify(linkupdate)
            });
      
            if (!response.ok) {
              throw new Error('Erro ao atualizar link');
            }
      
            const result = await response.text();
            console.log('url atualizada com sucesso:', result);
  
          } catch (error) {
            console.error('Erro:', error);
          }


    }
    const saveTitle = () => {
        setIsEditingTitle(false); // Sai do modo de edição ao salvar
    };

    const saveUrl = () => {
        setIsEditingUrl(false); // Sai do modo de edição ao salvar
    };


    const deleteLink = async(e) => {
        e.preventDefault();
        try {
            const response = await fetch(`http://192.168.100.59:5290/api/LinkApi/Links/Delete-Links/${idLink}`, {
                method: 'DELETE',
                headers: {
                    'Content-Type': 'application/json'
                }});
            if (!response.ok) {
              throw new Error('Erro ao atualizar link');
            }
            const result = await response.text();
            console.log('Link removido com sucesso:', result);  
            window.location.reload()
          } catch (error) {
            console.error('Erro:', error);
          }
    }

  return (
    <div className="card">
            <div className="card-content">
                {/* Se estiver editando o título, exibe o input, caso contrário, exibe o título */}
                {isEditingTitle ? (
                    <input
                        type="text"
                        value={title_}
                        onChange={(e) => handleTitleChange(e)}
                        onBlur={saveTitle} // Sai do modo de edição ao clicar fora
                        autoFocus
                    />
                ) : (
                    <h3 className="card-title">
                        {title_} <FaPen onClick={() => setIsEditingTitle(true)} />
                    </h3>
                )}

                {/* Se estiver editando o URL, exibe o input, caso contrário, exibe o URL */}
                {isEditingUrl ? (
                    <input
                        type="url"
                        value={url_}
                        onChange={(e) => handleUrlChange(e)}
                        onBlur={saveUrl} // Sai do modo de edição ao clicar fora
                        autoFocus
                    />
                ) : ( 
                    <p className="card-description">
                        {urlview} <FaPen onClick={() => setIsEditingUrl(true)} />
                    </p>
                )}

                <VscTrash className='trash-button' onClick={(e) => deleteLink(e)}/>
            </div>
        </div>
  );
};

