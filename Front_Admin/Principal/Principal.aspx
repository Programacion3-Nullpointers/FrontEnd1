<%@ Page Title="Inicio Admin" Language="C#" MasterPageFile="~/MainLayou.master" AutoEventWireup="true" CodeBehind="Principal.aspx.cs" Inherits="Front_Admin.Principal.Principal" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 0;
            overflow-x: hidden;
        }

        .content {
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
            max-height: 400px;
            height: auto;
            border-radius: 10px;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
            display: block;
            margin: 0 auto;
            object-fit: cover;
        }
    </style>
</asp:Content>

<asp:Content ID="MainContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
</asp:Content>
