<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="Principal.aspx.cs" Inherits="JMQPresentacion.Principal.Principal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Public/css/styles.css" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 0;
            overflow-x: hidden;
        }
      .card-img-container {
            width: 100%;
            height: 200px;
            background-size: 120% auto;
            background-position: center center;
            background-repeat: no-repeat;
            background-color: #fff;
            border-top-left-radius: 0.5rem;
            border-top-right-radius: 0.5rem;
        }


    </style>

    <!-- 🔹 Sección de bienvenida -->
    <div class="container bg-white mt-4">
        <h1 class="text-center">¡Bienvenido a JMQ Online!</h1>
        <p class="text-center">Descubre nuestros mejores productos y ofertas exclusivas.</p>
    </div>

    <!-- 🔹 Beneficios -->
    <div class="container-fluid bg-white py-4">
        <div class="container">
            <div class="row text-center">
                <div class="col-md-4 mb-3 mb-md-0 border-end border-secondary">
                    <h5 class="text-uppercase fw-bold mb-2" style="color: #026670;">DELIVERY GRATIS</h5>
                    <p class="mb-0 small">Para todo Lima, Perú<br> para compras a partir de S/. 200</p>
                </div>
                <div class="col-md-4 mb-3 mb-md-0 border-end border-secondary">
                    <h5 class="text-uppercase fw-bold mb-2" style="color: #026670;">PAGO SEGURO</h5>
                    <p class="mb-0 small">Aceptamos Visa, American Express<br> y Mastercard</p>
                </div>
                <div class="col-md-4">
                    <h5 class="text-uppercase fw-bold mb-2" style="color: #026670;">ASISTENCIA 24/7</h5>
                    <p class="mb-0 small">Si tiene algún problema, no dude en contactarnos<br>Llámanos: 203-4077</p>
                </div>
            </div>
        </div>
    </div>

    <!-- 🔹 Productos Destacados -->
    <div class="container bg-white mt-5">
        <h2 class="text-center bg-white mb-4">Productos Destacados</h2>
        <asp:Repeater ID="rptProductos" runat="server">
            <HeaderTemplate>
                <div class="row gx-4 gx-lg-5 row-cols-2 row-cols-md-3 row-cols-xl-4 justify-content-center">
            </HeaderTemplate>

            <ItemTemplate>
                <div class="col mb-5">
                    <div class="card h-100 shadow border-0">
                        <!-- Imagen con fondo (reemplazo de <img>) -->
                        <div class="card-img-container"
                             style='<%# "background-image: url(" + ConvertirByteAImagenBase64((byte[])Eval("imagen")) + ");" %>'>
                        </div>

                        <!-- Detalles -->
                        <div class="card-body text-center p-4 d-flex flex-column">
                            <h5 class="card-title fw-bold"><%# Eval("nombre") %></h5>
                            <p class="text-muted small mb-1"><%# Eval("categoria.nombre") %></p>
                            <p class="fw-bold text-success mb-2">S/ <%# Eval("precio") %></p>

                            <asp:Button ID="btnAgregar" runat="server" Text="Agregar al Carrito"
                                CssClass="btn btn-outline-primary mt-auto"
                                CommandArgument='<%# Eval("id") %>'
                                OnClick="btnAgregarProductos_Click" />
                        </div>
                    </div>
                </div>
            </ItemTemplate>


            <FooterTemplate>
                </div> <!-- Cierre final del grid -->
            </FooterTemplate>
        </asp:Repeater>
    </div>

    <!-- 🔹 Sección de cotización -->
    <div class="container mt-5 mb-5 text-center">
        <div class="row justify-content-center">
            <div class="col-md-8 p-4 border rounded" style="background-color: #f8f9fa;">
                <h3 class="mb-3" style="color: #026670;">¿No encuentras lo que buscas?</h3>
                <p class="lead mb-4">¡Nosotros lo conseguimos para ti! Solicita una cotización personalizada y te ayudaremos a encontrar el producto ideal.</p>
                <asp:Button ID="btnCotizar" runat="server" Text="Cotizar Ahora" 
                    CssClass="btn btn-success btn-lg px-4" 
                    OnClick="btnCotizar_Click" />
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <script src="Public/js/scripts.js"></script>
</asp:Content>
