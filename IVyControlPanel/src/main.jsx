import * as React from "react";
import * as ReactDOM from "react-dom/client";
import {BrowserRouter} from "react-router-dom"
import AppRouter from './App.jsx';
import { SidebarProvider } from './context/sidebarContext.jsx';
import LoginForm from "./Login/LoginForm.jsx";

ReactDOM.createRoot(document.getElementById("root")).render(
  
  <React.StrictMode>
    <BrowserRouter>
      <SidebarProvider>
        <AppRouter></AppRouter>
      </SidebarProvider>
    </BrowserRouter>
  </React.StrictMode>
);
