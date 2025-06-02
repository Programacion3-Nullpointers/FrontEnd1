<%@ Page Title="" Language="C#" MasterPageFile="~/Login/Login.Master" AutoEventWireup="true" CodeBehind="OlvidarContraseña.aspx.cs" Inherits="JMQPresentacion.Login.OlvidarContraseña" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container mt-2" style="max-width: 400px;">
        <h2 class="h4 mb-3">Recuperar Contraseña</h2>
        <div class="card">
            <div class="card-body p-3">
                <!-- Campo de entrada de correo -->
                <div class="mb-3">
                    <label class="form-label small">Ingresa tu correo electrónico:</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" 
                        placeholder="usuario@example.com" TextMode="Email"/>
                </div>

                <!-- Botón para enviar el enlace de recuperación -->
                <div class="d-flex justify-content-center">
                    <asp:Button ID="btnEnviar" runat="server" Text="Enviar enlace" CssClass="btn btn-jmq w-100" OnClick="btnEnviar_Click"/>
                </div>

                <div class="row mt-3 text-center">
                    <a href="Login.aspx" class="ms-1" Style="color: gray; text-decoration: none;">Volver al inicio </a>
                </div>


                <!-- Mensaje de error (si el correo no está registrado) -->
                <div class="mt-3 alert alert-danger small" id="divError" runat="server" style="display:none;">
                    <asp:Label ID="lblError" runat="server" Text=""/>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
