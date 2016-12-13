using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace OtobusBiletSatis
{
    public partial class login : System.Web.UI.Page
    {
        public string mesajTitle, mesajText = "";
        bool hata = false;
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
            if (!IsPostBack) { 
                txtUser.Focus();
                divMesaj.Visible = false;
            }


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
                otobusEntities ent = new otobusEntities();
                try
                {
                    var mus = (from m in ent.musteri
                               where m.kadi == txtUser.Text.Trim() && m.pass == txtPass.Text.Trim() && m.durum == true
                               select new { m.musteriID, m.cinsiyet }).First();
                    if (mus.cinsiyet != null && mus.cinsiyet != "")
                    {
                        Session["cinsiyet"] = Convert.ToString(mus.cinsiyet);
                        Session["musteriID"] = Convert.ToString(mus.musteriID);
                        Response.Redirect("/BiletIslemleri.aspx");
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
                //SqlConnection conn = new SqlConnection(genel.connStr);
                //SqlCommand comm = new SqlCommand("select * from musteri where kadi=@u and pass=@p", conn);
                //SqlDataReader dr;
                //comm.Parameters.AddWithValue("@u", txtUser.Text);
                //comm.Parameters.AddWithValue("@p", txtPass.Text);
                //if (conn.State == ConnectionState.Closed) conn.Open();
                //try
                //{
                //    dr = comm.ExecuteReader();
                //    if (dr.Read())
                //    {
                //        Session["cinsiyet"] = Convert.ToString(dr["cinsiyet"]);
                //        Session["musteriID"] = Convert.ToString(dr["musteriID"]);
                //        Response.Redirect("/Bilet.aspx");
                //    }
                //    else
                //    {
                //        mesajTitle = "Hatalı Giriş...";
                //        mesajText = "Lütfen Kontrol Edip Tekrar Deneyiniz...";
                //        hata = true;
                //        txtUser.Focus();
                //    }
                //    dr.Close();
                //}
                //catch (Exception)
                //{
                //    mesajTitle = "Opss... Bir Hata oluştu.";
                //    mesajText = "Daha zeki olmalısınız ve inputlarımızla oynamamalısınız... :)";
                //    hata = true;
                //}
                //finally { conn.Close(); }
            }
      
            divMesaj.Visible = hata;
        }
    }
}