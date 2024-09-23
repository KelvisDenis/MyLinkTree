import React from "react";
import "../styles/Home.css";


export default function Home(){
    return(
        <div className="home-container">
            <div className="share-content">
                <h6>Share your Linktree from your Instagram, TikTok, Twitter and other bios</h6>
                <p>
                    Track your engagement over time, monitor revenue and learn what’s
                    converting your audience. Make informed updates on the fly to keep them coming back.
                </p>
                <button value={"GET Start"} className="button-home">Get Started</button>
            </div>
            <div className="analize-content">
                <h6>Analyze your audience and keep your followers engaged</h6>
                <p>
                Track your engagement over time, monitor revenue and learn what’s
                converting your audience. Make informed updates on the fly to keep them coming back.
                </p>
                <div className="card-home">
                    <img 
                        src="/business-chart-with-arrow-free-png.webp" 
                        alt="Descrição da imagem"
                        className="profile-card1" 
                    />
                </div>
                <div className="card2-home">
                    <img 
                        src="/image-barras.png" 
                        alt="Descrição da imagem"
                        className="profile-card2" 
                    />
                </div>
                <div className="card3-home">
                    <img 
                        src="/terra-vetor-real.png" 
                        alt="Descrição da imagem"
                        className="profile-card3" 
                    />
                </div>
            </div>
           
        </div>
    )
}