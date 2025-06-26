    <%@ Page Title="Generador de Reportes" Language="C#" MasterPageFile="~/Admin/MainLayout2.Master" AutoEventWireup="true" CodeBehind="Reportes.aspx.cs" Inherits="JMQPresentacion.Admin.Reportes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .recuadro-reporte {
            background-color: #f8f9fa;
            padding: 20px;
            margin-bottom: 30px;
            border: 1px solid #dee2e6;
            border-radius: 8px;
            box-shadow: 0 2px 5px rgba(0,0,0,0.05);
        }

        .recuadro-reporte h4 {
            margin-bottom: 15px;
        }

        .input-modal {
            width: 100%;
            padding: 8px;
            margin-bottom: 10px;
            border-radius: 4px;
            border: 1px solid #ccc;
        }

        .btn-add {
            background-color: #007bff;
            color: white;
            border: none;
            padding: 10px 20px;
            border-radius: 4px;
            cursor: pointer;
        }

        .btn-add:hover {
            background-color: #0056b3;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="table-header">
        <h2>Generador de Reportes</h2>
    </div>

    <!-- Reporte Productos más Vendidos -->
    <div class="recuadro-reporte">
        <h4>Productos más Vendidos</h4>

        <label>Mes Inicio</label>
        <asp:DropDownList ID="ddlMesInicioProd" runat="server" CssClass="input-modal">
            <asp:ListItem Text="---" Value="0" />
            <asp:ListItem Text="Enero" Value="1" />
            <asp:ListItem Text="Febrero" Value="2" />
            <asp:ListItem Text="Marzo" Value="3" />
            <asp:ListItem Text="Abril" Value="4" />
            <asp:ListItem Text="Mayo" Value="5" />
            <asp:ListItem Text="Junio" Value="6" />
            <asp:ListItem Text="Julio" Value="7" />
            <asp:ListItem Text="Agosto" Value="8" />
            <asp:ListItem Text="Septiembre" Value="9" />
            <asp:ListItem Text="Octubre" Value="10" />
            <asp:ListItem Text="Noviembre" Value="11" />
            <asp:ListItem Text="Diciembre" Value="12" />
        </asp:DropDownList>

        <label>Año Inicio</label>
        <asp:TextBox ID="txtAnioInicioProd" runat="server" CssClass="input-modal" TextMode="Number"/>

        <label>Mes Fin</label>
        <asp:DropDownList ID="ddlMesFinProd" runat="server" CssClass="input-modal">
            <asp:ListItem Text="---" Value="0" />
            <asp:ListItem Text="Enero" Value="1" />
            <asp:ListItem Text="Febrero" Value="2" />
            <asp:ListItem Text="Marzo" Value="3" />
            <asp:ListItem Text="Abril" Value="4" />
            <asp:ListItem Text="Mayo" Value="5" />
            <asp:ListItem Text="Junio" Value="6" />
            <asp:ListItem Text="Julio" Value="7" />
            <asp:ListItem Text="Agosto" Value="8" />
            <asp:ListItem Text="Septiembre" Value="9" />
            <asp:ListItem Text="Octubre" Value="10" />
            <asp:ListItem Text="Noviembre" Value="11" />
            <asp:ListItem Text="Diciembre" Value="12" />
        </asp:DropDownList>

        <label>Año Fin</label>
        <asp:TextBox ID="txtAnioFinProd" runat="server" CssClass="input-modal" TextMode="Number"/>

        <asp:Button ID="btnGenerarProd" runat="server" Text="Generar Reporte" CssClass="btn-add" OnClick="btnGenerarReporte_Click"
            OnClientClick="this.form.target='_blank'; setTimeout(function(){this.form.target='';}.bind(this), 100);" />
    </div>

    <!-- Reporte de Stock -->
    <div class="recuadro-reporte">
        <h4>Reporte de Stock</h4>

        <label>Stock Mínimo</label>
        <asp:TextBox ID="StockMin" runat="server" CssClass="input-modal" TextMode="Number"/>

        <label>Stock Máximo</label>
        <asp:TextBox ID="StockMax" runat="server" CssClass="input-modal" TextMode="Number"/>

        <asp:DropDownList ID="ddlCategorias" runat="server" AutoPostBack="true" />

        <asp:Button ID="btnGenerarStock" runat="server" Text="Generar Reporte" CssClass="btn-add" OnClick="btnGenerarReporte_Click"
            OnClientClick="this.form.target='_blank'; setTimeout(function(){this.form.target='';}.bind(this), 100);" />
    </div>

    <!-- Reporte Clientes Recurrentes -->
    <div class="recuadro-reporte">
        <h4>Clientes Recurrentes</h4>

        <label>Mes Inicio</label>
        <asp:DropDownList ID="ddlMesInicioClientes" runat="server" CssClass="input-modal">
            <asp:ListItem Text="---" Value="0" />
            <asp:ListItem Text="Enero" Value="1" />
            <asp:ListItem Text="Febrero" Value="2" />
            <asp:ListItem Text="Marzo" Value="3" />
            <asp:ListItem Text="Abril" Value="4" />
            <asp:ListItem Text="Mayo" Value="5" />
            <asp:ListItem Text="Junio" Value="6" />
            <asp:ListItem Text="Julio" Value="7" />
            <asp:ListItem Text="Agosto" Value="8" />
            <asp:ListItem Text="Septiembre" Value="9" />
            <asp:ListItem Text="Octubre" Value="10" />
            <asp:ListItem Text="Noviembre" Value="11" />
            <asp:ListItem Text="Diciembre" Value="12" />
        </asp:DropDownList>

        <label>Año Inicio</label>
        <asp:TextBox ID="txtAnioInicioClientes" runat="server" CssClass="input-modal" TextMode="Number"/>

        <label>Mes Fin</label>
        <asp:DropDownList ID="ddlMesFinClientes" runat="server" CssClass="input-modal">
            <asp:ListItem Text="---" Value="0" />
            <asp:ListItem Text="Enero" Value="1" />
            <asp:ListItem Text="Febrero" Value="2" />
            <asp:ListItem Text="Marzo" Value="3" />
            <asp:ListItem Text="Abril" Value="4" />
            <asp:ListItem Text="Mayo" Value="5" />
            <asp:ListItem Text="Junio" Value="6" />
            <asp:ListItem Text="Julio" Value="7" />
            <asp:ListItem Text="Agosto" Value="8" />
            <asp:ListItem Text="Septiembre" Value="9" />
            <asp:ListItem Text="Octubre" Value="10" />
            <asp:ListItem Text="Noviembre" Value="11" />
            <asp:ListItem Text="Diciembre" Value="12" />
        </asp:DropDownList>

        <label>Año Fin</label>
        <asp:TextBox ID="txtAnioFinClientes" runat="server" CssClass="input-modal" TextMode="Number"/>

        <label>Mínimo de Compras</label>
        <asp:TextBox ID="txtMinCompras" runat="server" CssClass="input-modal" placeholder="Ej. 3"/>

        <asp:Button ID="btnGenerarClientes" runat="server" Text="Generar Reporte" CssClass="btn-add" OnClick="btnGenerarReporte_Click"
            OnClientClick="this.form.target='_blank'; setTimeout(function(){this.form.target='';}.bind(this), 100);" />
    </div>
</asp:Content>

