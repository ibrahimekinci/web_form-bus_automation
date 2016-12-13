using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OtobusBiletSatis
{
    public partial class iletisim : System.Web.UI.Page
    {
        otobusEntities ent = new otobusEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Session["araSubeSehirID"].ToString() != "")
                    {
                        subeBulunanSehirleriGetir();
                        dlSubeSehir.SelectedValue = Session["araSubeSehirID"].ToString();
                        try
                        {
                            if (Session["araSubeIlceID"].ToString() != "")
                            {
                                subeBulunanIlceleriGetir();
                                dlSubeIlce.SelectedValue = Session["araSubeIlceID"].ToString();
                                if (dlSubeIlce.SelectedIndex > 0)
                                    dlDoldurBySubeID();
                                else
                                    dlDoldur();

                            }
                        }
                        catch (Exception)
                        {


                        }

                    }
                }
                catch (Exception)
                {
                    subeBulunanSehirleriGetir();
                }
            }

        }

        void subeBulunanSehirleriGetir()
        {
            try
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
            catch (Exception)
            {


            }
        }
        void subeBulunanIlceleriGetir()
        {
            try
            {
                dlSubeIlce.Items.Clear();
                int sehirID = Convert.ToInt32(dlSubeSehir.SelectedValue.ToString());
                var veri = (from s in ent.sube where s.adres.sehirID == sehirID select new { s.ad, s.subeID }).ToList();
                dlSubeIlce.DataTextField = "ad";
                dlSubeIlce.DataValueField = "subeID";
                dlSubeIlce.DataSource = veri;
                dlSubeIlce.DataBind();
                ListItem li = new ListItem();
                li.Text = "Tümü";
                li.Value = "Tümü";
                dlSubeIlce.Items.Insert(0, li);

                if (dlSubeIlce.SelectedIndex > 0)
                    dlDoldurBySubeID();
                else
                    dlDoldur();
            }
            catch (Exception)
            {


            }
        }
        void dlDoldurBySubeID()
        {
            try
            {
                int sehirID = Convert.ToInt32(dlSubeSehir.SelectedValue.ToString());
                int subeID = Convert.ToInt32(dlSubeIlce.SelectedValue.ToString());
                var veri = (from s in ent.sube where s.adres.sehirID == sehirID && s.subeID == subeID select s).ToList();
                dlSube.DataSource = veri;
                dlSube.DataBind();

            }
            catch (Exception)
            {


            }

        }
        void dlDoldur()
        {
            try
            {
                int sehirID = Convert.ToInt32(dlSubeSehir.SelectedValue.ToString());
                var veri = (from s in ent.sube where s.adres.sehirID == sehirID select s).ToList();
                dlSube.DataSource = veri;
                dlSube.DataBind();
            }
            catch (Exception)
            {

            }

        }
        protected void dlSubeSehir_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dlSubeSehir.SelectedIndex > 0)
                subeBulunanIlceleriGetir();
            else
                dlSubeIlce.Items.Clear();

        }
        protected void dlSubeIlce_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (dlSubeIlce.SelectedIndex > 0)
                dlDoldurBySubeID();
            else
                dlDoldur();
        }
    }
}