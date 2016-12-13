using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OtobusBiletSatis
{
    public partial class InsanKaynaklari : System.Web.UI.Page
    {
        otobusEntities ent = new otobusEntities();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnGonder_Click(object sender, EventArgs e)
        {
            basvurular yeni = new basvurular();
            yeni.ad_soyad = txtAd.Text;
            yeni.dogumtarihi = Convert.ToDateTime(txtTarih.Text);
            yeni.dogumyeri = txtDYeri.Text;
            yeni.mail = txtPosta.Text;
            yeni.tel = txtGsm.Text;
            yeni.ikametadresi = txtAdres.Text;
            yeni.kısabilgi = txtBilgi.Text;
            yeni.medenihal = DropDownList1.SelectedValue;
            yeni.dosya = FileUpload1.PostedFile.FileName;
            FileUpload1.SaveAs("C:\\aspnetyukle\\" + FileUpload1.FileName);
            if (FileUpload1.HasFile)
                try
                {
                    ent.basvurular.Add(yeni);
                    ent.SaveChanges();
                    Response.Write("Başvuru Başarılı...");
                    Response.Redirect("Default.aspx");
                }
                catch (Exception ex)
                {
                    string hata = ex.Message;
                }
            else
            {
                Response.Write("Dosya Seçin ve Yükleyin");
            }
        }
    }
}