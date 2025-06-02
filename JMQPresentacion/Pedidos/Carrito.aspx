<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="JMQPresentacion.Pedidos.Carrito" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="container mt-4">
      <div class="row">
        <!-- Columna izquierda: Carro de compras -->
        <div class="col-md-8 mb-4">
          <div class="card shadow-sm">
            <div class="card-header bg-white border-bottom">
              <h4 class="mb-0"><i class="bi bi-cart-fill text-warning"></i> Carro de compras</h4>
            </div>
            <!-- productos -->
            <div class="card-body">
              <asp:Repeater ID="rptCarrito" runat="server">
                  <ItemTemplate>
                    <div class="row mb-3">
                      <div class="col-md-2">
                        <img src='<%# Eval("producto.imagen") %>' class="img-fluid rounded" alt="Producto">
                      </div>
                      <div class="col-md-7">
                        <h6><strong><%# Eval("producto.nombre") %></strong></h6>
                      </div>
                      <div class="col-md-3 text-end">
                        <div>
                          <strong>S/ <%# Eval("precio_unitario") %></strong><br />
                        </div>
                        <div class="input-group mt-2" style="width: 110px; margin-left: auto;">
                          <asp:Button ID="btnMenos" runat="server" CssClass="btn btn-outline-secondary btn-sm"
                            Text="-" CommandArgument='<%# Container.ItemIndex %>' OnClick="CambiarCantidad" />
                          <asp:TextBox ID="txtCantidad" runat="server" CssClass="form-control form-control-sm text-center"
                            Text='<%# Eval("cantidad") %>' ReadOnly="true"></asp:TextBox>
                          <asp:Button ID="btnMas" runat="server" CssClass="btn btn-outline-secondary btn-sm"
                            Text="+" CommandArgument='<%# Container.ItemIndex %>' OnClick="CambiarCantidad" />
                        </div>
                        <asp:Button ID="btnEliminar" runat="server" CssClass="btn btn-link text-danger small mt-1"
                          Text="✖" CommandArgument='<%# Container.ItemIndex %>' OnClick="btnEliminarProducto_Click" />
                      </div>
                    </div>
                  </ItemTemplate>
                </asp:Repeater>
            </div>
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
                    <asp:Button ID="btnCheckout" runat="server" CssClass="btn btn-warning w-100 mt-3 fw-bold" Text="Ir al checkout" OnClick="btnCheckout_Click" />
                    <a href="/Principal/Principal.aspx" class="d-block text-center mt-3 text-decoration-none">
                       <i class="bi bi-arrow-left"></i> Ver más productos
                    </a>
                </div>
            </div>
        </div>
      </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
