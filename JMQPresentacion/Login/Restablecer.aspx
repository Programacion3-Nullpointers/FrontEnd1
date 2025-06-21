<%@ Page Title="Restablecer Contraseña" Language="C#" MasterPageFile="~/Login/Login.Master" AutoEventWireup="true" CodeBehind="Restablecer.aspx.cs" Inherits="JMQPresentacion.Login.Restablecer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container mt-2" style="max-width: 400px;">
        <h2 class="h4 mb-3">Restablecer Contraseña</h2>
        <div class="card">
            <div class="card-body p-3">
                <asp:Label ID="lblMensaje" runat="server" CssClass="small text-danger" Visible="false" />

                <div class="mb-3">
                    <label class="form-label small">Nueva contraseña:</label>
                    <asp:TextBox ID="txtNuevaPassword" runat="server" CssClass="form-control" TextMode="Password" />
                </div>

                <div class="mb-3">
                    <label class="form-label small">Confirmar contraseña:</label>
                    <asp:TextBox ID="txtConfirmarPassword" runat="server" CssClass="form-control" TextMode="Password" />
                </div>

                <div class="d-flex justify-content-center">
                    <asp:Button ID="btnRestablecer" runat="server" CssClass="btn btn-jmq w-100" Text="Restablecer" OnClick="btnRestablecer_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
