<%@ Page Title="Restablecer Contraseña" Language="C#" MasterPageFile="~/Login/Login.Master" AutoEventWireup="true" CodeBehind="Restablecer.aspx.cs" Inherits="JMQPresentacion.Login.Restablecer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server" />
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container mt-2" style="max-width: 400px;">
        <h2 class="h4 mb-3 fw-semibold">Restablecer Contraseña</h2>
        <div class="card shadow-sm">
            <div class="card-body p-3">
                <div class="mb-3">
                    <label class="form-label small fw-semibold">Nueva contraseña:</label>
                    <asp:TextBox ID="txtNuevaPassword" runat="server" CssClass="form-control" TextMode="Password" />
                    <span id="errorNueva" class="text-danger small d-none"></span>
                </div>

                <div class="mb-3">
                    <label class="form-label small fw-semibold">Confirmar contraseña:</label>
                    <asp:TextBox ID="txtConfirmarPassword" runat="server" CssClass="form-control" TextMode="Password" />
                    <span id="errorConfirmar" class="text-danger small d-none"></span>
                </div>

                <div class="d-grid">
                    <asp:Button ID="btnRestablecer" runat="server"
                        CssClass="btn btn-jmq fw-semibold" Text="Restablecer"
                        OnClick="btnRestablecer_Click" OnClientClick="return validarFormulario();" />
                </div>

                <div id="mensajeFlotante" runat="server" class="text-danger small mt-2 text-center" style="display:none;"></div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
<script type="text/javascript">
    document.addEventListener("DOMContentLoaded", function () {
        const txtNueva = document.getElementById('<%= txtNuevaPassword.ClientID %>');
        const txtConfirmar = document.getElementById('<%= txtConfirmarPassword.ClientID %>');

        txtNueva.addEventListener("input", validarEnTiempoReal);
        txtConfirmar.addEventListener("input", validarEnTiempoReal);
    });

    function validarEnTiempoReal() {
        const nueva = document.getElementById('<%= txtNuevaPassword.ClientID %>').value.trim();
        const confirmar = document.getElementById('<%= txtConfirmarPassword.ClientID %>').value.trim();

        limpiarErrores();

        if (nueva.length > 0 && nueva.length < 8) {
            mostrarError("errorNueva", "La contraseña debe tener al menos 8 caracteres.");
        }

        if (confirmar.length > 0 && nueva !== confirmar) {
            mostrarError("errorConfirmar", "Las contraseñas no coinciden.");
        }
    }

    function mostrarError(id, mensaje) {
        const el = document.getElementById(id);
        el.textContent = mensaje;
        el.classList.remove("d-none");
    }

    function limpiarErrores() {
        ["errorNueva", "errorConfirmar"].forEach(id => {
            const el = document.getElementById(id);
            el.textContent = "";
            el.classList.add("d-none");
        });
    }

    function validarFormulario() {
        const nueva = document.getElementById('<%= txtNuevaPassword.ClientID %>').value.trim();
        const confirmar = document.getElementById('<%= txtConfirmarPassword.ClientID %>').value.trim();

        limpiarErrores();

        let esValido = true;

        if (nueva.length < 8) {
            mostrarError("errorNueva", "La contraseña debe tener al menos 8 caracteres.");
            esValido = false;
        }

        if (nueva !== confirmar) {
            mostrarError("errorConfirmar", "Las contraseñas no coinciden.");
            esValido = false;
        }

        return esValido;
    }
</script>
</asp:Content>
