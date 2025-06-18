<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="DatosEntrega.aspx.cs" Inherits="JMQPresentacion.Pedidos.DatosEntrega" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
        <div class="container mt-4">
          <div class="row">
            <!-- Columna izquierda: Carro de compras -->
            <div class="col-md-8 mb-4">
              <div class="card shadow-sm">
                <div class="card-header bg-white border-bottom">
                  <h4 class="mb-0"><i class="bi bi-cart-fill text-warning"></i> Detalle de entrega</h4>
                </div>
                <div class="btn-group w-100 mb-4" role="group">
                    <asp:Button ID="btnDespacho" runat="server" Text="Despacho a domicilio"
                        CssClass="btn btn-warning fw-bold" OnClick="btnDespacho_Click"/>
                    <asp:Button ID="btnRetiro" runat="server" Text="Retiro en tienda"
                        CssClass="btn btn-light" OnClick="btnRetiro_Click"/>
                </div>
                <asp:Panel ID="pnlDespacho" runat="server" Visible="True">
                    <div class="row mb-3">
                        <div class="col-md-6">
                            <label>Dirección (Av/Jr/Calle) <span style="color: red;">*</span></label>
                            <asp:TextBox ID="txtDireccion" runat="server" CssClass="form-control" Placeholder="Av. Siempre Viva"></asp:TextBox>
                        </div>
                        <div class="col-md-2">
                            <label>Nro. <span style="color: red;">*</span></label>
                            <asp:TextBox ID="txtNumero" runat="server" CssClass="sin-flechas form-control" Placeholder="123"></asp:TextBox>
                        </div>
                        <div class="col-md-4">
                            <label>Piso/Dpto. (Ej. 3er piso / 302)</label>
                            <asp:TextBox ID="txtPisoDpto" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <!-- Referencia -->
                        <div class="mb-3">
                            <label>Referencia</label>
                            <asp:TextBox ID="txtReferencia" runat="server" CssClass="form-control" Placeholder="Frente a parque, puerta verde, etc."></asp:TextBox>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlRetiro" runat="server" Visible="False">
                    <div class="row mb-3">
                        <div class="col-md-6">
                            <label>Ingresa DNI <span style="color: red;">*</span></label>
                            <asp:TextBox ID="txtDni" runat="server" CssClass="form-control" Placeholder="12345678"></asp:TextBox>
                        </div>
                    </div>
                </asp:Panel>
                <div class="card-footer text-end small text-muted">
                  Productos vendidos y despachados por: JMQOnline
                </div>
              </div>
            </div>

            <!-- Columna derecha: Resumen -->
            <div class="col-md-4">
                <div class="card shadow-sm">
                    <div class="card-body">
                        <div class="d-flex justify-content-between">
                            <strong>Resumen de tu compra:</strong>
                           <a href="#" class="text-decoration-none text-primary small">Agregar cupón</a>
                        </div>
                        <hr />
                        <div class="d-flex justify-content-between">
                            <span>Total productos</span>
                            <span><asp:Label ID="lblTotal" runat="server" /></span>
                        </div>
                        <div class="d-flex justify-content-between mt-2">
                            <h5>Total</h5>
                            <h5><asp:Label ID="lblTotal2" runat="server" /></h5>
                        </div>
                        <asp:Button ID="btnPagar" runat="server" CssClass="btn btn-warning w-100 mt-3 fw-bold" Text="Pagar" OnClick="btnPagar_Click" />
                        <a href="/Principal/Principal.aspx" class="d-block text-center mt-3 text-decoration-none">
                           <i class="bi bi-arrow-left"></i> Ver más productos
                        </a>
                    </div>
                </div>
            </div>
          </div>
          <!-- div error -->
          <div class="row">
             <div class="col-md-8 mb-4">
                 <div class="col-12 mt-2 alert alert-danger py-1 px-2 small" id="divError" runat="server" style="display:none;">
                     <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
                 </div>
             </div>
          </div>
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
