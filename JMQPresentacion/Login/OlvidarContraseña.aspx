<%@ Page Title="Recuperar Contraseña" Language="C#" MasterPageFile="~/Login/Login.Master" AutoEventWireup="true" CodeBehind="OlvidarContraseña.aspx.cs" Inherits="JMQPresentacion.Login.OlvidarContraseña" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server" />
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container mt-2" style="max-width: 400px;">
        <h2 class="h4 mb-3 fw-semibold">Recuperar Contraseña</h2>
        <div class="card shadow-sm">
            <div class="card-body p-3">
                <!-- Campo de entrada de correo -->
                <div class="mb-3">
                    <label class="form-label small fw-semibold">Ingresa tu correo electrónico:</label>
                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="usuario@example.com" TextMode="Email" />
                </div>

                <!-- Botón para enviar el enlace de recuperación -->
                <div class="d-grid">
                    <asp:Button ID="btnEnviar" runat="server" Text="Enviar enlace" CssClass="btn btn-jmq fw-semibold" OnClick="btnEnviar_Click" />
                </div>

                <!-- Volver al login -->
                <div class="text-center mt-3">
                    <a href="Login.aspx" style="color: gray; text-decoration: none;">Volver al inicio</a>
                </div>

                <!-- Mostrar botón de registrarse solo si el correo no está registrado -->
                <div class="d-grid mt-2">
                    <asp:Button ID="btnRegistrarse" runat="server" Text="Registrarse" CssClass="btn btn-outline-secondary" OnClick="btnRegistrarse_Click" Visible="false" />
                </div>

                <!-- Mensaje de error o éxito -->
                <div id="divError" runat="server" class="alert small mt-3" style="display: none;">
                    <asp:Label ID="lblError" runat="server" Text="" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server" />
