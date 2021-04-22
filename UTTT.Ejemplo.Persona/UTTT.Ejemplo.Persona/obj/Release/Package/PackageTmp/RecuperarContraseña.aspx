<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecuperarContraseña.aspx.cs" Inherits="UTTT.Ejemplo.Persona.RecuperarContraseña" %>

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
        
            <div class="container">
               
                
            <div class="col-lg-12 login-title">
                <h1>Recuperar Contraseña</h1>
            </div>
                <div class="col-lg-12 login-form">
            <div class="form-group">
                <asp:Label ID="Label1"  runat="server" class="form-control-label" >Usuario:</asp:Label>
                <asp:TextBox ID="txtUsuario" class="form-control" runat="server" placeholder="Ingresa tú usuario" MaxLength="50" ></asp:TextBox>
             
            </div>
            <div class="form-group">
             <asp:Label ID="Label2"  runat="server" class="form-control-label" >Contraseña:</asp:Label>  
            <asp:TextBox ID="txtCntraseña" class="form-control" runat="server" placeholder="Ingresa tú nueva contraseña" MaxLength="50" TextMode="Password"  ></asp:TextBox>
             </div>
            <div class="form-group">
              <asp:Label ID="Label3"  runat="server" class="form-control-label" > Verifica Contraseña:</asp:Label> 
            <asp:TextBox ID="txtCntraseña2"  class="form-control" runat="server" placeholder="Confirma tu Contraseña" MaxLength="50" TextMode="Password"  ></asp:TextBox>
                <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="No coincide la contraseña" ControlToCompare="txtCntraseña" ControlToValidate="txtCntraseña2"></asp:CompareValidator>
            
            </div>
                    <div class="col-lg-12 loginbttm">
            <div class="form-group">
                <asp:Label ID="lblMessa"  runat="server" ForeColor="Blue"></asp:Label>

            </div>
            <div class="form-group">
                <asp:Button ID="Button2" runat="server"  class="btn btn-primary btn-lg btn-block" Text ="Aceptar" OnClick="Button2_Click"  />
          
            </div>
                    </div>
                    </div>
        </div>
        
    </form>
</body>
</html>
