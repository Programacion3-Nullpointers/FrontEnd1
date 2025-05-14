<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="IniciarSesion.aspx.cs" Inherits="JMQPresentacion.Usuarios.IniciarSesion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="login-container">
        <h2>Iniciar Sesión</h2>
        <form>
            <input type="text" placeholder="Usuario" required>
            <input type="password" placeholder="Contraseña" required>
            <button type="button" style="margin-top: 10px;" onclick="window.location.href='/Principal/Principal.aspx'">Iniciar sesión</button>
            <button type="button" onclick="window.location.href='/Principal/Principal.aspx'" style="background-color: red;">Cancelar</button>
        </form>
    </div>
    <br />
    <div class="login-container">
        O <a href="/Usuarios/Registro.aspx">regístrate</a>.
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
