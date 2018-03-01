using ONG.Models;
using ONG.Utilitarios;
using System;
using System.Linq;
using System.Web.UI;

namespace ONG
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void LogIn(object sender, EventArgs e)
        {
            Boolean enviar = false;
            if (IsValid)
            {
                // Validate the user password
            amislEntities  context = new amislEntities ();
            try
            {
              //  Response.Redirect("Default.aspx");
                String  usuariostr = UserName.Text;
                usuario n = context.usuario.First(i => i.USUARIO1  == usuariostr);
                
                    if ( SecurePasswordHasher.Verify(Password.Text,n.CLAVE  )){
                        enviar = true;
       
                    }else{
                        FailureText.Text = "Usuario o Password Inválido.";
                        ErrorMessage.Visible = true;
                    }
                
            }catch(Exception ex){
                FailureText.Text = "Usuario o Password Inválido.";
                ErrorMessage.Visible = true;
            } 

                //ApplicationUser user = manager.Find(UserName.Text, Password.Text);
                //if (UserName.Text == "admin" && Password.Text == "admin")
                //{
                //    //IdentityHelper.SignIn(manager, user, RememberMe.Checked);
                //    //IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                //    Response.Redirect("Default.aspx");
                //}
                //else
                //{
                //    FailureText.Text = "Usuario o Password Inválido.";
                //    ErrorMessage.Visible = true;
                //}
            if (enviar) {
                Response.Redirect("Default.aspx");
            }
            }
        }
    }
}