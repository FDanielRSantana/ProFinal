<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CambiarContra.aspx.cs" Inherits="UTTT.Ejemplo.Persona.CambiarContra" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css">
<scriptsrc="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>


</head>
<body>
    <form id="form1" runat="server">
        <div>
        </div>

        <div class="col-lg-6 col-md-8 login-box">

            <asp:TextBox ID="txtcorreo" class="form-control " runat="server" placeholder="Ingresa tu correo" MaxLength="50"  ></asp:TextBox>
             </div>

            <div align="center">
                <asp:Label ID="lblMessage" runat="server" ForeColor="Blue"></asp:Label>

            </div>
            <div align="center">
                <asp:Button ID="BtnEnviar" class="btn btn-outline-primary" runat="server" Text="Enviar"   />
          
            </div>
        

        <div>


        </div>
    </form>
</body>
</html>
