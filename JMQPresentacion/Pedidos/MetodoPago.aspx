<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="MetodoPago.aspx.cs" Inherits="JMQPresentacion.Pedidos.MetodoPago" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .metodo-opcion {
            display: flex;
            align-items: center;
            gap: 10px;
            border: 2px solid #ccc;
            border-radius: 8px;
            padding: 10px;
            transition: border 0.3s ease;
            cursor: pointer;
        }

        .metodo-opcion:hover {
            border-color: #ff9800;
        }

        input[type=radio]:checked + .metodo-opcion {
            border-color: #ff5722;
            background-color: #fff3e0;
        }
        .visually-hidden {
            position: absolute !important;
            width: 1px;
            height: 1px;
            padding: 0;
            margin: -1px;
            overflow: hidden;
            clip: rect(0,0,0,0);
            border: 0;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="row">

        <div class="col-md-8 mb-4">
            <div class="card shadow-sm p-4">
                <h4 class="mb-4 text-orange"><i class="bi bi-credit-card"></i> Método de pago</h4>

                <div class="mb-3">
                    <label class="form-label fw-bold">1. Tipo de comprobante <span class="text-danger">*</span></label><br />
                    <asp:RadioButtonList ID="rblComprobante" runat="server" RepeatDirection="Horizontal" CssClass="form-check" AutoPostBack="true" OnSelectedIndexChanged="rblComprobante_SelectedIndexChanged">
                        <asp:ListItem Text="Boleta" Value="Boleta" Selected="True" />
                        <asp:ListItem Text="Factura" Value="Factura" />
                    </asp:RadioButtonList>
                </div>

                <asp:Panel ID="pnlFactura" runat="server" Visible="false">
                    <div class="row mb-3">
                        <div class="col-md-6">
                            <label class="form-label fw-bold">Razón Social <span class="text-danger">*</span></label>
                            <asp:TextBox ID="txtRazonSocial" runat="server" CssClass="form-control" />
                        </div>
                        <div class="col-md-6">
                            <label class="form-label fw-bold">RUC <span class="text-danger">*</span></label>
                            <asp:TextBox ID="txtRUC" runat="server" CssClass="form-control" />
                        </div>
                    </div>
                </asp:Panel>

                <div class="mb-3">
                    <label class="form-label fw-bold">2. Elige un método de pago <span class="text-danger">*</span></label>
                    <div class="row g-3">
                        <div class="col-md-6">
                            <asp:RadioButton ID="rbInterbank" runat="server" GroupName="MetodoPago"  CssClass="visually-hidden" AutoPostBack="true" OnCheckedChanged="MetodoPago_Changed" />
                            <label onclick="document.getElementById('<%= rbInterbank.ClientID %>').click();" class="form-control metodo-opcion">
                                <span>Tarjetas Interbank</span>
                            </label>
                        </div>
                        <div class="col-md-6">
                            <asp:RadioButton ID="rbVisa" runat="server" GroupName="MetodoPago"  CssClass="visually-hidden" AutoPostBack="true" OnCheckedChanged="MetodoPago_Changed" />
                            <label onclick="document.getElementById('<%= rbVisa.ClientID %>').click();" class="form-control metodo-opcion">
                                <span>Otras tarjetas de Crédito y Débito</span>
                            </label>
                        </div>
                        <div class="col-md-6">
                            <asp:RadioButton ID="rbSaldo" runat="server" GroupName="MetodoPago" CssClass="visually-hidden" AutoPostBack="true" OnCheckedChanged="MetodoPago_Changed" />
                            <label onclick="document.getElementById('<%= rbSaldo.ClientID %>').click();" class="form-control metodo-opcion">
                                <i class="bi bi-wallet2"></i>
                                <span>Saldo virtual</span>
                            </label>
                        </div>
                        <div class="col-md-6">
                            <asp:RadioButton ID="rbEfectivo" runat="server" GroupName="MetodoPago" CssClass="visually-hidden" AutoPostBack="true" OnCheckedChanged="MetodoPago_Changed" />
                            <label onclick="document.getElementById('<%= rbEfectivo.ClientID %>').click();" class="form-control metodo-opcion">
                                <i class="bi bi-wallet2"></i>
                                <span>Efectivo</span>
                            </label>
                        </div>
                    </div>
                </div>

                <asp:Panel ID="pnlVisa" runat="server" Visible="false" CssClass="mt-4">
                    <div class="text-center mb-4">
                        <h5>Pago con tarjeta</h5>
                    </div>
    
                    <div class="row g-3">
                        <div class="col-md-12">
                            <label class="form-label">Número de tarjeta</label>
                            <asp:TextBox ID="txtNumeroTarjeta" runat="server" CssClass="form-control" MaxLength="16" placeholder="•••• •••• •••• ••••" />
                        </div>

                        <div class="col-md-6">
                            <label class="form-label">CVV</label>
                            <asp:TextBox ID="txtCVV" runat="server" CssClass="form-control" MaxLength="3" placeholder="123" TextMode="Password" />
                        </div>

                        <div class="col-md-6">
                            <label class="form-label">Fecha de expiración</label>
                            <asp:TextBox ID="txtFechaExp" runat="server" CssClass="form-control" MaxLength="5" placeholder="MM/AA" />
                        </div>
                    </div>
                    <div class="text-center mt-4">
                        <p>Para continuar, haz click en "PAGAR".</p>
                        <asp:Button ID="btnPagar" runat="server" Text="PAGAR" CssClass="btn btn-primary w-100 fw-bold" OnClick="btnPagar_Click" />
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlSaldo" runat="server" Visible="false" CssClass="mt-4">
                    <div class="text-center mb-4">
                        <h5>Pago con saldo virtual</h5>
                    </div>
                    <div class="text-center">
                        <h4 class="text-primary">Tu saldo actual:</h4>
                        <h2 class="text-success mb-3"><asp:Label ID="lblSaldoPago" runat="server" /></h2>
                        <asp:Button ID="btnRecargarSaldo" runat="server" CssClass="btn btn-outline-primary fw-bold" Text="Recargar saldo" OnClick="btnRecargarSaldo_Click" />
                    </div>
                    <div class="text-center mt-4">
                        <p>Para continuar, haz click en "PAGAR".</p>
                        <asp:Button ID="btnPagarSaldo" runat="server" Text="PAGAR" CssClass="btn btn-primary w-100 fw-bold" OnClick="btnPagar_Click" />
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlEfectivo" runat="server" Visible="false" CssClass="mt-4">
                    <div class="text-center mb-4">
                        <h5>Pago en efectivo</h5>
                    </div>
                    <div class="text-center mt-4">
                        <p>Para continuar, haz click en "FINALIZAR COMPRA".</p>
                        <asp:Button ID="btnEfectivo" runat="server" Text="FINALIZAR COMPRA" CssClass="btn btn-primary w-100 fw-bold" OnClick="btnPagar_Click" />
                    </div>
                </asp:Panel>
            </div>

            <div class="col-12 mt-3 alert alert-danger py-1 px-2 small" id="divError" runat="server" style="display:none;">
                <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
            </div>
        </div> <div class="col-md-4">
            <div class="card shadow-sm p-3">
                <h5 class="fw-bold">Resumen de tu compra</h5>
                <hr />
                <div class="d-flex justify-content-between">
                    <span>Subtotal</span>
                    <span><asp:Label ID="lblTotal" runat="server" /></span>
                </div>
                <div class="d-flex justify-content-between">
                    <span>Entrega</span>
                    <span>Gratis</span>
                </div>
                <hr />
                <div class="d-flex justify-content-between">
                    <h5>Total</h5>
                    <h5><asp:Label ID="lblTotal2" runat="server" /></h5>
                </div>
                <div class="text-end mt-2">
                    <small><a href="#" class="text-decoration-none text-primary">Calcula tus cuotas con Tarjeta oh!</a></small>
                </div>
                <hr />
                <a href="/Principal/Principal.aspx" class="btn btn-outline-secondary w-100 mt-2">
                    <i class="bi bi-arrow-left"></i> Volver al carrito
                </a>
            </div>
        </div> 
    </div> 
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
