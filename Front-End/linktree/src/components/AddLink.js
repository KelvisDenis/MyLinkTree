import "../styles/AddLink.css"
import { BiX } from "react-icons/bi";
import { useState } from "react";
import { BiLogoInstagram } from "react-icons/bi";
import { BiLogoLinkedinSquare } from "react-icons/bi";
import { BiLogoMeta } from "react-icons/bi";
import { BiLogoPinterest } from "react-icons/bi";
import { BiLogoReddit } from "react-icons/bi";
import { BiLogoFacebookCircle } from "react-icons/bi";
import { BiLogoWhatsapp } from "react-icons/bi";
import { BiLogoYoutube } from "react-icons/bi";






export default function AddLink({state}){
    const [url, setUrl] = useState("");

    const saveAddUrl = async(e, name) => {
        e.preventDefault();
        const linkCreate = {
            id: 0,
            name: name,
            url: url,
            idUser: localStorage.getItem("id")
        }
        const arrayLink = {links: [linkCreate]}
        console.log("Enviando linkCreate:", arrayLink); 

        try {
            const response = await fetch(`http://192.168.100.59:5290/api/LinkApi/Links/Create-Links`, {
              method: 'POST',
              headers: {
                'Content-Type': 'application/json'
              },
              body: JSON.stringify(arrayLink)
            });
      
            if (!response.ok) {
              throw new Error('Erro ao atualizar link');
            }
      
            const result = await response.text();
            console.log('url atualizada com sucesso:', result);
            state(false)
            window.location.reload();
  
          } catch (error) {
            console.error('Erro:', error);
          }

    }

 
    return(
        <div className="card">
            <div className="card-content">
               <BiX onClick={() => state(false)} className="bix"/>
               <label htmlFor="text">Enter URL</label>
               <input
                        type="text"
                        value={url}
                        onChange={(e) => setUrl(e.target.value)}
                        autoFocus
                    />
                <hr/>
                <h6>some options</h6>
                <div className="icons-content">
                    <BiLogoInstagram className="icons" onClick={(e) => saveAddUrl(e, "instagram")}/>
                    <BiLogoLinkedinSquare className="icons"   onClick={(e) => saveAddUrl(e, "linkedin")}/>
                    <BiLogoMeta className="icons"  onClick={(e) => saveAddUrl(e, "meta")}/>
                    <BiLogoPinterest className="icons"  onClick={(e) => saveAddUrl(e, "pinterest")}/>
                    <BiLogoReddit className="icons"  onClick={(e) => saveAddUrl(e, "reddit")}/>
                    <BiLogoFacebookCircle className="icons"  onClick={(e) => saveAddUrl(e, "facebook")}/>
                    <BiLogoWhatsapp className="icons"  onClick={(e) => saveAddUrl(e, "whatsapp")} />
                    <BiLogoYoutube  className="icons"  onClick={(e) => saveAddUrl(e, "youtube")}/>

                </div>

            </div>
        </div>
    )
}