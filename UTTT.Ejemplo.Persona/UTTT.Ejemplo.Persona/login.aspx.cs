using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UTTT.Ejemplo.Linq.Data.Entity;
using UTTT.Ejemplo.Persona.Control.Ctrl;
using UTTT.Ejemplo.Persona.Modelo;
using Usuario = UTTT.Ejemplo.Linq.Data.Entity.Usuario;

namespace UTTT.Ejemplo.Persona
{
    public partial class login : System.Web.UI.Page
    {
        private int idUsuario = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            //lblMensaje.Visible = false;
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {



            try
            {
                DataContext dcGuardar = new DcGeneralDataContext();
                //Usuario usuario = new Usuario();
                Linq.Data.Entity.Persona persona = new Linq.Data.Entity.Persona();
                string Usuario;
                Usuario = this.txtUsuario.Text.Trim();
                string contrasenaUsuario;
                contrasenaUsuario = this.txtContraseña.Text.Trim();

                var usuarioLogin = dcGuardar.GetTable<Usuario>().FirstOrDefault(c => c.strNombreUsuario == Usuario);


                if (usuarioLogin != null)
                {
                    if (Usuario == usuarioLogin.strNombreUsuario && contrasenaUsuario == usuarioLogin.strContraseña)
                    {
                        if (usuarioLogin.idCatEstadoUser == 1)
                        {
                            this.showMessage("Usuario Valido");
                            Session["strNombreUsuario"] = txtUsuario.Text.Trim();
                            this.Response.Redirect("~/Inicio.aspx", false);
                        }
                        else
                        {
                            this.showMessage("Tu usuario no esta activo");
                        }

                    }
                    else
                    {
                        this.showMessage("Datos no validos");
                    }
                }
                else
                {
                    this.showMessage("Ingresa los datos correctos");
                }

            }
            catch (Exception)
            {
                this.showMessage("Ingresa los datos correctos");
                this.Response.Redirect("~/login.aspx", false);
            }



            //using (SqlConnection sqlConnect = new SqlConnection("Data Source=Persona1234.mssql.somee.com;Initial Catalog=Persona1234;Persist Security Info=True;User ID=DanieeelSanta_SQLLogin_1;Password=rpq4bhijqi"))
            //{
            //    string query = "SELECT COUNT(1) FROM Usuario WHERE strNombreUsuario=@strNombreUsuario AND strContraseña=@strContraseña";
            //    SqlCommand sqlCom = new SqlCommand(query, sqlConnect);
            //    sqlCom.Parameters.AddWithValue("@strNombreUsuario", txtUsuario.Text.Trim());
            //    string encriptPass = Encrypt.GetSHA256(txtContraseña.Text.Trim());
            //    sqlCom.Parameters.AddWithValue("@strContraseña", encriptPass);

            //    sqlConnect.Open();
            //    int cout = Convert.ToInt32(sqlCom.ExecuteScalar());
            //    if (cout == 1)
            //    {
            //        Session["strNombreUsuario"] = txtUsuario.Text.Trim();
            //        Response.Redirect("MenuUserPerson.aspx");
            //    }
            //    else
            //    {
            //        lblMensaje.Visible = true;
            //    }

            //}


            //string usuario = txtUsuario.Text;
            //string password = txtContraseña.Text;

            //int idUsuario = ClsLogin.Sessiones(usuario, password);
            //if (idUsuario == 0)
            //{
            //    this.lblMensaje.Text = "Verifique sus Datos ";
            //}
            //else
            //{
            //    Session["idUsuario"] = idUsuario;
            //    Response.Redirect("~/Inicio.aspx");
            //}
        }
    }
}