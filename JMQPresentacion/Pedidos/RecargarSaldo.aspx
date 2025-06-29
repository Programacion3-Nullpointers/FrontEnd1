<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="RecargarSaldo.aspx.cs" Inherits="JMQPresentacion.Pedidos.RecargarSaldo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container mt-4" style="max-width: 500px;">
        <h2 class="text-center" style="font-family:Roboto;">Recargar saldo</h2>
        <div class="card shadow-sm">
            <div class="card-body">
                <div class="text-center">
                    <h4 class="text-primary">Tu saldo actual:</h4>
                    <h2 class="text-success mb-3"><asp:Label ID="lblSaldo" runat="server" /></h2>
                </div>
                <div class="mb-3">
                    <label class="form-label">Monto a recargar (S/)</label>
                    <asp:TextBox ID="txtMonto" runat="server" CssClass="form-control" placeholder="Ingrese el monto" TextMode="Number" />
                </div>

                <hr />
                <h5 class="text-muted">Datos de tarjeta</h5>

                <div class="mb-3">
                    <label class="form-label">Número de tarjeta</label>
                    <asp:TextBox ID="txtNumero" runat="server" CssClass="form-control" MaxLength="16" placeholder="•••• •••• •••• ••••" />
                </div>

                <div class="row">
                    <div class="col-md-6 mb-3">
                        <label class="form-label">CVV</label>
                        <asp:TextBox ID="txtCVV" runat="server" CssClass="form-control" MaxLength="3" placeholder="123" TextMode="Password" />
                    </div>
                    <div class="col-md-6 mb-3">
                        <label class="form-label">Fecha de expiración</label>
                        <asp:TextBox ID="txtExp" runat="server" CssClass="form-control" MaxLength="5" placeholder="MM/AA" />
                    </div>
                </div>

                <div class="mt-4 text-center">
                    <asp:Button ID="btnRecargar" runat="server" CssClass="btn btn-success w-100 fw-bold" Text="Recargar saldo" OnClick="btnRecargar_Click" />
                </div>
                <asp:Panel ID="pnlRegresar" runat="server" Visible="false" CssClass="text-center mt-3">
                    <asp:Button ID="btnVolverPago" runat="server" Text="← Regresar al pago" CssClass="btn btn-outline-secondary" PostBackUrl="/Pedidos/MetodoPago.aspx" />
                </asp:Panel>
                <div class="col-12 mt-2 alert alert-danger py-1 px-2 small" id="divError" runat="server" style="display:none;">
                    <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
                </div>
                <div class="col-12 mt-2 alert alert-success py-1 px-2 small" id="divExito" runat="server" style="display:none;">
                    <asp:Label ID="lblExito" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
</asp:Content>
