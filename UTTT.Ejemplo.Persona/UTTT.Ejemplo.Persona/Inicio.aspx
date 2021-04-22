<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="UTTT.Ejemplo.Persona.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
  <title>Hidden Menu</title>
  <link rel="stylesheet" media="all" href="css/menustyle.css" />
</head>
<body>




    <nav class="btn-pluss-wrapper">
 <h2 class="tooltip">Menu</h2>
 <div href="#" class="btn-pluss">
  <ul>
    <li><a href="/PersonaPrincipal.aspx">Personas</a></li>
    <li><a href="/UsuarioPrincipal.aspx">Usuarios</a></li>
    <li><a href="/login.aspx">Cerrar sesión</a></li>
    
  </ul>
 </div>
</nav>
















    <%--<nav class="navbar navbar-expand-sm bg-dark navbar-dark">
        <a class="navbar-brand" href="Inicio.aspx">Inicio</a>
    </nav>
    <form id="form1" runat="server">
        <div class="form-group">
            <div class="text-center">

                <div align="right">



                   
            <asp:Button ID="btnsessionX" runat="server" Text="Cerrar Session" OnClick="btnsessionX_Click"  />
           </div>  
                
                <div>
                </div>
                <div>
                    
                        <asp:Label ID="lblSesion" runat="server" Text="."></asp:Label>
                    
                    <div>
                        <asp:ImageButton ID="imbtnPersona" runat="server" Height="190px" ImageUrl="~/Images/persona.png" Width="170px" OnClick="imbtnPersona_Click" />
                    </div>
                    <div>
                        <asp:Label ID="Label3" runat="server" Text="Persona"></asp:Label>
                    </div>
                </div>
                <div>
                    <asp:ImageButton ID="imbtnUsuario" runat="server" Height="190px" ImageUrl="~/Images/Usuario.png" Width="170px" OnClick="imbtnUsuario_Click" />
                </div>
                <div>
                    <asp:Label ID="Label4" runat="server" Text="Usuario"></asp:Label>
                </div>
            </div>
        </div>


    </form>--%>
</body>

</html>
