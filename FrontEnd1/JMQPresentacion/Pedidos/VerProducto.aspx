<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="VerProducto.aspx.cs" Inherits="JMQPresentacion.Pedidos.VerProducto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <div class="centered-container">
            <div class="container fuenteArial">
                <div class="category">Categoría: Herramientas</div>
                <div class="product">
                    <div class="product-image">
                        <img src="https://media.falabella.com/sodimacPE/4205634_01/w=1500,h=1500,fit=pad" alt="Martillo Fibra de Vidrio 16Oz Stanley" width="250px">
                    </div>
                    <div class="product-details">
                        <h1>Martillo Fibra de Vidrio 16Oz Stanley</h1>
                        <div class="price">
                            <span>S/ 47.90 / Unidad</span>
                        </div>
                        <div class="add-to-cart">
                            <button type="button" onclick="window.location.href='/Pedidos/Carrito.aspx'">Agregar al carro</button>
                        </div>
                    </div>
                </div>
            </div>
    </div>
    <br />
    <div class="centered-container fuenteArial">
        <h3 style="text-align: left">Descripción: </h3>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
