using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services;
using System.Data.SqlClient;

namespace OtobusBiletSatis
{
    public partial class BiletIslemleri : System.Web.UI.Page
    {
        otobusEntities ent = new otobusEntities();
        cGenel genel = new cGenel();
        static public string cinsiyet = "";
        static public int gidisKoltukSayisi = 56;
        static public int donusKoltukSayisi = 28;
        public bool ciftYon = false;
        public string tarih = "Tarih Seçiniz";
        public string mesajTitle, mesajText = "";
        bool hata = false;
        static DataTable dtSeferGidis = new DataTable();
        static DataTable dtSeferDonus = new DataTable();
        static int gidisSeferID = 0;
        static int donusSeferID = 0;
        static DataTable dtSatilanKoltuklarGidis = new DataTable();
        static DataTable dtSatilanKoltuklarDonus = new DataTable();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    divLogin.Visible = false;
                    divGidisKoltuklar.Visible = false;
                    divDonusKoltuklar.Visible = false;

                    divMesaj.Visible = false;
                    var veri = (from s in ent.sube
                                join se in ent.sehir on s.adres.sehirID equals se.sehirID
                                select new { subeID = s.subeID, ad = s.ad + " / " + se.ad }
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
                    try
                    {
                        ddlGidis.SelectedValue = Session["nereden"].ToString();
                        ddlDonus.SelectedValue = Session["nereye"].ToString();
                        tarih = Session["tarih"].ToString();
                    }
                    catch (Exception)
                    {

                    }
                }
                loginKontrol();
            }
            catch (Exception)
            {
                mesajTitle = "Opss... Bir Hata oluştu.";
                mesajText = "Daha zeki olmalısınız ve inputlarımızla oynamamalısınız... :)";
                hata = true;
            }
        }
        bool radioKontrol()
        {
            //0 hata biligisi tutar
            //1 çift yön mü tek yön müonu tutar
            // tut[1] false IServiceProvider tek yön true IServiceProvider çift yön seçilmiş demektir
            //Yön
            hata = false;
            string yon = "";
            try
            {
                yon = Request.Form["radioYon"].Trim();
                if (yon == "Tek Yön")
                    ciftYon = false;
                else if (yon == "Çift Yön")
                    ciftYon = true;
                else
                    hata = true;
            }
            catch (Exception)
            {
                mesajTitle = "Opss... Bir Hata oluştu.";
                mesajText = "Daha zeki olmalısınız ve inputlarımızla oynamamalısınız... :)";
                hata = true;
                divMesaj.Visible = hata;
            }
            if (hata)
            {
                mesajTitle = "Opss... Bir Hata oluştu.";
                mesajText = "Daha zeki olmalısınız ve inputlarımızla oynamamalısınız... :)";
            }

            return hata;
        }
        bool seferAraHataKontrol()
        {
            hata = false;
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
                else if (Request.Form["txtGidisTarihi"].ToString().Trim() == "")
                {
                    mesajTitle = "Tarih seçilmemiş...";
                    mesajText = "Gidiş tarihini seçmelisiniz...";
                    hata = true;
                }
                else if (!genel.tarihMi(Request.Form["txtGidisTarihi"].ToString().Trim()))
                {
                    mesajTitle = "Hatalı tarih formatı...";
                    mesajText = "Gidiş tarihini formatı hatalı...";
                    hata = true;
                }
                else if (ciftYon)
                {
                    if (Request.Form["txtDonusTarihi"].ToString().Trim() == "")
                    {
                        mesajTitle = "Tarih seçilmemiş...";
                        mesajText = "Donus tarihini seçmelisiniz...";
                        hata = true;
                    }
                    else if (!genel.tarihMi(Request.Form["txtDonusTarihi"].ToString().Trim()))
                    {
                        mesajTitle = "Hatalı tarih formatı...";
                        mesajText = "Donus tarihini formatı hatalı...";
                        hata = true;
                    }
                    else if (genel.tarihCevir(Request.Form["txtDonusTarihi"].ToString().Trim()) < genel.tarihCevir(Request.Form["txtGidisTarihi"].ToString().Trim()))
                    {
                        mesajTitle = "Hatalı tarih seçimi...";
                        mesajText = "Donus tarihi gidiş tarihinden önce olamaz...";
                        hata = true;
                    }
                }
            }
            catch (Exception)
            {
                mesajTitle = "Opss... Bir Hata oluştu.";
                mesajText = "Lütfen Tüm gerekli alanları doldurunuz ve daha sonra deneyiniz...";
                hata = true;
            }
            return hata;
        }
        bool biletSatisHataKontrol()
        {
            hata = false;
            string gidisNo = "", donusNo = "";
            try
            {
                if (divGidisKoltuklar.Visible)
                {
                    gidisNo = Request.Form["txtGidisNo"].ToString().Trim();
                    if (gidisNo == "")
                    {
                        mesajTitle = "Koltuk Seçilmemiş...";
                        mesajText = "Lütfen koltuk  seçimi yapınız...";
                        hata = true;
                    }

                    else if (Convert.ToInt32(gidisNo) < 0 || Convert.ToInt32(gidisNo) > gidisKoltukSayisi)
                    {
                        mesajTitle = "Hatalı seçim...";
                        mesajText = "Lütfen koltuk  seçimi yapınız...";
                        hata = true;
                    }
                    else if (ciftYon)
                    {

                        if (divDonusKoltuklar.Visible)
                        {
                            donusNo = Request.Form["txtDonusNo"].ToString().Trim();
                            if (Request.Form["txtDonusNo"].ToString().Trim() == "")
                            {
                                mesajTitle = "Koltuk Seçilmemiş...";
                                mesajText = "Lütfen Donus için koltuk  seçimi yapınız...";
                                hata = true;
                            }
                            else if (Convert.ToInt32(donusNo) < 0 || Convert.ToInt32(donusNo) > donusKoltukSayisi)
                            {
                                mesajTitle = "Hatalı seçim...";
                                mesajText = "Lütfen koltuk  seçimi yapınız...";
                                hata = true;
                            }
                        }
                        else
                        {
                            mesajTitle = "Eksik Bilgiler var";
                            mesajText = "lütfen zorunlu alanları doldurunuz..";
                            hata = true;
                        }
                    }
                }
                else
                {
                    mesajTitle = "Lütfen sefer seçiniz.";
                    mesajText = "Sefer Seçimi yapılmamış";
                    hata = true;
                }
            }
            catch (Exception)
            {
                mesajTitle = "Koltuk Seçilmemiş...";
                mesajText = "Lütfen koltuk  seçimi yapınız...";
                hata = true;
            }
            return hata;
        }
        bool loginKontrol()
        {
            hata = false;
            try
            {
                if (Session["musteriID"].ToString().Trim() == "" || Session["cinsiyet"].ToString().Trim() == "")
                {
                    mesajTitle = "Giriş Yapılmamış...";
                    mesajText = "Bilet satınalabilmek için lütfen giriş yapınız...";
                    hata = true;
                }
                else
                    cinsiyet = Session["cinsiyet"].ToString().Trim();
            }
            catch (Exception)
            {
                txtUser.Focus();
                mesajTitle = "Giriş Yapılmamış...";
                mesajText = "Bilet satınalabilmek için lütfen giriş yapınız...";
                hata = true;
            }

            return hata;
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
                        divMesaj.Visible = false;
                        divLogin.Visible = false;
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
        void seferGetir(DateTime t, bool a)
        {
            DataTable dtSefer = new DataTable();
            dtSefer.Columns.Add("SeferID");
            dtSefer.Columns.Add("Saat");
            dtSefer.Columns.Add("DurakSayisi");
            dtSefer.Columns.Add("MolaSayisi");
            dtSefer.Columns.Add("Mesafe");
            dtSefer.Columns.Add("Süre");
            dtSefer.Columns.Add("indirimliFiyat");
            dtSefer.Columns.Add("Fiyat");
            dtSefer.Columns.Add("NeredenSira");
            dtSefer.Columns.Add("NereyeSira");
            try
            {
                int nereden = 0;
                int nereye = 0;
                if (a)
                {
                    nereden = Convert.ToInt32(ddlGidis.SelectedValue.ToString());
                    nereye = Convert.ToInt32(ddlDonus.SelectedValue.ToString());
                    dtSeferGidis.Clear();
                }
                else
                {
                    nereden = Convert.ToInt32(ddlDonus.SelectedValue.ToString());
                    nereye = Convert.ToInt32(ddlGidis.SelectedValue.ToString());
                    dtSeferDonus.Clear();
                }
                var veri = ent.sp_seferGetirByGuzergahAndTarih(nereden, nereye, t).ToList();
                foreach (var i in veri)
                {
                    bool seferYon = i.yon;

                    bool iSeferYon = ent.sp_seferFiltreleByYon(i.guzergahID, nereden, nereye).FirstOrDefault().Value;

                    if (seferYon == iSeferYon)
                    {
                        var veriDetay = ent.sp_seferDetaylariGetir(i.guzergahID, nereden, nereye).ToList();
                        foreach (var id in veriDetay)
                        {

                            int neredenDurakSira = 0, nereyeDurakSira = 0, molaSayisi = 0, durakSayisi = 0, fiyat = 0, ifiyat = 0;
                            double sure = 0, mesafe = 0;
                            string sureText = "";
                            molaSayisi = Convert.ToInt32(id.molaSayisi);
                            mesafe = Convert.ToInt32(id.mesafe);
                            neredenDurakSira = Convert.ToInt32(id.neredenSira);
                            nereyeDurakSira = Convert.ToInt32(id.nereyeSira);
                            fiyat = Convert.ToInt32(mesafe / 7);
                            ifiyat = fiyat - fiyat / 10;
                            sure = mesafe / 90;//90 otobusun saatteki hızı
                            //saat hesaplama
                            sureText = Convert.ToString(Math.Floor(sure)) + " S ";
                            //dakika hesaplama
                            sure -= Math.Floor(sure);
                            sure *= 60;
                            sureText += Convert.ToString(Math.Ceiling(sure)) + " DK ";

                            if ((neredenDurakSira - nereyeDurakSira) > 0)
                                durakSayisi = neredenDurakSira - nereyeDurakSira;
                            else
                                durakSayisi = nereyeDurakSira - neredenDurakSira;
                            DataRow dr = dtSefer.NewRow();
                            dr["SeferID"] = i.seferID.ToString();
                            dr["Saat"] = String.Format("{0:HH:mm}", Convert.ToDateTime(i.kalkisTarihi));
                            dr["DurakSayisi"] = Convert.ToString(durakSayisi);
                            dr["MolaSayisi"] = Convert.ToString(molaSayisi);
                            dr["Mesafe"] = Convert.ToString(mesafe);
                            dr["Süre"] = Convert.ToString(sureText);
                            dr["indirimliFiyat"] = Convert.ToString(ifiyat);
                            dr["Fiyat"] = Convert.ToString(fiyat);
                            dr["NeredenSira"] = Convert.ToString(neredenDurakSira);
                            dr["NereyeSira"] = Convert.ToString(nereyeDurakSira);
                            dtSefer.Rows.Add(dr);

                        }
                    }
                }
                if (a)
                    dtSeferGidis = dtSefer;
                else
                    dtSeferDonus = dtSefer;
            }
            catch (Exception)
            {
                mesajTitle = "Opss... Bir Hata oluştu.";
                mesajText = "Daha zeki olmalısınız ve inputlarımızla oynamamalısınız... :)";
                hata = true;
                divMesaj.Visible = hata;
            }
        }
        int KoltukNoBelirler(int numara, int koltukSayisi)
        {
            if (koltukSayisi > 28)
            {
                if (numara % 2 == 1)
                {
                    if (numara < (koltukSayisi / 2) - 1)
                    {
                        numara += 2;
                    }
                    else if (numara == (koltukSayisi / 2) - 1)
                        numara = 2;
                    else
                    {
                        if (numara < (koltukSayisi) - 1)
                        {
                            numara += 2;
                        }
                        else if (numara == (koltukSayisi) - 1)
                            numara = (koltukSayisi / 2) + 2;
                    }
                }
                else
                {

                    if (numara == koltukSayisi / 2)
                        numara += 1;
                    else
                        numara += 2;
                }
            }
            else
            {
                if (numara <= 0)
                    numara = 1;
                else
                    numara += 1;
            }
            return numara;
        }
        bool satilanKoltuklariGetir(bool yon)
        {
            bool sonuc = false;
            int seferID = 0;
            DataTable dtSeferler = new DataTable();
            if (yon)
            {
                seferID = gidisSeferID;
                dtSeferler = dtSeferGidis;
                dtSatilanKoltuklarGidis.Clear();
            }
            else
            {
                seferID = donusSeferID;
                dtSeferler = dtSeferDonus;
                dtSatilanKoltuklarDonus.Clear();
            }
            int sirak = 0, sirab = 0;
            foreach (DataRow dr in dtSeferler.Rows)
            {
                if (Convert.ToString(dr["seferID"]) == seferID.ToString())
                {
                    if (Convert.ToInt32(dr["neredenSira"]) < Convert.ToInt32(dr["nereyeSira"]))
                    {
                        sirak = Convert.ToInt32(dr["neredenSira"]);
                        sirab = Convert.ToInt32(dr["nereyeSira"]);
                    }
                    else
                    {
                        sirab = Convert.ToInt32(dr["neredenSira"]);
                        sirak = Convert.ToInt32(dr["nereyeSira"]);
                    }
                    break;
                }
            }
            //try
            //{
            //1 yol entity olmadı bende uğraşmadım sıkıntıyı sonra çözücem :)

            //var satilanlar = (from s in ent.koltuk
            //                  where s.sefer.seferID == gidisSeferID && s.guzergahDurakSira >= sirak && s.guzergahDurakSira <= sirab
            //                  select new { s.koltukNo, s.musteri.cinsiyet }).ToList();
            //foreach (var s in satilanlar)
            //{
            //    DataRow dr = dtSatilanKoltukCinsiyet.NewRow();
            //    dr[0] = Convert.ToString(s.koltukNo);
            //    dr[1] = Convert.ToString(s.koltukNo);
            //    dtSatilanKoltukCinsiyet.Rows.Add(dr);
            //}

            //}
            //catch (Exception)
            //{
            //    mesajTitle = "Yolcu bilgileri çekilirken hata oluştu";
            //    mesajText = "Lütfen Daha sonra tekrar deneyiniz...";
            //    hata = true;
            //}
            SqlConnection conn = new SqlConnection(genel.connStr);
            string sqlSorgu = "select k.koltukNo,m.cinsiyet from koltuk k inner join sefer s on s.seferID =k.seferID inner join guzergahDurak gd on gd.guzergahID = s.guzergahID  inner join musteri m on m.musteriID= k.musteriID where and s.seferID =@seferID and k.guzergahDurakSira between @sira1 and @sira2 group by k.koltukNo,m.cinsiyet order by k.koltukNo";
            SqlDataAdapter da = new SqlDataAdapter(sqlSorgu, conn);
            da.SelectCommand.Parameters.Add("@seferID", SqlDbType.Int).Value = seferID;
            da.SelectCommand.Parameters.Add("@sira1", SqlDbType.Int).Value = sirak;
            da.SelectCommand.Parameters.Add("@sira2", SqlDbType.Int).Value = sirab;
            try
            {
                if (yon)
                    da.Fill(dtSatilanKoltuklarGidis);
                else
                    da.Fill(dtSatilanKoltuklarDonus);
                sonuc = true;
            }
            catch (Exception)
            {
                mesajTitle = "Yolcu bilgileri çekilirken hata oluştu";
                mesajText = "Lütfen Daha sonra tekrar deneyiniz...";
                hata = true;
            }
            return sonuc;
        }
        void koltuklariEkranaGetir(bool yon, int koltukSayisi)
        {
            hata = false;
            if (satilanKoltuklariGetir(yon))
            {
                DataTable dtSatilanKoltuklar = new DataTable();
                if (yon)
                    dtSatilanKoltuklar = dtSatilanKoltuklarGidis;
                else
                    dtSatilanKoltuklar = dtSatilanKoltuklarDonus;
                string html = "";
                int numara = -1;

                for (int i = 1; i <= koltukSayisi; i++)
                {
                    numara = KoltukNoBelirler(numara, koltukSayisi);
                    string yonText = "";
                    if (yon)
                        yonText = "gidis";
                    else
                        yonText = "donus";

                    string bay = "<div class=\"" + yonText + " koltuk koltukDolu koltukArkaBay\" number=\"" + numara.ToString() + "\"></div>";
                    string bayan = "<div class=\"" + yonText + " koltuk koltukDolu koltukArkaBayan\" number=\"" + numara.ToString() + "\"></div>";
                    string br = "<div class=\"clear\"></div>";
                    string bos = "<div class=\"" + yonText + " koltuk koltukBos\" number=\"" + numara.ToString() + "\"></div>";
                    int cinsiyet = 2;
                    foreach (DataRow dr in dtSatilanKoltuklar.Rows)
                    {
                        if (dr["koltukNo"].ToString() == numara.ToString())
                        {
                            if (dr["cinsiyet"].ToString().ToLower().Trim() == "bay")
                                cinsiyet = 0;
                            else if (dr[1].ToString().ToLower().Trim() == "bayan")
                                cinsiyet = 1;
                            break;
                        }
                    }

                    if (cinsiyet == 0)
                        html += bay;
                    else if (cinsiyet == 1)
                        html += bayan;
                    else if (cinsiyet == 2)
                        html += bos;
                    //koltuk nosu Belirle

                    if (koltukSayisi > 28)
                    {
                        if (i == koltukSayisi / 4 || i == (koltukSayisi / 2) || i == (koltukSayisi / 4) * 3 || i == koltukSayisi)
                        {
                            html += br;
                            if (i == koltukSayisi / 2)
                            {
                                if (yon)
                                    divGidisKoltukSira1.InnerHtml = html;
                                else
                                    divDonusKoltukSira1.InnerHtml = html;
                                html = "";
                            }
                            else if (i == koltukSayisi)
                            {
                                if (yon)
                                    divGidisKoltukSira2.InnerHtml = html;
                                else
                                    divDonusKoltukSira2.InnerHtml = html;
                                html = "";
                            }
                        }
                    }
                    else
                    {
                        if (i == koltukSayisi / 2 || i == koltukSayisi)
                        {
                            html += br;
                            if (i == koltukSayisi / 2)
                            {
                                if (yon)
                                    divGidisKoltukSira1.InnerHtml = html;
                                else
                                    divDonusKoltukSira1.InnerHtml = html;
                                html = "<div class=\"solaYasla\"> &nbsp;</div>" + br;
                            }
                            else if (i == koltukSayisi)
                            {
                                if (yon)
                                    divGidisKoltukSira2.InnerHtml = html;
                                else
                                    divDonusKoltukSira2.InnerHtml = html;
                                html = "";
                            }
                        }
                    }

                }
            }
        }
        protected void dlSeferler_ItemCommand(object source, DataListCommandEventArgs e)
        {
            if (e.CommandName == "gidis")
            {
                divGidisKoltuklar.Visible = true;
                gidisSeferID = Convert.ToInt32(e.CommandArgument);
                var veri = (from i in ent.sefer where i.seferID == gidisSeferID select new { koltukSayisi = i.otobus.otobusModel.koltukSayisi }).FirstOrDefault();
                koltuklariEkranaGetir(true, veri.koltukSayisi);
            }
            else if (e.CommandName == "donus")
            {
                divDonusKoltuklar.Visible = true;
                donusSeferID = Convert.ToInt32(e.CommandArgument);
                var veri = (from i in ent.sefer where i.seferID == donusSeferID select new { koltukSayisi = i.otobus.otobusModel.koltukSayisi }).FirstOrDefault();
                koltuklariEkranaGetir(false, veri.koltukSayisi);
            }
        }
        protected void btnSeferAra_Click(object sender, EventArgs e)
        {
            try
            {
                divGidisKoltuklar.Visible = false;
                divDonusKoltuklar.Visible = false;
                //DateTime tarih = DateTime.ParseExact(Request.Form["txtGidisTarihi"], "dd/MM/yyyy", null);
                //çift yon olump olmadığı public değişkende 
                if (!radioKontrol())
                {
                    if (!seferAraHataKontrol())
                    {

                        //DateTime gidisTarihi = Convert.ToDateTime(Request.Form["txtGidisTarihi"]);
                        if (!ciftYon)
                        {
                            seferGetir(genel.tarihCevir(Request.Form["txtGidisTarihi"].ToString()), true);
                            dlGidisSeferler.DataSource = dtSeferGidis;
                            dlGidisSeferler.DataBind();
                            dlDonusSeferler.Visible = false;
                        }
                        else
                        {
                            dlDonusSeferler.Visible = true;
                            seferGetir(genel.tarihCevir(Request.Form["txtGidisTarihi"].ToString()), true);
                            dlGidisSeferler.DataSource = dtSeferGidis;
                            dlGidisSeferler.DataBind();
                            seferGetir(genel.tarihCevir(Request.Form["txtDonusTarihi"].ToString()), false);
                            dlDonusSeferler.DataSource = dtSeferDonus;
                            dlDonusSeferler.DataBind();
                        }
                    }
                }
                divMesaj.Visible = hata;
            }
            catch (Exception)
            {
                mesajTitle = "Opss... Bir Hata oluştu.";
                mesajText = "Lütfen daha sonra tekrar deneyiniz :)";
                hata = true;
                divMesaj.Visible = hata;
            }
        }
        bool BiletiKes(bool yon)
        {
            hata = false;
            DataTable dtSeferler = new DataTable();
            int seferID = 0;
            decimal ucret = 0;
            int koltukNo = 0, neredenSira = 0, nereyeSira = 0 , musteriID=0;
            try
            {
                musteriID = Convert.ToInt32(Session["musteriID"].ToString().Trim());
            }
            catch (Exception)
            {
                mesajTitle = "Session süreniz sona ermiş.";
                mesajText = "Lütfen tekrar deneyiniz.";
                hata = true;
            }
            if (!hata)
            {
                if (yon)
                {
                    koltukNo = Convert.ToInt32(Request.Form["txtGidisNo"].ToString().Trim());
                    seferID = gidisSeferID;
                    dtSeferler = dtSeferGidis;
                }
                else
                {
                    seferID = donusSeferID;
                    dtSeferler = dtSeferDonus;
                    koltukNo = Convert.ToInt32(Request.Form["txtDonusNo"].ToString().Trim());
                }
            }
            if (!hata)
            {
                try
                {
                    int biletiVarmi = (from bk in ent.bilet where bk.seferID == seferID && bk.musteriID == musteriID && bk.durum == true select bk).Count();
                    if (biletiVarmi > 0)
                    {
                        mesajTitle = "Seçilen seferde zaten biletiniz mevcut";
                        mesajText = "Eğer tekrar bilet almak istiyorsanız başka bir sefer seçiniz.Eğer koltuk değimi yapmak istiyorsanız biletiniz iptal edip tekrar alabilirsiniz.Var olan tüm biletlerinizi biletlerim sekmesinde bulabilirsiniz.";
                        hata = true;
                    }
                }
                catch (Exception)
                {
                    mesajTitle = "Opss... Bir Hata oluştu.";
                    mesajText = "Lütfen daha sonra tekrar deneyiniz :)";
                    hata = true;
                }
            }
            if (!hata)
            {
                foreach (DataRow dr in dtSeferler.Rows)
                {
                    if (Convert.ToString(dr["seferID"]) == seferID.ToString())
                    {
                        ucret = Convert.ToDecimal(dr["Fiyat"]);
                        neredenSira = Convert.ToInt32(dr["neredenSira"]);
                        nereyeSira = Convert.ToInt32(dr["nereyeSira"]);
                        break;
                    }
                }

                bilet b = new bilet();
                b.seferID = seferID;
                if (yon)
                {
                    if( rbBiletIslemTuruGidis.SelectedValue.ToString()=="1")
                        b.islem=true;
                    else
                        b.islem = false;
                }
                else
                {
                    if (rbBiletIslemTuruDonus.SelectedValue.ToString() == "1")
                        b.islem = true;
                    else
                        b.islem = false;
                }
                b.musteriID = musteriID;
                b.ucret = ucret;
                b.CalisanID = 7;
                b.alinmaTarihi = DateTime.Now;
                b.durum = true;
                ent.bilet.Add(b);
                int basla = 0, bitis = 0;
                if (neredenSira < nereyeSira)
                {
                    basla = neredenSira;
                    bitis = nereyeSira;
                }
                else
                {
                    bitis = neredenSira;
                    basla = nereyeSira;
                }
                for (int i = basla; i <= bitis; i++)
                {
                    koltuk k = new koltuk();
                    k.seferID = seferID;
                    k.musteriID = musteriID;
                    k.guzergahDurakSira = i;
                    k.koltukNo = koltukNo;
                    k.durum = true;
                    ent.koltuk.Add(k);
                }
                try
                {
                    ent.SaveChanges();
                    hata = false;
                }
                catch (Exception)
                {

                    hata = true;
                }
            }
            return hata;
        }
        bool koltukAlinmisMiKontrol(bool yon)
        {
            bool sonuc = false;
            if (satilanKoltuklariGetir(yon))
            {
                DataTable dtSatilanKoltuklar = new DataTable();
                string koltukNo = "";
                if (yon)
                {
                    koltukNo = Request.Form["txtGidisNo"].ToString().Trim();
                    dtSatilanKoltuklar = dtSatilanKoltuklarGidis;
                }
                else
                {
                    koltukNo = Request.Form["txtDonusNo"].ToString().Trim();
                    dtSatilanKoltuklar = dtSatilanKoltuklarDonus;

                }
                foreach (DataRow dr in dtSatilanKoltuklar.Rows)
                {
                    if (dr[0].ToString() == koltukNo)
                    {
                        sonuc = true;
                        break;
                    }
                }
            }
            return sonuc;

        }

        protected void btnBitir_Click(object sender, EventArgs e)
        {
            hata = false;
            if (!radioKontrol())
            {
                if (!seferAraHataKontrol())
                {
                    if (!biletSatisHataKontrol())
                    {
                        if (!loginKontrol())
                        {
                            if (!koltukAlinmisMiKontrol(true))
                            {
                                if (!BiletiKes(true))
                                {
                                    if (ciftYon)
                                    {
                                        if (!koltukAlinmisMiKontrol(false))
                                        {
                                            if (!BiletiKes(false))
                                                Response.Redirect("/biletlerim.aspx");
                                            else
                                            {
                                                mesajTitle = "Opss... Bir Hata oluştu.";
                                                mesajText = "Donüş biletiniz kesilirken hatalar meydana geldi.Lütfen Daha sonra tekrar deneyiniz...";
                                                hata = true;
                                            }
                                        }
                                        else
                                        {
                                            mesajTitle = "Opss... Biri sizden daha önce davrandı.";
                                            mesajText = "Satın almak istediğiniz dönüş seferindeki koltuk az önce satıldı...";
                                            hata = true;
                                        }
                                    }
                                    else
                                        Response.Redirect("/biletlerim.aspx");
                                }
                            }
                            else
                            {
                                mesajTitle = "Opss... Biri sizden daha önce davrandı.";
                                mesajText = "Satın almak istediğiniz gidiş seferindeki koltuk az önce satıldı...";
                                hata = true;
                            }
                        }
                        else
                            divLogin.Visible = true;
                    }
                }
            }
            divMesaj.Visible = hata;
        }
    }
}