using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OtobusBiletSatis
{
    public partial class NestedMasterPage1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //soldaki menuyu girşi yapılmışsa açacağız...
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
    }
}