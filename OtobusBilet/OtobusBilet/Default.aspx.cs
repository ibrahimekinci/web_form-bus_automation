using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Text;


namespace OtobusBiletSatis
{
    public partial class anasayfa : System.Web.UI.Page
    {
        otobusEntities ent = new otobusEntities();
        cGenel genel = new cGenel();
        public string mesajTitle, mesajText = "";
        bool hata = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    divMesaj.Visible = false;
                    var veri = (from s in ent.sube
                                join se in ent.sehir on s.adres.sehirID equals se.sehirID
                                select new { s.subeID, ad = s.ad + " / " + se.ad }
                               ).ToList();
                    ddlGidis.DataTextField = "ad";
                    ddlGidis.DataValueField = "subeID";
                    ddlGidis.DataSource = veri;
                    ddlGidis.DataBind();
                    ddlGidis.Items.Insert(0, "Seç");

                    ddlDonus.DataTextField = "ad";
                    ddlDonus.DataValueField = "subeID";
                    ddlDonus.DataSource = veri;
                    ddlDonus.DataBind();
                    ddlDonus.Items.Insert(0, "Seç");
                    cldSeferGunu.SelectedDate = DateTime.Now;
                    txtSeferGunu.Text = genel.tarihCevirString(String.Format("{0:MM/dd/yyyy}", DateTime.Now));
                    subeBulunanSehirleriGetir();
                }
            }
            catch (Exception)
            {
                mesajTitle = "Tüh bir şeyler ters gitti";
                mesajText = "lütfen daha sonra tekrar deneyiniz";
                hata = true;
                divMesaj.Visible = hata;
            }

            try
            {
                if (Session["musteriID"].ToString().Trim() != "" || Session["cinsiyet"].ToString().Trim() != "")
                {
                    menuHizliGiris.Visible = false;
                    icerikHizliGiris.Visible = false;
                }
            }
            catch (Exception)
            {
                menuHizliGiris.Visible = true;
                icerikHizliGiris.Visible = true;
            }


        }
        protected void cldSeferGunu_SelectionChanged(object sender, EventArgs e)
        {
            txtSeferGunu.Text = genel.tarihCevirString(String.Format("{0:MM/dd/yyyy}", genel.tarihCevir(cldSeferGunu.SelectedDate.ToShortDateString())));
        }
        bool hataKontrol()
        {
            try
            {
                if (ddlGidis.SelectedIndex == 0)
                {
                    mesajTitle = "Eksik Seçim";
                    mesajText = "Lütfen Gidiş Yerinizi Seçiniz...";
                    hata = true;
                }
                else if (ddlDonus.SelectedIndex == 0)
                {
                    mesajTitle = "Eksik Seçim";
                    mesajText = "Lütfen Varış Yerinizi Seçiniz...";
                    hata = true;
                }
                else if (ddlDonus.SelectedValue == ddlGidis.SelectedValue)
                {
                    mesajTitle = "Aynı Seçim";
                    mesajText = "Gidiş ve Varış yeri aynı olmaz.Lütfen seçimlerinizi değiştirin...";
                    hata = true;
                }
            }
            catch (Exception)
            {
                hata = true;
                mesajTitle = "Tüh bir şeyler ters gitti";
                mesajText = "lütfen daha sonra tekrar deneyiniz";
            }
            return hata;
        }
        protected void btnSeferAra_Click(object sender, EventArgs e)
        {
            if (!hataKontrol())
            {
                Session["nereden"] = ddlGidis.SelectedValue.ToString();
                Session["nereye"] = ddlDonus.SelectedValue.ToString();
                Session["tarih"] = txtSeferGunu.Text;
                Response.Redirect("/BiletIslemleri.aspx");
            }
            divMesaj.Visible = hata;
        }
        //öneri ve görüşler
        protected void btnIlet_Click(object sender, EventArgs e)
        {
            hata = false;
            if (txtAd.Text.Trim() == "" )
            {
                 mesajTitle = "Ad alanı boş geçilemez";
                hata=true;
            }
            else if(txtmail.Text.Trim() == "")
            {
                mesajTitle = "Mail alanı boş geçilemez";
                hata = true;
            }
            else if(txtMesaj.Text.Trim() == "")
            {
                mesajTitle = "Mesaj alanı boş geçilemez";
                hata = true;
            }
            if (!hata)
            {
                try
                {
                    MailMessage mail = new MailMessage();
                    SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                    SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
                    SmtpServer.UseDefaultCredentials = false;
                    SmtpServer.Timeout = 10000;
                    mail.BodyEncoding = UTF8Encoding.UTF8;
                    mail.From = new MailAddress(txtmail.Text);
                    mail.To.Add("dndcyln@gmail.com");
                    mail.Subject = "Öneri";
                    mail.Body = txtMesaj.Text;
                    mail.IsBodyHtml = true;
                    SmtpServer.Port = 587;
                    SmtpServer.Credentials = new System.Net.NetworkCredential("crm.sonyproject@gmail.com", "sonysony1907");
                    SmtpServer.EnableSsl = true;
                    SmtpServer.Send(mail);
                    mesajTitle = "Mail gönderilmiştir !";
                    mesajText = "İlginiz için teşekkürler.En kısa zamanda isteğiniz doğrultusunda sizin iletişime geçeceğiz";
                    hata = true;
                    txtAd.Text = "";
                    txtmail.Text = "";
                    txtMesaj.Text = "";
                
                }
                catch (Exception ex)
                {
                    mesajTitle = "Mesajınız iletilirken Hata!";
                    mesajText = "Lütfen daha sonra tekrar deneyiniz..";
                    hata = true;
                }
            }
            divMesaj.Visible = hata;

        }
        protected void btnGirisYap_Click(object sender, EventArgs e)
        {
            hata = false;
            if (txtUser.Text.Trim() == "")
            {
                mesajTitle = "Eksik bilgi girişi...";
                mesajText = "Lütfen gerekli bilgileri eksiksiz girin...";
                txtUser.Focus();
                hata = true;
            }
            else if (txtPass.Text.Trim() == "")
            {
                mesajTitle = "Eksik bilgi girişi...";
                mesajText = "Lütfen gerekli bilgileri eksiksiz girin...";
                txtPass.Focus();
                hata = true;
            }
            if (!hata)
            {
                try
                {
                    var mus = (from m in ent.musteri
                               where m.kadi == txtUser.Text.Trim() && m.pass == txtPass.Text.Trim() && m.durum == true
                               select new { m.musteriID, m.cinsiyet }).First();
                    if (mus.cinsiyet != null && mus.cinsiyet != "")
                    {
                        Session["cinsiyet"] = Convert.ToString(mus.cinsiyet);
                        Session["musteriID"] = Convert.ToString(mus.musteriID);
                        Response.Redirect("/Default.aspx");
                    }
                    else
                    {
                        mesajTitle = "Hatalı  Giriş";
                        mesajText = "Lütfen bilgileriniz kontrol edip tekrar deneyiniz.";
                        hata = true;
                    }

                }
                catch (Exception)
                {
                    mesajTitle = "Hatalı  Giriş";
                    mesajText = "Lütfen bilgileriniz kontrol edip tekrar deneyiniz.";
                    txtUser.Focus();
                    hata = true;
                }
            }

            divMesaj.Visible = hata;
        }
        void subeBulunanSehirleriGetir()
        {
            dlSubeSehir.Items.Clear();
            var veri = (from s in ent.sube
                        join se in ent.sehir on s.adres.sehirID equals se.sehirID
                        select se).Distinct().ToList();//bu güzelmiş :)
            dlSubeSehir.DataTextField = "ad";
            dlSubeSehir.DataValueField = "sehirID";
            dlSubeSehir.DataSource = veri;
            dlSubeSehir.DataBind();
            dlSubeSehir.Items.Insert(0, "Seç");
        }
        void subeBulunanIlceleriGetir()
        {
            dlSubeIlce.Items.Clear();
            int sehirID=Convert.ToInt32(dlSubeSehir.SelectedValue.ToString());
            var veri = (from s in ent.sube where s.adres.sehirID==sehirID select new { s.ad, s.subeID }).ToList();
            dlSubeIlce.DataTextField = "ad";
            dlSubeIlce.DataValueField = "subeID";
            dlSubeIlce.DataSource = veri;
            dlSubeIlce.DataBind();
            ListItem li = new ListItem();
            li.Text="Tümü";
            li.Value="Tümü";
            dlSubeIlce.Items.Insert(0,li );
        }
        protected void dlSubeSehir_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dlSubeSehir.SelectedIndex > 0)
                subeBulunanIlceleriGetir();
            else
                dlSubeIlce.Items.Clear();
          
        }
        protected void btnSubeAra_Click(object sender, EventArgs e)
        {
            try
            {
                if (dlSubeSehir.SelectedIndex==0)
                {
                    mesajTitle = "Lütfen Şube Seçiniz";
                    mesajText = "";
                    hata = true;

                }
                else
                {
                    Session["araSubeSehirID"] = dlSubeSehir.SelectedValue.ToString();
                    Session["araSubeIlceID"] = dlSubeIlce.SelectedValue.ToString();
                    Response.Redirect("/Iletisim.aspx");
                }
            }
            catch (Exception)
            {
                
                throw;
            }
            divMesaj.Visible = hata;
        }
    }
}