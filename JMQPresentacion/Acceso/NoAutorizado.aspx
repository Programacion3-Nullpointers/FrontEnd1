<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NoAutorizado.aspx.cs" Inherits="JMQPresentacion.Acceso.NoAutorizado" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>¡Ups! No puedes pasar</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background-color: #f1f1f1;
            font-family: 'Segoe UI', sans-serif;
        }
        .container {
            margin-top: 100px;
            text-align: center;
        }
        .emoji {
            font-size: 80px;
        }
        .btn-primary {
            padding: 10px 20px;
            font-size: 18px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="emoji">🚫</div>
            <h1 class="display-5 fw-bold">¡Ups! Parece que no te has registrado</h1>
            <p class="lead">No puedes pasar por aquí sin iniciar sesión.</p>
            <a href="../Login/Login.aspx" class="btn btn-primary">Ir a Iniciar Sesión</a>
        </div>
    </form>
</body>
</html>
