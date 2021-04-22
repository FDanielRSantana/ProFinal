using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UTTT.Ejemplo.Linq.Data.Entity;
using System.Data.Linq;
using System.Linq.Expressions;
using System.Collections;
using UTTT.Ejemplo.Persona.Control;
using UTTT.Ejemplo.Persona.Control.Ctrl;
using EASendMail;
using System.Data.SqlClient;
using System.Data;

namespace UTTT.Ejemplo.Persona
{
    public partial class UsuarioManagerEditar : System.Web.UI.Page
    {

        private SessionManager session = new SessionManager();
        private int idUsuario = 0;
        private UTTT.Ejemplo.Linq.Data.Entity.Usuario baseEntity;
        private DataContext dcGlobal = new DcGeneralDataContext();
        private int tipoAccion = 0;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["strNombreUsuario"] == null)
                Response.Redirect("login.aspx");
            try
            {
                this.Response.Buffer = true;
                this.session = (SessionManager)this.Session["SessionManager"];
                this.idUsuario = this.session.Parametros["idPersona"] != null ?
                    int.Parse(this.session.Parametros["idPersona"].ToString()) : 0;
                if (this.idUsuario == 0)
                {
                    this.baseEntity = new Linq.Data.Entity.Usuario();
                    this.tipoAccion = 1;
                }
                else
                {
                    this.baseEntity = dcGlobal.GetTable<Linq.Data.Entity.Usuario>().First(c => c.id == this.idUsuario);
                    this.tipoAccion = 2;
                }

                if (!this.IsPostBack)
                {
                    if (this.session.Parametros["baseEntity"] == null)
                    {
                        this.session.Parametros.Add("baseEntity", this.baseEntity);
                    }
                    
                    List<Linq.Data.Entity.Persona> lista = dcGlobal.GetTable<Linq.Data.Entity.Persona>().ToList();

                    this.ddlPersona.DataTextField = "strNombre";
                    this.ddlPersona.DataValueField = "id";

                    
                    List<CatEstadoUser> listaa = dcGlobal.GetTable<CatEstadoUser>().ToList();

                    this.ddlEstado.DataTextField = "strValor";
                    this.ddlEstado.DataValueField = "id";


                    if (this.idUsuario == 0)
                    {
                        Linq.Data.Entity.Persona catTemp = new Linq.Data.Entity.Persona();
                        catTemp.id = -1;
                        catTemp.strNombre = "Seleccionar";
                        lista.Insert(0, catTemp);
                        this.ddlPersona.DataSource = lista;
                        this.ddlPersona.DataBind();

                        CatEstadoUser catTempp = new CatEstadoUser();
                        catTempp.id = -1;
                        catTempp.strValor = "Seleccionar";
                        listaa.Insert(0, catTempp);
                        this.ddlEstado.DataSource = listaa;
                        this.ddlEstado.DataBind();
                        UnirDropDownL();

                        this.lblAccion.Text = "Agregar";
                        DateTime time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                        this.txtfecha.Text = DateTime.Today.ToString("yyyy-MM-dd");
                        //this.Calendar1.SelectedDate = time;
                        //this.Calendar1.EndDate = time;


                       //lblEstado.Visible = false;
                        L5.Visible=true;
                        ddlEstado.Enabled = false;
                        


                    }
                    else
                    {
                        this.lblAccion.Text = "Editar";
                        this.txtNomUsuario.Text = this.baseEntity.strNombreUsuario;
                        this.txtContraseña.Text = (this.baseEntity.strContraseña);
                        this.txtVerificaContraseña.Text = (this.baseEntity.strContraseña);
                        DateTime fechaNacimiento = (DateTime)this.baseEntity.dteFecha;
                        if (fechaNacimiento != null)
                        {
                            txtfecha.Text = fechaNacimiento.Date.ToString("yyyy/MM/dd");


                        }

                        ddlPersona.Enabled = false;
                        L5.Visible = false;

                        this.ddlPersona.DataSource = lista;
                        this.ddlPersona.DataBind();


                        

                        this.ddlEstado.DataSource = listaa;
                        this.ddlEstado.DataBind();

                        this.setItem(ref this.ddlEstado, baseEntity.CatEstadoUser.strValor);
                        
                        this.setItem(ref this.ddlPersona, baseEntity.Persona.strNombre);
                    }
                    this.ddlPersona.SelectedIndexChanged += new EventHandler(ddlPersona_SelectedIndexChanged);
                    this.ddlPersona.AutoPostBack = false;
                }
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al cargar la página");
                this.Response.Redirect("~/UsuarioPrincipal.aspx", false);
                this.showMessageException(_e.Message);
            }

        }

        protected void ddlPersona_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
        {



            try
            {
                int idCatUsuario = int.Parse(this.ddlEstado.Text);
                Expression<Func<CatEstadoUser, bool>> predicateCatU = c => c.id == idCatUsuario;
                predicateCatU.Compile();
                List<CatEstadoUser> lista = dcGlobal.GetTable<CatEstadoUser>().Where(predicateCatU).ToList();

                CatEstadoUser catTemp = new CatEstadoUser();
                this.ddlEstado.DataTextField = "strValor";
                this.ddlEstado.DataValueField = "id";
                this.ddlEstado.DataSource = lista;
                this.ddlEstado.DataBind();
            }
            catch (Exception)
            {
                this.showMessage("Ha ocurrido un error inesperado");
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime fechaNacimiento = Convert.ToDateTime(this.txtfecha.Text);
                DateTime fechaHoy = DateTime.Now;
                int edad = fechaHoy.Year - fechaNacimiento.Year;
                if (fechaHoy < fechaNacimiento.AddYears(edad)) edad--;
                if (edad > 1)
                {
                    this.lblMensaje.Visible = true;
                    lblMensaje.Text = "Ingrese la Fecha Actual";
                }
                else
                {
                    if (!Page.IsValid)
                    {
                        return;
                    }
                    DataContext dcGuardar = new DcGeneralDataContext();
                    UTTT.Ejemplo.Linq.Data.Entity.Usuario persona = new Linq.Data.Entity.Usuario();
                    UTTT.Ejemplo.Linq.Data.Entity.Persona personaa = new Linq.Data.Entity.Persona();
                    UTTT.Ejemplo.Linq.Data.Entity.CatEstadoUser personaaa = new Linq.Data.Entity.CatEstadoUser();
                    if (this.idUsuario == 0)
                    {

                        persona.idCatEstadoUser = 1;
                        persona.strContraseña = txtContraseña.Text.Trim();
                        DateTime fecha = Convert.ToDateTime(txtfecha.Text);
                        persona.dteFecha = fechaNacimiento;



                        

                        //DateTime tiempo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                        //this.txtfecha.Text = Convert.ToString(tiempo.ToShortDateString());
                        var comprobar = dcGlobal.GetTable<Usuario>().Where(a => a.strNombreUsuario == txtNomUsuario.Text).FirstOrDefault();
                        var comprobare = dcGlobal.GetTable<Usuario>().Where(a => a.idComPersona == int.Parse(ddlPersona.Text)).FirstOrDefault();
                        if (comprobar != null)
                        {


                            this.lblMensaje.Visible = true;
                            this.lblMensaje.Text = "Nombre de Usuario Repetido";

                        }
                        else if (comprobare != null)
                        {


                            this.lblMensaje.Visible = true;
                            this.lblMensaje.Text = "La Persona ya está asignada a otro Usuario ";

                        }
                        else
                        {
                            persona.idComPersona = int.Parse(this.ddlPersona.Text);
                            persona.strNombreUsuario = this.txtNomUsuario.Text.Trim();
                            //String mensaje = String.Empty;
                            //if (!this.txtValidacion(persona, ref mensaje))
                            //{
                            //    this.lblMensaje.Text = mensaje;
                            //    this.lblMensaje.Visible = true;
                            //    return;
                            //}
                            //if (!this.validacionSQL(ref mensaje))
                            //{
                            //    this.lblMensaje.Text = mensaje;
                            //    this.lblMensaje.Visible = true;
                            //    return;
                            //}
                            //if (!this.validacionHTML(ref mensaje))
                            //{
                            //    this.lblMensaje.Text = mensaje;
                            //    this.lblMensaje.Visible = true;
                            //    return;
                            //}


                            dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Usuario>().InsertOnSubmit(persona);
                            dcGuardar.SubmitChanges();
                            this.showMessage("El registro se agrego correctamente.");
                            this.Response.Redirect("~/UsuarioPrincipal.aspx", false);
                        }
                    }
                    if (this.idUsuario > 0)
                    {
                        persona = dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Usuario>().First(c => c.id == idUsuario);

                        persona.strNombreUsuario = this.txtNomUsuario.Text.Trim();


                        //persona.constrasenia = Crypt.Desencriptar(txtContra.Text);
                        //persona.constrasenia = Crypt.Desencriptar(txtreContra.Text);
                        persona.strContraseña = this.txtContraseña.Text.Trim();
                        persona.strContraseña = this.txtContraseña.Text;

                        persona.idCatEstadoUser = int.Parse(this.ddlEstado.Text.Trim());
                        persona.idComPersona = int.Parse(this.ddlPersona.Text);

                        DateTime fecha = Convert.ToDateTime(txtfecha.Text);
                        persona.dteFecha = fechaNacimiento;
                        //String mensaje = String.Empty;
                        //if (!this.txtValidacion(persona, ref mensaje))
                        //{
                        //    this.lblMensaje.Text = mensaje;
                        //    this.lblMensaje.Visible = true;
                        //    return;
                        //}
                        //if (!this.validacionSQL(ref mensaje))
                        //{
                        //    this.lblMensaje.Text = mensaje;
                        //    this.lblMensaje.Visible = true;
                        //    return;
                        //}
                        //if (!this.validacionHTML(ref mensaje))
                        //{
                        //    this.lblMensaje.Text = mensaje;
                        //    this.lblMensaje.Visible = true;
                        //    return;
                        //}

                        dcGuardar.SubmitChanges();
                        this.showMessage("El registro se edito correctamente.");
                        this.Response.Redirect("~/UsuarioPrincipal.aspx", false);
                    }
                }
            }

            catch (Exception _e)
            {
                this.showMessageException(_e.Message);
                var mensaje = "Error message: " + _e.Message;
                if (_e.InnerException != null)
                {
                    mensaje = mensaje + " Inner exception: " + _e.InnerException.Message;
                }
                // Mi Correo
                mensaje = mensaje + " Stack trace: " + _e.StackTrace;
                this.Response.Redirect("~/ErrorPage.aspx", false);
               
            }

        }

        //public bool txtValidacion(UTTT.Ejemplo.Linq.Data.Entity.Usuario _usuario, ref String _mensaje)
        //{
        //    if (_usuario.id != Convert.ToInt32(ddlPersona.Text))

        //    {
        //        _mensaje = "Persona repetida ";
        //        return false;
        //    }

        //    //string cntra = _usuario.strContraseña.Trim();
        //    if (_usuario.strContraseña.Length < 3)
        //    {
        //        _mensaje = "Debe de tener mas de 3 caracteres en el campo contraseña";
        //        return false;
        //    }

        //    if (_usuario.strContraseña.Equals(String.Empty))
        //    {
        //        _mensaje = "La contraseña esta vacia";
        //        return false;
        //    }
        //    if (_usuario.strContraseña.Length > 200)
        //    {
        //        _mensaje = "numero de caracteres no permitido";
        //        return false;
        //    }

        //    return true;
        //}

        
        public bool UsuarioExistente(int id)
        {
            using (SqlConnection conn = new SqlConnection("Data Source=Persona1234.mssql.somee.com;Initial Catalog=Persona1234;User ID=DanieeelSanta_SQLLogin_1;Password=rpq4bhijqi"))
            {
                string query = "SELECT COUNT(*) FROM Usuario WHERE idPersona=@IdPersona";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IdPersona", id);
                conn.Open();

                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count == 0)
                    return false;
                else
                    return true;
            }
        }

        private void UnirDropDownL()
        {
            SqlConnection con = new SqlConnection("Data Source=Persona1234.mssql.somee.com;Initial Catalog=Persona1234;User ID=DanieeelSanta_SQLLogin_1;Password=rpq4bhijqi");
            SqlCommand cmd = new SqlCommand("select strNombre,id from Persona", con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();


            sda.Fill(ds);



            ddlPersona.DataSource = ds;

            ddlPersona.DataTextField = "strNombre"; 
            ddlPersona.DataValueField = "id";

           

            ddlPersona.DataBind();
            ddlPersona.Items.Insert(0, new ListItem("Seleccionar", String.Empty));

           

        }

        public void setItem(ref DropDownList _control, String _value)
        {
            foreach (ListItem item in _control.Items)
            {
                if (item.Value == _value)
                {
                    item.Selected = true;
                    break;
                }
            }
            _control.Items.FindByText(_value).Selected = true;
        }

        public void setItemEdith(ref DropDownList _control, String _value)
        {
            foreach (ListItem item in _control.Items)
            {
                if (item.Value != _value)
                {
                    item.Enabled = false;

                    break;
                }
            }
            _control.Items.FindByText(_value).Selected = true;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                this.Response.Redirect("~/UsuarioPrincipal.aspx", false);
            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un error inesperado");
                this.showMessageException(_e.Message);
            }
        }

        protected void btnsession(object sender, EventArgs e)
        {

            Session.Abandon();
            Response.Redirect("login.aspx");
        }

        
    }
}