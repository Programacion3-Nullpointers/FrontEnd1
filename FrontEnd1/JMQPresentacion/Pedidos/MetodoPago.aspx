<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="MetodoPago.aspx.cs" Inherits="JMQPresentacion.Pedidos.MetodoPago" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
     <!-- Columna izquierda: Carro de compras -->
    <div class="col-md-8 mb-4"> 
        <div class="card shadow-sm">
          <div class="card-header bg-white border-bottom">
            <h4 class="mb-0"><i class="bi bi-cart-fill text-warning"></i> Metodo de Pago</h4>
          </div>
           <div class="mb-4">
                <label class="form-label fw-bold">1. Tipo de comprobante <span class="text-danger">*</span></label><br />
                <asp:RadioButtonList ID="rblComprobante" runat="server" RepeatDirection="Horizontal" CssClass="form-check">
                    <asp:ListItem Text="Boleta" Value="Boleta" Selected="True" />
                    <asp:ListItem Text="Factura" Value="Factura" />
                </asp:RadioButtonList>
           </div>
            <div class="mb-4">
                <label class="form-label fw-bold">2. Elige un método de pago <span class="text-danger">*</span></label>

                <div class="row g-3">
                    <div class="col-md-6">
                        <asp:RadioButton ID="rbInterbank" runat="server" GroupName="MetodoPago" CssClass="d-none" OnCheckedChanged="MetodoPago_Changed" AutoPostBack="true"/>
                        <label for="rbInterbank" class="form-control border metodo-opcion">
                            <img src="interbank.png" alt="Interbank" height="20" />
                            <span class="ms-2">Tarjetas Interbank o Divídelo</span>
                        </label>
                    </div>

                    <div class="col-md-6">
                        <asp:RadioButton ID="rbVisa" runat="server" GroupName="MetodoPago" CssClass="d-none" OnCheckedChanged="MetodoPago_Changed" AutoPostBack="true"/>
                        <label for="rbVisa" class="form-control border metodo-opcion">
                            <img src="visa.png" alt="Visa" height="20" />
                            <span class="ms-2">Otras tarjetas de Crédito y Débito</span>
                        </label>
                    </div>
        
                    <!-- Agrega más métodos aquí -->
                </div>
                <asp:Panel ID="pnlVisa" runat="server" Visible="False">
                    <div class="text-center">
                        <h5>Pago al contado</h5>
                        <h4 class="text-success">S/ 210.00</h4>
                        <p>Para continuar, haz click en "IR A PAGAR".</p>
                    </div>
                    <asp:Button ID="Button1" runat="server" Text="IR A PAGAR" CssClass="btn btn-primary w-100" OnClick="btnPagar_Click" />
                </asp:Panel>
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

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
