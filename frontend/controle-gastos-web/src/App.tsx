// Configuração principal das rotas da aplicação.
// Define os caminhos disponíveis e quais páginas serão renderizadas em cada rota.

import { Navigate, Route, Routes } from "react-router-dom"; 
import { Layout } from "./components/Layout"; 
import { PessoasPage } from "./pages/PessoasPage"; 
import { TotaisPage } from "./pages/TotaisPage"; 
import { TransacoesPage } from "./pages/TransacoesPage"; 
 
// Componente principal responsável por organizar a navegação da aplicação.
export default function App() { 
  return ( 
    // Componente responsável por gerenciar todas as rotas do sistema.
    <Routes> 

      // Define um layout padrão compartilhado entre as páginas.
      // O conteúdo das páginas será renderizado dentro do componente Layout.
      <Route element={<Layout />}> 

        // Redireciona o acesso inicial da aplicação para a página de pessoas.
        <Route 
          path="/" 
          element={<Navigate to="/pessoas" replace />} 
        /> 

        // Rota responsável pelo cadastro e gerenciamento de pessoas.
        <Route path="/pessoas" element={<PessoasPage />} /> 

        // Rota responsável pelo cadastro e visualização das transações.
        <Route 
          path="/transacoes" 
          element={<TransacoesPage />} 
        /> 

        // Rota responsável pela consulta dos totais financeiros.
        <Route path="/totais" element={<TotaisPage />} /> 

      </Route> 
    </Routes> 
  ); 
}