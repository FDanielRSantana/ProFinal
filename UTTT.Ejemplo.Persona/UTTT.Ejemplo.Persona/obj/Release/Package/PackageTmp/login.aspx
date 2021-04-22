<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="UTTT.Ejemplo.Persona.login" %>

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
        <div class="row">
            <div class="col-lg-3 col-md-2"></div>
            <div class="col-lg-6 col-md-8 login-box">
                <div class="col-lg-12 login-key">
                    <i class="fa fa-key" aria-hidden="true"></i>
                </div>
                <div class="col-lg-12 login-title">
                    LOGIN
                </div>

                <div class="col-lg-12 login-form">
                   
                        <form>
                            <div class="form-group">
                                <asp:Label ID="Label2"  class="form-control-label" runat="server" Text="Usuario:"></asp:Label>
                                
                                
                                 <asp:TextBox ID="txtUsuario" class="form-control" runat="server" required=""></asp:TextBox>
                            </div>
                            <div class="form-group">

                                <asp:Label ID="lbl" class="form-control-label"  runat="server" Text="Contraseña:"></asp:Label>
            <asp:TextBox ID="txtContraseña" class="form-control" runat="server" required="" TextMode="Password"></asp:TextBox>
       
                                
                            </div>

                            <div class="col-lg-12 loginbttm">
                                <div class="col-lg-6 login-btm login-text">
                                    <!-- Error Message -->
                                </div>
                                <div class="">
                                <asp:Button ID="btnAceptar" runat="server" class="btn btn-primary btn-lg btn-block" Text="Aceptar" OnClick="btnAceptar_Click" />

                                    </div>
                                <div>
                                    <asp:Label ID="Label1" runat="server" Text="¿Olvidaste tu contraseña? "></asp:Label>
            <a href="#" onclick="window.open('CambiarContra.aspx','FP','width=650,height=350,top=300,left=300,fullscreeen=no,resizable=0');"> Recuperar </a>
            <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
                                    
                                </div>
                            </div>
                        </form>
                    </div>
                
                
        </div>


          
                
                
               
                   
                        
                        
                        

       
            

  

      
        
            
            

            
        </div>

   
  



        
        <%--<div>
            <asp:Label ID="Label1" runat="server" Text="Pagina de Registro"></asp:Label>
        </div>--%>
        
        
        
       
            


       
    </form>
    
</body>
</html>
