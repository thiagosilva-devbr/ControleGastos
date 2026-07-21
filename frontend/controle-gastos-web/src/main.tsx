// Configuração inicial da aplicação React.
// Responsável por renderizar o componente principal e configurar recursos globais.

import { StrictMode } from "react"; 
import { createRoot } from "react-dom/client"; 
import { BrowserRouter } from "react-router-dom"; 
import App from "./App"; 
import "./index.css"; 

// Cria a raiz da aplicação e renderiza os componentes principais.
createRoot(document.getElementById("root")!).render( 

  // Ativa verificações adicionais do React durante o desenvolvimento.
  <StrictMode> 

    {/* Habilita o gerenciamento de rotas utilizando o React Router.*/}
    <BrowserRouter> 

      {/* Renderiza o componente principal da aplicação.*/}
      <App /> 

    </BrowserRouter> 
  </StrictMode> 
);