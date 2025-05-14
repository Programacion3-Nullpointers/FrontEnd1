<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="Cotiza.aspx.cs" Inherits="JMQPresentacion.Pedidos.Cotiza" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Contenido" runat="server">
    <h4> Ingresa las descripciones de todos los productos que quieras cotizar, y su cantidad.</h4>
    <br />
    <form id="myForm" action="/Form/Submit" method="post">
        <table id="dataTable" border="1">
        <tr>
            <th>Descripción</th>
            <th>Cantidad</th>
            <th>Acción</th>
        </tr>
        <tr>
            <td><input type="text" name="desc"></td>
            <td><input type="number" name="cantidad"></td>
            <td><button type="button" onclick="addRow()">➕</button></td>
        </tr>
        </table>
        <br />
        <asp:Button ID="btnSubmit" runat="server" Text="Enviar" OnClick="SubmitForm" />
    </form>

<script>
    function addRow() {
        let table = document.getElementById("dataTable");
        let row = table.insertRow(-1);
        row.innerHTML = `<td><input type="text" name="desc"></td>
                         <td><input type="number" name="cantidad"></td>
                         <td><button type="button" onclick="addRow()">➕</button></td>
                         <td><button type="button" onclick="removeRow(this)">❌</button></td>`;
    }

    function removeRow(button) {
        button.parentElement.parentElement.remove();
    }
</script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Scripts" runat="server">
</asp:Content>
