using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;

namespace OtobusBiletSatis
{
    public partial class Unuttum : System.Web.UI.Page
    {
        public string mesajTitle, mesajText = "";
        bool hata = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtUser.Focus();
                divMesaj.Visible = false;
            }
        }

        protected void btnGonder_Click(object sender, EventArgs e)
        {
            hata = false;
            if (txtUser.Text.Trim() == "" && txtTC.Text.Trim() == "" && txtMail.Text.Trim() == "")
            {
                mesajTitle = "Eksik bilgi girişi...";
                mesajText = "Lütfen gerekli bilgileri eksiksiz girin...";
                txtUser.Focus();
                hata = true;
            }
            if (!hata)
            {
                try
                {
                    otobusEntities ent = new otobusEntities();
                    var musteriBilgi = (from s in ent.musteri
                                where s.kadi == txtUser.Text || s.tckn == txtTC.Text || s.eposta ==txtMail.Text
                                select new {s.pass,s.eposta,s.kadi}).First();
                    if (musteriBilgi.eposta != null)
                    {
                            MailMessage mail = new MailMessage();
                            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                            mail.From = new MailAddress("crm.sonyproject@gmail.com");
                            mail.To.Add(musteriBilgi.eposta);
                            mail.Subject = "Şifre İstek";
                            mail.Body ="Kullanıcı Adı:"+ musteriBilgi.kadi +"<br />"+"Şifre:"+musteriBilgi.pass+"<br />"+"DİS Turizm iyi yolculuklar diler";
                            mail.IsBodyHtml = true;
                            SmtpServer.Port = 587;
                            SmtpServer.Credentials = new System.Net.NetworkCredential("crm.sonyproject@gmail.com", "sonysony1907");
                            SmtpServer.EnableSsl = true;
                            SmtpServer.Send(mail);
                            Response.Redirect("/login/Login.aspx");
                    
                    }
                    else
                    {
                        hata = true;
                        mesajTitle = "Bilgilerinizde eposta adresiniz eksik!!!";
                        mesajText = "Eposta bilginiz bulunmadığı için şifreniz müşteri hizmetlerinde veya DİS Turizm acentalarından alabilirsiniz...";
                        txtUser.Focus();
                    }
                }
                catch (Exception)
                {
                    hata = true;
                    mesajTitle = "Girilen bilgilerle uyuşan bir hesap bulunamadı";
                    mesajText = "";
                    txtUser.Focus();
                }
            }
            divMesaj.Visible = hata;
        }

    }
}