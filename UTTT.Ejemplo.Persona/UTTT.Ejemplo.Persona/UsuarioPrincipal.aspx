<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UsuarioPrincipal.aspx.cs" Inherits="UTTT.Ejemplo.Persona.UsuarioPrincipal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />

    <title></title>
    
    <link href="Content/bootstrap.min.css" rel="stylesheet" />

    
    <script src="Scripts/bootstrap.min.js"></script>
    <script src="Scripts/jquery-1.4.1.min.js"></script>


    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="viewport" content="width=device-width, initial-scale=1">


</head>
<body>

    <nav class="navbar navbar-expand-sm navbar-light bg-primary">
        <a class="navbar-brand text-white" href="Inicio.aspx">Inicio</a>
    </nav>
    
        <form id="form3" runat="server">
<div class="container well">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

            
                <div>
                </div>
                <div class="form-group">
                    <asp:Label ID="lblNombrePrincipal" runat="server" Text="Nombre:"></asp:Label>




                    <asp:UpdatePanel ID="UpdatePaneBuscar" runat="server">
                        <ContentTemplate>
                            <input type="submit" name="btnTrick" value="" id="btnTrick" style="display: none;" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <asp:TextBox ID="txtNombre" class="form-control" runat="server" ViewStateMode="Disabled" ></asp:TextBox>


                </div>
                <br />

                <div class="form-group">

                    <asp:Label ID="lblSexoPrincipal" runat="server" Text="Estado:"></asp:Label>

                    <asp:DropDownList ID="ddlSexo" class="form-control" runat="server" AutoPostBack="True" ></asp:DropDownList>




                </div>

                <div class="btn-group">


                    <asp:Button ID="BtnBuscar" runat="server" Text="Buscar" class="btn btn-outline-primary" OnClick="BtnBuscar_Click"  />
                    <asp:Button ID="BtnAgregar" runat="server" Text="Agregar" class="btn btn-primary text-white" OnClick="BtnAgregar_Click"  />
                </div>



                <div>
                </div>

                <p>
                    <h3>Detalle</h3>
                </p>


                <div class="table-responsive">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="dgvUsuario" class="table table-striped" runat="server" Width="601px"
                                AllowPaging="True" AutoGenerateColumns="False"
                                DataSourceID="DataSourceUsuario" GridLines="Horizontal"
                                ViewStateMode="Disabled" OnRowCommand="dgvUsuarios">
                                <Columns>
                                    <asp:BoundField DataField="strNombreUsuario" HeaderText="Nombre" ReadOnly="True" SortExpression="strNombreUsuario" />
                                    <asp:BoundField DataField="idCatEstadoUser" HeaderText="Estado" ReadOnly="True" SortExpression="idCatEstadoUser" />
                                    <asp:TemplateField HeaderText="Editar">
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="imgEditar" CommandName="Editar" CommandArgument='<%#Bind("id") %>' ImageUrl="~/Images/editrecord_16x16.png" class="btn btn-outline-primary" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />


                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Eliminar">
                                        <ItemTemplate>
                                            <asp:ImageButton runat="server" ID="imgEliminar" CommandName="Eliminar" CommandArgument='<%#Bind("id") %>' ImageUrl="~/Images/delrecord_16x16.png" OnClientClick="javascript:return confirm('¿Está seguro de querer eliminar el registro seleccionado?', 'Mensaje de sistema')" class="btn btn-outline-danger" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnBuscar" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>

                </div>
            </div>
    <asp:LinqDataSource ID="DataSourceUsuario" runat="server"
        ContextTypeName="UTTT.Ejemplo.Linq.Data.Entity.DcGeneralDataContext"
        OnSelecting="DataSourceUsuario_Selecting"
        Select="new (strNombreUsuario, idCatEstadoUser,id)"
        TableName="Usuario" EntityTypeName="">
    </asp:LinqDataSource>


    </form>
    <script type="text/javascript">
        var nombre = document.getElementById("txtNombre").value;
        document.querySelector('#txtNombre').addEventListener('keyup', function () {
            const btnTrick = document.querySelector('#btnTrick');
            btnTrick.click();
        });
    </script>
</body>
</html>
