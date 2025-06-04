<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="Principal.aspx.cs" Inherits="JMQPresentacion.Principal.Principal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
<style>
    body {
        font-family: Arial, sans-serif;
        margin: 0;
        overflow-x: hidden;
    }

    .content {
        padding: 40px;
        box-sizing: border-box;
        overflow: hidden;
    }

    .welcome-text {
        text-align: center;
        margin-bottom: 30px;
    }

    .welcome-text h1 {
        color: #2c3e50;
        font-size: 28px;
        margin-bottom: 10px;
    }

    .welcome-text h2 {
        color: #34495e;
        font-size: 24px;
        margin-bottom: 5px;
    }

    .welcome-text h3 {
        color: #7f8c8d;
        font-size: 20px;
    }

    .image-container {
        text-align: center;
        margin-top: 30px;
    }

    .image-container img {
        width: 100%;
        max-width: 100%;
        max-height: 400px;
        height: auto;
        border-radius: 10px;
        box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
        display: block;
        margin: 0 auto;
        object-fit: cover;
    }
</style>
     <!-- 🔹 Sección de bienvenida -->
        <div class="container mt-4">
            <h1 class="text-center">¡Bienvenido a JMQ Online!</h1>
            <p class="text-center">Descubre nuestros mejores productos y ofertas exclusivas.</p>
        </div>
        <div class="container-fluid bg-light py-4">
            <div class="container">
                <div class="row text-center">
                <!-- Envío Gratis -->
                <div class="col-md-4 mb-3 mb-md-0 border-end border-secondary">
                    <h5 class="text-uppercase fw-bold mb-2" style="color: #026670;">DELIVERY GRATIS</h5>
                    <p class="mb-0 small">Para todo Lima, Perú<br> para compras a partir de S/. 200</p>
                </div>
            
                <!-- Pago Seguro -->
                <div class="col-md-4 mb-3 mb-md-0 border-end border-secondary">
                    <h5 class="text-uppercase fw-bold mb-2" style="color: #026670;">PAGO SEGURO</h5>
                    <p class="mb-0 small">Aceptamos Visa, American Express<br> y Mastercard</p>
                </div>
            
                <!-- Soporte 24/7 -->
                <div class="col-md-4">
                    <h5 class="text-uppercase fw-bold mb-2" style="color: #026670;">ASISTENCIA 24/7</h5>
                    <p class="mb-0 small">Si tiene algún problema, no dude en contactarnos<br>Llámanos: 203-4077</p>
                </div>
                </div>
            </div>
        </div>

        <!-- 🔹 Sección de productos -->
        <div class="container mt-4">
            <h2 class="text-center mb-4">Productos Destacados</h2>
            <asp:Repeater ID="rptProductos" runat="server">
                <HeaderTemplate>
                    <div class="row"> <!-- Inicio de la primera fila -->
                </HeaderTemplate>

                <ItemTemplate>
                    <div class="col-lg-3 col-md-6 mb-4">
                        <div class="card h-100">
                            <div class="image-container">
                                <asp:Image ID="imgProducto" runat="server" ImageUrl='<%# ConvertirByteAImagenBase64((byte[])Eval("imagen")) %>' />
                            </div>
                            <div class="card-body d-flex flex-column">
                                <h5 class="card-title"><%# Eval("nombre") %></h5>
                                <p class="card-text text-muted"><%# Eval("categoria.nombre") %></p>
                                <p class="fw-bold text-success">S/ <%# Eval("precio") %></p>
                                <asp:Button ID="btnAgregar" runat="server" Text="Agregar al Carrito" CssClass="btn btn-primary mt-auto"
                                    CommandArgument='<%# Eval("id") %>' OnClick="btnAgregarProductos_Click" />
                            </div>
                        </div>
                    </div>
                    <%# (Container.ItemIndex + 1) % 4 == 0 ? "</div><div class='row'>" : "" %> <!-- Cierra y abre fila cada 4 productos -->
                </ItemTemplate>

                <FooterTemplate>
                    </div> <!-- Cierre final de la fila -->
                </FooterTemplate>
            </asp:Repeater>
        </div>
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
</asp:Content>
