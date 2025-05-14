<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="JMQPresentacion.Usuarios.Registro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="login-container">
        <h2>Regístrate</h2>
        <form>
            <input type="text" placeholder="Nombre" required>
            <input type="text" placeholder="Apellido" required>
            <input type="email" placeholder="Correo Electrónico" required>
            <input type="password" placeholder="Contraseña" required>
            <input type="password" placeholder="Confirmar Contraseña" required>
            <button type="button" style="margin-top: 10px;" onclick="window.location.href='/Principal/Principal.aspx'">Registrarse</button>
            <button type="button" onclick="window.location.href='/Principal/Principal.aspx'" style="background-color: red;">Cancelar</button>
        </form>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
