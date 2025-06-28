<%@ Page Title="Registro de Usuario" Language="C#" MasterPageFile="~/Login/Login.Master" AutoEventWireup="true" CodeBehind="Registrarse.aspx.cs" Inherits="JMQPresentacion.Login.Registrarse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server" />

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container mt-2" style="max-width: 500px;">
        <h2 class="h4 mb-3 fw-semibold">Regístrate</h2>
        <div class="card shadow-sm">
            <div class="card-body p-3">
                <div class="mb-2">
                    <label class="form-label small fw-semibold">Tipo de Usuario:</label>
                    <div class="d-flex gap-3 align-items-center">
                        <div class="form-check form-check-inline">
                            <asp:RadioButton ID="rbEmpresa" runat="server" GroupName="TipoUsuario"
                                CssClass="form-check-input" AutoPostBack="true"
                                OnCheckedChanged="rblTipoUsuario_SelectedIndexChanged" AssociatedControlID="rbEmpresa" />
                            <label class="form-check-label">Empresa</label>
                        </div>
                        <div class="form-check form-check-inline">
                            <asp:RadioButton ID="rbCliente" runat="server" GroupName="TipoUsuario"
                                CssClass="form-check-input" AutoPostBack="true" Checked="true"
                                OnCheckedChanged="rblTipoUsuario_SelectedIndexChanged" AssociatedControlID="rbCliente" />
                            <label class="form-check-label">Persona Natural</label>
                        </div>
                    </div>
                </div>

                <div class="row g-2">
                    <asp:Panel ID="pnlEmpresa" runat="server" Visible="False">
                        <div class="col-12">
                            <label class="form-label small">Razón Social</label>
                            <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="form-control form-control-sm" placeholder="Razón Social" />
                            <span id="errorRazonSocial" class="text-danger small d-none"></span>
                        </div>
                        <div class="col-12">
                            <label class="form-label small">RUC</label>
                            <asp:TextBox ID="txtRUC" runat="server" CssClass="form-control form-control-sm" placeholder="RUC" />
                            <span id="errorRUC" class="text-danger small d-none"></span>
                        </div>
                    </asp:Panel>

                    <asp:Panel ID="pnlCliente" runat="server" Visible="True">
                        <div class="col-12">
                            <label class="form-label small">DNI</label>
                            <asp:TextBox ID="txtDNI" runat="server" CssClass="form-control form-control-sm" placeholder="DNI" />
                            <span id="errorDNI" class="text-danger small d-none"></span>
                        </div>
                    </asp:Panel>

                    <div class="col-md-6">
                        <label class="form-label small">Nombre</label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control form-control-sm" placeholder="Nombre"></asp:TextBox>
                        <span id="errorNombre" class="text-danger small d-none"></span>
                    </div>
                    <div class="col-md-6">
                        <label class="form-label small">Apellido</label>
                        <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control form-control-sm" placeholder="Apellidos"></asp:TextBox>
                        <span id="errorApellido" class="text-danger small d-none"></span>
                    </div>
                    <div class="col-12">
                        <label class="form-label small">Dirección</label>
                        <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control form-control-sm" placeholder="Dirección"></asp:TextBox>
                    </div>
                    <div class="col-12">
                        <label class="form-label small">Correo electrónico</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control form-control-sm" placeholder="usuario@example.com" TextMode="Email"></asp:TextBox>
                        <span id="errorEmail" class="text-danger small d-none"></span>
                    </div>
                    <div class="col-md-6">
                        <label class="form-label small">Contraseña</label>
                        <asp:TextBox ID="txtContr" runat="server" CssClass="form-control form-control-sm" placeholder="Contraseña" TextMode="Password"></asp:TextBox>
                        <span id="errorContr" class="text-danger small d-none"></span>
                    </div>
                    <div class="col-md-6">
                        <label class="form-label small">Confirmar Contraseña</label>
                        <asp:TextBox ID="txtContrConf" runat="server" CssClass="form-control form-control-sm" placeholder="Contraseña" TextMode="Password"></asp:TextBox>
                        <span id="errorConf" class="text-danger small d-none"></span>
                    </div>
                </div>

                <div class="row mt-3">
                    <div class="col-12 d-grid">
                        <asp:Button ID="btnGuardar" runat="server" Text="Registrarse" CssClass="btn btn-jmq fw-semibold" OnClick="btnGuardar_Click" OnClientClick="return validarAntesDeEnviar();" />
                    </div>
                </div>

                <div class="row mt-3 text-center">
                    <a href="Login.aspx" class="ms-1" style="color: gray; text-decoration: none;">Volver al inicio</a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
<script type="text/javascript">
    document.addEventListener("DOMContentLoaded", function () {
        const campos = [
            { id: '<%= txtNombre.ClientID %>', errorId: "errorNombre", min: 2, label: "nombre" },
            { id: '<%= txtApellido.ClientID %>', errorId: "errorApellido", min: 2, label: "apellido" },
            { id: '<%= txtEmail.ClientID %>', errorId: "errorEmail", regex: /^[^@\s]+@[^@\s]+\.[^@\s]+$/, label: "correo electrónico" },
            { id: '<%= txtContr.ClientID %>', errorId: "errorContr", min: 8, label: "contraseña" },
            { id: '<%= txtContrConf.ClientID %>', errorId: "errorConf", custom: validarCoincidencia },
            { id: '<%= txtDNI.ClientID %>', errorId: "errorDNI", regex: /^\d{8}$/, label: "DNI (8 dígitos)" },
            { id: '<%= txtRazonSocial.ClientID %>', errorId: "errorRazonSocial", min: 2, label: "razón social" },
            { id: '<%= txtRUC.ClientID %>', errorId: "errorRUC", regex: /^\d{11}$/, label: "RUC (11 dígitos)" }
        ];

        campos.forEach(campo => {
            const input = document.getElementById(campo.id);
            if (input) {
                input.addEventListener("input", function () {
                    validarCampo(campo);
                });
            }
        });

        function validarCampo(campo) {
            const valor = document.getElementById(campo.id)?.value.trim();
            const errorSpan = document.getElementById(campo.errorId);
            let mensaje = "";

            if (!errorSpan) return;

            if (campo.custom) {
                mensaje = campo.custom();
            } else if (campo.min && valor.length < campo.min) {
                mensaje = `El ${campo.label} debe tener al menos ${campo.min} caracteres.`;
            } else if (campo.regex && !campo.regex.test(valor)) {
                mensaje = `El ${campo.label} no es válido.`;
            }

            if (mensaje) {
                errorSpan.textContent = mensaje;
                errorSpan.classList.remove("d-none");
            } else {
                errorSpan.textContent = "";
                errorSpan.classList.add("d-none");
            }
        }

        function validarCoincidencia() {
            const pass = document.getElementById('<%= txtContr.ClientID %>').value.trim();
            const conf = document.getElementById('<%= txtContrConf.ClientID %>').value.trim();
            return pass !== conf ? "Las contraseñas no coinciden." : "";
        }
    });

    function validarAntesDeEnviar() {
        const errores = document.querySelectorAll("span.text-danger:not(.d-none)");
        return errores.length === 0;
    }
</script>
</asp:Content>
