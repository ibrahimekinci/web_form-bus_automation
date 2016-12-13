using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OtobusBiletSatis
{
    public partial class UyeOl : System.Web.UI.Page
    {
        otobusEntities ent = new otobusEntities();
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (Session["musteriID"].ToString().Trim() != "" || Session["cinsiyet"].ToString().Trim() != "")
                    Response.Redirect("/BiletIslemleri.aspx");
            }
            catch (Exception)
            {

            }
            if (!IsPostBack)
                sehilerigetir();
        }
        void sehilerigetir()
        {
            var sehirr = (from ibrahim in ent.sehir
                          select ibrahim).ToList();
            ddlIl.DataValueField = "sehirID";
            ddlIl.DataTextField = "ad";
            ddlIl.DataSource = sehirr;
            ddlIl.DataBind();

        }
        void ilcelerigetirbysehir(int sehirid)
        {
            var ilcee = (from dnd in ent.ilce
                         where dnd.sehirID == sehirid
                         select dnd).ToList();
            ddlIlce.DataValueField = "ilceID";
            ddlIlce.DataTextField = "ad";
            ddlIlce.DataSource = ilcee;
            ddlIlce.DataBind();
        }

        void mahallegetirbyilce(int ilceid)
        {
            var mah = (from snr in ent.mahalle
                       where snr.ilceID == ilceid
                       select snr).ToList();
            ddlMah.DataValueField = "mahalleID";
            ddlMah.DataTextField = "ad";
            ddlMah.DataSource = mah;
            ddlMah.DataBind();
        }
        protected void ddlIl_SelectedIndexChanged(object sender, EventArgs e)
        {
            ilcelerigetirbysehir(Convert.ToInt32(ddlIl.SelectedValue));
        }

        protected void ddlIlce_SelectedIndexChanged(object sender, EventArgs e)
        {
            mahallegetirbyilce(Convert.ToInt32(ddlIlce.SelectedValue));
        }
        int insertAdresID(int sehirid, int ilceid, int mahid)
        {
            int adresid = 0;
            adres yeni = new adres();
            yeni.sehirID = sehirid;
            yeni.ilceID = ilceid;
            yeni.mahalleID = mahid;
            ent.adres.Add(yeni);
            ent.SaveChanges();
            return adresid;
        }

        protected void btnKaydol_Click(object sender, EventArgs e)
        {
            musteri mus = new musteri();
            if (ddlIl.SelectedValue != "" && ddlIlce.SelectedValue != "" && ddlMah.SelectedValue != "")
            {
                int id = Convert.ToInt32(insertAdresID(Convert.ToInt32(ddlIl.SelectedValue), Convert.ToInt32(ddlIlce.SelectedValue), Convert.ToInt32(ddlMah.SelectedValue)));
                var sonid = ent.adres.OrderByDescending(u => u.adresID).FirstOrDefault();

                mus.tckn = txtTCKNO.Text;
                mus.ad = txtAd.Text;
                mus.soyad = txtSoyad.Text;
                mus.cinsiyet = ddlCinsiyet.SelectedValue;
                mus.dogumTarihi = Convert.ToDateTime(txtDTarih.Text);
                mus.cepTelefonu = txtTel.Text;
                mus.eposta = txtPosta.Text;
                mus.adresID = sonid.adresID;
                mus.kayitTarihi = DateTime.Now;
                mus.kadi = txtUser.Text;
                mus.pass = txtPass1.Text;
                ent.musteri.Add(mus);
                ent.SaveChanges();
                Response.Write("Kayıt Başarılı...");
                Response.Redirect("Login.aspx");
            }
        }

    }
}