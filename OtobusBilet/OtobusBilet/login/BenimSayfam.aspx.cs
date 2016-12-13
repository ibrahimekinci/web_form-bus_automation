using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OtobusBiletSatis
{
    public partial class BenimSayfam : System.Web.UI.Page
    {
        public string mesajTitle, mesajText = "";
        bool hata = false;
        static int musteriID = 0;
        static int adresID = 0;
        otobusEntities ent = new otobusEntities();
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (Session["musteriID"].ToString().Trim() == "" || Session["cinsiyet"].ToString().Trim() == "")
                    Response.Redirect("/login/login.aspx");
            }
            catch (Exception)
            {
                Response.Redirect("/login/login.aspx");
            }
            if (!IsPostBack)
            {
                divMesaj.Visible = false;
                musteriID = Convert.ToInt32(Session["musteriID"].ToString().Trim());
                adresBilgileriGetir();
                bilgileriGetir();
            }


        }

        void sehirleriGetir()
        {
            ddlil.Items.Clear();
            var sehirr = (from s in ent.sehir
                          select s).ToList();
            ddlil.DataValueField = "sehirID";
            ddlil.DataTextField = "ad";
            ddlil.DataSource = sehirr;
            ddlil.DataBind();
            ListItem li = new ListItem();
            li.Text = "Seç";
            li.Value = "0";
            ddlil.Items.Insert(0, li);
        }
        void ilceleriGetir()
        {
            ddlilce.Items.Clear();
            int sehirID = Convert.ToInt32(ddlil.SelectedValue.ToString());

            var ilcee = (from dnd in ent.ilce
                         where dnd.sehirID == sehirID
                         select dnd).ToList();
            ddlilce.DataValueField = "ilceID";
            ddlilce.DataTextField = "ad";
            ddlilce.DataSource = ilcee;
            ddlilce.DataBind();
            ListItem li = new ListItem();
            li.Text = "Seç";
            li.Value = "0";
            ddlilce.Items.Insert(0, li);
        }
        void mahalleriGetir()
        {
            ddlmah.Items.Clear();
            int ilceID = Convert.ToInt32(ddlilce.SelectedValue.ToString());
            var mah = (from snr in ent.mahalle
                       where snr.ilceID == ilceID
                       select new {snr.ad,snr.mahalleID }).ToList();
            ddlmah.DataValueField = "mahalleID";
            ddlmah.DataTextField = "ad";
            ddlmah.DataSource = mah;
            ddlmah.DataBind();
            ListItem li = new ListItem();
            li.Text = "Seç";
            li.Value = "0";
            ddlmah.Items.Insert(0, li);

        }
        void adresBilgileriGetir()
        {
            var adresBilgi = (from a in ent.adres
                              join m in ent.musteri
                              on a.adresID equals m.adresID
                              where m.musteriID == musteriID
                              select new { a.adresID, a.sehirID, a.ilceID, a.mahalleID, a.aciklama }).First();
            adresID = adresBilgi.adresID;

            //şehir
            sehirleriGetir();
            if (adresBilgi.sehirID.ToString() != "")
                ddlil.SelectedValue = adresBilgi.sehirID.ToString();

            //ilçe
            ilceleriGetir();
            if (adresBilgi.ilceID.ToString() != "")
                ddlilce.SelectedValue = adresBilgi.ilceID.ToString();

            mahalleriGetir();
            if (adresBilgi.mahalleID.ToString() != "")
                ddlmah.SelectedValue = adresBilgi.mahalleID.ToString();
        }
        void bilgileriGetir()
        {
            musteri m = (from i in ent.musteri where i.musteriID == musteriID select i).First();
            txtTC.Text = m.tckn;
            txtAd.Text = m.ad;
            txtSoyad.Text = m.soyad;
            txtKullanici.Text = m.kadi;
            txtMail.Text = m.eposta;
            txtTel.Text = m.cepTelefonu;
            txtEvTel.Text = m.evTelefonu;
            txtKullanici.Text = m.kadi;
            txtAdresAciklama.Text= m.adres.aciklama;
        }
        protected void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSifre.Text != "")
                {
                    string s = (from i in ent.musteri where i.musteriID == musteriID select i.pass).First();
                    if (s== txtSifre.Text)
                    {
                        if (txtYeniSifre.Text.Trim() != txtYeniSifreTekrar.Text)
                        {
                            mesajTitle = "Yeni şifre ile tekrarı aynı değil";
                            mesajText = "";
                            hata = true;
                        }
                        if (!hata)
                        {
                            adres a = (from i in ent.adres where i.adresID == adresID select i).First();
                            a.sehirID = Convert.ToInt32(ddlil.SelectedValue.ToString());
                            a.ilceID = Convert.ToInt32(ddlilce.SelectedValue.ToString());
                            a.mahalleID = Convert.ToInt32(ddlmah.SelectedValue.ToString());
                            a.aciklama = txtAdresAciklama.Text;
                        
                            musteri m = (from i in ent.musteri where i.musteriID == musteriID select i).First();
                            m.tckn = txtTC.Text;
                            m.kadi = txtKullanici.Text;

                            m.ad = txtAd.Text;
                            m.soyad = txtSoyad.Text;
                            m.cepTelefonu = txtTel.Text;
                            m.evTelefonu = txtEvTel.Text;
                            m.eposta = txtMail.Text;

                            if (txtYeniSifre.Text.Trim()!="")
                                m.pass = txtYeniSifre.Text;
                            else
                            {
                                m.pass = txtSifre.Text;
                            }

                            ent.SaveChanges(); ;
                            mesajText = "";
                            mesajTitle = "Bilgileriniz Güncellendi..";
                            txtSifre.Text = "";
                            txtYeniSifre.Text = "";
                            txtYeniSifreTekrar.Text = "";
                            hata = true;
                        }

                    }
                    else
                    {
                        mesajText = "";
                        mesajTitle = "şifrenizi yanlış girdiniz..";
                        hata = true;
                    }
                }
                else
                {
                    mesajTitle = "şifrenizi girmelisiniz";
                    mesajText = "";
                    hata = true;
                }
            }
            catch (Exception)
            {
                mesajTitle = "Lütfen daha sonra tekrar deneyiniz..";
                mesajText = "opps bir hata oluştu..";
                hata = true;
            }
            divMesaj.Visible = hata;
        }

        protected void ddlil_SelectedIndexChanged(object sender, EventArgs e)
        {
            ilceleriGetir();
        }

        protected void ddlilce_SelectedIndexChanged(object sender, EventArgs e)
        {
            mahalleriGetir();
        }
    }
}