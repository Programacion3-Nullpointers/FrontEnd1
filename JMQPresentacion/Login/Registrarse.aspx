<%@ Page Title="" Language="C#" MasterPageFile="~/Login/Login.Master" AutoEventWireup="true" CodeBehind="Registrarse.aspx.cs" Inherits="JMQPresentacion.Login.Registrarse" %>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container mt-2" style="max-width: 500px;">
        <h2 class="h4 mb-3">Regístrate</h2> <!-- Título más pequeño -->
        <div class="card">
            <div class="card-body p-3"> <!-- Padding reducido -->
                <div class="mb-2">
                    <label class="form-label small">Tipo de Usuario:</label>
                    <div class="d-flex gap-3 align-items-center"> <!-- gap-3 = 1rem de separación -->
                        <div class="form-check form-check-inline">
                            <asp:RadioButton ID="rbEmpresa" runat="server" GroupName="TipoUsuario"
                                CssClass="form-check-input" AutoPostBack="true"
                                OnCheckedChanged="rblTipoUsuario_SelectedIndexChanged" AssociatedControlID="rbEmpresa"/>
                            <label class="form-check-label">Empresa</label>
                        </div>
                        <div class="form-check form-check-inline">
                            <asp:RadioButton ID="rbCliente" runat="server" GroupName="TipoUsuario"
                                CssClass="form-check-input" AutoPostBack="true" Checked="true"
                                OnCheckedChanged="rblTipoUsuario_SelectedIndexChanged" AssociatedControlID="rbCliente"/>
                            <label class="form-check-label">Persona Natural</label>
                        </div>
                    </div>
                </div>
                <div class="row g-2"> <!-- Espaciado entre filas reducido -->
                    <asp:Panel ID="pnlEmpresa" runat="server" Visible="False">
                        <div class="col-12">
                            <label class="form-label small">Razón Social</label>
                            <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="form-control form-control-sm" placeholder="Razón Social" />
                        </div>
                        <div class="col-12">
                            <label class="form-label small">RUC</label>
                            <asp:TextBox ID="txtRUC" runat="server" CssClass="form-control form-control-sm" placeholder="RUC"/>
                        </div>
                    </asp:Panel>
                    
                    <asp:Panel ID="pnlCliente" runat="server" Visible="True">
                        <div class="col-12">
                            <label class="form-label small">DNI</label>
                            <asp:TextBox ID="txtDNI" runat="server" CssClass="form-control form-control-sm" placeholder="DNI" />
                        </div>
                    </asp:Panel>

                    <!-- Campos comunes con clases reducidas -->
                    <div class="col-md-6">
                        <label class="form-label small">Nombre</label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control form-control-sm" placeholder="Nombre"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <label class="form-label small">Apellido</label>
                        <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control form-control-sm" placeholder="Apellidos"></asp:TextBox>
                    </div>
                    <div class="col-12">
                        <label class="form-label small">Dirección</label>
                        <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control form-control-sm" placeholder="Dirección"></asp:TextBox>
                    </div>
                    <div class="col-12">
                        <label class="form-label small">Correo electrónico</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control form-control-sm" placeholder="usuario@example.com" TextMode="Email"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <label class="form-label small">Contraseña</label>
                        <asp:TextBox ID="txtContr" runat="server" CssClass="form-control form-control-sm" placeholder="Contraseña" TextMode="Password"></asp:TextBox>
                    </div>
                    <div class="col-md-6">
                        <label class="form-label small">Confirmar Contraseña</label>
                        <asp:TextBox ID="txtContrConf" runat="server" CssClass="form-control form-control-sm" placeholder="Contraseña" TextMode="Password"></asp:TextBox>
                    </div>
                </div>

                <div class="row mt-2 g-2"> <!-- Margen superior y espaciado reducido -->
                    <div class="col-12 d-flex justify-content-between">
                        <asp:Button ID="btnGuardar" runat="server" Text="Registrarse" CssClass="btn btn-jmq w-100" OnClick="btnGuardar_Click"/> 
                    </div>
                    <div class="col-12 mt-2 alert alert-danger py-1 px-2 small" id="divError" runat="server" style="display:none;">
                        <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
                    </div>
                </div>
                <div class="row mt-3 text-center">
                    <a href="Login.aspx" class="ms-1" Style="color: gray; text-decoration: none;">Volver al inicio </a>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="registroExitosoModal" tabindex="-1" aria-labelledby="registroExitosoLabel" aria-hidden="true">
      <div class="modal-dialog modal-dialog-centered">
        <div class="modal-content">
          <div class="modal-header bg-success text-white">
            <h5 class="modal-title" id="registroExitosoLabel">Registro exitoso</h5>
          </div>
          <div class="modal-body">
            ¡Tu usuario fue registrado correctamente!
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-success" data-bs-dismiss="modal">Aceptar</button>
          </div>
        </div>
      </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
