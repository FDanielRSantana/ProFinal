using EASendMail;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using UTTT.Ejemplo.Linq.Data.Entity;
using UTTT.Ejemplo.Persona.Control.Ctrl;
using UTTT.Ejemplo.Persona.Modelo;

using Usuario = UTTT.Ejemplo.Linq.Data.Entity.Usuario;


namespace UTTT.Ejemplo.Persona
{
    public partial class CambiarContra : System.Web.UI.Page
    {
        Persona1234Entities bd= new Persona1234Entities();
        DataContext dcGuardar = new DcGeneralDataContext();
        string url;

        
        private UTTT.Ejemplo.Linq.Data.Entity.Usuario baseEntity;
        private DataContext dcGlobal = new DcGeneralDataContext();

        protected void Page_Load(object sender, EventArgs e)
        {


            try
            {
                var usuario = bd.Persona.FirstOrDefault(x => x.strCorreo == txtcorreo.Text);
                if (usuario != null)
                {
                    
                    var usu2 = bd.Usuario.FirstOrDefault(u => u.idComPersona == usuario.id);
                    string correo = usuario.strCorreo.ToString();
                   // MD5("120500");
                    string tak = Token();
                    CorreoE(tak, correo);
                    this.baseEntity = dcGlobal.GetTable<Linq.Data.Entity.Usuario>().First(c => c.id == usu2.id);
                    DataContext dcGuardar = new DcGeneralDataContext();
                    UTTT.Ejemplo.Linq.Data.Entity.Usuario user = new Linq.Data.Entity.Usuario();
                    if (dcGlobal != null)
                    {
                        user = dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Usuario>().First(u => u.id == usu2.id);
                        var tok = tak;
                        Session["open"] = tok.ToString().Trim();
                        user.token = tok.ToString().Trim();
                        var net = Session["open"].ToString();
                        dcGuardar.SubmitChanges();
                        
                        this.lblMessage.Text = "Correo de recuperación enviadio";
                    }
                        
                    

                }
                else
                {
                    
                   // this.lblMessage.Text = "Correo no registrado";
                    
                }

            }
            catch (Exception ex)
            {
                this.lblMessage.Text = ex.Message;
            }
            //try
            //{
            //    var correo = this.txtcorreo.Text;




            //    // var usuario = bd.Persona.FirstOrDefault(x => x.strCorreo == txtcorreo.Text);
            //    var persona = dcGuardar.GetTable<UTTT.Ejemplo.Linq.Data.Entity.Persona>().Where(c => c.strCorreo == correo).FirstOrDefault();
            //    var correoExiste = dcGuardar.GetTable<Usuario>().Where(c => c.idComPersona == persona.id).FirstOrDefault();

            //    if (correoExiste != null)
            //    {

            //        CorreoE(correo);
            //        this.lblMessage.Text = "Correo Enviado con Éxito";

            //    }
            //    else
            //    {
            //        this.lblMessage.Text = "Correo no registrado";

            //    }

            //}
            //catch (Exception _e)
            //{

            //}
        }


            
        public new void CorreoE(string error, string correo)
        
        {
                string EmailOrigen = "fdanielrsantana@gmail.com";
                string EmailDestino = correo;
                string contra = "contraseña";
                 url = "http://fdrsantana.somee.com/RecuperarContraseña.aspx?token=" + error;

                MailMessage oMailMessage = new MailMessage(EmailOrigen, EmailDestino,
                    "Recuperacion de Contraseña ",
                     "<p>Para recuperar su cuenta ingrese al siguiente link </p> </br>" + "<a href=" + url + "> Recuperar</a>");
                //varhttp://www.fdrsantana.somee.com/RestablecerContraseña.aspx
                oMailMessage.IsBodyHtml = true;
                System.Net.Mail.SmtpClient oSmtpClient = new System.Net.Mail.SmtpClient("smtp.gmail.com");
                oSmtpClient.UseDefaultCredentials = false;
                oSmtpClient.Credentials = new System.Net.NetworkCredential("fdanielrsantana@gmail.com", "maquina1986");
                oSmtpClient.EnableSsl = true; 
               
                oSmtpClient.Port = 587;
                

                oSmtpClient.Send(oMailMessage);

                oMailMessage.Dispose();
            }

        public static string Token()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray()) i *= ((int)b + 1);
            
            return MD5(string.Format("{0:x}", i - DateTime.Now.Ticks));
        }

        public static string MD5(string word)
        {
            MD5 md5 = MD5CryptoServiceProvider.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();

            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = md5.ComputeHash(encoding.GetBytes(word));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

    }

    }
