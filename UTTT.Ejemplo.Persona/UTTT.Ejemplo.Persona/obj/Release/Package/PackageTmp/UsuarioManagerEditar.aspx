<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UsuarioManagerEditar.aspx.cs" Inherits="UTTT.Ejemplo.Persona.UsuarioManagerEditar" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="Content/bootstrap.min.css" rel="stylesheet" />

    
    <script src="Scripts/bootstrap.min.js"></script>
    <script src="Scripts/jquery-1.4.1.min.js"></script>

    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title></title>
</head>
<body>
    <nav class="navbar navbar-expand-sm navbar-light bg-primary">
        <a class="navbar-brand text-white" href="Inicio.aspx">Inicio</a>
    </nav>

    
    <form id="form1" runat="server">
        

          
            
                <div class="container well" >
                
                <div >
                    <h2>
                        <asp:Label ID="lblAccion" runat="server" Text="Editar" Font-Bold="True" BorderColor="Red"></asp:Label>
                    </h2>
                </div>
                    <br />
            <div>
               <asp:Label ID="lblMensaje" class="alert alert-warning"  runat="server" Text=""></asp:Label>
            </div>
                    <br />
            <div>

            </div>

            <div class="form-group">
                <asp:Label ID="Label1" runat="server" Text="Persona:"></asp:Label>
               
                <asp:DropDownList ID="ddlPersona" runat="server" class="form-control" OnSelectedIndexChanged="ddlPersona_SelectedIndexChanged"></asp:DropDownList>

            </div>
            <div class="form-group">
                <asp:Label ID="Label2" runat="server" Text="Nombre de usuario:"></asp:Label>
                <asp:TextBox ID="txtNomUsuario" CssClass="form-control" required="" runat="server"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="Label3" runat="server" Text="Contraseña:"></asp:Label>
                <asp:TextBox ID="txtContraseña" Type="password" CssClass="form-control" runat="server" required="" ></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label ID="Label4" runat="server" Text="Verifica contraseña:"></asp:Label>
                <asp:TextBox ID="txtVerificaContraseña" Type="password" CssClass="form-control" runat="server"  ></asp:TextBox>
                <asp:CompareValidator runat="server" ErrorMessage="La Contraseña no coincide" ControlToCompare="txtContraseña" ControlToValidate="txtVerificaContraseña" Type="String"></asp:CompareValidator>
                  </div>
            <div class="form-group" >
                <asp:Label ID="lblfecha" runat="server"  Text="Fecha de ingreso:"></asp:Label>
                <asp:TextBox ID="txtfecha" required="" CssClass="form-control" runat="server"></asp:TextBox>
           


            </div>

            <div class="form-group">
                <asp:Label ID="L5" runat="server" Text="Estado:"></asp:Label>
                
                <asp:DropDownList ID="ddlEstado"  class="form-control"  runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlEstado_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div>
               
                <asp:Button ID="Button2" runat="server" Text="Cancelar" class="btn btn-outline-danger" OnClick="Button2_Click" CausesValidation="false" formnovalidate=""  />
             <asp:Button ID="btn_Aceptar" runat="server" Text="Aceptar" class="btn btn-outline-primary" OnClick="btnAceptar_Click" />
            
            </div>
            

        </div>
          
        
    </form>
     
</body>
</html>
