<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogOut.aspx.cs" Inherits="Front_Admin.LogOut.LogOut" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sesión cerrada</title>
    <style>
        body {
            margin: 0;
            padding: 0;
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background: linear-gradient(135deg, #1abc9c, #16a085);
            display: flex;
            align-items: center;
            justify-content: center;
            height: 100vh;
            color: #fff;
        }

        .logout-container {
            text-align: center;
            background-color: rgba(255, 255, 255, 0.1);
            padding: 40px;
            border-radius: 20px;
            box-shadow: 0 8px 20px rgba(0,0,0,0.2);
            backdrop-filter: blur(8px);
            max-width: 400px;
        }

        .logout-container h1 {
            font-size: 26px;
            margin-bottom: 15px;
        }

        .logout-container p {
            font-size: 18px;
            margin-bottom: 30px;
        }

        .logout-container a {
            text-decoration: none;
            padding: 10px 20px;
            background-color: #ffffff;
            color: #16a085;
            border-radius: 8px;
            font-weight: bold;
            transition: background-color 0.3s ease;
        }

        .logout-container a:hover {
            background-color: #f1f1f1;
        }

        .emoji {
            font-size: 40px;
            margin-bottom: 15px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="logout-container">
            <div class="emoji">👋</div>
            <h1>Saliste de tu cuenta exitosamente</h1>
            <p>Te esperamos pronto. ¡Gracias!</p>
        </div>
    </form>
</body>
</html>
