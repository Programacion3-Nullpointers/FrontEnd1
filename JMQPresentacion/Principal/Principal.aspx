<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="Principal.aspx.cs" Inherits="JMQPresentacion.Principal.Principal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="Public/css/styles.css" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <!-- 🔹 ScriptManager necesario para SweetAlert -->
    <asp:ScriptManager ID="ScriptManager1" runat="server" />

    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 0;
            overflow-x: hidden;
        }

        .card-img-container {
            width: 100%;
            height: 200px;
            background-size: contain;
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

  <!-- 🔍 Buscador + Filtros (ajustado) -->
    <div class="container bg-light mt-4 p-4 rounded-4 shadow-sm border">
        <div class="row g-3">
            <!-- Fila superior: Buscador y botón -->
            <div class="col-md-8">
                <asp:TextBox ID="txtBuscarNombre" runat="server" CssClass="form-control shadow-sm" placeholder="Buscar producto..." />
            </div>
            <div class="col-md-4">
                <asp:Button ID="btnBuscarNombre" runat="server" Text="🔍"
                CssClass="btn btn-outline-primary shadow-sm px-3"
                OnClick="btnBuscarNombre_Click" />
            </div>

            <!-- Fila inferior: Checkbox y Categoría -->
            <div class="col-md-4 d-flex align-items-center">
                <asp:CheckBox 
                    ID="chkOfertas" 
                    runat="server" 
                    AutoPostBack="true"
                    OnCheckedChanged="chkOfertas_CheckedChanged"
                    CssClass="form-check-input me-2 shadow-sm" />
                <label class="form-check-label fw-semibold text-success" for="chkOfertas">
                    🎁 Ofertas especiales
                </label>
            </div>

            <div class="col-md-8">
                <label for="ddlCategoria" class="form-label fw-bold text-primary mb-1">🔍 Categoría:</label>
                <asp:DropDownList 
                    ID="ddlCategoria" 
                    runat="server" 
                    AutoPostBack="true"
                    OnSelectedIndexChanged="ddlCategoria_SelectedIndexChanged"
                    CssClass="form-select border-primary shadow-sm" />
            </div>
        </div>
    </div>



    <!-- 🔹 Productos Destacados -->
    <div class="container bg-white mt-5">
        <h2 class="text-center bg-white mb-4">Productos Destacados</h2>
        <!-- ✅ Mensaje si no se encuentran productos -->
        <asp:Label ID="lblMensaje" runat="server" Visible="false" CssClass="text-danger text-center fw-bold d-block mb-3" />

        <asp:Repeater ID="rptProductos" runat="server">
            <HeaderTemplate>
                <div class="row gx-4 gx-lg-5 row-cols-2 row-cols-md-3 row-cols-xl-4 justify-content-center">
            </HeaderTemplate>

            <ItemTemplate>
                <div class="col mb-5">
                    <div class="card h-100 shadow border-0">
                        <!-- Imagen -->
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
                                OnClientClick='<%# "agregarAlCarrito(" + Eval("id") + "); return false;" %>' />
                        </div>
                    </div>
                </div>
            </ItemTemplate>

            <FooterTemplate>
                </div> <!-- cierre del grid -->
            </FooterTemplate>
        </asp:Repeater>
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
    <!-- 🔹 Sección Cotizar -->
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
    <!-- ✅ SweetAlert2 -->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <!-- ✅ Script para agregar al carrito -->
    <script>
        function agregarAlCarrito(idProducto) {
            fetch(`/AgregarCarrito.ashx?id=${idProducto}`, { credentials: 'include' })
                .then(res => res.json())
                .then(data => {
                    if (data.success) {
                        Swal.fire({
                            icon: 'success',
                            title: 'Agregado al carrito',
                            text: data.message,
                            timer: 1500,
                            showConfirmButton: false
                        });
                    } else {
                        Swal.fire({
                            icon: 'error',
                            title: 'Error',
                            text: data.message
                        });
                    }
                })
                .catch(() => {
                    Swal.fire({
                        icon: 'error',
                        title: 'Error inesperado',
                        text: 'No se pudo agregar el producto al carrito.'
                    });
                });
        }
    </script>

    <!-- Bootstrap -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
    <script src="Public/js/scripts.js"></script>
</asp:Content>
