#region Using

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



#endregion

namespace UTTT.Ejemplo.Persona
{
    public partial class PersonaManager : System.Web.UI.Page
    {
        
        #region Variables

        private SessionManager session = new SessionManager();
        private int idPersona = 0;
        private UTTT.Ejemplo.Linq.Data.Entity.Persona baseEntity;
        private DataContext dcGlobal = new DcGeneralDataContext();
        private int tipoAccion = 0;

        #endregion

        #region Eventos

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["strNombreUsuario"] == null)
                Response.Redirect("login.aspx");
            try
            {
                this.Response.Buffer = true;
                this.session = (SessionManager)this.Session["SessionManager"];
                this.idPersona = this.session.Parametros["idPersona"] != null ?
                    int.Parse(this.session.Parametros["idPersona"].ToString()) : 0;
                if (this.idPersona == 0)
                {
                    this.baseEntity = new Linq.Data.Entity.Persona();
                    this.tipoAccion = 1;
                }
                else
                {
                    this.baseEntity = dcGlobal.GetTable<Linq.Data.Entity.Persona>().First(c => c.id == this.idPersona);
                    this.tipoAccion = 2;
                }

                if (!this.IsPostBack)
                {
                    if (this.session.Parametros["baseEntity"] == null)
                    {
                        this.session.Parametros.Add("baseEntity", this.baseEntity);
                    }
                    List<CatSexo> lista = dcGlobal.GetTable<CatSexo>().ToList();
                    CatSexo catTemp = new CatSexo();
                    catTemp.id = -1;
                    catTemp.strValor = "Sexo";
                    lista.Insert(0, catTemp);
                    this.ddlSexo.DataTextField = "strValor";
                    this.ddlSexo.DataValueField = "id";
                    this.ddlSexo.DataSource = lista;
                    this.ddlSexo.DataBind();
                    
                    this.ddlSexo.SelectedIndexChanged += new EventHandler(ddlSexo_SelectedIndexChanged);
                    this.ddlSexo.AutoPostBack = true;

                    List<CatEstadoCivil> listaEstadoCivil = dcGlobal.GetTable<CatEstadoCivil>().ToList();
                    CatEstadoCivil catEstadoCivilTemp = new CatEstadoCivil();
                    catEstadoCivilTemp.id = -1;
                    catEstadoCivilTemp.strValor = "Estado";
                    listaEstadoCivil.Insert(0, catEstadoCivilTemp);
                    this.ddlEstadoCivil.DataTextField = "strValor";
                    this.ddlEstadoCivil.DataValueField = "id";
                    this.ddlEstadoCivil.DataSource = listaEstadoCivil;
                    this.ddlEstadoCivil.DataBind();

                    this.ddlEstadoCivil.SelectedIndexChanged += new EventHandler(ddlEstadoCivil_SelectedIndexChanged);
                    this.ddlEstadoCivil.AutoPostBack = true;

                    if (this.idPersona == 0)
                    {
                        this.lblAccion.Text = "Agregar";
                        DateTime tiempo = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                        this.txtFechaNac2.Text = Convert.ToString(tiempo.ToShortDateString());
                        this.CalendarExtender1.SelectedDate = tiempo;
                        //txtFechaNac2.Text = tiempo.Date.ToShortDateString();
                    }
                    else
                    {
                        
                        this.lblAccion.Text = "Editar";
                        
                        this.txtNombre.Text = this.baseEntity.strNombre;
                        this.txtAPaterno.Text = this.baseEntity.strAPaterno;
                        this.txtAMaterno.Text = this.baseEntity.strAMaterno;
                        this.txtClaveUnica.Text = this.baseEntity.strClaveUnica;
                        DateTime fechaNacimiento = (DateTime)this.baseEntity.dteFechaNacimiento;
                        if (fechaNacimiento != null)
                        {
                            txtFechaNac2.Text = fechaNacimiento.Date.ToString("yyyy/MM/dd");

                            
                        }
                            this.txtCorreo.Text = this.baseEntity.strCorreo;
                        this.txtCPostal.Text = this.baseEntity.strCPostal;
                        this.txtRFC.Text = this.baseEntity.strRFC;
                        this.setItemEditar(ref this.ddlSexo, baseEntity.CatSexo.strValor);
                        this.setItemEditar(ref this.ddlEstadoCivil, baseEntity.CatEstadoCivil.strValor);
                    }
                }
               

            }
            catch (Exception _e)
            {
                this.showMessage("Ha ocurrido un problema al cargar la página" + _e.Message);
                this.Response.Redirect("~/PersonaPrincipal.aspx", false);
            }

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime fechaNacimiento1 = Convert.ToDateTime(txtFechaNac2.Text);
                DateTime fechaHoy = DateTime.Today;
                
                if (CalcularEdad(fechaNacimiento1) < 18)
                {
                    
                    this.lblCalendario.Visible = true;
                    this.lblCalendario.Text = "Eres menor de edad";

                }
                else
                {

                    if (!Page.IsValid)
                    {
                        return;
                    }
                    DataContext dcGuardar = new DcGeneralDataContext();
                    UTTT.Ejemplo.Linq.Data.Entity.Persona persona = new Linq.Data.Entity.Persona();
                    //si la accion es agregar
                    if (this.idPersona == 0)
                    {
                        persona.strClaveUnica = this.txtClaveUnica.Text.Trim();
                        persona.strNombre = this.txtNombre.Text.Trim();
                        persona.strAMaterno = this.txtAMaterno.Text.Trim();
                        persona.strAPaterno = this.txtAPaterno.Text.Trim();
                        persona.idCatSexo = int.Parse(this.ddlSexo.Text);
                        persona.strCorreo = this.txtCorreo.Text.Trim();
                        persona.strCPostal = this.txtCPostal.Text.Trim();
                        persona.strRFC = this.txtRFC.Text.Trim();
                        persona.idCadEstadoCivil = int.Parse(this.ddlEstadoCivil.Text);
                        DateTime fechaNacimiento = Convert.ToDateTime(txtFechaNac2.Text);
                        persona.dteFechaNacimiento = fechaNacimiento;


                        String mensaje = String.Empty;
                        
                        if (!this.validacion(persona, ref mensaje))
                        {

                            this.lblMensaje.Text = mensaje;
                            this.lblMensaje.Visible = true;
                            return;
                        }

                        if (!this.validaSql(ref mensaje))
                        {

                            this.lblMensaje.Text = mensaje;
                            this.lblMensaje.Visible = true;
                            return;
                        }
                        if (!this.validaHTML(ref mensaje))
                        {
                            this.lblMensaje.Text = mensaje;
                            this.lblMensaje.Visible = true;
                            return;
                        }
                        dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Persona>().InsertOnSubmit(persona);
                        dcGuardar.SubmitChanges();
                        this.showMessage("El registro se agrego correctamente.");
                        this.Response.Redirect("~/PersonaPrincipal.aspx", false);
                    }
                    if (this.idPersona > 0)
                    {
                        if (CalcularEdad(fechaNacimiento1) < 18)
                        {
                            this.lblCalendario.Visible = true;
                            this.lblCalendario.Text = "Eres menor de edad";
                        }
                        else
                        {
                            persona = dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Persona>().First(c => c.id == idPersona);
                            persona.strClaveUnica = this.txtClaveUnica.Text.Trim();
                            persona.strNombre = this.txtNombre.Text.Trim();
                            persona.strAMaterno = this.txtAMaterno.Text.Trim();
                            persona.strAPaterno = this.txtAPaterno.Text.Trim();
                            persona.idCatSexo = int.Parse(this.ddlSexo.Text);
                            persona.strCorreo = this.txtCorreo.Text.Trim();
                            persona.strCPostal = this.txtCPostal.Text.Trim();
                            persona.strRFC = this.txtRFC.Text.Trim();
                            persona.idCadEstadoCivil = int.Parse(this.ddlEstadoCivil.Text);
                            DateTime fechaNacimiento = Convert.ToDateTime(txtFechaNac2.Text);
                            persona.dteFechaNacimiento = fechaNacimiento;



                            String mensaje = String.Empty;
                            
                            if (!this.validacion(persona, ref mensaje))
                            {

                                this.lblMensaje.Text = mensaje;
                                this.lblMensaje.Visible = true;
                                return;
                            }

                            if (!this.validaSql(ref mensaje))
                            {

                                this.lblMensaje.Text = mensaje;
                                this.lblMensaje.Visible = true;
                                return;
                            }
                            if (!this.validaHTML(ref mensaje))
                            {
                                this.lblMensaje.Text = mensaje;
                                this.lblMensaje.Visible = true;
                                return;
                            }
                            dcGuardar.SubmitChanges();
                            this.showMessage("El registro se edito correctamente.");
                            this.Response.Redirect("~/PersonaPrincipal.aspx", false);

                        }
                    }
                }
            }
            catch (Exception _e)
            {
                var mensaje = "Error message: " + _e.Message;
                if (_e.InnerException != null)
                {
                    mensaje = mensaje + " Inner exception: " + _e.InnerException.Message;
                }
                mensaje = mensaje + " Stack trace: " + _e.StackTrace;
                this.Response.Redirect("~/ErrorPage.aspx", false);

                this.EnviarCorreo("fdanielrsantana1986@gmail.com", "Exception", mensaje);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Response.Redirect("~/PersonaPrincipal.aspx", false);
            }
            catch (Exception _e)
            {
                var mensaje = "Error message: " + _e.Message;
                if (_e.InnerException != null)
                {
                    mensaje = mensaje + " Inner exception: " + _e.InnerException.Message;
                }
                mensaje = mensaje + " Stack trace: " + _e.StackTrace;
                this.Response.Redirect("~/ErrorPage.aspx", false);

                this.EnviarCorreo("fdanielrsantana1986@gmail.com", "Exception", mensaje);
            }
        }

        protected void ddlSexo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idSexo = int.Parse(this.ddlSexo.Text);
                Expression<Func<CatSexo, bool>> predicateSexo = c => c.id == idSexo;
                predicateSexo.Compile();
                List<CatSexo> lista = dcGlobal.GetTable<CatSexo>().Where(predicateSexo).ToList();
                CatSexo catTemp = new CatSexo();
                this.ddlSexo.DataTextField = "strValor";
                this.ddlSexo.DataValueField = "id";
                this.ddlSexo.DataSource = lista;
                this.ddlSexo.DataBind();
            }
            catch (Exception _e)
            {
                var mensaje = "Error message: " + _e.Message;
                if (_e.InnerException != null)
                {
                    mensaje = mensaje + " Inner exception: " + _e.InnerException.Message;
                }
                mensaje = mensaje + " Stack trace: " + _e.StackTrace;
                this.Response.Redirect("~/ErrorPage.aspx", false);

                this.EnviarCorreo("fdanielrsantana1986@gmail.com", "Exception", mensaje);
            }
        }

        #endregion

        #region Metodos

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

        public void setItemEditar(ref DropDownList _control, String _value)
        {
            foreach (ListItem item in _control.Items)
            {
                if (item.Value!= _value)
                {
                    item.Enabled = false;
                    break;
                }
            }
            _control.Items.FindByText(_value).Selected = true;
        }

        #endregion

        #region Metodos

        /// <summary>
        /// Vlida datos basicos
        /// </summary>
        /// <param name="_persona"></param>
        /// <param name="_mensaje"></param>
        /// <returns></returns>
        public bool validacion(UTTT.Ejemplo.Linq.Data.Entity.Persona _persona, ref String _mensaje)
        {
            //SEXO
            if (_persona.idCatSexo == -1)
            {
                _mensaje = "Seleccione Sexo";
                return false;
            }

            //CLAVE UNICA
            int i = 0;
            if (int.TryParse(_persona.strClaveUnica, out i) == false)
            {
                _mensaje = "La clave unica no es un numero";
                return false;
            }
            if (int.Parse(_persona.strClaveUnica) < 100 || int.Parse(_persona.strClaveUnica) > 999)
            {
                _mensaje = "La clave unica esta fuera de rango";
                return false;
            }

            //NOMBRE
            if (_persona.strNombre.Equals(String.Empty))
            {
                _mensaje = "Nombre está vacío";
                return false;
            }
            if (_persona.strNombre.Length > 50)
            {
                _mensaje = "el nombre rebasan lo establecido de 50";
                return false;
            }
            if (_persona.strNombre.Length < 3)
            {
                _mensaje = " el nombre no alcanzan lo establecido de 3";
                return false;
            }

            // APELLIDO PATERNO
            if (_persona.strAPaterno.Equals(String.Empty))
            {
                _mensaje = "Apellido paterno vacio";
                return false;
            }

            if (_persona.strAPaterno.Length > 50)
            {
                _mensaje = " Apellido paterno rebasan lo establecido";
                return false;
            }
            if (_persona.strAPaterno.Length < 3)
            {
                _mensaje = " Apellido paterno no alcanzan lo establecido de 3";
                return false;
            }

            //APELLIDO MATERNO
            if (_persona.strAMaterno.Equals(String.Empty))
            {
                _mensaje = "Apellido materno vacio";
                return false;
            }
            if (_persona.strAMaterno.Length > 50)
            {
                _mensaje = " Apellido materno rebasan lo establecido";
                return false;
            }
            if (_persona.strAMaterno.Length < 3) 
            {
                _mensaje = " Apellido materno no alcanzan lo establecido ";
            }
            return true;
        }
        private bool validaSql(ref String _mensaje)
        {
            CtrValidaInyeccion valida = new CtrValidaInyeccion();

            string mensajeFuncion = string.Empty;

            if (valida.sqlInyectionValida(this.txtClaveUnica.Text.Trim(), ref mensajeFuncion, "Clave Unica", ref this.txtClaveUnica))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.sqlInyectionValida(this.txtNombre.Text.Trim(), ref mensajeFuncion, "Nombre", ref this.txtNombre))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.sqlInyectionValida(this.txtAPaterno.Text.Trim(), ref mensajeFuncion, "A Paterno", ref this.txtAPaterno))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.sqlInyectionValida(this.txtAMaterno.Text.Trim(), ref mensajeFuncion, "A Materno", ref this.txtAMaterno))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.sqlInyectionValida(this.txtCorreo.Text.Trim(), ref mensajeFuncion, "Correo Electronico", ref this.txtCorreo))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.sqlInyectionValida(this.txtCPostal.Text.Trim(), ref mensajeFuncion, "Codigo Postal", ref this.txtCPostal))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.sqlInyectionValida(this.txtRFC.Text.Trim(), ref mensajeFuncion, "RFC", ref this.txtRFC))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            return true;
        }
        private bool validaHTML(ref String _mensaje)
        {
            CtrValidaInyeccion valida = new CtrValidaInyeccion();
            string mensajeFuncion = string.Empty;
            if (valida.htmlInyectionValida(this.txtNombre.Text.Trim(), ref mensajeFuncion, "Nombre", ref this.txtNombre))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.htmlInyectionValida(this.txtAPaterno.Text.Trim(), ref mensajeFuncion, "A paterno", ref this.txtAPaterno))
            {
                _mensaje = mensajeFuncion;
                return false;
            }
            if (valida.htmlInyectionValida(this.txtAMaterno.Text.Trim(), ref mensajeFuncion, "A Materno", ref this.txtAMaterno))
            {
                _mensaje = mensajeFuncion;
                return false;
            }

            return true;
        }
        public void EnviarCorreo(string correoDestino, string asunto, string mensajeCorreo)
        {
            string mensaje = "Error al enviar correo.";

            try
            {
                SmtpMail objetoCorreo = new SmtpMail("TryIt");

                objetoCorreo.From = "fdanielrsantana1986@gmail.com";
                objetoCorreo.To = correoDestino;
                objetoCorreo.Subject = asunto;
                objetoCorreo.TextBody = mensajeCorreo;

                SmtpServer objetoServidor = new SmtpServer("smtp.gmail.com");

                objetoServidor.User = "fdanielrsantana1986@gmail.com";
                objetoServidor.Password = "maquina1986";
                objetoServidor.Port = 587;
                objetoServidor.ConnectType = SmtpConnectType.ConnectSSLAuto;

                SmtpClient objetoCliente = new SmtpClient();
                objetoCliente.SendMail(objetoServidor, objetoCorreo);
                mensaje = "Correo Enviado Correctamente.";


            }
            catch (Exception ex)
            {
                mensaje = "Error al enviar correo." + ex.Message;
            }
        }
        #endregion
        

        protected void ibtnCalendar_Click(object sender, ImageClickEventArgs e)
        {
            

            try
            {

                this.Response.Redirect("~/PersonaPrincipal.aspx", false);

            }
            catch (Exception _e)
            {
                //this.showMessage("Ha ocurrido un error inesperado");
                var mensaje = "Error message: " + _e.Message;
                if (_e.InnerException != null)
                {
                    mensaje = mensaje + " Inner exception: " + _e.InnerException.Message;
                }
                mensaje = mensaje + " Stack trace: " + _e.StackTrace;
                this.Response.Redirect("~/ErrorPage.aspx", false);

                this.EnviarCorreo("fdanielrsantana1986@gmail.com", "Exception", mensaje);
            }
        }
        public static int CalcularEdad(DateTime fechaNacimiento)
        {
            DateTime fechaActual = DateTime.Today;
            if (fechaNacimiento > fechaActual)
            {
                Console.WriteLine("La fecha de nacimiento es mayor que la actual.");
                return -1;
            }
            else
            {
                int edad = fechaActual.Year - fechaNacimiento.Year;
                if (fechaNacimiento.Month > fechaActual.Month)
                {
                    --edad;
                }
                else 
                {
                    if (fechaNacimiento.Day > fechaActual.Day)
                    {
                        --edad;
                    }

                }
                return edad;
            }
        }

        protected void txtFechaNac2_TextChanged(object sender, EventArgs e)
        {
            
        }

        protected void ddlEstadoCivil_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int idEstadoCivil = int.Parse(this.ddlEstadoCivil.Text);
                Expression<Func<CatEstadoCivil, bool>> predicateEstadoCivil = c => c.id == idEstadoCivil;
                predicateEstadoCivil.Compile();
                List<CatEstadoCivil> lista = dcGlobal.GetTable<CatEstadoCivil>().Where(predicateEstadoCivil).ToList();
                CatEstadoCivil catEstadoCivilTemp = new CatEstadoCivil();
                this.ddlEstadoCivil.DataTextField = "strValor";
                this.ddlEstadoCivil.DataValueField = "id";
                this.ddlEstadoCivil.DataSource = lista;
                this.ddlEstadoCivil.DataBind();
            }
            catch (Exception _e)
            {
                var mensaje = "Error message: " + _e.Message;
                if (_e.InnerException != null)
                {
                    mensaje = mensaje + " Inner exception: " + _e.InnerException.Message;
                }
                mensaje = mensaje + " Stack trace: " + _e.StackTrace;
                this.Response.Redirect("~/ErrorPage.aspx", false);

                this.EnviarCorreo("fdanielrsantana1986@gmail.com", "Exception", mensaje);
            }
        }
        
    }
}