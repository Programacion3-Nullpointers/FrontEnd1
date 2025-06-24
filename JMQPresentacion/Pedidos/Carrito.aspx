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
                                    <!-- Imagen del producto -->
                                    <div class="col-md-2 d-flex align-items-center">
                                        <div class="image-container">
                                            <asp:Image ID="imgProducto" runat="server"
                                                ImageUrl='<%# ConvertirByteAImagenBase64((byte[])Eval("producto.imagen")) %>'
                                                Width="100%" CssClass="img-fluid" />
                                        </div>
                                    </div>

                                    <!-- Nombre del producto -->
                                    <div class="col-md-4 d-flex align-items-center">
                                        <h6><strong><%# Eval("producto.nombre") %></strong></h6>
                                    </div>

                                    <!-- Precio / Cantidad / Eliminar -->
                                    <div class="col-md-4 d-flex align-items-center justify-content-center">
                                        <div>
                                            <strong>Precio unitario</strong><br />
                                            <span>S/ <%# Eval("precio_unitario") %></span>
                                        </div>
                                        <div class="input-group mt-2" style="width: 110px; margin-left: auto;">
                                            <asp:Button ID="btnMenos" runat="server"
                                                CssClass='<%# ((int)Eval("cantidad") == 1) ? "btn btn-outline-secondary btn-sm btn-deshabilitado" : "btn btn-outline-secondary btn-sm" %>'
                                                Text="-" CommandArgument='<%# Container.ItemIndex %>' OnClick="CambiarCantidad" />
                                            <asp:TextBox ID="txtCantidad" runat="server"
                                                CssClass="form-control form-control-sm text-center"
                                                Text='<%# Eval("cantidad") %>' ReadOnly="true" />
                                            <asp:Button ID="btnMas" runat="server"
                                                CssClass='<%# ((int)Eval("cantidad") == (int)Eval("producto.stock")) ? "btn btn-outline-secondary btn-sm btn-deshabilitado" : "btn btn-outline-secondary btn-sm" %>'
                                                Text="+" CommandArgument='<%# Container.ItemIndex %>' OnClick="CambiarCantidad" />
                                        </div>
                                        <asp:Button ID="btnEliminar" runat="server"
                                            CssClass="btn btn-link text-danger small mt-1"
                                            Text="✖" CommandArgument='<%# Container.ItemIndex %>' OnClick="btnEliminarProducto_Click" />
                                    </div>

                                    <!-- Subtotal -->
                                    <div class="col-md-2 text-end d-flex flex-column justify-content-center">
                                        <div>
                                            <strong>Subtotal</strong><br />
                                            <span>S/ <%# Convert.ToDecimal(Eval("cantidad")) * Convert.ToDecimal(Eval("precio_unitario")) %></span>
                                        </div>
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

                        <!-- Botón visible si NO está logueado -->
                        <asp:PlaceHolder ID="phBotonSesion" runat="server" Visible="false">
                            <a href="/Login/Login.aspx?redirect=Pedidos/Carrito.aspx" class="btn btn-primary w-100 mt-3 fw-bold">
                                Iniciar sesión para continuar
                            </a>
                        </asp:PlaceHolder>

                        <!-- Botón visible si está logueado -->
                        <asp:PlaceHolder ID="phBotonCheckout" runat="server" Visible="false">
                            <asp:Button ID="btnCheckout" runat="server" CssClass="btn btn-warning w-100 mt-3 fw-bold" Text="Ir al checkout" OnClick="btnCheckout_Click" />
                        </asp:PlaceHolder>

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
    <!-- Ya no hay JavaScript adicional porque se eliminó el SweetAlert -->
</asp:Content>
