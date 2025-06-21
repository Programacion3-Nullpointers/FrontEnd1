<%@ Page Title="" Language="C#" MasterPageFile="~/Login/Login.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="JMQPresentacion.Login.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container mt-4" style="max-width: 500px;">
        <h2 style="font-family:Roboto; font-size: 24px; text-align: center;">Iniciar sesión</h2>
        <div class="card">
            <div class="card-body">
                <div class="row mb-3 gap-1">
                    <label style ="font-family: Roboto;">Correo electrónico</label>
                    <div class="input-group">
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Ingresa tu correo electrónico" TextMode="Email" Style="color: black;"></asp:TextBox>
                    </div>
                    <label style ="font-family: Roboto;">Contraseña</label>
                    <div class="input-group mb-2"> 
                        <asp:TextBox ID="txtContr" runat="server" CssClass="form-control" placeholder="Ingresa tu contraseña" TextMode="Password" Style="color: gray;"></asp:TextBox>
                    </div>
                    <div class="input-group mb-2">
                        <asp:HyperLink ID="lnkForgotPassword" runat="server" NavigateUrl="OlvidarContraseña.aspx" Style="color: gray; text-decoration: none;">
                            ¿Olvidaste tu contraseña?
                        </asp:HyperLink>
                    </div>
                    <div class="col-md-12 text-center">
                        <asp:Button ID="btnGuardar" runat="server" Text="Ingresar" CssClass="btn btn-jmq w-100" style ="font-family: Roboto;" OnClick="btnLogin_Click"/>
                    </div>
                    <div class="col-12 mt-2 alert alert-danger py-1 px-2 small" id="divError" runat="server" style="display:none;">
                        <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row mt-3 text-center">
                    <div class="d-inline">¿Aún no tienes cuenta? <a href="Registrarse.aspx" class="ms-1">Regístrate</a></div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>