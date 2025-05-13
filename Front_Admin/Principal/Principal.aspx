<%@ Page Title="Inicio Admin" Language="C#" MasterPageFile="~/MainLayou.master" AutoEventWireup="true" CodeBehind="Principal.aspx.cs" Inherits="Front_Admin.Principal.Principal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<style>
    body {
        font-family: Arial, sans-serif;
        margin: 0;
        overflow-x: hidden;
    }

    .main-layout {
        display: flex;
        min-height: 100vh;
    }

    .sidebar {
        width: 200px;
        height: 100vh;
        background-color: #2c3e50;
        color: white;
        display: flex;
        flex-direction: column;
        gap: 10px;
        padding: 20px;
        box-sizing: border-box;
    }

    .menu-title {
        font-weight: bold;
        font-size: 18px;
        color: white;
        text-decoration: none;
        margin-bottom: 20px;
    }

    .menu-item {
        background-color: #34495e;
        padding: 10px;
        border-radius: 6px;
        text-align: center;
        color: white;
        text-decoration: none;
        transition: background-color 0.3s ease;
    }

    .menu-item:hover {
        background-color: #1abc9c;
        color: white;
    }

    .logout {
        margin-top: auto; /* empuja hacia abajo */
        background-color: #e74c3c;
    }

    .logout:hover {
        background-color: #c0392b;
    }

    .content {
        flex: 1;
        padding: 40px;
        box-sizing: border-box;
        overflow: hidden;
    }

    .welcome-text {
        text-align: center;
        margin-bottom: 30px;
    }

    .welcome-text h1 {
        color: #2c3e50;
        font-size: 28px;
        margin-bottom: 10px;
    }

    .welcome-text h2 {
        color: #34495e;
        font-size: 24px;
        margin-bottom: 5px;
    }

    .welcome-text h3 {
        color: #7f8c8d;
        font-size: 20px;
    }

    .image-container {
        text-align: center;
        margin-top: 30px;
    }

    .image-container img {
        width: 100%;
        max-width: 100%;
        max-height: 400px; /* nuevo: limita la altura */
        height: auto;
        border-radius: 10px;
        box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
        display: block;
        margin: 0 auto;
        object-fit: cover; /* recorta si es necesario manteniendo proporción */
    }

</style>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main-layout">
        <div class="sidebar">
            <a class="menu-title" href="../Principal/Principal.aspx">Menú</a>
            <a class="menu-item" href="../Usuarios/Usuarios.aspx">Usuarios</a>
            <a class="menu-item" href="../Cotizaciones/Cotizaciones.aspx">Cotizaciones</a>
            <a class="menu-item" href="../Pagos/Pagos.aspx">Pagos</a>
            <a class="menu-item logout" href="../Login/Login.aspx">Logout</a>
        </div>

        <div class="content">
            <div class="welcome-text">
                <h1>Bienvenido, Juancin Cemanu Mendo</h1>
                <h2>Bienvenido a JMQ Enterprise</h2>
                <h3>Panel Administrativo</h3>
            </div>

            <div class="image-container">
                <img src="../Imagenes/ferreteria.jpeg" alt="Ferretería" />
            </div>
        </div>
    </div>
</asp:Content>
