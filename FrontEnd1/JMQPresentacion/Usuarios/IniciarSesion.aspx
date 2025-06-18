<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="IniciarSesion.aspx.cs" Inherits="JMQPresentacion.Usuarios.IniciarSesion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container mt-4" style="max-width: 600px;">
        <h2>Iniciar sesión</h2>
        <div class="card">
            <div class="card-body">
                <div class="row mb-3 gap-1">
                    <label>Correo electrónico:</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Ingresa tu correo electrónico..." TextMode="Email"></asp:TextBox>
                    </div>
                    <label>Contraseña:</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtContr" runat="server" CssClass="form-control" placeholder="Ingresa tu contraseña..." TextMode="Password"></asp:TextBox>
                    </div>
                </div>
                ¿No tienes cuenta? <a href="/Usuarios/Registro.aspx">Regístrate</a>.
                <div class="row mt-3">
                    <div class="col-md-6">
                        <asp:Button ID="btnGuardar" runat="server" Text="Iniciar Sesión" CssClass="btn btn-jmq"/>
                        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" />
                    </div>
                    <div class="col-md-12 mt-4 alert alert-danger" id="divError" runat="server" style="display:none;">
                        <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
