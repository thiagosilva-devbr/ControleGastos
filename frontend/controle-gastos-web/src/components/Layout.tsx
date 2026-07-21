import { NavLink, Outlet } from "react-router-dom";

// Componente responsável pelo layout principal da aplicação
export function Layout() {
  return (
    <>
      {/* Cabeçalho do sistema */}
      <header>
        <div className="container cabecalho">
          <strong>Controle de Gastos</strong>

          {/* Menu de navegação entre as páginas */}
          <nav>
            <NavLink to="/pessoas">Pessoas</NavLink>
            <NavLink to="/transacoes">Transações</NavLink>
            <NavLink to="/totais">Totais</NavLink>
          </nav>
        </div>
      </header>

      {/* Área onde as páginas são exibidas */}
      <main className="container">
        <Outlet />
      </main>
    </>
  );
}