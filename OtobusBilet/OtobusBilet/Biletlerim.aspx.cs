using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

namespace OtobusBiletSatis
{
    public partial class Biletlerim : System.Web.UI.Page
    {
        otobusEntities ent = new otobusEntities();
        public string mesajTitle, mesajText = "";
        bool hata;
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
                biletleriGetir();
            }
        }

        bool biletleriGetir()
        {
            int musteriID = 0;
            hata = false;
            try
            {
                musteriID = Convert.ToInt32(Session["musteriID"].ToString().Trim());
            }
            catch (Exception)
            {
                Response.Redirect("/login/login.aspx");
            }
            DataTable dt = new DataTable();
            dt.Columns.Add("biletID");
            dt.Columns.Add("ad");
            dt.Columns.Add("islem");
            dt.Columns.Add("ucret");
            dt.Columns.Add("yolculukTarihi");
            dt.Columns.Add("durum");
            dt.Columns.Add("btnText");
            try
            {
                var biletler = (from b in ent.bilet
                                orderby b.alinmaTarihi descending
                                where b.musteriID == musteriID
                                select new { b.biletID, b.sefer.guzergah.ad, b.islem, b.ucret, b.alinmaTarihi, b.durum, b.sefer.kalkisTarihi }).ToList();

                foreach (var i in biletler)
                {
                    //System.Web.UI.HtmlControls.HtmlGenericControl divSec =
                    //             new System.Web.UI.HtmlControls.HtmlGenericControl("DIV");
                    //divSec.Attributes["class"] = "seferSutun";
                    //Button btnBiletSec = new Button();
                    //btnBiletSec.Text = "İptal Et";
                    //btnBiletSec.CommandArgument = i.biletID.ToString();
                    //btnBiletSec.CssClass = "buton";
                    //divSec.Controls.Add(btnBiletSec);
                    DataRow dr = dt.NewRow();
                    dr["biletID"] = i.biletID.ToString();
                    dr["ad"] = i.ad;
                    if (i.islem)
                        dr["islem"] = "Satış";
                    else
                        dr["islem"] = "Rezervasyon";

                    dr["ucret"] = i.ucret.ToString();
                    dr["yolculukTarihi"] = i.kalkisTarihi.ToShortDateString();


                    if (i.durum && i.kalkisTarihi > DateTime.Now)
                    {
                        dr["durum"] = "Aktif";
                        dr["btnText"] = "İptal Et";
                    }
                    else if (!i.durum)
                    {
                        dr["durum"] = "İptal";
                        dr["btnText"] = "";
                    }
                    else
                    {
                        dr["durum"] = "Geçmiş";
                        dr["btnText"] = "";
                    }
                    dt.Rows.Add(dr);
                }
                dlBiletlerim.DataSource = dt;
                dlBiletlerim.DataBind();
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Button btn = (Button)dlBiletlerim.Controls[i].FindControl("btnBiletSec");
                        Button btn2 = (Button)dlBiletlerim.Controls[i].FindControl("btnBiletKes");
                        if (btn.CommandName != "Aktif")
                            btn.Visible = false;


                        if (btn2.CommandName != "Rezervasyon" || btn.CommandName != "Aktif")
                            btn2.Visible = false;
                    }
                }
                else
                {
                    mesajTitle = "Hiç bilet işleminiz bulunmamakta.";
                    hata = true;
                }
            }
            catch (Exception)
            {
                mesajTitle = "Opss... Bir Hata oluştu.";
                mesajText = "Veri tabanından verilerinizi çekerken bir hata oluştu,Lütfen daha sonra tekrar deneyiniz...";
                hata = true;

            }
            divMesaj.Visible = hata;
            return hata;
        }

        protected void dlBiletlerim_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "Aktif")
            {
                int biletID = 0;
                biletID = Convert.ToInt32(e.CommandArgument);
                try
                {
                    bilet b = (from vr in ent.bilet where vr.biletID == biletID select vr).First();
                    ent.bilet.Remove(b);
                    ent.SaveChanges();
                      biletleriGetir();
                }
                catch (Exception)
                {
                    mesajTitle = "Opss... Bir Hata oluştu.";
                    mesajText = "Bilet iptali sırasında bir hata oluştu.Lüten daha sonra tekrar deneyiniz..";
                }
            }
            else if (e.CommandName == "Rezervasyon")
            {
                int biletID = 0;
                biletID = Convert.ToInt32(e.CommandArgument);
                try
                {
                    bilet b = (from vr in ent.bilet where vr.biletID == biletID select vr).First();
                    b.islem = true;
                    ent.SaveChanges();
                    biletleriGetir();
                }
                catch (Exception)
                {
                    mesajTitle = "Opss... Bir Hata oluştu.";
                    mesajText = "Bilet satın alma sırasında bir hata oluştu.Lüten daha sonra tekrar deneyiniz..";
                }
            }
        }
    }
}