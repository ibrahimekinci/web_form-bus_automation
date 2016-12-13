using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OtobusBiletSatis
{
    public partial class site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["musteriID"].ToString().Trim() != "" || Session["cinsiyet"].ToString().Trim() != "")
                {
                divLoginOff.Visible = false;
                divLoginOn.Visible = true;
                }      
            }
            catch (Exception)
            {
                divLoginOff.Visible = true;
                divLoginOn.Visible = false;
            }
        }

        protected void lbCikis_Click(object sender, EventArgs e)
        {
            try
            {
                Session.Remove("musteriID");
                Session.Remove("cinsiyet");
                divLoginOff.Visible = true;
                divLoginOn.Visible = false;
            }
            catch (Exception)
            {
                divLoginOff.Visible = true;
                divLoginOn.Visible = false;
            }
            Response.Redirect(Request.RawUrl);//url satırını alır ve tekrar aynı sayfaya yönlendirir = reflesh ettim
        }
    }
}